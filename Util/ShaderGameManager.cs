// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.Util.ShaderGameManager
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.CustomBattle;
using TaleWorlds.MountAndBlade.CustomBattle.CustomBattle;
using TaleWorlds.ObjectSystem;

namespace SeparatistCrisis.Util
{
  public class ShaderGameManager : CustomGameManager
  {
    public override void OnLoadFinished()
    {
      this.IsLoaded = true;
      this.LoadScene();
    }

    private void LoadScene()
    {
      CustomBattleData data = new CustomBattleData()
      {
        GameType = CustomBattleGameType.Battle,
        SceneId = "battle_terrain_a",
        PlayerCharacter = this.SelectPlayer()
      };
      data.PlayerParty = this.GetPlayerParty(data.PlayerCharacter);
      data.EnemyParty = this.GetEnemyParty();
      data.IsPlayerGeneral = true;
      data.PlayerSideGeneralCharacter = (BasicCharacterObject) null;
      data.SeasonId = "summer";
      data.SceneLevel = "";
      data.TimeOfDay = 12f;
      CustomBattleHelper.StartGame(data);
    }

    private CustomBattleCombatant GetEnemyParty()
    {
      BasicCultureObject culture = MBObjectManager.Instance.GetObject<BasicCultureObject>("galactic_republic");
      BasicCharacterObject characterObject = MBObjectManager.Instance.GetObject<BasicCharacterObject>("republic_clone_cadet");
      CustomBattleCombatant enemyParty = new CustomBattleCombatant(new TextObject("Enemy Party"), culture, Banner.CreateRandomBanner());
      enemyParty.AddCharacter(characterObject, 1);
      enemyParty.Side = BattleSideEnum.Attacker;
      return enemyParty;
    }

    private CustomBattleCombatant GetPlayerParty(BasicCharacterObject playerCharacter)
    {
      MBReadOnlyList<BasicCharacterObject> objectTypeList = MBObjectManager.Instance.GetObjectTypeList<BasicCharacterObject>();
      BasicCultureObject culture0 = MBObjectManager.Instance.GetObject<BasicCultureObject>("galactic_republic");
      IEnumerable<BasicCharacterObject> basicCharacterObjects = objectTypeList.Where<BasicCharacterObject>((Func<BasicCharacterObject, bool>) (x => (x.IsSoldier || x.IsHero) && x.Culture == culture0 && x != playerCharacter));
      CustomBattleCombatant playerParty = new CustomBattleCombatant(new TextObject("Player Party"), culture0, Banner.CreateRandomBanner());
      playerParty.AddCharacter(playerCharacter, 1);
      playerParty.SetGeneral(playerCharacter);
      playerParty.Side = BattleSideEnum.Defender;
      foreach (BasicCharacterObject characterObject in basicCharacterObjects)
      {
        int number = 1;
        playerParty.AddCharacter(characterObject, number);
      }
      return playerParty;
    }

    private BasicCharacterObject SelectPlayer() => MBObjectManager.Instance.GetObject<BasicCharacterObject>("republic_clone_cadet");
  }
}
