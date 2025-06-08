#pragma once

#ifdef MYDLL_EXPORTS
#define MYDLL_API __declspec(dllexport)
#else
#define MYDLL_API __declspec(dllimport)
#endif

// ʹ�� extern "C" ��ֹ C++ �������� (name mangling)
// ʹ�� __stdcall ָ������Լ�� (�� DllImport Ĭ�ϵ� Winapi ƥ��Ϻ�)
extern "C" MYDLL_API int __stdcall add(int a, int b);