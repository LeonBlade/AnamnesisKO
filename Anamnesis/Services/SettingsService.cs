﻿// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis.Services
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.IO;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Input;
	using Anamnesis;
	using Anamnesis.Files;
	using Anamnesis.GUI.Dialogs;
	using Anamnesis.Serialization;
	using MaterialDesignThemes.Wpf;
	using XivToolsWpf;

	public class SettingsService : ServiceBase<SettingsService>
	{
		private static readonly string SettingsPath = FileService.ParseToFilePath(FileService.StoreDirectory + "/Settings.json");

		public static event PropertyChangedEventHandler? SettingsChanged;

		public static Settings Current => Instance.Settings!;

		public Settings? Settings { get; private set; }
		public bool FirstTimeUser { get; private set; }

		public static void ShowDirectory()
		{
			FileService.OpenDirectory(FileService.StoreDirectory);
		}

		public static void Save()
		{
			string json = SerializerService.Serialize(Instance.Settings!);
			File.WriteAllText(SettingsPath, json);
		}

		public static void ApplyTheme()
		{
			if (Current.OverrideSystemTheme)
			{
				Themes.ApplyCustomTheme(Current.ThemeLight, Current.ThemeColor);
			}
			else
			{
				Themes.ApplySystemTheme();
			}
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			if (!File.Exists(SettingsPath))
			{
				this.FirstTimeUser = true;
				this.Settings = new Settings();
				Save();
			}
			else
			{
				this.FirstTimeUser = false;
				try
				{
					await Dispatch.MainThread();

					if (Keyboard.IsKeyDown(Key.LeftShift))
						throw new Exception("User Abort");

					string json = File.ReadAllText(SettingsPath);
					this.Settings = SerializerService.Deserialize<Settings>(json);
				}
				catch (Exception ex)
				{
					Log.Warning(ex, "Failed to load settings");
					await GenericDialog.Show("Failed to load Settings. Your settings have been reset.", "Error", MessageBoxButton.OK);
					this.Settings = new Settings();
					Save();
				}
			}

			this.Settings.PropertyChanged += this.OnSettingsChanged;
			this.OnSettingsChanged(null, new PropertyChangedEventArgs(null));
		}

		private void OnSettingsChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (this.Settings == null)
				return;

			if (sender is Settings settings)
			{
				Save();
			}

			ApplyTheme();
			SettingsChanged?.Invoke(sender, e);
		}
	}
}
