using System;
using System.Runtime.CompilerServices;

namespace LuaEmbedment.Utilities
{
    internal static class NullUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfArgumentIsNull(object arg, string argName)
        {
            if (arg is null)
                throw new ArgumentNullException(argName);
        }
    }
}
