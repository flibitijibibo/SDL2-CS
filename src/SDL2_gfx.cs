#region Using Statements
using System;
using System.Runtime.InteropServices;
#endregion

namespace SDL2
{
    public static class SDL_gfx
    {
        #region SDL2# Variables

        /* Used by DllImport to load the native library. */
        private const string nativeLibName = "SDL2_gfx";

        #endregion
        
        public const double M_PI = 3.1415926535897932384626433832795;
        
        #region SDL2_gfxPrimitives.h
        
        public const uint SDL2_GFXPRIMITIVES_MAJOR = 1;
        public const uint SDL2_GFXPRIMITIVES_MINOR = 0;
        public const uint SDL2_GFXPRIMITIVES_MICRO = 1;

        [DllImport(nativeLibName, EntryPoint = "pixelColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PixelColor(IntPtr renderer, short x, short y, uint color);

        [DllImport(nativeLibName, EntryPoint = "pixelRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PixelRGBA(IntPtr renderer, short x, short y, byte r, byte g, byte b, byte a);

        [DllImport(nativeLibName, EntryPoint = "hlineColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int HLineColor(IntPtr renderer, short x1, short x2, short y, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "hlineRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int HLineRGBA(IntPtr renderer, short x1, short x2, short y, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "vlineColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int VLineColor(IntPtr renderer, short x, short y1, short y2, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "vlineRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int VLineRGBA(IntPtr renderer, short x, short y1, short y2, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "rectangleColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RectangleColor(IntPtr renderer, short x1, short y1, short x2, short y2, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "rectangleRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RectangleRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, byte r, byte g, byte b, byte a);

        [DllImport(nativeLibName, EntryPoint = "roundedRectangleColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RoundedRectangleColor(IntPtr renderer, short x1, short y1, short x2, short y2, short rad, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "roundedRectangleRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RoundedRectangleRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, short rad, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "boxColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BoxColor(IntPtr renderer, short x1, short y1, short x2, short y2, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "boxRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BoxRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "roundedBoxColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RoundedBoxColor(IntPtr renderer, short x1, short y1, short x2, short y2, short rad, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "roundedBoxRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RoundedBoxRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, short rad, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "lineColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LineColor(IntPtr renderer, short x1, short y1, short x2, short y2, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "lineRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LineRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "aalineColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AALineColor(IntPtr renderer, short x1, short y1, short x2, short y2, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "aalineRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AALineRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "thickLineColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ThickLineColor(IntPtr renderer, short x1, short y1, short x2, short y2, byte width, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "thickLineRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ThickLineRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, byte width, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "circleColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CircleColor(IntPtr renderer, short x, short y, short rad, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "circleRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CircleRGBA(IntPtr renderer, short x, short y, short rad, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "arcColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ArcColor(IntPtr renderer, short x, short y, short rad, short start, short end, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "arcRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ArcRGBA(IntPtr renderer, short x, short y, short rad, short start, short end, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "aacircleColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AACircleColor(IntPtr renderer, short x, short y, short rad, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "aacircleRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AACircleRGBA(IntPtr renderer, short x, short y, short rad, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "filledCircleColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledCircleColor(IntPtr renderer, short x, short y, short rad, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "filledCircleRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledCircleRGBA(IntPtr renderer, short x, short y, short rad, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "ellipseColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int EllipseColor(IntPtr renderer, short x, short y, short rx, short ry, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "ellipseRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int EllipseRGBA(IntPtr renderer, short x, short y, short rx, short ry, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "aaellipseColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AAEllipseColor(IntPtr renderer, short x, short y, short rx, short ry, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "aaellipseRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AAEllipseRGBA(IntPtr renderer, short x, short y, short rx, short ry, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "filledEllipseColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledEllipseColor(IntPtr renderer, short x, short y, short rx, short ry, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "filledEllipseRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledEllipseRGBA(IntPtr renderer, short x, short y, short rx, short ry, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "pieColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PieColor(IntPtr renderer, short x, short y, short rad, short start, short end, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "pieRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PieRGBA(IntPtr renderer, short x, short y, short rad, short start, short end, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "filledPieColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledPieColor(IntPtr renderer, short x, short y, short rad, short start, short end, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "filledPieRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledPieRGBA(IntPtr renderer, short x, short y, short rad, short start, short end, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "trigonColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TrigonColor(IntPtr renderer, short x1, short y1, short x2, short y2, short x3, short y3, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "trigonRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TrigonRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, short x3, short y3, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "aatrigonColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AATrigonColor(IntPtr renderer, short x1, short y1, short x2, short y2, short x3, short y3, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "aatrigonRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AATrigonRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, short x3, short y3, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "filledTrigonColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledTrigonColor(IntPtr renderer, short x1, short y1, short x2, short y2, short x3, short y3, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "filledTrigonRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledTrigonRGBA(IntPtr renderer, short x1, short y1, short x2, short y2, short x3, short y3, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "polygonColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PolygonColor(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "polygonRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PolygonRGBA(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "aapolygonColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AAPolygonColor(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "aapolygonRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AAPolygonRGBA(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "filledPolygonColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledPolygonColor(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "filledPolygonRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FilledPolygonRGBA(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "texturedPolygon", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TexturedPolygon(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, IntPtr texture, int texture_dx, int texture_dy);
        
        [DllImport(nativeLibName, EntryPoint = "bezierColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BezierColor(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, int s, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "BezierRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BezierRGBA(IntPtr renderer, [In] short[] vx, [In] short[] vy, int n, int s, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "gfxPrimitivesSetFont", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GfxPrimitivesSetFont([In] byte[] fontdata, uint cw, uint ch);
        
        [DllImport(nativeLibName, EntryPoint = "gfxPrimitivesSetFontRotation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GfxPrimitivesSetFontRotation(uint rotation);
        
        [DllImport(nativeLibName, EntryPoint = "characterColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CharacterColor(IntPtr renderer, short x, short y, char c, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "characterRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CharacterRGBA(IntPtr renderer, short x, short y, char c, byte r, byte g, byte b, byte a);
        
        [DllImport(nativeLibName, EntryPoint = "stringColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StringColor(IntPtr renderer, short x, short y, string s, uint color);
        
        [DllImport(nativeLibName, EntryPoint = "stringRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StringRGBA(IntPtr renderer, short x, short y, string s, byte r, byte g, byte b, byte a);

        #endregion

        #region SDL2_rotozoom.h

        public const int SMOOTHING_OFF = 0;
        public const int SMOOTHING_ON = 1;
        
        [DllImport(nativeLibName, EntryPoint = "rotozoomSurface", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr RotozoomSurface(IntPtr src, double angle, double zoom, int smooth);
        
        [DllImport(nativeLibName, EntryPoint = "rotozoomSurfaceXY", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr RotozoomSurfaceXY(IntPtr src, double angle, double zoomx, double zoomy, int smooth);
        
        [DllImport(nativeLibName, EntryPoint = "rotozoomSurfaceSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RotozoomSurfaceSize(int width, int height, double angle, double zoom, out int dstwidth, out int dstheight);
        
        [DllImport(nativeLibName, EntryPoint = "rotozoomSurfaceSizeXY", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RotozoomSurfaceSizeXY(int width, int height, double angle, double zoomx, double zoomy, out int dstwidth, out int dstheight);
        
        [DllImport(nativeLibName, EntryPoint = "zoomSurface", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ZoomSurface(IntPtr src, double zoomx, double zoomy, int smooth);
        
        [DllImport(nativeLibName, EntryPoint = "zoomSurfaceSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZoomSurfaceSize(int width, int height, double zoomx, double zoomy, out int dstwidth, out int dstheight);
        
        [DllImport(nativeLibName, EntryPoint = "shrinkSurface", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ShrinkSurface(IntPtr src, int factorx, int factory);
        
        [DllImport(nativeLibName, EntryPoint = "rotateSurface90Degrees", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr RotateSurface90Degrees(IntPtr src, int numClockwiseTurns);

        #endregion

        #region SDL2_framerate.h

        public const int FPS_UPPER_LIMIT = 200;
        public const int FPS_LOWER_LIMIT = 1;
        public const int FPS_DEFAULT = 30;
        
        [StructLayout(LayoutKind.Sequential)]
        public struct FPSmanager
        {
            public uint framecount;
            public float rateticks;
            public uint baseticks;
            public uint lastticks;
            public uint rate;
        }
        
        [DllImport(nativeLibName, EntryPoint = "SDL_initFramerate", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_initFramerate(ref FPSmanager manager);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_setFramerate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_setFramerate(ref FPSmanager manager, uint rate);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_getFramerate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_getFramerate(ref FPSmanager manager);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_getFramecount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_getFramecount(ref FPSmanager manager);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_framerateDelay", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_framerateDelay(ref FPSmanager manager);

        #endregion

        #region SDL2_imageFilter.h

        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMMXdetect", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterMMXdetect();
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMMXoff", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_imageFilterMMXoff();
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMMXon", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_imageFilterMMXon();

        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterAdd", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterAdd([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMean", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterMean([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterSub", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterSub([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterAbsDiff", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterAbsDiff([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMult", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterMult([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMultNor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterMultNor([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMultDivby2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterMultDivby2([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMultDivby4", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterMultDivby4([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterBitAnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterBitAnd([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterBitOr", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterBitOr([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterDiv", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterDiv([In] byte[] src1, [In] byte[] src2, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterBitNegation", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterBitNegation([In] byte[] src1, [Out] byte[] dest, uint length);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterAddByte", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterAddByte([In] byte[] src1, [Out] byte[] dest, uint length, byte c);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterAddUint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterAddUInt([In] byte[] src1, [Out] byte[] dest, uint length, uint c);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterAddByteToHalf", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterAddByteToHalf([In] byte[] src1, [Out] byte[] dest, uint length, byte c);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterSubByte", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterSubByte([In] byte[] src1, [Out] byte[] dest, uint length, byte c);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterSubUint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterSubUInt([In] byte[] src1, [Out] byte[] dest, uint length, uint c);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterShiftRight", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterShiftRight([In] byte[] src1, [Out] byte[] dest, uint length, byte n);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterShiftRightUint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterShiftRightUInt([In] byte[] src1, [Out] byte[] dest, uint length, byte n);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterMultByByte", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterMultByByte([In] byte[] src1, [Out] byte[] dest, uint length, byte c);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterShiftRightAndMultByByte", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterShiftRightAndMultByByte([In] byte[] src1, [Out] byte[] dest, uint length, byte n, byte c);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterShiftLeftByte", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterShiftLeftByte([In] byte[] src1, [Out] byte[] dest, uint length, byte n);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterShiftLeftUint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterShiftLeftUInt([In] byte[] src1, [Out] byte[] dest, uint length, byte n);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterShiftLeft", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterShiftLeft([In] byte[] src1, [Out] byte[] dest, uint length, byte n);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterBinarizeUsingThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterBinarizeUsingThreshold([In] byte[] src1, [Out] byte[] dest, uint length, byte t);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterClipToRange", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterClipToRange([In] byte[] src1, [Out] byte[] dest, uint length, byte tmin, byte tmax);
        
        [DllImport(nativeLibName, EntryPoint = "SDL_imageFilterNormalizeLinear", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_imageFilterNormalizeLinear([In] byte[] src1, [Out] byte[] dest, uint length, int cmin, int cmax, int nmin, int nmax);

        #endregion
    }
}