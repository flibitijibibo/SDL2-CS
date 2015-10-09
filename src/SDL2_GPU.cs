using System;
using System.Runtime.InteropServices;

namespace SDL2
{
	/// <summary>
	/// Description of SDL_GPU.
	/// </summary>
	public static class SDL2_GPU
	{
		#region SDL2# Variables

		/// <summary>
		/// Used by DllImport to load the native library.
		/// </summary>
		private const string nativeLibName = "SDL2_gpu.dll";

		#endregion
		
		#region Initialization
		[Flags]
		public enum GPU_FeatureEnum: uint
		{
			GPU_FEATURE_NON_POWER_OF_TWO = 0x1,
			GPU_FEATURE_RENDER_TARGETS = 0x2,
			GPU_FEATURE_BLEND_EQUATIONS = 0x4,
			GPU_FEATURE_BLEND_FUNC_SEPARATE = 0x8,
			GPU_FEATURE_BLEND_EQUATIONS_SEPARATE = 0x10,
			GPU_FEATURE_GL_BGR = 0x20,
			GPU_FEATURE_GL_BGRA = 0x40,
			GPU_FEATURE_GL_ABGR = 0x80,
			GPU_FEATURE_VERTEX_SHADER = 0x100,
			GPU_FEATURE_FRAGMENT_SHADER = 0x200,
			GPU_FEATURE_PIXEL_SHADER = 0x200,
			GPU_FEATURE_GEOMETRY_SHADER = 0x400,
			GPU_FEATURE_WRAP_REPEAT_MIRRORED = 0x800,
			
			GPU_FEATURE_ALL_BASE = GPU_FEATURE_RENDER_TARGETS,
			GPU_FEATURE_ALL_BLEND_PRESETS = (GPU_FEATURE_BLEND_EQUATIONS | GPU_FEATURE_BLEND_FUNC_SEPARATE),
			GPU_FEATURE_ALL_GL_FORMATS = (GPU_FEATURE_GL_BGR | GPU_FEATURE_GL_BGRA | GPU_FEATURE_GL_ABGR),
			GPU_FEATURE_BASIC_SHADERS = (GPU_FEATURE_FRAGMENT_SHADER | GPU_FEATURE_VERTEX_SHADER),
			GPU_FEATURE_ALL_SHADERS = (GPU_FEATURE_FRAGMENT_SHADER | GPU_FEATURE_VERTEX_SHADER | GPU_FEATURE_GEOMETRY_SHADER)
		}

		public enum GPU_RendererEnum: uint {
			GPU_RENDERER_UNKNOWN = 0,  // invalid value
			GPU_RENDERER_OPENGL_1_BASE = 1,
			GPU_RENDERER_OPENGL_1 = 2,
			GPU_RENDERER_OPENGL_2 = 3,
			GPU_RENDERER_OPENGL_3 = 4,
			GPU_RENDERER_OPENGL_4 = 5,
			GPU_RENDERER_GLES_1 = 11,
			GPU_RENDERER_GLES_2 = 12,
			GPU_RENDERER_GLES_3 = 13,
			GPU_RENDERER_D3D9 = 21,
			GPU_RENDERER_D3D10 = 22,
			GPU_RENDERER_D3D11 = 23
		}

		/*
		 * Initialization flags for changing default init parameters.  Can be bitwise OR'ed together.
		 * Default (0) is to use late swap vsync and double buffering.
		 * \see GPU_SetPreInitFlags()
		 * \see GPU_GetPreInitFlags()
		 */
		[Flags]
		public enum GPU_InitFlagEnum: uint {
			GPU_INIT_ENABLE_VSYNC = 0x1,
			GPU_INIT_DISABLE_VSYNC = 0x2,
			GPU_INIT_DISABLE_DOUBLE_BUFFER = 0x4,
			GPU_INIT_DISABLE_AUTO_VIRTUAL_RESOLUTION = 0x8,
			GPU_INIT_REQUEST_COMPATIBILITY_PROFILE = 0x10
		}

		/* The window corresponding to 'windowID' will be used to create the rendering context instead of creating a new window. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetInitWindow ( UInt32 windowID);

		/* Returns the window ID that has been set via GPU_SetInitWindow(). */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 GPU_GetInitWindow ();

		/* Set special flags to use for initialization. Set these before calling GPU_Init().

		   Parameters:
			GPU_flags	An OR'ed combination of GPU_InitFlagEnum flags. Default flags (0) enable late swap vsync and double buffering. 
		*/
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetPreInitFlags (GPU_InitFlagEnum GPU_flags);
 
		/* Returns the current special flags to use for initialization. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_InitFlagEnum GPU_GetPreInitFlags ();

		/* Set required features to use for initialization. Set these before calling GPU_Init().

		   Parameters:
			features	An OR'ed combination of GPU_FeatureEnum flags. Required features will force
						GPU_Init() to create a renderer that supports all of the given flags or else fail. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetRequiredFeatures (GPU_FeatureEnum features);

		/* Returns the current required features to use for initialization. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_FeatureEnum GPU_GetRequiredFeatures ();

		/* Gets the default initialization renderer IDs for the current platform copied into the
		   'order' array and the number of renderer IDs into 'order_size'. Pass NULL for 'order' to just
		   get the size of the renderer order array. Will return at most GPU_RENDERER_ORDER_MAX renderers.  */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GetDefaultRendererOrder (
			ref int order_size,
			[Out()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex =  0)] GPU_RendererID[] order);

