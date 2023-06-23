// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.Patches.FixCrosshair
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.GauntletUI.Mission;
using TaleWorlds.ScreenSystem;

namespace SeparatistCrisis.Patches
{
  [HarmonyPatch]
  public class FixCrosshair
  {
    [HarmonyPostfix]
    [HarmonyPatch(typeof (MissionGauntletCrosshair), "GetShouldCrosshairBeVisible")]
    private static void GetShouldCrosshairBeVisible(
      MissionGauntletCrosshair __instance,
      ref bool __result)
    {
      if (__result || !BannerlordConfig.DisplayTargetingReticule || __instance.Mission.Mode == MissionMode.Conversation || __instance.Mission.Mode == MissionMode.CutScene || ScreenManager.GetMouseVisibility() || __instance.Mission.MainAgent == null || __instance.Mission.MainAgent.WieldedWeapon.IsEmpty)
        return;
      MissionWeapon missionWeapon = __instance.Mission.MainAgent.WieldedWeapon;
      if (!missionWeapon.CurrentUsageItem.IsRangedWeapon || __instance.MissionScreen.IsViewingCharacter() || (NativeObject) __instance.MissionScreen.CustomCamera != (NativeObject) null || missionWeapon.CurrentUsageItem.WeaponClass != WeaponClass.Crossbow)
        return;
      missionWeapon = missionWeapon.AmmoWeapon;
      if (missionWeapon.Amount <= (short) 0)
        return;
      __result = true;
    }
  }
}
