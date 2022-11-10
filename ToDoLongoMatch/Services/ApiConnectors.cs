using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;
using System.Globalization;
using System.Configuration;
using TodoApi.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ToDoLongoMatch.Services
{
    public class ApiConnectors
    {
       static string URLAPI = "http://10.0.2.2:8080/api/todo/";
        HttpClient client = new HttpClient();
        Uri ApiUri;
        public ApiConnectors()
        {
            ApiUri = new Uri(URLAPI);
        }

     

        public async Task <IEnumerable<TodoItem>> GetAll()
        {
            client = new HttpClient();
            client.BaseAddress = ApiUri;
            var ListItems=new List<TodoItem>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response =  await client.GetAsync("").ConfigureAwait(false); 
            if (response.IsSuccessStatusCode)
            {
                 ListItems = (response.Content.ReadFromJsonAsync<List<TodoItem>>()).Result;
            }
            return  ListItems;

        }
        public async Task Remove(TodoItem item)
        {
            client = new HttpClient();
            client.BaseAddress = ApiUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.DeleteAsync("Delete?id="+item.Key);

            //Task<IEnumerable<TodoItem>?> ListItems = (response.Content.ReadFromJsonAsync<IEnumerable<TodoItem>>());
            return;
        }
        public async Task Create(TodoItem item)
        {
            client = new HttpClient();
            client.BaseAddress = ApiUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Create", content);

          
            return;
        }
        public async Task Update(string Id,TodoItem item)
        {
            client = new HttpClient();
            client.BaseAddress = ApiUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Update?id="+Id, content);

            return;
        }
    }
}
