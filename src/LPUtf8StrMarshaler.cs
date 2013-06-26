/* SDL2# - C# Wrapper for SDL2
 *
 * Copyright (c) 2013 Ethan Lee.
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
 * Ethan "flibitijibibo" Lee <flibitijibibo@flibitijibibo.com>
 *
 */

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SDL2
{
	internal unsafe class LPUtf8StrMarshaler : ICustomMarshaler
	{
		private static LPUtf8StrMarshaler _instance = new LPUtf8StrMarshaler();

		private static ICustomMarshaler GetInstance(string cookie)
		{
			return _instance;
		}

		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			var ptr = (byte*)pNativeData;
			while (*ptr != 0)
			{
				ptr++;
			}
			var bytes = new byte[ptr - (byte*)pNativeData];
			Marshal.Copy(pNativeData, bytes, 0, bytes.Length);
			return Encoding.UTF8.GetString(bytes);
		}

		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			var str = ManagedObj as string;
			if (str == null)
			{
				throw new ArgumentException("ManagedObj must be a string.", "ManagedObj");
			}
			var bytes = Encoding.UTF8.GetBytes(str);
			var mem = Marshal.AllocHGlobal(bytes.Length + 1);
			Marshal.Copy(bytes, 0, mem, bytes.Length);
			((byte*)mem)[bytes.Length] = 0;
			return mem;
		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{
			Marshal.FreeHGlobal(pNativeData);
		}

		public void CleanUpManagedData(object ManagedObj)
		{
		}

		public int GetNativeDataSize()
		{
			return -1;
		}
	}
}
