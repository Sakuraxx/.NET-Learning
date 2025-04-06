using System.Runtime.InteropServices; // 需要这个命名空间

public static class DllWrapper
{
    // 告诉 C# 从哪里加载 DLL，以及函数的入口点 (函数名)
    // 注意：默认 CallingConvention 是 Winapi (通常是 StdCall)
    [DllImport("46_UnsafeCode_AddDll.dll", EntryPoint = "add")]
    // internal 或 public 都可以，取决于你是否想在项目其他地方调用
    // static extern 是 P/Invoke 的标准写法
    internal static extern int Add(int a, int b);
}