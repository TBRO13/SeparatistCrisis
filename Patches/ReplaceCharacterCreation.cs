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
