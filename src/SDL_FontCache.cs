using System;
using System.Runtime.InteropServices;

namespace SDL2
{
	public static class SDL_FontCache
	{
		private const string nativeLibName = "SDL_FontCache.dll";
		
		public struct FC_GlyphData
		{
			public SDL.SDL_Rect rect;
			public int cache_level;
		}
		
		public struct FC_GlyphData_PTR
		{
			public IntPtr Pointer;
			
			public FC_GlyphData Value {
				get { return Marshal.PtrToStructure<FC_GlyphData>(Pointer); }
				set { Marshal.StructureToPtr(value, Pointer, false); }
			}
			
			public static FC_GlyphData_PTR Null {
				get { return default(FC_GlyphData_PTR); }
			}
		}
		
		public enum FC_AlignEnum
		{
			FC_ALIGN_LEFT,
			FC_ALIGN_CENTER,
			FC_ALIGN_RIGHT
		};
		
		public struct FC_Scale
		{
			float x;
			float y;
		};
		
		public struct FC_Font
		{
			public IntPtr ttf_source;  // TTF_Font source of characters
			public byte owns_ttf_source;  // Can we delete the TTF_Font ourselves?
			
			public SDL.SDL_Color default_color;
			public UInt16 height;
			
			public UInt16 maxWidth;
			public UInt16 baseline;
			public int ascent;
			public int descent;
			
			public int lineSpacing;
			public int letterSpacing;
			
			// Uses 32-bit (4-byte) Unicode codepoints to refer to each glyph
			// Codepoints are little endian (reversed from UTF-8) so that something like 0x00000005 is ASCII 5 and the map can be indexed by ASCII values
			public IntPtr /* FC_Map* */ glyphs;
			
			public FC_GlyphData last_glyph;  // Texture packing cursor
			public int glyph_cache_size;
			public int glyph_cache_count;
			public IntPtr /* SDL2_GPU.GPU_Image_PTR */ glyph_cache;
			
			public CString loading_string;
		}
		
		public struct FC_Font_PTR
		{
			public IntPtr Pointer;
			
			public FC_Font Value {
				get { return Marshal.PtrToStructure<FC_Font>(Pointer); }
				set { Marshal.StructureToPtr(value, Pointer, false); }
			}
			
			public static FC_Font_PTR Null {
				get { return default(FC_Font_PTR); }
			}
		}
		
		public struct FC_Effect
		{
			FC_AlignEnum alignment;
			FC_Scale scale;
			SDL.SDL_Color color;
		}
		
		// Object creation
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_MakeRect(float x, float y, float w, float h);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern FC_Scale  FC_MakeScale(float x, float y);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Color  FC_MakeColor(byte r, byte g, byte b, byte a);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern FC_Effect  FC_MakeEffect(FC_AlignEnum alignment, FC_Scale scale, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern FC_GlyphData  FC_MakeGlyphData(int cache_level, Int16 x, Int16 y, UInt16 w, UInt16 h);
		
		
		
		// Font object
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern FC_Font_PTR  FC_CreateFont();
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte  FC_LoadFont(
			FC_Font_PTR font,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string filename_ttf,
			Int32 pointSize,
			SDL.SDL_Color color,
			int style);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte  FC_LoadFontFromTTF(FC_Font_PTR font, IntPtr ttf, SDL.SDL_Color color);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void  FC_ClearFont(FC_Font_PTR font);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void  FC_FreeFont(FC_Font_PTR font);
		
		
		
		// Internal settings
		
		/*! Sets the string from which to load the initial glyphs.  Use this if you need upfront loading for any reason (such as lack of render-target support). */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void  FC_SetLoadingString(FC_Font_PTR font,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string str);
		
		/*! Changes the size of the internal buffer which is used for unpacking variadic text data.  This buffer is shared by all FC_Fonts. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void  FC_SetBufferSize(uint size);
		
		
		// Custom caching
		
		/*! Returns the number of cache levels that are active. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int  FC_GetNumCacheLevels(FC_Font_PTR font);
		
		/*! Returns the cache source texture at the given cache level. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Image_PTR FC_GetGlyphCacheLevel(FC_Font_PTR font, int cache_level);
		
		// TODO: Specify ownership of the texture (should be shareable)
		/*! Sets a cache source texture for rendering.  New cache levels must be sequential. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte  FC_SetGlyphCacheLevel(FC_Font_PTR font, int cache_level, SDL2_GPU.GPU_Image_PTR cache_texture);
		
		/*! Stores the glyph data for the given codepoint in 'result'.  Returns 0 if the codepoint was not found in the cache. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte  FC_GetGlyphData(FC_Font_PTR font, FC_GlyphData_PTR result, UInt32 codepoint);
		
		/*! Sets the glyph data for the given codepoint.  Duplicates are not checked.  Returns a pointer to the stored data. */
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern FC_GlyphData_PTR FC_SetGlyphData(FC_Font_PTR font, UInt32 codepoint, FC_GlyphData glyph_data);
		
		
		// Rendering
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect FC_Draw(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y,
		    [In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawAlign(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y,
			FC_AlignEnum align,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawScale(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y,
			FC_Scale scale,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawColor(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y,
			SDL.SDL_Color color,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawEffect(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y,
			FC_Effect effect,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawBox(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			SDL2_GPU.GPU_Rect box,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawBoxAlign(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			SDL2_GPU.GPU_Rect box, FC_AlignEnum align,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawBoxEffect(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			SDL2_GPU.GPU_Rect box, FC_Effect effect,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawColumn(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y, UInt16 width, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawColumnAlign(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y, UInt16 width, FC_AlignEnum align, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_DrawColumnEffect(
			FC_Font_PTR font, SDL2_GPU.GPU_Target_PTR dest,
			float x, float y, UInt16 width, FC_Effect effect, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		
		// Getters
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt16  FC_GetLineHeight(FC_Font_PTR font);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt16  FC_GetHeight(
			FC_Font_PTR font,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt16  FC_GetWidth(
			FC_Font_PTR font,
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL2_GPU.GPU_Rect  FC_GetCharacterOffset(
			FC_Font_PTR font,
			UInt16 position_index, int column_width, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt16  FC_GetColumnHeight(
			FC_Font_PTR font,
			UInt16 width, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int  FC_GetAscent(
			FC_Font_PTR font, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int  FC_GetDescent(
			FC_Font_PTR font, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int  FC_GetBaseline(FC_Font_PTR font);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int  FC_GetSpacing(FC_Font_PTR font);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int  FC_GetLineSpacing(FC_Font_PTR font);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt16  FC_GetMaxWidth(FC_Font_PTR font);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Color  FC_GetDefaultColor(FC_Font_PTR font);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte  FC_InRect(float x, float y, SDL2_GPU.GPU_Rect input_rect);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UInt16  FC_GetPositionFromOffset(
			FC_Font_PTR font,
			float x, float y, int column_width, FC_AlignEnum align, 
			[In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))]
			string formatted_text, __arglist);
		
		// Setters
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void  FC_SetSpacing(FC_Font_PTR font, int LetterSpacing);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void  FC_SetLineSpacing(FC_Font_PTR font, int LineSpacing);
		
		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void  FC_SetDefaultColor(FC_Font_PTR font, SDL.SDL_Color color);

	}
}
