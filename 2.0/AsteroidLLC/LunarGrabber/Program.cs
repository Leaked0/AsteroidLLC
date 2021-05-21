using System;
using System.Configuration;
using System.Windows.Forms;

namespace LunarGrabber
{
	// Token: 0x02000020 RID: 32
	public static class Program
	{
		// Token: 0x060000DD RID: 221 RVA: 0x0000DDCA File Offset: 0x0000BFCA
		[STAThread]
		public static void Main()
		{
			OnProgramStart.Initialize("Asteroid", "79248", "3SHd65TViYwPjBc2CrJEo1BeRP2f4kuI46F", "2.0");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new LoginForm());
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000DDFC File Offset: 0x0000BFFC
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

		// Token: 0x060000DF RID: 223 RVA: 0x0000DE58 File Offset: 0x0000C058
		public static string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}
