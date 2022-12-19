using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SmartParking.Domain;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SmartParking.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Map : ControllerBase
    {
        IConfiguration configuration;
        public Map(IConfiguration iconfig)
        {
            configuration = iconfig;
        }
        [HttpGet]
        public Search GetMap(string id)
        {            
            string apiURL = configuration.GetSection("AzureAPI").Value;
            string url = string.Format(apiURL, id);
            Search searchResult = new Search();
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(url);
                HttpResponseMessage response = client.GetAsync(url).Result;
                if(response.IsSuccessStatusCode)
                {
                    string resp= response.Content.ReadAsStringAsync().Result;
                    searchResult.SearchResult = resp;

                    JObject json= JObject.Parse(resp);
                    var lat = json["results"][0]["position"]["lon"].ToString();
                    var searchKeyword=lat.Substring(0, lat.IndexOf("."));
                    //Getting data from database
                    ParkingDBConetxt parkingDBConetxt = new ParkingDBConetxt();
                    var parkings = parkingDBConetxt.ParkingSensorData.Where(x => x.SensorLocation.StartsWith(searchKeyword) && x.OccupancyState.Equals("Vacant"));
                    searchResult.Parkings = parkings;
                }
            }
           
            return searchResult;
        }
    }
}
