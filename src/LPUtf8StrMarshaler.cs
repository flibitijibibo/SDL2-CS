using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SDL2 {
	internal unsafe class LPUtf8StrMarshaler : ICustomMarshaler {
		static LPUtf8StrMarshaler _instance = new LPUtf8StrMarshaler();

		static ICustomMarshaler GetInstance (string cookie) {
			return _instance;
		}

		public object MarshalNativeToManaged (IntPtr pNativeData) {
			var ptr = (byte*)pNativeData;
			while (*ptr != 0)
				ptr++;
			var bytes = new byte[ptr - (byte*)pNativeData];
			Marshal.Copy(pNativeData, bytes, 0, bytes.Length);
			return Encoding.UTF8.GetString(bytes);
		}

		public IntPtr MarshalManagedToNative (object ManagedObj) {
			var str = ManagedObj as string;
			if (str == null)
				throw new ArgumentException("ManagedObj must be a string.", "ManagedObj");
			var bytes = Encoding.UTF8.GetBytes(str);
			var mem = Marshal.AllocHGlobal(bytes.Length + 1);
			Marshal.Copy(bytes, 0, mem, bytes.Length);
			((byte*)mem)[bytes.Length] = 0;
			return mem;
		}

		public void CleanUpNativeData (IntPtr pNativeData) {
			Marshal.FreeHGlobal(pNativeData);
		}

		public void CleanUpManagedData (object ManagedObj) {
		}

		public int GetNativeDataSize () {
			return -1;
		}
	}
}
