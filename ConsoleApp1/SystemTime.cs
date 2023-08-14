using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public struct SystemTime
	{
		public void FromDateTime(DateTime time)
		{
			this.wYear = (ushort)time.Year;
			this.wMonth = (ushort)time.Month;
			this.wDayOfWeek = (ushort)time.DayOfWeek;
			this.wDay = (ushort)time.Day;
			this.wHour = (ushort)time.Hour;
			this.wMinute = (ushort)time.Minute;
			this.wSecond = (ushort)time.Second;
			this.wMilliseconds = (ushort)time.Millisecond;
		}

		public DateTime ToDateTime()
		{
			return new DateTime((int)this.wYear, (int)this.wMonth, (int)this.wDay, (int)this.wHour, (int)this.wMinute, (int)this.wSecond, (int)this.wMilliseconds);
		}

		public static DateTime ToDateTime(SystemTime time)
		{
			return time.ToDateTime();
		}

		public ushort wYear;

		public ushort wMonth;

		public ushort wDayOfWeek;

		public ushort wDay;

		public ushort wHour;

		public ushort wMinute;

		public ushort wSecond;

		public ushort wMilliseconds;
	}
}
