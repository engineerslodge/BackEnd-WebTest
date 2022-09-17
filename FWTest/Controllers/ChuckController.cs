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
    public class ChuckController : ApiController
    {


        [HttpGet]
        [Route("api/Chuck/Categories")]
        public async Task<IEnumerable<string>> Get()
        {
            string Baseurl = "Https://api.chucknorris.io/jokes/";
            List<string> EmpInfo = new List<string>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource getAllCategories using HttpClient
                HttpResponseMessage Res = await client.GetAsync("categories");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<string>>(EmpResponse);
                }
                //returning the employee list to view
                return EmpInfo;
            }
        }
       
    }
}
