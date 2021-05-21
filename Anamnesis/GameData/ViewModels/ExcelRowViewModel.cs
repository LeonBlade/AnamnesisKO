// © Anamnesis.
// Developed by W and A Walsh.
// Licensed under the MIT license.

namespace Anamnesis.GameData.ViewModels
{
	using System;
	using Lumina;
	using Lumina.Excel;

	public abstract class ExcelRowViewModel<T> : IRow
		where T : ExcelRow
	{
		protected readonly GameData lumina;

		private ExcelSheet<T> sheet;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ExcelRowViewModel(int key, ExcelSheet<T> sheet, GameData lumina)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
			this.sheet = sheet;
			this.Key = key;
			this.lumina = lumina;

			try
			{
#pragma warning disable CS8601 // Possible null reference assignment.
				this.Value = this.sheet.GetRow((uint)this.Key);
#pragma warning restore CS8601 // Possible null reference assignment.
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to read Lumina row: {this.Key} for type: {typeof(T).Name}", ex);
			}
		}

		public int Key
		{
			get;
			private set;
		}

		public T Value { get; private set; }

		public abstract string Name
		{
			get;
		}

		public virtual string? Description
		{
			get => null;
		}
	}
}
