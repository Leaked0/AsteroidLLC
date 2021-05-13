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
	public class LoginForm : MetroForm
	{
		private IContainer components;

		private MetroTextBox usernameTextBox;

		private MetroButton loginButton;

		private MetroStyleManager styleManager;

		private MetroTextBox passwordTextBox;

		private MetroTabControl mainTabControl;

		private MetroTabPage metroTabPage1;

		private MetroLabel lblRegister;

		public LoginForm()
			: this()
		{
			InitializeComponent();
		}

		private void loginButton_Click(object sender, EventArgs e)
		{
			if (API.Login(((Control)(object)usernameTextBox).Text, ((Control)(object)passwordTextBox).Text))
			{
				API.Log(((Control)(object)usernameTextBox).Text, "Login");
				Program.SetValue("Username", ((Control)(object)usernameTextBox).Text);
				Program.SetValue("Password", ((Control)(object)passwordTextBox).Text);
				MessageBox.Show("Succesfully logged in!", OnProgramStart.Name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				((Control)this).Hide();
				((Control)(object)new MainForm()).Show();
			}
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			if (Program.GetValue("Color") != null)
			{
				components.SetStyle((MetroForm)(object)this, (MetroColorStyle)(Convert.ToInt32(Program.GetValue("Color")) + 1));
			}
			else
			{
				components.SetStyle((MetroForm)(object)this, styleManager.get_Style());
			}
			if (Program.GetValue("Username") != null && Program.GetValue("Password") != null)
			{
				((Form)this).ShowInTaskbar = false;
				((Control)this).Visible = false;
				((Form)this).Shown += LoginForm_Shown;
			}
		}

		private void LoginForm_Shown(object sender, EventArgs e)
		{
			if (Program.GetValue("Username") != null && Program.GetValue("Password") != null && API.Login(Program.GetValue("Username"), Program.GetValue("Password")))
			{
				API.Log(Program.GetValue("Username"), "Login");
				((Control)this).Hide();
				((Control)(object)new MainForm()).Show();
			}
		}

		private void registerLabel_Click(object sender, EventArgs e)
		{
			((Control)this).Hide();
			((Control)(object)new RegisterForm()).Show();
		}

		private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(1);
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
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LoginForm));
			usernameTextBox = new MetroTextBox();
			loginButton = new MetroButton();
			styleManager = new MetroStyleManager(components);
			passwordTextBox = new MetroTextBox();
			mainTabControl = new MetroTabControl();
			metroTabPage1 = new MetroTabPage();
			lblRegister = new MetroLabel();
			((ISupportInitialize)styleManager).BeginInit();
			((Control)(object)mainTabControl).SuspendLayout();
			((Control)(object)metroTabPage1).SuspendLayout();
			((Control)this).SuspendLayout();
			usernameTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)usernameTextBox.get_CustomButton()).Location = new Point(313, 2);
			((Control)(object)usernameTextBox.get_CustomButton()).Name = "";
			((Control)(object)usernameTextBox.get_CustomButton()).Size = new Size(17, 17);
			usernameTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)usernameTextBox.get_CustomButton()).TabIndex = 1;
			usernameTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			usernameTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)usernameTextBox.get_CustomButton()).Visible = false;
			((Control)(object)usernameTextBox).ForeColor = Color.White;
			usernameTextBox.set_Lines(new string[0]);
			((Control)(object)usernameTextBox).Location = new Point(19, 18);
			usernameTextBox.set_MaxLength(32767);
			((Control)(object)usernameTextBox).Name = "usernameTextBox";
			usernameTextBox.set_PasswordChar('\0');
			usernameTextBox.set_PromptText("Username");
			usernameTextBox.set_ScrollBars(ScrollBars.None);
			usernameTextBox.set_SelectedText("");
			usernameTextBox.set_SelectionLength(0);
			usernameTextBox.set_SelectionStart(0);
			usernameTextBox.set_ShortcutsEnabled(true);
			((Control)(object)usernameTextBox).Size = new Size(333, 22);
			((Control)(object)usernameTextBox).TabIndex = 3;
			usernameTextBox.set_Theme((MetroThemeStyle)2);
			usernameTextBox.set_UseSelectable(true);
			usernameTextBox.set_WaterMark("Username");
			usernameTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			usernameTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			loginButton.set_Highlight(true);
			((Control)(object)loginButton).Location = new Point(19, 74);
			((Control)(object)loginButton).Name = "loginButton";
			((Control)(object)loginButton).Size = new Size(333, 26);
			((Control)(object)loginButton).TabIndex = 20;
			((Control)(object)loginButton).Text = "Login";
			loginButton.set_Theme((MetroThemeStyle)2);
			loginButton.set_UseSelectable(true);
			((Control)(object)loginButton).Click += loginButton_Click;
			styleManager.set_Owner((ContainerControl)null);
			styleManager.set_Style((MetroColorStyle)13);
			passwordTextBox.get_CustomButton().set_Image((Image)null);
			((Control)(object)passwordTextBox.get_CustomButton()).Location = new Point(313, 2);
			((Control)(object)passwordTextBox.get_CustomButton()).Name = "";
			((Control)(object)passwordTextBox.get_CustomButton()).Size = new Size(17, 17);
			passwordTextBox.get_CustomButton().set_Style((MetroColorStyle)4);
			((Control)(object)passwordTextBox.get_CustomButton()).TabIndex = 1;
			passwordTextBox.get_CustomButton().set_Theme((MetroThemeStyle)1);
			passwordTextBox.get_CustomButton().set_UseSelectable(true);
			((Control)(object)passwordTextBox.get_CustomButton()).Visible = false;
			((Control)(object)passwordTextBox).ForeColor = Color.White;
			passwordTextBox.set_Lines(new string[0]);
			((Control)(object)passwordTextBox).Location = new Point(19, 46);
			passwordTextBox.set_MaxLength(32767);
			((Control)(object)passwordTextBox).Name = "passwordTextBox";
			passwordTextBox.set_PasswordChar('●');
			passwordTextBox.set_PromptText("Password");
			passwordTextBox.set_ScrollBars(ScrollBars.None);
			passwordTextBox.set_SelectedText("");
			passwordTextBox.set_SelectionLength(0);
			passwordTextBox.set_SelectionStart(0);
			passwordTextBox.set_ShortcutsEnabled(true);
			((Control)(object)passwordTextBox).Size = new Size(333, 22);
			((Control)(object)passwordTextBox).TabIndex = 21;
			passwordTextBox.set_Theme((MetroThemeStyle)2);
			passwordTextBox.set_UseSelectable(true);
			passwordTextBox.set_UseSystemPasswordChar(true);
			passwordTextBox.set_WaterMark("Password");
			passwordTextBox.set_WaterMarkColor(Color.FromArgb(109, 109, 109));
			passwordTextBox.set_WaterMarkFont(new Font("Segoe UI", 12f, FontStyle.Italic, GraphicsUnit.Pixel));
			((Control)(object)mainTabControl).Controls.Add((Control)(object)metroTabPage1);
			((Control)(object)mainTabControl).Location = new Point(23, 63);
			((Control)(object)mainTabControl).Name = "mainTabControl";
			((TabControl)(object)mainTabControl).SelectedIndex = 0;
			((Control)(object)mainTabControl).Size = new Size(380, 173);
			((TabControl)(object)mainTabControl).SizeMode = TabSizeMode.Fixed;
			((Control)(object)mainTabControl).TabIndex = 22;
			mainTabControl.set_Theme((MetroThemeStyle)2);
			mainTabControl.set_UseSelectable(true);
			((Panel)(object)metroTabPage1).BorderStyle = BorderStyle.FixedSingle;
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)lblRegister);
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)passwordTextBox);
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)loginButton);
			((Control)(object)metroTabPage1).Controls.Add((Control)(object)usernameTextBox);
			metroTabPage1.set_HorizontalScrollbarBarColor(true);
			metroTabPage1.set_HorizontalScrollbarHighlightOnWheel(false);
			metroTabPage1.set_HorizontalScrollbarSize(10);
			((TabPage)(object)metroTabPage1).Location = new Point(4, 38);
			((Control)(object)metroTabPage1).Name = "metroTabPage1";
			((Control)(object)metroTabPage1).Size = new Size(372, 131);
			((TabPage)(object)metroTabPage1).TabIndex = 0;
			((Control)(object)metroTabPage1).Text = "Login";
			metroTabPage1.set_Theme((MetroThemeStyle)2);
			metroTabPage1.set_VerticalScrollbarBarColor(true);
			metroTabPage1.set_VerticalScrollbarHighlightOnWheel(false);
			metroTabPage1.set_VerticalScrollbarSize(10);
			((Control)(object)lblRegister).AutoSize = true;
			lblRegister.set_FontWeight((MetroLabelWeight)1);
			((Control)(object)lblRegister).ForeColor = Color.Blue;
			((Control)(object)lblRegister).Location = new Point(108, 104);
			((Control)(object)lblRegister).Name = "lblRegister";
			((Control)(object)lblRegister).Size = new Size(153, 19);
			((Control)(object)lblRegister).TabIndex = 22;
			((Control)(object)lblRegister).Text = "Don't have an account?";
			lblRegister.set_Theme((MetroThemeStyle)2);
			lblRegister.set_UseStyleColors(true);
			((Control)(object)lblRegister).Click += registerLabel_Click;
			((ContainerControl)this).AutoScaleMode = AutoScaleMode.None;
			((Form)this).ClientSize = new Size(420, 257);
			((Control)this).Controls.Add((Control)(object)mainTabControl);
			((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			((Control)this).Name = "LoginForm";
			((MetroForm)this).set_Resizable(false);
			((MetroForm)this).set_ShadowType((MetroFormShadowType)0);
			((MetroForm)this).set_Style((MetroColorStyle)0);
			((Control)(object)this).Text = "Asteroid LLC";
			((MetroForm)this).set_Theme((MetroThemeStyle)2);
			((Form)this).FormClosing += LoginForm_FormClosing;
			((Form)this).Load += LoginForm_Load;
			((ISupportInitialize)styleManager).EndInit();
			((Control)(object)mainTabControl).ResumeLayout(performLayout: false);
			((Control)(object)metroTabPage1).ResumeLayout(performLayout: false);
			((Control)(object)metroTabPage1).PerformLayout();
			((Control)this).ResumeLayout(performLayout: false);
		}
	}
}
