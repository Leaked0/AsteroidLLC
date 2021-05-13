#define DEBUG
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;

namespace LunarGrabber
{
	public static class MyExtensions
	{
		public static void SetStyle(this IContainer container, MetroForm ownerForm, MetroColorStyle formStyle)
		{
			if (container == null)
			{
				container = new Container();
			}
			MetroStyleManager val = new MetroStyleManager(container);
			val.set_Owner((ContainerControl)(object)ownerForm);
			ownerForm.set_Style(formStyle);
			container.SetDefaultStyle(ownerForm, formStyle);
		}

		public static void SetDefaultStyle(this IContainer contr, MetroForm owner, MetroColorStyle style)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			MetroStyleManager val = FindManager(contr, owner);
			val.set_Style(style);
		}

		public static void SetTheme(this IContainer container, MetroForm ownerForm, MetroThemeStyle formTheme)
		{
			if (container == null)
			{
				container = new Container();
			}
			MetroStyleManager val = new MetroStyleManager(container);
			val.set_Owner((ContainerControl)(object)ownerForm);
			ownerForm.set_Theme(formTheme);
			container.SetDefaultTheme(ownerForm, formTheme);
		}

		public static void SetDefaultTheme(this IContainer contr, MetroForm owner, MetroThemeStyle theme)
		{
			MetroStyleManager val = FindManager(contr, owner);
			val.set_Theme(theme);
		}

		private static MetroStyleManager FindManager(IContainer contr, MetroForm owner)
		{
			MetroStyleManager result = new MetroStyleManager(contr);
			foreach (IComponent component in contr.Components)
			{
				if (((MetroStyleManager)component).get_Owner() == owner)
				{
					result = (MetroStyleManager)component;
				}
			}
			return result;
		}

		public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
		{
			HttpMethod val = new HttpMethod("PATCH");
			HttpRequestMessage val2 = new HttpRequestMessage(val, requestUri);
			val2.set_Content(content);
			HttpRequestMessage val3 = val2;
			HttpResponseMessage response = new HttpResponseMessage();
			try
			{
				response = await client.SendAsync(val3);
				return response;
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine("ERROR: " + ex.ToString());
				return response;
			}
		}
	}
}