		/* Gets the current renderer ID order for initialization copied into the 'order' array and
		   the number of renderer IDs into 'order_size'. Pass NULL for 'order' to just get the
		   size of the renderer order array. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GetRendererOrder (
			ref int order_size,
			[Out()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex =  0)] GPU_RendererID[] order);

		/* Sets the renderer ID order to use for initialization. If 'order' is NULL, it will restore the default order. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetRendererOrder (
			int order_size,
			[In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] GPU_RendererID[] order);

		/* Initializes SDL and SDL_gpu. Creates a window and goes through the renderer order to create a renderer context.

		   See also
			GPU_SetRendererOrder() 
		 */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_Init (UInt16 w, UInt16 h, SDL.SDL_WindowFlags SDL_flags);

		/* Initializes SDL and SDL_gpu. Creates a window and the requested renderer context. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_InitRenderer (GPU_RendererEnum renderer_enum, UInt16 w, UInt16 h, SDL.SDL_WindowFlags SDL_flags);

		/* Initializes SDL and SDL_gpu. Creates a window and the requested renderer context.
		   By requesting a renderer via ID, you can specify the major and minor versions of
		   an individual renderer backend.

		   See also
			GPU_MakeRendererID */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_InitRendererByID (GPU_RendererID renderer_request, UInt16 w, UInt16 h, SDL.SDL_WindowFlags SDL_flags);

		/* Checks for important GPU features which may not be supported depending on a device's
		   extension support. Feature flags (GPU_FEATURE_*) can be bitwise OR'd together.

		   Returns
			1 if all of the passed features are enabled/supported 
			0 if any of the passed features are disabled/unsupported 

		*/
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_IsFeatureEnabled (GPU_FeatureEnum feature);

		/* Clean up the renderer state. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_CloseCurrentRenderer ();

		/* Clean up the renderer state and shut down SDL_gpu.  */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Quit ();

		#endregion
		
		#region Debugging, Logging, and Error Handling
		
		/* Use GPU_Log() for normal logging output (e.g. to replace printf). Other logging
		   priorities are handled by GPU_LogWarning() and GPU_LogError().

		   SDL_gpu stores an error stack that you can read and manipulate using GPU_PopErrorCode()
		   and GPU_PushErrorCode(). If you set the debug level using GPU_SetDebugLevel(), you can
		   have any errors automatically logged as they are generated. */

		/* Type enumeration for error codes.

		   See also
			GPU_PushErrorCode() 
			GPU_PopErrorCode() */
		public enum GPU_ErrorEnum {
			GPU_ERROR_NONE = 0,
			GPU_ERROR_BACKEND_ERROR = 1,
			GPU_ERROR_DATA_ERROR = 2,
			GPU_ERROR_USER_ERROR = 3,
			GPU_ERROR_UNSUPPORTED_FUNCTION = 4,
			GPU_ERROR_NULL_ARGUMENT = 5,
			GPU_ERROR_FILE_NOT_FOUND = 6
		};

		/* Type enumeration for debug levels.

		   See also
			GPU_SetDebugLevel()
			GPU_GetDebugLevel() */
		public enum GPU_DebugLevelEnum {
			GPU_DEBUG_LEVEL_0 = 0,
			GPU_DEBUG_LEVEL_1 = 1,
			GPU_DEBUG_LEVEL_2 = 2,
			GPU_DEBUG_LEVEL_3 = 3,
			GPU_DEBUG_LEVEL_MAX = 3
		}

