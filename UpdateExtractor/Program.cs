// © Anamnesis.
// Licensed under the MIT license.

namespace UpdateExtractor
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Reflection;
	using System.Threading;

	public class Program
	{
		public static void Main(string[] args)
		{
			string processName = "Unknown";

			try
			{
				if (args.Length != 2)
					throw new Exception("Invalid arguments. Update Extractor must be run with the following arguments: 1) destination directory, 2) name of orignal process.");

				string? destDir = args[0];
				processName = args[1];
				Console.WriteLine($"{processName} Updater");

				Console.Write($"Waiting for {processName} to terminate");
				while (true)
				{
					Process[] procs = Process.GetProcessesByName(processName);
					if (procs.Length <= 0)
					{
						break;
					}
					else
					{
						Thread.Sleep(100);
						Console.Write(".");
					}
				}

				Console.WriteLine(" done.");

				string? sourceDir = Path.Combine(Path.GetTempPath(), "AnamnesisUpdateLatest");

				if (!Directory.Exists(sourceDir))
					throw new Exception("Unable to determine current process path");

				if (string.IsNullOrEmpty(sourceDir))
					throw new Exception("Unable to determine source directory");

				if (string.IsNullOrEmpty(destDir))
					throw new Exception("Unable to determine destination directory");

				// Is destDir actually a file path, get a directory instead.
				if (File.Exists(destDir))
				{
					destDir = Path.GetDirectoryName(destDir);

					if (string.IsNullOrEmpty(destDir))
					{
						throw new Exception("Unable to determine destination directory");
					}
				}

				destDir = destDir.Replace('/', '\\');
				destDir = destDir.Trim('\\');
				destDir += "\\";

				string oldExe = destDir + "Anamnesis.exe";

				if (!File.Exists(oldExe))
					throw new Exception($"No Anamnesis executable found at: {oldExe}");

				Console.WriteLine("Cleaning old version");
				DeleteFileIfExists(Path.Combine(destDir, "Anamnesis.exe"));
				DeleteFileIfExists(Path.Combine(destDir, "AnamnesisLauncher.exe"));
				DeleteFileIfExists(Path.Combine(destDir, "Anamnesis.pdb"));
				DeleteFileIfExists(Path.Combine(destDir, "Anamnesis.xml"));
				DeleteFileIfExists(Path.Combine(destDir, "Version.txt"));

				DeleteDirectoryIfExists(Path.Combine(destDir, "Data"));
				DeleteDirectoryIfExists(Path.Combine(destDir, "Languages"));
				DeleteDirectoryIfExists(Path.Combine(destDir, "Updater"));
				DeleteDirectoryIfExists(Path.Combine(destDir, "bin"));

				Console.WriteLine("Copying Update Files");

				string[] files = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories);
				foreach (string sourceFile in files)
				{
					string destFile = sourceFile.Replace(sourceDir, destDir);
					Console.WriteLine("    > " + destFile);

					string? directory = Path.GetDirectoryName(destFile);
					if (directory != null && !Directory.Exists(directory))
						Directory.CreateDirectory(directory);

					File.Copy(sourceFile, destFile, true);
				}

				Console.WriteLine("Restarting application");

				string launch = destDir + "Anamnesis.exe";
				Console.WriteLine("    > " + launch);
				ProcessStartInfo start = new ProcessStartInfo(launch);
				Process.Start(start);
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Failed to update {processName}");
				Console.WriteLine(ex.Message);
				Console.WriteLine();
				Console.WriteLine("Please download the update manually.");
				Console.WriteLine();
				Console.WriteLine("Press any key to close this window.");
				Console.ReadKey();
			}
		}

		private static void DeleteFileIfExists(string path)
		{
			if (!File.Exists(path))
				return;

			File.Delete(path);
		}

		private static void DeleteDirectoryIfExists(string path)
		{
			if (!Directory.Exists(path))
				return;

			Directory.Delete(path, true);
		}
	}
}
