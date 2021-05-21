// © Anamnesis.
// Developed by W and A Walsh.
// Licensed under the MIT license.

namespace Anamnesis.GameData.Sheets
{
	using Lumina.Data;
	using Lumina.Excel;
	using Lumina.Text;
	using LuminaData = Lumina.GameData;

	[Sheet("ClassJobCategory")]
	public class ClassJobCategory : ExcelRow
	{
		public SeString? Name;
		public bool ADV { get; set; }
		public bool GLA { get; set; }
		public bool PGL { get; set; }
		public bool MRD { get; set; }
		public bool LNC { get; set; }
		public bool ARC { get; set; }
		public bool CNJ { get; set; }
		public bool THM { get; set; }
		public bool CRP { get; set; }
		public bool BSM { get; set; }
		public bool ARM { get; set; }
		public bool GSM { get; set; }
		public bool LTW { get; set; }
		public bool WVR { get; set; }
		public bool ALC { get; set; }
		public bool CUL { get; set; }
		public bool MIN { get; set; }
		public bool BTN { get; set; }
		public bool FSH { get; set; }
		public bool PLD { get; set; }
		public bool MNK { get; set; }
		public bool WAR { get; set; }
		public bool DRG { get; set; }
		public bool BRD { get; set; }
		public bool WHM { get; set; }
		public bool BLM { get; set; }
		public bool ACN { get; set; }
		public bool SMN { get; set; }
		public bool SCH { get; set; }
		public bool ROG { get; set; }
		public bool NIN { get; set; }
		public bool MCH { get; set; }
		public bool DRK { get; set; }
		public bool AST { get; set; }
		public bool SAM { get; set; }
		public bool RDM { get; set; }
		public bool BLU { get; set; }
		public bool GNB { get; set; }
		public bool DNC { get; set; }

		public override void PopulateData(RowParser parser, LuminaData gameData, Language language)
		{
			base.PopulateData(parser, gameData, language);

			this.Name = parser.ReadColumn<SeString>(0);
			this.ADV = parser.ReadColumn<bool>(1);
			this.GLA = parser.ReadColumn<bool>(2);
			this.PGL = parser.ReadColumn<bool>(3);
			this.MRD = parser.ReadColumn<bool>(4);
			this.LNC = parser.ReadColumn<bool>(5);
			this.ARC = parser.ReadColumn<bool>(6);
			this.CNJ = parser.ReadColumn<bool>(7);
			this.THM = parser.ReadColumn<bool>(8);
			this.CRP = parser.ReadColumn<bool>(9);
			this.BSM = parser.ReadColumn<bool>(10);
			this.ARM = parser.ReadColumn<bool>(11);
			this.GSM = parser.ReadColumn<bool>(12);
			this.LTW = parser.ReadColumn<bool>(13);
			this.WVR = parser.ReadColumn<bool>(14);
			this.ALC = parser.ReadColumn<bool>(15);
			this.CUL = parser.ReadColumn<bool>(16);
			this.MIN = parser.ReadColumn<bool>(17);
			this.BTN = parser.ReadColumn<bool>(18);
			this.FSH = parser.ReadColumn<bool>(19);
			this.PLD = parser.ReadColumn<bool>(20);
			this.MNK = parser.ReadColumn<bool>(21);
			this.WAR = parser.ReadColumn<bool>(22);
			this.DRG = parser.ReadColumn<bool>(23);
			this.BRD = parser.ReadColumn<bool>(24);
			this.WHM = parser.ReadColumn<bool>(25);
			this.BLM = parser.ReadColumn<bool>(26);
			this.ACN = parser.ReadColumn<bool>(27);
			this.SMN = parser.ReadColumn<bool>(28);
			this.SCH = parser.ReadColumn<bool>(29);
			this.ROG = parser.ReadColumn<bool>(30);
			this.NIN = parser.ReadColumn<bool>(31);
			this.MCH = parser.ReadColumn<bool>(32);
			this.DRK = parser.ReadColumn<bool>(33);
			this.AST = parser.ReadColumn<bool>(34);
			this.SAM = parser.ReadColumn<bool>(35);
			this.RDM = parser.ReadColumn<bool>(36);
			this.BLU = parser.ReadColumn<bool>(37);
			this.GNB = parser.ReadColumn<bool>(38);
			this.DNC = parser.ReadColumn<bool>(39);
		}
	}
}