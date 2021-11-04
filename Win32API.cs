using System;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
	public class Win32API
	{
		[DllImport("Kernel32.dll")]
		public static extern bool SetLocalTime(ref SystemTime Time);

		[DllImport("Kernel32.dll")]
		public static extern void GetLocalTime(ref SystemTime Time);
	}
}
