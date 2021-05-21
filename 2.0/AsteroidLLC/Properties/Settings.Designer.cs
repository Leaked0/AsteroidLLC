using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace AsteroidLLC.Properties
{
	// Token: 0x02000003 RID: 3
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020FE File Offset: 0x000002FE
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000003 RID: 3
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
