using System;
using SDL2;

namespace SDL2
{
	
	public class NFont
	{
		const int TTF_STYLE_OUTLINE = 16;
		
		public enum AlignEnum {LEFT, CENTER, RIGHT};
		
		public class Scale
		{
			public float x;
			public float y;
			
			public enum ScaleTypeEnum {NEAREST};
			public ScaleTypeEnum type;
			
			public Scale() { x = 1.0f; y = 1.0f; type = ScaleTypeEnum.NEAREST; }
			public Scale(float xy) { x = xy; y = xy; type = ScaleTypeEnum.NEAREST; }
			public Scale(float xy, ScaleTypeEnum type) { x = xy; y = xy; this.type = type; }
			public Scale(float x, float y) { this.x = x; this.y = y; this.type = ScaleTypeEnum.NEAREST; }
			public Scale(float x, float y, ScaleTypeEnum type) { this.x = x; this.y = y; this.type = type; }
		}
		
		public class Effect
		{
			public AlignEnum alignment;
			public Scale scale;
			public bool use_color;
			public SDL.SDL_Color color;
			
			public Effect() { alignment = AlignEnum.LEFT; use_color = false; color = new SDL.SDL_Color(){r = 255, g = 255, b = 255, a = 255}; }
			
			public Effect(ref Scale scale) { alignment = AlignEnum.LEFT; this.scale = scale; use_color = false; color =  new SDL.SDL_Color(){r = 255, g = 255, b = 255, a = 255}; }
			
			public Effect(AlignEnum alignment) { this.alignment = alignment; use_color = false; color = new SDL.SDL_Color(){r = 255, g = 255, b = 255, a = 255}; }
			
			public Effect(ref SDL.SDL_Color color) { alignment = AlignEnum.LEFT; use_color = true; this.color = color; }
			
			public Effect(AlignEnum alignment, ref Scale scale) 
			{ this.alignment = alignment; this.scale = scale; use_color = false; color = new SDL.SDL_Color(){r = 255, g = 255, b = 255, a = 255}; }
			
			public Effect(AlignEnum alignment, ref SDL.SDL_Color color) { this.alignment = alignment; use_color = true; this.color = color; }
			
			public Effect(ref Scale scale, ref SDL.SDL_Color color) { alignment = AlignEnum.LEFT; this.scale = scale; use_color = true; this.color = color; }
			
			public Effect(AlignEnum alignment, ref Scale scale, ref SDL.SDL_Color color)
			{ this.alignment = alignment; this.scale = scale; use_color = true; this.color = color; }
		}
		
		
		// Constructors
		public NFont() { init(); }
		
		public NFont(ref NFont font)
		{
			init();
			// FIXME: Duplicate font data
		}
		
		public NFont(IntPtr ttf)
		{
			init();
			load(ttf, SDL_FontCache.FC_GetDefaultColor(font));
		}
		
		public NFont(IntPtr ttf, ref SDL.SDL_Color color)
		{
			init();
			load(ttf, color);
		}
		
		public NFont(string filename_ttf, UInt32 pointSize)
		{
			init();
			load(filename_ttf, pointSize);
		}
		
		public NFont(string filename_ttf, UInt32 pointSize, ref SDL.SDL_Color color, int style = SDL_ttf.TTF_STYLE_NORMAL)
		{
			init();
			load(filename_ttf, pointSize, color, style);
		}
		
		~NFont()
		{
			SDL_FontCache.FC_FreeFont(font);
		}
		
		// Loading
		public void setLoadingString(string str)
		{
			SDL_FontCache.FC_SetLoadingString(font, str);
		}
		
		public bool load(IntPtr ttf)
		{
			return load(ttf, SDL_FontCache.FC_GetDefaultColor(font));
		}
		
		public bool load(IntPtr ttf, SDL.SDL_Color color)
		{
			if (ttf == null)
				return false;
			
			SDL_FontCache.FC_ClearFont(font);
			return SDL_FontCache.FC_LoadFontFromTTF(font, ttf, color) != 0;
		}
		
		public bool load(string filename_ttf, UInt32 pointSize)
		{
			return load(filename_ttf, pointSize, new SDL.SDL_Color(){r = 0, g = 0, b = 0, a = 255});
		}
		
		public bool load(string filename_ttf, UInt32 pointSize, SDL.SDL_Color color, int style = SDL_ttf.TTF_STYLE_NORMAL)
		{
			SDL_FontCache.FC_ClearFont(font);
			return SDL_FontCache.FC_LoadFont(font, filename_ttf, (int)pointSize, color, style) != 0;
		}
		
