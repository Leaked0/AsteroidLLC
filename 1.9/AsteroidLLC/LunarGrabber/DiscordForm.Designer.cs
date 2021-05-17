namespace LunarGrabber
{
	// Token: 0x02000004 RID: 4
	public partial class DiscordForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002264 File Offset: 0x00000464
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002284 File Offset: 0x00000484
		private void InitializeComponent()
		{
			this.webView2 = new global::Microsoft.Web.WebView2.WinForms.WebView2();
			this.webView2.BeginInit();
			base.SuspendLayout();
			this.webView2.CreationProperties = null;
			this.webView2.DefaultBackgroundColor = global::System.Drawing.Color.White;
			this.webView2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.webView2.Location = new global::System.Drawing.Point(0, 0);
			this.webView2.Name = "webView2";
			this.webView2.Size = new global::System.Drawing.Size(800, 450);
			this.webView2.TabIndex = 2;
			this.webView2.ZoomFactor = 1.0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(800, 450);
			base.Controls.Add(this.webView2);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "DiscordForm";
			base.ShowIcon = false;
			this.Text = "DiscordForm";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.DiscordForm_Load);
			this.webView2.EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000006 RID: 6
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000007 RID: 7
		private global::Microsoft.Web.WebView2.WinForms.WebView2 webView2;
	}
}
