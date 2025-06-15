#region Usings

using System.IO.Pipes;
using System.Text;
using System.Text.Json;

#endregion Usings

public record TodoItem(int Id, string Title, bool IsCompleted);

class UiApp
{
    private const string PipeName = "TodoListPipe";

    static async Task Main(string[] args)
    {
        Console.WriteLine("--- To-Do List UI App ---");
        Console.WriteLine("Press any key to fetch to-do items from the data server...");
        Console.ReadKey(true);

        try
        {
            await using var clientStream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            Console.WriteLine("Connecting to the data server...");
            await clientStream.ConnectAsync(5000);
            Console.WriteLine("Successfully connected to the server!");

            // 1. Prepare and send the request.
            string request = "GET_TODOS";
            byte[] requestBytes = Encoding.UTF8.GetBytes(request);
            Console.WriteLine("Sending request 'GET_TODOS'...");
            await clientStream.WriteAsync(requestBytes, 0, requestBytes.Length);

            // Important: Signal that we are done writing. Some pipe implementations need this.
            clientStream.WaitForPipeDrain();

            // 2. Read the response.
            var buffer = new byte[4096];
            int bytesRead = await clientStream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 3. Deserialize and display the data.
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
            Console.ResetColor();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
    }
}