		public void free()
		{
			SDL_FontCache.FC_ClearFont(font);
		}
	
		static SDL_FontCache.FC_AlignEnum translate_enum_NFont_to_FC(AlignEnum align)
		{
			switch(align)
			{
			case AlignEnum.LEFT:
				return SDL_FontCache.FC_AlignEnum.FC_ALIGN_LEFT;
			case AlignEnum.CENTER:
				return SDL_FontCache.FC_AlignEnum.FC_ALIGN_CENTER;
			case AlignEnum.RIGHT:
				return SDL_FontCache.FC_AlignEnum.FC_ALIGN_RIGHT;
			default:
				return SDL_FontCache.FC_AlignEnum.FC_ALIGN_LEFT;
			}
}
	
		// Drawing
		public SDL2_GPU.GPU_Rect draw(SDL2_GPU.GPU_Target_PTR dest, float x, float y, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			return SDL_FontCache.FC_Draw(font, dest, x, y, formatted_text, __arglist());
		}
		
		public SDL2_GPU.GPU_Rect draw(SDL2_GPU.GPU_Target_PTR dest, float x, float y, AlignEnum align, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			return SDL_FontCache.FC_DrawAlign(font, dest, x, y, translate_enum_NFont_to_FC(align), formatted_text, __arglist());
		}

		public SDL2_GPU.GPU_Rect draw(SDL2_GPU.GPU_Target_PTR dest, float x, float y, ref Scale scale, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			return SDL_FontCache.FC_DrawScale(font, dest, x, y, SDL_FontCache.FC_MakeScale(scale.x, scale.y), formatted_text, __arglist());
		}
		
		public SDL2_GPU.GPU_Rect draw(SDL2_GPU.GPU_Target_PTR dest, float x, float y, ref SDL.SDL_Color color, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			return SDL_FontCache.FC_DrawColor(font, dest, x, y, color, formatted_text, __arglist());
		}

		public SDL2_GPU.GPU_Rect draw(SDL2_GPU.GPU_Target_PTR dest, float x, float y, ref Effect effect, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			     
			return SDL_FontCache.FC_DrawEffect(font, dest, x, y, 
			                     SDL_FontCache.FC_MakeEffect(
			                     	translate_enum_NFont_to_FC(effect.alignment),
			                     	SDL_FontCache.FC_MakeScale(effect.scale.x, effect.scale.y),
			                     	effect.color),
			                     formatted_text, __arglist());
		}
		
		public SDL2_GPU.GPU_Rect drawBox(SDL2_GPU.GPU_Target_PTR dest, ref SDL2_GPU.GPU_Rect box, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(box.x, box.y, 0, 0);
			
			return SDL_FontCache.FC_DrawBox(font, dest, box, formatted_text, __arglist());
		}
		
		public SDL2_GPU.GPU_Rect drawBox(SDL2_GPU.GPU_Target_PTR dest, ref SDL2_GPU.GPU_Rect box, AlignEnum align, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(box.x, box.y, 0, 0);
			
			return SDL_FontCache.FC_DrawBoxAlign(font, dest, box, translate_enum_NFont_to_FC(align), formatted_text, __arglist());
		}

		public SDL2_GPU.GPU_Rect drawBox(SDL2_GPU.GPU_Target_PTR dest, ref SDL2_GPU.GPU_Rect box, ref Effect effect, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(box.x, box.y, 0, 0);
			
			return SDL_FontCache.FC_DrawBoxEffect(font, dest, box, 
			                        SDL_FontCache.FC_MakeEffect(
			                        	translate_enum_NFont_to_FC(effect.alignment),
			                        	SDL_FontCache.FC_MakeScale(effect.scale.x, effect.scale.y),
			                        	effect.color),
			                        formatted_text, __arglist());
		}
		
		public SDL2_GPU.GPU_Rect drawColumn(SDL2_GPU.GPU_Target_PTR dest, float x, float y, UInt16 width, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			return SDL_FontCache.FC_DrawColumn(font, dest, x, y, width, formatted_text, __arglist());
		}

		public SDL2_GPU.GPU_Rect drawColumn(SDL2_GPU.GPU_Target_PTR dest, float x, float y, UInt16 width, AlignEnum align, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			return SDL_FontCache.FC_DrawColumnAlign(font, dest, x, y, width, translate_enum_NFont_to_FC(align), formatted_text, __arglist());
		}
		
