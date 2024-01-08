using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using LuaEmbedment.Utilities;
using static LuaEmbedment.LuaAPI;

namespace LuaEmbedment
{
    public sealed record Table : IDisposable
    {
        private readonly LuaRuntime runtime;

        private IntPtr L => runtime.L;

        public int Ref { get; }



        /* ===== Constructor ===== */
        
        /// <summary>
        /// Create a new table.
        /// </summary>
        /// <param name="runtime">LuaRuntime instance reference.</param>
        public Table([NotNull] LuaRuntime runtime)
        {
            ThrowIfInvalidLuaRuntime(runtime);
            
            this.runtime = runtime;
            
            lua_newtable(runtime.L);
            Ref = luaL_ref(runtime.L, LUA_REGISTRYINDEX);
        }

        /// <summary>
        /// Create a table based from the provided Lua registry reference.
        /// Doesn't create any new table in Lua.
        /// </summary>
        /// <param name="runtime">LuaRuntime instance reference.</param>
        /// <param name="ref">Reference to the table in the Lua registry.</param>
        public Table(LuaRuntime runtime, int @ref)
        {
            ThrowIfInvalidLuaRuntime(runtime);
            
            this.runtime = runtime;
            Ref = @ref;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowIfInvalidLuaRuntime([NotNull] LuaRuntime runtime)
        {
            NullUtilities.ThrowIfArgumentIsNull(runtime, nameof(runtime));
            if (runtime.IsClosed)
                throw new ArgumentException("The Lua runtime is already closed.", nameof(runtime));
        }



        /* ===== Disposing ===== */
        
        private bool isDisposed = false;
        
        public void Dispose()
        {
            ThrowIfDisposed();
            isDisposed = true;
            
            Unref();
            GC.SuppressFinalize(this);
        }

        ~Table()
            => Unref();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Unref()
        {
            // Only unreference if runtime is not closed
            if (!runtime.IsClosed)
                luaL_unref(L, LUA_REGISTRYINDEX, Ref);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new ObjectDisposedException(nameof(Table));
        }
        
        
        
        /* ===== Get Fields ===== */

        public long GetFieldAsInteger([NotNull] string name)
        { 
            TryGetFieldAsInteger(name, out long integer);
            return integer;
        }

        public bool TryGetFieldAsInteger([NotNull] string name, out int integer)
        {
            bool isSucceed = TryGetFieldAsInteger(name, out long @long);
            integer = (int)@long;
            return isSucceed;
        }
        
        public unsafe bool TryGetFieldAsInteger([NotNull] string name, out long integer)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Get the field
            lua_pushstring(L, name);
            lua_gettable(L, -2);
            
            // Convert the field to an integer and check if the operation is succeeded
            int isNum;
            integer = lua_tointegerx(L, -1, new IntPtr(&isNum));
            
            lua_pop(L, 2); // Pop the value and the table
            
            return isNum != 0;
        }

        public double GetFieldAsNumber([NotNull] string name)
        {
            TryGetFieldAsNumber(name, out double number);
            return number;
        }

        public bool TryGetFieldAsNumber([NotNull] string name, out float number)
        {
            bool isSucceed = TryGetFieldAsNumber(name, out double @double);
            number = (float)@double;
            return isSucceed;
        }
        
        public unsafe bool TryGetFieldAsNumber([NotNull] string name, out double number)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Get the field
            lua_pushstring(L, name);
            lua_gettable(L, -2);
            
            // Convert the field to an integer and check if the operation is succeeded
            int isNum;
            number = lua_tonumberx(L, -1, new IntPtr(&isNum));
            
            lua_pop(L, 2); // Pop the value and the table
            
            return isNum != 0;
        }

        public bool GetFieldAsBoolean([NotNull] string name)
        {
            TryGetFieldAsBoolean(name, out bool boolean);
            return boolean;
        }
        
        public unsafe bool TryGetFieldAsBoolean([NotNull] string name, out bool boolean)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Get the field
            lua_pushstring(L, name);
            lua_gettable(L, -2);

            // Check if the field is a boolean and convert it to a boolean
            bool isBoolean = lua_isboolean(L, -1) != 0;
            boolean = lua_toboolean(L, -1) != 0;
            
            lua_pop(L, 2); // Pop the value and the table

