using CrmStepMobileApp.Helper;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{

   public class HttpClientBase :ApiBase
    {
        public string BaseUrl = "https://api.crmstep.com/";

        public async Task<T> Post<T>(string endpoint, string jsonobject, string accessToken = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string resultStr = "";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.BaseAddress = new Uri(BaseUrl);

                    if (accessToken != "")
                    {
                        var res = StaticData.IsSessionExpired();
                        if (res)
                        {
                            var newTkn = await StaticData.RefreshToken();
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + newTkn);
                        }
                        else
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    }
                    response = await httpClient.PostAsync(endpoint, new StringContent(jsonobject, Encoding.UTF8, "application/json"));
                    resultStr = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(resultStr);
                }


            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>endpoint: \n" + JsonConvert.SerializeObject(endpoint) + "\n";
                logFile += "==>access token: \n" + JsonConvert.SerializeObject(accessToken) + "\n";
                logFile += "==>API response: \n" + JsonConvert.SerializeObject(response) + "\n";
                logFile += "==>API response JSON: \n" + JsonConvert.SerializeObject(resultStr) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: API call"), dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: HttpClientBase\n" +
                                            $"endpoint: {endpoint}\n" +
                                            $"Exception: API call\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
                throw;
            }

        }
        public async Task<T> Get<T>(string endpoint, string accessToken = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string resultStr = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.BaseAddress = new Uri(BaseUrl);
                    if (accessToken != "")
                    {
                        var res = StaticData.IsSessionExpired();
                        if (res)
                        {
                            var newTkn = await StaticData.RefreshToken();
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + newTkn);
                        }
                        else
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    }
                    response = await httpClient.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    resultStr = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<T>(resultStr);
                    return json;
                }
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>endpoint: \n" + JsonConvert.SerializeObject(endpoint) + "\n";
                logFile += "==>access token: \n" + JsonConvert.SerializeObject(accessToken) + "\n";
                logFile += "==>API response: \n" + JsonConvert.SerializeObject(response) + "\n";
                logFile += "==>API response JSON: \n" + JsonConvert.SerializeObject(resultStr) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: API call"), dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: HttpClientBase\n" +
                                            $"endpoint: {endpoint}\n" +
                                            $"Exception: API call\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
                throw;
            }


        }

        public async Task<T> Delete<T>(string endpoint, string accessToken = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string resultStr = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.BaseAddress = new Uri(BaseUrl);
                    if (accessToken != "")
                    {
                        var res = StaticData.IsSessionExpired();
                        if (res)
                        {
                            var newTkn = await StaticData.RefreshToken();
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + newTkn);
                        }
                        else
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    }
                    response = await httpClient.DeleteAsync(endpoint).ConfigureAwait(false);
                    resultStr = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<T>(resultStr);
                    return json;
                }
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>endpoint: \n" + JsonConvert.SerializeObject(endpoint) + "\n";
                logFile += "==>access token: \n" + JsonConvert.SerializeObject(accessToken) + "\n";
                logFile += "==>API response: \n" + JsonConvert.SerializeObject(response) + "\n";
                logFile += "==>API response JSON: \n" + JsonConvert.SerializeObject(resultStr) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: API call"), dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: HttpClientBase\n" +
                                            $"endpoint: {endpoint}\n" +
                                            $"Exception: API call\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
                throw;
            }


        }

        public async Task<string> GetJson(string endpoint, string accessToken = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string resultStr = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.BaseAddress = new Uri(BaseUrl);
                    if (accessToken != "")
                    {
                        var res = StaticData.IsSessionExpired();
                        if (res)
                        {
                            var newTkn = await StaticData.RefreshToken();
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + newTkn);
                        }
                        else
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    }
                    response = await httpClient.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    resultStr = await response.Content.ReadAsStringAsync();
                    return resultStr;
                }
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>endpoint: \n" + JsonConvert.SerializeObject(endpoint) + "\n";
                logFile += "==>access token: \n" + JsonConvert.SerializeObject(accessToken) + "\n";
                logFile += "==>API response: \n" + JsonConvert.SerializeObject(response) + "\n";
                logFile += "==>API response JSON: \n" + JsonConvert.SerializeObject(resultStr) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: API call"), dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: HttpClientBase\n" +
                                            $"endpoint: {endpoint}\n" +
                                            $"Exception: API call\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
            }
            return null;


        }



        public async Task<byte[]> GetStream(string endpoint, string html)
        {
            try
            {
                var form = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("html", html)
                });

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.BaseAddress = new Uri("https://api.html2pdf.app/");

                    //var encodedItems = form.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
                    //var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");

                    var response = await httpClient.PostAsync(endpoint, form);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }
                    var result = await response.Content.ReadAsByteArrayAsync();
                    //     var mybytearray = Convert.FromBase64String(result);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public async Task<byte[]> GetBytes(string endpoint, string accessToken = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.BaseAddress = new Uri(BaseUrl);
                    if (accessToken != "")
                    {
                        var res = StaticData.IsSessionExpired();
                        if (res)
                        {
                            var newTkn = await StaticData.RefreshToken();
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + newTkn);
                        }
                        else
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    }
                    response = await httpClient.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    var result = await response.Content.ReadAsByteArrayAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>endpoint: \n" + JsonConvert.SerializeObject(endpoint) + "\n";
                logFile += "==>access token: \n" + JsonConvert.SerializeObject(accessToken) + "\n";
                logFile += "==>API response: \n" + JsonConvert.SerializeObject(response) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: API call"), dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: HttpClientBase\n" +
                                            $"endpoint: {endpoint}\n" +
                                            $"Exception: API call\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
                throw;
            }


        }


        public async Task<T> Upload<T>(string endpoint, byte[] file, string name, string accessToken = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string resultStr = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseUrl);
                    if (accessToken != "")
                    {
                        var res = StaticData.IsSessionExpired();
                        if (res)
                        {
                            var newTkn = await StaticData.RefreshToken();
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + newTkn);
                        }
                        else
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    }
                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StreamContent(new MemoryStream(file)), "file", name);
                        response = await httpClient.PostAsync(endpoint, content);
                        resultStr = await response.Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<T>(resultStr);
                        return json;

                    }
                }
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>endpoint: \n" + JsonConvert.SerializeObject(endpoint) + "\n";
                logFile += "==>access token: \n" + JsonConvert.SerializeObject(accessToken) + "\n";
                logFile += "==>API response: \n" + JsonConvert.SerializeObject(response) + "\n";
                logFile += "==>API response JSON: \n" + JsonConvert.SerializeObject(resultStr) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: API call"), dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: HttpClientBase\n" +
                                            $"endpoint: {endpoint}\n" +
                                            $"Exception: API call\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
                throw;
            }
        }

        public byte[] GetBytes(string url)
        {
            var bytes = new WebClient().DownloadData(url);
            return bytes;
        }
    }
}
