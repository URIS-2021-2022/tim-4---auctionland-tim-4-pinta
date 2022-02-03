using ComplaintAggregate.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComplaintAggregate.ServiceCalls
{
    public class FileAComplaintService :IFileAComplaintService
    {
        private readonly IConfiguration configuration;

        public FileAComplaintService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool FileAComplaint(BuyerDTO buyer)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:KupacMikroservis"];
                Uri url = new Uri($"{ configuration["Services:KupacMikroservis"] }api/kupac");

            //    HttpContent content = new StringContent(JsonConvert.SerializeObject(buyer));
              //  content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.GetAsync(url).Result;
                
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
