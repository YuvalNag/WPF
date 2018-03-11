using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CurrencyLayerDotNet
{
    public partial class CurrencyLayerApi
    {
        static int reqCount = 0;
        private string _baseUrl;
        private string _queryUrl;
        private string _accessKey;
        private string _content;
        private HttpResponseMessage _response;
        private HttpClient _httpClient;

        public CurrencyLayerApi()
        {
            this._baseUrl = "http://apilayer.net/api/";
            this._accessKey = "1072679b68e6b7b81e9139872e0319c9";
        }
        public async Task<T> Invoke<T>(string path, Dictionary<string, string> postdata = null)
        {
            try
            {
                reqCount++;
                _httpClient = new HttpClient();

                this._queryUrl = (this._baseUrl + path + buildEndpointRoute(_accessKey, postdata));

                _response = await _httpClient.GetAsync(this._queryUrl);
                _response.EnsureSuccessStatusCode();
                _content = await _response.Content.ReadAsStringAsync();

                this._queryUrl = this._baseUrl;
                return JsonConvert.DeserializeObject<T>(_content);
            }
            catch
            {
                return default(T);
            }
           
        }
        private string buildEndpointRoute(string key, Dictionary<string, string> parameters)
        {

            if(parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("access_key", key);
            parameters.Add("format", "1");

            UriBuilder uriBuilder = new UriBuilder();
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var urlParameter in parameters)
            {
                query[urlParameter.Key] = urlParameter.Value;
            }
            uriBuilder.Query = query.ToString();
            return uriBuilder.Query;

        }
    }
}
