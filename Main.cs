using HarmonyLib;
using SeparatistCrisis.MissionSC;
using SeparatistCrisis.Patches;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SeparatistCrisis
{
  public class Main : MBSubModuleBase
  {
    protected override void OnSubModuleLoad()
    {
      new Harmony("com.separatistcrisis.patches").PatchAll();
      MainMenuPatches mainMenuPatches = new MainMenuPatches();
    }

    protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
    {
      base.OnGameStart(game, gameStarterObject);
      if (game.GameType is Campaign)
        ;
    }

    public override void OnMissionBehaviorInitialize(Mission mission)
    {
      base.OnMissionBehaviorInitialize(mission);
      mission.AddMissionBehavior((MissionBehavior) new MissionLogicForceAtmosphere());
    }
  }
}
