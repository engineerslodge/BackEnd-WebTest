using LitJson;
using Newtonsoft.Json;
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
    public class SwapiController : ApiController
    {
        [HttpGet]
        [Route("api/swapi/people/")]
        public async Task<string> People()
        {
            string Baseurl = "https://swapi.dev/api/people/";
            string result = string.Empty;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("");

                if (Res.IsSuccessStatusCode)
                {

                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    JsonData data = JsonMapper.ToObject(EmpResponse);
                    result = data.ToJson();
                    // Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
                    // EmpInfo = JsonConvert.DeserializeObject<dataDictionary>(data.ToJson());
                }

                return result;
            }
        }


    }
}
