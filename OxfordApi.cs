using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using jsonhelp.models;
using Newtonsoft.Json;

namespace jsonhelp
{
    public class OxfordApi
    {
        private string _baseUrl;
        private string _apiKey;
        private string _appId;      

        public OxfordApi(string id, string key)
        {
            _baseUrl = "https://od-api.oxforddictionaries.com/api/v2";
            _appId   = id;
            _apiKey  = key;
        }

        private string GetAPIRequest(string url, string sourceLang = "en")
        {            
            string response;            
            url = $"{_baseUrl}{url}";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("app_id", _appId);                
                httpClient.DefaultRequestHeaders.Add("app_key", _apiKey);                                                
                            
                response = httpClient.GetStringAsync(url).Result;                
            }
            
            return response;
        }

        public string DefineOxford(string word, string sourceLang = "en-us")
        {
            string url = string.Empty;
            var define = new OxfordDefine();
            try
            {
                 url = $"/words/{sourceLang}?q={word}&fields=definitions";
                 define = JsonConvert.DeserializeObject<OxfordDefine>(GetAPIRequest(url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return define.Results[0].LexicalEntries[0].Entries[0].Senses[0].Definitions[0];
        }
    }
}