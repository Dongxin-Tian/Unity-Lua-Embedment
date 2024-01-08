using System;
using System.Runtime.InteropServices;
using System.Security;

namespace LuaEmbedment
{
    using lua_Integer = Int64;
    using lua_Number = Double;
    using lua_Unsigned = UInt64;
    using lua_KContext = IntPtr;
    
    using size_t = UIntPtr;
    
    using LUA_INTEGER = Int64;
    using LUA_NUMBER = Double;
    using LUA_UNSIGNED = UInt64;
        
    public static class LuaAPI
    { 
        /* ===== Library Info ===== */
            
        public const string LibraryName = "Lua546";
            
            
            
        /* ===== Version ===== */
            
        public const string LUA_VERSION_MAJOR = "5";
        public const string LUA_VERSION_MINOR = "4";
        public const string LUA_VERSION_RELEASE = "6";
            
        public const int LUA_VERSION_NUM = 504;
        public const int LUA_VERSION_RELEASE_NUM = LUA_VERSION_NUM * 100 + 6;
        
        public static readonly string LUA_VERSION = $"Lua {LUA_VERSION_MAJOR}.{LUA_VERSION_MINOR}";
        public static readonly string LUA_RELEASE = $"{LUA_VERSION}.{LUA_VERSION_RELEASE}";
        public static readonly string LUA_COPYRIGHT = $"{LUA_RELEASE}  Copyright (C) 1994-2023 Lua.org, PUC-Rio";
        public static readonly string LUA_AUTHORS = "R. Ierusalimschy, L. H. de Figueiredo, W. Celes";
        
        
        
        /* Option for multiple returns in 'lua_pcall' and 'lua_call' */
        
        public const int LUA_MULTRET = -1;
        
        
        
        /* ===== Pseudo-indices ===== */
        
        public const int LUA_REGISTRYINDEX = -LUAI_MAXSTACK - 1000;
        
        
        
        
        /* ===== Thread Status ===== */
        
        public const int LUA_OK = 0;
        public const int LUA_YIELD = 1;
        public const int LUA_ERRRUN = 2;
        public const int LUA_ERRSYNTAX = 3;
        public const int LUA_ERRMEM = 4;
        public const int LUA_ERRERR = 5;
        
        
        
        /* LUAI_MAXSTACK limits the size of the Lua stack. */
        
        public const int LUAI_MAXSTACK = LUAI_IS32INT ? 1000000 : 15000;
        
        
        
        /* LUAI_IS32INT is true iff 'int' has (at least) 32 bits. */

        public const bool LUAI_IS32INT = true; // ((UINT_MAX >> 30) >= 3)
        
        
        
        public const int LUAL_NUMSIZES = sizeof(lua_Integer) * 16 + sizeof(lua_Number);
        
        
        
        /* ===== Comparison Opcodes ===== */
        
        public const int LUA_OPADD = 0;
        public const int LUA_OPSUB = 1;
        public const int LUA_OPMUL = 2;
        public const int LUA_OPMOD = 3;
        public const int LUA_OPPOW = 4;
        public const int LUA_OPDIV = 5;
        public const int LUA_OPIDIV = 6;
        public const int LUA_OPBAND = 7;
        public const int LUA_OPBOR = 8;
        public const int LUA_OPBXOR = 9;
        public const int LUA_OPSHL = 10;
        public const int LUA_OPSHR = 11;
        public const int LUA_OPUNM = 12;
        public const int LUA_OPBNOT = 13;
        
        
        
        /* ===== Basic Types ===== */
        
        public const int LUA_TNONE = -1;
        
        public const int LUA_TNIL = 0;
        public const int LUA_TBOOLEAN = 1;
        public const int LUA_TLIGHTUSERDATA = 2;
        public const int LUA_TNUMBER = 3;
        public const int LUA_TSTRING = 4;
        public const int LUA_TTABLE = 5;
        public const int LUA_TFUNCTION = 6;
        public const int LUA_TUSERDATA = 7;
        public const int LUA_TTHREAD = 8;

