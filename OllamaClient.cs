namespace OllamaIntegrationApp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class OllamaClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _modelName;
        private readonly Logger _logger;

        public OllamaClient(string baseUrl, string modelName, Logger logger)
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(10) // Increased timeout to 10 minutes
            };
            _baseUrl = baseUrl;
            _modelName = modelName;
            _logger = logger;
        }


        public async Task<string> GetEmbeddingsAsync(string text)
        {
            var requestUrl = $"{_baseUrl}/embeddings";
            var payload = new { model = "nomic-embed-text", prompt = text };

            _logger.Log($"Sending embedding request to {requestUrl} with text: {text}");
            var content = new StringContent(JObject.FromObject(payload).ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.Log($"Embedding request failed with status code {response.StatusCode}: {errorContent}");
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.Log("Embedding response received successfully");
            return responseContent;
        }

        public async Task<string> GetChatResponseAsync(string prompt)
        {
            var requestUrl = $"{_baseUrl}/generate";
            var payload = new { model = _modelName, prompt = prompt };

            _logger.Log($"Sending chat request to {requestUrl} with prompt: {prompt}");
            var content = new StringContent(JObject.FromObject(payload).ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.Log("Chat response received successfully");
            return responseContent;
        }
    }
}
