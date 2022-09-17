using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace FWTest.Controllers
{
    public class SearchController : ApiController
    {
        public class ApiResults
        {
            public string ApiType { get; set; }
            public string ApiResult { get; set; }
        }

        // GET: api/Search
        [HttpGet]
        [Route("api/Search")]
        public async Task<ApiResults> Get(string query)
        {
            string url1 = "Https://api.chucknorris.io/jokes/";
            string url2 = "https://swapi.dev/api/people/?search?query=";

            List<string> EmpInfo = new List<string>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(url1);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var client2 = new HttpClient();
            client2.BaseAddress = new Uri(url2);
            client2.DefaultRequestHeaders.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string result = string.Empty;
            var Auth = string.Empty;
            HttpResponseMessage Res = await client.GetAsync("search?query=" + query);

            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                JsonData data = JsonMapper.ToObject(EmpResponse);
                Auth = Res.RequestMessage.RequestUri.Authority;
                result = data.ToJson();
            }

            HttpResponseMessage Res2 = await client2.GetAsync(query);
            if (Res2.IsSuccessStatusCode)
            {

                var EmpResponse = Res2.Content.ReadAsStringAsync().Result;
                Auth = Res2.RequestMessage.RequestUri.Authority;
                JsonData data = JsonMapper.ToObject(EmpResponse);
                result = data.ToJson();

            }

            ApiResults resu = new ApiResults
            {
                ApiType = Auth,
                ApiResult = result
            };

            return resu;
        }
    }
}
