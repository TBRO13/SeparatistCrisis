// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.Patches.ReplaceCharacterCreation
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

using HarmonyLib;
using SandBox;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;

namespace SeparatistCrisis.Patches
{
  [HarmonyPatch]
  public class ReplaceCharacterCreation
  {
    [HarmonyPrefix]
    [HarmonyPatch(typeof (SandBoxGameManager), "LaunchSandboxCharacterCreation")]
    private static bool LaunchSandboxCharacterCreation()
    {
      Game.Current.GameStateManager.CleanAndPushState((GameState) Game.Current.GameStateManager.CreateState<CharacterCreationState>((object) new ScCharacterCreationContent()));
      return false;
    }
  }
}
