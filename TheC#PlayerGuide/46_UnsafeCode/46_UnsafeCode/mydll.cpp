#include "pch.h" // 或者 <windows.h> 如果没有预编译头
#include "mydll.h" // 包含头文件

// 定义 DLL 入口点 (通常由 VS 模板自动生成，用于附加/分离线程/进程)
BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}


// 实现导出的 add 函数
extern "C" MYDLL_API int __stdcall add(int a, int b)
{
    return a + b;
}