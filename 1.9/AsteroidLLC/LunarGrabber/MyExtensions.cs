using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;

namespace LunarGrabber
{
	// Token: 0x02000005 RID: 5
	public static class MyExtensions
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000023C4 File Offset: 0x000005C4
		public static void SetStyle(this IContainer container, MetroForm ownerForm, MetroColorStyle formStyle)
		{
			if (container == null)
			{
				container = new Container();
			}
			MetroStyleManager metroStyleManager = new MetroStyleManager(container);
			metroStyleManager.Owner = ownerForm;
			ownerForm.Style = formStyle;
			container.SetDefaultStyle(ownerForm, formStyle);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023F8 File Offset: 0x000005F8
		public static void SetDefaultStyle(this IContainer contr, MetroForm owner, MetroColorStyle style)
		{
			MetroStyleManager metroStyleManager = MyExtensions.FindManager(contr, owner);
			metroStyleManager.Style = style;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002414 File Offset: 0x00000614
		public static void SetTheme(this IContainer container, MetroForm ownerForm, MetroThemeStyle formTheme)
		{
			if (container == null)
			{
				container = new Container();
			}
			MetroStyleManager metroStyleManager = new MetroStyleManager(container);
			metroStyleManager.Owner = ownerForm;
			ownerForm.Theme = formTheme;
			container.SetDefaultTheme(ownerForm, formTheme);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002448 File Offset: 0x00000648
		public static void SetDefaultTheme(this IContainer contr, MetroForm owner, MetroThemeStyle theme)
		{
			MetroStyleManager metroStyleManager = MyExtensions.FindManager(contr, owner);
			metroStyleManager.Theme = theme;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002464 File Offset: 0x00000664
		private static MetroStyleManager FindManager(IContainer contr, MetroForm owner)
		{
			MetroStyleManager result = new MetroStyleManager(contr);
			foreach (object obj in contr.Components)
			{
				IComponent component = (IComponent)obj;
				if (((MetroStyleManager)component).Owner == owner)
				{
					result = (MetroStyleManager)component;
				}
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024D4 File Offset: 0x000006D4
		public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
		{
			MyExtensions.<PatchAsync>d__5 <PatchAsync>d__;
			<PatchAsync>d__.client = client;
			<PatchAsync>d__.requestUri = requestUri;
			<PatchAsync>d__.content = content;
			<PatchAsync>d__.<>t__builder = AsyncTaskMethodBuilder<HttpResponseMessage>.Create();
			<PatchAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<HttpResponseMessage> <>t__builder = <PatchAsync>d__.<>t__builder;
			<>t__builder.Start<MyExtensions.<PatchAsync>d__5>(ref <PatchAsync>d__);
			return <PatchAsync>d__.<>t__builder.Task;
		}
	}
}
