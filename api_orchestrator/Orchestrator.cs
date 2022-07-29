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

namespace api_orchestrator
{
    public class Orchestrator
    {
        public string urlAPI = "";
        public string caminhoDiretorio = "";
        public string[] linhasArquivo = new string[2]; 

        public Orchestrator(string caminhoDiretorio)
        {
            this.LerArquivo(caminhoDiretorio);
        }

        public string Token { get; set; }

        public void LerArquivo(string arquivo)
        {
            this.linhasArquivo = File.ReadAllLines(arquivo);

            for(int i = 0; i < this.linhasArquivo.Length -1; i++)
            {
                this.urlAPI = this.linhasArquivo[0];
                this.caminhoDiretorio = this.linhasArquivo[1];
            }
        }

        public void GetToken(object body)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlAPI);
                var serializedInfo = JsonConvert.SerializeObject(body);
                var content = new StringContent(serializedInfo, Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{client.BaseAddress}user/authenticate", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var token = response.Content.ReadAsStringAsync();
                    token.Wait();
                    Token = JsonConvert.DeserializeObject(token.Result).ToString();
                }
                else
                {
                    throw new Exception(response.StatusCode + ": " + response.Content);
                }
            }
        }

        public async Task Get(string? bearer, string rota)
        {
            using (var client = new HttpClient())
            {
                //this.GetToken(body);
                try
                {
                    client.BaseAddress = new Uri(this.urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();

                    if(String.IsNullOrEmpty(bearer))
                    {
                        throw new Exception("Bearer token missing");
                    }

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
                    client.BaseAddress = new Uri(this.urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();

                    if (String.IsNullOrEmpty(bearer))
                    {
                        throw new Exception("Bearer token missing");
                    }

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
                    client.BaseAddress = new Uri(this.urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();

                    if (String.IsNullOrEmpty(bearer))
                    {
                        throw new Exception("Bearer token missing");
                    }

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                    HttpResponseMessage response;
                    StringContent content;
                    string serializedInfo = "";

                    if (body == null || body == "")
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
                    client.BaseAddress = new Uri(this.urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();

                    if (String.IsNullOrEmpty(bearer))
                    {
                        throw new Exception("Bearer token missing");
                    }

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
