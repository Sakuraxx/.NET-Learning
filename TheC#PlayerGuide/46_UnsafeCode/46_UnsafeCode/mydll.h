#pragma once

#ifdef MYDLL_EXPORTS
#define MYDLL_API __declspec(dllexport)
#else
#define MYDLL_API __declspec(dllimport)
#endif

// 使用 extern "C" 防止 C++ 名称修饰 (name mangling)
// 使用 __stdcall 指定调用约定 (与 DllImport 默认的 Winapi 匹配较好)
extern "C" MYDLL_API int __stdcall add(int a, int b);