// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis.GameData.Sheets
{
	using Lumina.Data;
	using Lumina.Excel;
	using Lumina.Text;
	using Generated = Lumina.Excel.GeneratedSheets;
	using LuminaData = Lumina.GameData;

	[Sheet("Item")]
	public class Item : ExcelRow
	{
		public SeString? Description;
		public SeString? Name;
		public ushort Icon;
		public byte ItemUICategory;
		public LazyRow<Generated.EquipSlotCategory>? EquipSlotCategory;
		public byte EquipRestriction;
		public LazyRow<ClassJobCategory>? ClassJobCategory;
		public ulong ModelMain;
		public ulong ModelSub;

		public override void PopulateData(RowParser parser, LuminaData lumina, Language language)
		{
			this.RowId = parser.Row;
			this.SubRowId = parser.SubRow;

			this.Description = parser.ReadColumn<SeString>(8);
			this.Name = parser.ReadColumn<SeString>(9);
			this.Icon = parser.ReadColumn<ushort>(10);
			this.ItemUICategory = parser.ReadColumn<byte>(15);
			this.EquipSlotCategory = new LazyRow<Generated.EquipSlotCategory>(lumina, parser.ReadColumn<byte>(17), language);
			this.EquipRestriction = parser.ReadColumn<byte>(43);
			this.ClassJobCategory = new LazyRow<ClassJobCategory>(lumina, parser.ReadColumn<byte>(44), language);
			this.ModelMain = parser.ReadColumn<ulong>(48);
			this.ModelSub = parser.ReadColumn<ulong>(49);
		}
	}
}