            return isBoolean;
        }

        [CanBeNull] public string GetFieldAsString([NotNull] string name)
        {
            TryGetFieldAsString(name, out string str);
            return str;
        }
        
        public unsafe bool TryGetFieldAsString([NotNull] string name, out string str)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Get the field
            lua_pushstring(L, name);
            lua_gettable(L, -2);

            // Converts the field to a string
            nuint len;
            IntPtr luaStr = lua_tolstring(L, -1, new IntPtr(&len));
            str = luaStr != IntPtr.Zero ? Marshal.PtrToStringAnsi(luaStr, (int)len) : null;
            
            lua_pop(L, 2); // Pop the value and the table

            return str is not null;
        }

        /// <summary>
        /// Converts the field to a string or use its '__tostring' field in its metatable.
        /// </summary>
        [NotNull] public unsafe string FieldToString([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Get the field
            lua_pushstring(L, name);
            lua_gettable(L, -2);

            // Converts the field to a string
            nuint len;
            string str = Marshal.PtrToStringAnsi(luaL_tolstring(L, -1, new IntPtr(&len)), (int)len);
            
            lua_pop(L, 2); // Pop the value and the table

            return str;
        }

        /// <summary>
        /// IntPtr.Zero (NULL) will be returned if the field is not a user data.
        /// </summary>
        public IntPtr GetFieldAsUserData([NotNull] string name)
        {
            TryGetFieldAsUserData(name, out IntPtr userData);
            return userData;
        }
        
        public bool TryGetFieldAsUserData([NotNull] string name, out IntPtr userData)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Get the field
            lua_pushstring(L, name);
            lua_gettable(L, -2);
            
            userData = lua_touserdata(L, -1); // Convert the field to a user data
            
            lua_pop(L, 2); // Pop the value and the table
            
            return userData != IntPtr.Zero;
        }
        
        
        
        /* ===== Set Fields ===== */
        
        public void SetFieldInteger([NotNull] string name, int integer)
            => SetFieldInteger(name, (long)integer);

        public void SetFieldInteger([NotNull] string name, long integer)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_pushinteger(L, integer);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }

        public void SetFieldNumber([NotNull] string name, float number)
            => SetFieldNumber(name, (double)number);
        
        public void SetFieldNumber([NotNull] string name, double number)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_pushnumber(L, number);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }

        public void SetFieldBoolean([NotNull] string name, bool boolean)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_pushboolean(L, boolean ? 1 : 0);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }

        public void SetFieldString([NotNull] string name, [NotNull] string str)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            NullUtilities.ThrowIfArgumentIsNull(str, nameof(str));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_pushstring(L, str);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }

        public void SetFieldTable([NotNull] string name, [NotNull] Table table)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            NullUtilities.ThrowIfArgumentIsNull(table, nameof(table));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the self table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_rawgeti(L, LUA_REGISTRYINDEX, table.Ref);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }

        public void RegisterFunction([NotNull] string name, [NotNull] lua_CFunction func)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            NullUtilities.ThrowIfArgumentIsNull(func, nameof(func));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_pushcfunction(L, func);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }

        /// <summary>
        /// 'RegisterFunction' alias.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetFieldAsCFunction([NotNull] string name, [NotNull] lua_CFunction func)
            => RegisterFunction(name, func);

        public void SetFieldUserData([NotNull] string name, IntPtr userData)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_pushlightuserdata(L, userData);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }

        public void SetFieldNil([NotNull] string name)
        {
            NullUtilities.ThrowIfArgumentIsNull(name, nameof(name));
            
            ThrowIfDisposed();
            
            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref); // Push the table reference to the stack
            
            // Push the field name and the value
            lua_pushstring(L, name);
            lua_pushnil(L);
            
            lua_settable(L, -3); // Set the field
            
            lua_pop(L, 1); // Pop the table reference
        }
        
        
        
        /* ===== ToString Method ===== */

        public override string ToString()
        {
            ThrowIfDisposed();

            lua_rawgeti(L, LUA_REGISTRYINDEX, Ref);
            string str = Marshal.PtrToStringAnsi(luaL_tolstring(L, -1, IntPtr.Zero));
            lua_pop(L, 1);
            return str ?? "nil";
        }
    }
}
