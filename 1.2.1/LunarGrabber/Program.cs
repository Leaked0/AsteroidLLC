using System;
using System.Configuration;
using System.Windows.Forms;

namespace LunarGrabber
{
	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			OnProgramStart.Initialize("Asteroid", "79248", "3SHd65TViYwPjBc2CrJEo1BeRP2f4kuI46F", "1.2");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run((Form)(object)new LoginForm());
		}

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

		public static string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}
