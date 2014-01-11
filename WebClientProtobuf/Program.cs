using System;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApiContrib.Formatting;
using WebAPiProtobuf.Models;

namespace WebClientProtobuf
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient {BaseAddress = new Uri("http://localhost:49916/")};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

            HttpResponseMessage response = client.GetAsync("api/Values/4").Result; 
            
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var p = response.Content.ReadAsAsync<ProtobufModelDto>(new[] { new ProtoBufFormatter()}).Result;
                Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.StringValue, p.Id);               
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.ReadLine();
        }        
    }
}
