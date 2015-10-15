/* UTF-8 string wrapper
 *
 * Copyright (c) 2015 Silicon Studio Corp. (http://siliconstudio.co.jp)
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 */

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace SDL2
{
    /// <summary>
    /// .NET representation of a UTF8 string. Mostly used for marshalling between .NET and UTF-8.
    /// </summary>
    public unsafe struct UTF8String : IDisposable
    {
#region Initialization
        /// <summary>
        /// Initialize instance with .NET string <see cref="s"/>
        /// </summary>
        /// <param name="s">.NET string to wrap into a UTF8 sequence of unmanaged bytes.</param>
        public UTF8String(string s)
        {
            if (s != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                int nb = bytes.Length;
                IntPtr lPtr = SDL.SDL_malloc((IntPtr) (nb + 1));
                if (lPtr == IntPtr.Zero)
                {
                    throw new OutOfMemoryException("Cannot Allocate UTF8String");
                }
                else
                {
                    Marshal.Copy(bytes, 0, lPtr, bytes.Length);
                    ((byte*) lPtr)[bytes.Length] = 0;
                }
                _handle = lPtr;
                _capacity = bytes.Length;
            }
            else
            {
                _handle = IntPtr.Zero;
                _capacity = 0;
            }
            IsShared = false;

            Contract.Ensures((s == null) || (_handle != IntPtr.Zero), "handle set");
            Contract.Ensures((s == null) || (_capacity >= s.Length), "capacity_greater_than_input");
            Contract.Ensures(ReferenceEquals(s, String()) || (String().Equals(s)), "string_set");
            Contract.Ensures(!IsShared, "not shared.");
        }

        /// <summary>
        /// Initialize instance with a sequence of unmanaged bytes without owning the associated pointer, i.e. we won't free the resource.
        /// </summary>
        /// <param name="o">Unmanaged pointer from which data will be read.</param>
        public UTF8String(IntPtr o)
        {
            if (o != IntPtr.Zero)
            {
                byte* ptr = (byte*) o;
                    // Count number of available bytes.
                while (*ptr != 0) { ptr++; }
                long nb = ptr - (byte*) o + 1;
                if (nb > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("UTF-8 string too large.");
                }
                _capacity = (int) nb;
                _handle = o;
            }
            else
            {
                _handle = IntPtr.Zero;
                _capacity = 0;
            }
            IsShared = true;

            Contract.Ensures(_handle == o, "handle set");
            Contract.Ensures(_capacity >= 0, "capacity_non_negative");
            Contract.Ensures(IsShared, "shared");
        }
#endregion

#region Access
        /// <summary>
        /// Access to the underlying unmanaged memory.
        /// </summary>
        public IntPtr Handle { get { return _handle; } }

        /// <summary>
        /// .NET string representation of current UTF-8 encoding string.
        /// </summary>
        /// <returns></returns>
        public string String()
        {
            return (_handle == IntPtr.Zero ? null : Encoding.UTF8.GetString((byte*) _handle, _capacity - 1));
        }
#endregion

#region Measurements
        /// <summary>
        /// Number of bytes of allocated unmanaged data.
        /// </summary>
        public int Capacity { get { return _capacity; } }

        public bool IsShared { get; set; }

#endregion

#region Element change
        public void SetString(string s)
        {
            if (s != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                int nb = bytes.Length;
                if (nb + 1 > Capacity)
                {
                    IntPtr lPtr = SDL.SDL_realloc(_handle, (IntPtr) (nb + 1));
                    if (lPtr == IntPtr.Zero)
                    {
                        Dispose();
                        throw new OutOfMemoryException("Cannot reallocate UTF8String");
                    }
                    _handle = lPtr;
                    _capacity = nb + 1;
                }

                Marshal.Copy(bytes, 0, _handle, nb);
                ((byte*) _handle)[nb] = 0;
            }
            else
            {
                Dispose();
            }

            Contract.Ensures(String().Equals(s), "string_set");
        }
#endregion

#region Dispose
        /// <summary>
        /// Free allocated memory.
        /// </summary>
        public void Dispose()
        {
            if (_handle != IntPtr.Zero)
            {
                SDL.SDL_free(_handle);
                _handle = IntPtr.Zero;
                _capacity = 0;
            }
        }
#endregion

#region Implementation: Accesss
        private int _capacity;
        private IntPtr _handle;
#endregion

        [ContractInvariantMethod]
        private void ClassInvariant()
        {
            //			Contract.Invariant (_handle != IntPtr.Zero, "internal pointer not null");
            //			Contract.Invariant (_capacity >= 0, "capacity non-negative");
        }
    }
}
