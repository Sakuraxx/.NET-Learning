#region Usings

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

#endregion Usings

public record TodoItem(int Id, string Title, bool IsCompleted);

class DataServer
{
    private const int Port = 11000;
    private const string IpAddress = "127.0.0.1";

    static async Task Main(string[] args)
    {
        var listener = new TcpListener(IPAddress.Any, Port);

        try
        {
            listener.Start();
            Console.WriteLine("--- Data Server (Socket Version) ---");
            Console.WriteLine($"Server is listening on port {Port}...");
            Console.WriteLine("Waiting for a client to connect...");

            while (true)
            {
                // AcceptTcpClientAsync returns a TcpClient that we need to manage.
                TcpClient client = await listener.AcceptTcpClientAsync(); // The accepted client
                Console.WriteLine($"Client connected from {client.Client.RemoteEndPoint}!");

                // Pass the client to the handler. Don't use 'using' here,
                // because the handler will take ownership and dispose of it.
                _ = HandleClientAsync(client);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred in the server: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            listener.Stop();
        }
    }

    private static async Task HandleClientAsync(TcpClient client)
    {
        // Use a standard 'using' block to ensure the client is disposed of
        // when the handler is finished.
        using (client)
        {
            try
            {
                // The stream is also owned by the client, so 'using' it separately is good practice.
                await using var stream = client.GetStream(); // NetworkStream DOES support IAsyncDisposable
                var buffer = new byte[4096];

                // Read the request from the client.
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) return; // Client disconnected.

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received request: '{request.Trim()}'");

                // Process the request and get the JSON response.
                string responseJson = ProcessRequest(request.Trim());

                // Encode the response to bytes.
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);

                // Write the response bytes back to the client.
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                Console.WriteLine("Response sent.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred with client {client.Client.RemoteEndPoint}: {ex.Message}");
            }
            // 'finally' block is no longer needed, 'using' handles it.
            Console.WriteLine($"Client {client.Client.RemoteEndPoint} disconnected. Waiting for the next client...");
        }
    }

    private static string ProcessRequest(string request)
    {
        if (request.Equals("GET_TODOS", StringComparison.OrdinalIgnoreCase))
        {
            var todoList = new[]
            {
                new TodoItem(1, "Learn Avalonia", true),
                new TodoItem(2, "Master Sockets", true), // Updated task!
                new TodoItem(3, "Drink a coffee", false)
            };
            return JsonSerializer.Serialize(todoList);
        }
        return "ERROR: Unknown request";
    }

}