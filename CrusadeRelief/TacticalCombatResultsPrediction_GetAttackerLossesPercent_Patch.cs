using HarmonyLib;
using Kingmaker.Armies;
using Kingmaker.Armies.TacticalCombat;
using Kingmaker.Globalmap.State;

namespace CrusadeRelief {
	[HarmonyPatch(typeof(TacticalCombatResultsPrediction), nameof(TacticalCombatResultsPrediction.GetAttackerLossesPercent))]
	public static class TacticalCombatResultsPrediction_GetAttackerLossesPercent_Patch {
		public static void Postfix(ref float __result, GlobalMapArmyState attacker) {
			if(!Main.enabled) return;
			if(attacker.Data.Faction == ArmyFaction.Crusaders) {
				__result = 0f;
			}
			else {
				__result = 100f;
			}
		}
	}
}