		public SDL2_GPU.GPU_Rect drawColumn(SDL2_GPU.GPU_Target_PTR dest, float x, float y, UInt16 width, ref Effect effect, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(x, y, 0, 0);
			
			return SDL_FontCache.FC_DrawColumnEffect(font, dest, x, y, width, 
			                           SDL_FontCache.FC_MakeEffect(
			                           	translate_enum_NFont_to_FC(effect.alignment),
			                           	SDL_FontCache.FC_MakeScale(effect.scale.x, effect.scale.y),
			                           	effect.color),
			                           formatted_text, __arglist());
		}
		
		// Getters
//		public GPU_Image_PTR getImage();
//		public IntPtr getSurface();
		
		public UInt16 getHeight()
		{
			return SDL_FontCache.FC_GetLineHeight(font);
		}
		
		public UInt16 getHeight(string formatted_text)
		{
			if(formatted_text == null)
				return 0;
			
			return SDL_FontCache.FC_GetHeight(font, formatted_text, __arglist());
		}
		
		public UInt16 getWidth(string formatted_text)
		{
			if(formatted_text == null)
				return 0;
			
			return SDL_FontCache.FC_GetWidth(font, formatted_text, __arglist());
		}

		public SDL2_GPU.GPU_Rect getCharacterOffset(UInt16 position_index, int column_width, string formatted_text)
		{
			if(formatted_text == null)
				return SDL2_GPU.GPU_MakeRect(0,0,0,0);
			
			return SDL_FontCache.FC_GetCharacterOffset(font, position_index, column_width, formatted_text, __arglist());
		}

		public UInt16 getColumnHeight(UInt16 width, string formatted_text)
		{
			if(formatted_text == null)
				return 0;
			
			return SDL_FontCache.FC_GetColumnHeight(font, width, formatted_text, __arglist());
		}
		
		public int getSpacing()
		{
			return SDL_FontCache.FC_GetSpacing(font);
		}
		
		public int getLineSpacing()
		{
			return SDL_FontCache.FC_GetLineSpacing(font);
		}
		
		public int getBaseline()
		{
			return SDL_FontCache.FC_GetBaseline(font);
		}
		
		public int getAscent()
		{
			return SDL_FontCache.FC_GetAscent(font, null, __arglist());
		}
		
		public int getAscent(char character)
		{
			return SDL_FontCache.FC_GetAscent(font, "%c", __arglist(character));
		}
		
		public int getAscent(string formatted_text)
		{
			if(formatted_text == null)
				return SDL_FontCache.FC_GetAscent(font, null, __arglist());
			
			return SDL_FontCache.FC_GetAscent(font, formatted_text, __arglist());
		}

		public int getDescent()
		{
			return SDL_FontCache.FC_GetDescent(font, null, __arglist());
		}
		
		public int getDescent(char character)
		{
			return SDL_FontCache.FC_GetDescent(font, "%c", __arglist(character));
		}
		
		public int getDescent(string formatted_text)
		{
			if(formatted_text == null)
				return SDL_FontCache.FC_GetDescent(font, null, __arglist());
			
			return SDL_FontCache.FC_GetDescent(font, formatted_text, __arglist());
		}
		
		public UInt16 getMaxWidth()
		{
			return SDL_FontCache.FC_GetMaxWidth(font);
		}
		
		public SDL.SDL_Color getDefaultColor()
		{
			return SDL_FontCache.FC_GetDefaultColor(font);
		}
		
		// Setters
		public void setSpacing(int LetterSpacing)
		{
			SDL_FontCache.FC_SetSpacing(font, LetterSpacing);
		}
		
		public void setLineSpacing(int LineSpacing)
		{
			SDL_FontCache.FC_SetLineSpacing(font, LineSpacing);
		}
		
		public void setBaseline()
		{
		}
		
		public void setBaseline(UInt16 Baseline)
		{
		}
		
		public void setDefaultColor(ref SDL.SDL_Color color)
		{
			SDL_FontCache.FC_SetDefaultColor(font, color);
		}
		
		public void enableTTFOwnership()
		{
		}
		
		private SDL_FontCache.FC_Font_PTR font;
		
		private void init()  // Common constructor
		{
			font = SDL_FontCache.FC_CreateFont();
		}
	
	}
}