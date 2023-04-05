using UnityModManagerNet;

namespace CrusadeRelief {
	public class Setting :UnityModManager.ModSettings {
		public bool AvoidRandomEncounter = true;
		public bool CrusadeAutoWin = true;
		public override void Save(UnityModManager.ModEntry modEntry) {
			Save(this, modEntry);
		}
	}
}
