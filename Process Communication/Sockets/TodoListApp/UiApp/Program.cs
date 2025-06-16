#region Usings

using System.Net.Sockets;
using System.Text;
using System.Text.Json;

#endregion Usings

public record TodoItem(int Id, string Title, bool IsCompleted);

class UiApp
{
    private const int Port = 11000;
    private const string IpAddress = "127.0.0.1";

    static async Task Main(string[] args)
    {
        Console.WriteLine("--- To-Do List UI App (Socket Version) ---");
        Console.WriteLine("Press any key to fetch to-do items from the data server...");
        Console.ReadKey(true);

        using var client = new TcpClient();

        try
        {
            Console.WriteLine($"Connecting to the data server at {IpAddress}:{Port}...");
            await client.ConnectAsync(IpAddress, Port);

            await using var stream = client.GetStream();

            string request = "GET_TODOS";
            byte[] requestBytes = Encoding.UTF8.GetBytes(request);
            Console.WriteLine("Sending request 'GET_TODOS'...");
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            var buffer = new byte[4096];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var todoItems = JsonSerializer.Deserialize<TodoItem[]>(responseJson);

            Console.WriteLine("\n--- Today's To-Do List ---");
            if (todoItems != null)
            {
                foreach (var item in todoItems)
                {
                    Console.WriteLine($"[{(item.IsCompleted ? "x" : " ")}] #{item.Id}: {item.Title}");
                }
            }
            Console.WriteLine("--------------------------\n");

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("Failed to connect or communicate with the server. Is it running?");
            Console.ResetColor();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
    }
}