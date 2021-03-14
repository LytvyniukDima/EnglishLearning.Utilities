using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnglishLearning.Utilities.Http.Extensions
{
    public static class EnglishLearningHttpClientExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        
        public static async Task<T> GetAsync<T>(this HttpClient client, Uri url)
        {
            using var response = await client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<T>(responseStream, JsonOptions);
        }

        public static async Task PostAsync<T>(
            this HttpClient httpClient, 
            Uri url,
            T body)
        {
            var serializedBody = JsonSerializer.Serialize(body);
            var content = new StringContent(serializedBody, Encoding.UTF8, "application/json");
            
            using var response = await httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
        }
        
        public static async Task<TResponse> PostAsync<TRequest, TResponse>(
            this HttpClient httpClient,
            Uri url,
            TRequest body)
        {
            var serializedBody = JsonSerializer.Serialize(body);
            
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new StringContent(serializedBody, Encoding.UTF8, "application/json");
            using var response = await httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
            
            await using var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<TResponse>(responseStream, JsonOptions);
        }
    }
}