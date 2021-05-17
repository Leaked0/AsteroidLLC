using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura
{
	// Token: 0x02000023 RID: 35
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		// Token: 0x060000DE RID: 222 RVA: 0x0000DA51 File Offset: 0x0000BC51
		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return "";
			}
			return culture.Name;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000DA64 File Offset: 0x0000BC64
		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			Assembly[] assemblies = currentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name2.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000DB08 File Offset: 0x0000BD08
		private static Stream LoadStream(string fullName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullName.EndsWith(".compressed"))
			{
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName))
				{
					using (DeflateStream deflateStream = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
					{
						MemoryStream memoryStream = new MemoryStream();
						AssemblyLoader.CopyTo(deflateStream, memoryStream);
						memoryStream.Position = 0L;
						return memoryStream;
					}
				}
			}
			return executingAssembly.GetManifestResourceStream(fullName);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			string fullName;
			if (resourceNames.TryGetValue(name, out fullName))
			{
				return AssemblyLoader.LoadStream(fullName);
			}
			return null;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = requestedAssemblyName.CultureInfo.Name + "." + text;
			}
			byte[] rawAssembly;
			using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, text))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = AssemblyLoader.ReadStream(stream);
			}
			using (Stream stream2 = AssemblyLoader.LoadStream(symbolNames, text))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000DC94 File Offset: 0x0000BE94
		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			object obj = AssemblyLoader.nullCacheLock;
			lock (obj)
			{
				if (AssemblyLoader.nullCache.ContainsKey(e.Name))
				{
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(e.Name);
			Assembly assembly = AssemblyLoader.ReadExistingAssembly(assemblyName);
			if (assembly != null)
			{
				return assembly;
			}
			assembly = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
			if (assembly == null)
			{
				object obj2 = AssemblyLoader.nullCacheLock;
				lock (obj2)
				{
					AssemblyLoader.nullCache[e.Name] = true;
				}
				if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000DD78 File Offset: 0x0000BF78
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyLoader()
		{
			AssemblyLoader.assemblyNames.Add("anonfileapi", "costura.anonfileapi.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bouncycastle.crypto", "costura.bouncycastle.crypto.dll.compressed");
			AssemblyLoader.assemblyNames.Add("costura", "costura.costura.dll.compressed");
			AssemblyLoader.symbolNames.Add("costura", "costura.costura.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("dnlib", "costura.dnlib.dll.compressed");
			AssemblyLoader.assemblyNames.Add("mailkit", "costura.mailkit.dll.compressed");
			AssemblyLoader.symbolNames.Add("mailkit", "costura.mailkit.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("metroframework.design", "costura.metroframework.design.dll.compressed");
			AssemblyLoader.assemblyNames.Add("metroframework", "costura.metroframework.dll.compressed");
			AssemblyLoader.assemblyNames.Add("metroframework.fonts", "costura.metroframework.fonts.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.web.webview2.core", "costura.microsoft.web.webview2.core.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.web.webview2.winforms", "costura.microsoft.web.webview2.winforms.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.web.webview2.wpf", "costura.microsoft.web.webview2.wpf.dll.compressed");
			AssemblyLoader.assemblyNames.Add("mimekit", "costura.mimekit.dll.compressed");
			AssemblyLoader.symbolNames.Add("mimekit", "costura.mimekit.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("siticone.ui", "costura.siticone.ui.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.buffers", "costura.system.buffers.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.diagnosticsource", "costura.system.diagnostics.diagnosticsource.dll.compressed");
			AssemblyLoader.assemblyNames.Add("websocket-sharp", "costura.websocket-sharp.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zeroit.framework.codetextbox", "costura.zeroit.framework.codetextbox.dll.compressed");
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000DF40 File Offset: 0x0000C140
		public static void Attach()
		{
			if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
			{
				return;
			}
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// Token: 0x040000D8 RID: 216
		private static object nullCacheLock = new object();

		// Token: 0x040000D9 RID: 217
		private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

		// Token: 0x040000DA RID: 218
		private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

		// Token: 0x040000DB RID: 219
		private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();

		// Token: 0x040000DC RID: 220
		private static int isAttached;
	}
}
