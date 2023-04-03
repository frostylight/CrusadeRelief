using HarmonyLib;
using System.Reflection;
using UnityModManagerNet;

namespace CrusadeRelief {
	public static class Main {
		public static bool enabled = false;
		public static bool Load(UnityModManager.ModEntry modEntry) {
			modEntry.OnToggle = OnToggle;
			new Harmony(modEntry.Info.Id).PatchAll(Assembly.GetExecutingAssembly());
			return true;
		}
		public static bool OnToggle(UnityModManager.ModEntry modEntry, bool state) {
			enabled = state;
			return true;
		}
	}
}
