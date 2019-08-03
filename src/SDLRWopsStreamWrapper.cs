using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.InteropServices;

namespace Snowball.Platforms
{
    public class SDLRWopsStreamWrapper : IDisposable
    {
        public const int RW_SEEK_SET = 0;
        public const int RW_SEEK_CUR = 1;
        public const int RW_SEEK_END = 2;

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_RWops
        {
            public IntPtr size;
            public IntPtr seek;
            public IntPtr read;
            public IntPtr write;
            public IntPtr close;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate long SizeFunc(IntPtr context);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate long SeekFunc(
                IntPtr context,
                long offset,
                int whence
            );

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr ReadFunc(
                IntPtr context,
                IntPtr ptr,
                IntPtr size,
                IntPtr num
            );

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr WriteFunc(
                IntPtr context,
                IntPtr ptr,
                IntPtr size,
                IntPtr num
            );

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int CloseFunc(IntPtr context);
        }

        [DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_AllocRW();

        [DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeRW(IntPtr area);

        private static readonly SDL_RWops.SizeFunc sizeFunc = StaticSize;
        private static readonly SDL_RWops.SeekFunc seekFunc = StaticSeek;
        private static readonly SDL_RWops.ReadFunc readFunc = StaticRead;
        private static readonly SDL_RWops.WriteFunc writeFunc = StaticWrite;
        private static readonly SDL_RWops.CloseFunc closeFunc = StaticClose;

        private static readonly IntPtr sizePtr = Marshal.GetFunctionPointerForDelegate(sizeFunc);
        private static readonly IntPtr seekPtr = Marshal.GetFunctionPointerForDelegate(seekFunc);
        private static readonly IntPtr readPtr = Marshal.GetFunctionPointerForDelegate(readFunc);
        private static readonly IntPtr writePtr = Marshal.GetFunctionPointerForDelegate(writeFunc);
        private static readonly IntPtr closePtr = Marshal.GetFunctionPointerForDelegate(closeFunc);

        private static ConcurrentDictionary<IntPtr, SDLRWopsStreamWrapper> streams = new ConcurrentDictionary<IntPtr, SDLRWopsStreamWrapper>();

        private readonly Stream _stream;
        private IntPtr _rwops;

        public SDLRWopsStreamWrapper(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));

            _rwops = SDL_AllocRW();
            unsafe
            {
                SDL_RWops* rwopsPtr = (SDL_RWops*)_rwops;
                rwopsPtr->size = sizePtr;
                rwopsPtr->seek = seekPtr;
                rwopsPtr->read = readPtr;
                rwopsPtr->write = writePtr;
                rwopsPtr->close = closePtr;
            }

            streams.TryAdd(_rwops, this);
        }

        public static implicit operator IntPtr(SDLRWopsStreamWrapper streamWrapper) => streamWrapper._rwops;

        public void Dispose()
        {
            if (_rwops != IntPtr.Zero)
            {
                SDL_FreeRW(_rwops);
            }
        }

        private static long StaticSize(IntPtr context)
        {
            if (streams.TryGetValue(context, out var stream))
                return stream.Size();

            return -1;
        }

        private long Size()
        {
            return _stream.Length;
        }

        private static long StaticSeek(IntPtr context, long offset, int whence)
        {
            if (streams.TryGetValue(context, out var stream))
                return stream.Seek(offset, whence);

            return -1;
        }

        private long Seek(long offset, int whence)
        {
            long result;

            switch (whence)
            {
                case RW_SEEK_SET:
                    result = _stream.Seek(offset, SeekOrigin.Begin);
                    break;

                case RW_SEEK_CUR:
                    result = _stream.Seek(offset, SeekOrigin.Current);
                    break;

                case RW_SEEK_END:
                    result = _stream.Seek(offset, SeekOrigin.End);
                    break;

                default:
                    result = -1;
                    break;
            }

            return result;
        }

        private static IntPtr StaticRead(IntPtr context, IntPtr ptr, IntPtr size, IntPtr num)
        {
            if (streams.TryGetValue(context, out var stream))
                return stream.Read(ptr, size, num);

            return IntPtr.Zero;
        }

        private IntPtr Read(IntPtr ptr, IntPtr size, IntPtr num)
        {
            int length = size.ToInt32() * num.ToInt32();
            var buffer = new byte[length];

            length = _stream.Read(buffer, 0, length);

#if NETSTANDARD2_0
            unsafe
            {
                fixed (void* bufferPtr = buffer)
                {
                    Buffer.MemoryCopy(bufferPtr, (void*)ptr, length, length);
                }
            }
#else
            Marshal.Copy(buffer, 0, ptr, length);
#endif

            return (IntPtr)length;
        }

        private static IntPtr StaticWrite(IntPtr context, IntPtr ptr, IntPtr size, IntPtr num)
        {
            if (streams.TryGetValue(context, out var stream))
                return stream.Write(ptr, size, num);

            return IntPtr.Zero;
        }

        private IntPtr Write(IntPtr ptr, IntPtr size, IntPtr num)
        {
            int length = size.ToInt32() * num.ToInt32();
            var buffer = new byte[length];

#if NETSTANDARD2_0
            unsafe
            {
                fixed (void* bufferPtr = buffer)
                {

                    Buffer.MemoryCopy(ptr.ToPointer(), bufferPtr, length, length);
                }
            }
#else
            Marshal.Copy(ptr, buffer, 0, length);
#endif

            return (IntPtr)length;
        }

        private static int StaticClose(IntPtr context)
        {
            if (streams.TryGetValue(context, out var stream))
                return stream.Close();

            return 0;
        }

        private int Close()
        {
            SDL_FreeRW(_rwops);
            _rwops = IntPtr.Zero;
            return 0;
        }
    }
}