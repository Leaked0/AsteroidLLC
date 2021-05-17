using System;
using System.Linq;
using System.Security.Principal;

namespace LunarGrabber
{
	// Token: 0x02000009 RID: 9
	internal class Constants
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000320E File Offset: 0x0000140E
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00003215 File Offset: 0x00001415
		public static string Token { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000321D File Offset: 0x0000141D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00003224 File Offset: 0x00001424
		public static string Date { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000322C File Offset: 0x0000142C
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00003233 File Offset: 0x00001433
		public static string APIENCRYPTKEY { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000323B File Offset: 0x0000143B
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00003242 File Offset: 0x00001442
		public static string APIENCRYPTSALT { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x0000324A File Offset: 0x0000144A
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
			select s[Constants.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003285 File Offset: 0x00001485
		public static string HWID()
		{
			return WindowsIdentity.GetCurrent().User.Value;
		}

		// Token: 0x0400001E RID: 30
		public static bool Breached = false;

		// Token: 0x0400001F RID: 31
		public static bool Started = false;

		// Token: 0x04000020 RID: 32
		public static string IV = null;

		// Token: 0x04000021 RID: 33
		public static string Key = null;

		// Token: 0x04000022 RID: 34
		public static string ApiUrl = "https://api.auth.gg/csharp/";

		// Token: 0x04000023 RID: 35
		public static bool Initialized = false;

		// Token: 0x04000024 RID: 36
		public static Random random = new Random();
	}
}
