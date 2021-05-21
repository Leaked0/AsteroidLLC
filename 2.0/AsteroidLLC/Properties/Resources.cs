using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AsteroidLLC.Properties
{
	// Token: 0x02000002 RID: 2
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("AsteroidLLC.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002099 File Offset: 0x00000299
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020A0 File Offset: 0x000002A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020A8 File Offset: 0x000002A8
		internal static string Code
		{
			get
			{
				return Resources.ResourceManager.GetString("Code", Resources.resourceCulture);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020C0 File Offset: 0x000002C0
		internal static byte[] GrabberProject
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("GrabberProject", Resources.resourceCulture);
				return (byte[])@object;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		internal static string RunBindedFile
		{
			get
			{
				return Resources.ResourceManager.GetString("RunBindedFile", Resources.resourceCulture);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager resourceMan;

		// Token: 0x04000002 RID: 2
		private static CultureInfo resourceCulture;
	}
}
