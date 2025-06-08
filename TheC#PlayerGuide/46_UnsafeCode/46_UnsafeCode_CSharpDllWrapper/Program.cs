using System;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            int num1 = 10;
            int num2 = 5;
            int result = DllWrapper.Add(num1, num2); // 调用 DLL 中的函数

            Console.WriteLine($"{num1} + {num2} = {result}"); // 输出: 10 + 5 = 15
        }
        catch (DllNotFoundException ex)
        {
            Console.WriteLine($"错误：找不到 DLL 文件。请确保 'MyDLL.dll' 与应用程序在同一目录或在系统路径中。");
            Console.WriteLine(ex.Message);
        }
        catch (EntryPointNotFoundException ex)
        {
            Console.WriteLine($"错误：在 DLL 中找不到指定的函数 'add'。请检查 DLL 导出和 EntryPoint 名称。");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex) // 捕获其他可能的异常
        {
            Console.WriteLine($"发生未知错误: {ex.Message}");
        }

        Console.WriteLine("按任意键退出...");
        Console.ReadKey();
    }
}