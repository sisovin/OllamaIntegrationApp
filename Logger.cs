using System;
using System.IO;

public class Logger
{
    private readonly string logFilePath;
    private readonly string chatHistoryFilePath;

    public Logger(string logDirectory)
    {
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        logFilePath = Path.Combine(logDirectory, "logregister.log");
        chatHistoryFilePath = Path.Combine(logDirectory, "chat_history.json");

        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath).Dispose();
        }

        if (!File.Exists(chatHistoryFilePath))
        {
            File.Create(chatHistoryFilePath).Dispose();
        }
    }

    public void Log(string message)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    public void SaveChatHistory(string chatHistory)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(chatHistoryFilePath))
            {
                writer.Write(chatHistory);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving chat history: {ex.Message}");
        }
    }

    public string LoadChatHistory()
    {
        try
        {
            if (File.Exists(chatHistoryFilePath))
            {
                using (StreamReader reader = new StreamReader(chatHistoryFilePath))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading chat history: {ex.Message}");
        }
        return string.Empty;
    }
}
