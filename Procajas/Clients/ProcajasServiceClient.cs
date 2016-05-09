using Newtonsoft.Json;
using Procajas.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Procajas.Clients
{
    public class ProcajasServiceClient
    {
        private const string BaseAddressString = @"https://procajas.scm.azurewebsites.net/";
        private const string JsonMediaType = @"application/json";

        public const string ClientError = "ClientError";

        public ProcajasServiceClient()
        {
            this.httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddressString)
            };
        }

        private HttpClient httpClient { get; }

        public async Task<List<string>> PostDiscrepancyResource(IEnumerable<DiscrepancyResource> resources)
        {
            using(HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(JsonConvert.SerializeObject(resources), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    List<string> idList = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());

                    return idList;
                }
                else
                {
                    // TODO: handle Conflicts and/or retry logic
                    // TODO: return error codes to the app somehow
                    return new List<string>() { ClientError };
                }
            }
        }
    }
}
