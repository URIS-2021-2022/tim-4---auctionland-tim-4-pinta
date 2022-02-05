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
    public class FileAComplaintService : IFileAComplaintService
    {
        private readonly IConfiguration configuration;

        public FileAComplaintService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool ConnectLogger(LogModel model)
        {
            using (HttpClient client = new HttpClient())
            {
              
                Uri url = new ($"{ configuration["Services:LoggerAggregate"] }api/logger");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
        }

            public bool FileAComplaint(Guid kupacId)
            {
                using (HttpClient client = new HttpClient())
                {
                  
                    Uri url = new Uri($"{ configuration["Services:KupacMikroservis"] }api/kupac/{kupacId}");

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


