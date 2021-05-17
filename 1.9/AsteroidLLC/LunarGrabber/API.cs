using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LunarGrabber
{
	// Token: 0x0200000E RID: 14
	internal class API
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000038A4 File Offset: 0x00001AA4
		public static void Log(string username, string action)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(action))
			{
				MessageBox.Show("Missing log information!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["aid"] = Encryption.APIService(OnProgramStart.AID);
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["pcuser"] = Encryption.APIService(Environment.UserName);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["data"] = Encryption.APIService(action);
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("log");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					Security.End();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A74 File Offset: 0x00001C74
		public static bool AIO(string AIO)
		{
			return API.AIOLogin(AIO) || API.AIORegister(AIO);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A8C File Offset: 0x00001C8C
		public static bool AIOLogin(string AIO)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(AIO))
			{
				MessageBox.Show("Missing user login information!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Security.End();
				return false;
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["username"] = Encryption.APIService(AIO);
					nameValueCollection["password"] = Encryption.APIService(AIO);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("login");
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
						if (text == "success")
						{
							Security.End();
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text2 = array[14];
							foreach (string text3 in text2.Split(new char[]
							{
								'~'
							}))
							{
								string[] array3 = text3.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array3[0], array3[1]);
								}
								catch
								{
								}
							}
							return true;
						}
						if (text == "invalid_details")
						{
							Security.End();
							return false;
						}
						if (text == "time_expired")
						{
							MessageBox.Show("Your subscription has expired!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							Security.End();
							return false;
						}
						if (text == "hwid_updated")
						{
							MessageBox.Show("New machine has been binded, re-open the application!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							Security.End();
							return false;
						}
						if (text == "invalid_hwid")
						{
							MessageBox.Show("This user is binded to another computer, please contact support!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Security.End();
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003E9C File Offset: 0x0000209C
		public static bool AIORegister(string AIO)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(AIO))
			{
				MessageBox.Show("Invalid registering information!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Security.End();
				return false;
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["type"] = Encryption.APIService("register");
					nameValueCollection["username"] = Encryption.APIService(AIO);
					nameValueCollection["password"] = Encryption.APIService(AIO);
					nameValueCollection["email"] = Encryption.APIService(AIO);
					nameValueCollection["license"] = Encryption.APIService(AIO);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						Security.End();
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
					Security.End();
					string text = array[2];
					if (text != null)
					{
						if (text == "success")
						{
							return true;
						}
						if (text == "error")
						{
							return false;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004160 File Offset: 0x00002360
		public static bool Login(string username, string password)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Missing user login information!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Security.End();
				return false;
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["password"] = Encryption.APIService(password);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("login");
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
						if (text == "success")
						{
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text2 = array[14];
							foreach (string text3 in text2.Split(new char[]
							{
								'~'
							}))
							{
								string[] array3 = text3.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array3[0], array3[1]);
								}
								catch
								{
								}
							}
							Security.End();
							return true;
						}
						if (text == "invalid_details")
						{
							MessageBox.Show("Sorry, your username/password does not match!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
						if (text == "time_expired")
						{
							MessageBox.Show("Your subscription has expired!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							Security.End();
							return false;
						}
						if (text == "hwid_updated")
						{
							MessageBox.Show("New machine has been binded, re-open the application!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							Security.End();
							return false;
						}
						if (text == "invalid_hwid")
						{
							MessageBox.Show("This user is binded to another computer, please contact support!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Security.End();
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000458C File Offset: 0x0000278C
		public static bool Register(string username, string password, string license)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(license))
			{
				MessageBox.Show("Invalid registering information!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Security.End();
				return false;
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["type"] = Encryption.APIService("register");
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["password"] = Encryption.APIService(password);
					nameValueCollection["email"] = Encryption.APIService(username);
					nameValueCollection["license"] = Encryption.APIService(license);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						Security.End();
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
						if (text == "success")
						{
							Security.End();
							return true;
						}
						if (text == "invalid_license")
						{
							MessageBox.Show("License does not exist!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
						if (text == "email_used")
						{
							MessageBox.Show("You entered an invalid/used username!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
						if (text == "invalid_username")
						{
							MessageBox.Show("You entered an invalid/used username!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000048D0 File Offset: 0x00002AD0
		public static bool ExtendSubscription(string username, string password, string license)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(license))
			{
				MessageBox.Show("Invalid registrar information!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["type"] = Encryption.APIService("extend");
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["password"] = Encryption.APIService(password);
					nameValueCollection["license"] = Encryption.APIService(license);
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						Security.End();
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
						if (text == "success")
						{
							Security.End();
							return true;
						}
						if (text == "invalid_token")
						{
							MessageBox.Show("Token does not exist!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
						if (text == "invalid_details")
						{
							MessageBox.Show("Your user details are invalid!", ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Security.End();
							return false;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, ApplicationSettings.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}
	}
}