		public struct GPU_ErrorObject
		{
			public CString function;
			public GPU_ErrorEnum error;
			public CString details;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetDebugLevel (GPU_DebugLevelEnum level);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_DebugLevelEnum GPU_GetDebugLevel ();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_LogInfo (
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string format,
			__arglist
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_LogWarning (
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string format,
			__arglist
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_LogError (
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string format,
			__arglist
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_PushErrorCode (
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string function,
			GPU_ErrorEnum error,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string details,
			__arglist
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_ErrorObject GPU_PopErrorCode ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string GPU_GetErrorString (GPU_ErrorEnum error);

		#endregion
		
		#region Renderer Setup
		
		/* Renderer ID object for identifying a specific renderer.

		   See also
			GPU_MakeRendererID()
			GPU_InitRendererByID() */
		public struct GPU_RendererID {
			public CString name;
			public GPU_RendererEnum renderer;
			public int major_version;
			public int minor_version;
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_RendererID GPU_MakeRendererID (
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string name,
			GPU_RendererEnum renderer, int major_version, int minor_version);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_RendererID GPU_GetRendererID (GPU_RendererEnum renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_RendererID GPU_GetRendererIDByIndex (uint index);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int GPU_GetNumRegisteredRenderers ();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GetRegisteredRendererList (
			[Out()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)]
			GPU_RendererID[] renderers_array);

		#endregion
		
		#region Renderer Controls
		
		/*! Renderer object which specializes the API to a particular backend. */
		public struct GPU_Renderer
		{
			/*! Struct identifier of the renderer. */
			public GPU_RendererID id;
			public GPU_RendererID requested_id;
			public SDL.SDL_WindowFlags SDL_init_flags;
			public GPU_InitFlagEnum GPU_init_flags;
			
			public GPU_ShaderLanguageEnum shader_language;
			public int min_shader_version;
			public int max_shader_version;
		    public GPU_FeatureEnum enabled_features;
			
			/*! Current display target */
			public GPU_Target_PTR current_context_target;
			
			/*! 0 for inverted, 1 for mathematical */
			public byte coordinate_mode;
			
			public IntPtr impl;
		};

		public struct GPU_Renderer_PTR
		{
			public IntPtr Pointer;
			
			public GPU_Renderer Value {
				get { return Marshal.PtrToStructure<GPU_Renderer>(Pointer); }
				set { Marshal.StructureToPtr(value, Pointer, false); }
			}
			
			public static GPU_Renderer_PTR Null {
				get { return default(GPU_Renderer_PTR); }
			}
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_RendererEnum GPU_ReserveNextRendererEnum ();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int GPU_GetNumActiveRenderers ();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GetActiveRendererList (
			[Out()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)]
			GPU_RendererID[] renderers_array);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Renderer_PTR GPU_GetRenderer (uint index);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Renderer_PTR GPU_GetRendererByID (GPU_RendererID id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Renderer_PTR GPU_GetCurrentRenderer ();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetCurrentRenderer (GPU_RendererID id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_ResetRendererState ();

		#endregion
		
		#region Context Controls
		
		public struct GPU_Context
		{
			/*! SDL_GLContext */
			public IntPtr context;
			public byte failed;
			
			/*! SDL window ID */
			public uint windowID;
			
			/*! Actual window dimensions */
			public int window_w;
			public int window_h;
			
			/*! Drawable region dimensions */
			public int drawable_w;
			public int drawable_h;
			
			/*! Window dimensions for restoring windowed mode after GPU_SetFullscreen(1,1). */
			public int stored_window_w;
			public int stored_window_h;
			
			/*! Internal state */
			public uint current_shader_program;
			public uint default_textured_shader_program;
			public uint default_untextured_shader_program;
			
			public byte shapes_use_blending;
			public GPU_BlendMode shapes_blend_mode;
			public float line_thickness;
			public byte use_texturing;
			
			public int matrix_mode;
			public GPU_MatrixStack projection_matrix;
			public GPU_MatrixStack modelview_matrix;
			
			public IntPtr data;
		};

		public struct GPU_Context_PTR
		{
			public IntPtr Pointer;
			
			public GPU_Context Value {
				get { return Marshal.PtrToStructure<GPU_Context>(Pointer); }
				set { Marshal.StructureToPtr(value, Pointer, false); }
			}
			
			public static GPU_Context_PTR Null {
				get { return default(GPU_Context_PTR); }
			}
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_GetContextTarget ();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_GetWindowTarget (UInt32 windowID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_CreateTargetFromWindow (UInt32 windowID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_MakeCurrent (GPU_Target_PTR target, UInt32 windowID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_SetWindowResolution (UInt16 w, UInt16 h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_SetFullscreen (byte enable_fullscreen, byte use_desktop_resolution);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_GetFullscreen ();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetShapeBlending (byte enable);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_BlendMode GPU_GetBlendModeFromPreset (GPU_BlendPresetEnum preset);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetShapeBlendFunction (GPU_BlendFuncEnum source_color, GPU_BlendFuncEnum dest_color, GPU_BlendFuncEnum source_alpha, GPU_BlendFuncEnum dest_alpha);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetShapeBlendEquation (GPU_BlendEqEnum color_equation, GPU_BlendEqEnum alpha_equation);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetShapeBlendMode (GPU_BlendPresetEnum mode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float GPU_SetLineThickness (float thickness);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float GPU_GetLineThickness ();
		
		#endregion
		
		#region Target Controls
		
		/* Camera object that determines viewing transform.
		 * \see GPU_SetCamera() 
		 * \see GPU_GetDefaultCamera() 
		 * \see GPU_GetCamera()
		 */
		public struct GPU_Camera {
			public float x, y, z;
			public float angle;
			public float zoom;
		};
		
		public struct GPU_Camera_PTR
		{
			public IntPtr Pointer;
			
			public GPU_Camera Value {
				get { return Marshal.PtrToStructure<GPU_Camera>(Pointer); }
				set { Marshal.StructureToPtr(value, Pointer, false); }
			}
			
			public static GPU_Camera_PTR Null {
				get { return default(GPU_Camera_PTR); }
			}
		}

		/* Render target object for use as a blitting destination.
		 * A GPU_Target can be created from a GPU_Image with GPU_LoadTarget().
		 * A GPU_Target can also represent a separate window with GPU_CreateTargetFromWindow().  In that case, 'context' is allocated and filled in.
		 * Note: You must have passed the SDL_WINDOW_OPENGL flag to SDL_CreateWindow() for OpenGL renderers to work with new windows.
		 * Free the memory with GPU_FreeTarget() when you're done.
		 * \see GPU_LoadTarget()
		 * \see GPU_CreateTargetFromWindow()
		 * \see GPU_FreeTarget()
		 */
		public struct GPU_Target
		{
			//GPU_Renderer_PTR
			public IntPtr renderer;
			
			public GPU_Image_PTR image;
			
			/* void* */
			public IntPtr data;
			
			public UInt16 w, h;
			public byte using_virtual_resolution;
			public UInt16 base_w, base_h;  // The true dimensions of the underlying image or window
			public byte use_clip_rect;
			public GPU_Rect clip_rect;
			public byte use_color;
			public SDL.SDL_Color color;
			
			public GPU_Rect viewport;
			
			/*! Perspective and object viewing transforms. */
			public GPU_Camera camera;
			
			/*! Renderer context data.  NULL if the target does not represent a window or rendering context. */
			public GPU_Context_PTR context;
			
			public int refcount;
			public byte is_alias;
		};
		
		public struct GPU_Target_PTR
		{
			public IntPtr Pointer;
			
			public GPU_Target Value {
				get { return Marshal.PtrToStructure<GPU_Target>(Pointer); }
				set { Marshal.StructureToPtr(value, Pointer, false); }
			}
			
			public static GPU_Target_PTR Null {
				get { return default(GPU_Target_PTR); }
			}
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_CreateAliasTarget (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Target_PTR GPU_LoadTarget (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_FreeTarget (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetVirtualResolution (IntPtr target, UInt16 w, UInt16 h);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GetVirtualResolution (GPU_Target_PTR target, out UInt16 w, out UInt16 h);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GetVirtualCoords (GPU_Target_PTR target, ref float x, ref float y, float displayX, float displayY);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UnsetVirtualResolution (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Rect GPU_MakeRect (float x, float y, float w, float h);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Color GPU_MakeColor (byte r, byte g, byte b, byte a);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetViewport (GPU_Target_PTR target, GPU_Rect viewport);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UnsetViewport (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Camera GPU_GetDefaultCamera ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Camera GPU_GetCamera (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		/* target param is GPU_Target *, cam param is GPU_Camera * */
		public static extern GPU_Camera GPU_SetCamera (GPU_Target_PTR target, GPU_Camera_PTR cam);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Color GPU_GetPixel (GPU_Target_PTR target, Int16 x, Int16 y);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Rect GPU_SetClipRect (GPU_Target_PTR target, GPU_Rect rect);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Rect GPU_SetClip (GPU_Target_PTR target, Int16 x, Int16 y, UInt16 w, UInt16 h);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UnsetClip (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetTargetColor (GPU_Target_PTR target, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetTargetRGB (GPU_Target_PTR target, byte r, byte g, byte b);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetTargetRGBA (GPU_Target_PTR target, byte r, byte g, byte b, byte a);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UnsetTargetColor (GPU_Target_PTR target);
		
		#endregion
		
		#region Surface Controls
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr GPU_LoadSurface (
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string filename);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_SaveSurface (
			IntPtr surface,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string filename);
		
		#endregion
		
		#region Image Controls
		
		/* Blend component functions
		 * \see GPU_SetBlendFunction()
		 * Values chosen for direct OpenGL compatibility.
		 */
		public enum GPU_BlendFuncEnum {
			GPU_FUNC_ZERO = 0,
			GPU_FUNC_ONE = 1,
			GPU_FUNC_SRC_COLOR = 0x0300,
			GPU_FUNC_DST_COLOR = 0x0306,
			GPU_FUNC_ONE_MINUS_SRC = 0x0301,
			GPU_FUNC_ONE_MINUS_DST = 0x0307,
			GPU_FUNC_SRC_ALPHA = 0x0302,
			GPU_FUNC_DST_ALPHA = 0x0304,
			GPU_FUNC_ONE_MINUS_SRC_ALPHA = 0x0303,
			GPU_FUNC_ONE_MINUS_DST_ALPHA = 0x0305
		};
		
		/*! Blend component equations
		 * \see GPU_SetBlendEquation()
		 * Values chosen for direct OpenGL compatibility.
		 */
		public enum GPU_BlendEqEnum {
			GPU_EQ_ADD = 0x8006,
			GPU_EQ_SUBTRACT = 0x800A,
			GPU_EQ_REVERSE_SUBTRACT = 0x800B
		};
		
		/* Blend mode presets 
		 * \see GPU_SetBlendMode()
		 * \see GPU_GetBlendModeFromPreset()
		 */
		public enum GPU_BlendPresetEnum {
			GPU_BLEND_NORMAL = 0,
			GPU_BLEND_PREMULTIPLIED_ALPHA = 1,
			GPU_BLEND_MULTIPLY = 2,
			GPU_BLEND_ADD = 3,
			GPU_BLEND_SUBTRACT = 4,
			GPU_BLEND_MOD_ALPHA = 5,
			GPU_BLEND_SET_ALPHA = 6,
			GPU_BLEND_SET = 7,
			GPU_BLEND_NORMAL_KEEP_ALPHA = 8,
			GPU_BLEND_NORMAL_ADD_ALPHA = 9
		};
		
		public enum GPU_FilterEnum {
			GPU_FILTER_NEAREST = 0,
			GPU_FILTER_LINEAR = 1,
			GPU_FILTER_LINEAR_MIPMAP = 2
		};
 
		public enum GPU_SnapEnum {
			GPU_SNAP_NONE = 0,
			GPU_SNAP_POSITION = 1, 
			GPU_SNAP_DIMENSIONS = 2,
			GPU_SNAP_POSITION_AND_DIMENSIONS = 3
		};
		 
		public enum GPU_WrapEnum {
			GPU_WRAP_NONE = 0,
			GPU_WRAP_REPEAT = 1,
			GPU_WRAP_MIRRORED = 2
		};
		 
		public enum GPU_FormatEnum {
			GPU_FORMAT_LUMINANCE = 1,
			GPU_FORMAT_LUMINANCE_ALPHA = 2,
			GPU_FORMAT_RGB = 3,
			GPU_FORMAT_RGBA = 4,
			GPU_FORMAT_ALPHA = 5,
			GPU_FORMAT_RG = 6,
			GPU_FORMAT_YCbCr422 = 7,
			GPU_FORMAT_YCbCr420P = 8
		};
		
		public struct GPU_BlendMode
		{
			public GPU_BlendFuncEnum source_color;
			public GPU_BlendFuncEnum dest_color;
			public GPU_BlendFuncEnum source_alpha;
			public GPU_BlendFuncEnum dest_alpha;
			
			public GPU_BlendEqEnum color_equation;
			public GPU_BlendEqEnum alpha_equation;
		};
		
		/*
		 * Image object for containing pixel/texture data.
		 * A GPU_Image can be created with GPU_CreateImage(), GPU_LoadImage(), GPU_CopyImage(), or GPU_CopyImageFromSurface().
		 * Free the memory with GPU_FreeImage() when you're done.
		 * \see GPU_CreateImage()
		 * \see GPU_LoadImage()
		 * \see GPU_CopyImage()
		 * \see GPU_CopyImageFromSurface()
		 * \see GPU_Target
		 */
		public struct GPU_Image
		{
			//GPU_Renderer_PTR
			public IntPtr renderer;
			
			//GPU_Target_PTR 
			public IntPtr target;
			
			public UInt16 w, h;
			public byte using_virtual_resolution;
			public GPU_FormatEnum format;
			public int num_layers;
			public int bytes_per_pixel;
			public UInt16 base_w, base_h;  // Original image dimensions
			public UInt16 texture_w, texture_h;  // Underlying texture dimensions
			public byte has_mipmaps;
			
			public SDL.SDL_Color color;
			public byte use_blending;
			public GPU_BlendMode blend_mode;
			public GPU_FilterEnum filter_mode;
			public GPU_SnapEnum snap_mode;
			public GPU_WrapEnum wrap_mode_x;
			public GPU_WrapEnum wrap_mode_y;
			
			/* void* */
			public IntPtr data;
			
			public int refcount;
			public byte is_alias;
		};
		
		public struct GPU_Image_PTR
		{
			public IntPtr Pointer;
			
			public GPU_Image Value {
				get { return Marshal.PtrToStructure<GPU_Image>(Pointer); }
				set { Marshal.StructureToPtr(value, Pointer, false); }
			}
			
			public static GPU_Image_PTR Null {
				get { return default(GPU_Image_PTR); }
			}
		}
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Image_PTR GPU_CreateImage (UInt16 w, UInt16 h, GPU_FormatEnum format);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Image_PTR GPU_CreateImageUsingTexture (UInt32 handle, byte take_ownership);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Image_PTR GPU_LoadImage (
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string filename);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Image_PTR GPU_CreateAliasImage (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Image_PTR GPU_CopyImage (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_FreeImage (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UpdateImage (
			GPU_Image_PTR image,
			IntPtr surface,
			ref GPU_Rect surface_rect);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UpdateSubImage (
			GPU_Image_PTR image,
			ref GPU_Rect image_rect,
			IntPtr surface,
			ref GPU_Rect surface_rect);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UpdateImageBytes (
			GPU_Image_PTR image,
			ref GPU_Rect image_rect,
			[In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1)]
			byte[] bytes,
			int bytes_per_row
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_SaveImage (
			GPU_Image_PTR image, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string filename);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GenerateMipmaps (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetColor (GPU_Image_PTR image, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Color GPU_GetColor (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetRGB (GPU_Image_PTR image, byte r, byte g, byte b);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetRGBA (GPU_Image_PTR image, byte r, byte g, byte b, byte a);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_UnsetColor (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_GetBlending (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetBlending (GPU_Image_PTR image, byte enable);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetBlendFunction (
			GPU_Image_PTR image,
			GPU_BlendFuncEnum source_color,
			GPU_BlendFuncEnum dest_color,
			GPU_BlendFuncEnum source_alpha,
			GPU_BlendFuncEnum dest_alpha);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetBlendEquation (
			GPU_Image_PTR image,
			GPU_BlendEqEnum color_equation,
			GPU_BlendEqEnum alpha_equation);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetBlendMode (GPU_Image_PTR image, GPU_BlendPresetEnum mode);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetImageFilter (GPU_Image_PTR image, GPU_FilterEnum filter);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_SnapEnum GPU_GetSnapMode (GPU_Image_PTR image);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetSnapMode (GPU_Image_PTR image, GPU_SnapEnum mode);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetWrapMode (GPU_Image_PTR image, GPU_WrapEnum wrap_mode_x, GPU_WrapEnum wrap_mode_y);
			
		#endregion
		
		#region Surface, Image, and Target Conversions
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Image_PTR GPU_CopyImageFromSurface (IntPtr surface);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Image_PTR GPU_CopyImageFromTarget (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr GPU_CopySurfaceFromTarget (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr GPU_CopySurfaceFromImage (GPU_Image_PTR image);
		
		#endregion
		
		#region Matrix
		const int GPU_MODELVIEW = 0;
		const int GPU_PROJECTION = 1;
		const int GPU_MATRIX_STACK_MAX = 5;
		
		/*! Matrix stack data structure for global vertex transforms.  */
		public unsafe struct GPU_MatrixStack {
			public uint size;
			public fixed float matrix[GPU_MATRIX_STACK_MAX * 16];
		};
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_MatrixCopy (
			[Out()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)] float[] result,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] A);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_MatrixIdentity (
			[Out()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)] float[] result
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern  void GPU_Multiply4x4 (
			[Out()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)]
			float[] result,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] A,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] B);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern  void GPU_MultiplyAndAssign (
			[Out()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)] float[] result,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] A);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string GPU_GetMatrixString (
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] A
		);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)]
		public static extern float[] GPU_GetCurrentMatrix ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)]
		public static extern float[] GPU_GetModelView ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)]
		public static extern float[] GPU_GetProjection ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_GetModelViewProjection ([Out()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = 16)] float[] result);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_MatrixMode (int matrix_mode);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_PushMatrix ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_PopMatrix ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_LoadIdentity ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Ortho (float left, float right, float bottom, float top, float near, float far);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Frustum (float right, float left, float bottom, float top, float near, float far);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Translate (float x, float y, float z);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Scale (float sx, float sy, float sz);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Rotate (float degrees, float x, float y, float z);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_MultMatrix (
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] matrix4x4
		);
		
		#endregion
		
		#region Rendering
		
		/* A struct representing a rectangular area with floating point precision.
		 * \see GPU_MakeRect() 
		 */
		public struct GPU_Rect {
			public float x, y;
			public float w, h;
		};
		
		[Flags]
		public enum GPU_BlitFlagEnum: uint {
			GPU_PASSTHROUGH_VERTICES = 0x1,
			GPU_PASSTHROUGH_TEXCOORDS = 0x2,
			GPU_PASSTHROUGH_COLORS = 0x4,
			GPU_USE_DEFAULT_POSITIONS = 0x8,
			GPU_USE_DEFAULT_SRC_RECTS = 0x10,
			GPU_USE_DEFAULT_COLORS = 0x20,
			GPU_PASSTHROUGH_ALL = GPU_PASSTHROUGH_VERTICES | GPU_PASSTHROUGH_TEXCOORDS | GPU_PASSTHROUGH_COLORS
		}

		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Clear (GPU_Target_PTR target);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_ClearColor (GPU_Target_PTR target, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_ClearRGB (GPU_Target_PTR target, byte r, byte g, byte b);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_ClearRGBA (GPU_Target_PTR target, byte r, byte g, byte b, byte a);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Blit (
			GPU_Image_PTR image,
			ref GPU_Rect src_rect,
			GPU_Target_PTR target,
			float x,
			float y);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		/* This overload allows you to pass a null to src_rect */
		public static extern void GPU_Blit (
			GPU_Image_PTR image,
			IntPtr src_rect,
			GPU_Target_PTR target,
			float x, float y);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_BlitRotate (
			GPU_Image_PTR image,
			ref GPU_Rect src_rect,
			GPU_Target_PTR target,
			float x, float y, float degrees);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_BlitScale (
			GPU_Image_PTR image,
			ref GPU_Rect src_rect,
			GPU_Target_PTR target,
			float x, float y, float scaleX, float scaleY);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		/* This overload allows you to pass a null to src_rect */
		public static extern void GPU_BlitScale (
			GPU_Image_PTR image,
			IntPtr src_rect,
			GPU_Target_PTR target,
			float x, float y, float scaleX, float scaleY);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_BlitTransform (
			GPU_Image_PTR image,
			ref GPU_Rect src_rect,
			GPU_Target_PTR target,
			float x, float y, float degrees, float scaleX, float scaleY);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_BlitTransformX (
			GPU_Image_PTR image,
			ref GPU_Rect src_rect,
			GPU_Target_PTR target,
			float x, float y, float pivot_x, float pivot_y, float degrees, float scaleX, float scaleY);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_BlitTransformMatrix (
			GPU_Image_PTR image,
			ref GPU_Rect src_rect,
			GPU_Target_PTR target,
			float x,
			float y,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] matrix3x3);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_BlitBatch (
			GPU_Image_PTR image,
			GPU_Target_PTR target,
			uint num_sprites,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] values,
			GPU_BlitFlagEnum flags);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_BlitBatchSeparate (
			GPU_Image_PTR image,
			GPU_Target_PTR target,
			uint num_sprites,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] positions,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] src_rects,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] colors,
			GPU_BlitFlagEnum flags);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_TriangleBatch (
			GPU_Image_PTR image,
			GPU_Target_PTR target,
			ushort num_vertices,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] values,
			uint num_indices,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			ushort[] indices,
			GPU_BlitFlagEnum flags);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_FlushBlitBuffer ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Flip (GPU_Target_PTR target);

		#endregion
		
		#region Shapes
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Pixel (GPU_Target_PTR target, float x, float y, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Line (GPU_Target_PTR target, float x1, float y1, float x2, float y2, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Arc (GPU_Target_PTR target, float x, float y, float radius, float start_angle, float end_angle, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_ArcFilled (GPU_Target_PTR target, float x, float y, float radius, float start_angle, float end_angle, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Circle (GPU_Target_PTR target, float x, float y, float radius, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_CircleFilled (GPU_Target_PTR target, float x, float y, float radius, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Ellipse (GPU_Target_PTR target, float x, float y, float rx, float ry, float degrees, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_EllipseFilled (GPU_Target_PTR target, float x, float y, float rx, float ry, float degrees, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Sector (GPU_Target_PTR target, float x, float y, float inner_radius, float outer_radius, float start_angle, float end_angle, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SectorFilled (GPU_Target_PTR target, float x, float y, float inner_radius, float outer_radius, float start_angle, float end_angle, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Tri (GPU_Target_PTR target, float x1, float y1, float x2, float y2, float x3, float y3, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_TriFilled (GPU_Target_PTR target, float x1, float y1, float x2, float y2, float x3, float y3, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Rectangle (GPU_Target_PTR target, float x1, float y1, float x2, float y2, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_RectangleFilled (GPU_Target_PTR target, float x1, float y1, float x2, float y2, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_RectangleRound (GPU_Target_PTR target, float x1, float y1, float x2, float y2, float radius, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_RectangleRoundFilled (GPU_Target_PTR target, float x1, float y1, float x2, float y2, float radius, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_Polygon (
			GPU_Target_PTR target,
			uint num_vertices,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] vertices,
			SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_PolygonFilled (
			GPU_Target_PTR target,
			uint num_vertices,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] vertices,
			SDL.SDL_Color color);
		
		#endregion
		
		#region Shader Interface
		
		/* Type enumeration for GPU_AttributeFormat specifications. */
		public enum GPU_TypeEnum: uint {
		// Use OpenGL's values for simpler translation
			GPU_TYPE_BYTE = 0x1400,
			GPU_TYPE_UNSIGNED_BYTE = 0x1401,
			GPU_TYPE_SHORT = 0x1402,
			GPU_TYPE_UNSIGNED_SHORT = 0x1403,
			GPU_TYPE_INT = 0x1404,
			GPU_TYPE_UNSIGNED_INT = 0x1405,
			GPU_TYPE_FLOAT = 0x1406,
			GPU_TYPE_DOUBLE = 0x140A
		};
		
		/* Type enumeration for the shader language used by the renderer. */
		public enum GPU_ShaderLanguageEnum {
			GPU_LANGUAGE_NONE = 0,
			GPU_LANGUAGE_ARB_ASSEMBLY = 1,
			GPU_LANGUAGE_GLSL = 2,
			GPU_LANGUAGE_GLSLES = 3,
			GPU_LANGUAGE_HLSL = 4,
			GPU_LANGUAGE_CG = 5
		};
		
		public enum GPU_ShaderEnum {
			GPU_VERTEX_SHADER = 0,
			GPU_FRAGMENT_SHADER = 1,
			GPU_PIXEL_SHADER = 1,
			GPU_GEOMETRY_SHADER = 2
		};
		
		/* Container for the built-in shader attribute and uniform locations (indices).
		 * \see GPU_LoadShaderBlock()
		 * \see GPU_SetShaderBlock()
		 */
		public struct GPU_ShaderBlock {
			// Attributes
			public int position_loc;
			public int texcoord_loc;
			public int color_loc;
			// Uniforms
			public int modelViewProjection_loc;
		};
		
		public struct GPU_AttributeFormat {
			public byte is_per_sprite;  // Per-sprite values are expanded to 4 vertices
			public int num_elems_per_value;
			public GPU_TypeEnum type;  // GPU_TYPE_FLOAT, GPU_TYPE_INT, GPU_TYPE_UNSIGNED_INT, etc.
			public byte normalize;
			public int stride_bytes;  // Number of bytes between two vertex specifications
			public int offset_bytes;  // Number of bytes to skip at the beginning of 'values'
		};
		
		public struct GPU_Attribute
		{
			public int location;
			/* void* */
			public IntPtr values;  // Expect 4 values for each sprite
			public GPU_AttributeFormat format;
		};
		
		public struct GPU_AttributeSource
		{
			public byte enabled;
			public int num_values;
			
			/* void* */
			public IntPtr next_value;
			
			// Automatic storage format
			public int per_vertex_storage_stride_bytes;
			public int per_vertex_storage_offset_bytes;
			public int per_vertex_storage_size;  // Over 0 means that the per-vertex storage has been automatically allocated
			
			/* void* */
			public IntPtr per_vertex_storage;  // Could point to the attribute's values or to allocated storage
			
			public GPU_Attribute attribute;
		};
		
		public struct GPU_MultitextureBlock
		{
			public int num_textures;
			public IntPtr image_names; /* array of char* */
			public IntPtr texcoord_names; /* array of char* */
		};
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 GPU_CreateShaderProgram ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_FreeShaderProgram (UInt32 program_object);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 GPU_CompileShader_RW (GPU_ShaderEnum shader_type, IntPtr /* SDL_RWops* */ shader_source);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 GPU_CompileShader (
				GPU_ShaderEnum shader_type,
				[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string shader_source);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 GPU_LoadShader (
				GPU_ShaderEnum shader_type,
				[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string filename);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 GPU_LinkShaders (UInt32 shader_object1, UInt32 shader_object2);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_FreeShader (UInt32 shader_object);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_AttachShader (UInt32 program_object, UInt32 shader_object);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_DetachShader (UInt32 program_object, UInt32 shader_object);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_LinkShaderProgram (UInt32 program_object);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt32 GPU_GetCurrentShaderProgram ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GPU_IsDefaultShaderProgram (UInt32 program_object);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_ActivateShaderProgram (UInt32 program_object, ref GPU_ShaderBlock block);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_ActivateShaderProgram (UInt32 program_object, IntPtr block);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_DeactivateShaderProgram ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		[return : MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler), MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
		public static extern string GPU_GetShaderMessage ();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int GPU_GetAttributeLocation (
			UInt32 program_object,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string attrib_name);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_AttributeFormat GPU_MakeAttributeFormat (
				int num_elems_per_vertex,
				GPU_TypeEnum type,
				byte normalize,
				int stride_bytes,
				int offset_bytes);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Attribute GPU_MakeAttribute (
			int location,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)]
			int[] values,
			GPU_AttributeFormat format);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Attribute GPU_MakeAttribute (
			int location,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)]
			uint[] values,
			GPU_AttributeFormat format);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_Attribute GPU_MakeAttribute (
			int location,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] values,
			GPU_AttributeFormat format);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int GPU_GetUniformLocation (
				UInt32 program_object,
				[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string uniform_name);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_ShaderBlock GPU_LoadShaderBlock (
				UInt32 program_object,
				[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string position_name,
				[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string texcoord_name,
				[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string color_name,
				[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
				string modelViewMatrix_name);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetShaderBlock (GPU_ShaderBlock block);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern GPU_MultitextureBlock GPU_LoadMultitextureBlock (
			int count,
			[In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 0)]
			string[] image_names,
			[In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 0)]
			string[] texcoord_names);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetMultitextureBlock (ref GPU_MultitextureBlock value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_MultitextureBlit (
			[In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysUInt)]
			GPU_Image_PTR[] images,
			[In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)]
			GPU_Rect[] rects,
			GPU_Target_PTR target, float x, float y);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetShaderImage (GPU_Image_PTR image, int location, int image_unit);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetUniformi (int location, int value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetUniformiv (
			int location,
			int num_elements_per_value,
			int num_values,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)]
			int[] values);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetUniformui (int location, uint value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetUniformuiv (
			int location,
			int num_elements_per_value,
			int num_values,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)]
			uint[] values);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetUniformf (int location, float value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetUniformfv (
			int location,
			int num_elements_per_value,
			int num_values,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] values);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetUniformMatrixfv (
			int location,
			int num_matrices,
			int num_rows,
			int num_columns,
			byte transpose,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] values);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetAttributef (int location, float value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetAttributei (int location, int value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetAttributeui (int location, uint value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetAttributefv (
			int location,
			int num_elements,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			float[] value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetAttributeiv (
			int location,
			int num_elements,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			int[] value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetAttributeuiv (
			int location,
			int num_elements,
			[In()][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)]
			uint[] value);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void GPU_SetAttributeSource (int num_values, GPU_Attribute source);
		
		#endregion
	}
}
