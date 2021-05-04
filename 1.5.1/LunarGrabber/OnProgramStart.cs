using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LunarGrabber
{
	internal class OnProgramStart
	{
		public static string AID;

		public static string Secret;

		public static string Version;

		public static string Name;

		public static string Salt;

		public static void Initialize(string name, string aid, string secret, string version)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(aid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
			{
				MessageBox.Show("Invalid application information!", Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
			AID = aid;
			Secret = secret;
			Version = version;
			Name = name;
			string[] array = new string[0];
			using WebClient webClient = new WebClient();
			try
			{
				webClient.Proxy = null;
				Security.Start();
				array = Encryption.DecryptService(Encoding.Default.GetString(webClient.UploadValues(Constants.ApiUrl, new NameValueCollection
				{
					["token"] = Encryption.EncryptService(Constants.Token),
					["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
					["aid"] = Encryption.APIService(AID),
					["session_id"] = Constants.IV,
					["api_id"] = Constants.APIENCRYPTSALT,
					["api_key"] = Constants.APIENCRYPTKEY,
					["session_key"] = Constants.Key,
					["secret"] = Encryption.APIService(Secret),
					["type"] = Encryption.APIService("start")
				}))).Split("|".ToCharArray());
				if (array[0] != Constants.Token)
				{
					MessageBox.Show("Security error has been triggered!", Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
				}
				if (Security.MaliciousCheck(array[1]))
				{
					MessageBox.Show("Possible malicious activity detected!", Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					Process.GetCurrentProcess().Kill();
				}
				if (Constants.Breached)
				{
					MessageBox.Show("Possible malicious activity detected!", Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					Process.GetCurrentProcess().Kill();
				}
				switch (array[2])
				{
				case "success":
					Constants.Initialized = true;
					if (array[3] == "Enabled")
					{
						ApplicationSettings.Status = true;
					}
					if (array[4] == "Enabled")
					{
						ApplicationSettings.DeveloperMode = true;
					}
					ApplicationSettings.Hash = array[5];
					ApplicationSettings.Version = array[6];
					ApplicationSettings.Update_Link = array[7];
					if (array[8] == "Enabled")
					{
						ApplicationSettings.Freemode = true;
					}
					if (array[9] == "Enabled")
					{
						ApplicationSettings.Login = true;
					}
					ApplicationSettings.Name = array[10];
					if (array[11] == "Enabled")
					{
						ApplicationSettings.Register = true;
					}
					if (ApplicationSettings.DeveloperMode)
					{
						MessageBox.Show("Application is in Developer Mode, bypassing integrity and update check!", Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						File.Create(Environment.CurrentDirectory + "/integrity.log").Close();
						string contents = Security.Integrity(Process.GetCurrentProcess().MainModule.FileName);
						File.WriteAllText(Environment.CurrentDirectory + "/integrity.log", contents);
						MessageBox.Show("Your applications hash has been saved to integrity.txt, please refer to this when your application is ready for release!", Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						if (array[12] == "Enabled" && ApplicationSettings.Hash != Security.Integrity(Process.GetCurrentProcess().MainModule.FileName))
						{
							MessageBox.Show("File has been tampered with, couldn't verify integrity!", Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Process.GetCurrentProcess().Kill();
						}
						if (ApplicationSettings.Version != Version)
						{
							MessageBox.Show("Update " + ApplicationSettings.Version + " available, download from the discord server!", Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Process.GetCurrentProcess().Kill();
						}
					}
					if (!ApplicationSettings.Status)
					{
						MessageBox.Show("Looks like this application is disabled, please try again later!", Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						Process.GetCurrentProcess().Kill();
					}
					break;
				case "binderror":
					MessageBox.Show(Encryption.Decode("RmFpbGVkIHRvIGJpbmQgdG8gc2VydmVyLCBjaGVjayB5b3VyIEFJRCAmIFNlY3JldCBpbiB5b3VyIGNvZGUh"), Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
					return;
				case "banned":
					MessageBox.Show("This application has been banned for violating the TOS" + Environment.NewLine + "Contact us at support@auth.gg", Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
					return;
				}
				Security.End();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
		}
	}
}
