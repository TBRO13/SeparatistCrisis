// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.Main
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

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
