using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace LunarGrabber
{
	// Token: 0x02000016 RID: 22
	public class LoginForm : MetroForm
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00005559 File Offset: 0x00003759
		public LoginForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005568 File Offset: 0x00003768
		private void loginButton_Click(object sender, EventArgs e)
		{
			if (API.Login(this.usernameTextBox.Text, this.passwordTextBox.Text))
			{
				API.Log(this.usernameTextBox.Text, "Login");
				Program.SetValue("Username", this.usernameTextBox.Text);
				Program.SetValue("Password", this.passwordTextBox.Text);
				MessageBox.Show("Succesfully logged in!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				base.Hide();
				new MainForm().Show();
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000055F4 File Offset: 0x000037F4
		private void LoginForm_Load(object sender, EventArgs e)
		{
			if (Program.GetValue("Color") != null)
			{
				this.components.SetStyle(this, Convert.ToInt32(Program.GetValue("Color")) + 1);
			}
			else
			{
				this.components.SetStyle(this, this.styleManager.Style);
			}
			if (Program.GetValue("Username") != null && Program.GetValue("Password") != null)
			{
				base.ShowInTaskbar = false;
				base.Visible = false;
				base.Shown += this.LoginForm_Shown;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000567C File Offset: 0x0000387C
		private void LoginForm_Shown(object sender, EventArgs e)
		{
			if (Program.GetValue("Username") != null && Program.GetValue("Password") != null && API.Login(Program.GetValue("Username"), Program.GetValue("Password")))
			{
				API.Log(Program.GetValue("Username"), "Login");
				base.Hide();
				new MainForm().Show();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000056E0 File Offset: 0x000038E0
		private void registerLabel_Click(object sender, EventArgs e)
		{
			base.Hide();
			new RegisterForm().Show();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000026EB File Offset: 0x000008EB
		private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(1);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000056F2 File Offset: 0x000038F2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005714 File Offset: 0x00003914
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LoginForm));
			this.usernameTextBox = new MetroTextBox();
			this.loginButton = new MetroButton();
			this.styleManager = new MetroStyleManager(this.components);
			this.passwordTextBox = new MetroTextBox();
			this.mainTabControl = new MetroTabControl();
			this.metroTabPage1 = new MetroTabPage();
			this.lblRegister = new MetroLabel();
			this.styleManager.BeginInit();
			this.mainTabControl.SuspendLayout();
			this.metroTabPage1.SuspendLayout();
			base.SuspendLayout();
			this.usernameTextBox.CustomButton.Image = null;
			this.usernameTextBox.CustomButton.Location = new Point(313, 2);
			this.usernameTextBox.CustomButton.Name = "";
			this.usernameTextBox.CustomButton.Size = new Size(17, 17);
			this.usernameTextBox.CustomButton.Style = 4;
			this.usernameTextBox.CustomButton.TabIndex = 1;
			this.usernameTextBox.CustomButton.Theme = 1;
			this.usernameTextBox.CustomButton.UseSelectable = true;
			this.usernameTextBox.CustomButton.Visible = false;
			this.usernameTextBox.ForeColor = Color.White;
			this.usernameTextBox.Lines = new string[0];
			this.usernameTextBox.Location = new Point(19, 18);
			this.usernameTextBox.MaxLength = 32767;
			this.usernameTextBox.Name = "usernameTextBox";
			this.usernameTextBox.PasswordChar = '\0';
			this.usernameTextBox.PromptText = "Username";
			this.usernameTextBox.ScrollBars = ScrollBars.None;
			this.usernameTextBox.SelectedText = "";
			this.usernameTextBox.SelectionLength = 0;
			this.usernameTextBox.SelectionStart = 0;
			this.usernameTextBox.ShortcutsEnabled = true;
			this.usernameTextBox.Size = new Size(333, 22);
			this.usernameTextBox.TabIndex = 3;
			this.usernameTextBox.Theme = 2;
			this.usernameTextBox.UseSelectable = true;
			this.usernameTextBox.WaterMark = "Username";
			this.usernameTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.usernameTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.loginButton.Highlight = true;
			this.loginButton.Location = new Point(19, 74);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new Size(333, 26);
			this.loginButton.TabIndex = 20;
			this.loginButton.Text = "Login";
			this.loginButton.Theme = 2;
			this.loginButton.UseSelectable = true;
			this.loginButton.Click += this.loginButton_Click;
			this.styleManager.Owner = null;
			this.styleManager.Style = 13;
			this.passwordTextBox.CustomButton.Image = null;
			this.passwordTextBox.CustomButton.Location = new Point(313, 2);
			this.passwordTextBox.CustomButton.Name = "";
			this.passwordTextBox.CustomButton.Size = new Size(17, 17);
			this.passwordTextBox.CustomButton.Style = 4;
			this.passwordTextBox.CustomButton.TabIndex = 1;
			this.passwordTextBox.CustomButton.Theme = 1;
			this.passwordTextBox.CustomButton.UseSelectable = true;
			this.passwordTextBox.CustomButton.Visible = false;
			this.passwordTextBox.ForeColor = Color.White;
			this.passwordTextBox.Lines = new string[0];
			this.passwordTextBox.Location = new Point(19, 46);
			this.passwordTextBox.MaxLength = 32767;
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '●';
			this.passwordTextBox.PromptText = "Password";
			this.passwordTextBox.ScrollBars = ScrollBars.None;
			this.passwordTextBox.SelectedText = "";
			this.passwordTextBox.SelectionLength = 0;
			this.passwordTextBox.SelectionStart = 0;
			this.passwordTextBox.ShortcutsEnabled = true;
			this.passwordTextBox.Size = new Size(333, 22);
			this.passwordTextBox.TabIndex = 21;
			this.passwordTextBox.Theme = 2;
			this.passwordTextBox.UseSelectable = true;
			this.passwordTextBox.UseSystemPasswordChar = true;
			this.passwordTextBox.WaterMark = "Password";
			this.passwordTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.passwordTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.mainTabControl.Controls.Add(this.metroTabPage1);
			this.mainTabControl.Location = new Point(23, 63);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new Size(380, 173);
			this.mainTabControl.SizeMode = TabSizeMode.Fixed;
			this.mainTabControl.TabIndex = 22;
			this.mainTabControl.Theme = 2;
			this.mainTabControl.UseSelectable = true;
			this.metroTabPage1.BorderStyle = BorderStyle.FixedSingle;
			this.metroTabPage1.Controls.Add(this.lblRegister);
			this.metroTabPage1.Controls.Add(this.passwordTextBox);
			this.metroTabPage1.Controls.Add(this.loginButton);
			this.metroTabPage1.Controls.Add(this.usernameTextBox);
			this.metroTabPage1.HorizontalScrollbarBarColor = true;
			this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
			this.metroTabPage1.HorizontalScrollbarSize = 10;
			this.metroTabPage1.Location = new Point(4, 38);
			this.metroTabPage1.Name = "metroTabPage1";
			this.metroTabPage1.Size = new Size(372, 131);
			this.metroTabPage1.TabIndex = 0;
			this.metroTabPage1.Text = "Login";
			this.metroTabPage1.Theme = 2;
			this.metroTabPage1.VerticalScrollbarBarColor = true;
			this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
			this.metroTabPage1.VerticalScrollbarSize = 10;
			this.lblRegister.AutoSize = true;
			this.lblRegister.FontWeight = 1;
			this.lblRegister.ForeColor = Color.Blue;
			this.lblRegister.Location = new Point(108, 104);
			this.lblRegister.Name = "lblRegister";
			this.lblRegister.Size = new Size(153, 19);
			this.lblRegister.TabIndex = 22;
			this.lblRegister.Text = "Don't have an account?";
			this.lblRegister.Theme = 2;
			this.lblRegister.UseStyleColors = true;
			this.lblRegister.Click += this.registerLabel_Click;
			base.AutoScaleMode = AutoScaleMode.None;
			base.ClientSize = new Size(420, 257);
			base.Controls.Add(this.mainTabControl);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "LoginForm";
			base.Resizable = false;
			base.ShadowType = 0;
			base.Style = 0;
			this.Text = "Asteroid LLC";
			base.Theme = 2;
			base.FormClosing += this.LoginForm_FormClosing;
			base.Load += this.LoginForm_Load;
			this.styleManager.EndInit();
			this.mainTabControl.ResumeLayout(false);
			this.metroTabPage1.ResumeLayout(false);
			this.metroTabPage1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400004D RID: 77
		private IContainer components;

		// Token: 0x0400004E RID: 78
		private MetroTextBox usernameTextBox;

		// Token: 0x0400004F RID: 79
		private MetroButton loginButton;

		// Token: 0x04000050 RID: 80
		private MetroStyleManager styleManager;

		// Token: 0x04000051 RID: 81
		private MetroTextBox passwordTextBox;

		// Token: 0x04000052 RID: 82
		private MetroTabControl mainTabControl;

		// Token: 0x04000053 RID: 83
		private MetroTabPage metroTabPage1;

		// Token: 0x04000054 RID: 84
		private MetroLabel lblRegister;
	}
}
