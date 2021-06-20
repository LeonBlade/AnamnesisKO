// © Anamnesis.
// Developed by W and A Walsh.
// Licensed under the MIT license.

namespace Anamnesis.GameData.Sheets
{
	using Lumina.Data;
	using Lumina.Excel;
	using Lumina.Text;
	using LuminaData = Lumina.GameData;

	[Sheet("PlaceName")]
	public class PlaceName : ExcelRow
	{
		public SeString? Name;

		public override void PopulateData(RowParser parser, LuminaData lumina, Language language)
		{
			base.PopulateData(parser, lumina, language);

			this.Name = parser.ReadColumn<SeString>(0);
		}
	}
}