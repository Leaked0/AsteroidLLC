using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace LunarGrabber
{
	public class RegisterForm : MetroForm
	{
		private IContainer components;

		private MetroTextBox passwordTextBox;

		private MetroButton registerButton;

		private MetroTextBox usernameTextBox;

		private MetroTextBox licenseKeyTextBox;

		private MetroTabControl mainTabControl;

		private MetroTabPage metroTabPage1;

		private MetroLabel goBackLabel;

		private MetroStyleManager styleManager;

		public RegisterForm()
			: this()
		{
			InitializeComponent();
		}

		private void registerButton_Click(object sender, EventArgs e)
		{
			if (API.Register(((Control)(object)usernameTextBox).Text, ((Control)(object)passwordTextBox).Text, ((Control)(object)licenseKeyTextBox).Text))
			{
				API.Log(((Control)(object)usernameTextBox).Text, "Register");
				MessageBox.Show("Succesfully registered account!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void goBackLabel_Click(object sender, EventArgs e)
		{
			((Control)this).Hide();
			((Control)(object)new LoginForm()).Show();
		}

		private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(1);
		}

		private void RegisterForm_Load(object sender, EventArgs e)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			if (Program.GetValue("Color") != null)
			{
				components.SetStyle((MetroForm)(object)this, (MetroColorStyle)(Convert.ToInt32(Program.GetValue("Color")) + 1));
			}
			else
			{
				components.SetStyle((MetroForm)(object)this, styleManager.get_Style());
			}
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
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
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
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RegisterForm));
			passwordTextBox = new MetroTextBox();
			registerButton = new MetroButton();
			usernameTextBox = new MetroTextBox();
			licenseKeyTextBox = new MetroTextBox();
			mainTabControl = new MetroTabControl();
			metroTabPage1 = new MetroTabPage();
			goBackLabel = new MetroLabel();
			styleManager = new MetroStyleManager(components);
			((Control)(object)mainTabControl).SuspendLayout();
			((Control)(object)metroTabPage1).SuspendLayout();
			((ISupportInitialize)styleManager).BeginInit();
			((Control)this).SuspendLayout();
			passwordTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)passwordTextBox.get_CustomButton()).Location = new Point(312, 2);
			((Control)(object)passwordTextBox.get_CustomButton()).Name = "";
			((Control)(object)passwordTextBox.get_CustomButton()).Size = new Size(17, 17);
			passwordTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)passwordTextBox.get_CustomButton()).TabIndex = 1;
			passwordTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			passwordTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)passwordTextBox.get_CustomButton()).Visible = false;
			((Control)(object)passwordTextBox).ForeColor = Color.White;
			passwordTextBox.set_Lines(new string[0]);
			((Control)(object)passwordTextBox).Location = new Point(19, 44);
			passwordTextBox.set_MaxLength(32767);
			((Control)(object)passwordTextBox).Name = "passwordTextBox";
			passwordTextBox.set_PasswordChar('●');
			passwordTextBox.set_PromptText("Password");
			passwordTextBox.set_ScrollBars(ScrollBars.None);
			passwordTextBox.set_SelectedText("");
			passwordTextBox.set_SelectionLength(0);
			passwordTextBox.set_SelectionStart(0);
			passwordTextBox.set_ShortcutsEnabled(true);
			((Control)(object)passwordTextBox).Size = new Size(332, 22);
			((Control)(object)passwordTextBox).TabIndex = 29;
			passwordTextBox.set_Theme((MetroThemeStyle)2);
			passwordTextBox.set_UseSelectable(true);
			passwordTextBox.set_UseSystemPasswordChar(true);
			passwordTextBox.set_WaterMark("Password");
			passwordTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			passwordTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			registerButton.set_Highlight(true);
			((Control)(object)registerButton).Location = new Point(19, 100);
			((Control)(object)registerButton).Name = "registerButton";
			((Control)(object)registerButton).Size = new Size(332, 26);
			((Control)(object)registerButton).TabIndex = 28;
			((Control)(object)registerButton).Text = "Register";
			registerButton.set_Theme((MetroThemeStyle)2);
			registerButton.set_UseSelectable(true);
			((Control)(object)registerButton).Click += registerButton_Click;
			usernameTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)usernameTextBox.get_CustomButton()).Location = new Point(312, 2);
			((Control)(object)usernameTextBox.get_CustomButton()).Name = "";
			((Control)(object)usernameTextBox.get_CustomButton()).Size = new Size(17, 17);
			usernameTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)usernameTextBox.get_CustomButton()).TabIndex = 1;
			usernameTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			usernameTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)usernameTextBox.get_CustomButton()).Visible = false;
			((Control)(object)usernameTextBox).ForeColor = Color.White;
			usernameTextBox.set_Lines(new string[0]);
			((Control)(object)usernameTextBox).Location = new Point(19, 16);
			usernameTextBox.set_MaxLength(32767);
			((Control)(object)usernameTextBox).Name = "usernameTextBox";
			usernameTextBox.set_PasswordChar('\0');
			usernameTextBox.set_PromptText("Username");
			usernameTextBox.set_ScrollBars(ScrollBars.None);
			usernameTextBox.set_SelectedText("");
			usernameTextBox.set_SelectionLength(0);
			usernameTextBox.set_SelectionStart(0);
			usernameTextBox.set_ShortcutsEnabled(true);
			((Control)(object)usernameTextBox).Size = new Size(332, 22);
			((Control)(object)usernameTextBox).TabIndex = 26;
			usernameTextBox.set_Theme((MetroThemeStyle)2);
			usernameTextBox.set_UseSelectable(true);
			usernameTextBox.set_WaterMark("Username");
			usernameTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			usernameTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			licenseKeyTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)licenseKeyTextBox.get_CustomButton()).Location = new Point(312, 2);
			((Control)(object)licenseKeyTextBox.get_CustomButton()).Name = "";
			((Control)(object)licenseKeyTextBox.get_CustomButton()).Size = new Size(17, 17);
			licenseKeyTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)licenseKeyTextBox.get_CustomButton()).TabIndex = 1;
			licenseKeyTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			licenseKeyTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)licenseKeyTextBox.get_CustomButton()).Visible = false;
			((Control)(object)licenseKeyTextBox).ForeColor = Color.White;
			licenseKeyTextBox.set_Lines(new string[0]);
			((Control)(object)licenseKeyTextBox).Location = new Point(19, 72);
			licenseKeyTextBox.set_MaxLength(32767);
			((Control)(object)licenseKeyTextBox).Name = "licenseKeyTextBox";
			licenseKeyTextBox.set_PasswordChar('\0');
			licenseKeyTextBox.set_PromptText("License Key");
			licenseKeyTextBox.set_ScrollBars(ScrollBars.None);
			licenseKeyTextBox.set_SelectedText("");
			licenseKeyTextBox.set_SelectionLength(0);
			licenseKeyTextBox.set_SelectionStart(0);
			licenseKeyTextBox.set_ShortcutsEnabled(true);
			((Control)(object)licenseKeyTextBox).Size = new Size(332, 22);
			((Control)(object)licenseKeyTextBox).TabIndex = 27;
			licenseKeyTextBox.set_Theme((MetroThemeStyle)2);
			licenseKeyTextBox.set_UseSelectable(true);
			licenseKeyTextBox.set_WaterMark("License Key");
			licenseKeyTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			licenseKeyTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)mainTabControl).Controls.Add((Control)(object)metroTabPage1);
			((Control)(object)mainTabControl).Location = new Point(23, 63);
			((Control)(object)mainTabControl).Name = "mainTabControl";
			((TabControl)(object)mainTabControl).SelectedIndex = 0;
			((Control)(object)mainTabControl).Size = new Size(378, 202);
			((TabControl)(object)mainTabControl).SizeMode = TabSizeMode.Fixed;
			((Control)(object)mainTabControl).TabIndex = 30;
			mainTabControl.set_Theme((MetroThemeStyle)2);
			mainTabControl.set_UseSelectable(true);
			((Panel)(object)metroTabPage1).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)goBackLabel);
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)passwordTextBox);
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)registerButton);
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)usernameTextBox);
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)licenseKeyTextBox);
			metroTabPage1.set_HorizontalScrollbarBarColor(true);
			metroTabPage1.set_HorizontalScrollbarHighlightOnWheel(false);
			metroTabPage1.set_HorizontalScrollbarSize(10);
			((TabPage)(object)metroTabPage1).Location = new Point(4, 38);
			((Control)(object)metroTabPage1).Name = "metroTabPage1";
			((Control)(object)metroTabPage1).Size = new Size(370, 160);
			((TabPage)(object)metroTabPage1).TabIndex = 0;
			((Control)(object)metroTabPage1).Text = "Register";
			metroTabPage1.set_Theme((MetroThemeStyle)2);
			metroTabPage1.set_VerticalScrollbarBarColor(true);
			metroTabPage1.set_VerticalScrollbarHighlightOnWheel(false);
			metroTabPage1.set_VerticalScrollbarSize(10);
			((Control)(object)goBackLabel).AutoSize = true;
			goBackLabel.set_FontWeight((MetroLabelWeight)1);
			((Control)(object)goBackLabel).ForeColor = Color.Blue;
			((Control)(object)goBackLabel).Location = new Point(95, 130);
			((Control)(object)goBackLabel).Name = "goBackLabel";
			((Control)(object)goBackLabel).Size = new Size(165, 19);
			((Control)(object)goBackLabel).TabIndex = 30;
			((Control)(object)goBackLabel).Text = "Already have an account?";
			goBackLabel.set_Theme((MetroThemeStyle)2);
			goBackLabel.set_UseStyleColors(true);
			((Control)(object)goBackLabel).Click += goBackLabel_Click;
			styleManager.set_Owner((ContainerControl)null);
			styleManager.set_Style((MetroColorStyle)13);
			((ContainerControl)this).AutoScaleMode = AutoScaleMode.None;
			((Form)this).ClientSize = new Size(427, 285);
			((Control)this).Controls.Add((Control)(object)mainTabControl);
			((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			((Control)this).Name = "RegisterForm";
			((MetroForm)this).set_Resizable(false);
			((MetroForm)this).set_Style((MetroColorStyle)0);
			((Control)(object)this).Text = "Asteroid LLC";
			((MetroForm)this).set_Theme((MetroThemeStyle)2);
			((Form)this).FormClosing += RegisterForm_FormClosing;
			((Form)this).Load += RegisterForm_Load;
			((Control)(object)mainTabControl).ResumeLayout(performLayout: false);
			((Control)(object)metroTabPage1).ResumeLayout(performLayout: false);
			((Control)(object)metroTabPage1).PerformLayout();
			((ISupportInitialize)styleManager).EndInit();
			((Control)this).ResumeLayout(performLayout: false);
		}
	}
}
