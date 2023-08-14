using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.SynchronizeSystemTime();
            for (; ; )
            {
                Thread.Sleep(10000);
                Program.StopWindowsService("Windows Update");
            }
        }
		private static void StopWindowsService(string windowsServiceName)
		{
			try
			{
				using (ServiceController serviceController = new ServiceController(windowsServiceName))
				{
					bool flag = serviceController.Status == ServiceControllerStatus.Running;
					if (flag)
					{
						Console.WriteLine("服务停止......");
						serviceController.Stop();
						Console.WriteLine("服务已经停止......");
					}
					else
					{
						bool flag2 = serviceController.Status == ServiceControllerStatus.Stopped;
						if (flag2)
						{
							Console.WriteLine("服务已经停止......");
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private static void SynchronizeSystemTime()
		{
			try
			{
				HttpClient httpClient = new HttpClient();
				HttpResponseMessage result = httpClient.GetAsync("http://api.m.taobao.com/rest/api3.do?api=mtop.common.getTimestamp").Result;
				string result2 = result.Content.ReadAsStringAsync().Result;
				GetTimestampModel getTimestampModel = Program.JsonDeserialize<GetTimestampModel>(result2);
				string t = getTimestampModel.data.t;
				DateTime dateTime = Program.GetDateTime(long.Parse(t));
				Program.SetLocalTime(dateTime);
			}
			catch (Exception ex)
			{
			}
		}

		public static T JsonDeserialize<T>(string jsonString)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
			MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
			return (T)((object)dataContractJsonSerializer.ReadObject(stream));
		}

		private static DateTime GetDateTime(long timeStamp)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = timeStamp * 10000L;
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}

		private static void SetLocalTime(DateTime time)
		{
			SystemTime systemTime = default(SystemTime);
			systemTime.FromDateTime(time);
			Win32API.SetLocalTime(ref systemTime);
		}
	}
}
