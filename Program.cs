using OllamaIntegrationApp;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var apiBase = "http://localhost:11434/api";
        var modelName = "llama3.2:3b";
        var logger = new Logger("ollamalog");
        var client = new OllamaClient(apiBase, modelName, logger);

        try
        {
            // Loop for continuous chat
            while (true)
            {
                Console.WriteLine("Enter your text to get embeddings (or type 'exit' to quit):");
                var text = Console.ReadLine();
                if (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("Text cannot be null or empty.");
                    logger.Log("User entered an empty text for embeddings.");
                    continue;
                }

                if (text.ToLower() == "exit")
                {
                    break;
                }

                var embeddingsResponse = await client.GetEmbeddingsAsync(text);
                var readableEmbeddings = FormatEmbeddingsResponse(embeddingsResponse);
                Console.WriteLine($"Embeddings: {readableEmbeddings}");
                logger.Log($"Retrieved embeddings for text: {text}");

                Console.WriteLine("Enter your prompt for the chat (or type 'exit' to quit):");
                var prompt = Console.ReadLine();
                if (string.IsNullOrEmpty(prompt))
                {
                    Console.WriteLine("Prompt cannot be null or empty.");
                    logger.Log("User entered an empty prompt for the chat.");
                    continue;
                }

                if (prompt.ToLower() == "exit")
                {
                    break;
                }

                var chatResponse = await client.GetChatResponseAsync(prompt);
                var readableChatResponse = FormatChatResponse(chatResponse);
                Console.WriteLine($"CodeBot: {readableChatResponse}");
                logger.Log($"Received chat response for prompt: {prompt}");

                // Save chat history
                var chatHistory = $"{text}\n{prompt}\n{readableChatResponse}";
                logger.SaveChatHistory(chatHistory);
                logger.Log("Chat history saved.");
            }
        }
        catch (TimeoutException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            logger.Log($"Timeout error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            logger.Log($"General error: {ex.Message}");
        }
    }

    static string FormatEmbeddingsResponse(string embeddingsResponse)
    {
        try
        {
            // Process the embeddings response to make it readable
            var responseJson = JObject.Parse(embeddingsResponse);
            var embeddingArray = responseJson["embedding"]?.ToObject<float[]>();
            if (embeddingArray == null)
            {
                return "No embeddings available.";
            }

            // Log the embeddings array for debugging
            Console.WriteLine("Embeddings array: " + string.Join(", ", embeddingArray));

            return string.Join(", ", embeddingArray);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error formatting embeddings response: {ex.Message}");
            return "Error formatting embeddings.";
        }
    }

    static string FormatChatResponse(string chatResponse)
    {
        try
        {
            // Process the chat response to concatenate chunks and make it readable
            var stringBuilder = new StringBuilder();
            foreach (var line in chatResponse.Split('\n'))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var jsonLine = JObject.Parse(line);
                    var messageContent = jsonLine["response"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(messageContent))
                    {
                        stringBuilder.Append(messageContent);
                    }
                }
            }
            return stringBuilder.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error formatting chat response: {ex.Message}");
            return "Error formatting chat response.";
        }
    }
}
