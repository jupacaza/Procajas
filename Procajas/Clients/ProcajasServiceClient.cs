using Newtonsoft.Json;
using Procajas.Contracts;
using Procajas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Procajas.Clients
{
    public class ProcajasServiceClient
    {
        private readonly string BaseAddressString = ConfigurationManager.AppSettings["ida:ProcajasApiEndpoint"];
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
        public async Task<ProcessResource> GetProcessResourceByDepartmentAndId(string department, string id)
        {
            string requestUrl = string.Format("process/{0}/{1}", department, id);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                ProcessResource processResource;
                if (response.IsSuccessStatusCode)
                {
                    processResource = JsonConvert.DeserializeObject<ProcessResource>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    this.Log(response);
                    processResource = null;
                }

                return processResource;
            }
        }

        public async Task<WarehouseResource> GetWarehouseResourceByDepartmentAndId(string department, string id)
        {
            string requestUrl = string.Format("warehouse/{0}/{1}", department, id);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                WarehouseResource warehouseResource;
                if (response.IsSuccessStatusCode)
                {
                    warehouseResource = JsonConvert.DeserializeObject<WarehouseResource>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    this.Log(response);
                    warehouseResource = null;
                }

                return warehouseResource;
            }
        }
        #endregion

        #region List
        public async Task<List<string>> ListAdminItemsByType(string type)
        {
            string requestUrl = string.Format("adminitem/{0}", type);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                List<string> adminItems;
                if (response.IsSuccessStatusCode)
                {
                    List<AdminItemResource> contractList = JsonConvert.DeserializeObject<List<AdminItemResource>>(await response.Content.ReadAsStringAsync());
                    adminItems = contractList.Select(resource => resource.Name).ToList();
                }
                else
                {
                    this.Log(response);
                    adminItems = new List<string>();
                }

                return adminItems;
            }
        }

        public async Task<List<ProcessResource>> ListProcessResourcesByDepartment(string department)
        {
            string requestUrl = string.Format("process/{0}", department);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                List<ProcessResource> processResources;
                if (response.IsSuccessStatusCode)
                {
                    processResources = JsonConvert.DeserializeObject<List<ProcessResource>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    this.Log(response);
                    processResources = new List<ProcessResource>();
                }

                return processResources;
            }
        }

        public async Task<List<WarehouseResource>> ListWarehouseResourcesByDepartment(string department)
        {
            string requestUrl = string.Format("warehouse/{0}", department);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                List<WarehouseResource> warehouseResources;
                if (response.IsSuccessStatusCode)
                {
                    warehouseResources = JsonConvert.DeserializeObject<List<WarehouseResource>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    this.Log(response);
                    warehouseResources = new List<WarehouseResource>();
                }

                return warehouseResources;
            }
        }
        #endregion

        #region Update
        public async Task<string> PutProcess(string id, ProcessResource resource)
        {
            string requestUrl = string.Format("process/{0}", id);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUrl))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    IdResource idResource = JsonConvert.DeserializeObject<IdResource>(await response.Content.ReadAsStringAsync());
                    return idResource.Id;
                }
                else
                {
                    this.Log(response);
                    return null;
                }
            }
        }

        public async Task<string> PutWarehouse(string id, WarehouseResource resource)
        {
            string requestUrl = string.Format("warehouse/{0}", id);
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUrl))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, JsonMediaType);

                HttpResponseMessage response = await this.httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    IdResource idResource = JsonConvert.DeserializeObject<IdResource>(await response.Content.ReadAsStringAsync());
                    return idResource.Id;
                }
                else
                {
                    this.Log(response);
                    return null;
                }
            }
        }        
        #endregion

        private void Log(HttpResponseMessage response)
        {
            // TODO: Log the error.
            return;
        }
    }
}
