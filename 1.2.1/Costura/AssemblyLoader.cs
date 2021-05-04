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
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		private static object nullCacheLock = new object();

		private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

		private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

		private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();

		private static int isAttached;

		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return "";
			}
			return culture.Name;
		}

		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			Assembly[] assemblies = currentDomain.GetAssemblies();
			Assembly[] array = assemblies;
			foreach (Assembly assembly in array)
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(CultureToString(name2.CultureInfo), CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		private static Stream LoadStream(string fullName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullName.EndsWith(".compressed"))
			{
				using (Stream stream = executingAssembly.GetManifestResourceStream(fullName))
				{
					using DeflateStream source = new DeflateStream(stream, CompressionMode.Decompress);
					MemoryStream memoryStream = new MemoryStream();
					CopyTo(source, memoryStream);
					memoryStream.Position = 0L;
					return memoryStream;
				}
			}
			return executingAssembly.GetManifestResourceStream(fullName);
		}

		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			if (resourceNames.TryGetValue(name, out var value))
			{
				return LoadStream(value);
			}
			return null;
		}

		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = requestedAssemblyName.CultureInfo.Name + "." + text;
			}
			byte[] rawAssembly;
			using (Stream stream = LoadStream(assemblyNames, text))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = ReadStream(stream);
			}
			using (Stream stream2 = LoadStream(symbolNames, text))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			lock (nullCacheLock)
			{
				if (nullCache.ContainsKey(e.Name))
				{
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(e.Name);
			Assembly assembly = ReadExistingAssembly(assemblyName);
			if (assembly != null)
			{
				return assembly;
			}
			assembly = ReadFromEmbeddedResources(assemblyNames, symbolNames, assemblyName);
			if (assembly == null)
			{
				lock (nullCacheLock)
				{
					nullCache[e.Name] = true;
				}
				if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != 0)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		static AssemblyLoader()
		{
			assemblyNames.Add("anonfileapi", "costura.anonfileapi.dll.compressed");
			assemblyNames.Add("bouncycastle.crypto", "costura.bouncycastle.crypto.dll.compressed");
			assemblyNames.Add("costura", "costura.costura.dll.compressed");
			symbolNames.Add("costura", "costura.costura.pdb.compressed");
			assemblyNames.Add("dnlib", "costura.dnlib.dll.compressed");
			assemblyNames.Add("mailkit", "costura.mailkit.dll.compressed");
			symbolNames.Add("mailkit", "costura.mailkit.pdb.compressed");
			assemblyNames.Add("metroframework.design", "costura.metroframework.design.dll.compressed");
			assemblyNames.Add("metroframework", "costura.metroframework.dll.compressed");
			assemblyNames.Add("metroframework.fonts", "costura.metroframework.fonts.dll.compressed");
			assemblyNames.Add("microsoft.web.webview2.core", "costura.microsoft.web.webview2.core.dll.compressed");
			assemblyNames.Add("microsoft.web.webview2.winforms", "costura.microsoft.web.webview2.winforms.dll.compressed");
			assemblyNames.Add("microsoft.web.webview2.wpf", "costura.microsoft.web.webview2.wpf.dll.compressed");
			assemblyNames.Add("mimekit", "costura.mimekit.dll.compressed");
			symbolNames.Add("mimekit", "costura.mimekit.pdb.compressed");
			assemblyNames.Add("siticone.ui", "costura.siticone.ui.dll.compressed");
			assemblyNames.Add("system.buffers", "costura.system.buffers.dll.compressed");
			assemblyNames.Add("system.diagnostics.diagnosticsource", "costura.system.diagnostics.diagnosticsource.dll.compressed");
			assemblyNames.Add("websocket-sharp", "costura.websocket-sharp.dll.compressed");
			assemblyNames.Add("zeroit.framework.codetextbox", "costura.zeroit.framework.codetextbox.dll.compressed");
		}

		public static void Attach()
		{
			if (Interlocked.Exchange(ref isAttached, 1) == 1)
			{
				return;
			}
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs e)
			{
				lock (nullCacheLock)
				{
					if (nullCache.ContainsKey(e.Name))
					{
						return null;
					}
				}
				AssemblyName assemblyName = new AssemblyName(e.Name);
				Assembly assembly = ReadExistingAssembly(assemblyName);
				if (assembly != null)
				{
					return assembly;
				}
				assembly = ReadFromEmbeddedResources(assemblyNames, symbolNames, assemblyName);
				if (assembly == null)
				{
					lock (nullCacheLock)
					{
						nullCache[e.Name] = true;
					}
					if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != 0)
					{
						assembly = Assembly.Load(assemblyName);
					}
				}
				return assembly;
			};
		}
	}
}
