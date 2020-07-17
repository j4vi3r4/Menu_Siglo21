

namespace Menu_Siglo21.Services
{
    using GalaSoft.MvvmLight.Messaging;
    using Menu_Siglo21.Model;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Enciende tu Internet",
                };
            }
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No Hay Conexión Con el Servidor",
                };
            }
            return new Response
            {
                IsSuccess = true,
            };
        }
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                var url = $"{ prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list, 
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
            
        }

        public async Task<Response> PostUpdate<T>(string jsonObject, string urlBase, string prefix, string controller)
        {
            try
            {
                // Crea un objeto que maneje la conexión HTTP
                HttpClient socket = new HttpClient();

                //Parsea la url en solo un string
                string url = $"{urlBase}{prefix}{controller}";

                //Convierte el json en el body
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                //Ejecuta la conexión a la API como POST
                var response = await socket.PostAsync(url, content);
                //Debug.WriteLine("----> POST response: " + response.ToString());

                //Parsea la respuesta como string
                string answer = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("----> POST awser: " + answer.ToString());

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                // Crea una definición para hacer el parseo a .NET
                // Tiene la misma estructura que el resultado de la API
                // Como puede variar el resultado según el post que se aplique, 
                // este var se puede pasar como param para así hacer este método
                // multifuncional
                var definition = new { result = "" };

                // Convierte el resultado en el formato dado anteriormente a un objeto
                var resultado = JsonConvert.DeserializeAnonymousType(answer, definition);

                // Muestra de que se parseó correctamente a objeto
                Debug.WriteLine("----> Result: " + resultado.result);


                //var obj = JsonConvert.DeserializeObject(answer);
                return new Response
                {
                    IsSuccess = true,
                    // En caso de que se mande la definition como param, se deberá obtener 
                    // un Diccionario, por que no se puede pasar el objeto solo
                    Result = resultado.result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }        

    }


    }
}
