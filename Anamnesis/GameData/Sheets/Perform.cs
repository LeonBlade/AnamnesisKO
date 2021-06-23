// © Anamnesis.
// Developed by W and A Walsh.
// Licensed under the MIT license.

namespace Anamnesis.GameData.Sheets
{
	using Lumina.Data;
	using Lumina.Excel;
	using Lumina.Text;
	using Generated = Lumina.Excel.GeneratedSheets;
	using LuminaData = Lumina.GameData;

	[Sheet("Perform")]
	public class Perform : ExcelRow
	{
		public SeString? Name { get; set; }
		public bool Unknown1 { get; set; }
		public ulong ModelKey { get; set; }
		public LazyRow<Generated.ActionTimeline>? AnimationStart { get; set; }
		public LazyRow<Generated.ActionTimeline>? AnimationEnd { get; set; }
		public LazyRow<Generated.ActionTimeline>? AnimationIdle { get; set; }
		public LazyRow<Generated.ActionTimeline>? AnimationPlay01 { get; set; }
		public LazyRow<Generated.ActionTimeline>? AnimationPlay02 { get; set; }
		public LazyRow<Generated.ActionTimeline>? StopAnimation { get; set; }
		public SeString? Instrument { get; set; }
		public int Order { get; set; }
		public LazyRow<Generated.PerformTransient>? Transient { get; set; }

		public override void PopulateData(RowParser parser, LuminaData gameData, Language language)
		{
			base.PopulateData(parser, gameData, language);

			this.Name = parser.ReadColumn<SeString>(0);
			this.Unknown1 = parser.ReadColumn<bool>(1);
			this.ModelKey = parser.ReadColumn<ulong>(2);
			this.AnimationStart = new LazyRow<Generated.ActionTimeline>(gameData, parser.ReadColumn<ushort>(3), language);
			this.AnimationEnd = new LazyRow<Generated.ActionTimeline>(gameData, parser.ReadColumn<ushort>(4), language);
			this.AnimationIdle = new LazyRow<Generated.ActionTimeline>(gameData, parser.ReadColumn<ushort>(5), language);
			this.AnimationPlay01 = new LazyRow<Generated.ActionTimeline>(gameData, parser.ReadColumn<ushort>(6), language);
			this.AnimationPlay02 = new LazyRow<Generated.ActionTimeline>(gameData, parser.ReadColumn<ushort>(7), language);
			this.StopAnimation = new LazyRow<Generated.ActionTimeline>(gameData, parser.ReadColumn<int>(8), language);
			this.Instrument = parser.ReadColumn<SeString>(9);
			this.Order = parser.ReadColumn<int>(10);
			this.Transient = new LazyRow<Generated.PerformTransient>(gameData, parser.ReadColumn<byte>(11), language);
		}
	}
}