        public const int LUA_NUMTYPES = 9;
        
        
        
        /* Minimum Lua stack available to a C function */
        
        public const int LUA_MINSTACK = 20;


        /* Predefined values in the registry */

        public const int LUA_RIDX_MAINTHREAD = 1;
        public const int LUA_RIDX_GLOBALS = 2;
        public const int LUA_RIDX_LAST = LUA_RIDX_GLOBALS;
        
        
        
        /* ===== Integer Limits ===== */
        
        public const long LUA_MAXINTEGER = long.MaxValue;
        public const long LUA_MININTEGER = long.MinValue;
        
        
        
        /* ===== Functions and Types ===== */

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_absindex(IntPtr L, int idx);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr lua_Alloc(IntPtr ud, IntPtr ptr, size_t osize, size_t nsize);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_arith(IntPtr L, int op);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_CFunction lua_atpanic(IntPtr L, lua_CFunction panicf);
        
        public static void lua_call(IntPtr L, int nargs, int nresults)
                => lua_callk(L, nargs, nresults, (lua_KContext)0, null);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_callk(IntPtr L, int nargs, int nresults, lua_KContext ctx, lua_KFunction k);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int lua_CFunction(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_checkstack(IntPtr L, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_close(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_closeslot(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_closethread(IntPtr L, IntPtr from);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_compare(IntPtr L, int index1, int index2, int op);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_concat(IntPtr L, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_copy(IntPtr L, int fromidx, int toidx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_createtable(IntPtr L, int narray, int nrec);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_dump(IntPtr L, lua_Writer writer, IntPtr data, int strip);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_error(IntPtr L);

        public const int LUA_GCSTOP = 0;
        public const int LUA_GCRESTART = 1;
        public const int LUA_GCCOLLECT = 2;
        public const int LUA_GCCOUNT = 3;
        public const int LUA_GCCOUNTB = 4;
        public const int LUA_GCSTEP = 5;
        public const int LUA_GCSETPAUSE = 6;
        public const int LUA_GCSETSTEPMUL = 7;
        public const int LUA_GCISRUNNING = 9;
        public const int LUA_GCGEN = 10;
        public const int LUA_GCINC = 11;
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gc(IntPtr L, int what);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gc(IntPtr L, int what, int param);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gc(IntPtr L, int what, int param1, int param2);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gc(IntPtr L, int what, int param1, int param2, int param3);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Alloc lua_getallocf(IntPtr L, IntPtr ud);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getfield(IntPtr L, int idx, string k);

        public static readonly int LUA_EXTRASPACE = IntPtr.Size; // sizeof(void*)

        public static IntPtr lua_getextraspace(IntPtr L)
            => L - LUA_EXTRASPACE;
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getglobal(IntPtr L, string name);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_geti(IntPtr L, int idx, lua_Integer n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getmetatable(IntPtr L, int objindex);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gettable(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gettop(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getiuservalue(IntPtr L, int idx, int n);

        public static void lua_insert(IntPtr L, int index) 
            => lua_rotate(L, index, 1);

        public static int lua_isboolean(IntPtr L, int index)
            => lua_type(L, index) == LUA_TBOOLEAN ? 1 : 0;
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_iscfunction(IntPtr L, int idx);

        public static int lua_isfunction(IntPtr L, int index)
            => lua_type(L, index) == LUA_TFUNCTION ? 1 : 0;
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isinteger(IntPtr L, int idx);

        public static int lua_islightuserdata(IntPtr L, int index) 
            => lua_type(L, index) == LUA_TLIGHTUSERDATA ? 1 : 0;

        public static int lua_isnil(IntPtr L, int index)
            => lua_type(L, index) == LUA_TNIL ? 1 : 0;

        public static int lua_isnone(IntPtr L, int index)
            => lua_type(L, index) == LUA_TNONE ? 1 : 0;

        public static int lua_isnoneornil(IntPtr L, int index)
            => lua_type(L, index) <= 0 ? 1 : 0;
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isnumber(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isstring(IntPtr L, int idx);

        public static int lua_istable(IntPtr L, int index) 
            => lua_type(L, index) == LUA_TTABLE ? 1 : 0;

        public static int lua_isthread(IntPtr L, int index)
            => lua_type(L, index) == LUA_TTHREAD ? 1 : 0;
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isuserdata(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isyieldable(IntPtr L);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int lua_KFunction(IntPtr L, int status, IntPtr ctx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_len(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_load(IntPtr L, lua_Reader reader, IntPtr dt, string chunkname, string mode);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_newstate(lua_Alloc f, IntPtr ud);

        public static void lua_newtable(IntPtr L)
            => lua_createtable(L, 0, 0);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_newthread(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_newuserdatauv(IntPtr L, size_t sz, int nuvalue);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_next(IntPtr L, int idx);

        public unsafe static int lua_numbertointeger(lua_Number n, lua_Integer* p)
        {
            if (n is >= (LUA_NUMBER)LUA_MININTEGER and < -(LUA_NUMBER)LUA_MININTEGER)
            {
                *p = (lua_Integer)n;
                return 1;
            }
            return 0;
        }

        public static bool lua_numbertointeger(lua_Number n, out lua_Integer i)
        {
            if (n is >= (LUA_NUMBER)LUA_MININTEGER and < -(LUA_NUMBER)LUA_MININTEGER)
            {
                i = (lua_Integer)n;
                return true;
            }
            i = 0;
            return false;
        }
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_pcallk(IntPtr L, int nargs, int nresults, int msgh, lua_KContext ctx, lua_KFunction k);
        
        public static int lua_pcall(IntPtr L, int nargs, int nresults, int msgh) 
            => lua_pcallk(L, nargs, nresults, msgh, (lua_KContext)0, null);

        public static void lua_pop(IntPtr L, int n)
            => lua_settop(L, -n - 1);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushboolean(IntPtr L, int b);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushcclosure(IntPtr L, lua_CFunction fn, int n);
        
        public static void lua_pushcfunction(IntPtr L, lua_CFunction f) 
            => lua_pushcclosure(L, f, 0);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_pushfstring(IntPtr L, string fmt);

        public static void lua_pushglobaltable(IntPtr L)
                => lua_rawgeti(L, LUA_REGISTRYINDEX, LUA_RIDX_GLOBALS);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushinteger(IntPtr L, lua_Integer n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushlightuserdata(IntPtr L, IntPtr p);

        public static IntPtr lua_pushliteral(IntPtr L, string s)
            => lua_pushstring(L, "" + s);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_pushlstring(IntPtr L, string s, size_t len);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushnil(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushnumber(IntPtr L, lua_Number n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_pushstring(IntPtr L, string s);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_pushthread(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushvalue(IntPtr L, int idx);
        
        // const char *lua_pushvfstring (lua_State *L, const char *fmt, va_list argp); is ignored
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_rawequal(IntPtr L, int idx1, int idx2);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_rawget(IntPtr L, int idx);
        

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_rawgeti(IntPtr L, int idx, lua_Integer n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_rawgetp(IntPtr L, int idx, IntPtr p);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Unsigned lua_rawlen(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rawset(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rawseti(IntPtr L, int idx, lua_Integer n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rawsetp (IntPtr L, int idx, IntPtr p);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr lua_Reader(IntPtr L, IntPtr ud, ref size_t sz);

        public static void lua_register(IntPtr L, string name, lua_CFunction f)
        { 
            lua_pushcfunction(L, (f));
            lua_setglobal(L, name);
        }

        public static void lua_remove(IntPtr L, int index)
        { 
            lua_rotate(L, index, -1);
            lua_pop(L, 1);
        }

        public static void lua_replace(IntPtr L, int index)
        { 
            lua_copy(L, -1, index);
            lua_pop(L, 1);
        }
        
        // int lua_resetthread (lua_State *L); is deprecated

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_resume(IntPtr L, IntPtr from, int nargs, IntPtr nresults);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rotate(IntPtr L, int idx, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_setallocf(IntPtr L, lua_Alloc f, IntPtr ud);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_setfield(IntPtr L, int idx, string k);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_setglobal(IntPtr L, string name);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_seti(IntPtr L, int idx, lua_Integer n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_setiuservalue(IntPtr L, int idx, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_setmetatable(IntPtr L, int objindex);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_settable(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_settop(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_setwarnf(IntPtr L, lua_WarnFunction f, IntPtr ud);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_status(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern size_t lua_stringtonumber(IntPtr L, string s);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_toboolean(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_CFunction lua_tocfunction(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_toclose(IntPtr L, int idx);

        public static lua_Integer lua_tointeger(IntPtr L, int index)
            => lua_tointegerx(L, index, IntPtr.Zero);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Integer lua_tointegerx(IntPtr L, int idx, IntPtr isnum);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_tolstring(IntPtr L, int idx, IntPtr len);

        public static lua_Number lua_tonumber(IntPtr L, int index) 
            => lua_tonumberx(L, index, IntPtr.Zero);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Number lua_tonumberx(IntPtr L, int idx, IntPtr isnum);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_topointer(IntPtr L, int idx);

        public static IntPtr lua_tostring(IntPtr L, int index)
                => lua_tolstring(L, index, IntPtr.Zero);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_tothread(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_touserdata(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_type(IntPtr L, int idx);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_typename(IntPtr L, int tp);

        public static int lua_upvalueindex(int i)
            => LUA_REGISTRYINDEX - i;
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Number lua_version(IntPtr L);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void lua_WarnFunction(IntPtr ud, string msg, int tocont);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_warning(IntPtr L, string msg, int tocont);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr lua_Writer(IntPtr L, IntPtr p, size_t sz, IntPtr ud);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_xmove(IntPtr from, IntPtr to, int n);

        public static int lua_yield(IntPtr L, int nresults)
            => lua_yieldk(L, nresults, (lua_KContext)0, null);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_yieldk(IntPtr L, int nresults, lua_KContext ctx, lua_KFunction k);
        
        
        
        /* ===== Debug Interface ===== */
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Hook lua_gethook(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gethookcount(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gethookmask(IntPtr L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getinfo(IntPtr L, string what, IntPtr ar);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_getlocal(IntPtr L, IntPtr ar, int n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getstack(IntPtr L, int level, IntPtr ar);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_getupvalue(IntPtr L, int funcindex, int n);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void lua_Hook(IntPtr L, IntPtr ar);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_sethook(IntPtr L, lua_Hook func, int mask, int count);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_setlocal(IntPtr L, IntPtr ar, int n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_setupvalue(IntPtr L, int funcindex, int n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_upvalueid(IntPtr L, int fidx, int n);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_upvaluejoin(IntPtr L, int fidx1, int n1, int fidx2, int n2);
            
            
            
        /* ===== Auxiliary Library ===== */
        
        public static void luaL_argcheck(IntPtr L, int cond, int arg, string extramsg)
        {
            if (cond == 0)
                luaL_argerror(L, arg, extramsg);
        }
            
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_argerror(IntPtr L, int arg, string extramsg);

        public static void luaL_argexpected(IntPtr L, int cond, int arg, string tname)
        { 
            if (cond == 0)
                luaL_typeerror(L, arg, tname);
        }
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_callmeta(IntPtr L, int obj, string e);
      
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_checkany(IntPtr L, int arg);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Integer luaL_checkinteger(IntPtr L, int arg);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_checklstring(IntPtr L, int arg, IntPtr l);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Number luaL_checknumber(IntPtr L, int arg);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_checkoption(IntPtr L, int arg, string def, string[] lst);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_checkstack(IntPtr L, int sz, string msg);

        public static IntPtr luaL_checkstring(IntPtr L, int arg)
            => luaL_checklstring(L, arg, IntPtr.Zero);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_checktype(IntPtr L, int arg, int t);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_checkudata(IntPtr L, int ud, string tname); 
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_checkversion_(IntPtr L, lua_Number ver, size_t sz);

        public static void luaL_checkversion(IntPtr L)
            => luaL_checkversion_(L, LUA_VERSION_NUM, (size_t)LUAL_NUMSIZES);
        
        public static int luaL_dofile(IntPtr L, string filename)
        { 
            int loadfile = luaL_loadfile(L, filename);
            return loadfile != 0 ? loadfile : lua_pcall(L, 0, LUA_MULTRET, 0);
        }
        
        public static int luaL_dostring(IntPtr L, string str)
        {
            int loadstring = luaL_loadstring(L, str);
            return loadstring != 0 ? loadstring : lua_pcall(L, 0, LUA_MULTRET, 0);
        }
        
        public static int luaL_dostring_unmanaged(IntPtr L, IntPtr str)
        {
            int loadstring = luaL_loadstring_unmanaged(L, str);
            return loadstring != 0 ? loadstring : lua_pcall(L, 0, LUA_MULTRET, 0);
        }
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_error(IntPtr L, string fmt);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_execresult(IntPtr L, int stat);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_fileresult(IntPtr L, int stat, string fname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_getmetafield(IntPtr L, int obj, string e);

        public static int luaL_getmetatable(IntPtr L, string tname)
            => lua_getfield(L, LUA_REGISTRYINDEX, tname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_getsubtable(IntPtr L, int idx, string fname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_gsub(IntPtr L, string s, string p, string r);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Integer luaL_len(IntPtr L, int idx);

        public static int luaL_loadbuffer(IntPtr L, IntPtr buff, size_t sz, string name)
                => luaL_loadbufferx(L, buff, sz, name, null);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadbufferx(IntPtr L, IntPtr buff, size_t sz, string name, [MarshalAs(UnmanagedType.LPStr)] string mode);
        
        public static int luaL_loadfile(IntPtr L, string filename) 
            => luaL_loadfilex(L, filename, null);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadfilex(IntPtr L, string filename, [MarshalAs(UnmanagedType.LPStr)] string mode);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadstring(IntPtr L, string s);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, EntryPoint = "luaL_loadstring", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadstring_unmanaged(IntPtr L, IntPtr s);
        
        public static void luaL_newlib(IntPtr L, luaL_Reg[] l)
        {
            luaL_checkversion(L);
            luaL_newlibtable(L, l);
            luaL_setfuncs(L, l, 0);
        }
        
        public static void luaL_newlibtable(IntPtr L, luaL_Reg[] l)
            => lua_createtable(L, 0, l.Length - 1);
        
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_newmetatable(IntPtr L, string tname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_newstate();

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_openlibs(IntPtr L);
        
        // T luaL_opt (L, func, arg, dflt);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Integer luaL_optinteger(IntPtr L, int arg, lua_Integer def);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_optlstring(IntPtr L, int arg, string def, IntPtr len);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Number luaL_optnumber(IntPtr L, int arg, lua_Number def);

        public static IntPtr luaL_optstring(IntPtr L, int arg, string d) 
            => luaL_optlstring(L, arg, d, IntPtr.Zero);

        public static void luaL_pushfail(IntPtr L) 
            => lua_pushnil(L);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_ref(IntPtr L, int t);
        
        [StructLayout(LayoutKind.Sequential)]
        public struct luaL_Reg
        {
            public string name;
            public lua_CFunction func;

            public luaL_Reg(string name, lua_CFunction func)
            {
                this.name = name;
                this.func = func;
            }

            public readonly static luaL_Reg Sentinel = new luaL_Reg(null, null);
        }
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_requiref(IntPtr L, string modname, lua_CFunction openf, int glb);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_setfuncs(IntPtr L, luaL_Reg[] l, int nup);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_setmetatable(IntPtr L, string tname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_testudata(IntPtr L, int ud, string tname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_tolstring(IntPtr L, int idx, IntPtr len);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_traceback(IntPtr L, IntPtr L1, string msg, int level);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_typeerror(IntPtr L, int arg, string tname);

        public static IntPtr luaL_typename(IntPtr L, int index)
            => lua_typename(L, lua_type(L, index));
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_unref(IntPtr L, int t, int @ref);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_where(IntPtr L, int lvl);
    }
}
