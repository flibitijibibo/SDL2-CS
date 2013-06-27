#region License
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
#endregion

#region Using Statements
using System;
using System.Runtime.InteropServices;
#endregion

/* We have lots of padding fields, which we want private */
#pragma warning disable 0169

namespace SDL2
{
	public static class SDL
	{
		#region SDL2# Variables
		
		/* Used by DllImport to load the native library. */
		private const string nativeLibName = "SDL2.dll";
		
		#endregion
		
		#region SDL_stdinc.h
		
		public static uint SDL_FOURCC(byte A, byte B, byte C, byte D)
		{
			return (uint) (A | (B << 8) | (C << 16) | (D << 24));
		}
		
		public enum SDL_bool
		{
			SDL_FALSE = 0,
			SDL_TRUE = 1
		}
		
		#endregion
		
		#region SDL_rwops.h
		
		/* Note about SDL2# and RWops:
		 * These functions are currently not supported for public use.
		 * They are only meant to be used internally in functions marked with
		 * the phrase "THIS IS AN RWops FUNCTION!"
		 */
		
		[DllImport(nativeLibName, EntryPoint = "SDL_RWFromFile", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr INTERNAL_SDL_RWFromFile(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string file,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string mode
		);
		
		#endregion

		#region SDL_main.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetMainReady();

		#endregion
		
		#region SDL.h
		
		public const uint SDL_INIT_TIMER =		0x00000001;
		public const uint SDL_INIT_AUDIO =		0x00000010;
		public const uint SDL_INIT_VIDEO =		0x00000020;
		public const uint SDL_INIT_JOYSTICK =		0x00000200;
		public const uint SDL_INIT_HAPTIC =		0x00001000;
		public const uint SDL_INIT_GAMECONTROLLER =	0x00002000;
		public const uint SDL_INIT_NOPARACHUTE =	0x00100000;
		public const uint SDL_INIT_EVERYTHING = (
			SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO |
			SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC |
			SDL_INIT_GAMECONTROLLER
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_Init(uint flags);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_InitSubSystem(uint flags);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Quit();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_QuitSubSystem(uint flags);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WasInit(uint flags);
		
		#endregion
		
		#region SDL_hints.h
		
		public const string SDL_HINT_FRAMEBUFFER_ACCELERATION =
			"SDL_FRAMEBUFFER_ACCELERATION";
		public const string SDL_HINT_RENDER_DRIVER =
			"SDL_RENDER_DRIVER";
		public const string SDL_HINT_RENDER_OPENGL_SHADERS =
			"SDL_RENDER_OPENGL_SHADERS";
		public const string SDL_HINT_RENDER_VSYNC =
			"SDL_RENDER_VSYNC";
		public const string SDL_HINT_VIDEO_X11_XVIDMODE =
			"SDL_VIDEO_X11_XVIDMODE";
		public const string SDL_HINT_VIDEO_X11_XINERAMA =
			"SDL_VIDEO_X11_XINERAMA";
		public const string SDL_HINT_VIDEO_X11_XRANDR =
			"SDL_VIDEO_X11_XRANDR";
		public const string SDL_HINT_GRAB_KEYBOARD =
			"SDL_GRAB_KEYBOARD";
		public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS =
			"SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
		public const string SDL_HINT_IDLE_TIMER_DISABLED =
			"SDL_IOS_DLE_TIMER_DISABLED";
		public const string SDL_HINT_ORIENTATIONS =
			"SDL_IOS_ORIENTATIONS";
		public const string SDL_HINT_XINPUT_ENABLED =
			"SDL_XINPUT_ENABLED";
		public const string SDL_HINT_GAMECONTROLLERCONFIG =
			"SDL_GAMECONTROLLERCONFIG";
		public const string SDL_HINT_ALLOW_TOPMOST =
			"SDL_ALLOW_TOPMOST";
		
		public enum SDL_HintPriority
		{
			SDL_HINT_DEFAULT,
			SDL_HINT_NORMAL,
			SDL_HINT_OVERRIDE
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ClearHints();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetHint(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string name
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_SetHint(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string name,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string value
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_SetHintWithPriority(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string name,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string value,
			SDL_HintPriority priority
		);
		
		#endregion
		
		#region SDL_error.h
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ClearError();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetError();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetError(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		#endregion
		
		#region SDL_log.h
		
		/* Begin nameless enum SDL_LOG_CATEGORY */
		public const int SDL_LOG_CATEGORY_APPLICATION = 0;
		public const int SDL_LOG_CATEGORY_ERROR = 1;
		public const int SDL_LOG_CATEGORY_ASSERT = 2;
		public const int SDL_LOG_CATEGORY_SYSTEM = 3;
		public const int SDL_LOG_CATEGORY_AUDIO = 4;
		public const int SDL_LOG_CATEGORY_VIDEO = 5;
		public const int SDL_LOG_CATEGORY_RENDER = 6;
		public const int SDL_LOG_CATEGORY_INPUT = 7;
		public const int SDL_LOG_CATEGORY_TEST = 8;
		
		/* Reserved for future SDL library use */
		public const int SDL_LOG_CATEGORY_RESERVED1 = 9;
		public const int SDL_LOG_CATEGORY_RESERVED2 = 10;
		public const int SDL_LOG_CATEGORY_RESERVED3 = 11;
		public const int SDL_LOG_CATEGORY_RESERVED4 = 12;
		public const int SDL_LOG_CATEGORY_RESERVED5 = 13;
		public const int SDL_LOG_CATEGORY_RESERVED6 = 14;
		public const int SDL_LOG_CATEGORY_RESERVED7 = 15;
		public const int SDL_LOG_CATEGORY_RESERVED8 = 16;
		public const int SDL_LOG_CATEGORY_RESERVED9 = 17;
		public const int SDL_LOG_CATEGORY_RESERVED10 = 18;
		
		/* Beyond this point is reserved for application use, e.g.
			enum {
				LOG_CATEGORY_AWESOME1 = SDL_LOG_CATEGORY_CUSTOM,
				LOG_CATEGORY_AWESOME2,
				LOG_CATEGORY_AWESOME3,
				...
			};
		*/
		public const int SDL_LOG_CATEGORY_CUSTOM = 19;
		/* End nameless enum SDL_LOG_CATEGORY */
		
		public enum SDL_LogPriority
		{
			SDL_LOG_PRIORITY_VERBOSE = 1,
			SDL_LOG_PRIORITY_DEBUG,
			SDL_LOG_PRIORITY_INFO,
			SDL_LOG_PRIORITY_WARN,
			SDL_LOG_PRIORITY_ERROR,
			SDL_LOG_PRIORITY_CRITICAL,
			SDL_NUM_LOG_PRIORITIES
		}
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_LogOutputFunction(
			IntPtr userdata, // void*
			int category,
			SDL_LogPriority priority,
			IntPtr message // const char*
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Log(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogVerbose(
			int category,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogDebug(
			int category,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogInfo(
			int category,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogWarn(
			int category,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogError(
			int category,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogCritical(
			int category,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogMessage(
			int category,
			SDL_LogPriority priority,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogMessageV(
			int category,
			SDL_LogPriority priority,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_LogPriority SDL_LogGetPriority(
			int category
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogSetPriority(
			int category,
			SDL_LogPriority priority
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogSetAllPriority(
			SDL_LogPriority priority
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogResetPriorities();
		
		/* userdata refers to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogGetOutputFunction(
			ref SDL_LogOutputFunction callback,
			ref IntPtr userdata
		);
		
		/* userdata refers to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogSetOutputFunction(
			SDL_LogOutputFunction callback,
			IntPtr userdata
		);
		
		#endregion
		
		#region SDL_version.h, SDL_revision.h
		
		/* Similar to the headers, this is the version we're expecting to be
		 * running with. You will likely want to check this somewhere in your
		 * program!
		 */
		public const int SDL_MAJOR_VERSION =	2;
		public const int SDL_MINOR_VERSION =	0;
		public const int SDL_PATCHLEVEL =	0;
		
		public static readonly int SDL_COMPILEDVERSION = SDL_VERSIONNUM(
			SDL_MAJOR_VERSION,
			SDL_MINOR_VERSION,
			SDL_PATCHLEVEL
		);
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_version
		{
			public byte major;
			public byte minor;
			public byte patch;
		}
		
		public static void SDL_VERSION(ref SDL_version x)
		{
			x.major = SDL_MAJOR_VERSION;
			x.minor = SDL_MINOR_VERSION;
			x.patch = SDL_PATCHLEVEL;
		}
		
		public static int SDL_VERSIONNUM(int X, int Y, int Z)
		{
			return (X * 1000) + (Y * 100) + Z;
		}
		
		public static bool SDL_VERSION_ATLEAST(int X, int Y, int Z)
		{
			return (SDL_COMPILEDVERSION >= SDL_VERSIONNUM(X, Y, Z));
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void SDL_GetVersion(ref SDL_version ver);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetRevision();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRevisionNumber();
		
		#endregion
		
		#region SDL_video.h
		
		/* Actually, this is from SDL_blendmode.h */
		[Flags]
		public enum SDL_BlendMode
		{
			SDL_BLENDMODE_NONE =	0x00000000,
			SDL_BLENDMODE_BLEND =	0x00000001,
			SDL_BLENDMODE_ADD =	0x00000002,
			SDL_BLENDMODE_MOD =	0x00000004
		}
		
		public enum SDL_GLattr
		{
			SDL_GL_RED_SIZE,
			SDL_GL_GREEN_SIZE,
			SDL_GL_BLUE_SIZE,
			SDL_GL_ALPHA_SIZE,
			SDL_GL_BUFFER_SIZE,
			SDL_GL_DOUBLEBUFFER,
			SDL_GL_DEPTH_SIZE,
			SDL_GL_STENCIL_SIZE,
			SDL_GL_ACCUM_RED_SIZE,
			SDL_GL_ACCUM_GREEN_SIZE,
			SDL_GL_ACCUM_BLUE_SIZE,
			SDL_GL_ACCUM_ALPHA_SIZE,
			SDL_GL_STEREO,
			SDL_GL_MULTISAMPLEBUFFERS,
			SDL_GL_MULTISAMPLESAMPLES,
			SDL_GL_ACCELERATED_VISUAL,
			SDL_GL_RETAINED_BACKING,
			SDL_GL_CONTEXT_MAJOR_VERSION,
			SDL_GL_CONTEXT_MINOR_VERSION,
			SDL_GL_CONTEXT_EGL,
			SDL_GL_CONTEXT_FLAGS,
			SDL_GL_PROFILE_MASK,
			SDL_GL_SHARE_WITH_CURRENT_CONTEXT
		}
		
		public enum SDL_WindowEventID : byte
		{
			SDL_WINDOWEVENT_NONE,
			SDL_WINDOWEVENT_SHOWN,
			SDL_WINDOWEVENT_HIDDEN,
			SDL_WINDOWEVENT_EXPOSED,
			SDL_WINDOWEVENT_MOVED,
			SDL_WINDOWEVENT_RESIZED,
			SDL_WINDOWEVENT_SIZE_CHANGED,
			SDL_WINDOWEVENT_MINIMIZED,
			SDL_WINDOWEVENT_MAXIMIZED,
			SDL_WINDOWEVENT_RESTORED,
			SDL_WINDOWEVENT_ENTER,
			SDL_WINDOWEVENT_LEAVE,
			SDL_WINDOWEVENT_FOCUS_GAINED,
			SDL_WINDOWEVENT_FOCUS_LOST,
			SDL_WINDOWEVENT_CLOSE,
		}
		
		[Flags]
		public enum SDL_WindowFlags
		{
			SDL_WINDOW_FULLSCREEN =		0x00000001,
			SDL_WINDOW_OPENGL =		0x00000002,
			SDL_WINDOW_SHOWN =		0x00000004,
			SDL_WINDOW_HIDDEN =		0x00000008,
			SDL_WINDOW_BORDERLESS =		0x00000010,
			SDL_WINDOW_RESIZABLE =		0x00000020,
			SDL_WINDOW_MINIMIZED =		0x00000040,
			SDL_WINDOW_MAXIMIZED =		0x00000080,
			SDL_WINDOW_INPUT_GRABBED =	0x00000100,
			SDL_WINDOW_INPUT_FOCUS =	0x00000200,
			SDL_WINDOW_MOUSE_FOCUS =	0x00000400,
			SDL_WINDOW_FULLSCREEN_DESKTOP =
				(SDL_WINDOW_FULLSCREEN | 0x00001000),
			SDL_WINDOW_FOREIGN =		0x00000800,
		}
		
		public const int SDL_WINDOWPOS_UNDEFINED_MASK =	0x1FFF0000;
		public const int SDL_WINDOWPOS_CENTERED_MASK =	0x2FFF0000;
		public const int SDL_WINDOWPOS_UNDEFINED =		0x1FFF0000;
		public const int SDL_WINDOWPOS_CENTERED =		0x2FFF0000;
		
		public static int SDL_WINDOWPOS_UNDEFINED_DISPLAY(int X)
		{
			return (SDL_WINDOWPOS_UNDEFINED_MASK | X);
		}
		
		public static bool SDL_WINDOWPOS_ISUNDEFINED(int X)
		{
			return (X & 0xFFFF0000) == SDL_WINDOWPOS_UNDEFINED_MASK;
		}
		
		public static int SDL_WINDOWPOS_CENTERED_DISPLAY(int X)
		{
			return (SDL_WINDOWPOS_CENTERED_MASK | X);
		}
		
		public static bool SDL_WINDOWPOS_ISCENTERED(int X)
		{
			return (X & 0xFFFF0000) == SDL_WINDOWPOS_CENTERED_MASK;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_DisplayMode
		{
			public uint format;
			public int w;
			public int h;
			public int refresh_rate;
			public IntPtr driverdata; // void*
		}
		
		/* IntPtr refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateWindow(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string title,
			int x,
			int y,
			int w,
			int h,
			SDL_WindowFlags flags
		);
		
		/* window and renderer refer to an SDL_Window* and SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_CreateWindowAndRenderer(
			int width,
			int height,
			SDL_WindowFlags window_flags,
			ref IntPtr window,
			ref IntPtr renderer
		);
		
		/* IntPtr refers to an SDL_Window*. data is a void* pointer. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateWindowFrom(IntPtr data);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyWindow(IntPtr window);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DisableScreenSaver();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EnableScreenSaver();
		
		/* IntPtr refers to an SDL_DisplayMode. Just use closest. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetClosestDisplayMode(
			int displayIndex,
			ref SDL_DisplayMode mode,
			ref SDL_DisplayMode closest
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetCurrentDisplayMode(
			int displayIndex,
			ref SDL_DisplayMode mode
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetCurrentVideoDriver();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDesktopDisplayMode(
			int displayIndex,
			ref SDL_DisplayMode mode
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDisplayBounds(
			int displayIndex,
			ref SDL_Rect rect
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDisplayMode(
			int displayIndex,
			int modeIndex,
			ref SDL_DisplayMode mode
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumDisplayModes(
			int displayIndex
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumVideoDisplays();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumVideoDrivers();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetVideoDriver(
			int index
		);

		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowBrightness(
			IntPtr window
		);
		
		/* IntPtr refers to void* data. window refers to an SDL_Window*. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowData(
			IntPtr window,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string name
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowDisplayIndex(
			IntPtr window
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowDisplayMode(
			IntPtr window,
			ref SDL_DisplayMode mode
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowFlags(IntPtr window);
		
		/* IntPtr refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowFromID(uint id);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowGammaRamp(
			IntPtr window,
			ref ushort red,
			ref ushort green,
			ref ushort blue
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_GetWindowGrab(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowID(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowPixelFormat(
			IntPtr window
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetWindowPosition(
			IntPtr window,
			ref int x,
			ref int y
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetWindowSize(
			IntPtr window,
			ref int w,
			ref int h
		);
		
		/* IntPtr refers to an SDL_Surface*, window to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowSurface(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetWindowTitle(
			IntPtr window
		);

		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_BindTexture(
			IntPtr texture,
			ref float texw,
			ref float texh
		);
		
		/* IntPtr and window refer to an SDL_GLContext and SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_CreateContext(IntPtr window);
		
		/* context refers to an SDL_GLContext */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_DeleteContext(IntPtr context);
		
		/* IntPtr refers to a function pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_GetProcAddress(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string proc
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_GL_ExtensionSupported(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string extension
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_GetAttribute(
			SDL_GLattr attr,
			ref int value
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_GetSwapInterval();
		
		/* window and context refer to an SDL_Window* and SDL_GLContext */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_MakeCurrent(
			IntPtr window,
			IntPtr context
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_SetAttribute(
			SDL_GLattr attr,
			int value
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_SetSwapInterval(int interval);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_SwapWindow(IntPtr window);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_UnbindTexture(IntPtr texture);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_HideWindow(IntPtr window);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_IsScreenSaverEnabled();
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MaximizeWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MinimizeWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RaiseWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RestoreWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowBrightness(
			IntPtr window,
			float brightness
		);
		
		/* IntPtr and userdata are void*, window is an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SetWindowData(
			IntPtr window,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string name,
			IntPtr userdata
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowDisplayMode(
			IntPtr window,
			ref SDL_DisplayMode mode
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowFullscreen(
			IntPtr window,
			uint flags
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowGammaRamp(
			IntPtr window,
			ref ushort red,
			ref ushort green,
			ref ushort blue
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowGrab(
			IntPtr window,
			SDL_bool grabbed
		);
		
		/* window refers to an SDL_Window*, icon to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowIcon(
			IntPtr window,
			IntPtr icon
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowPosition(
			IntPtr window,
			int x,
			int y
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowSize(
			IntPtr window,
			int w,
			int h
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowBordered(
			IntPtr window,
			SDL_bool bordered
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowTitle(
			IntPtr window,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string title
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ShowWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateWindowSurface(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateWindowSurfaceRects(
			IntPtr window,
			SDL_Rect[] rects,
			int numrects
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_VideoInit(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string driver_name
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_VideoQuit();
		
		#endregion
		
		#region SDL_render.h
		
		[Flags]
		public enum SDL_RendererFlags
		{
			SDL_RENDERER_SOFTWARE =		0x00000001,
			SDL_RENDERER_ACCELERATED =	0x00000002,
			SDL_RENDERER_PRESENTVSYNC =	0x00000004,
			SDL_RENDERER_TARGETTEXTURE =	0x00000008
		}
		
		[Flags]
		public enum SDL_RendererFlip
		{
			SDL_FLIP_NONE =		0x00000000,
			SDL_FLIP_HORIZONTAL =	0x00000001,
			SDL_FLIP_VERTICAL =	0x00000002
		}
		
		public enum SDL_TextureAccess
		{
			SDL_TEXTUREACCES_STATIC,
			SDL_TEXTUREACCESS_STREAMING,
			SDL_TEXTUREACCESS_TARGET
		}
		
		[Flags]
		public enum SDL_TextureModulate
		{
			SDL_TEXTUREMODULATE_NONE =		0x00000000,
			SDL_TEXTUREMODULATE_HORIZONTAL =	0x00000001,
			SDL_TEXTUREMODULATE_VERTICAL =		0x00000002
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SDL_RendererInfo
		{
			public IntPtr name; // const char*
			public uint flags;
			public uint num_texture_formats;
			public fixed uint texture_formats[16];
			public int max_texture_width;
			public int max_texture_height;
		}
		
		/* IntPtr refers to an SDL_Renderer*, window to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRenderer(
			IntPtr window,
			int index,
			uint flags
		);
		
		/* IntPtr refers to an SDL_Renderer*, surface to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRenderer(IntPtr surface);
		
		/* IntPtr refers to an SDL_Texture*, renderer to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTexture(
			IntPtr renderer,
			uint format,
			int access,
			int w,
			int h
		);
		
		/* IntPtr refers to an SDL_Texture*
		 * renderer refers to an SDL_Renderer*
		 * surface refers to an SDL_Surface*
		 */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTextureFromSurface(
			IntPtr renderer,
			IntPtr surface
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyRenderer(IntPtr renderer);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyTexture(IntPtr texture);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumRenderDrivers();
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRenderDrawBlendMode(
			IntPtr renderer,
			ref SDL_BlendMode blendMode
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRenderDrawColor(
			IntPtr renderer,
			ref byte r,
			ref byte g,
			ref byte b,
			ref byte a
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRenderDriverInfo(
			int index,
			ref SDL_RendererInfo info
		);
		
		/* IntPtr refers to an SDL_Renderer*, window to an SDL_Window* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderer(IntPtr window);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRendererInfo(
			IntPtr renderer,
			ref SDL_RendererInfo info
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetTextureAlphaMod(
			IntPtr texture,
			ref byte alpha
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetTextureBlendMode(
			IntPtr texture,
			ref SDL_BlendMode blendMode
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetTextureColorMod(
			IntPtr texture,
			ref byte r,
			ref byte g,
			ref byte b
		);
		
		/* texture refers to an SDL_Texture*, pixels to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LockTexture(
			IntPtr texture,
			ref SDL_Rect rect,
			ref IntPtr pixels,
			ref int pitch
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_QueryTexture(
			IntPtr texture,
			ref uint format,
			ref int access,
			ref int w,
			ref int h
		);
		
		/* texture refers to an SDL_Texture, pixels to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_QueryTexturePixels(
			IntPtr texture,
			ref IntPtr pixels,
			ref int pitch
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderClear(IntPtr renderer);
		
		/* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopy(
			IntPtr renderer,
			IntPtr texture,
			ref SDL_Rect srcrect,
			ref SDL_Rect dstrect
		);
		
		/* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(
			IntPtr renderer,
			IntPtr texture,
			ref SDL_Rect srcrect,
			ref SDL_Rect dstrect,
			double angle,
			ref SDL_Point center,
			SDL_RendererFlip flip
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawLine(
			IntPtr renderer,
			int x1,
			int y1,
			int x2,
			int y2
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawLines(
			IntPtr renderer,
			SDL_Point[] points,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawPoint(
			IntPtr renderer,
			int x,
			int y
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawPoints(
			IntPtr renderer,
			SDL_Point[] points,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRect(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRects(
			IntPtr renderer,
			SDL_Rect[] rects,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRect(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRects(
			IntPtr renderer,
			SDL_Rect[] rects,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RendererGetViewport(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RenderPresent(IntPtr renderer);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderReadPixels(
			IntPtr renderer,
			ref SDL_Rect rect,
			uint format,
			IntPtr pixels,
			int pitch
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetViewport(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRenderDrawBlendMode(
			IntPtr renderer,
			SDL_BlendMode blendMode
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRenderDrawColor(
			IntPtr renderer,
			byte r,
			byte g,
			byte b,
			byte a
		);
		
		/* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRenderTarget(
			IntPtr renderer,
			IntPtr texture
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureAlphaMod(
			IntPtr texture,
			byte alpha
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureBlendMode(
			IntPtr texture,
			SDL_BlendMode blendMode
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureColorMod(
			IntPtr texture,
			byte r,
			byte g,
			byte b
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockTexture(IntPtr texture);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateTexture(
			IntPtr texture,
			ref SDL_Rect rect,
			IntPtr pixels,
			int pitch
		);
		
		#endregion
		
		#region SDL_pixels.h
		
		public static uint SDL_DEFINE_PIXELFOURCC(byte A, byte B, byte C, byte D)
		{
			return SDL_FOURCC(A, B, C, D);
		}
		
		public static uint SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM type,
			SDL_PIXELORDER_ENUM order,
			SDL_PACKEDLAYOUT_ENUM layout,
			byte bits,
			byte bytes
		) {
			return (uint) (
				(1 << 28) |
				(((byte) type) << 24) |
				(((byte) order) << 20) |
				(((byte) layout) << 16) |
				(bits << 8) |
				(bytes)
			);
		}
		
		public static byte SDL_PIXELFLAG(uint X)
		{
			return (byte) ((X >> 28) & 0x0F);
		}
		
		public static byte SDL_PIXELTYPE(uint X)
		{
			return (byte) ((X >> 24) & 0x0F);
		}
		
		public static byte SDL_PIXELORDER(uint X)
		{
			return (byte) ((X >> 20) & 0x0F);
		}
		
		public static byte SDL_BITSPERPIXEL(uint X)
		{
			return (byte) ((X << 16) & 0x0F);
		}
		
		public static byte SDL_BYTESPERPIXEL(uint X)
		{
			if (SDL_ISPIXELFORMAT_FOURCC(X))
			{
				if (	(X == SDL_PIXELFORMAT_YUY2) ||
						(X == SDL_PIXELFORMAT_UYVY) ||
						(X == SDL_PIXELFORMAT_YVYU)	)
				{
					return 2;
				}
				return 1;
			}
			return (byte) (X & 0xFF);
		}
		
		public static bool SDL_ISPIXELFORMAT_INDEXED(uint format)
		{
			if (SDL_ISPIXELFORMAT_FOURCC(format))
			{
				return false;
			}
			SDL_PIXELTYPE_ENUM pType =
					(SDL_PIXELTYPE_ENUM) SDL_PIXELTYPE(format);
			return (
				pType == SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1 ||
			    pType == SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX4 ||
			    pType == SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX8
			);
		}
		
		public static bool SDL_ISPIXELFORMAT_ALPHA(uint format)
		{
			if (SDL_ISPIXELFORMAT_FOURCC(format))
			{
				return false;
			}
			SDL_PIXELORDER_ENUM pOrder =
					(SDL_PIXELORDER_ENUM) SDL_PIXELORDER(format);
			return (
				pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB ||
			    pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA ||
			    pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR ||
			    pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA
			);
		}
		
		public static bool SDL_ISPIXELFORMAT_FOURCC(uint format)
		{
			return (format == 0) && (SDL_PIXELFLAG(format) != 1);
		}
		
		public enum SDL_PIXELTYPE_ENUM
		{
			SDL_PIXELTYPE_UNKNOWN,
			SDL_PIXELTYPE_INDEX1,
			SDL_PIXELTYPE_INDEX4,
			SDL_PIXELTYPE_INDEX8,
			SDL_PIXELTYPE_PACKED8,
			SDL_PIXELTYPE_PACKED16,
			SDL_PIXELTYPE_PACKED32,
			SDL_PIXELTYPE_ARRAYU8,
			SDL_PIXELTYPE_ARRAYU16,
			SDL_PIXELTYPE_ARRAYU32,
			SDL_PIXELTYPE_ARRAYF16,
			SDL_PIXELTYPE_ARRAYF32
		}
		
		public enum SDL_PIXELORDER_ENUM
		{
			/* BITMAPORDER */
			SDL_BITMAPORDER_NONE,
			SDL_BITMAPORDER_4321,
			SDL_BITMAPORDER_1234,
			/* PACKEDORDER */
			SDL_PACKEDORDER_NONE = 0,
			SDL_PACKEDORDER_XRGB,
			SDL_PACKEDORDER_RGBX,
			SDL_PACKEDORDER_ARGB,
			SDL_PACKEDORDER_RGBA,
			SDL_PACKEDORDER_XBGR,
			SDL_PACKEDORDER_BGRX,
			SDL_PACKEDORDER_ABGR,
			SDL_PACKEDORDER_BGRA,
			/* ARRAYORDER */
			SDL_ARRAYORDER_NONE = 0,
			SDL_ARRAYORDER_RGB,
			SDL_ARRAYORDER_RGBA,
			SDL_ARRAYORDER_ARGB,
			SDL_ARRAYORDER_BGR,
			SDL_ARRAYORDER_BGRA,
			SDL_ARRAYORDER_ABGR
		}
		
		public enum SDL_PACKEDLAYOUT_ENUM
		{
			SDL_PACKEDLAYOUT_NONE,
			SDL_PACKEDLAYOUT_332,
			SDL_PACKEDLAYOUT_4444,
			SDL_PACKEDLAYOUT_1555,
			SDL_PACKEDLAYOUT_5551,
			SDL_PACKEDLAYOUT_565,
			SDL_PACKEDLAYOUT_8888,
			SDL_PACKEDLAYOUT_2101010,
			SDL_PACKEDLAYOUT_1010102
		}
		
		public static readonly uint SDL_PIXELFORMAT_UNKNOWN = 0;
		public static readonly uint SDL_PIXELFORMAT_INDEX1LSB =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1,
				SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_4321,
				0,
				1, 0
			);
		public static readonly uint SDL_PIXELFORMAT_INDEX1MSB =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1,
				SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_1234,
				0,
				1, 0
			);
		public static readonly uint SDL_PIXELFORMAT_INDEX4LSB =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX4,
				SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_4321,
				0,
				4, 0
			);
		public static readonly uint SDL_PIXELFORMAT_INDEX4MSB =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX4,
				SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_1234,
				0,
				4, 0
			);
		public static readonly uint SDL_PIXELFORMAT_INDEX8 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX8,
				0,
				0,
				8, 1
			);
		public static readonly uint SDL_PIXELFORMAT_RGB332 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED8,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_332,
				8, 1
			);
		public static readonly uint SDL_PIXELFORMAT_RGB444 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
				12, 2
			);
		public static readonly uint SDL_PIXELFORMAT_RGB555 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
				15, 2
			);
		public static readonly uint SDL_PIXELFORMAT_BGR555 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1,
				SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_4321,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
				15, 2
			);
		public static readonly uint SDL_PIXELFORMAT_ARGB4444 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_RGBA4444 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_ABGR4444 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_BGRA4444 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_ARGB1555 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_RGBA5551 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_5551,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_ABGR1555 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_BGRA5551 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_5551,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_RGB565 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_565,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_BGR565 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XBGR,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_565,
				16, 2
			);
		public static readonly uint SDL_PIXELFORMAT_RGB24 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_ARRAYU8,
				SDL_PIXELORDER_ENUM.SDL_ARRAYORDER_RGB,
				0,
				24, 3
			);
		public static readonly uint SDL_PIXELFORMAT_BGR24 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_ARRAYU8,
				SDL_PIXELORDER_ENUM.SDL_ARRAYORDER_BGR,
				0,
				24, 3
			);
		public static readonly uint SDL_PIXELFORMAT_RGB888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				24, 4
			);
		public static readonly uint SDL_PIXELFORMAT_RGBX8888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBX,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				24, 4
			);
		public static readonly uint SDL_PIXELFORMAT_BGR888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XBGR,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				24, 4
			);
		public static readonly uint SDL_PIXELFORMAT_BGRX8888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRX,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				24, 4
			);
		public static readonly uint SDL_PIXELFORMAT_ARGB8888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				32, 4
			);
		public static readonly uint SDL_PIXELFORMAT_RGBA8888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				32, 4
			);
		public static readonly uint SDL_PIXELFORMAT_ABGR8888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				32, 4
			);
		public static readonly uint SDL_PIXELFORMAT_BGRA8888 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
				32, 4
			);
		public static readonly uint SDL_PIXELFORMAT_ARGB2101010 =
			SDL_DEFINE_PIXELFORMAT(
				SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
				SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
				SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_2101010,
				32, 4
			);
		public static readonly uint SDL_PIXELFORMAT_YV12 =
			SDL_DEFINE_PIXELFOURCC(
				(byte) 'Y', (byte) 'V', (byte) '1', (byte) '2'
			);
		public static readonly uint SDL_PIXELFORMAT_IYUV =
			SDL_DEFINE_PIXELFOURCC(
				(byte) 'I', (byte) 'Y', (byte) 'U', (byte) 'V'
			);
		public static readonly uint SDL_PIXELFORMAT_YUY2 =
			SDL_DEFINE_PIXELFOURCC(
				(byte) 'Y', (byte) 'U', (byte) 'Y', (byte) '2'
			);
		public static readonly uint SDL_PIXELFORMAT_UYVY =
			SDL_DEFINE_PIXELFOURCC(
				(byte) 'U', (byte) 'Y', (byte) 'V', (byte) 'Y'
			);
		public static readonly uint SDL_PIXELFORMAT_YVYU =
			SDL_DEFINE_PIXELFOURCC(
				(byte) 'Y', (byte) 'V', (byte) 'Y', (byte) 'U'
			);
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Color
		{
			public byte r;
			public byte g;
			public byte b;
			public byte a;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Palette
		{
			public int ncolors;
			public SDL_Color[] colors;
			public int version;
			public int refcount;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_PixelFormat
		{
			public uint format;
			public IntPtr palette; // SDL_Palette*
			public byte BitsPerPixel;
			public byte BytesPerPixel;
			public uint Rmask;
			public uint Gmask;
			public uint Bmask;
			public uint Amask;
			public byte Rloss;
			public byte Gloss;
			public byte Bloss;
			public byte Aloss;
			public byte Rshift;
			public byte Gshift;
			public byte Bshift;
			public byte Ashift;
			public int refcount;
			public IntPtr next; // SDL_PixelFormat*
		}
		
		/* IntPtr refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AllocFormat(uint pixel_format);
		
		/* IntPtr refers to an SDL_Palette* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AllocPalette(int ncolors);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CalculateGammaRamp(
			float gamma,
			ref ushort ramp
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeFormat(IntPtr format);
		
		/* palette refers to an SDL_Palette* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreePalette(IntPtr palette);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetPixelFormatName(
			uint format
		);

		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGB(
			uint pixel,
			IntPtr format,
			ref byte r,
			ref byte g,
			ref byte b
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGBA(
			uint pixel,
			IntPtr format,
			ref byte r,
			ref byte g,
			ref byte b,
			ref byte a
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGB(
			IntPtr format,
			byte r,
			byte g,
			byte b
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGBA(
			IntPtr format,
			byte r,
			byte g,
			byte b,
			byte a
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MasksToPixelFormatEnum(
			int bpp,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_PixelFormatEnumToMasks(
			uint format,
			ref int bpp,
			ref uint Rmask,
			ref uint Gmask,
			ref uint Bmask,
			ref uint Amask
		);
		
		/* palette refers to an SDL_Palette* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetPaletteColors(
			IntPtr palette,
			SDL_Color[] colors,
			int firstcolor,
			int ncolors
		);
		
		/* format and palette refer to an SDL_PixelFormat* and SDL_Palette* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetPixelFormatPalette(
			IntPtr format,
			IntPtr palette
		);
		
		#endregion
		
		#region SDL_rect.h
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Point
		{
			public int x;
			public int y;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Rect
		{
			public int x;
			public int y;
			public int w;
			public int h;
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_EnclosePoints(
			SDL_Point[] points,
			int count,
			ref SDL_Rect clip,
			ref SDL_Rect result
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_HasIntersection(
			ref SDL_Rect A,
			ref SDL_Rect B
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_IntersectRect(
			ref SDL_Rect A,
			ref SDL_Rect B,
			ref SDL_Rect result
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_IntersectRectAndLine(
			ref SDL_Rect rect,
			ref int X1,
			ref int Y1,
			ref int X2,
			ref int Y2
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_RectEmpty(ref SDL_Rect rect);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_RectEquals(
			ref SDL_Rect A,
			ref SDL_Rect B
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnionRect(
			ref SDL_Rect A,
			ref SDL_Rect B,
			ref SDL_Rect result
		);
		
		#endregion
		
		#region SDL_surface.h
		
		public const uint SDL_SWSURFACE =	0x00000000;
		public const uint SDL_PREALLOC =	0x00000001;
		public const uint SDL_RLEACCEL =	0x00000002;
		public const uint SDL_DONTFREE =	0x00000004;
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Surface
		{
			public uint flags;
			public IntPtr format; // SDL_PixelFormat*
			public int w;
			public int h;
			public int pitch;
			public IntPtr pixels; // void*
			public IntPtr userdata; // void*
			public int locked;
			public IntPtr lock_data; // void*
			public SDL_Rect clip_rect;
			public IntPtr map; // SDL_BlitMap*
			public int refcount;
		}
		
		/* surface refers to an SDL_Surface* */
		public static bool SDL_MUSTLOCK(IntPtr surface)
		{
			SDL_Surface sur;
			sur = (SDL_Surface) Marshal.PtrToStructure(
				surface,
				typeof(SDL_Surface)
			);
			return (sur.flags & SDL_RLEACCEL) != 0;
		}
		
		/* src and dst refer to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_BlitSurface(
			IntPtr src,
			ref SDL_Rect srcrect,
			IntPtr dst,
			ref SDL_Rect dstrect
		);
		
		/* src and dst are void* pointers */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_ConvertPixels(
			int width,
			int height,
			uint src_format,
			IntPtr src,
			int src_pitch,
			uint dst_format,
			IntPtr dst,
			int dst_pitch
		);
		
		/* IntPtr refers to an SDL_Surface*
		 * src refers to an SDL_Surface*
		 * fmt refers to an SDL_PixelFormat*
		 */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurface(
			IntPtr src,
			IntPtr fmt,
			uint flags
		);
		
		/* IntPtr refers to an SDL_Surface*, src to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurfaceFormat(
			IntPtr src,
			uint pixel_format,
			uint flags
		);
		
		/* IntPtr refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRGBSurface(
			uint flags,
			int width,
			int height,
			int depth,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		);
		
		/* IntPtr refers to an SDL_Surface*, pixels to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRGBSurfaceFrom(
			IntPtr pixels,
			int width,
			int height,
			int depth,
			int pitch,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		);
		
		/* dst refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_FillRect(
			IntPtr dst,
			ref SDL_Rect rect,
			uint color
		);
		
		/* dst refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_FillRects(
			IntPtr dst,
			SDL_Rect[] rects,
			int count,
			uint color
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeSurface(IntPtr surface);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetClipRect(
			IntPtr surface,
			ref SDL_Rect rect
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetColorKey(
			IntPtr surface,
			ref uint key
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSurfaceAlphaMod(
			IntPtr surface,
			ref byte alpha
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSurfaceBlendMode(
			IntPtr surface,
			ref SDL_BlendMode blendMode
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSurfaceColorMod(
			IntPtr surface,
			ref byte r,
			ref byte g,
			ref byte b
		);

		/* These are for SDL_LoadBMP, which is a macro in the SDL headers. */
		/* IntPtr refers to an SDL_Surface* */
		/* THIS IS AN RWops FUNCTION! */
		[DllImport(nativeLibName, EntryPoint = "SDL_LoadBMP_RW", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_LoadBMP_RW(
			IntPtr src,
			int freesrc
		);
		public static IntPtr SDL_LoadBMP(string file)
		{
			IntPtr rwops = INTERNAL_SDL_RWFromFile(file, "rb");
			return INTERNAL_SDL_LoadBMP_RW(rwops, 1);
		}
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LockSurface(IntPtr surface);
		
		/* src and dst refer to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LowerBlit(
			IntPtr src,
			ref SDL_Rect srcrect,
			IntPtr dst,
			ref SDL_Rect dstrect
		);
		
		/* src and dst refer to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LowerBlitScaled(
			IntPtr src,
			ref SDL_Rect srcrect,
			IntPtr dst,
			ref SDL_Rect dstrect
		);
		
		/* These are for SDL_SaveBMP, which is a macro in the SDL headers. */
		/* IntPtr refers to an SDL_Surface* */
		/* THIS IS AN RWops FUNCTION! */
		[DllImport(nativeLibName, EntryPoint = "SDL_SaveBMP_RW", CallingConvention = CallingConvention.Cdecl)]
		private static extern int INTERNAL_SDL_SaveBMP_RW(
			IntPtr surface,
			IntPtr src,
			int freesrc
		);
		public static int SDL_SaveBMP(IntPtr surface, string file)
		{
			IntPtr rwops = INTERNAL_SDL_RWFromFile(file, "wb");
			return INTERNAL_SDL_SaveBMP_RW(surface, rwops, 1);
		}
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_SetClipRect(
			IntPtr surface,
			ref SDL_Rect rect
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetColorKey(
			IntPtr surface,
			int flag,
			uint key
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceAlphaMod(
			IntPtr surface,
			byte alpha
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceBlendMode(
			IntPtr surface,
			SDL_BlendMode blendMode
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceColorMod(
			IntPtr surface,
			byte r,
			byte g,
			byte b
		);
		
		/* surface refers to an SDL_Surface*, palette to an SDL_Palette* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfacePalette(
			IntPtr surface,
			IntPtr palette
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceRLE(
			IntPtr surface,
			int flag
		);
		
		/* src and dst refer to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SoftStretch(
			IntPtr src,
			ref SDL_Rect srcrect,
			IntPtr dst,
			ref SDL_Rect dstrect
		);
		
		/* surface refers to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockSurface(IntPtr surface);
		
		/* src and dst refer to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpperBlit(
			IntPtr src,
			ref SDL_Rect srcrect,
			IntPtr dst,
			ref SDL_Rect dstrect
		);
		
		/* src and dst refer to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpperBlitScaled(
			IntPtr src,
			ref SDL_Rect srcrect,
			IntPtr dst,
			ref SDL_Rect dstrect
		);
		
		#endregion
		
		#region SDL_clipboard.h
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_HasClipboardText();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetClipboardText();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetClipboardText(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string text
		);
		
		#endregion

		#region SDL_events.h

		/* General keyboard/mouse state definitions. */
		public const byte SDL_PRESSED =		0;
		public const byte SDL_RELEASED =	1;

		/* Default size is according to SDL2 default. */
		public const int SDL_TEXTEDITINGEVENT_TEXT_SIZE = 32;
		public const int SDL_TEXTINPUTEVENT_TEXT_SIZE = 32;

		/* The types of events that can be delivered. */
		public enum SDL_EventType : uint
		{
			SDL_FIRSTEVENT =		0,

			/* Application events */
			SDL_QUIT = 			0x100,

			/* Window events */
			SDL_WINDOWEVENT = 		0x200,
			SDL_SYSWMEVENT,
			
			/* Keyboard events */
			SDL_KEYDOWN = 			0x300,
			SDL_KEYUP,
			SDL_TEXTEDITING,
			SDL_TEXTINPUT,

			/* Mouse events */
			SDL_MOUSEMOTION = 		0x400,
			SDL_MOUSEBUTTONDOWN,
			SDL_MOUSEBUTTONUP,
			SDL_MOUSEWHEEL,

			/* Joystick events */
			SDL_JOYAXISMOTION =		0x600,
			SDL_JOYBALLMOTION,
			SDL_JOYHATMOTION,
			SDL_JOYBUTTONDOWN,
			SDL_JOYBUTTONUP,
			SDL_JOYDEVICEADDED,
			SDL_JOYDEVICEREMOVED,

			/* Game controller events */
			SDL_CONTROLLERAXISMOTION = 	0x650,
			SDL_CONTROLLERBUTTONDOWN,
			SDL_CONTROLLERBUTTONUP,
			SDL_CONTROLLERDEVICEADDED,
			SDL_CONTROLLERDEVICEREMOVED,
			SDL_CONTROLLERDEVICEREMAPPED,

			/* Touch events */
			SDL_FINGERDOWN = 		0x700,
			SDL_FINGERUP,
			SDL_FINGERMOTION,

			/* Gesture events */
			SDL_DOLLARGESTURE =		0x800,
			SDL_DOLLARRECORD,
			SDL_MULTIGESTURE,

			/* Clipboard events */
			SDL_CLIPBOARDUPDATE =		0x900,

			/* Drag and drop events */
			SDL_DROPFILE =			0x1000,

			/* Events SDL_USEREVENT through SDL_LASTEVENT are for
			 * your use, and should be allocated with
			 * SDL_RegisterEvents()
			 */
			SDL_USEREVENT =			0x8000,

			/* The last event, used for bouding arrays. */
			SDL_LASTEVENT =			0xFFFF
		}

		/* Fields shared by every event */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GenericEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
		}

		/* Window state change event data (event.window.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_WindowEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public SDL_WindowEventID windowEvent; // event, lolC#
			byte padding1;
			byte padding2;
			byte padding3;
			public Int32 data1;
			public Int32 data2;
		}
		
		/* Keyboard button event structure (event.key.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_KeyboardEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public byte state;
			public byte repeat; /* non-zero if this is a repeat */
			byte padding2;
			byte padding3;
			public SDL_Keysym keysym;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SDL_TextEditingEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public fixed char text[SDL_TEXTEDITINGEVENT_TEXT_SIZE];
			public Int32 start;
			public Int32 length;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SDL_TextInputEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public fixed char text[SDL_TEXTINPUTEVENT_TEXT_SIZE];
		}

		/* Mouse motion event structure (event.motion.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_MouseMotionEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public UInt32 which;
			public byte state; /* bitmask of buttons */
			byte padding1;
			byte padding2;
			byte padding3;
			public Int32 x;
			public Int32 y;
			public Int32 xrel;
			public Int32 yrel;
		}

		/* Mouse button event structure (event.button.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_MouseButtonEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public UInt32 which;
			public byte button; /* button id */
			public byte state; /* SDL_PRESSED or SDL_RELEASED */
			byte padding1;
			byte padding2;
			public Int32 x;
			public Int32 y;
		}

		/* Mouse wheel event structure (event.wheel.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_MouseWheelEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public UInt32 which;
			public Int32 x; /* amount scrolled horizontally */
			public Int32 y; /* amount scrolled vertically */
		}

		/* Joystick axis motion event structure (event.jaxis.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyAxisEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte axis;
			byte padding1;
			byte padding2;
			byte padding3;
			public Int16 axisValue; /* value, lolC# */
			public UInt16 padding4;
		}

		/* Joystick trackball motion event structure (event.jball.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyBallEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte ball;
			byte padding1;
			byte padding2;
			byte padding3;
			public Int16 xrel;
			public Int16 yrel;
		}

		/* Joystick hat position change event struct (event.jhat.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyHatEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte hat; /* index of the hat */
			public byte hatValue; /* value, lolC# */
			byte padding1;
			byte padding2;
		}

		/* Joystick button event structure (event.jbutton.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyButtonEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte button;
			public byte state; /* SDL_PRESSED or SDL_RELEASED */
			byte padding1;
			byte padding2;
		}

		/* Joystick device event structure (event.jdevice.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyDeviceEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
		}

		/* Game controller axis motion event (event.caxis.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_ControllerAxisEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte axis;
			byte padding1;
			byte padding2;
			byte padding3;
			public Int16 axisValue; /* value, lolC# */
			UInt16 padding4;
		}

		/* Game controller button event (event.cbutton.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_ControllerButtonEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte button;
			public byte state;
			byte padding1;
			byte padding2;
		}

		/* Game controller device event (event.cdevice.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_ControllerDeviceEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* joystick id for ADDED, else
					       instance id */
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_TouchFingerEvent
		{
			public UInt32 type;
			public UInt32 timestamp;
			public Int64 touchId; // SDL_TouchID
			public Int64 fingerId; // SDL_GestureID
			public float x;
			public float y;
			public float dx;
			public float dy;
			public float pressure;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_MultiGestureEvent
		{
			public UInt32 type;
			public UInt32 timestamp;
			public Int64 touchId; // SDL_TouchID
			public float dTheta;
			public float dDist;
			public float x;
			public float y;
			public UInt16 numFingers;
			public UInt16 padding;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_DollarGestureEvent
		{
			public UInt32 type;
			public UInt32 timestamp;
			public Int64 touchId; // SDL_TouchID
			public Int64 gestureId; // SDL_GestureID
			public UInt32 numFingers;
			public float error;
			public float x;
			public float y;
		}

		/* File open request by system (event.drop.*), disabled by
		 * default
		 */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_DropEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public IntPtr file; /* char* filename, to be freed */
		}

		/* The "quit requested" event */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_QuitEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
		}

		/* A user defined event (event.user.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_UserEvent
		{
			public UInt32 type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public Int32 code;
			public IntPtr data1; /* user-defined */
			public IntPtr data2; /* user-defined */
		}

		/* A video driver dependent event (event.syswm.*), disabled */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_SysWMEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public IntPtr msg; /* SDL_SysWMmsg*, system-dependent*/
		}

		/* General event structure */
		// C# doesn't do unions, so we do this ugly thing. */
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_Event
		{
			[FieldOffset(0)]
			public SDL_EventType type;
			[FieldOffset(0)]
			public SDL_WindowEvent window;
			[FieldOffset(0)]
			public SDL_KeyboardEvent key;
			[FieldOffset(0)]
			public SDL_TextEditingEvent edit;
			[FieldOffset(0)]
			public SDL_TextInputEvent text;
			[FieldOffset(0)]
			public SDL_MouseMotionEvent motion;
			[FieldOffset(0)]
			public SDL_MouseButtonEvent button;
			[FieldOffset(0)]
			public SDL_MouseWheelEvent wheel;
			[FieldOffset(0)]
			public SDL_JoyAxisEvent jaxis;
			[FieldOffset(0)]
			public SDL_JoyBallEvent jball;
			[FieldOffset(0)]
			public SDL_JoyHatEvent jhat;
			[FieldOffset(0)]
			public SDL_JoyButtonEvent jbutton;
			[FieldOffset(0)]
			public SDL_JoyDeviceEvent jdevice;
			[FieldOffset(0)]
			public SDL_ControllerAxisEvent caxis;
			[FieldOffset(0)]
			public SDL_ControllerButtonEvent cbutton;
			[FieldOffset(0)]
			public SDL_ControllerDeviceEvent cdevice;
			[FieldOffset(0)]
			public SDL_QuitEvent quit;
			[FieldOffset(0)]
			public SDL_UserEvent user;
			[FieldOffset(0)]
			public SDL_SysWMEvent syswm;
			[FieldOffset(0)]
			public SDL_TouchFingerEvent tfinger;
			[FieldOffset(0)]
			public SDL_MultiGestureEvent mgesture;
			[FieldOffset(0)]
			public SDL_DollarGestureEvent dgesture;
			[FieldOffset(0)]
			public SDL_DropEvent drop;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SDL_EventFilter(
			IntPtr userdata, // void*
			IntPtr sdlevent // SDL_Event* event, lolC#
		);

		/* Pump the event loop, getting events from the input devices*/
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PumpEvents();

		public enum SDL_eventaction
		{
			SDL_ADDEVENT,
			SDL_PEEKEVENT,
			SDL_GETEVENT
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PeepEvents(
			SDL_Event[] events,
			int numevents,
			SDL_eventaction action,
			SDL_EventType minType,
			SDL_EventType maxType
		);

		/* Checks to see if certain events are in the event queue */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_HasEvent(SDL_EventType type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_HasEvents(
			SDL_EventType minType,
			SDL_EventType maxType
		);

		/* Clears events from the event queue */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvent(SDL_EventType type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvents(
			SDL_EventType min,
			SDL_EventType max
		);

		/* Polls for currently pending events */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PollEvent(out SDL_Event _event);

		/* Waits indefinitely for the next event */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_WaitEvent(out SDL_Event _event);

		/* Waits until the specified timeout (in ms) for the next event
		 */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_WaitEventTimeout(out SDL_Event _event, int timeout);
		
		/* Add an event to the event queue */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PushEvent(ref SDL_Event _event);

		/* userdata refers to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetEventFilter(
			SDL_EventFilter filter,
			IntPtr userdata
		);

		/* userdata refers to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_GetEventFilter(
			ref SDL_EventFilter filter,
			ref IntPtr userdata
		);

		/* userdata refers to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_AddEventWatch(
			SDL_EventFilter filter,
			IntPtr userdata
		);

		/* userdata refers to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DelEventWatch(
			SDL_EventFilter filter,
			IntPtr userdata
		);

		/* userdata refers to a void* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FilterEvents(
			SDL_EventFilter filter,
			IntPtr userdata
		);

		/* These are for SDL_EventState() */
		public const int SDL_QUERY = 		-1;
		public const int SDL_IGNORE = 		0;
		public const int SDL_DISABLE =		0;
		public const int SDL_ENABLE = 		1;

		/* This function allows you to enable/disable certain events */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_EventState(SDL_EventType type, int state);

		/* Get the state of an event */
		public static byte SDL_GetEventState(SDL_EventType type)
		{
			return SDL_EventState(type, SDL_QUERY);
		}

		/* Allocate a set of user-defined events */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 SDL_RegisterEvents(int numevents);
		#endregion
		
		#region SDL_scancode.h

		/* Scancodes based off USB keyboard page (0x07) */
		public enum SDL_Scancode
		{
			SDL_SCANCODE_UNKNOWN = 0,
			
			SDL_SCANCODE_A = 4,
			SDL_SCANCODE_B = 5,
			SDL_SCANCODE_C = 6,
			SDL_SCANCODE_D = 7,
			SDL_SCANCODE_E = 8,
			SDL_SCANCODE_F = 9,
			SDL_SCANCODE_G = 10,
			SDL_SCANCODE_H = 11,
			SDL_SCANCODE_I = 12,
			SDL_SCANCODE_J = 13,
			SDL_SCANCODE_K = 14,
			SDL_SCANCODE_L = 15,
			SDL_SCANCODE_M = 16,
			SDL_SCANCODE_N = 17,
			SDL_SCANCODE_O = 18,
			SDL_SCANCODE_P = 19,
			SDL_SCANCODE_Q = 20,
			SDL_SCANCODE_R = 21,
			SDL_SCANCODE_S = 22,
			SDL_SCANCODE_T = 23,
			SDL_SCANCODE_U = 24,
			SDL_SCANCODE_V = 25,
			SDL_SCANCODE_W = 26,
			SDL_SCANCODE_X = 27,
			SDL_SCANCODE_Y = 28,
			SDL_SCANCODE_Z = 29,

			SDL_SCANCODE_1 = 30,
			SDL_SCANCODE_2 = 31,
			SDL_SCANCODE_3 = 32,
			SDL_SCANCODE_4 = 33,
			SDL_SCANCODE_5 = 34,
			SDL_SCANCODE_6 = 35,
			SDL_SCANCODE_7 = 36,
			SDL_SCANCODE_8 = 37,
			SDL_SCANCODE_9 = 38,
			SDL_SCANCODE_0 = 39,

			SDL_SCANCODE_RETURN = 40,
			SDL_SCANCODE_ESCAPE = 41,
			SDL_SCANCODE_BACKSPACE = 42,
			SDL_SCANCODE_TAB = 43,
			SDL_SCANCODE_SPACE = 44,

			SDL_SCANCODE_MINUS = 45,
			SDL_SCANCODE_EQUALS = 46,
			SDL_SCANCODE_LEFTBRACKET = 47,
			SDL_SCANCODE_RIGHTBRACKET = 48,
			SDL_SCANCODE_BACKSLASH = 49,
			SDL_SCANCODE_NONUSHASH = 50,
			SDL_SCANCODE_SEMICOLON = 51,
			SDL_SCANCODE_APOSTROPHE = 52,
			SDL_SCANCODE_GRAVE = 53,
			SDL_SCANCODE_COMMA = 54,
			SDL_SCANCODE_PERIOD = 55,
			SDL_SCANCODE_SLASH = 56,

			SDL_SCANCODE_CAPSLOCK = 57,

			SDL_SCANCODE_F1 = 58,
			SDL_SCANCODE_F2 = 59,
			SDL_SCANCODE_F3 = 60,
			SDL_SCANCODE_F4 = 61,
			SDL_SCANCODE_F5 = 62,
			SDL_SCANCODE_F6 = 63,
			SDL_SCANCODE_F7 = 64,
			SDL_SCANCODE_F8 = 65,
			SDL_SCANCODE_F9 = 66,
			SDL_SCANCODE_F10 = 67,
			SDL_SCANCODE_F11 = 68,
			SDL_SCANCODE_F12 = 69,

			SDL_SCANCODE_PRINTSCREEN = 70,
			SDL_SCANCODE_SCROLLLOCK = 71,
			SDL_SCANCODE_PAUSE = 72,
			SDL_SCANCODE_INSERT = 73,
			SDL_SCANCODE_HOME = 74,
			SDL_SCANCODE_PAGEUP = 75,
			SDL_SCANCODE_DELETE = 76,
			SDL_SCANCODE_END = 77,
			SDL_SCANCODE_PAGEDOWN = 78,
			SDL_SCANCODE_RIGHT = 79,
			SDL_SCANCODE_LEFT = 80,
			SDL_SCANCODE_DOWN = 81,
			SDL_SCANCODE_UP = 82,

			SDL_SCANCODE_NUMLOCKCLEAR = 83,
			SDL_SCANCODE_KP_DIVIDE = 84,
			SDL_SCANCODE_KP_MULTIPLY = 85,
			SDL_SCANCODE_KP_MINUS = 86,
			SDL_SCANCODE_KP_PLUS = 87,
			SDL_SCANCODE_KP_ENTER = 88,
			SDL_SCANCODE_KP_1 = 89,
			SDL_SCANCODE_KP_2 = 90,
			SDL_SCANCODE_KP_3 = 91,
			SDL_SCANCODE_KP_4 = 92,
			SDL_SCANCODE_KP_5 = 93,
			SDL_SCANCODE_KP_6 = 94,
			SDL_SCANCODE_KP_7 = 95,
			SDL_SCANCODE_KP_8 = 96,
			SDL_SCANCODE_KP_9 = 97,
			SDL_SCANCODE_KP_0 = 98,
			SDL_SCANCODE_KP_PERIOD = 99,

			SDL_SCANCODE_NONUSBACKSLASH = 100,
			SDL_SCANCODE_APPLICATION = 101,
			SDL_SCANCODE_POWER = 102,
			SDL_SCANCODE_KP_EQUALS = 103,
			SDL_SCANCODE_F13 = 104,
			SDL_SCANCODE_F14 = 105,
			SDL_SCANCODE_F15 = 106,
			SDL_SCANCODE_F16 = 107,
			SDL_SCANCODE_F17 = 108,
			SDL_SCANCODE_F18 = 109,
			SDL_SCANCODE_F19 = 110,
			SDL_SCANCODE_F20 = 111,
			SDL_SCANCODE_F21 = 112,
			SDL_SCANCODE_F22 = 113,
			SDL_SCANCODE_F23 = 114,
			SDL_SCANCODE_F24 = 115,
			SDL_SCANCODE_EXECUTE = 116,
			SDL_SCANCODE_HELP = 117,
			SDL_SCANCODE_MENU = 118,
			SDL_SCANCODE_SELECT = 119,
			SDL_SCANCODE_STOP = 120,
			SDL_SCANCODE_AGAIN = 121,
			SDL_SCANCODE_UNDO = 122,
			SDL_SCANCODE_CUT = 123,
			SDL_SCANCODE_COPY = 124,
			SDL_SCANCODE_PASTE = 125,
			SDL_SCANCODE_FIND = 126,
			SDL_SCANCODE_MUTE = 127,
			SDL_SCANCODE_VOLUMEUP = 128,
			SDL_SCANCODE_VOLUMEDOWN = 129,
			/* not sure whether there's a reason to enable these */
			/*     SDL_SCANCODE_LOCKINGCAPSLOCK = 130,  */
			/*     SDL_SCANCODE_LOCKINGNUMLOCK = 131, */
			/*     SDL_SCANCODE_LOCKINGSCROLLLOCK = 132, */
			SDL_SCANCODE_KP_COMMA = 133,
			SDL_SCANCODE_KP_EQUALSAS400 = 134,

			SDL_SCANCODE_INTERNATIONAL1 = 135,
			SDL_SCANCODE_INTERNATIONAL2 = 136,
			SDL_SCANCODE_INTERNATIONAL3 = 137,
			SDL_SCANCODE_INTERNATIONAL4 = 138,
			SDL_SCANCODE_INTERNATIONAL5 = 139,
			SDL_SCANCODE_INTERNATIONAL6 = 140,
			SDL_SCANCODE_INTERNATIONAL7 = 141,
			SDL_SCANCODE_INTERNATIONAL8 = 142,
			SDL_SCANCODE_INTERNATIONAL9 = 143,
			SDL_SCANCODE_LANG1 = 144,
			SDL_SCANCODE_LANG2 = 145,
			SDL_SCANCODE_LANG3 = 146,
			SDL_SCANCODE_LANG4 = 147,
			SDL_SCANCODE_LANG5 = 148,
			SDL_SCANCODE_LANG6 = 149,
			SDL_SCANCODE_LANG7 = 150,
			SDL_SCANCODE_LANG8 = 151,
			SDL_SCANCODE_LANG9 = 152,

			SDL_SCANCODE_ALTERASE = 153,
			SDL_SCANCODE_SYSREQ = 154,
			SDL_SCANCODE_CANCEL = 155,
			SDL_SCANCODE_CLEAR = 156,
			SDL_SCANCODE_PRIOR = 157,
			SDL_SCANCODE_RETURN2 = 158,
			SDL_SCANCODE_SEPARATOR = 159,
			SDL_SCANCODE_OUT = 160,
			SDL_SCANCODE_OPER = 161,
			SDL_SCANCODE_CLEARAGAIN = 162,
			SDL_SCANCODE_CRSEL = 163,
			SDL_SCANCODE_EXSEL = 164,

			SDL_SCANCODE_KP_00 = 176,
			SDL_SCANCODE_KP_000 = 177,
			SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
			SDL_SCANCODE_DECIMALSEPARATOR = 179,
			SDL_SCANCODE_CURRENCYUNIT = 180,
			SDL_SCANCODE_CURRENCYSUBUNIT = 181,
			SDL_SCANCODE_KP_LEFTPAREN = 182,
			SDL_SCANCODE_KP_RIGHTPAREN = 183,
			SDL_SCANCODE_KP_LEFTBRACE = 184,
			SDL_SCANCODE_KP_RIGHTBRACE = 185,
			SDL_SCANCODE_KP_TAB = 186,
			SDL_SCANCODE_KP_BACKSPACE = 187,
			SDL_SCANCODE_KP_A = 188,
			SDL_SCANCODE_KP_B = 189,
			SDL_SCANCODE_KP_C = 190,
			SDL_SCANCODE_KP_D = 191,
			SDL_SCANCODE_KP_E = 192,
			SDL_SCANCODE_KP_F = 193,
			SDL_SCANCODE_KP_XOR = 194,
			SDL_SCANCODE_KP_POWER = 195,
			SDL_SCANCODE_KP_PERCENT = 196,
			SDL_SCANCODE_KP_LESS = 197,
			SDL_SCANCODE_KP_GREATER = 198,
			SDL_SCANCODE_KP_AMPERSAND = 199,
			SDL_SCANCODE_KP_DBLAMPERSAND = 200,
			SDL_SCANCODE_KP_VERTICALBAR = 201,
			SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
			SDL_SCANCODE_KP_COLON = 203,
			SDL_SCANCODE_KP_HASH = 204,
			SDL_SCANCODE_KP_SPACE = 205,
			SDL_SCANCODE_KP_AT = 206,
			SDL_SCANCODE_KP_EXCLAM = 207,
			SDL_SCANCODE_KP_MEMSTORE = 208,
			SDL_SCANCODE_KP_MEMRECALL = 209,
			SDL_SCANCODE_KP_MEMCLEAR = 210,
			SDL_SCANCODE_KP_MEMADD = 211,
			SDL_SCANCODE_KP_MEMSUBTRACT = 212,
			SDL_SCANCODE_KP_MEMMULTIPLY = 213,
			SDL_SCANCODE_KP_MEMDIVIDE = 214,
			SDL_SCANCODE_KP_PLUSMINUS = 215,
			SDL_SCANCODE_KP_CLEAR = 216,
			SDL_SCANCODE_KP_CLEARENTRY = 217,
			SDL_SCANCODE_KP_BINARY = 218,
			SDL_SCANCODE_KP_OCTAL = 219,
			SDL_SCANCODE_KP_DECIMAL = 220,
			SDL_SCANCODE_KP_HEXADECIMAL = 221,

			SDL_SCANCODE_LCTRL = 224,
			SDL_SCANCODE_LSHIFT = 225,
			SDL_SCANCODE_LALT = 226,
			SDL_SCANCODE_LGUI = 227,
			SDL_SCANCODE_RCTRL = 228,
			SDL_SCANCODE_RSHIFT = 229,
			SDL_SCANCODE_RALT = 230,
			SDL_SCANCODE_RGUI = 231,

			SDL_SCANCODE_MODE = 257,
			
			/* These come from the USB consumer page (0x0C) */	
			SDL_SCANCODE_AUDIONEXT = 258,
			SDL_SCANCODE_AUDIOPREV = 259,
			SDL_SCANCODE_AUDIOSTOP = 260,
			SDL_SCANCODE_AUDIOPLAY = 261,
			SDL_SCANCODE_AUDIOMUTE = 262,
			SDL_SCANCODE_MEDIASELECT = 263,
			SDL_SCANCODE_WWW = 264,
			SDL_SCANCODE_MAIL = 265,
			SDL_SCANCODE_CALCULATOR = 266,
			SDL_SCANCODE_COMPUTER = 267,
			SDL_SCANCODE_AC_SEARCH = 268,
			SDL_SCANCODE_AC_HOME = 269,
			SDL_SCANCODE_AC_BACK = 270,
			SDL_SCANCODE_AC_FORWARD = 271,
			SDL_SCANCODE_AC_STOP = 272,
			SDL_SCANCODE_AC_REFRESH = 273,
			SDL_SCANCODE_AC_BOOKMARKS = 274,

			/* These come from other sources, and are mostly mac related */
			SDL_SCANCODE_BRIGHTNESSDOWN = 275,
			SDL_SCANCODE_BRIGHTNESSUP = 276,
			SDL_SCANCODE_DISPLAYSWITCH = 277,
			SDL_SCANCODE_KBDILLUMTOGGLE = 278,
			SDL_SCANCODE_KBDILLUMDOWN = 279,
			SDL_SCANCODE_KBDILLUMUP = 280,
			SDL_SCANCODE_EJECT = 281,
			SDL_SCANCODE_SLEEP = 282,

			SDL_SCANCODE_APP1 = 283,
			SDL_SCANCODE_APP2 = 284,

			/* This is not a key, simply marks the number of scancodes
			 * so that you know how big to make your arrays. */
			SDL_NUM_SCANCODES = 512
		}

		#endregion
		#region SDL_keycode.h

		public const int SDLK_SCANCODE_MASK = (1 << 30);
		public static SDL_Keycode SDL_SCANCODE_TO_KEYCODE(SDL_Scancode X)
		{
			return (SDL_Keycode)((int)X | SDLK_SCANCODE_MASK);
		}
		
		/* So, in the C headers, SDL_Keycode is a typedef of Sint32
		 * and all of the names are in an anonymous enum. Yeah...
		 * that's not going to cut it for C#. We'll just put them in an
		 * enum for now? */
		public enum SDL_Keycode
		{
			SDLK_UNKNOWN = 0,

			SDLK_RETURN = '\r',
			SDLK_ESCAPE = 27, // '\033'
			SDLK_BACKSPACE = '\b',
			SDLK_TAB = '\t',
			SDLK_SPACE = ' ',
			SDLK_EXCLAIM = '!',
			SDLK_QUOTEDBL = '"',
			SDLK_HASH = '#',
			SDLK_PERCENT = '%',
			SDLK_DOLLAR = '$',
			SDLK_AMPERSAND = '&',
			SDLK_QUOTE = '\'',
			SDLK_LEFTPAREN = '(',
			SDLK_RIGHTPAREN = ')',
			SDLK_ASTERISK = '*',
			SDLK_PLUS = '+',
			SDLK_COMMA = ',',
			SDLK_MINUS = '-',
			SDLK_PERIOD = '.',
			SDLK_SLASH = '/',
			SDLK_0 = '0',
			SDLK_1 = '1',
			SDLK_2 = '2',
			SDLK_3 = '3',
			SDLK_4 = '4',
			SDLK_5 = '5',
			SDLK_6 = '6',
			SDLK_7 = '7',
			SDLK_8 = '8',
			SDLK_9 = '9',
			SDLK_COLON = ':',
			SDLK_SEMICOLON = ';',
			SDLK_LESS = '<',
			SDLK_EQUALS = '=',
			SDLK_GREATER = '>',
			SDLK_QUESTION = '?',
			SDLK_AT = '@',
			/* 
			Skip uppercase letters
			*/
			SDLK_LEFTBRACKET = '[',
			SDLK_BACKSLASH = '\\',
			SDLK_RIGHTBRACKET = ']',
			SDLK_CARET = '^',
			SDLK_UNDERSCORE = '_',
			SDLK_BACKQUOTE = '`',
			SDLK_a = 'a',
			SDLK_b = 'b',
			SDLK_c = 'c',
			SDLK_d = 'd',
			SDLK_e = 'e',
			SDLK_f = 'f',
			SDLK_g = 'g',
			SDLK_h = 'h',
			SDLK_i = 'i',
			SDLK_j = 'j',
			SDLK_k = 'k',
			SDLK_l = 'l',
			SDLK_m = 'm',
			SDLK_n = 'n',
			SDLK_o = 'o',
			SDLK_p = 'p',
			SDLK_q = 'q',
			SDLK_r = 'r',
			SDLK_s = 's',
			SDLK_t = 't',
			SDLK_u = 'u',
			SDLK_v = 'v',
			SDLK_w = 'w',
			SDLK_x = 'x',
			SDLK_y = 'y',
			SDLK_z = 'z',

			SDLK_CAPSLOCK = (int)SDL_Scancode.SDL_SCANCODE_CAPSLOCK | SDLK_SCANCODE_MASK,

			SDLK_F1 = (int)SDL_Scancode.SDL_SCANCODE_F1 | SDLK_SCANCODE_MASK,
			SDLK_F2 = (int)SDL_Scancode.SDL_SCANCODE_F2 | SDLK_SCANCODE_MASK,
			SDLK_F3 = (int)SDL_Scancode.SDL_SCANCODE_F3 | SDLK_SCANCODE_MASK,
			SDLK_F4 = (int)SDL_Scancode.SDL_SCANCODE_F4 | SDLK_SCANCODE_MASK,
			SDLK_F5 = (int)SDL_Scancode.SDL_SCANCODE_F5 | SDLK_SCANCODE_MASK,
			SDLK_F6 = (int)SDL_Scancode.SDL_SCANCODE_F6 | SDLK_SCANCODE_MASK,
			SDLK_F7 = (int)SDL_Scancode.SDL_SCANCODE_F7 | SDLK_SCANCODE_MASK,
			SDLK_F8 = (int)SDL_Scancode.SDL_SCANCODE_F8 | SDLK_SCANCODE_MASK,
			SDLK_F9 = (int)SDL_Scancode.SDL_SCANCODE_F9 | SDLK_SCANCODE_MASK,
			SDLK_F10 = (int)SDL_Scancode.SDL_SCANCODE_F10 | SDLK_SCANCODE_MASK,
			SDLK_F11 = (int)SDL_Scancode.SDL_SCANCODE_F11 | SDLK_SCANCODE_MASK,
			SDLK_F12 = (int)SDL_Scancode.SDL_SCANCODE_F12 | SDLK_SCANCODE_MASK,

			SDLK_PRINTSCREEN = (int)SDL_Scancode.SDL_SCANCODE_PRINTSCREEN | SDLK_SCANCODE_MASK,
			SDLK_SCROLLLOCK = (int)SDL_Scancode.SDL_SCANCODE_SCROLLLOCK | SDLK_SCANCODE_MASK,
			SDLK_PAUSE = (int)SDL_Scancode.SDL_SCANCODE_PAUSE | SDLK_SCANCODE_MASK,
			SDLK_INSERT = (int)SDL_Scancode.SDL_SCANCODE_INSERT | SDLK_SCANCODE_MASK,
			SDLK_HOME = (int)SDL_Scancode.SDL_SCANCODE_HOME | SDLK_SCANCODE_MASK,
			SDLK_PAGEUP = (int)SDL_Scancode.SDL_SCANCODE_PAGEUP | SDLK_SCANCODE_MASK,
			SDLK_DELETE = 177,
			SDLK_END = (int)SDL_Scancode.SDL_SCANCODE_END | SDLK_SCANCODE_MASK,
			SDLK_PAGEDOWN = (int)SDL_Scancode.SDL_SCANCODE_PAGEDOWN | SDLK_SCANCODE_MASK,
			SDLK_RIGHT = (int)SDL_Scancode.SDL_SCANCODE_RIGHT | SDLK_SCANCODE_MASK,
			SDLK_LEFT = (int)SDL_Scancode.SDL_SCANCODE_LEFT | SDLK_SCANCODE_MASK,
			SDLK_DOWN = (int)SDL_Scancode.SDL_SCANCODE_DOWN | SDLK_SCANCODE_MASK,
			SDLK_UP = (int)SDL_Scancode.SDL_SCANCODE_UP | SDLK_SCANCODE_MASK,

			SDLK_NUMLOCKCLEAR = (int)SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR | SDLK_SCANCODE_MASK,
			SDLK_KP_DIVIDE = (int)SDL_Scancode.SDL_SCANCODE_KP_DIVIDE | SDLK_SCANCODE_MASK,
			SDLK_KP_MULTIPLY = (int)SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY | SDLK_SCANCODE_MASK,
			SDLK_KP_MINUS = (int)SDL_Scancode.SDL_SCANCODE_KP_MINUS | SDLK_SCANCODE_MASK,
			SDLK_KP_PLUS = (int)SDL_Scancode.SDL_SCANCODE_KP_PLUS | SDLK_SCANCODE_MASK,
			SDLK_KP_ENTER = (int)SDL_Scancode.SDL_SCANCODE_KP_ENTER | SDLK_SCANCODE_MASK,
			SDLK_KP_1 = (int)SDL_Scancode.SDL_SCANCODE_KP_1 | SDLK_SCANCODE_MASK,
			SDLK_KP_2 = (int)SDL_Scancode.SDL_SCANCODE_KP_2 | SDLK_SCANCODE_MASK,
			SDLK_KP_3 = (int)SDL_Scancode.SDL_SCANCODE_KP_3 | SDLK_SCANCODE_MASK,
			SDLK_KP_4 = (int)SDL_Scancode.SDL_SCANCODE_KP_4 | SDLK_SCANCODE_MASK,
			SDLK_KP_5 = (int)SDL_Scancode.SDL_SCANCODE_KP_5 | SDLK_SCANCODE_MASK,
			SDLK_KP_6 = (int)SDL_Scancode.SDL_SCANCODE_KP_6 | SDLK_SCANCODE_MASK,
			SDLK_KP_7 = (int)SDL_Scancode.SDL_SCANCODE_KP_7 | SDLK_SCANCODE_MASK,
			SDLK_KP_8 = (int)SDL_Scancode.SDL_SCANCODE_KP_8 | SDLK_SCANCODE_MASK,
			SDLK_KP_9 = (int)SDL_Scancode.SDL_SCANCODE_KP_9 | SDLK_SCANCODE_MASK,
			SDLK_KP_0 = (int)SDL_Scancode.SDL_SCANCODE_KP_0 | SDLK_SCANCODE_MASK,
			SDLK_KP_PERIOD = (int)SDL_Scancode.SDL_SCANCODE_KP_PERIOD | SDLK_SCANCODE_MASK,

			SDLK_APPLICATION = (int)SDL_Scancode.SDL_SCANCODE_APPLICATION | SDLK_SCANCODE_MASK,
			SDLK_POWER = (int)SDL_Scancode.SDL_SCANCODE_POWER | SDLK_SCANCODE_MASK,
			SDLK_KP_EQUALS = (int)SDL_Scancode.SDL_SCANCODE_KP_EQUALS | SDLK_SCANCODE_MASK,
			SDLK_F13 = (int)SDL_Scancode.SDL_SCANCODE_F13 | SDLK_SCANCODE_MASK,
			SDLK_F14 = (int)SDL_Scancode.SDL_SCANCODE_F14 | SDLK_SCANCODE_MASK,
			SDLK_F15 = (int)SDL_Scancode.SDL_SCANCODE_F15 | SDLK_SCANCODE_MASK,
			SDLK_F16 = (int)SDL_Scancode.SDL_SCANCODE_F16 | SDLK_SCANCODE_MASK,
			SDLK_F17 = (int)SDL_Scancode.SDL_SCANCODE_F17 | SDLK_SCANCODE_MASK,
			SDLK_F18 = (int)SDL_Scancode.SDL_SCANCODE_F18 | SDLK_SCANCODE_MASK,
			SDLK_F19 = (int)SDL_Scancode.SDL_SCANCODE_F19 | SDLK_SCANCODE_MASK,
			SDLK_F20 = (int)SDL_Scancode.SDL_SCANCODE_F20 | SDLK_SCANCODE_MASK,
			SDLK_F21 = (int)SDL_Scancode.SDL_SCANCODE_F21 | SDLK_SCANCODE_MASK,
			SDLK_F22 = (int)SDL_Scancode.SDL_SCANCODE_F22 | SDLK_SCANCODE_MASK,
			SDLK_F23 = (int)SDL_Scancode.SDL_SCANCODE_F23 | SDLK_SCANCODE_MASK,
			SDLK_F24 = (int)SDL_Scancode.SDL_SCANCODE_F24 | SDLK_SCANCODE_MASK,
			SDLK_EXECUTE = (int)SDL_Scancode.SDL_SCANCODE_EXECUTE | SDLK_SCANCODE_MASK,
			SDLK_HELP = (int)SDL_Scancode.SDL_SCANCODE_HELP | SDLK_SCANCODE_MASK,
			SDLK_MENU = (int)SDL_Scancode.SDL_SCANCODE_MENU | SDLK_SCANCODE_MASK,
			SDLK_SELECT = (int)SDL_Scancode.SDL_SCANCODE_SELECT | SDLK_SCANCODE_MASK,
			SDLK_STOP = (int)SDL_Scancode.SDL_SCANCODE_STOP | SDLK_SCANCODE_MASK,
			SDLK_AGAIN = (int)SDL_Scancode.SDL_SCANCODE_AGAIN | SDLK_SCANCODE_MASK,
			SDLK_UNDO = (int)SDL_Scancode.SDL_SCANCODE_UNDO | SDLK_SCANCODE_MASK,
			SDLK_CUT = (int)SDL_Scancode.SDL_SCANCODE_CUT | SDLK_SCANCODE_MASK,
			SDLK_COPY = (int)SDL_Scancode.SDL_SCANCODE_COPY | SDLK_SCANCODE_MASK,
			SDLK_PASTE = (int)SDL_Scancode.SDL_SCANCODE_PASTE | SDLK_SCANCODE_MASK,
			SDLK_FIND = (int)SDL_Scancode.SDL_SCANCODE_FIND | SDLK_SCANCODE_MASK,
			SDLK_MUTE = (int)SDL_Scancode.SDL_SCANCODE_MUTE | SDLK_SCANCODE_MASK,
			SDLK_VOLUMEUP = (int)SDL_Scancode.SDL_SCANCODE_VOLUMEUP | SDLK_SCANCODE_MASK,
			SDLK_VOLUMEDOWN = (int)SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN | SDLK_SCANCODE_MASK,
			SDLK_KP_COMMA = (int)SDL_Scancode.SDL_SCANCODE_KP_COMMA | SDLK_SCANCODE_MASK,
			SDLK_KP_EQUALSAS400 =
			(int)SDL_Scancode.SDL_SCANCODE_KP_EQUALSAS400 | SDLK_SCANCODE_MASK,

			SDLK_ALTERASE = (int)SDL_Scancode.SDL_SCANCODE_ALTERASE | SDLK_SCANCODE_MASK,
			SDLK_SYSREQ = (int)SDL_Scancode.SDL_SCANCODE_SYSREQ | SDLK_SCANCODE_MASK,
			SDLK_CANCEL = (int)SDL_Scancode.SDL_SCANCODE_CANCEL | SDLK_SCANCODE_MASK,
			SDLK_CLEAR = (int)SDL_Scancode.SDL_SCANCODE_CLEAR | SDLK_SCANCODE_MASK,
			SDLK_PRIOR = (int)SDL_Scancode.SDL_SCANCODE_PRIOR | SDLK_SCANCODE_MASK,
			SDLK_RETURN2 = (int)SDL_Scancode.SDL_SCANCODE_RETURN2 | SDLK_SCANCODE_MASK,
			SDLK_SEPARATOR = (int)SDL_Scancode.SDL_SCANCODE_SEPARATOR | SDLK_SCANCODE_MASK,
			SDLK_OUT = (int)SDL_Scancode.SDL_SCANCODE_OUT | SDLK_SCANCODE_MASK,
			SDLK_OPER = (int)SDL_Scancode.SDL_SCANCODE_OPER | SDLK_SCANCODE_MASK,
			SDLK_CLEARAGAIN = (int)SDL_Scancode.SDL_SCANCODE_CLEARAGAIN | SDLK_SCANCODE_MASK,
			SDLK_CRSEL = (int)SDL_Scancode.SDL_SCANCODE_CRSEL | SDLK_SCANCODE_MASK,
			SDLK_EXSEL = (int)SDL_Scancode.SDL_SCANCODE_EXSEL | SDLK_SCANCODE_MASK,

			SDLK_KP_00 = (int)SDL_Scancode.SDL_SCANCODE_KP_00 | SDLK_SCANCODE_MASK,
			SDLK_KP_000 = (int)SDL_Scancode.SDL_SCANCODE_KP_000 | SDLK_SCANCODE_MASK,
			SDLK_THOUSANDSSEPARATOR =
			(int)SDL_Scancode.SDL_SCANCODE_THOUSANDSSEPARATOR | SDLK_SCANCODE_MASK,
			SDLK_DECIMALSEPARATOR =
			(int)SDL_Scancode.SDL_SCANCODE_DECIMALSEPARATOR | SDLK_SCANCODE_MASK,
			SDLK_CURRENCYUNIT = (int)SDL_Scancode.SDL_SCANCODE_CURRENCYUNIT | SDLK_SCANCODE_MASK,
			SDLK_CURRENCYSUBUNIT =
			(int)SDL_Scancode.SDL_SCANCODE_CURRENCYSUBUNIT | SDLK_SCANCODE_MASK,
			SDLK_KP_LEFTPAREN = (int)SDL_Scancode.SDL_SCANCODE_KP_LEFTPAREN | SDLK_SCANCODE_MASK,
			SDLK_KP_RIGHTPAREN = (int)SDL_Scancode.SDL_SCANCODE_KP_RIGHTPAREN | SDLK_SCANCODE_MASK,
			SDLK_KP_LEFTBRACE = (int)SDL_Scancode.SDL_SCANCODE_KP_LEFTBRACE | SDLK_SCANCODE_MASK,
			SDLK_KP_RIGHTBRACE = (int)SDL_Scancode.SDL_SCANCODE_KP_RIGHTBRACE | SDLK_SCANCODE_MASK,
			SDLK_KP_TAB = (int)SDL_Scancode.SDL_SCANCODE_KP_TAB | SDLK_SCANCODE_MASK,
			SDLK_KP_BACKSPACE = (int)SDL_Scancode.SDL_SCANCODE_KP_BACKSPACE | SDLK_SCANCODE_MASK,
			SDLK_KP_A = (int)SDL_Scancode.SDL_SCANCODE_KP_A | SDLK_SCANCODE_MASK,
			SDLK_KP_B = (int)SDL_Scancode.SDL_SCANCODE_KP_B | SDLK_SCANCODE_MASK,
			SDLK_KP_C = (int)SDL_Scancode.SDL_SCANCODE_KP_C | SDLK_SCANCODE_MASK,
			SDLK_KP_D = (int)SDL_Scancode.SDL_SCANCODE_KP_D | SDLK_SCANCODE_MASK,
			SDLK_KP_E = (int)SDL_Scancode.SDL_SCANCODE_KP_E | SDLK_SCANCODE_MASK,
			SDLK_KP_F = (int)SDL_Scancode.SDL_SCANCODE_KP_F | SDLK_SCANCODE_MASK,
			SDLK_KP_XOR = (int)SDL_Scancode.SDL_SCANCODE_KP_XOR | SDLK_SCANCODE_MASK,
			SDLK_KP_POWER = (int)SDL_Scancode.SDL_SCANCODE_KP_POWER | SDLK_SCANCODE_MASK,
			SDLK_KP_PERCENT = (int)SDL_Scancode.SDL_SCANCODE_KP_PERCENT | SDLK_SCANCODE_MASK,
			SDLK_KP_LESS = (int)SDL_Scancode.SDL_SCANCODE_KP_LESS | SDLK_SCANCODE_MASK,
			SDLK_KP_GREATER = (int)SDL_Scancode.SDL_SCANCODE_KP_GREATER | SDLK_SCANCODE_MASK,
			SDLK_KP_AMPERSAND = (int)SDL_Scancode.SDL_SCANCODE_KP_AMPERSAND | SDLK_SCANCODE_MASK,
			SDLK_KP_DBLAMPERSAND =
			(int)SDL_Scancode.SDL_SCANCODE_KP_DBLAMPERSAND | SDLK_SCANCODE_MASK,
			SDLK_KP_VERTICALBAR =
			(int)SDL_Scancode.SDL_SCANCODE_KP_VERTICALBAR | SDLK_SCANCODE_MASK,
			SDLK_KP_DBLVERTICALBAR =
			(int)SDL_Scancode.SDL_SCANCODE_KP_DBLVERTICALBAR | SDLK_SCANCODE_MASK,
			SDLK_KP_COLON = (int)SDL_Scancode.SDL_SCANCODE_KP_COLON | SDLK_SCANCODE_MASK,
			SDLK_KP_HASH = (int)SDL_Scancode.SDL_SCANCODE_KP_HASH | SDLK_SCANCODE_MASK,
			SDLK_KP_SPACE = (int)SDL_Scancode.SDL_SCANCODE_KP_SPACE | SDLK_SCANCODE_MASK,
			SDLK_KP_AT = (int)SDL_Scancode.SDL_SCANCODE_KP_AT | SDLK_SCANCODE_MASK,
			SDLK_KP_EXCLAM = (int)SDL_Scancode.SDL_SCANCODE_KP_EXCLAM | SDLK_SCANCODE_MASK,
			SDLK_KP_MEMSTORE = (int)SDL_Scancode.SDL_SCANCODE_KP_MEMSTORE | SDLK_SCANCODE_MASK,
			SDLK_KP_MEMRECALL = (int)SDL_Scancode.SDL_SCANCODE_KP_MEMRECALL | SDLK_SCANCODE_MASK,
			SDLK_KP_MEMCLEAR = (int)SDL_Scancode.SDL_SCANCODE_KP_MEMCLEAR | SDLK_SCANCODE_MASK,
			SDLK_KP_MEMADD = (int)SDL_Scancode.SDL_SCANCODE_KP_MEMADD | SDLK_SCANCODE_MASK,
			SDLK_KP_MEMSUBTRACT =
			(int)SDL_Scancode.SDL_SCANCODE_KP_MEMSUBTRACT | SDLK_SCANCODE_MASK,
			SDLK_KP_MEMMULTIPLY =
			(int)SDL_Scancode.SDL_SCANCODE_KP_MEMMULTIPLY | SDLK_SCANCODE_MASK,
			SDLK_KP_MEMDIVIDE = (int)SDL_Scancode.SDL_SCANCODE_KP_MEMDIVIDE | SDLK_SCANCODE_MASK,
			SDLK_KP_PLUSMINUS = (int)SDL_Scancode.SDL_SCANCODE_KP_PLUSMINUS | SDLK_SCANCODE_MASK,
			SDLK_KP_CLEAR = (int)SDL_Scancode.SDL_SCANCODE_KP_CLEAR | SDLK_SCANCODE_MASK,
			SDLK_KP_CLEARENTRY = (int)SDL_Scancode.SDL_SCANCODE_KP_CLEARENTRY | SDLK_SCANCODE_MASK,
			SDLK_KP_BINARY = (int)SDL_Scancode.SDL_SCANCODE_KP_BINARY | SDLK_SCANCODE_MASK,
			SDLK_KP_OCTAL = (int)SDL_Scancode.SDL_SCANCODE_KP_OCTAL | SDLK_SCANCODE_MASK,
			SDLK_KP_DECIMAL = (int)SDL_Scancode.SDL_SCANCODE_KP_DECIMAL | SDLK_SCANCODE_MASK,
			SDLK_KP_HEXADECIMAL =
			(int)SDL_Scancode.SDL_SCANCODE_KP_HEXADECIMAL | SDLK_SCANCODE_MASK,

			SDLK_LCTRL = (int)SDL_Scancode.SDL_SCANCODE_LCTRL | SDLK_SCANCODE_MASK,
			SDLK_LSHIFT = (int)SDL_Scancode.SDL_SCANCODE_LSHIFT | SDLK_SCANCODE_MASK,
			SDLK_LALT = (int)SDL_Scancode.SDL_SCANCODE_LALT | SDLK_SCANCODE_MASK,
			SDLK_LGUI = (int)SDL_Scancode.SDL_SCANCODE_LGUI | SDLK_SCANCODE_MASK,
			SDLK_RCTRL = (int)SDL_Scancode.SDL_SCANCODE_RCTRL | SDLK_SCANCODE_MASK,
			SDLK_RSHIFT = (int)SDL_Scancode.SDL_SCANCODE_RSHIFT | SDLK_SCANCODE_MASK,
			SDLK_RALT = (int)SDL_Scancode.SDL_SCANCODE_RALT | SDLK_SCANCODE_MASK,
			SDLK_RGUI = (int)SDL_Scancode.SDL_SCANCODE_RGUI | SDLK_SCANCODE_MASK,

			SDLK_MODE = (int)SDL_Scancode.SDL_SCANCODE_MODE | SDLK_SCANCODE_MASK,

			SDLK_AUDIONEXT = (int)SDL_Scancode.SDL_SCANCODE_AUDIONEXT | SDLK_SCANCODE_MASK,
			SDLK_AUDIOPREV = (int)SDL_Scancode.SDL_SCANCODE_AUDIOPREV | SDLK_SCANCODE_MASK,
			SDLK_AUDIOSTOP = (int)SDL_Scancode.SDL_SCANCODE_AUDIOSTOP | SDLK_SCANCODE_MASK,
			SDLK_AUDIOPLAY = (int)SDL_Scancode.SDL_SCANCODE_AUDIOPLAY | SDLK_SCANCODE_MASK,
			SDLK_AUDIOMUTE = (int)SDL_Scancode.SDL_SCANCODE_AUDIOMUTE | SDLK_SCANCODE_MASK,
			SDLK_MEDIASELECT = (int)SDL_Scancode.SDL_SCANCODE_MEDIASELECT | SDLK_SCANCODE_MASK,
			SDLK_WWW = (int)SDL_Scancode.SDL_SCANCODE_WWW | SDLK_SCANCODE_MASK,
			SDLK_MAIL = (int)SDL_Scancode.SDL_SCANCODE_MAIL | SDLK_SCANCODE_MASK,
			SDLK_CALCULATOR = (int)SDL_Scancode.SDL_SCANCODE_CALCULATOR | SDLK_SCANCODE_MASK,
			SDLK_COMPUTER = (int)SDL_Scancode.SDL_SCANCODE_COMPUTER | SDLK_SCANCODE_MASK,
			SDLK_AC_SEARCH = (int)SDL_Scancode.SDL_SCANCODE_AC_SEARCH | SDLK_SCANCODE_MASK,
			SDLK_AC_HOME = (int)SDL_Scancode.SDL_SCANCODE_AC_HOME | SDLK_SCANCODE_MASK,
			SDLK_AC_BACK = (int)SDL_Scancode.SDL_SCANCODE_AC_BACK | SDLK_SCANCODE_MASK,
			SDLK_AC_FORWARD = (int)SDL_Scancode.SDL_SCANCODE_AC_FORWARD | SDLK_SCANCODE_MASK,
			SDLK_AC_STOP = (int)SDL_Scancode.SDL_SCANCODE_AC_STOP | SDLK_SCANCODE_MASK,
			SDLK_AC_REFRESH = (int)SDL_Scancode.SDL_SCANCODE_AC_REFRESH | SDLK_SCANCODE_MASK,
			SDLK_AC_BOOKMARKS = (int)SDL_Scancode.SDL_SCANCODE_AC_BOOKMARKS | SDLK_SCANCODE_MASK,

			SDLK_BRIGHTNESSDOWN =
			(int)SDL_Scancode.SDL_SCANCODE_BRIGHTNESSDOWN | SDLK_SCANCODE_MASK,
			SDLK_BRIGHTNESSUP = (int)SDL_Scancode.SDL_SCANCODE_BRIGHTNESSUP | SDLK_SCANCODE_MASK,
			SDLK_DISPLAYSWITCH = (int)SDL_Scancode.SDL_SCANCODE_DISPLAYSWITCH | SDLK_SCANCODE_MASK,
			SDLK_KBDILLUMTOGGLE =
			(int)SDL_Scancode.SDL_SCANCODE_KBDILLUMTOGGLE | SDLK_SCANCODE_MASK,
			SDLK_KBDILLUMDOWN = (int)SDL_Scancode.SDL_SCANCODE_KBDILLUMDOWN | SDLK_SCANCODE_MASK,
			SDLK_KBDILLUMUP = (int)SDL_Scancode.SDL_SCANCODE_KBDILLUMUP | SDLK_SCANCODE_MASK,
			SDLK_EJECT = (int)SDL_Scancode.SDL_SCANCODE_EJECT | SDLK_SCANCODE_MASK,
			SDLK_SLEEP = (int)SDL_Scancode.SDL_SCANCODE_SLEEP | SDLK_SCANCODE_MASK
		}

		/* Key modifiers (bitfield) */
		[Flags]
		public enum SDL_Keymod : ushort
		{
			KMOD_NONE = 0x0000,
			KMOD_LSHIFT = 0x0001,
			KMOD_RSHIFT = 0x0002,
			KMOD_LCTRL = 0x0040,
			KMOD_RCTRL = 0x0080,
			KMOD_LALT = 0x0100,
			KMOD_RALT = 0x0200,
			KMOD_LGUI = 0x0400,
			KMOD_RGUI = 0x0800,
			KMOD_NUM = 0x1000,
			KMOD_CAPS = 0x2000,
			KMOD_MODE = 0x4000,
			KMOD_RESERVED = 0x8000,

			/* These are defines in the SDL headers */
			KMOD_CTRL = (KMOD_LCTRL | KMOD_RCTRL),
			KMOD_SHIFT = (KMOD_LSHIFT | KMOD_RSHIFT),
			KMOD_ALT = (KMOD_LALT | KMOD_RALT),
			KMOD_GUI = (KMOD_LGUI | KMOD_RGUI)
		}

		#endregion

		#region SDL_keyboard.h
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Keysym
		{
			public SDL_Scancode scancode;
			public SDL_Keycode sym;
			public SDL_Keymod mod; /* UInt16 */
			public UInt32 unicode; /* Deprecated */
		}

		/* Get the window which has kbd focus */
		/* Return type is an SDL_Window pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardFocus();

		/* Get a snapshot of the keyboard state. */
		/* Return value is a pointer to a UInt8 array */
		/* Numkeys returns the size of the array if non-null */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardState(ref int numkeys);

		/* Get the current key modifier state for the keyboard. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_Keymod SDL_GetModState();

		/* Set the current key modifier state */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetModState(SDL_Keymod modstate);

		/* Get the key code corresponding to the given scancode
		 * with the current keyboard layout.
		 */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetKeyFromScancode(SDL_Scancode scancode);

		/* Get the scancode for the given keycode */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetScancodeFromKey(SDL_Keycode key);
		
		/* Wrapper for SDL_GetScancodeName */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetScancodeName(SDL_Scancode scancode);

		/* Get a scancode from a human-readable name */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_Scancode SDL_GetScancodeFromName(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))] string name
		);
		
		/* Wrapper for SDL_GetKeyName */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetKeyName(SDL_Keycode key);

		/* Get a key code from a human-readable name */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_Keycode SDL_GetKeyFromName(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))] string name
		);
		
		/* Start accepting Unicode text input events, show keyboard */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_StartTextInput();

		/* Check if unicode input events are enabled */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_IsTextInputActive();

		/* Stop receiving any text input events, hide onscreen kbd */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_StopTextInput();

		/* Set the rectangle used for text input, hint for IME */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetTextInputRect(ref SDL_Rect rect);

		/* Does the platform support an on-screen keyboard? */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_HasScreenKeyboardSupport();

		/* Is the on-screen keyboard shown for a given window? */
		/* window is an SDL_Window pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_IsScreenKeyboardShown(IntPtr window);
		
		#endregion
		
		#region SDL_mouse.c

		/* Note: SDL_Cursor is a typedef normally. We'll treat it as
		 * an IntPtr, because C# doesn't do typedefs. Yay!
		 */

		/* System cursor types */
		public enum SDL_SystemCursor
		{
			SDL_SYSTEM_CURSOR_ARROW,     // Arrow
			SDL_SYSTEM_CURSOR_IBEAM,     // I-beam
			SDL_SYSTEM_CURSOR_WAIT,      // Wait
			SDL_SYSTEM_CURSOR_CROSSHAIR, // Crosshair
			SDL_SYSTEM_CURSOR_WAITARROW, // Small wait cursor (or Wait if not available)
			SDL_SYSTEM_CURSOR_SIZENWSE,  // Double arrow pointing northwest and southeast
			SDL_SYSTEM_CURSOR_SIZENESW,  // Double arrow pointing northeast and southwest
			SDL_SYSTEM_CURSOR_SIZEWE,    // Double arrow pointing west and east
			SDL_SYSTEM_CURSOR_SIZENS,    // Double arrow pointing north and south
			SDL_SYSTEM_CURSOR_SIZEALL,   // Four pointed arrow pointing north, south, east, and west
			SDL_SYSTEM_CURSOR_NO,        // Slashed circle or crossbones
			SDL_SYSTEM_CURSOR_HAND,      // Hand
			SDL_NUM_SYSTEM_CURSORS
		}

		/* Get the window which currently has mouse focus */
		/* Return value is an SDL_Window pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetMouseFocus();

		/* Get the current state of the mouse */
		/* NOTE: Not sure if x,y should be 'ref', as SDL accepts null */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 SDL_GetMouseState(out int x, out int y);

		/* Get the mouse state with relative coords*/
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 SDL_GetRelativeMouseState(out int x, out int y);

		/* Set the mouse cursor's position (within a window) */
		/* window is an SDL_Window pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WarpMouseInWindow(IntPtr window, int x, int y);

		/* Enable/Disable relative mouse mode (grabs mouse, rel coords) */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRelativeMouseMode(SDL_bool enabled);

		/* Query if the relative mouse mode is enabled */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_GetRelativeMouseMode();

		/* Create a cursor from bitmap data (amd mask) in MSB format */
		/* data and mask are byte arrays, and w must be a multiple of 8 */
		/* return value is an SDL_Cursor pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateCursor(
			IntPtr data,
			IntPtr mask,
			int w,
			int h,
			int hot_x,
			int hot_y
		);

		/* Create a cursor from an SDL_Surface */
		/* IntPtr refers to an SDL_Cursor*, surface to an SDL_Surface* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateColorCursor(
			IntPtr surface,
			int hot_x,
			int hot_y
		);

		/* Create a cursor from a system cursor id */
		/* return value is an SDL_Cursor pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSystemCursor(SDL_SystemCursor id);

		/* Set the active cursor */
		/* cursor is an SDL_Cursor pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetCursor(IntPtr cursor);

		/* Return the active cursor */
		/* return value is an SDL_Cursor pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCursor();

		/* Frees a cursor created with one of the CreateCursor functions */
		/* cursor in an SDL_Cursor pointer */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeCursor(IntPtr cursor);

		/* Toggle whether or not the cursor is shown */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_ShowCursor(int toggle);

		public static uint SDL_BUTTON(uint X)
		{
			// If only there were a better way of doing this in C#
			return (uint) (1 << ((int) X - 1));
		}

		public const uint SDL_BUTTON_LEFT =     1;
		public const uint SDL_BUTTON_MIDDLE =   2;
		public const uint SDL_BUTTON_RIGHT =    3;
		public const uint SDL_BUTTON_X1 =       4;
		public const uint SDL_BUTTON_X2 =       5;
		public static readonly UInt32 SDL_BUTTON_LMASK =    SDL_BUTTON(SDL_BUTTON_LEFT);
		public static readonly UInt32 SDL_BUTTON_MMASK =    SDL_BUTTON(SDL_BUTTON_MIDDLE);
		public static readonly UInt32 SDL_BUTTON_RMASK =    SDL_BUTTON(SDL_BUTTON_RIGHT);
		public static readonly UInt32 SDL_BUTTON_X1MASK =   SDL_BUTTON(SDL_BUTTON_X1);
		public static readonly UInt32 SDL_BUTTON_X2MASK =   SDL_BUTTON(SDL_BUTTON_X2);

		#endregion
		
		#region SDL_joystick.h
		
		public const byte SDL_HAT_CENTERED =	0x00;
		public const byte SDL_HAT_UP =		0x01;
		public const byte SDL_HAT_RIGHT =	0x02;
		public const byte SDL_HAT_DOWN =	0x04;
		public const byte SDL_HAT_LEFT =	0x08;
		public const byte SDL_HAT_RIGHTUP =	SDL_HAT_RIGHT | SDL_HAT_UP;
		public const byte SDL_HAT_RIGHTDOWN =	SDL_HAT_RIGHT | SDL_HAT_DOWN;
		public const byte SDL_HAT_LEFTUP =	SDL_HAT_LEFT | SDL_HAT_UP;
		public const byte SDL_HAT_LEFTDOWN =	SDL_HAT_LEFT | SDL_HAT_DOWN;
		
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SDL_JoystickGUID
		{
			public fixed byte data[16];
		}
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_JoystickClose(IntPtr joystick);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickEventState(int state);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_JoystickGetAxis(
			IntPtr joystick,
			int axis
		);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickGetBall(
			IntPtr joystick,
			int ball,
			ref int dx,
			ref int dy
		);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_JoystickGetButton(
			IntPtr joystick,
			int button
		);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_JoystickGetHat(
			IntPtr joystick,
			int hat
		);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickIndex(IntPtr joystick);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_JoystickName(
			IntPtr joystick
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_JoystickNameForIndex(
			int device_index
		);

		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumAxes(IntPtr joystick);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumBalls(IntPtr joystick);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumButtons(IntPtr joystick);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumHats(IntPtr joystick);
		
		/* IntPtr refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_JoystickOpen(int device_index);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickOpened(int device_index);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_JoystickUpdate();
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_NumJoysticks();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_JoystickGUID SDL_JoystickGetDeviceGUID(
			int device_index
		);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_JoystickGUID SDL_JoystickGetGUID(
			IntPtr joystick
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_JoystickGetGUIDString(
			SDL_JoystickGUID guid,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				System.Text.StringBuilder pszGUID,
			int cbGUID
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_JoystickGUID SDL_JoystickGetGUIDFromString(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string pchGUID
		);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_JoystickGetAttached(IntPtr joystick);
		
		/* int refers to an SDL_JoystickID, joystick to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickInstanceID(IntPtr joystick);
		
		#endregion
		
		#region SDL_gamecontroller.h
		
		public enum SDL_GameControllerBindType
		{
			SDL_CONTROLLER_BINDTYPE_NONE,
			SDL_CONTROLLER_BINDTYPE_BUTTON,
			SDL_CONTROLLER_BINDTYPE_AXIS,
			SDL_CONTROLLER_BINDTYPE_HAT
		}
		
		public enum SDL_GameControllerAxis
		{
			SDL_CONTROLLER_AXIS_INVALID = -1,
			SDL_CONTROLLER_AXIS_LEFTX,
			SDL_CONTROLLER_AXIS_LEFTY,
			SDL_CONTROLLER_AXIS_RIGHTX,
			SDL_CONTROLLER_AXIS_RIGHTY,
			SDL_CONTROLLER_AXIS_TRIGGERLEFT,
			SDL_CONTROLLER_AXIS_TRIGGERRIGHT,
			SDL_CONTROLLER_AXIS_MAX
		}
		
		public enum SDL_GameControllerButton
		{
			SDL_CONTROLLER_BUTTON_INVALID = -1,
			SDL_CONTROLLER_BUTTON_A,
			SDL_CONTROLLER_BUTTON_B,
			SDL_CONTROLLER_BUTTON_X,
			SDL_CONTROLLER_BUTTON_Y,
			SDL_CONTROLLER_BUTTON_BACK,
			SDL_CONTROLLER_BUTTON_GUIDE,
			SDL_CONTROLLER_BUTTON_START,
			SDL_CONTROLLER_BUTTON_LEFTSTICK,
			SDL_CONTROLLER_BUTTON_RIGHTSTICK,
			SDL_CONTROLLER_BUTTON_LEFTSHOULDER,
			SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
			SDL_CONTROLLER_BUTTON_DPAD_UP,
			SDL_CONTROLLER_BUTTON_DPAD_DOWN,
			SDL_CONTROLLER_BUTTON_DPAD_LEFT,
			SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
			SDL_CONTROLLER_BUTTON_MAX,
		}
		
		// FIXME: I'd rather this somehow be private...
		[StructLayout(LayoutKind.Sequential)]
		public struct INTERNAL_GameControllerButtonBind_hat
		{
			public int hat;
			public int hat_mask;
		}
		
		/* This struct has a union in it, hence the Explicit layout. */
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_GameControllerButtonBind
		{
			/* Note: enum size is 4 bytes. */
			[FieldOffset(0)]
			public SDL_GameControllerBindType bindType;
			[FieldOffset(4)]
			public int button;
			[FieldOffset(4)]
			public int axis;
			[FieldOffset(4)]
			public INTERNAL_GameControllerButtonBind_hat hat;
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerAddMapping(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string mappingString
		);
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GameControllerMappingForGUID", CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GameControllerMappingForGUID(
			SDL_JoystickGUID guid
		);
		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GameControllerMapping(
			IntPtr gamecontroller
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_IsGameController(int joystick_index);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GameControllerNameForIndex(
			int joystick_index
		);

		/* IntPtr refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GameControllerOpen(int joystick_index);
		
		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GameControllerName(
			IntPtr gamecontroller
		);

		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_bool SDL_GameControllerGetAttached(
			IntPtr gamecontroller
		);
		
		/* IntPtr refers to an SDL_Joystick*
		 * gamecontroller refers to an SDL_GameController*
		 */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GameControllerGetJoystick(
			IntPtr gamecontroller
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerEventState(int state);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GameControllerUpdate();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GameControllerAxis SDL_GameControllerGetAxisFromString(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string pchString
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GameControllerGetStringForAxis(
			SDL_GameControllerAxis axis
		);

		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GameControllerButtonBind SDL_GameControllerGetBindForAxis(
			IntPtr gamecontroller,
			SDL_GameControllerAxis axis
		);
		
		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_GameControllerGetAxis(
			IntPtr gamecontroller,
			SDL_GameControllerAxis axis
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GameControllerButton SDL_GameControllerGetButtonFromString(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string pchString
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GameControllerGetStringForButton(
			SDL_GameControllerButton button
		);

		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GameControllerButtonBind SDL_GameControllerGetBindForButton(
			IntPtr gamecontroller,
			SDL_GameControllerButton button
		);
		
		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GameControllerGetButton(
			IntPtr gamecontroller,
			SDL_GameControllerButton button
		);
		
		/* gamecontroller refers to an SDL_GameController* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GameControllerClose(
			IntPtr gamecontroller
		);
		
		#endregion
		
		#region SDL_haptic.h
		
		/* SDL_HapticEffect type */
		public const ushort SDL_HAPTIC_CONSTANT =	(1 << 0);
		public const ushort SDL_HAPTIC_SINE =		(1 << 1);
		public const ushort SDL_HAPTIC_SQUARE =		(1 << 2);
		public const ushort SDL_HAPTIC_TRIANGLE =	(1 << 3);
		public const ushort SDL_HAPTIC_SAWTOOTHUP =	(1 << 4);
		public const ushort SDL_HAPTIC_SAWTOOTHDOWN =	(1 << 5);
		public const ushort SDL_HAPTIC_SPRING =		(1 << 7);
		public const ushort SDL_HAPTIC_DAMPER =		(1 << 8);
		public const ushort SDL_HAPTIC_INERTIA =	(1 << 9);
		public const ushort SDL_HAPTIC_FRICTION =	(1 << 10);
		public const ushort SDL_HAPTIC_CUSTOM =		(1 << 11);
		public const ushort SDL_HAPTIC_GAIN =		(1 << 12);
		public const ushort SDL_HAPTIC_AUTOCENTER =	(1 << 13);
		public const ushort SDL_HAPTIC_STATUS =		(1 << 14);
		public const ushort SDL_HAPTIC_PAUSE =		(1 << 15);
		public const ushort SDL_HAPTIC_RUMBLE =		(1 << 11); /* FIXME: Uint16, SHIIIIIIIT */
		
		/* SDL_HapticDirection type */
		public const byte SDL_HAPTIC_POLAR =		0;
		public const byte SDL_HAPTIC_CARTESIAN =	1;
		public const byte SDL_HAPTIC_SPHERICAL =	2;
		
		/* SDL_HapticRunEffect */
		public const uint SDL_HAPTIC_INFINITY = 4292967295U;
		
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SDL_HapticDirection
		{
			public byte type;
			public fixed int dir[3];
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_HapticConstant
		{
			// Header
			public ushort type;
			public SDL_HapticDirection direction;
			// Replay
			public uint length;
			public ushort delay;
			// Trigger
			public ushort button;
			public ushort interval;
			// Constant
			public short level;
			// Envelope
			public ushort attack_length;
			public ushort attack_level;
			public ushort fade_length;
			public ushort fade_level;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_HapticPeriodic
		{
			// Header
			public ushort type;
			public SDL_HapticDirection direction;
			// Replay
			public uint length;
			public ushort delay;
			// Trigger
			public ushort button;
			public ushort interval;
			// Periodic
			public ushort period;
			public short magnitude;
			public short offset;
			public ushort phase;
			// Envelope
			public ushort attack_length;
			public ushort attack_level;
			public ushort fade_length;
			public ushort fade_level;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SDL_HapticCondition
		{
			// Header
			public ushort type;
			public SDL_HapticDirection direction;
			// Replay
			public uint length;
			public ushort delay;
			// Trigger
			public ushort button;
			public ushort interval;
			// Condition
			public fixed ushort right_sat[3];
			public fixed ushort left_sat[3];
			public fixed short right_coeff[3];
			public fixed short left_coeff[3];
			public fixed ushort deadband[3];
			public fixed short center[3];
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_HapticRamp
		{
			// Header
			public ushort type;
			public SDL_HapticDirection direction;
			// Replay
			public uint length;
			public ushort delay;
			// Trigger
			public ushort button;
			public ushort interval;
			// Ramp
			public short start;
			public short end;
			// Envelope
			public ushort attack_length;
			public ushort attack_level;
			public ushort fade_length;
			public ushort fade_level;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_HapticRumble
		{
			// Header
			public ushort type;
			public SDL_HapticDirection direction;
			// Replay
			public uint length;
			public ushort delay;
			// Trigger
			public ushort button;
			public ushort interval;
			// Rumble
			public ushort large_magnitude;
			public ushort small_magnitude;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_HapticCustom
		{
			// Header
			public ushort type;
			public SDL_HapticDirection direction;
			// Replay
			public uint length;
			public ushort delay;
			// Trigger
			public ushort button;
			public ushort interval;
			// Custom
			public byte channels;
			public ushort period;
			public ushort samples;
			public IntPtr data; // Uint16*
			// Envelope
			public ushort attack_length;
			public ushort attack_level;
			public ushort fade_length;
			public ushort fade_level;
		}
		
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_HapticEffect
		{
			[FieldOffset(0)]
			public ushort type;
			[FieldOffset(0)]
			public SDL_HapticConstant constant;
			[FieldOffset(0)]
			public SDL_HapticPeriodic periodic;
			[FieldOffset(0)]
			public SDL_HapticCondition condition;
			[FieldOffset(0)]
			public SDL_HapticRamp ramp;
			[FieldOffset(0)]
			public SDL_HapticRumble rumble;
			[FieldOffset(0)]
			public SDL_HapticCustom custom;
		}
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_HapticClose(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_HapticDestroyEffect(
			IntPtr haptic,
			int effect
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticEffectSupported(
			IntPtr haptic,
			ref SDL_HapticEffect effect
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticGetEffectStatus(
			IntPtr haptic,
			int effect
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticIndex(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_HapticName(int device_index);

		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNewEffect(
			IntPtr haptic,
			ref SDL_HapticEffect effect
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNumAxes(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNumEffects(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNumEffectsPlaying(IntPtr haptic);
		
		/* IntPtr refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_HapticOpen(int device_index);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticOpened(int device_index);
		
		/* IntPtr refers to an SDL_Haptic*, joystick to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_HapticOpenFromJoystick(
			IntPtr joystick
		);
		
		/* IntPtr refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_HapticOpenFromMouse();
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticPause(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_HapticQuery(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumbleInit(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumblePlay(
			IntPtr haptic,
			float strength,
			uint length
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumbleStop(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumbleSupported(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRunEffect(
			IntPtr haptic,
			int effect,
			uint iterations
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticSetAutocenter(
			IntPtr haptic,
			int autocenter
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticSetGain(
			IntPtr haptic,
			int gain
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticStopAll(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticStopEffect(
			IntPtr haptic,
			int effect
		);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticUnpause(IntPtr haptic);
		
		/* haptic refers to an SDL_Haptic* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticUpdateEffect(
			IntPtr haptic,
			int effect,
			ref SDL_HapticEffect data
		);
		
		/* joystick refers to an SDL_Joystick* */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickIsHaptic(IntPtr joystick);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_MouseIsHaptic();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_NumHaptics();
		
		#endregion

		#region SDL_audio.h
		
		public const ushort SDL_AUDIO_MASK_BITSIZE =	0xFF;
		public const ushort SDL_AUDIO_MASK_DATATYPE =	(1 << 8);
		public const ushort SDL_AUDIO_MASK_ENDIAN =	(1 << 12);
		public const ushort SDL_AUDIO_MASK_SIGNED =	(1 << 15);
		
		public static ushort SDL_AUDIO_BITSIZE(ushort x)
		{
			return (ushort) (x & SDL_AUDIO_MASK_BITSIZE);
		}
		
		public static bool SDL_AUDIO_ISFLOAT(ushort x)
		{
			return (x & SDL_AUDIO_MASK_DATATYPE) != 0;
		}
		
		public static bool SDL_AUDIO_ISBIGENDIAN(ushort x)
		{
			return (x & SDL_AUDIO_MASK_ENDIAN) != 0;
		}
		
		public static bool SDL_AUDIO_ISSIGNED(ushort x)
		{
			return (x & SDL_AUDIO_MASK_SIGNED) != 0;
		}
		
		public static bool SDL_AUDIO_ISINT(ushort x)
		{
			return (x & SDL_AUDIO_MASK_DATATYPE) == 0;
		}
		
		public static bool SDL_AUDIO_ISLITTLEENDIAN(ushort x)
		{
			return (x & SDL_AUDIO_MASK_ENDIAN) == 0;
		}
		
		public static bool SDL_AUDIO_ISUNSIGNED(ushort x)
		{
			return (x & SDL_AUDIO_MASK_SIGNED) == 0;
		}
		
		public const ushort AUDIO_U8 =		0x0008;
		public const ushort AUDIO_S8 =		0x8008;
		public const ushort AUDIO_U16LSB =	0x0010;
		public const ushort AUDIO_S16LSB =	0x8010;
		public const ushort AUDIO_U16MSB =	0x1010;
		public const ushort AUDIO_S16MSB =	0x9010;
		public const ushort AUDIO_U16 =		AUDIO_U16LSB;
		public const ushort AUDIO_S16 =		AUDIO_S16LSB;
		public const ushort AUDIO_S32LSB =	0x8020;
		public const ushort AUDIO_S32MSB =	0x9020;
		public const ushort AUDIO_S32 =		AUDIO_S32LSB;
		public const ushort AUDIO_F32LSB =	0x8120;
		public const ushort AUDIO_F32MSB =	0x9120;
		public const ushort AUDIO_F32 =		AUDIO_F32LSB;
		
		public static readonly ushort AUDIO_U16SYS =
			BitConverter.IsLittleEndian ? AUDIO_U16LSB : AUDIO_U16MSB;
		public static readonly ushort AUDIO_S16SYS =
			BitConverter.IsLittleEndian ? AUDIO_S16LSB : AUDIO_S16MSB;
		public static readonly ushort AUDIO_S32SYS =
			BitConverter.IsLittleEndian ? AUDIO_S32LSB : AUDIO_S32MSB;
		public static readonly ushort AUDIO_F32SYS =
			BitConverter.IsLittleEndian ? AUDIO_F32LSB : AUDIO_F32MSB;
		
		public const uint SDL_AUDIO_ALLOW_FREQUENCY_CHANGE =	0x00000001;
		public const uint SDL_AUDIO_ALLOW_FORMAT_CHANGE =	0x00000001;
		public const uint SDL_AUDIO_ALLOW_CHANNELS_CHANGE =	0x00000001;
		public const uint SDL_AUDIO_ALLOW_ANY_CHANGE = (
			SDL_AUDIO_ALLOW_FREQUENCY_CHANGE |
			SDL_AUDIO_ALLOW_FORMAT_CHANGE |
			SDL_AUDIO_ALLOW_CHANNELS_CHANGE
		);
		
		public const int SDL_MIX_MAXVOLUME = 128;
		
		public enum SDL_AudioStatus
		{
			SDL_AUDIO_STOPPED,
			SDL_AUDIO_PLAYING,
			SDL_AUDIO_PAUSED
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_AudioSpec
		{
			public int freq;
			public ushort format; // SDL_AudioFormat
			public byte channels;
			public byte silence;
			public ushort samples;
			public uint size;
			public SDL_AudioCallback callback;
			public IntPtr userdata; // void*
		}
		
		/* userdata refers to a void*, stream to a Uint8 */
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AudioCallback(
			IntPtr userdata,
			IntPtr stream,
			int len
		);
		
		/* dev refers to an SDL_AudioDeviceID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AudioDeviceConnected(uint dev);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AudioInit(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string driver_name
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_AudioQuit();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseAudio();
		
		/* dev refers to an SDL_AudioDeviceID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseAudioDevice(uint dev);
		
		/* audio_buf refers to a malloc()'d buffer from SDL_LoadWAV */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeWAV(IntPtr audio_buf);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetAudioDeviceName(
			int index,
			int iscapture
		);

		/* dev refers to an SDL_AudioDeviceID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_AudioStatus SDL_GetAudioDeviceStatus(
			uint dev
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetAudioDriver(int index);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_AudioStatus SDL_GetAudioStatus();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string SDL_GetCurrentAudioDriver();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumAudioDevices(int iscapture);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumAudioDrivers();
		
		/* audio_buf will refer to a malloc()'d byte buffer */
		/* THIS IS AN RWops FUNCTION! */
		[DllImport(nativeLibName, EntryPoint = "SDL_LoadWAV_RW", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_LoadWAV_RW(
			IntPtr src,
			int freesrc,
			ref SDL_AudioSpec spec,
			ref IntPtr audio_buf,
			ref uint audio_len
		);
		public static SDL_AudioSpec SDL_LoadWAV(
			string file,
			ref SDL_AudioSpec spec,
			ref IntPtr audio_buf,
			ref uint audio_len
		) {
			SDL_AudioSpec result;
			IntPtr rwops = INTERNAL_SDL_RWFromFile(file, "rb");
			IntPtr result_ptr = INTERNAL_SDL_LoadWAV_RW(
				rwops,
				1,
				ref spec,
				ref audio_buf,
				ref audio_len
			);
			result = (SDL_AudioSpec) Marshal.PtrToStructure(
				result_ptr,
				typeof(SDL_AudioSpec)
			);
			return result;
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockAudio();
		
		/* dev refers to an SDL_AudioDeviceID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockAudioDevice(uint dev);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MixAudio(
			byte[] dst,
			byte[] src,
			uint len,
			int volume
		);
		
		/* format refers to an SDL_AudioFormat */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MixAudioFormat(
			byte[] dst,
			byte[] src,
			ushort format,
			uint len,
			int volume
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_OpenAudio(
			ref SDL_AudioSpec desired,
			ref SDL_AudioSpec obtained
		);
		
		/* uint refers to an SDL_AudioDeviceID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_OpenAudioDevice(
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string device,
			int iscapture,
			ref SDL_AudioSpec desired,
			ref SDL_AudioSpec obtained,
			int allowed_changes
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PauseAudio(int pause_on);
		
		/* dev refers to an SDL_AudioDeviceID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PauseAudioDevice(
			uint dev,
			int pause_on
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockAudio();
		
		/* dev refers to an SDL_AudioDeviceID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockAudioDevice(uint dev);
		
		#endregion
	}
}

#pragma warning restore 0169
