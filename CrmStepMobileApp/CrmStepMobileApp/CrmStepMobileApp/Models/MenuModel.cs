using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    public class MenuModel
    {
        public MenuModel(int id, string icon, string title)
        {
            Id = id;
            Icon = icon;
            Title = title;
        }

        public int Id { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }

    }
}
