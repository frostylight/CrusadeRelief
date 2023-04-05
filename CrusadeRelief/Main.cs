using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;

namespace CrusadeRelief {
	public static class Main {
		public static bool Enabled = false;
		public static Setting settings;
		public static bool Load(UnityModManager.ModEntry modEntry) {
			settings = UnityModManager.ModSettings.Load<Setting>(modEntry);
			modEntry.OnToggle = OnToggle;
			modEntry.OnGUI = OnGui;
			modEntry.OnSaveGUI = OnSaveGUI;
			new Harmony(modEntry.Info.Id).PatchAll(Assembly.GetExecutingAssembly());
			return true;
		}
		public static bool OnToggle(UnityModManager.ModEntry modEntry, bool state) {
			Enabled = state;
			return true;
		}
		public static void OnGui(UnityModManager.ModEntry modEntry) {
			settings.CrusadeAutoWin = GUILayout.Toggle(settings.CrusadeAutoWin, "Auto Win Crusade Combat");
			settings.AvoidRandomEncounter = GUILayout.Toggle(settings.AvoidRandomEncounter, "Avoid Random Encounter");
		}
		public static void OnSaveGUI(UnityModManager.ModEntry modEntry) {
			settings.Save(modEntry);
		}
	}
}
