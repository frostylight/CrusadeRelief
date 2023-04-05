using HarmonyLib;
using Kingmaker.Assets.Controllers.GlobalMap;
using Kingmaker.RandomEncounters;

namespace CrusadeRelief {
	[HarmonyPatch(typeof(RandomEncountersController), "GetAvoidanceCheckResult")]
	public static class RandomEncountersController_GetAvoidanceCheckResult_Patch {
		public static void Postfix(ref RandomEncounterAvoidanceCheckResult __result) {
			if(!Main.Enabled || !Main.settings.AvoidRandomEncounter) return;
			__result = RandomEncounterAvoidanceCheckResult.Success;
		}
	}
}
