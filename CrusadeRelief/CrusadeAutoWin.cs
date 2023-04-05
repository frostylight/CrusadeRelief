using HarmonyLib;
using Kingmaker;
using Kingmaker.Armies;
using Kingmaker.Armies.TacticalCombat;
using Kingmaker.Armies.TacticalCombat.Brain;
using Kingmaker.Armies.TacticalCombat.Controllers;
using Kingmaker.Armies.TacticalCombat.Data;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Globalmap.State;
using Kingmaker.PubSubSystem;

namespace CrusadeRelief {
	[HarmonyPatch(typeof(TacticalCombatResultsPrediction), nameof(TacticalCombatResultsPrediction.GetAttackerLossesPercent))]
	public static class TacticalCombatResultsPrediction_GetAttackerLossesPercent_Patch {
		public static void Postfix(ref float __result, GlobalMapArmyState attacker) {
			if(!Main.Enabled || !Main.settings.CrusadeAutoWin) return;
			if(attacker.Data.Faction == ArmyFaction.Crusaders) {
				__result = 0f;
			}
			else {
				__result = 100f;
			}
		}
	}
	[HarmonyPatch(typeof(TacticalCombatAIBrainController), nameof(TacticalCombatAIBrainController.Tick))]
	internal static class TacticalCombatAIBrainController_Tick_Patch {
		public static bool Prefix(TacticalCombatDecisionContext ___s_UnitContext) {
			if(!Main.Enabled || !Main.settings.CrusadeAutoWin) {
				return true;
			}
			TacticalCombatController tacticalCombat = Game.Instance.TacticalCombat;
			TurnState turn = tacticalCombat.Data.Turn;
			UnitEntityData unit = turn.Unit;
			if(tacticalCombat.Data.IsBusy || unit == null || unit.IsDirectlyControllable || !unit.Commands.Empty || turn.StandardActionUsed) {
				return false;
			}
			TacticalCombatArmyData tacticalData = unit.GetTacticalData();
			if(tacticalData.Faction == ArmyFaction.Crusaders) {
				return true;
			}
			___s_UnitContext.InitUnit(unit);
			TacticalCombatData data = Game.Instance.TacticalCombat.Data;
			data.Turn.IsBonusMoraleTurn = true;
			data.Turn.UsedActionsCount = unit.Blueprint.GetArmyData().MaxExtraActions;
			data.Turn.UpdateStandardAction(true);
			EventBus.RaiseEvent(delegate (ITacticalCombatSkipTurnHandler h) {
				h.HandleSkipTurnTurn(data.Turn.Unit);
			}, true);
			Game.Instance.TacticalCombat.TurnController.EndTurn(false);
			___s_UnitContext.ReleaseUnit();
			___s_UnitContext.ReleaseGroup();
			return false;
		}
	}
}
