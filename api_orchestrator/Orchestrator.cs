using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Microsoft.Identity.Web;
using System.Runtime.Serialization.Json;

namespace api_orchestrator
{
    public class Orchestrator
    {
        public string urlAPI = "";
        public string caminhoDiretorio = "";
        public string[] linhasArquivo = new string[2];
        public object jsonFile;
        public string file;

        public Orchestrator()
        {
            this.file = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"config.json");
            this.jsonFile = JsonConvert.DeserializeObject(file);          
        }

        public string Token { get; set; }

        public async Task Get(string? bearer, string rota)
        {
            using (var client = new HttpClient())
            {
                
                try
                {
                    dynamic json = this.jsonFile;
                    client.BaseAddress = new Uri(json.urlApi.ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                    var response = await client.GetAsync($"{client.BaseAddress}{rota}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(response.StatusCode + ": " + response.Content);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
            }
        }

        public async Task Post(string? bearer, object? body, string rota)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    dynamic json = this.jsonFile;
                    client.BaseAddress = new Uri(json.urlApi.ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                    var serializedInfo = JsonConvert.SerializeObject(body);
                    var content = new StringContent(serializedInfo, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{client.BaseAddress}{rota}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(response.StatusCode + ": " + response.Content);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task Put(string? bearer, object? body, string rota)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    dynamic json = this.jsonFile;
                    client.BaseAddress = new Uri(json.urlApi.ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                    HttpResponseMessage response;
                    StringContent content;
                    string serializedInfo = "";

                    if (body == null || body == String.Empty)
                    {
                        serializedInfo = JsonConvert.SerializeObject(body);
                        body = new StringContent(serializedInfo, Encoding.UTF8, "application/json");
                        response = await client.PutAsJsonAsync($"{client.BaseAddress}{rota}", body);
                    }
                    else
                    {
                        serializedInfo = JsonConvert.SerializeObject(body);
                        content = new StringContent(serializedInfo, Encoding.UTF8, "application/json");
                        response = await client.PutAsync($"{client.BaseAddress}{rota}", content);
                    }

                    
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(response.StatusCode + ": " + response.Content);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task Delete(string? bearer, string rota)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    dynamic json = this.jsonFile;
                    client.BaseAddress = new Uri(json.urlApi.ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
    
                    var response = await client.DeleteAsync($"{client.BaseAddress}{rota}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(response.StatusCode + ": " + response.Content);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

    }
}
