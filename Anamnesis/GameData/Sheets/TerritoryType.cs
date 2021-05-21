// © Anamnesis.
// Developed by W and A Walsh.
// Licensed under the MIT license.

namespace Anamnesis.GameData.Sheets
{
	using Lumina.Data;
	using Lumina.Excel;
	using Lumina.Text;
	using LuminaData = Lumina.GameData;

	[Sheet("TerritoryType")]
	public class TerritoryType : ExcelRow
	{
		public SeString? Name;
		public LazyRow<PlaceName>? PlaceNameRegion;
		public LazyRow<PlaceName>? PlaceNameZone;
		public LazyRow<PlaceName>? PlaceName;
		public byte WeatherRate;

		public override void PopulateData(RowParser parser, LuminaData gameData, Language language)
		{
			base.PopulateData(parser, gameData, language);

			this.Name = parser.ReadColumn<SeString>(0);
			this.PlaceNameRegion = new LazyRow<PlaceName>(gameData, parser.ReadColumn<ushort>(3), language);
			this.PlaceNameZone = new LazyRow<PlaceName>(gameData, parser.ReadColumn<ushort>(4), language);
			this.PlaceName = new LazyRow<PlaceName>(gameData, parser.ReadColumn<ushort>(5), language);
			this.WeatherRate = parser.ReadColumn<byte>(12);
		}
	}
}