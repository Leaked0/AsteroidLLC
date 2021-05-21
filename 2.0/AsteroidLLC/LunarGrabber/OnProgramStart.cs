using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LunarGrabber
{
	// Token: 0x0200000D RID: 13
	internal class OnProgramStart
	{
		// Token: 0x0600005F RID: 95 RVA: 0x0000341C File Offset: 0x0000161C
		public static void Initialize(string name, string aid, string secret, string version)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(aid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
			{
				MessageBox.Show("Invalid application information!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
			OnProgramStart.AID = aid;
			OnProgramStart.Secret = secret;
			OnProgramStart.Version = version;
			OnProgramStart.Name = name;
			string[] array = new string[0];
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.Proxy = null;
					Security.Start();
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString());
					nameValueCollection["aid"] = Encryption.APIService(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("start");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						Process.GetCurrentProcess().Kill();
					}
					if (Security.MaliciousCheck(array[1]))
					{
						MessageBox.Show("Possible malicious activity detected!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					if (Constants.Breached)
					{
						MessageBox.Show("Possible malicious activity detected!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					string text = array[2];
					if (text != null)
					{
						if (!(text == "success"))
						{
							if (text == "binderror")
							{
								MessageBox.Show(Encryption.Decode("RmFpbGVkIHRvIGJpbmQgdG8gc2VydmVyLCBjaGVjayB5b3VyIEFJRCAmIFNlY3JldCBpbiB5b3VyIGNvZGUh"), OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
								Process.GetCurrentProcess().Kill();
								return;
							}
							if (text == "banned")
							{
								MessageBox.Show("This application has been banned for violating the TOS" + Environment.NewLine + "Contact us at support@auth.gg", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
								Process.GetCurrentProcess().Kill();
								return;
							}
						}
						else
						{
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
								MessageBox.Show("Application is in Developer Mode, bypassing integrity and update check!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								File.Create(Environment.CurrentDirectory + "/integrity.log").Close();
								string contents = Security.Integrity(Process.GetCurrentProcess().MainModule.FileName);
								File.WriteAllText(Environment.CurrentDirectory + "/integrity.log", contents);
								MessageBox.Show("Your applications hash has been saved to integrity.txt, please refer to this when your application is ready for release!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								if (array[12] == "Enabled" && ApplicationSettings.Hash != Security.Integrity(Process.GetCurrentProcess().MainModule.FileName))
								{
									MessageBox.Show("File has been tampered with, couldn't verify integrity!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
									Process.GetCurrentProcess().Kill();
								}
								if (ApplicationSettings.Version != OnProgramStart.Version)
								{
									MessageBox.Show("Update " + ApplicationSettings.Version + " available, download from the discord server!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
									Process.GetCurrentProcess().Kill();
								}
							}
							if (!ApplicationSettings.Status)
							{
								MessageBox.Show("Looks like this application is disabled, please try again later!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
								Process.GetCurrentProcess().Kill();
							}
						}
					}
					Security.End();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x0400003B RID: 59
		public static string AID;

		// Token: 0x0400003C RID: 60
		public static string Secret;

		// Token: 0x0400003D RID: 61
		public static string Version;

		// Token: 0x0400003E RID: 62
		public static string Name;

		// Token: 0x0400003F RID: 63
		public static string Salt;
	}
}
