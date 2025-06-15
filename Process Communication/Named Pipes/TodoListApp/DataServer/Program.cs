#region Usings

using System.IO.Pipes;
using System.Text;
using System.Text.Json;

#endregion Usings

// Data model - no changes needed here
public record TodoItem(int Id, string Title, bool IsCompleted);

class DataServer
{
    private const string PipeName = "TodoListPipe";

    static async Task Main(string[] args)
    {
        Console.WriteLine("--- Data Server ---");
        Console.WriteLine($"Waiting for a client to connect to pipe '{PipeName}'...");

        while (true)
        {
            await using var serverStream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            await serverStream.WaitForConnectionAsync();
            Console.WriteLine("Client connected!");

            try
            {
                var buffer = new byte[4096]; // 4KB buffer is usually sufficient.

                // Read the request from the client.
                int bytesRead = await serverStream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) continue; // Client disconnected immediately.

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received request: '{request.Trim()}'");

                // Process the request and get the JSON response.
                string responseJson = ProcessRequest(request.Trim());

                // Encode the response to bytes.
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);

                // Write the response bytes back to the client.
                await serverStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                Console.WriteLine("Response sent.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                if (serverStream.IsConnected)
                {
                    serverStream.Disconnect();
                }
                Console.WriteLine("Waiting for the next client connection...");
            }
        }
    }

    private static string ProcessRequest(string request)
    {
        if (request.Equals("GET_TODOS", StringComparison.OrdinalIgnoreCase))
        {
            var todoList = new[]
            {
                new TodoItem(1, "Learn Avalonia", true),
                new TodoItem(2, "Master Named Pipes", true),
                new TodoItem(3, "Drink a coffee", false)
            };
            return JsonSerializer.Serialize(todoList);
        }
        return "ERROR: Unknown request";
    }
}