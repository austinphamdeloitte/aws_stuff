using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using aws_stuff.models;
using Microsoft.Extensions.DependencyInjection;

namespace aws_stuff.controllers {
    public class APIController {
        private HttpClient _client;
        public APIController(IServiceProvider serviceProvider) {
            _client = serviceProvider.GetService<HttpClient>();
        }
        public async Task<PersonDTO> FetchUserFactory(string url) {
            HttpResponseMessage res = await _client.GetAsync(url);
            if (res.IsSuccessStatusCode) {
                RandomAPITemplate jsonDTO = JsonConvert.DeserializeObject<RandomAPITemplate>(
                    await res.Content.ReadAsStringAsync()
                );
                // Question: HOW TO PASS INTERFACE IN GENERIC TYPE
                ResultTemplate result = jsonDTO.Results[0];
                return result.User;
            }
            return null;
        }
    }
}


