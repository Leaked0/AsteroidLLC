using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace LunarGrabber
{
	internal class DiscordForm
	{
		private IContainer components;

		private WebView2 webView2;

		private void InitializeComponent()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			webView2 = new WebView2();
			((ISupportInitialize)webView2).BeginInit();
			webView2.set_CreationProperties((CoreWebView2CreationProperties)null);
			webView2.set_DefaultBackgroundColor(Color.White);
			((Control)(object)webView2).Dock = DockStyle.Fill;
			((Control)(object)webView2).Location = new Point(0, 0);
			((Control)(object)webView2).Name = "webView2";
			((Control)(object)webView2).Size = new Size(800, 450);
			((Control)(object)webView2).TabIndex = 2;
			webView2.set_ZoomFactor(1.0);
		}
	}
}
