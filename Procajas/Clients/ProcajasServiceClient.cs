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
        private const string BaseAddressString = @"https://procajas.scm.azurewebsites.net/api/";
        private const string JsonMediaType = @"application/json";

        public ProcajasServiceClient()
        {
            this.httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddressString)
            };
        }

        private HttpClient httpClient { get; }

        #region Add
        public async Task<bool> PostAdminItem(AdminItemResource resource)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "adminitem"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    this.Log(response);
                    return false;
                }
            }
        }

        public async Task<List<string>> PostDiscrepancies(IEnumerable<DiscrepancyResource> resources)
        {
            using(HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "discrepancy"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(resources), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    List<string> idList = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());

                    return idList;
                }
                else
                {
                    this.Log(response);
                    return new List<string>();
                }
            }
        }

        public async Task<bool> PostFinishedProduct(FinishedProductResource resource)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "finishedproduct"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    this.Log(response);
                    return false;
                }
            }
        }

        public async Task<string> PostProcess(ProcessResource resource)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "process"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    IdResource id = JsonConvert.DeserializeObject<IdResource>(await response.Content.ReadAsStringAsync());

                    return id.Id;
                }
                else
                {
                    this.Log(response);
                    return string.Empty;
                }
            }
        }

        public async Task<string> PostWarehouse(WarehouseResource resource)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "warehouse"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    IdResource id = JsonConvert.DeserializeObject<IdResource>(await response.Content.ReadAsStringAsync());

                    return id.Id;
                }
                else
                {
                    this.Log(response);
                    return string.Empty;
                }
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAdminItem(string type, string name)
        {
            string requestUrl = string.Format("adminitem/{0}/{1}", type, name);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUrl))
            {
                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    this.Log(response);
                    return false;
                }
            }
        }
        #endregion

        #region Get
        #endregion

        #region List
        #endregion

        private void Log(HttpResponseMessage response)
        {
            // TODO: Log the error.
            return;
        }
    }
}
