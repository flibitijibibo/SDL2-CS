/* This file is a big workaround to avoid having to use OpenTK.GraphicsContext.
 * -flibit
 */

using System;

namespace OpenTK.Graphics
{
	public class GraphicsContext
	{
		public static IntPtr CurrentContext
		{
			get;
			set;
		}
		
		public static bool ErrorChecking
		{
			get;
			set;
		}
	}
}