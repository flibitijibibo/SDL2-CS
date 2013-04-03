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

namespace SDL2
{
	public static class SDL2
	{
		#region SDL2# Variables
		
		/* Used by DllImport to load the native library. */
		internal const String nativeLibName = "SDL2.dll";
		
		#endregion
		
		#region SDL_stdinc.h
		
		public enum SDL_bool
		{
			SDL_FALSE = 0,
			SDL_TRUE = 1
		}
		
		#endregion
		
		#region SDL.h
		
		public const uint SDL_INIT_TIMER =			0x00000001;
		public const uint SDL_INIT_AUDIO =			0x00000010;
		public const uint SDL_INIT_VIDEO =			0x00000020;
		public const uint SDL_INIT_JOYSTICK =		0x00000200;
		public const uint SDL_INIT_HAPTIC =			0x00001000;
		public const uint SDL_INIT_GAMECONTROLLER =	0x00002000;
		public const uint SDL_INIT_NOPARACHUTE =	0x00100000;
		public const uint SDL_INIT_EVERYTHING = (
			SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO |
			SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC | SDL_INIT_GAMECONTROLLER
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_Init(uint flags);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_InitSubSystem(uint flags);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_Quit();
		
		[DllImport(nativeLibName)]
		public static extern void SDL_QuitSubSystem(uint flags);
		
		[DllImport(nativeLibName)]
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
		
		[DllImport(nativeLibName)]
		public static extern void SDL_ClearHints();
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetHint")]
		private static extern IntPtr INTERNAL_SDL_GetHint(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name
		);
		public static string SDL_GetHint(string name)
		{
			return Marshal.PtrToStringAnsi(INTERNAL_SDL_GetHint(name));
		}
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_SetHint(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string value
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_SetHintWithPriority(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string value,
			SDL_HintPriority priority
		);
		
		#endregion
		
		#region SDL_error.h
		
		[DllImport(nativeLibName)]
		public static extern void SDL_ClearError();
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetError")]
		private static extern IntPtr INTERNAL_SDL_GetError();
		public static string SDL_GetError()
		{
			return Marshal.PtrToStringAnsi(INTERNAL_SDL_GetError());
		}
		
		[DllImport(nativeLibName)]
		public static extern void SDL_SetError(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
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
				MYAPP_CATEGORY_AWESOME1 = SDL_LOG_CATEGORY_CUSTOM,
				MYAPP_CATEGORY_AWESOME2,
				MYAPP_CATEGORY_AWESOME3,
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
		
		[DllImport(nativeLibName)]
		public static extern void SDL_Log(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogVerbose(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogDebug(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogInfo(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogWarn(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogError(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogCritical(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogMessage(
			int category,
			SDL_LogPriority priority,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogMessageV(
			int category,
			SDL_LogPriority priority,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_LogPriority SDL_LogGetPriority(int category);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogSetPriority(
			int category,
			SDL_LogPriority priority
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogSetAllPriority(
			SDL_LogPriority priority
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogResetPriorities();
		
		// TODO: SDL_LogGetOutputFunction
		// TODO: SDL_LogSetOutputFunction
		
		#endregion
		
		#region SDL_version.h, SDL_revision.h
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_version
		{
			public byte major;
			public byte minor;
			public byte patch;
		}
		
		[DllImport(nativeLibName)]
		private static extern void SDL_GetVersion(ref SDL_version ver);
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetRevision")]
		private static extern IntPtr INTERNAL_SDL_GetRevision();
		public static string SDL_GetRevision()
		{
			return Marshal.PtrToStringAnsi(INTERNAL_SDL_GetRevision());
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetRevisionNumber();
		
		#endregion
		
		#region SDL_video.h
		
		/* Actually, this is from SDL_blendmode.h */
		public enum SDL_BlendMode
		{
			SDL_BLENDMODE_NONE =	0x00000000,
			SDL_BLENDMODE_BLEND =	0x00000001,
			SDL_BLENDMODE_ADD =		0x00000002,
			SDL_BLENDMODE_MOD =		0x00000004
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
		
		public enum SDL_WindowEventID
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
		
		public enum SDL_WindowFlags
		{
			SDL_WINDOW_FULLSCREEN =		0x00000001,
			SDL_WINDOW_OPENGL =			0x00000002,
			SDL_WINDOW_SHOWN =			0x00000004,
			SDL_WINDOW_HIDDEN =			0x00000008,
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
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_DisplayMode
		{
			public uint format;
			public int w;
			public int h;
			public int refresh_rate;
			public IntPtr driverdata; // void*
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_WindowEvent
		{
			public uint type;
			public uint windowID;
			public byte windowEvent; // event, lolC#
			public int data1;
			public int data2;
		}
		
		/* IntPtr refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_CreateWindow(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string title,
			int x,
			int y,
			int w,
			int h,
			uint flags
		);
		
		/* TODO: SDL_video.h:
		 * http://wiki.libsdl.org/moin.fcg/CategoryVideo
		 * http://hg.libsdl.org/SDL/file/default/include/SDL_video.h
		 */
		
		#endregion
		
		#region SDL_render.h
		
		/* TODO: SDL_render.h:
		 * http://wiki.libsdl.org/moin.fcg/CategoryRender
		 * http://hg.libsdl.org/SDL/file/default/include/SDL_render.h
		 */
		
		#endregion
		
		#region SDL_pixels.h
		
		/* TODO: SDL_pixels.h:
		 * http://wiki.libsdl.org/moin.fcg/CategoryPixels
		 * http://hg.libsdl.org/SDL/file/default/include/SDL_pixels.h
		 */
		
		#endregion
		
		#region SDL_rect.h
		
		public struct SDL_Point
		{
			public int x;
			public int y;
		}
		
		public struct SDL_Rect
		{
			public int x;
			public int y;
			public int w;
			public int h;
		}
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_EnclosePoints(
			SDL_Point[] points,
			int count,
			ref SDL_Rect clip,
			ref SDL_Rect result
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_HasIntersection(
			ref SDL_Rect A,
			ref SDL_Rect B
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_IntersectRect(
			ref SDL_Rect A,
			ref SDL_Rect B,
			ref SDL_Rect result
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_IntersectRectAndLine(
			ref SDL_Rect rect,
			ref int X1,
			ref int Y1,
			ref int X2,
			ref int Y2
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_RectEmpty(ref SDL_Rect rect);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_RectEquals(
			ref SDL_Rect A,
			ref SDL_Rect B
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_UnionRect(
			ref SDL_Rect A,
			ref SDL_Rect B,
			ref SDL_Rect result
		);
		
		#endregion
		
		#region SDL_surface.h
		
		/* TODO: SDL_surface.h:
		 * http://wiki.libsdl.org/moin.fcg/CategorySurface
		 * http://hg.libsdl.org/SDL/file/default/include/SDL_surface.h
		 */
		
		#endregion
		
		#region SDL_clipboard.h
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_HasClipboardText();
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetClipboardText")]
		private static extern IntPtr INTERNAL_SDL_GetClipboardText();
		public static string SDL_GetClipboardText(string name)
		{
			return Marshal.PtrToStringAnsi(INTERNAL_SDL_GetClipboardText());
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SetClipboardText(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string text
		);
		
		#endregion
	}
}