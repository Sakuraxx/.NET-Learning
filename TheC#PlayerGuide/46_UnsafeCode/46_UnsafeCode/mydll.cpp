#include "pch.h" // ���� <windows.h> ���û��Ԥ����ͷ
#include "mydll.h" // ����ͷ�ļ�

// ���� DLL ��ڵ� (ͨ���� VS ģ���Զ����ɣ����ڸ���/�����߳�/����)
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


// ʵ�ֵ����� add ����
extern "C" MYDLL_API int __stdcall add(int a, int b)
{
    return a + b;
}