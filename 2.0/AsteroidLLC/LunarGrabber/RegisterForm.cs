using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace LunarGrabber
{
	// Token: 0x02000007 RID: 7
	public class RegisterForm : MetroForm
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000266E File Offset: 0x0000086E
		public RegisterForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000267C File Offset: 0x0000087C
		private void registerButton_Click(object sender, EventArgs e)
		{
			if (API.Register(this.usernameTextBox.Text, this.passwordTextBox.Text, this.licenseKeyTextBox.Text))
			{
				API.Log(this.usernameTextBox.Text, "Register");
				MessageBox.Show("Succesfully registered account!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026D9 File Offset: 0x000008D9
		private void goBackLabel_Click(object sender, EventArgs e)
		{
			base.Hide();
			new LoginForm().Show();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026EB File Offset: 0x000008EB
		private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(1);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026F4 File Offset: 0x000008F4
		private void RegisterForm_Load(object sender, EventArgs e)
		{
			if (Program.GetValue("Color") != null)
			{
				this.components.SetStyle(this, Convert.ToInt32(Program.GetValue("Color")) + 1);
				return;
			}
			this.components.SetStyle(this, this.styleManager.Style);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002742 File Offset: 0x00000942
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002764 File Offset: 0x00000964
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RegisterForm));
			this.passwordTextBox = new MetroTextBox();
			this.registerButton = new MetroButton();
			this.usernameTextBox = new MetroTextBox();
			this.licenseKeyTextBox = new MetroTextBox();
			this.mainTabControl = new MetroTabControl();
			this.metroTabPage1 = new MetroTabPage();
			this.goBackLabel = new MetroLabel();
			this.styleManager = new MetroStyleManager(this.components);
			this.mainTabControl.SuspendLayout();
			this.metroTabPage1.SuspendLayout();
			this.styleManager.BeginInit();
			base.SuspendLayout();
			this.passwordTextBox.CustomButton.Image = null;
			this.passwordTextBox.CustomButton.Location = new Point(312, 2);
			this.passwordTextBox.CustomButton.Name = "";
			this.passwordTextBox.CustomButton.Size = new Size(17, 17);
			this.passwordTextBox.CustomButton.Style = 4;
			this.passwordTextBox.CustomButton.TabIndex = 1;
			this.passwordTextBox.CustomButton.Theme = 1;
			this.passwordTextBox.CustomButton.UseSelectable = true;
			this.passwordTextBox.CustomButton.Visible = false;
			this.passwordTextBox.ForeColor = Color.White;
			this.passwordTextBox.Lines = new string[0];
			this.passwordTextBox.Location = new Point(19, 44);
			this.passwordTextBox.MaxLength = 32767;
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '●';
			this.passwordTextBox.PromptText = "Password";
			this.passwordTextBox.ScrollBars = ScrollBars.None;
			this.passwordTextBox.SelectedText = "";
			this.passwordTextBox.SelectionLength = 0;
			this.passwordTextBox.SelectionStart = 0;
			this.passwordTextBox.ShortcutsEnabled = true;
			this.passwordTextBox.Size = new Size(332, 22);
			this.passwordTextBox.TabIndex = 29;
			this.passwordTextBox.Theme = 2;
			this.passwordTextBox.UseSelectable = true;
			this.passwordTextBox.UseSystemPasswordChar = true;
			this.passwordTextBox.WaterMark = "Password";
			this.passwordTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.passwordTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.registerButton.Highlight = true;
			this.registerButton.Location = new Point(19, 100);
			this.registerButton.Name = "registerButton";
			this.registerButton.Size = new Size(332, 26);
			this.registerButton.TabIndex = 28;
			this.registerButton.Text = "Register";
			this.registerButton.Theme = 2;
			this.registerButton.UseSelectable = true;
			this.registerButton.Click += this.registerButton_Click;
			this.usernameTextBox.CustomButton.Image = null;
			this.usernameTextBox.CustomButton.Location = new Point(312, 2);
			this.usernameTextBox.CustomButton.Name = "";
			this.usernameTextBox.CustomButton.Size = new Size(17, 17);
			this.usernameTextBox.CustomButton.Style = 4;
			this.usernameTextBox.CustomButton.TabIndex = 1;
			this.usernameTextBox.CustomButton.Theme = 1;
			this.usernameTextBox.CustomButton.UseSelectable = true;
			this.usernameTextBox.CustomButton.Visible = false;
			this.usernameTextBox.ForeColor = Color.White;
			this.usernameTextBox.Lines = new string[0];
			this.usernameTextBox.Location = new Point(19, 16);
			this.usernameTextBox.MaxLength = 32767;
			this.usernameTextBox.Name = "usernameTextBox";
			this.usernameTextBox.PasswordChar = '\0';
			this.usernameTextBox.PromptText = "Username";
			this.usernameTextBox.ScrollBars = ScrollBars.None;
			this.usernameTextBox.SelectedText = "";
			this.usernameTextBox.SelectionLength = 0;
			this.usernameTextBox.SelectionStart = 0;
			this.usernameTextBox.ShortcutsEnabled = true;
			this.usernameTextBox.Size = new Size(332, 22);
			this.usernameTextBox.TabIndex = 26;
			this.usernameTextBox.Theme = 2;
			this.usernameTextBox.UseSelectable = true;
			this.usernameTextBox.WaterMark = "Username";
			this.usernameTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.usernameTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.licenseKeyTextBox.CustomButton.Image = null;
			this.licenseKeyTextBox.CustomButton.Location = new Point(312, 2);
			this.licenseKeyTextBox.CustomButton.Name = "";
			this.licenseKeyTextBox.CustomButton.Size = new Size(17, 17);
			this.licenseKeyTextBox.CustomButton.Style = 4;
			this.licenseKeyTextBox.CustomButton.TabIndex = 1;
			this.licenseKeyTextBox.CustomButton.Theme = 1;
			this.licenseKeyTextBox.CustomButton.UseSelectable = true;
			this.licenseKeyTextBox.CustomButton.Visible = false;
			this.licenseKeyTextBox.ForeColor = Color.White;
			this.licenseKeyTextBox.Lines = new string[0];
			this.licenseKeyTextBox.Location = new Point(19, 72);
			this.licenseKeyTextBox.MaxLength = 32767;
			this.licenseKeyTextBox.Name = "licenseKeyTextBox";
			this.licenseKeyTextBox.PasswordChar = '\0';
			this.licenseKeyTextBox.PromptText = "License Key";
			this.licenseKeyTextBox.ScrollBars = ScrollBars.None;
			this.licenseKeyTextBox.SelectedText = "";
			this.licenseKeyTextBox.SelectionLength = 0;
			this.licenseKeyTextBox.SelectionStart = 0;
			this.licenseKeyTextBox.ShortcutsEnabled = true;
			this.licenseKeyTextBox.Size = new Size(332, 22);
			this.licenseKeyTextBox.TabIndex = 27;
			this.licenseKeyTextBox.Theme = 2;
			this.licenseKeyTextBox.UseSelectable = true;
			this.licenseKeyTextBox.WaterMark = "License Key";
			this.licenseKeyTextBox.WaterMarkColor = Color.FromArgb(109, 109, 109);
			this.licenseKeyTextBox.WaterMarkFont = new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel);
			this.mainTabControl.Controls.Add(this.metroTabPage1);
			this.mainTabControl.Location = new Point(23, 63);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new Size(378, 202);
			this.mainTabControl.SizeMode = TabSizeMode.Fixed;
			this.mainTabControl.TabIndex = 30;
			this.mainTabControl.Theme = 2;
			this.mainTabControl.UseSelectable = true;
			this.metroTabPage1.BorderStyle = BorderStyle.FixedSingle;
			this.metroTabPage1.Controls.Add(this.goBackLabel);
			this.metroTabPage1.Controls.Add(this.passwordTextBox);
			this.metroTabPage1.Controls.Add(this.registerButton);
			this.metroTabPage1.Controls.Add(this.usernameTextBox);
			this.metroTabPage1.Controls.Add(this.licenseKeyTextBox);
			this.metroTabPage1.HorizontalScrollbarBarColor = true;
			this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
			this.metroTabPage1.HorizontalScrollbarSize = 10;
			this.metroTabPage1.Location = new Point(4, 38);
			this.metroTabPage1.Name = "metroTabPage1";
			this.metroTabPage1.Size = new Size(370, 160);
			this.metroTabPage1.TabIndex = 0;
			this.metroTabPage1.Text = "Register";
			this.metroTabPage1.Theme = 2;
			this.metroTabPage1.VerticalScrollbarBarColor = true;
			this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
			this.metroTabPage1.VerticalScrollbarSize = 10;
			this.goBackLabel.AutoSize = true;
			this.goBackLabel.FontWeight = 1;
			this.goBackLabel.ForeColor = Color.Blue;
			this.goBackLabel.Location = new Point(95, 130);
			this.goBackLabel.Name = "goBackLabel";
			this.goBackLabel.Size = new Size(165, 19);
			this.goBackLabel.TabIndex = 30;
			this.goBackLabel.Text = "Already have an account?";
			this.goBackLabel.Theme = 2;
			this.goBackLabel.UseStyleColors = true;
			this.goBackLabel.Click += this.goBackLabel_Click;
			this.styleManager.Owner = null;
			this.styleManager.Style = 13;
			base.AutoScaleMode = AutoScaleMode.None;
			base.ClientSize = new Size(427, 285);
			base.Controls.Add(this.mainTabControl);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "RegisterForm";
			base.Resizable = false;
			base.Style = 0;
			this.Text = "Asteroid LLC";
			base.Theme = 2;
			base.FormClosing += this.RegisterForm_FormClosing;
			base.Load += this.RegisterForm_Load;
			this.mainTabControl.ResumeLayout(false);
			this.metroTabPage1.ResumeLayout(false);
			this.metroTabPage1.PerformLayout();
			this.styleManager.EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400000F RID: 15
		private IContainer components;

		// Token: 0x04000010 RID: 16
		private MetroTextBox passwordTextBox;

		// Token: 0x04000011 RID: 17
		private MetroButton registerButton;

		// Token: 0x04000012 RID: 18
		private MetroTextBox usernameTextBox;

		// Token: 0x04000013 RID: 19
		private MetroTextBox licenseKeyTextBox;

		// Token: 0x04000014 RID: 20
		private MetroTabControl mainTabControl;

		// Token: 0x04000015 RID: 21
		private MetroTabPage metroTabPage1;

		// Token: 0x04000016 RID: 22
		private MetroLabel goBackLabel;

		// Token: 0x04000017 RID: 23
		private MetroStyleManager styleManager;
	}
}
