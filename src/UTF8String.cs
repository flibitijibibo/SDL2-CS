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
	public unsafe class Utf8String : IDisposable
	{
#region Initialization
		/// <summary>
		/// Initialize instance with .NET string <see cref="s"/>
		/// </summary>
		/// <param name="s">.NET string to wrap into a UTF8 sequence of unmanaged bytes.</param>
		public Utf8String(string s)
		{
			Contract.Requires(s != null, "s is not null");

			byte[] bytes = Encoding.UTF8.GetBytes(s);
			int nb = bytes.Length;
			IntPtr lPtr = SDL.SDL_malloc((IntPtr)(nb + 1));
			if (lPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException("Cannot Allocate Utf8String");
			}
			else
			{
				Marshal.Copy(bytes, 0, lPtr, nb);
				((byte*)lPtr)[nb] = 0;
			}
			_count = nb;
			_capacity = nb + 1;
			_handle = lPtr;

			Contract.Ensures((_handle != IntPtr.Zero), "handle set");
			Contract.Ensures((_capacity >= s.Length + 1), "capacity_greater_than_input");
			Contract.Ensures(ReferenceEquals(s, String()) || (String().Equals(s)), "string_set");
		}
#endregion

#region Access
		/// <summary>
		/// Access to the underlying unmanaged memory.
		/// </summary>
		public IntPtr Handle { get { return _handle; } }
#endregion

#region Statics
		/// <summary>
		/// Avoid allocating external memory by reusing an internal buffer for the UTF-8 encoded representation of <paramref name="s"/>
		/// and returns a pointer to that representation.
		/// </summary>
		/// <remarks>Consecutive calls to this routine will invalidate any previous calls. Use <see cref="ReusableBufferPtrBis"/> when you need another handle.</remarks>
		/// <param name="s">String to encode.</param>
		public static IntPtr ReusableBufferPtr(string s)
		{
			var buf = _buffer1;
			if (buf == null)
			{
				buf = new Utf8String(s);
				_buffer1 = buf;
			}
			else
			{
				buf.SetString(s);
			}
			return buf._handle;
		}

		/// <summary>
		/// Avoid allocating external memory by reusing an internal buffer for the UTF-8 encoded representation of <paramref name="s"/>
		/// and returns a pointer to that representation.
		/// </summary>
		/// <remarks>Consecutive calls to this routine will invalidate any previous calls. Use <see cref="ReusableBufferPtr"/> when you need another handle.</remarks>
		/// <param name="s">String to encode.</param>	
		public static IntPtr ReusableBufferPtrBis(string s)
		{
			var buf = _buffer2;
			if (buf == null)
			{
				buf = new Utf8String(s);
				_buffer2 = buf;
			}
			else
			{
				buf.SetString(s);
			}
			return buf._handle;
		}

		/// <summary>
		/// String instance corresponding to the null terminated bytes pointed by <paramref name="o"/>.
		/// </summary>
		/// <param name="o">Unmanaged pointer from which data will be read.</param>
		public static string String(byte* o)
		{
			if (o == null)
			{
				return null;
			}
			else
			{
				byte* ptr = o;
				// Count number of available bytes.
				while (*ptr != 0) { ptr++; }
				// `nb' contains the number of bytes not including the null terminating one.
				long nb = ptr - o;
				if (nb > int.MaxValue)
				{
					throw new ArgumentOutOfRangeException("UTF-8 string `o' is too large");
				}
#if !NET46
				byte[] bytes = new byte[nb];
				Marshal.Copy((IntPtr)o, bytes, 0, (int)nb);
				return Encoding.UTF8.GetString(bytes, 0, (int) nb);
#else
				return Encoding.UTF8.GetString((byte*) o, (int) nb);
#endif
			}
		}

		/// <summary>
		/// Overload of <see cref="String(byte *)"/>
		/// </summary>
		public static string String(IntPtr o)
		{
			return String((byte*)o);
		}

		/// <summary>
		/// String instance corresponding to the sequence of the first <paramref name="n"/> bytes pointed by <paramref name="o"/>.
		/// </summary>
		/// <param name="o">Unmanaged pointer from which data will be read.</param>
		/// <param name="n">Index from where to copy the data</param>
		public static string String(byte* o, int n)
		{
#if !NET46
			byte[] bytes = new byte[n];
			Marshal.Copy((IntPtr)o, bytes, 0, n);
			return Encoding.UTF8.GetString(bytes, 0, n);
#else
			return Encoding.UTF8.GetString((byte*) o, n);
#endif
		}

		/// <summary>
		/// Overload of <see cref="String(byte *, int)"/>
		/// </summary>
		public static string String(IntPtr o, int n)
		{
			return String((byte*)o, n);
		}

		/// <summary>
		/// .NET string representation of current UTF-8 encoded string.
		/// </summary>
		/// <returns></returns>
		public string String()
		{
			return String(_handle, _count);
		}
#endregion

#region Element change
		/// <summary>
		/// Set current with <paramref name="s"/>.
		/// </summary>
		/// <param name="s">String to convert into UTF-8.</param>
		public void SetString(string s)
		{
			if (s != null)
			{
					// This code is not optimized. One could actually read the string and copy the content
					// directly in _handle without hitting any allocation. This would require us to include
					// the logic of converting .NET strings into UTF8 ourselves, something we do not want
					// to do for the time being.
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				int nb = bytes.Length;
				if (nb >= _capacity)
				{
						// By default increase size by the max of 50% or new capacity nb.
					int newSize = Math.Max(_capacity + _capacity / 2, nb + 1);
					IntPtr lPtr = SDL.SDL_realloc(_handle, (IntPtr) newSize);
					if (lPtr == IntPtr.Zero)
					{
						Dispose();
						throw new OutOfMemoryException("Cannot reallocate Utf8String");
					}
					_handle = lPtr;
					_capacity = newSize;
				}
				_count = nb;

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
		/// Free external memory held by current.
		/// </summary>
		public void Dispose()
		{
			if (_handle != IntPtr.Zero)
			{
				SDL.SDL_free(_handle);
				_handle = IntPtr.Zero;
				_capacity = 0;
				_count = 0;
			}
		}

		/// <summary>
		/// Finalizer to free external memory held by current if not already done.
		/// </summary>
		~Utf8String()
		{
			Dispose();
		}

		/// <summary>
		/// Explicit interface implementation of the Dispose pattern.
		/// </summary>
		void IDisposable.Dispose()
		{
			Dispose();
			// No need to have the finalizer running now.
			GC.SuppressFinalize(this);
		}
		
#endregion

#region Implementation: Accesss
		/// <summary>
		/// Number of bytes used to represent the UTF8 representation of the .NET string (not including the null-terminator).
		/// </summary>
		private int _count;

		/// <summary>
		/// Number of bytes used to hold the UTF8 representation of the .NET string (including the null-terminator).
		/// </summary>
		private int _capacity;

		/// <summary>
		/// Storage where we hold the UTF8 representation of the .NET string.
		/// </summary>
		private IntPtr _handle;

		[ThreadStatic] private static Utf8String _buffer1;
		[ThreadStatic] private static Utf8String _buffer2;
#endregion

	}
}
