# OllamaIntegrationApp

## Overview
OllamaIntegrationApp is a C# console application that integrates with the Ollama AI API. It provides functionalities to retrieve text embeddings and interact with a chatbot, logging interactions and saving chat history.

## Features
- Retrieve text embeddings using the Ollama AI API.
- Chat with the Ollama AI chatbot.
- Log interactions and save chat history.
- Continuous chat loop until the user exits.

## Prerequisites
- .NET 8.0 SDK
- Two NuGet packages: `Newtonsoft.Json` and `System.Net.Http`

## Installation

### Step 1: Clone the Repository

git clone https://github.com/sisovin/OllamaIntegrationApp.git
cd OllamaIntegrationApp

### Step 2: Install Required NuGet Packages

dotnet add package Newtonsoft.Json
dotnet add package System.Net.Http

### Step 3: Restore Dependencies

dotnet restore

### Step 4: Build the Project

dotnet build

### Step 5: Run the ProjectStep

dotnet run

## Usage

**1. Run the Application**:

- Execute the application using dotnet run.

**2. Get Embeddings**:

- When prompted, enter the text you want to get embeddings for.

**3. Chat with CodeBot**:

- Enter your prompt for the chatbot. The conversation will continue until you type exit.

## Example

Enter your text to get embeddings (or type 'exit' to quit):
Why does the rainbow occur after the rain stops?
Embeddings: 0.6055274, 0.8509508, -3.7203066, ...

Enter your prompt for the chat (or type 'exit' to quit):
Hello
CodeBot: How can I assist you today?

## Project Structure

**- Logger.cs**: Handles logging and managing chat history.

**- OllamaClient.cs**: Manages API interactions with the Ollama AI.

**- Program.cs**: Main entry point of the application, managing user interactions and invoking API calls.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgements

This `README.md` provides a comprehensive overview of your project, including features, installation steps, usage instructions, and project structure. Feel free to customize it further to suit your needs! If you have any other questions or need more details, let me know! 😊🚀