using CrmStepMobileApp.CustomControls;
using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.ViewModels;
using CrmStepMobileApp.Views.PopupView;
using FFImageLoading.Forms;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CrmStepMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        AddPageViewModel viewModel = App.Locator.AddPageViewModel;
        string _id;
        public AddPage(string  id="",bool showSubmit=true)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.SectionsList = new ObservableCollection<DetailSectionModel>();
            viewModel.Id = "";
            NavigationPage.SetHasNavigationBar(this, false);
            _id = id;

            viewModel.Initilize(_id, showSubmit);
            App.SetPagePadding((Grid)Content);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

        }

        private async void DatePicker_Tapped(object sender, EventArgs e)
        {

            try
            {
                if (sender is StackLayout stackLayout)
                {
                    if (stackLayout.BindingContext is DetailModel field)
                    {
                        var innerStackLayout = (Grid)stackLayout.Children[1];
                        var label = (CustomLabel)innerStackLayout.Children[0];
                        CalendarPopupView popup;
                        if (String.IsNullOrEmpty(label.Text))
                        {
                            popup = new CalendarPopupView();
                            popup.viewModel.SelectedDate = DateTime.MinValue;
                        }
                        else
                        {
                            popup = new CalendarPopupView(new List<DateTime>() { DateTime.ParseExact(label.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) });
                            popup.viewModel.SelectedDate = DateTime.ParseExact(label.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        }


                        popup.Disappearing += (s, ee) =>
                        {
                            //var innerStackLayout = (Grid)stackLayout.Children[1];
                            //var label = (Label)innerStackLayout.Children[0];

                            if (popup.viewModel.SelectedDate == DateTime.MinValue)
                            {
                                return;
                            }

                            label.Text = popup.viewModel.SelectedDate.ToString(StaticData.SettingsModel.date_format);
                            field.TextValue = popup.viewModel.SelectedDate.ToString("yyyy-MM-dd hh:mm:ss");
                            field.TextValueDateTime = popup.viewModel.SelectedDate;
                        };
                        await PopupNavigation.Instance.PushAsync(popup);

                        //var datePi cker = (DatePicker)stackLayout.Children[2];
                        //datePicker.Unfocused += (s, ee) =>
                        //{
                        //    var innerStackLayout = (Grid)stackLayout.Children[1];
                        //    var label = (Label)innerStackLayout.Children[0];
                        //    label.Text = datePicker.Date.ToString(StaticData.SettingsModel.date_format);
                        //    field.TextValue = datePicker.Date.ToString("yyyy-MM-dd hh:mm:ss");

                        //};
                        //stackLayout.Children.Add(datePicker);
                        //  datePicker.Focus();
                    }
                }
            }
            catch(Exception ex)
            {

            }
         
        }

        private async void Select_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is StackLayout stackLayout)
                {
                    if (stackLayout.BindingContext is DetailModel field)
                    {
                        var innerStackLayout = (Grid)stackLayout.Children[1];
                        var label = (CustomLabel)innerStackLayout.Children[ 0];

                        var result = await viewModel.ShowActionSheet(field.translatedTitle, field.choices.ToList());
                        if (result >= 0)
                        {

                            label.Text = field.choices[result];
                            field.TextValue = field.choices[result];
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }

           
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //if (sender is CheckBox checkBox)
            //{
            //    if (checkBox.BindingContext is string str)
            //    {
            //        if (checkBox.Parent.BindingContext is DetailModel field)
            //        {
            //            if (e.Value)
            //            {
            //                field.Selectedchoices.Add(str);
            //            }
            //            else
            //            {
            //                field.Selectedchoices.Remove(str);
            //            }
            //        }

            //    }
            //}
        }

        private async void CustomEditor_Focused(object sender, FocusEventArgs e)
        {
            if (sender is CustomEditor customEditor)
            {
                if (customEditor.Parent is StackLayout stack)
                {
                    MainStack.Padding = new Thickness(0, 0, 0, 300);
                    await MainScroll.ScrollToAsync(MainScroll.ScrollX, MainScroll.ScrollY + 300, false);

                }
            }
        }

        private void CustomEditor_Unfocused(object sender, FocusEventArgs e)
        {
            if (sender is CustomEditor customEditor)
            {
                if (customEditor.Parent is StackLayout stack)
                {
                    MainStack.Padding = new Thickness(0);

                }
            }
        }

        private async void Photo_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is CachedImage cachedImage)
                {
                    if (cachedImage.BindingContext is DetailModel field)
                    {
                        var result = await viewModel.MediaService.SelectSource();
                        if (result != null)
                        {
                            //field.TextValue = "https://dev.crmstep.com/UploadedFiles/UploadedFiles/ContactFiles/5de9039216f5e53f44535ed3//2019111321359.png";
                            field.FileBytesStr = "data:image/png;base64," + Convert.ToBase64String(result.Item3);
                            // field.TextValue = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".png";
                            cachedImage.Source = ImageSource.FromStream(() => new MemoryStream(result.Item3));

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public bool IsItemSelected { get; set; }
        private async void SearchItem_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is ContentView contentView)
                {


                    if (contentView.BindingContext is SearchModel searchModel)
                    {

                        if (contentView.Parent.BindingContext is DetailModel field)
                        {
                            IsItemSelected = true;
                            field.TextValue = searchModel.Name;
                            field.SelectedSearchModel = searchModel;
                            field.SearchList.Clear();
                            IsItemSelected = false;

                        }


                    }
                }
            }
            catch (Exception ex)
            {

            }



        }

        private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (IsItemSelected) return;

                if (sender is CustomEntry customEntry)
                {
                    if (customEntry.BindingContext is DetailModel field)
                    {

                        await Task.Delay(500);
                        if (e.NewTextValue == customEntry.Text)
                        {
                            int formType = 0;
                            if (StaticData.CurrentFormType == 2)
                            {
                                formType = 3;
                            }
                            else if (StaticData.CurrentFormType == 2)
                            {
                                formType = 2;

                            }
                            else if (StaticData.CurrentFormType == 4)
                            {
                                if(field.controlId== "client-contact")
                                {
                                    formType = 2;
                                }
                                else if(field.controlId == "client-company")
                                {
                                    formType = 3;
                                }
                            }


                            var searchApiModel = await viewModel.DetailsService.Search( formType.ToString(), customEntry.Text);
                            if (searchApiModel.status == 200)
                            {
                                if (searchApiModel.data.list != null)
                                {
                                    //if(customEntry.Parent is ScrollView scrollView)
                                    //{

                                    //}
                                    field.SearchList = new System.Collections.ObjectModel.ObservableCollection<SearchModel>();
                                    searchApiModel.data.list.ForEach(s => field.SearchList.Add(s));
                                }
                            }
                            else
                            {
                                field.SearchList = new System.Collections.ObjectModel.ObservableCollection<SearchModel>();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private async void SearchEntry_Focused(object sender, FocusEventArgs e)
        {
            if (sender is CustomEntry customEntry)
            {
                //   MainStack.Padding = new Thickness(0, 0, 0, 300);
                //var stack=   (StackLayout)customEntry.Parent;
                // var xx=  stack.Y;

                await MainScroll.ScrollToAsync(MainScroll.ScrollX, MainScroll.ScrollY + 250, false);
            }


        }

        private async void NumberEntry_Focused(object sender, FocusEventArgs e)
        {
            if (sender is CustomEntry customEntry)
            {
                if (customEntry.BindingContext is DetailModel field)
                {
                    var val = field.TextValue;
                    if (!String.IsNullOrEmpty(val))
                    {
                        field.TextValue = Convert.ToString(Convert.ToDouble(val));
                    }
                }
            }
        }

        private void NumberEntry_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (sender is CustomEntry customEntry)
                {
                    if (customEntry.BindingContext is DetailModel field)
                    {
                        var val = field.TextValue;
                        if (!String.IsNullOrEmpty(val))
                        {
                            field.TextValue = System.Convert.ToDouble(string.Format("{0:F2}", val)).ToString("N");
                        }



                        // field.TextValue = Convert.ToString(Convert.ToDouble(val));
                    }
                }

            }
            catch (Exception ex)
            {
               // viewModel.ShowAlert("")
            }

        }
    }
}