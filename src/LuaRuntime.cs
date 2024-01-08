using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using LuaEmbedment.Utilities;
using static LuaEmbedment.LuaAPI;

namespace LuaEmbedment
{
    public class LuaRuntime : IDisposable
    {
        /* ===== Lua Metadata ===== */

        public IntPtr L { get; protected set; }



        /* ===== Constructor ===== */
        
        public LuaRuntime()
        {
            L = luaL_newstate();
            luaL_openlibs(L);
        }
        
        /* ===== Disposing ===== */

        protected bool isDisposed = false;

        public bool IsClosed { get; protected set; } = false;

        public virtual void Dispose()
        {
            ThrowIfDisposed();
            isDisposed = true;

            CloseLua();
            GC.SuppressFinalize(this);
        }

        ~LuaRuntime()
            => CloseLua();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CloseLua()
        {
            lua_close(L);
            IsClosed = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new ObjectDisposedException(nameof(LuaRuntime));
        }
        
        
        
        /* ===== Executions ===== */

        public virtual void DoString([NotNull] string code)
        {
            NullUtilities.ThrowIfArgumentIsNull(code, nameof(code));
            
            ThrowIfDisposed();
            
            if (luaL_dostring(L, code) != LUA_OK)
                throw new RuntimeException(Marshal.PtrToStringAnsi(lua_tostring(L, -1)));
        }

        public virtual void DoFile([NotNull] string path)
        {
            NullUtilities.ThrowIfArgumentIsNull(path, nameof(path));
            
            ThrowIfDisposed();
            
            if (luaL_dofile(L, path) != LUA_OK)
                throw new RuntimeException(Marshal.PtrToStringAnsi(lua_tostring(L, -1)));
        }
        
        
        
        /* ===== Table ===== */

        public Table CreateTable()
            => new Table(this);

        
        
        /* ===== Get/Set Global ===== */

        public virtual long GetGlobalAsInteger([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();

            lua_getglobal(L, name);
            long integer = lua_tointeger(L, -1);
            lua_pop(L, 1);
            return integer;
        }

        public virtual double GetGlobalAsNumber([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_getglobal(L, name);
            double number = lua_tonumber(L, -1);
            lua_pop(L, 1);
            return number;
        }

        public virtual bool GetGlobalAsBoolean([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();

            lua_getglobal(L, name);
            bool boolean = lua_toboolean(L, -1) != 0;
            lua_pop(L, 1);
            return boolean;
        }
        
        [CanBeNull] public virtual string GetGlobalAsString([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();

            lua_getglobal(L, name);
            string str = Marshal.PtrToStringAnsi(lua_tostring(L, -1));
            lua_pop(L, 1);
            return str;
        }

        /// <summary>
        /// Get and return a global table. If the value at the given name is not a table, null will be returned.
        /// </summary>
        /// <returns>Table instance if the value at the name is a table; null otherwise.</returns>
        [CanBeNull] public virtual Table GetGlobalAsTable([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();

            lua_getglobal(L, name);
            if (lua_istable(L, -1) == 0) // lua_istable(L, -1) == false
            {
                lua_pop(L, 1);
                return null;
            }
            Table table = new Table(this, luaL_ref(L, LUA_REGISTRYINDEX));
            return table;
        }

        public virtual bool TryGetGlobalAsTable([NotNull] string name, out Table table)
        {
            table = GetGlobalAsTable(name);
            return table is not null;
        }

        public virtual IntPtr GetGlobalAsUserData([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();

            lua_getglobal(L, name);
            IntPtr userData = lua_touserdata(L, -1);
            lua_pop(L, 1);
            return userData;
        }
        
        public virtual void SetGlobalInteger([NotNull] string name, int integer)
            => SetGlobalInteger(name, (long)integer);

        public virtual void SetGlobalInteger([NotNull] string name, long integer)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_pushinteger(L, integer);
            lua_setglobal(L, name);
        }

        public virtual void SetGlobalNumber([NotNull] string name, float number)
            => SetGlobalNumber(name, (double)number);

        public virtual void SetGlobalNumber([NotNull] string name, double number)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_pushnumber(L, number);
            lua_setglobal(L, name);
        }

        public virtual void SetGlobalBoolean([NotNull] string name, bool boolean)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_pushboolean(L, boolean ? 1 : 0);
            lua_setglobal(L, name);
        }
        
        public virtual void SetGlobalString([NotNull] string name, [NotNull] string str)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            NullUtilities.ThrowIfArgumentIsNull(str, nameof(str));
            
            ThrowIfDisposed();

            lua_pushstring(L, str);
            lua_setglobal(L, name);
        }

        public virtual void SetGlobalTable([NotNull] string name, [NotNull] Table table)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            NullUtilities.ThrowIfArgumentIsNull(table, nameof(table));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, table.Ref);
            lua_setglobal(L, name);
        }
        
        public virtual void SetGlobalCFunction([NotNull] string name, [NotNull] lua_CFunction func)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            NullUtilities.ThrowIfArgumentIsNull(func, nameof(func));
            
            ThrowIfDisposed();
            
            lua_register(L, name, func);
        }

        /// <summary>
        /// 'SetGlobalCFunction' alias.
        /// </summary>
        public virtual void RegisterGlobalFunction([NotNull] string name, [NotNull] lua_CFunction func)
            => SetGlobalCFunction(name, func);

        public virtual void SetGlobalUserData([NotNull] string name, IntPtr userData)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_pushlightuserdata(L, userData);
            lua_setglobal(L, name);
        }
        
        public virtual void SetGlobalNil([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_pushnil(L);
            lua_setglobal(L, name);
        }
    }
}
