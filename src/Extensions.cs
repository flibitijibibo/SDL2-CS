#region License
/* SDL2# - C# Wrapper for SDL2
 *
 * Copyright (c) 2013-2016 Ethan Lee.
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
using System.Drawing;
using System.Runtime.InteropServices;
using SDL2;
using static SDL2.SDL;
#endregion

namespace SDL2
{
	public static class SDL2Extensions
	{
		public static bool ToBool(
			this SDL_bool @this)
		{
			return (@this == SDL_bool.SDL_TRUE);
		}

		public static SDL_bool ToSDLBool(
			this bool @this)
		{
			return @this ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE;
		}

		public static SDL_Point ToSDLPoint(
			this Point point)
		{
			SDL_Point p = new SDL_Point();

			p.x = point.X;
			p.y = point.Y;

			return p;
		}

		public static Point ToPoint(
			this SDL_Point point)
		{
			return new Point(point.x, point.y);
		}

		public static SDL_FPoint ToSDLFPoint(
			this PointF point)
		{
			SDL_FPoint p = new SDL_FPoint();

			p.x = point.X;
			p.y = point.Y;

			return p;
		}

		public static PointF ToPointF(
			this SDL_FPoint point)
		{
			return new PointF(point.x, point.y);
		}

		public static SDL_Rect ToSDLRect(
			this Rectangle rect)
		{
			SDL_Rect r = new SDL_Rect();

			r.x = rect.X;
			r.y = rect.Y;
			r.w = rect.Width;
			r.h = rect.Height;

			return r;
		}

		public static Rectangle ToRectangle(
			this SDL_Rect rect)
		{
			return new Rectangle(rect.x, rect.y, rect.w, rect.h);
		}

		public static SDL_FRect ToSDLFRect(
			this RectangleF rect)
		{
			SDL_FRect r = new SDL_FRect();

			r.x = rect.X;
			r.y = rect.Y;
			r.w = rect.Width;
			r.h = rect.Height;

			return r;
		}

		public static RectangleF ToRectangleF(
			this SDL_FRect rect)
		{
			return new RectangleF(rect.x, rect.y, rect.w, rect.h);
		}

		public static Color ToColor(
			this SDL_Color color)
		{
			return Color.FromArgb(color.a, color.r, color.g, color.b);
		}

		public static SDL_Color ToSDLColor(
			this Color color)
		{
			SDL_Color result = new SDL_Color();

			result.r = color.R;
			result.g = color.G;
			result.b = color.B;
			result.a = color.A;

			return result;
		}

		public static uint ToSDLPixel(
			this Color color,
			IntPtr surface)
		{
			uint pixel = 0;

#if UNSAFE
			unsafe
			{
				pixel = 
					SDL_MapRGBA(
						((SDL_Surface*)surface.ToPointer())->format,
						color.R,
						color.G,
						color.B,
						color.A);
			}
#else
			SDL_Surface surf = new SDL_Surface();
			Marshal.PtrToStructure(surface, surf);
			pixel = color.ToSDLPixel(ref surf);
#endif

			return pixel;
		}

		public static uint ToSDLPixel(
			this Color color,
			ref SDL_Surface surface)
		{
			return SDL_MapRGBA(
						surface.format,
						color.R,
						color.G,
						color.B,
						color.A);
		}

		public static Color ToColorFromSDLPixel(
			this uint pixel,
			IntPtr surface)
		{
			Color result = Color.Black;

#if UNSAFE
			unsafe
			{
				SDL_GetRGBA(
					pixel,
					((SDL_Surface*)surface.ToPointer())->format,
					out byte r,
					out byte g,
					out byte b,
					out byte a);

				result = Color.FromArgb(a, r, g, b);
			}
#else
			SDL_Surface surf = new SDL_Surface();
			Marshal.PtrToStructure(surface, surf);
			result = pixel.ToColorFromSDLPixel(ref surf);
#endif

			return result;
		}

		public static Color ToColorFromSDLPixel(
			this uint pixel,
			ref SDL_Surface surface)
		{
			SDL_GetRGBA(
				pixel,
				surface.format,
				out byte r,
				out byte g,
				out byte b,
				out byte a);

			return Color.FromArgb(a, r, g, b);
		}
	}
}
