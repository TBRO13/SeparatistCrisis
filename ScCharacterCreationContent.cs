// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.ScCharacterCreationContent
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SeparatistCrisis
{
  public class ScCharacterCreationContent : CharacterCreationContentBase
  {
    protected const int FocusToAddYouthStart = 2;
    protected const int FocusToAddAdultStart = 4;
    protected const int FocusToAddMiddleAgedStart = 6;
    protected const int FocusToAddElderlyStart = 8;
    protected const int AttributeToAddYouthStart = 1;
    protected const int AttributeToAddAdultStart = 2;
    protected const int AttributeToAddMiddleAgedStart = 3;
    protected const int AttributeToAddElderlyStart = 4;
    protected ScCharacterCreationContent.SandboxAgeOptions _startingAge = ScCharacterCreationContent.SandboxAgeOptions.YoungAdult;
    protected ScCharacterCreationContent.OccupationTypes _familyOccupationType;
    protected TextObject _educationIntroductoryText = new TextObject("{=!}{EDUCATION_INTRO}");
    protected TextObject _youthIntroductoryText = new TextObject("{=!}{YOUTH_INTRO}");

    public override TextObject ReviewPageDescription => new TextObject("{=W6pKpEoT}You prepare to set off for a grand adventure in Calradia! Here is your character. Continue if you are ready, or go back to make changes.");

    public override IEnumerable<Type> CharacterCreationStages
    {
      get
      {
        yield return typeof (CharacterCreationCultureStage);
        yield return typeof (CharacterCreationFaceGeneratorStage);
        yield return typeof (CharacterCreationGenericStage);
        yield return typeof (CharacterCreationBannerEditorStage);
        yield return typeof (CharacterCreationClanNamingStage);
        yield return typeof (CharacterCreationReviewStage);
        yield return typeof (CharacterCreationOptionsStage);
      }
    }

    protected override void OnCultureSelected()
    {
      this.SelectedTitleType = 1;
      this.SelectedParentType = 0;
      TextObject clanNameforPlayer = FactionHelper.GenerateClanNameforPlayer();
      Clan.PlayerClan.ChangeClanName(clanNameforPlayer, clanNameforPlayer);
    }

    public override int GetSelectedParentType() => this.SelectedParentType;

    public override void OnCharacterCreationFinalized()
    {
      CultureObject culture = this.GetSelectedCulture();
      MobileParty.MainParty.Position2D = Settlement.FindFirst((Func<Settlement, bool>) (s => s.Culture == culture)).GatePosition;
      ((MapState) GameStateManager.Current.ActiveState).Handler.TeleportCameraToMainParty();
      this.SetHeroAge((float) this._startingAge);
    }

    protected override void OnInitialized(CharacterCreation characterCreation)
    {
      this.AddParentsMenu(characterCreation);
      this.AddChildhoodMenu(characterCreation);
      this.AddEducationMenu(characterCreation);
      this.AddYouthMenu(characterCreation);
      this.AddAdulthoodMenu(characterCreation);
      this.AddAgeSelectionMenu(characterCreation);
    }

    protected override void OnApplyCulture()
    {
    }

    protected void AddParentsMenu(CharacterCreation characterCreation)
    {
      CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("{=b4lDDcli}Family"), new TextObject("{=XgFU1pCx}You were born into a family of..."), new CharacterCreationOnInit(this.ParentsOnInit));
      this.AddRepublicParentOptions(menu);
      this.AddSeparatistParentOptions(menu);
      this.AddMandalorianParentOptions(menu);
      characterCreation.AddNewMenu(menu);
    }

    private void AddMandalorianParentOptions(CharacterCreationMenu menu)
    {
      CharacterCreationCategory creationCategory = menu.AddMenuCategory(new CharacterCreationOnCondition(this.MandalorianParentsOnCondition));
      TextObject textObject1 = new TextObject("{=mc78FEbA}A boyar's companions");
      MBList<SkillObject> skillObjectList1 = new MBList<SkillObject>();
      skillObjectList1.Add(DefaultSkills.Riding);
      skillObjectList1.Add(DefaultSkills.TwoHanded);
      CharacterAttribute social = DefaultCharacterAttributes.Social;
      int focusToAdd1 = this.FocusToAdd;
      int skillLevelToAdd1 = this.SkillLevelToAdd;
      int attributeLevelToAdd1 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect1 = new CharacterCreationOnSelect(this.MandalorianBoyarsCompanionOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects1 = new CharacterCreationApplyFinalEffects(this.MandalorianBoyarsCompanionOnApply);
      TextObject textObject2 = new TextObject("{=hob3WVkU}Your father was a member of a boyar's druzhina, the 'companions' that make up his retinue. He sat at his lord's table in the great hall, oversaw the boyar's estates, and stood by his side in the center of the shield wall in battle.");
      creationCategory.AddCategoryOption(textObject1, skillObjectList1, social, focusToAdd1, skillLevelToAdd1, attributeLevelToAdd1, (CharacterCreationOnCondition) null, creationOnSelect1, applyFinalEffects1, textObject2, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject3 = new TextObject("{=HqzVBfpl}Urban traders");
      MBList<SkillObject> skillObjectList2 = new MBList<SkillObject>();
      skillObjectList2.Add(DefaultSkills.Trade);
      skillObjectList2.Add(DefaultSkills.Tactics);
      CharacterAttribute cunning = DefaultCharacterAttributes.Cunning;
      int focusToAdd2 = this.FocusToAdd;
      int skillLevelToAdd2 = this.SkillLevelToAdd;
      int attributeLevelToAdd2 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect2 = new CharacterCreationOnSelect(this.MandalorianTraderOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects2 = new CharacterCreationApplyFinalEffects(this.MandalorianTraderOnApply);
      TextObject textObject4 = new TextObject("{=bjVMtW3W}Your family were merchants who lived in one of Sturgia's great river ports, organizing the shipment of the north's bounty of furs, honey and other goods to faraway lands.");
      creationCategory.AddCategoryOption(textObject3, skillObjectList2, cunning, focusToAdd2, skillLevelToAdd2, attributeLevelToAdd2, (CharacterCreationOnCondition) null, creationOnSelect2, applyFinalEffects2, textObject4, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject5 = new TextObject("{=zrpqSWSh}Free farmers");
      MBList<SkillObject> skillObjectList3 = new MBList<SkillObject>();
      skillObjectList3.Add(DefaultSkills.Athletics);
      skillObjectList3.Add(DefaultSkills.Polearm);
      CharacterAttribute endurance = DefaultCharacterAttributes.Endurance;
      int focusToAdd3 = this.FocusToAdd;
      int skillLevelToAdd3 = this.SkillLevelToAdd;
      int attributeLevelToAdd3 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect3 = new CharacterCreationOnSelect(this.MandalorianFreemanOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects3 = new CharacterCreationApplyFinalEffects(this.MandalorianFreemanOnApply);
      TextObject textObject6 = new TextObject("{=Mcd3ZyKq}Your family had just enough land to feed themselves and make a small profit. People like them were the pillars of the kingdom's economy, as well as the backbone of the levy.");
      creationCategory.AddCategoryOption(textObject5, skillObjectList3, endurance, focusToAdd3, skillLevelToAdd3, attributeLevelToAdd3, (CharacterCreationOnCondition) null, creationOnSelect3, applyFinalEffects3, textObject6, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject7 = new TextObject("{=v48N6h1t}Urban artisans");
      MBList<SkillObject> skillObjectList4 = new MBList<SkillObject>();
      skillObjectList4.Add(DefaultSkills.Crafting);
      skillObjectList4.Add(DefaultSkills.OneHanded);
      CharacterAttribute intelligence = DefaultCharacterAttributes.Intelligence;
      int focusToAdd4 = this.FocusToAdd;
      int skillLevelToAdd4 = this.SkillLevelToAdd;
      int attributeLevelToAdd4 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect4 = new CharacterCreationOnSelect(this.MandalorianArtisanOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects4 = new CharacterCreationApplyFinalEffects(this.MandalorianArtisanOnApply);
      TextObject textObject8 = new TextObject("{=ueCm5y1C}Your family owned their own workshop in a city, making goods from raw materials brought in from the countryside. Your father played an active if minor role in the town council, and also served in the militia.");
      creationCategory.AddCategoryOption(textObject7, skillObjectList4, intelligence, focusToAdd4, skillLevelToAdd4, attributeLevelToAdd4, (CharacterCreationOnCondition) null, creationOnSelect4, applyFinalEffects4, textObject8, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject9 = new TextObject("{=YcnK0Thk}Hunters");
      MBList<SkillObject> skillObjectList5 = new MBList<SkillObject>();
      skillObjectList5.Add(DefaultSkills.Scouting);
      skillObjectList5.Add(DefaultSkills.Bow);
      CharacterAttribute vigor = DefaultCharacterAttributes.Vigor;
      int focusToAdd5 = this.FocusToAdd;
      int skillLevelToAdd5 = this.SkillLevelToAdd;
      int attributeLevelToAdd5 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect5 = new CharacterCreationOnSelect(this.MandalorianHunterOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects5 = new CharacterCreationApplyFinalEffects(this.MandalorianHunterOnApply);
      TextObject textObject10 = new TextObject("{=WyZ2UtFF}Your family had no taste for the authority of the boyars. They made their living deep in the woods, slashing and burning fields which they tended for a year or two before moving on. They hunted and trapped fox, hare, ermine, and other fur-bearing animals.");
      creationCategory.AddCategoryOption(textObject9, skillObjectList5, vigor, focusToAdd5, skillLevelToAdd5, attributeLevelToAdd5, (CharacterCreationOnCondition) null, creationOnSelect5, applyFinalEffects5, textObject10, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject11 = new TextObject("{=TPoK3GSj}Vagabonds");
      MBList<SkillObject> skillObjectList6 = new MBList<SkillObject>();
      skillObjectList6.Add(DefaultSkills.Roguery);
      skillObjectList6.Add(DefaultSkills.Throwing);
      CharacterAttribute control = DefaultCharacterAttributes.Control;
      int focusToAdd6 = this.FocusToAdd;
      int skillLevelToAdd6 = this.SkillLevelToAdd;
      int attributeLevelToAdd6 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect6 = new CharacterCreationOnSelect(this.MandalorianVagabondOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects6 = new CharacterCreationApplyFinalEffects(this.MandalorianVagabondOnApply);
      TextObject textObject12 = new TextObject("{=2SDWhGmQ}Your family numbered among the poor migrants living in the slums that grow up outside the walls of the river cities, making whatever money they could from a variety of odd jobs. Sometimes they did services for one of the region's many criminal gangs.");
      creationCategory.AddCategoryOption(textObject11, skillObjectList6, control, focusToAdd6, skillLevelToAdd6, attributeLevelToAdd6, (CharacterCreationOnCondition) null, creationOnSelect6, applyFinalEffects6, textObject12, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
    }

    private void AddSeparatistParentOptions(CharacterCreationMenu menu)
    {
      CharacterCreationCategory creationCategory1 = menu.AddMenuCategory(new CharacterCreationOnCondition(this.SeparatistParentsOnCondition));
      CharacterCreationCategory creationCategory2 = creationCategory1;
      TextObject textObject1 = new TextObject("{=2TptWc4m}A baron's retainers");
      MBList<SkillObject> skillObjectList1 = new MBList<SkillObject>();
      skillObjectList1.Add(DefaultSkills.Riding);
      skillObjectList1.Add(DefaultSkills.Polearm);
      CharacterAttribute social = DefaultCharacterAttributes.Social;
      int focusToAdd1 = this.FocusToAdd;
      int skillLevelToAdd1 = this.SkillLevelToAdd;
      int attributeLevelToAdd1 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect1 = new CharacterCreationOnSelect(this.SeparatistBaronsRetainerOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects1 = new CharacterCreationApplyFinalEffects(this.SeparatistBaronsRetainerOnApply);
      TextObject textObject2 = new TextObject("{=0Suu1Q9q}Your father was a bailiff for a local feudal magnate. He looked after his liege's estates, resolved disputes in the village, and helped train the village levy. He rode with the lord's cavalry, fighting as an armored knight.");
      creationCategory2.AddCategoryOption(textObject1, skillObjectList1, social, focusToAdd1, skillLevelToAdd1, attributeLevelToAdd1, (CharacterCreationOnCondition) null, creationOnSelect1, applyFinalEffects1, textObject2, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory3 = creationCategory1;
      TextObject textObject3 = new TextObject("{=651FhzdR}Urban merchants");
      MBList<SkillObject> skillObjectList2 = new MBList<SkillObject>();
      skillObjectList2.Add(DefaultSkills.Trade);
      skillObjectList2.Add(DefaultSkills.Charm);
      CharacterAttribute intelligence = DefaultCharacterAttributes.Intelligence;
      int focusToAdd2 = this.FocusToAdd;
      int skillLevelToAdd2 = this.SkillLevelToAdd;
      int attributeLevelToAdd2 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect2 = new CharacterCreationOnSelect(this.SeparatistMerchantOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects2 = new CharacterCreationApplyFinalEffects(this.SeparatistMerchantOnApply);
      TextObject textObject4 = new TextObject("{=qNZFkxJb}Your family were merchants in one of the main cities of the kingdom. They organized caravans to nearby towns, were active in the local merchant's guild.");
      creationCategory3.AddCategoryOption(textObject3, skillObjectList2, intelligence, focusToAdd2, skillLevelToAdd2, attributeLevelToAdd2, (CharacterCreationOnCondition) null, creationOnSelect2, applyFinalEffects2, textObject4, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory4 = creationCategory1;
      TextObject textObject5 = new TextObject("{=RDfXuVxT}Yeomen");
      MBList<SkillObject> skillObjectList3 = new MBList<SkillObject>();
      skillObjectList3.Add(DefaultSkills.Polearm);
      skillObjectList3.Add(DefaultSkills.Crossbow);
      CharacterAttribute endurance = DefaultCharacterAttributes.Endurance;
      int focusToAdd3 = this.FocusToAdd;
      int skillLevelToAdd3 = this.SkillLevelToAdd;
      int attributeLevelToAdd3 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect3 = new CharacterCreationOnSelect(this.SeparatistYeomanOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects3 = new CharacterCreationApplyFinalEffects(this.SeparatistYeomanOnApply);
      TextObject textObject6 = new TextObject("{=BLZ4mdhb}Your family were small farmers with just enough land to feed themselves and make a small profit. People like them were the pillars of the kingdom's economy, as well as the backbone of the levy.");
      creationCategory4.AddCategoryOption(textObject5, skillObjectList3, endurance, focusToAdd3, skillLevelToAdd3, attributeLevelToAdd3, (CharacterCreationOnCondition) null, creationOnSelect3, applyFinalEffects3, textObject6, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory5 = creationCategory1;
      TextObject textObject7 = new TextObject("{=p2KIhGbE}Urban blacksmith");
      MBList<SkillObject> skillObjectList4 = new MBList<SkillObject>();
      skillObjectList4.Add(DefaultSkills.Crafting);
      skillObjectList4.Add(DefaultSkills.TwoHanded);
      CharacterAttribute vigor = DefaultCharacterAttributes.Vigor;
      int focusToAdd4 = this.FocusToAdd;
      int skillLevelToAdd4 = this.SkillLevelToAdd;
      int attributeLevelToAdd4 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect4 = new CharacterCreationOnSelect(this.SeparatistBlacksmithOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects4 = new CharacterCreationApplyFinalEffects(this.SeparatistBlacksmithOnApply);
      TextObject textObject8 = new TextObject("{=btsMpRcA}Your family owned a smithy in a city. Your father played an active if minor role in the town council, and also served in the militia.");
      creationCategory5.AddCategoryOption(textObject7, skillObjectList4, vigor, focusToAdd4, skillLevelToAdd4, attributeLevelToAdd4, (CharacterCreationOnCondition) null, creationOnSelect4, applyFinalEffects4, textObject8, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory6 = creationCategory1;
      TextObject textObject9 = new TextObject("{=YcnK0Thk}Hunters");
      MBList<SkillObject> skillObjectList5 = new MBList<SkillObject>();
      skillObjectList5.Add(DefaultSkills.Scouting);
      skillObjectList5.Add(DefaultSkills.Crossbow);
      CharacterAttribute control = DefaultCharacterAttributes.Control;
      int focusToAdd5 = this.FocusToAdd;
      int skillLevelToAdd5 = this.SkillLevelToAdd;
      int attributeLevelToAdd5 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect5 = new CharacterCreationOnSelect(this.SeparatistHunterOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects5 = new CharacterCreationApplyFinalEffects(this.SeparatistHunterOnApply);
      TextObject textObject10 = new TextObject("{=yRFSzSDZ}Your family lived in a village, but did not own their own land. Instead, your father supplemented paid jobs with long trips in the woods, hunting and trapping, always keeping a wary eye for the lord's game wardens.");
      creationCategory6.AddCategoryOption(textObject9, skillObjectList5, control, focusToAdd5, skillLevelToAdd5, attributeLevelToAdd5, (CharacterCreationOnCondition) null, creationOnSelect5, applyFinalEffects5, textObject10, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory7 = creationCategory1;
      TextObject textObject11 = new TextObject("{=ipQP6aVi}Mercenaries");
      MBList<SkillObject> skillObjectList6 = new MBList<SkillObject>();
      skillObjectList6.Add(DefaultSkills.Roguery);
      skillObjectList6.Add(DefaultSkills.Crossbow);
      CharacterAttribute cunning = DefaultCharacterAttributes.Cunning;
      int focusToAdd6 = this.FocusToAdd;
      int skillLevelToAdd6 = this.SkillLevelToAdd;
      int attributeLevelToAdd6 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect6 = new CharacterCreationOnSelect(this.SeparatistMercenaryOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects6 = new CharacterCreationApplyFinalEffects(this.SeparatistMercenaryOnApply);
      TextObject textObject12 = new TextObject("{=yYhX6JQC}Your father joined one of Vlandia's many mercenary companies, composed of men who got such a taste for war in their lord's service that they never took well to peace. Their crossbowmen were much valued across Calradia. Your mother was a camp follower, taking you along in the wake of bloody campaigns.");
      creationCategory7.AddCategoryOption(textObject11, skillObjectList6, cunning, focusToAdd6, skillLevelToAdd6, attributeLevelToAdd6, (CharacterCreationOnCondition) null, creationOnSelect6, applyFinalEffects6, textObject12, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
    }

    private void AddRepublicParentOptions(CharacterCreationMenu menu)
    {
      CharacterCreationCategory creationCategory1 = menu.AddMenuCategory(new CharacterCreationOnCondition(this.RepublicParentsOnCondition));
      CharacterCreationCategory creationCategory2 = creationCategory1;
      TextObject textObject1 = new TextObject("{=InN5ZZt3}A landlord's retainers");
      MBList<SkillObject> skillObjectList1 = new MBList<SkillObject>();
      skillObjectList1.Add(DefaultSkills.Riding);
      skillObjectList1.Add(DefaultSkills.Polearm);
      CharacterAttribute vigor = DefaultCharacterAttributes.Vigor;
      int focusToAdd1 = this.FocusToAdd;
      int skillLevelToAdd1 = this.SkillLevelToAdd;
      int attributeLevelToAdd1 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect1 = new CharacterCreationOnSelect(this.RepublicLandlordsRetainerOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects1 = new CharacterCreationApplyFinalEffects(this.RepublicLandlordsRetainerOnApply);
      TextObject textObject2 = new TextObject("{=ivKl4mV2}Your father was a trusted lieutenant of the local landowning aristocrat. He rode with the lord's cavalry, fighting as an armored lancer.");
      creationCategory2.AddCategoryOption(textObject1, skillObjectList1, vigor, focusToAdd1, skillLevelToAdd1, attributeLevelToAdd1, (CharacterCreationOnCondition) null, creationOnSelect1, applyFinalEffects1, textObject2, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory3 = creationCategory1;
      TextObject textObject3 = new TextObject("{=651FhzdR}Urban merchants");
      MBList<SkillObject> skillObjectList2 = new MBList<SkillObject>();
      skillObjectList2.Add(DefaultSkills.Trade);
      skillObjectList2.Add(DefaultSkills.Charm);
      CharacterAttribute social = DefaultCharacterAttributes.Social;
      int focusToAdd2 = this.FocusToAdd;
      int skillLevelToAdd2 = this.SkillLevelToAdd;
      int attributeLevelToAdd2 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect2 = new CharacterCreationOnSelect(this.RepublicMerchantOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects2 = new CharacterCreationApplyFinalEffects(this.RepublicMerchantOnApply);
      TextObject textObject4 = new TextObject("{=FQntPChs}Your family were merchants in one of the main cities of the Empire. They sometimes organized caravans to nearby towns, and discussed issues in the town council.");
      creationCategory3.AddCategoryOption(textObject3, skillObjectList2, social, focusToAdd2, skillLevelToAdd2, attributeLevelToAdd2, (CharacterCreationOnCondition) null, creationOnSelect2, applyFinalEffects2, textObject4, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory4 = creationCategory1;
      TextObject textObject5 = new TextObject("{=sb4gg8Ak}Freeholders");
      MBList<SkillObject> skillObjectList3 = new MBList<SkillObject>();
      skillObjectList3.Add(DefaultSkills.Athletics);
      skillObjectList3.Add(DefaultSkills.Polearm);
      CharacterAttribute endurance = DefaultCharacterAttributes.Endurance;
      int focusToAdd3 = this.FocusToAdd;
      int skillLevelToAdd3 = this.SkillLevelToAdd;
      int attributeLevelToAdd3 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect3 = new CharacterCreationOnSelect(this.RepublicFreeholderOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects3 = new CharacterCreationApplyFinalEffects(this.RepublicFreeholderOnApply);
      TextObject textObject6 = new TextObject("{=09z8Q08f}Your family were small farmers with just enough land to feed themselves and make a small profit. People like them were the pillars of the imperial rural economy, as well as the backbone of the levy.");
      creationCategory4.AddCategoryOption(textObject5, skillObjectList3, endurance, focusToAdd3, skillLevelToAdd3, attributeLevelToAdd3, (CharacterCreationOnCondition) null, creationOnSelect3, applyFinalEffects3, textObject6, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory5 = creationCategory1;
      TextObject textObject7 = new TextObject("{=v48N6h1t}Urban artisans");
      MBList<SkillObject> skillObjectList4 = new MBList<SkillObject>();
      skillObjectList4.Add(DefaultSkills.Crafting);
      skillObjectList4.Add(DefaultSkills.Crossbow);
      CharacterAttribute intelligence = DefaultCharacterAttributes.Intelligence;
      int focusToAdd4 = this.FocusToAdd;
      int skillLevelToAdd4 = this.SkillLevelToAdd;
      int attributeLevelToAdd4 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect4 = new CharacterCreationOnSelect(this.RepublicArtisanOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects4 = new CharacterCreationApplyFinalEffects(this.RepublicArtisanOnApply);
      TextObject textObject8 = new TextObject("{=ZKynvffv}Your family owned their own workshop in a city, making goods from raw materials brought in from the countryside. Your father played an active if minor role in the town council, and also served in the militia.");
      creationCategory5.AddCategoryOption(textObject7, skillObjectList4, intelligence, focusToAdd4, skillLevelToAdd4, attributeLevelToAdd4, (CharacterCreationOnCondition) null, creationOnSelect4, applyFinalEffects4, textObject8, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory6 = creationCategory1;
      TextObject textObject9 = new TextObject("{=7eWmU2mF}Foresters");
      MBList<SkillObject> skillObjectList5 = new MBList<SkillObject>();
      skillObjectList5.Add(DefaultSkills.Scouting);
      skillObjectList5.Add(DefaultSkills.Bow);
      CharacterAttribute control = DefaultCharacterAttributes.Control;
      int focusToAdd5 = this.FocusToAdd;
      int skillLevelToAdd5 = this.SkillLevelToAdd;
      int attributeLevelToAdd5 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect5 = new CharacterCreationOnSelect(this.RepublicWoodsmanOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects5 = new CharacterCreationApplyFinalEffects(this.RepublicWoodsmanOnApply);
      TextObject textObject10 = new TextObject("{=yRFSzSDZ}Your family lived in a village, but did not own their own land. Instead, your father supplemented paid jobs with long trips in the woods, hunting and trapping, always keeping a wary eye for the lord's game wardens.");
      creationCategory6.AddCategoryOption(textObject9, skillObjectList5, control, focusToAdd5, skillLevelToAdd5, attributeLevelToAdd5, (CharacterCreationOnCondition) null, creationOnSelect5, applyFinalEffects5, textObject10, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      CharacterCreationCategory creationCategory7 = creationCategory1;
      TextObject textObject11 = new TextObject("{=aEke8dSb}Urban vagabonds");
      MBList<SkillObject> skillObjectList6 = new MBList<SkillObject>();
      skillObjectList6.Add(DefaultSkills.Roguery);
      skillObjectList6.Add(DefaultSkills.Throwing);
      CharacterAttribute cunning = DefaultCharacterAttributes.Cunning;
      int focusToAdd6 = this.FocusToAdd;
      int skillLevelToAdd6 = this.SkillLevelToAdd;
      int attributeLevelToAdd6 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect6 = new CharacterCreationOnSelect(this.RepublicVagabondOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects6 = new CharacterCreationApplyFinalEffects(this.RepublicVagabondOnApply);
      TextObject textObject12 = new TextObject("{=Jvf6K7TZ}Your family numbered among the many poor migrants living in the slums that grow up outside the walls of imperial cities, making whatever money they could from a variety of odd jobs. Sometimes they did service for one of the Empire's many criminal gangs, and you had an early look at the dark side of life.");
      creationCategory7.AddCategoryOption(textObject11, skillObjectList6, cunning, focusToAdd6, skillLevelToAdd6, attributeLevelToAdd6, (CharacterCreationOnCondition) null, creationOnSelect6, applyFinalEffects6, textObject12, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
    }

    protected void AddChildhoodMenu(CharacterCreation characterCreation)
    {
      CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("{=8Yiwt1z6}Early Childhood"), new TextObject("{=character_creation_content_16}As a child you were noted for..."), new CharacterCreationOnInit(this.ChildhoodOnInit));
      CharacterCreationCategory creationCategory = menu.AddMenuCategory();
      TextObject textObject1 = new TextObject("{=kmM68Qx4}your leadership skills.");
      MBList<SkillObject> skillObjectList1 = new MBList<SkillObject>();
      skillObjectList1.Add(DefaultSkills.Leadership);
      skillObjectList1.Add(DefaultSkills.Tactics);
      CharacterAttribute cunning = DefaultCharacterAttributes.Cunning;
      int focusToAdd1 = this.FocusToAdd;
      int skillLevelToAdd1 = this.SkillLevelToAdd;
      int attributeLevelToAdd1 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect1 = new CharacterCreationOnSelect(ScCharacterCreationContent.ChildhoodYourLeadershipSkillsOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects1 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.ChildhoodGoodLeadingOnApply);
      TextObject textObject2 = new TextObject("{=FfNwXtii}If the wolf pup gang of your early childhood had an alpha, it was definitely you. All the other kids followed your lead as you decided what to play and where to play, and led them in games and mischief.");
      creationCategory.AddCategoryOption(textObject1, skillObjectList1, cunning, focusToAdd1, skillLevelToAdd1, attributeLevelToAdd1, (CharacterCreationOnCondition) null, creationOnSelect1, applyFinalEffects1, textObject2, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject3 = new TextObject("{=5HXS8HEY}your brawn.");
      MBList<SkillObject> skillObjectList2 = new MBList<SkillObject>();
      skillObjectList2.Add(DefaultSkills.TwoHanded);
      skillObjectList2.Add(DefaultSkills.Throwing);
      CharacterAttribute vigor = DefaultCharacterAttributes.Vigor;
      int focusToAdd2 = this.FocusToAdd;
      int skillLevelToAdd2 = this.SkillLevelToAdd;
      int attributeLevelToAdd2 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect2 = new CharacterCreationOnSelect(ScCharacterCreationContent.ChildhoodYourBrawnOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects2 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.ChildhoodGoodAthleticsOnApply);
      TextObject textObject4 = new TextObject("{=YKzuGc54}You were big, and other children looked to have you around in any scrap with children from a neighboring village. You pushed a plough and throw an axe like an adult.");
      creationCategory.AddCategoryOption(textObject3, skillObjectList2, vigor, focusToAdd2, skillLevelToAdd2, attributeLevelToAdd2, (CharacterCreationOnCondition) null, creationOnSelect2, applyFinalEffects2, textObject4, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject5 = new TextObject("{=QrYjPUEf}your attention to detail.");
      MBList<SkillObject> skillObjectList3 = new MBList<SkillObject>();
      skillObjectList3.Add(DefaultSkills.Athletics);
      skillObjectList3.Add(DefaultSkills.Bow);
      CharacterAttribute control = DefaultCharacterAttributes.Control;
      int focusToAdd3 = this.FocusToAdd;
      int skillLevelToAdd3 = this.SkillLevelToAdd;
      int attributeLevelToAdd3 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect3 = new CharacterCreationOnSelect(ScCharacterCreationContent.ChildhoodAttentionToDetailOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects3 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.ChildhoodGoodMemoryOnApply);
      TextObject textObject6 = new TextObject("{=JUSHAPnu}You were quick on your feet and attentive to what was going on around you. Usually you could run away from trouble, though you could give a good account of yourself in a fight with other children if cornered.");
      creationCategory.AddCategoryOption(textObject5, skillObjectList3, control, focusToAdd3, skillLevelToAdd3, attributeLevelToAdd3, (CharacterCreationOnCondition) null, creationOnSelect3, applyFinalEffects3, textObject6, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject7 = new TextObject("{=Y3UcaX74}your aptitude for numbers.");
      MBList<SkillObject> skillObjectList4 = new MBList<SkillObject>();
      skillObjectList4.Add(DefaultSkills.Engineering);
      skillObjectList4.Add(DefaultSkills.Trade);
      CharacterAttribute intelligence = DefaultCharacterAttributes.Intelligence;
      int focusToAdd4 = this.FocusToAdd;
      int skillLevelToAdd4 = this.SkillLevelToAdd;
      int attributeLevelToAdd4 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect4 = new CharacterCreationOnSelect(ScCharacterCreationContent.ChildhoodAptitudeForNumbersOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects4 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.ChildhoodGoodMathOnApply);
      TextObject textObject8 = new TextObject("{=DFidSjIf}Most children around you had only the most rudimentary education, but you lingered after class to study letters and mathematics. You were fascinated by the marketplace - weights and measures, tallies and accounts, the chatter about profits and losses.");
      creationCategory.AddCategoryOption(textObject7, skillObjectList4, intelligence, focusToAdd4, skillLevelToAdd4, attributeLevelToAdd4, (CharacterCreationOnCondition) null, creationOnSelect4, applyFinalEffects4, textObject8, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject9 = new TextObject("{=GEYzLuwb}your way with people.");
      MBList<SkillObject> skillObjectList5 = new MBList<SkillObject>();
      skillObjectList5.Add(DefaultSkills.Charm);
      skillObjectList5.Add(DefaultSkills.Leadership);
      CharacterAttribute social = DefaultCharacterAttributes.Social;
      int focusToAdd5 = this.FocusToAdd;
      int skillLevelToAdd5 = this.SkillLevelToAdd;
      int attributeLevelToAdd5 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect5 = new CharacterCreationOnSelect(ScCharacterCreationContent.ChildhoodWayWithPeopleOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects5 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.ChildhoodGoodMannersOnApply);
      TextObject textObject10 = new TextObject("{=w2TEQq26}You were always attentive to other people, good at guessing their motivations. You studied how individuals were swayed, and tried out what you learned from adults on your friends.");
      creationCategory.AddCategoryOption(textObject9, skillObjectList5, social, focusToAdd5, skillLevelToAdd5, attributeLevelToAdd5, (CharacterCreationOnCondition) null, creationOnSelect5, applyFinalEffects5, textObject10, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject11 = new TextObject("{=MEgLE2kj}your skill with horses.");
      MBList<SkillObject> skillObjectList6 = new MBList<SkillObject>();
      skillObjectList6.Add(DefaultSkills.Riding);
      skillObjectList6.Add(DefaultSkills.Medicine);
      CharacterAttribute endurance = DefaultCharacterAttributes.Endurance;
      int focusToAdd6 = this.FocusToAdd;
      int skillLevelToAdd6 = this.SkillLevelToAdd;
      int attributeLevelToAdd6 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect6 = new CharacterCreationOnSelect(ScCharacterCreationContent.ChildhoodSkillsWithHorsesOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects6 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.ChildhoodAffinityWithAnimalsOnApply);
      TextObject textObject12 = new TextObject("{=ngazFofr}You were always drawn to animals, and spent as much time as possible hanging out in the village stables. You could calm horses, and were sometimes called upon to break in new colts. You learned the basics of veterinary arts, much of which is applicable to humans as well.");
      creationCategory.AddCategoryOption(textObject11, skillObjectList6, endurance, focusToAdd6, skillLevelToAdd6, attributeLevelToAdd6, (CharacterCreationOnCondition) null, creationOnSelect6, applyFinalEffects6, textObject12, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      characterCreation.AddNewMenu(menu);
    }

    protected void AddEducationMenu(CharacterCreation characterCreation)
    {
      CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("{=rcoueCmk}Adolescence"), this._educationIntroductoryText, new CharacterCreationOnInit(this.EducationOnInit));
      CharacterCreationCategory creationCategory = menu.AddMenuCategory();
      TextObject textObject1 = new TextObject("{=RKVNvimC}herded the sheep.");
      MBList<SkillObject> skillObjectList1 = new MBList<SkillObject>();
      skillObjectList1.Add(DefaultSkills.Athletics);
      skillObjectList1.Add(DefaultSkills.Throwing);
      CharacterAttribute control1 = DefaultCharacterAttributes.Control;
      int focusToAdd1 = this.FocusToAdd;
      int skillLevelToAdd1 = this.SkillLevelToAdd;
      int attributeLevelToAdd1 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition1 = new CharacterCreationOnCondition(this.RuralAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect1 = new CharacterCreationOnSelect(this.RuralAdolescenceHerderOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects1 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.RuralAdolescenceHerderOnApply);
      TextObject textObject2 = new TextObject("{=KfaqPpbK}You went with other fleet-footed youths to take the villages' sheep, goats or cattle to graze in pastures near the village. You were in charge of chasing down stray beasts, and always kept a big stone on hand to be hurled at lurking predators if necessary.");
      creationCategory.AddCategoryOption(textObject1, skillObjectList1, control1, focusToAdd1, skillLevelToAdd1, attributeLevelToAdd1, creationOnCondition1, creationOnSelect1, applyFinalEffects1, textObject2, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject3 = new TextObject("{=bTKiN0hr}worked in the village smithy.");
      MBList<SkillObject> skillObjectList2 = new MBList<SkillObject>();
      skillObjectList2.Add(DefaultSkills.TwoHanded);
      skillObjectList2.Add(DefaultSkills.Crafting);
      CharacterAttribute vigor1 = DefaultCharacterAttributes.Vigor;
      int focusToAdd2 = this.FocusToAdd;
      int skillLevelToAdd2 = this.SkillLevelToAdd;
      int attributeLevelToAdd2 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition2 = new CharacterCreationOnCondition(this.RuralAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect2 = new CharacterCreationOnSelect(this.RuralAdolescenceSmithyOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects2 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.RuralAdolescenceSmithyOnApply);
      TextObject textObject4 = new TextObject("{=y6j1bJTH}You were apprenticed to the local smith. You learned how to heat and forge metal, hammering for hours at a time until your muscles ached.");
      creationCategory.AddCategoryOption(textObject3, skillObjectList2, vigor1, focusToAdd2, skillLevelToAdd2, attributeLevelToAdd2, creationOnCondition2, creationOnSelect2, applyFinalEffects2, textObject4, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject5 = new TextObject("{=tI8ZLtoA}repaired projects.");
      MBList<SkillObject> skillObjectList3 = new MBList<SkillObject>();
      skillObjectList3.Add(DefaultSkills.Crafting);
      skillObjectList3.Add(DefaultSkills.Engineering);
      CharacterAttribute intelligence1 = DefaultCharacterAttributes.Intelligence;
      int focusToAdd3 = this.FocusToAdd;
      int skillLevelToAdd3 = this.SkillLevelToAdd;
      int attributeLevelToAdd3 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition3 = new CharacterCreationOnCondition(this.RuralAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect3 = new CharacterCreationOnSelect(this.RuralAdolescenceRepairmanOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects3 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.RuralAdolescenceRepairmanOnApply);
      TextObject textObject6 = new TextObject("{=6LFj919J}You helped dig wells, rethatch houses, and fix broken plows. You learned about the basics of construction, as well as what it takes to keep a farming community prosperous.");
      creationCategory.AddCategoryOption(textObject5, skillObjectList3, intelligence1, focusToAdd3, skillLevelToAdd3, attributeLevelToAdd3, creationOnCondition3, creationOnSelect3, applyFinalEffects3, textObject6, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject7 = new TextObject("{=TRwgSLD2}gathered herbs in the wild.");
      MBList<SkillObject> skillObjectList4 = new MBList<SkillObject>();
      skillObjectList4.Add(DefaultSkills.Medicine);
      skillObjectList4.Add(DefaultSkills.Scouting);
      CharacterAttribute endurance1 = DefaultCharacterAttributes.Endurance;
      int focusToAdd4 = this.FocusToAdd;
      int skillLevelToAdd4 = this.SkillLevelToAdd;
      int attributeLevelToAdd4 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition4 = new CharacterCreationOnCondition(this.RuralAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect4 = new CharacterCreationOnSelect(this.RuralAdolescenceGathererOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects4 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.RuralAdolescenceGathererOnApply);
      TextObject textObject8 = new TextObject("{=9ks4u5cH}You were sent by the village healer up into the hills to look for useful medicinal plants. You learned which herbs healed wounds or brought down a fever, and how to find them.");
      creationCategory.AddCategoryOption(textObject7, skillObjectList4, endurance1, focusToAdd4, skillLevelToAdd4, attributeLevelToAdd4, creationOnCondition4, creationOnSelect4, applyFinalEffects4, textObject8, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject9 = new TextObject("{=T7m7ReTq}hunted small game.");
      MBList<SkillObject> skillObjectList5 = new MBList<SkillObject>();
      skillObjectList5.Add(DefaultSkills.Bow);
      skillObjectList5.Add(DefaultSkills.Tactics);
      CharacterAttribute control2 = DefaultCharacterAttributes.Control;
      int focusToAdd5 = this.FocusToAdd;
      int skillLevelToAdd5 = this.SkillLevelToAdd;
      int attributeLevelToAdd5 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition5 = new CharacterCreationOnCondition(this.RuralAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect5 = new CharacterCreationOnSelect(this.RuralAdolescenceHunterOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects5 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.RuralAdolescenceHunterOnApply);
      TextObject textObject10 = new TextObject("{=RuvSk3QT}You accompanied a local hunter as he went into the wilderness, helping him set up traps and catch small animals.");
      creationCategory.AddCategoryOption(textObject9, skillObjectList5, control2, focusToAdd5, skillLevelToAdd5, attributeLevelToAdd5, creationOnCondition5, creationOnSelect5, applyFinalEffects5, textObject10, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject11 = new TextObject("{=qAbMagWq}sold produce at the market.");
      MBList<SkillObject> skillObjectList6 = new MBList<SkillObject>();
      skillObjectList6.Add(DefaultSkills.Trade);
      skillObjectList6.Add(DefaultSkills.Charm);
      CharacterAttribute social1 = DefaultCharacterAttributes.Social;
      int focusToAdd6 = this.FocusToAdd;
      int skillLevelToAdd6 = this.SkillLevelToAdd;
      int attributeLevelToAdd6 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition6 = new CharacterCreationOnCondition(this.RuralAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect6 = new CharacterCreationOnSelect(this.RuralAdolescenceHelperOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects6 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.RuralAdolescenceHelperOnApply);
      TextObject textObject12 = new TextObject("{=DIgsfYfz}You took your family's goods to the nearest town to sell your produce and buy supplies. It was hard work, but you enjoyed the hubbub of the marketplace.");
      creationCategory.AddCategoryOption(textObject11, skillObjectList6, social1, focusToAdd6, skillLevelToAdd6, attributeLevelToAdd6, creationOnCondition6, creationOnSelect6, applyFinalEffects6, textObject12, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject13 = new TextObject("{=nOfSqRnI}at the town watch's training ground.");
      MBList<SkillObject> skillObjectList7 = new MBList<SkillObject>();
      skillObjectList7.Add(DefaultSkills.Crossbow);
      skillObjectList7.Add(DefaultSkills.Tactics);
      CharacterAttribute control3 = DefaultCharacterAttributes.Control;
      int focusToAdd7 = this.FocusToAdd;
      int skillLevelToAdd7 = this.SkillLevelToAdd;
      int attributeLevelToAdd7 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition7 = new CharacterCreationOnCondition(this.UrbanAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect7 = new CharacterCreationOnSelect(this.UrbanAdolescenceWatcherOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects7 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceWatcherOnApply);
      TextObject textObject14 = new TextObject("{=qnqdEJOv}You watched the town's watch practice shooting and perfect their plans to defend the walls in case of a siege.");
      creationCategory.AddCategoryOption(textObject13, skillObjectList7, control3, focusToAdd7, skillLevelToAdd7, attributeLevelToAdd7, creationOnCondition7, creationOnSelect7, applyFinalEffects7, textObject14, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject15 = new TextObject("{=8a6dnLd2}with the alley gangs.");
      MBList<SkillObject> skillObjectList8 = new MBList<SkillObject>();
      skillObjectList8.Add(DefaultSkills.Roguery);
      skillObjectList8.Add(DefaultSkills.OneHanded);
      CharacterAttribute cunning = DefaultCharacterAttributes.Cunning;
      int focusToAdd8 = this.FocusToAdd;
      int skillLevelToAdd8 = this.SkillLevelToAdd;
      int attributeLevelToAdd8 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition8 = new CharacterCreationOnCondition(this.UrbanAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect8 = new CharacterCreationOnSelect(this.UrbanAdolescenceGangerOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects8 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceGangerOnApply);
      TextObject textObject16 = new TextObject("{=1SUTcF0J}The gang leaders who kept watch over the slums of Calradian cities were always in need of poor youth to run messages and back them up in turf wars, while thrill-seeking merchants' sons and daughters sometimes slummed it in their company as well.");
      creationCategory.AddCategoryOption(textObject15, skillObjectList8, cunning, focusToAdd8, skillLevelToAdd8, attributeLevelToAdd8, creationOnCondition8, creationOnSelect8, applyFinalEffects8, textObject16, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject17 = new TextObject("{=7Hv984Sf}at docks and building sites.");
      MBList<SkillObject> skillObjectList9 = new MBList<SkillObject>();
      skillObjectList9.Add(DefaultSkills.Athletics);
      skillObjectList9.Add(DefaultSkills.Crafting);
      CharacterAttribute vigor2 = DefaultCharacterAttributes.Vigor;
      int focusToAdd9 = this.FocusToAdd;
      int skillLevelToAdd9 = this.SkillLevelToAdd;
      int attributeLevelToAdd9 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition9 = new CharacterCreationOnCondition(this.UrbanAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect9 = new CharacterCreationOnSelect(this.UrbanAdolescenceDockerOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects9 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceDockerOnApply);
      TextObject textObject18 = new TextObject("{=bhdkegZ4}All towns had their share of projects that were constantly in need of both skilled and unskilled labor. You learned how hoists and scaffolds were constructed, how planks and stones were hewn and fitted, and other skills.");
      creationCategory.AddCategoryOption(textObject17, skillObjectList9, vigor2, focusToAdd9, skillLevelToAdd9, attributeLevelToAdd9, creationOnCondition9, creationOnSelect9, applyFinalEffects9, textObject18, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject19 = new TextObject("{=kbcwb5TH}in the markets and caravanserais.");
      MBList<SkillObject> skillObjectList10 = new MBList<SkillObject>();
      skillObjectList10.Add(DefaultSkills.Trade);
      skillObjectList10.Add(DefaultSkills.Charm);
      CharacterAttribute social2 = DefaultCharacterAttributes.Social;
      int focusToAdd10 = this.FocusToAdd;
      int skillLevelToAdd10 = this.SkillLevelToAdd;
      int attributeLevelToAdd10 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition10 = new CharacterCreationOnCondition(this.UrbanPoorAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect10 = new CharacterCreationOnSelect(this.UrbanAdolescenceMarketerOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects10 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceMarketerOnApply);
      TextObject textObject20 = new TextObject("{=lLJh7WAT}You worked in the marketplace, selling trinkets and drinks to busy shoppers.");
      creationCategory.AddCategoryOption(textObject19, skillObjectList10, social2, focusToAdd10, skillLevelToAdd10, attributeLevelToAdd10, creationOnCondition10, creationOnSelect10, applyFinalEffects10, textObject20, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject21 = new TextObject("{=kbcwb5TH}in the markets and caravanserais.");
      MBList<SkillObject> skillObjectList11 = new MBList<SkillObject>();
      skillObjectList11.Add(DefaultSkills.Trade);
      skillObjectList11.Add(DefaultSkills.Charm);
      CharacterAttribute social3 = DefaultCharacterAttributes.Social;
      int focusToAdd11 = this.FocusToAdd;
      int skillLevelToAdd11 = this.SkillLevelToAdd;
      int attributeLevelToAdd11 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition11 = new CharacterCreationOnCondition(this.UrbanRichAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect11 = new CharacterCreationOnSelect(this.UrbanAdolescenceMarketerOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects11 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceMarketerOnApply);
      TextObject textObject22 = new TextObject("{=rmMcwSn8}You helped your family handle their business affairs, going down to the marketplace to make purchases and oversee the arrival of caravans.");
      creationCategory.AddCategoryOption(textObject21, skillObjectList11, social3, focusToAdd11, skillLevelToAdd11, attributeLevelToAdd11, creationOnCondition11, creationOnSelect11, applyFinalEffects11, textObject22, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject23 = new TextObject("{=mfRbx5KE}reading and studying.");
      MBList<SkillObject> skillObjectList12 = new MBList<SkillObject>();
      skillObjectList12.Add(DefaultSkills.Engineering);
      skillObjectList12.Add(DefaultSkills.Leadership);
      CharacterAttribute intelligence2 = DefaultCharacterAttributes.Intelligence;
      int focusToAdd12 = this.FocusToAdd;
      int skillLevelToAdd12 = this.SkillLevelToAdd;
      int attributeLevelToAdd12 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition12 = new CharacterCreationOnCondition(this.UrbanPoorAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect12 = new CharacterCreationOnSelect(this.UrbanAdolescenceTutorOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects12 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceDockerOnApply);
      TextObject textObject24 = new TextObject("{=elQnygal}Your family scraped up the money for a rudimentary schooling and you took full advantage, reading voraciously on history, mathematics, and philosophy and discussing what you read with your tutor and classmates.");
      creationCategory.AddCategoryOption(textObject23, skillObjectList12, intelligence2, focusToAdd12, skillLevelToAdd12, attributeLevelToAdd12, creationOnCondition12, creationOnSelect12, applyFinalEffects12, textObject24, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject25 = new TextObject("{=etG87fB7}with your tutor.");
      MBList<SkillObject> skillObjectList13 = new MBList<SkillObject>();
      skillObjectList13.Add(DefaultSkills.Engineering);
      skillObjectList13.Add(DefaultSkills.Leadership);
      CharacterAttribute intelligence3 = DefaultCharacterAttributes.Intelligence;
      int focusToAdd13 = this.FocusToAdd;
      int skillLevelToAdd13 = this.SkillLevelToAdd;
      int attributeLevelToAdd13 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition13 = new CharacterCreationOnCondition(this.UrbanRichAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect13 = new CharacterCreationOnSelect(this.UrbanAdolescenceTutorOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects13 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceDockerOnApply);
      TextObject textObject26 = new TextObject("{=hXl25avg}Your family arranged for a private tutor and you took full advantage, reading voraciously on history, mathematics, and philosophy and discussing what you read with your tutor and classmates.");
      creationCategory.AddCategoryOption(textObject25, skillObjectList13, intelligence3, focusToAdd13, skillLevelToAdd13, attributeLevelToAdd13, creationOnCondition13, creationOnSelect13, applyFinalEffects13, textObject26, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject27 = new TextObject("{=FKpLEamz}caring for horses.");
      MBList<SkillObject> skillObjectList14 = new MBList<SkillObject>();
      skillObjectList14.Add(DefaultSkills.Riding);
      skillObjectList14.Add(DefaultSkills.Steward);
      CharacterAttribute endurance2 = DefaultCharacterAttributes.Endurance;
      int focusToAdd14 = this.FocusToAdd;
      int skillLevelToAdd14 = this.SkillLevelToAdd;
      int attributeLevelToAdd14 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition14 = new CharacterCreationOnCondition(this.UrbanRichAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect14 = new CharacterCreationOnSelect(this.UrbanAdolescenceHorserOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects14 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceDockerOnApply);
      TextObject textObject28 = new TextObject("{=Ghz90npw}Your family owned a few horses at the town stables and you took charge of their care. Many evenings you would take them out beyond the walls and gallup through the fields, racing other youth.");
      creationCategory.AddCategoryOption(textObject27, skillObjectList14, endurance2, focusToAdd14, skillLevelToAdd14, attributeLevelToAdd14, creationOnCondition14, creationOnSelect14, applyFinalEffects14, textObject28, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject29 = new TextObject("{=vH7GtuuK}working at the stables.");
      MBList<SkillObject> skillObjectList15 = new MBList<SkillObject>();
      skillObjectList15.Add(DefaultSkills.Riding);
      skillObjectList15.Add(DefaultSkills.Steward);
      CharacterAttribute endurance3 = DefaultCharacterAttributes.Endurance;
      int focusToAdd15 = this.FocusToAdd;
      int skillLevelToAdd15 = this.SkillLevelToAdd;
      int attributeLevelToAdd15 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition15 = new CharacterCreationOnCondition(this.UrbanPoorAdolescenceOnCondition);
      CharacterCreationOnSelect creationOnSelect15 = new CharacterCreationOnSelect(this.UrbanAdolescenceHorserOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects15 = new CharacterCreationApplyFinalEffects(ScCharacterCreationContent.UrbanAdolescenceDockerOnApply);
      TextObject textObject30 = new TextObject("{=csUq1RCC}You were employed as a hired hand at the town's stables. The overseers recognized that you had a knack for horses, and you were allowed to exercise them and sometimes even break in new steeds.");
      creationCategory.AddCategoryOption(textObject29, skillObjectList15, endurance3, focusToAdd15, skillLevelToAdd15, attributeLevelToAdd15, creationOnCondition15, creationOnSelect15, applyFinalEffects15, textObject30, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      characterCreation.AddNewMenu(menu);
    }

    protected void AddYouthMenu(CharacterCreation characterCreation)
    {
      CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("{=ok8lSW6M}Youth"), this._youthIntroductoryText, new CharacterCreationOnInit(this.YouthOnInit));
      CharacterCreationCategory creationCategory = menu.AddMenuCategory();
      TextObject textObject1 = new TextObject("{=CITG915d}joined a commander's staff.");
      MBList<SkillObject> skillObjectList1 = new MBList<SkillObject>()
      {
        DefaultSkills.Steward,
        DefaultSkills.Tactics
      };
      CharacterAttribute cunning1 = DefaultCharacterAttributes.Cunning;
      int focusToAdd1 = this.FocusToAdd;
      int skillLevelToAdd1 = this.SkillLevelToAdd;
      int attributeLevelToAdd1 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition1 = new CharacterCreationOnCondition(this.YouthCommanderOnCondition);
      CharacterCreationOnSelect creationOnSelect1 = new CharacterCreationOnSelect(this.YouthCommanderOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects1 = new CharacterCreationApplyFinalEffects(this.YouthCommanderOnApply);
      TextObject textObject2 = new TextObject("{=Ay0G3f7I}Your family arranged for you to be part of the staff of an imperial strategos. You were not given major responsibilities - mostly carrying messages and tending to his horse -- but it did give you a chance to see how campaigns were planned and men were deployed in battle.");
      creationCategory.AddCategoryOption(textObject1, skillObjectList1, cunning1, focusToAdd1, skillLevelToAdd1, attributeLevelToAdd1, creationOnCondition1, creationOnSelect1, applyFinalEffects1, textObject2, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject3 = new TextObject("{=bhE2i6OU}served as a baron's groom.");
      MBList<SkillObject> skillObjectList2 = new MBList<SkillObject>();
      skillObjectList2.Add(DefaultSkills.Steward);
      skillObjectList2.Add(DefaultSkills.Tactics);
      CharacterAttribute cunning2 = DefaultCharacterAttributes.Cunning;
      int focusToAdd2 = this.FocusToAdd;
      int skillLevelToAdd2 = this.SkillLevelToAdd;
      int attributeLevelToAdd2 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition2 = new CharacterCreationOnCondition(this.YouthGroomOnCondition);
      CharacterCreationOnSelect creationOnSelect2 = new CharacterCreationOnSelect(this.YouthGroomOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects2 = new CharacterCreationApplyFinalEffects(this.YouthGroomOnApply);
      TextObject textObject4 = new TextObject("{=iZKtGI6Y}Your family arranged for you to accompany a minor baron of the Vlandian kingdom. You were not given major responsibilities - mostly carrying messages and tending to his horse -- but it did give you a chance to see how campaigns were planned and men were deployed in battle.");
      creationCategory.AddCategoryOption(textObject3, skillObjectList2, cunning2, focusToAdd2, skillLevelToAdd2, attributeLevelToAdd2, creationOnCondition2, creationOnSelect2, applyFinalEffects2, textObject4, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject5 = new TextObject("{=F2bgujPo}were a chieftain's servant.");
      MBList<SkillObject> skillObjectList3 = new MBList<SkillObject>();
      skillObjectList3.Add(DefaultSkills.Steward);
      skillObjectList3.Add(DefaultSkills.Tactics);
      CharacterAttribute cunning3 = DefaultCharacterAttributes.Cunning;
      int focusToAdd3 = this.FocusToAdd;
      int skillLevelToAdd3 = this.SkillLevelToAdd;
      int attributeLevelToAdd3 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition3 = new CharacterCreationOnCondition(this.YouthChieftainOnCondition);
      CharacterCreationOnSelect creationOnSelect3 = new CharacterCreationOnSelect(this.YouthChieftainOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects3 = new CharacterCreationApplyFinalEffects(this.YouthChieftainOnApply);
      TextObject textObject6 = new TextObject("{=7AYJ3SjK}Your family arranged for you to accompany a chieftain of your people. You were not given major responsibilities - mostly carrying messages and tending to his horse -- but it did give you a chance to see how campaigns were planned and men were deployed in battle.");
      creationCategory.AddCategoryOption(textObject5, skillObjectList3, cunning3, focusToAdd3, skillLevelToAdd3, attributeLevelToAdd3, creationOnCondition3, creationOnSelect3, applyFinalEffects3, textObject6, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject7 = new TextObject("{=h2KnarLL}trained with the cavalry.");
      MBList<SkillObject> skillObjectList4 = new MBList<SkillObject>();
      skillObjectList4.Add(DefaultSkills.Riding);
      skillObjectList4.Add(DefaultSkills.Polearm);
      CharacterAttribute endurance1 = DefaultCharacterAttributes.Endurance;
      int focusToAdd4 = this.FocusToAdd;
      int skillLevelToAdd4 = this.SkillLevelToAdd;
      int attributeLevelToAdd4 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition4 = new CharacterCreationOnCondition(this.YouthCavalryOnCondition);
      CharacterCreationOnSelect creationOnSelect4 = new CharacterCreationOnSelect(this.YouthCavalryOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects4 = new CharacterCreationApplyFinalEffects(this.YouthCavalryOnApply);
      TextObject textObject8 = new TextObject("{=7cHsIMLP}You could never have bought the equipment on your own but you were a good enough rider so that the local lord lent you a horse and equipment. You joined the armored cavalry, training with the lance.");
      creationCategory.AddCategoryOption(textObject7, skillObjectList4, endurance1, focusToAdd4, skillLevelToAdd4, attributeLevelToAdd4, creationOnCondition4, creationOnSelect4, applyFinalEffects4, textObject8, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject9 = new TextObject("{=zsC2t5Hb}trained with the hearth guard.");
      MBList<SkillObject> skillObjectList5 = new MBList<SkillObject>();
      skillObjectList5.Add(DefaultSkills.Riding);
      skillObjectList5.Add(DefaultSkills.Polearm);
      CharacterAttribute endurance2 = DefaultCharacterAttributes.Endurance;
      int focusToAdd5 = this.FocusToAdd;
      int skillLevelToAdd5 = this.SkillLevelToAdd;
      int attributeLevelToAdd5 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition5 = new CharacterCreationOnCondition(this.YouthHearthGuardOnCondition);
      CharacterCreationOnSelect creationOnSelect5 = new CharacterCreationOnSelect(this.YouthHearthGuardOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects5 = new CharacterCreationApplyFinalEffects(this.YouthHearthGuardOnApply);
      TextObject textObject10 = new TextObject("{=RmbWW6Bm}You were a big and imposing enough youth that the chief's guard allowed you to train alongside them, in preparation to join them some day.");
      creationCategory.AddCategoryOption(textObject9, skillObjectList5, endurance2, focusToAdd5, skillLevelToAdd5, attributeLevelToAdd5, creationOnCondition5, creationOnSelect5, applyFinalEffects5, textObject10, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject11 = new TextObject("{=aTncHUfL}stood guard with the garrisons.");
      MBList<SkillObject> skillObjectList6 = new MBList<SkillObject>();
      skillObjectList6.Add(DefaultSkills.Crossbow);
      skillObjectList6.Add(DefaultSkills.Engineering);
      CharacterAttribute intelligence1 = DefaultCharacterAttributes.Intelligence;
      int focusToAdd6 = this.FocusToAdd;
      int skillLevelToAdd6 = this.SkillLevelToAdd;
      int attributeLevelToAdd6 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition6 = new CharacterCreationOnCondition(this.YouthGarrisonOnCondition);
      CharacterCreationOnSelect creationOnSelect6 = new CharacterCreationOnSelect(this.YouthGarrisonOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects6 = new CharacterCreationApplyFinalEffects(this.YouthGarrisonOnApply);
      TextObject textObject12 = new TextObject("{=63TAYbkx}Urban troops spend much of their time guarding the town walls. Most of their training was in missile weapons, especially useful during sieges.");
      creationCategory.AddCategoryOption(textObject11, skillObjectList6, intelligence1, focusToAdd6, skillLevelToAdd6, attributeLevelToAdd6, creationOnCondition6, creationOnSelect6, applyFinalEffects6, textObject12, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject13 = new TextObject("{=aTncHUfL}stood guard with the garrisons.");
      MBList<SkillObject> skillObjectList7 = new MBList<SkillObject>();
      skillObjectList7.Add(DefaultSkills.Bow);
      skillObjectList7.Add(DefaultSkills.Engineering);
      CharacterAttribute intelligence2 = DefaultCharacterAttributes.Intelligence;
      int focusToAdd7 = this.FocusToAdd;
      int skillLevelToAdd7 = this.SkillLevelToAdd;
      int attributeLevelToAdd7 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition7 = new CharacterCreationOnCondition(this.YouthOtherGarrisonOnCondition);
      CharacterCreationOnSelect creationOnSelect7 = new CharacterCreationOnSelect(this.YouthOtherGarrisonOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects7 = new CharacterCreationApplyFinalEffects(this.YouthOtherGarrisonOnApply);
      TextObject textObject14 = new TextObject("{=1EkEElZd}Urban troops spend much of their time guarding the town walls. Most of their training was in missile.");
      creationCategory.AddCategoryOption(textObject13, skillObjectList7, intelligence2, focusToAdd7, skillLevelToAdd7, attributeLevelToAdd7, creationOnCondition7, creationOnSelect7, applyFinalEffects7, textObject14, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject15 = new TextObject("{=VlXOgIX6}rode with the scouts.");
      MBList<SkillObject> skillObjectList8 = new MBList<SkillObject>();
      skillObjectList8.Add(DefaultSkills.Riding);
      skillObjectList8.Add(DefaultSkills.Bow);
      CharacterAttribute endurance3 = DefaultCharacterAttributes.Endurance;
      int focusToAdd8 = this.FocusToAdd;
      int skillLevelToAdd8 = this.SkillLevelToAdd;
      int attributeLevelToAdd8 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition8 = new CharacterCreationOnCondition(this.YouthOutridersOnCondition);
      CharacterCreationOnSelect creationOnSelect8 = new CharacterCreationOnSelect(this.YouthOutridersOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects8 = new CharacterCreationApplyFinalEffects(this.YouthOutridersOnApply);
      TextObject textObject16 = new TextObject("{=888lmJqs}All of Calradia's kingdoms recognize the value of good light cavalry and horse archers, and are sure to recruit nomads and borderers with the skills to fulfill those duties. You were a good enough rider that your neighbors pitched in to buy you a small pony and a good bow so that you could fulfill their levy obligations.");
      creationCategory.AddCategoryOption(textObject15, skillObjectList8, endurance3, focusToAdd8, skillLevelToAdd8, attributeLevelToAdd8, creationOnCondition8, creationOnSelect8, applyFinalEffects8, textObject16, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject17 = new TextObject("{=VlXOgIX6}rode with the scouts.");
      MBList<SkillObject> skillObjectList9 = new MBList<SkillObject>();
      skillObjectList9.Add(DefaultSkills.Riding);
      skillObjectList9.Add(DefaultSkills.Bow);
      CharacterAttribute endurance4 = DefaultCharacterAttributes.Endurance;
      int focusToAdd9 = this.FocusToAdd;
      int skillLevelToAdd9 = this.SkillLevelToAdd;
      int attributeLevelToAdd9 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition9 = new CharacterCreationOnCondition(this.YouthOtherOutridersOnCondition);
      CharacterCreationOnSelect creationOnSelect9 = new CharacterCreationOnSelect(this.YouthOtherOutridersOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects9 = new CharacterCreationApplyFinalEffects(this.YouthOtherOutridersOnApply);
      TextObject textObject18 = new TextObject("{=sYuN6hPD}All of Calradia's kingdoms recognize the value of good light cavalry, and are sure to recruit nomads and borderers with the skills to fulfill those duties. You were a good enough rider that your neighbors pitched in to buy you a small pony and a sheaf of javelins so that you could fulfill their levy obligations.");
      creationCategory.AddCategoryOption(textObject17, skillObjectList9, endurance4, focusToAdd9, skillLevelToAdd9, attributeLevelToAdd9, creationOnCondition9, creationOnSelect9, applyFinalEffects9, textObject18, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject19 = new TextObject("{=a8arFSra}trained with the infantry.");
      MBList<SkillObject> skillObjectList10 = new MBList<SkillObject>();
      skillObjectList10.Add(DefaultSkills.Polearm);
      skillObjectList10.Add(DefaultSkills.OneHanded);
      CharacterAttribute vigor = DefaultCharacterAttributes.Vigor;
      int focusToAdd10 = this.FocusToAdd;
      int skillLevelToAdd10 = this.SkillLevelToAdd;
      int attributeLevelToAdd10 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect10 = new CharacterCreationOnSelect(this.YouthInfantryOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects10 = new CharacterCreationApplyFinalEffects(this.YouthInfantryOnApply);
      TextObject textObject20 = new TextObject("{=afH90aNs}Levy armed with spear and shield, drawn from smallholding farmers, have always been the backbone of most armies of Calradia.");
      creationCategory.AddCategoryOption(textObject19, skillObjectList10, vigor, focusToAdd10, skillLevelToAdd10, attributeLevelToAdd10, (CharacterCreationOnCondition) null, creationOnSelect10, applyFinalEffects10, textObject20, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject21 = new TextObject("{=oMbOIPc9}joined the skirmishers.");
      MBList<SkillObject> skillObjectList11 = new MBList<SkillObject>();
      skillObjectList11.Add(DefaultSkills.Throwing);
      skillObjectList11.Add(DefaultSkills.OneHanded);
      CharacterAttribute control1 = DefaultCharacterAttributes.Control;
      int focusToAdd11 = this.FocusToAdd;
      int skillLevelToAdd11 = this.SkillLevelToAdd;
      int attributeLevelToAdd11 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition10 = new CharacterCreationOnCondition(this.YouthSkirmisherOnCondition);
      CharacterCreationOnSelect creationOnSelect11 = new CharacterCreationOnSelect(this.YouthSkirmisherOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects11 = new CharacterCreationApplyFinalEffects(this.YouthSkirmisherOnApply);
      TextObject textObject22 = new TextObject("{=bXAg5w19}Younger recruits, or those of a slighter build, or those too poor to buy shield and armor tend to join the skirmishers. Fighting with bow and javelin, they try to stay out of reach of the main enemy forces.");
      creationCategory.AddCategoryOption(textObject21, skillObjectList11, control1, focusToAdd11, skillLevelToAdd11, attributeLevelToAdd11, creationOnCondition10, creationOnSelect11, applyFinalEffects11, textObject22, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject23 = new TextObject("{=cDWbwBwI}joined the kern.");
      MBList<SkillObject> skillObjectList12 = new MBList<SkillObject>();
      skillObjectList12.Add(DefaultSkills.Throwing);
      skillObjectList12.Add(DefaultSkills.OneHanded);
      CharacterAttribute control2 = DefaultCharacterAttributes.Control;
      int focusToAdd12 = this.FocusToAdd;
      int skillLevelToAdd12 = this.SkillLevelToAdd;
      int attributeLevelToAdd12 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition11 = new CharacterCreationOnCondition(this.YouthKernOnCondition);
      CharacterCreationOnSelect creationOnSelect12 = new CharacterCreationOnSelect(this.YouthKernOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects12 = new CharacterCreationApplyFinalEffects(this.YouthKernOnApply);
      TextObject textObject24 = new TextObject("{=tTb28jyU}Many Battanians fight as kern, versatile troops who could both harass the enemy line with their javelins or join in the final screaming charge once it weakened.");
      creationCategory.AddCategoryOption(textObject23, skillObjectList12, control2, focusToAdd12, skillLevelToAdd12, attributeLevelToAdd12, creationOnCondition11, creationOnSelect12, applyFinalEffects12, textObject24, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      TextObject textObject25 = new TextObject("{=GFUggps8}marched with the camp followers.");
      MBList<SkillObject> skillObjectList13 = new MBList<SkillObject>();
      skillObjectList13.Add(DefaultSkills.Roguery);
      skillObjectList13.Add(DefaultSkills.Throwing);
      CharacterAttribute cunning4 = DefaultCharacterAttributes.Cunning;
      int focusToAdd13 = this.FocusToAdd;
      int skillLevelToAdd13 = this.SkillLevelToAdd;
      int attributeLevelToAdd13 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition12 = new CharacterCreationOnCondition(this.YouthCamperOnCondition);
      CharacterCreationOnSelect creationOnSelect13 = new CharacterCreationOnSelect(this.YouthCamperOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects13 = new CharacterCreationApplyFinalEffects(this.YouthCamperOnApply);
      TextObject textObject26 = new TextObject("{=64rWqBLN}You avoided service with one of the main forces of your realm's armies, but followed instead in the train - the troops' wives, lovers and servants, and those who make their living by caring for, entertaining, or cheating the soldiery.");
      creationCategory.AddCategoryOption(textObject25, skillObjectList13, cunning4, focusToAdd13, skillLevelToAdd13, attributeLevelToAdd13, creationOnCondition12, creationOnSelect13, applyFinalEffects13, textObject26, (MBList<TraitObject>) null, 0, 0, 0, 0, 0);
      characterCreation.AddNewMenu(menu);
    }

    protected void AddAdulthoodMenu(CharacterCreation characterCreation)
    {
      MBTextManager.SetTextVariable("EXP_VALUE", this.SkillLevelToAdd);
      CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("{=MafIe9yI}Young Adulthood"), new TextObject("{=4WYY0X59}Before you set out for a life of adventure, your biggest achievement was..."), new CharacterCreationOnInit(this.AccomplishmentOnInit));
      CharacterCreationCategory creationCategory = menu.AddMenuCategory();
      TextObject textObject1 = new TextObject("{=8bwpVpgy}you defeated an enemy in battle.");
      MBList<SkillObject> skillObjectList1 = new MBList<SkillObject>()
      {
        DefaultSkills.OneHanded,
        DefaultSkills.TwoHanded
      };
      CharacterAttribute vigor = DefaultCharacterAttributes.Vigor;
      int focusToAdd1 = this.FocusToAdd;
      int skillLevelToAdd1 = this.SkillLevelToAdd;
      int attributeLevelToAdd1 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect1 = new CharacterCreationOnSelect(this.AccomplishmentDefeatedEnemyOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects1 = new CharacterCreationApplyFinalEffects(this.AccomplishmentDefeatedEnemyOnApply);
      TextObject textObject2 = new TextObject("{=1IEroJKs}Not everyone who musters for the levy marches to war, and not everyone who goes on campaign sees action. You did both, and you also took down an enemy warrior in direct one-to-one combat, in the full view of your comrades.");
      creationCategory.AddCategoryOption(textObject1, skillObjectList1, vigor, focusToAdd1, skillLevelToAdd1, attributeLevelToAdd1, (CharacterCreationOnCondition) null, creationOnSelect1, applyFinalEffects1, textObject2, new MBList<TraitObject>()
      {
        DefaultTraits.Valor
      }, 1, 20, 0, 0, 0);
      TextObject textObject3 = new TextObject("{=mP3uFbcq}you led a successful manhunt.");
      MBList<SkillObject> skillObjectList2 = new MBList<SkillObject>()
      {
        DefaultSkills.Tactics,
        DefaultSkills.Leadership
      };
      CharacterAttribute cunning1 = DefaultCharacterAttributes.Cunning;
      int focusToAdd2 = this.FocusToAdd;
      int skillLevelToAdd2 = this.SkillLevelToAdd;
      int attributeLevelToAdd2 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition1 = new CharacterCreationOnCondition(this.AccomplishmentPosseOnConditions);
      CharacterCreationOnSelect creationOnSelect2 = new CharacterCreationOnSelect(this.AccomplishmentExpeditionOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects2 = new CharacterCreationApplyFinalEffects(this.AccomplishmentExpeditionOnApply);
      TextObject textObject4 = new TextObject("{=4f5xwzX0}When your community needed to organize a posse to pursue horse thieves, you were the obvious choice. You hunted down the raiders, surrounded them and forced their surrender, and took back your stolen property.");
      creationCategory.AddCategoryOption(textObject3, skillObjectList2, cunning1, focusToAdd2, skillLevelToAdd2, attributeLevelToAdd2, creationOnCondition1, creationOnSelect2, applyFinalEffects2, textObject4, new MBList<TraitObject>()
      {
        DefaultTraits.Calculating
      }, 1, 10, 0, 0, 0);
      TextObject textObject5 = new TextObject("{=wfbtS71d}you led a caravan.");
      MBList<SkillObject> skillObjectList3 = new MBList<SkillObject>();
      skillObjectList3.Add(DefaultSkills.Tactics);
      skillObjectList3.Add(DefaultSkills.Leadership);
      CharacterAttribute cunning2 = DefaultCharacterAttributes.Cunning;
      int focusToAdd3 = this.FocusToAdd;
      int skillLevelToAdd3 = this.SkillLevelToAdd;
      int attributeLevelToAdd3 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition2 = new CharacterCreationOnCondition(this.AccomplishmentMerchantOnCondition);
      CharacterCreationOnSelect creationOnSelect3 = new CharacterCreationOnSelect(this.AccomplishmentMerchantOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects3 = new CharacterCreationApplyFinalEffects(this.AccomplishmentExpeditionOnApply);
      TextObject textObject6 = new TextObject("{=joRHKCkm}Your family needed someone trustworthy to take a caravan to a neighboring town. You organized supplies, ensured a constant watch to keep away bandits, and brought it safely to its destination.");
      creationCategory.AddCategoryOption(textObject5, skillObjectList3, cunning2, focusToAdd3, skillLevelToAdd3, attributeLevelToAdd3, creationOnCondition2, creationOnSelect3, applyFinalEffects3, textObject6, new MBList<TraitObject>()
      {
        DefaultTraits.Calculating
      }, 1, 10, 0, 0, 0);
      TextObject textObject7 = new TextObject("{=x1HTX5hq}you saved your village from a flood.");
      MBList<SkillObject> skillObjectList4 = new MBList<SkillObject>();
      skillObjectList4.Add(DefaultSkills.Tactics);
      skillObjectList4.Add(DefaultSkills.Leadership);
      CharacterAttribute cunning3 = DefaultCharacterAttributes.Cunning;
      int focusToAdd4 = this.FocusToAdd;
      int skillLevelToAdd4 = this.SkillLevelToAdd;
      int attributeLevelToAdd4 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition3 = new CharacterCreationOnCondition(this.AccomplishmentSavedVillageOnCondition);
      CharacterCreationOnSelect creationOnSelect4 = new CharacterCreationOnSelect(this.AccomplishmentSavedVillageOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects4 = new CharacterCreationApplyFinalEffects(this.AccomplishmentExpeditionOnApply);
      TextObject textObject8 = new TextObject("{=bWlmGDf3}When a sudden storm caused the local stream to rise suddenly, your neighbors needed quick-thinking leadership. You provided it, directing them to build levees to save their homes.");
      creationCategory.AddCategoryOption(textObject7, skillObjectList4, cunning3, focusToAdd4, skillLevelToAdd4, attributeLevelToAdd4, creationOnCondition3, creationOnSelect4, applyFinalEffects4, textObject8, new MBList<TraitObject>()
      {
        DefaultTraits.Calculating
      }, 1, 10, 0, 0, 0);
      TextObject textObject9 = new TextObject("{=s8PNllPN}you saved your city quarter from a fire.");
      MBList<SkillObject> skillObjectList5 = new MBList<SkillObject>();
      skillObjectList5.Add(DefaultSkills.Tactics);
      skillObjectList5.Add(DefaultSkills.Leadership);
      CharacterAttribute cunning4 = DefaultCharacterAttributes.Cunning;
      int focusToAdd5 = this.FocusToAdd;
      int skillLevelToAdd5 = this.SkillLevelToAdd;
      int attributeLevelToAdd5 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition4 = new CharacterCreationOnCondition(this.AccomplishmentSavedStreetOnCondition);
      CharacterCreationOnSelect creationOnSelect5 = new CharacterCreationOnSelect(this.AccomplishmentSavedStreetOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects5 = new CharacterCreationApplyFinalEffects(this.AccomplishmentExpeditionOnApply);
      TextObject textObject10 = new TextObject("{=ZAGR6PYc}When a sudden blaze broke out in a back alley, your neighbors needed quick-thinking leadership and you provided it. You organized a bucket line to the nearest well, putting the fire out before any homes were lost.");
      creationCategory.AddCategoryOption(textObject9, skillObjectList5, cunning4, focusToAdd5, skillLevelToAdd5, attributeLevelToAdd5, creationOnCondition4, creationOnSelect5, applyFinalEffects5, textObject10, new MBList<TraitObject>()
      {
        DefaultTraits.Calculating
      }, 1, 10, 0, 0, 0);
      TextObject textObject11 = new TextObject("{=xORjDTal}you invested some money in a workshop.");
      MBList<SkillObject> skillObjectList6 = new MBList<SkillObject>();
      skillObjectList6.Add(DefaultSkills.Trade);
      skillObjectList6.Add(DefaultSkills.Crafting);
      CharacterAttribute intelligence1 = DefaultCharacterAttributes.Intelligence;
      int focusToAdd6 = this.FocusToAdd;
      int skillLevelToAdd6 = this.SkillLevelToAdd;
      int attributeLevelToAdd6 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition5 = new CharacterCreationOnCondition(this.AccomplishmentUrbanOnCondition);
      CharacterCreationOnSelect creationOnSelect6 = new CharacterCreationOnSelect(this.AccomplishmentWorkshopOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects6 = new CharacterCreationApplyFinalEffects(this.AccomplishmentWorkshopOnApply);
      TextObject textObject12 = new TextObject("{=PyVqDLBu}Your parents didn't give you much money, but they did leave just enough for you to secure a loan against a larger amount to build a small workshop. You paid back what you borrowed, and sold your enterprise for a profit.");
      creationCategory.AddCategoryOption(textObject11, skillObjectList6, intelligence1, focusToAdd6, skillLevelToAdd6, attributeLevelToAdd6, creationOnCondition5, creationOnSelect6, applyFinalEffects6, textObject12, new MBList<TraitObject>()
      {
        DefaultTraits.Calculating
      }, 1, 10, 0, 0, 0);
      TextObject textObject13 = new TextObject("{=xKXcqRJI}you invested some money in land.");
      MBList<SkillObject> skillObjectList7 = new MBList<SkillObject>();
      skillObjectList7.Add(DefaultSkills.Trade);
      skillObjectList7.Add(DefaultSkills.Crafting);
      CharacterAttribute intelligence2 = DefaultCharacterAttributes.Intelligence;
      int focusToAdd7 = this.FocusToAdd;
      int skillLevelToAdd7 = this.SkillLevelToAdd;
      int attributeLevelToAdd7 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition6 = new CharacterCreationOnCondition(this.AccomplishmentRuralOnCondition);
      CharacterCreationOnSelect creationOnSelect7 = new CharacterCreationOnSelect(this.AccomplishmentWorkshopOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects7 = new CharacterCreationApplyFinalEffects(this.AccomplishmentWorkshopOnApply);
      TextObject textObject14 = new TextObject("{=cbF9jdQo}Your parents didn't give you much money, but they did leave just enough for you to purchase a plot of unused land at the edge of the village. You cleared away rocks and dug an irrigation ditch, raised a few seasons of crops, than sold it for a considerable profit.");
      creationCategory.AddCategoryOption(textObject13, skillObjectList7, intelligence2, focusToAdd7, skillLevelToAdd7, attributeLevelToAdd7, creationOnCondition6, creationOnSelect7, applyFinalEffects7, textObject14, new MBList<TraitObject>()
      {
        DefaultTraits.Calculating
      }, 1, 10, 0, 0, 0);
      TextObject textObject15 = new TextObject("{=TbNRtUjb}you hunted a dangerous animal.");
      MBList<SkillObject> skillObjectList8 = new MBList<SkillObject>();
      skillObjectList8.Add(DefaultSkills.Polearm);
      skillObjectList8.Add(DefaultSkills.Crossbow);
      CharacterAttribute control1 = DefaultCharacterAttributes.Control;
      int focusToAdd8 = this.FocusToAdd;
      int skillLevelToAdd8 = this.SkillLevelToAdd;
      int attributeLevelToAdd8 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition7 = new CharacterCreationOnCondition(this.AccomplishmentRuralOnCondition);
      CharacterCreationOnSelect creationOnSelect8 = new CharacterCreationOnSelect(this.AccomplishmentSiegeHunterOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects8 = new CharacterCreationApplyFinalEffects(this.AccomplishmentSiegeHunterOnApply);
      TextObject textObject16 = new TextObject("{=I3PcdaaL}Wolves, bears are a constant menace to the flocks of northern Calradia, while hyenas and leopards trouble the south. You went with a group of your fellow villagers and fired the missile that brought down the beast.");
      creationCategory.AddCategoryOption(textObject15, skillObjectList8, control1, focusToAdd8, skillLevelToAdd8, attributeLevelToAdd8, creationOnCondition7, creationOnSelect8, applyFinalEffects8, textObject16, (MBList<TraitObject>) null, 0, 5, 0, 0, 0);
      TextObject textObject17 = new TextObject("{=WbHfGCbd}you survived a siege.");
      MBList<SkillObject> skillObjectList9 = new MBList<SkillObject>();
      skillObjectList9.Add(DefaultSkills.Bow);
      skillObjectList9.Add(DefaultSkills.Crossbow);
      CharacterAttribute control2 = DefaultCharacterAttributes.Control;
      int focusToAdd9 = this.FocusToAdd;
      int skillLevelToAdd9 = this.SkillLevelToAdd;
      int attributeLevelToAdd9 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition8 = new CharacterCreationOnCondition(this.AccomplishmentUrbanOnCondition);
      CharacterCreationOnSelect creationOnSelect9 = new CharacterCreationOnSelect(this.AccomplishmentSiegeHunterOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects9 = new CharacterCreationApplyFinalEffects(this.AccomplishmentSiegeHunterOnApply);
      TextObject textObject18 = new TextObject("{=FhZPjhli}Your hometown was briefly placed under siege, and you were called to defend the walls. Everyone did their part to repulse the enemy assault, and everyone is justly proud of what they endured.");
      creationCategory.AddCategoryOption(textObject17, skillObjectList9, control2, focusToAdd9, skillLevelToAdd9, attributeLevelToAdd9, creationOnCondition8, creationOnSelect9, applyFinalEffects9, textObject18, (MBList<TraitObject>) null, 0, 5, 0, 0, 0);
      TextObject textObject19 = new TextObject("{=kNXet6Um}you had a famous escapade in town.");
      MBList<SkillObject> skillObjectList10 = new MBList<SkillObject>();
      skillObjectList10.Add(DefaultSkills.Athletics);
      skillObjectList10.Add(DefaultSkills.Roguery);
      CharacterAttribute endurance1 = DefaultCharacterAttributes.Endurance;
      int focusToAdd10 = this.FocusToAdd;
      int skillLevelToAdd10 = this.SkillLevelToAdd;
      int attributeLevelToAdd10 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition9 = new CharacterCreationOnCondition(this.AccomplishmentRuralOnCondition);
      CharacterCreationOnSelect creationOnSelect10 = new CharacterCreationOnSelect(this.AccomplishmentEscapadeOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects10 = new CharacterCreationApplyFinalEffects(this.AccomplishmentEscapadeOnApply);
      TextObject textObject20 = new TextObject("{=DjeAJtix}Maybe it was a love affair, or maybe you cheated at dice, or maybe you just chose your words poorly when drinking with a dangerous crowd. Anyway, on one of your trips into town you got into the kind of trouble from which only a quick tongue or quick feet get you out alive.");
      creationCategory.AddCategoryOption(textObject19, skillObjectList10, endurance1, focusToAdd10, skillLevelToAdd10, attributeLevelToAdd10, creationOnCondition9, creationOnSelect10, applyFinalEffects10, textObject20, new MBList<TraitObject>()
      {
        DefaultTraits.Valor
      }, 1, 5, 0, 0, 0);
      TextObject textObject21 = new TextObject("{=qlOuiKXj}you had a famous escapade.");
      MBList<SkillObject> skillObjectList11 = new MBList<SkillObject>();
      skillObjectList11.Add(DefaultSkills.Athletics);
      skillObjectList11.Add(DefaultSkills.Roguery);
      CharacterAttribute endurance2 = DefaultCharacterAttributes.Endurance;
      int focusToAdd11 = this.FocusToAdd;
      int skillLevelToAdd11 = this.SkillLevelToAdd;
      int attributeLevelToAdd11 = this.AttributeLevelToAdd;
      CharacterCreationOnCondition creationOnCondition10 = new CharacterCreationOnCondition(this.AccomplishmentUrbanOnCondition);
      CharacterCreationOnSelect creationOnSelect11 = new CharacterCreationOnSelect(this.AccomplishmentEscapadeOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects11 = new CharacterCreationApplyFinalEffects(this.AccomplishmentEscapadeOnApply);
      TextObject textObject22 = new TextObject("{=lD5Ob3R4}Maybe it was a love affair, or maybe you cheated at dice, or maybe you just chose your words poorly when drinking with a dangerous crowd. Anyway, you got into the kind of trouble from which only a quick tongue or quick feet get you out alive.");
      creationCategory.AddCategoryOption(textObject21, skillObjectList11, endurance2, focusToAdd11, skillLevelToAdd11, attributeLevelToAdd11, creationOnCondition10, creationOnSelect11, applyFinalEffects11, textObject22, new MBList<TraitObject>()
      {
        DefaultTraits.Valor
      }, 1, 5, 0, 0, 0);
      TextObject textObject23 = new TextObject("{=Yqm0Dics}you treated people well.");
      MBList<SkillObject> skillObjectList12 = new MBList<SkillObject>();
      skillObjectList12.Add(DefaultSkills.Charm);
      skillObjectList12.Add(DefaultSkills.Steward);
      CharacterAttribute social = DefaultCharacterAttributes.Social;
      int focusToAdd12 = this.FocusToAdd;
      int skillLevelToAdd12 = this.SkillLevelToAdd;
      int attributeLevelToAdd12 = this.AttributeLevelToAdd;
      CharacterCreationOnSelect creationOnSelect12 = new CharacterCreationOnSelect(this.AccomplishmentTreaterOnConsequence);
      CharacterCreationApplyFinalEffects applyFinalEffects12 = new CharacterCreationApplyFinalEffects(this.AccomplishmentTreaterOnApply);
      TextObject textObject24 = new TextObject("{=dDmcqTzb}Yours wasn't the kind of reputation that local legends are made of, but it was the kind that wins you respect among those around you. You were consistently fair and honest in your business dealings and helpful to those in trouble. In doing so, you got a sense of what made people tick.");
      creationCategory.AddCategoryOption(textObject23, skillObjectList12, social, focusToAdd12, skillLevelToAdd12, attributeLevelToAdd12, (CharacterCreationOnCondition) null, creationOnSelect12, applyFinalEffects12, textObject24, new MBList<TraitObject>()
      {
        DefaultTraits.Mercy,
        DefaultTraits.Generosity,
        DefaultTraits.Honor
      }, 1, 5, 0, 0, 0);
      characterCreation.AddNewMenu(menu);
    }

    protected void AddAgeSelectionMenu(CharacterCreation characterCreation)
    {
      MBTextManager.SetTextVariable("EXP_VALUE", this.SkillLevelToAdd);
      CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("{=HDFEAYDk}Starting Age"), new TextObject("{=VlOGrGSn}Your character started off on the adventuring path at the age of..."), new CharacterCreationOnInit(this.StartingAgeOnInit));
      CharacterCreationCategory creationCategory = menu.AddMenuCategory();
      creationCategory.AddCategoryOption(new TextObject("{=!}20"), new MBList<SkillObject>(), (CharacterAttribute) null, 0, 0, 0, (CharacterCreationOnCondition) null, new CharacterCreationOnSelect(this.StartingAgeYoungOnConsequence), new CharacterCreationApplyFinalEffects(this.StartingAgeYoungOnApply), new TextObject("{=2k7adlh7}While lacking experience a bit, you are full with youthful energy, you are fully eager, for the long years of adventuring ahead."), (MBList<TraitObject>) null, 0, 0, 0, 2, 1);
      creationCategory.AddCategoryOption(new TextObject("{=!}30"), new MBList<SkillObject>(), (CharacterAttribute) null, 0, 0, 0, (CharacterCreationOnCondition) null, new CharacterCreationOnSelect(this.StartingAgeAdultOnConsequence), new CharacterCreationApplyFinalEffects(this.StartingAgeAdultOnApply), new TextObject("{=NUlVFRtK}You are at your prime, You still have some youthful energy but also have a substantial amount of experience under your belt. "), (MBList<TraitObject>) null, 0, 0, 0, 4, 2);
      creationCategory.AddCategoryOption(new TextObject("{=!}40"), new MBList<SkillObject>(), (CharacterAttribute) null, 0, 0, 0, (CharacterCreationOnCondition) null, new CharacterCreationOnSelect(this.StartingAgeMiddleAgedOnConsequence), new CharacterCreationApplyFinalEffects(this.StartingAgeMiddleAgedOnApply), new TextObject("{=5MxTYApM}This is the right age for starting off, you have years of experience, and you are old enough for people to respect you and gather under your banner."), (MBList<TraitObject>) null, 0, 0, 0, 6, 3);
      creationCategory.AddCategoryOption(new TextObject("{=!}50"), new MBList<SkillObject>(), (CharacterAttribute) null, 0, 0, 0, (CharacterCreationOnCondition) null, new CharacterCreationOnSelect(this.StartingAgeElderlyOnConsequence), new CharacterCreationApplyFinalEffects(this.StartingAgeElderlyOnApply), new TextObject("{=ePD5Afvy}While you are past your prime, there is still enough time to go on that last big adventure for you. And you have all the experience you need to overcome anything!"), (MBList<TraitObject>) null, 0, 0, 0, 8, 4);
      characterCreation.AddNewMenu(menu);
    }

    protected void ParentsOnInit(CharacterCreation characterCreation)
    {
      characterCreation.IsPlayerAlone = false;
      characterCreation.HasSecondaryCharacter = false;
      ScCharacterCreationContent.ClearMountEntity(characterCreation);
      characterCreation.ClearFaceGenPrefab();
      if (this.PlayerBodyProperties != CharacterObject.PlayerCharacter.GetBodyProperties(CharacterObject.PlayerCharacter.Equipment, -1))
      {
        this.PlayerBodyProperties = CharacterObject.PlayerCharacter.GetBodyProperties(CharacterObject.PlayerCharacter.Equipment, -1);
        BodyProperties motherBodyProperties = this.PlayerBodyProperties;
        BodyProperties fatherBodyProperties = this.PlayerBodyProperties;
        FaceGen.GenerateParentKey(this.PlayerBodyProperties, 0, ref motherBodyProperties, ref fatherBodyProperties);
        motherBodyProperties = new BodyProperties(new DynamicBodyProperties(33f, 0.3f, 0.2f), motherBodyProperties.StaticProperties);
        fatherBodyProperties = new BodyProperties(new DynamicBodyProperties(33f, 0.5f, 0.5f), fatherBodyProperties.StaticProperties);
        this.MotherFacegenCharacter = new FaceGenChar(motherBodyProperties, 0, new Equipment(), true, "anim_mother_1");
        this.FatherFacegenCharacter = new FaceGenChar(fatherBodyProperties, 0, new Equipment(), false, "anim_father_1");
      }
      characterCreation.ChangeFaceGenChars(new List<FaceGenChar>()
      {
        this.MotherFacegenCharacter,
        this.FatherFacegenCharacter
      });
      this.ChangeParentsOutfit(characterCreation);
      this.ChangeParentsAnimation(characterCreation);
    }

    protected void ChangeParentsOutfit(
      CharacterCreation characterCreation,
      string fatherItemId = "",
      string motherItemId = "",
      bool isLeftHandItemForFather = true,
      bool isLeftHandItemForMother = true)
    {
      characterCreation.ClearFaceGenPrefab();
      List<Equipment> equipmentList = new List<Equipment>();
      Equipment equipment1 = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>("mother_char_creation_" + this.SelectedParentType.ToString() + "_" + this.GetSelectedCulture().StringId)?.DefaultEquipment ?? MBEquipmentRoster.EmptyEquipment;
      Equipment equipment2 = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>("father_char_creation_" + this.SelectedParentType.ToString() + "_" + this.GetSelectedCulture().StringId)?.DefaultEquipment ?? MBEquipmentRoster.EmptyEquipment;
      if (motherItemId != "")
      {
        ItemObject itemObject = Game.Current.ObjectManager.GetObject<ItemObject>(motherItemId);
        if (itemObject != null)
          equipment1.AddEquipmentToSlotWithoutAgent(isLeftHandItemForMother ? EquipmentIndex.WeaponItemBeginSlot : EquipmentIndex.Weapon1, new EquipmentElement(itemObject));
        else
          characterCreation.ChangeCharacterPrefab(motherItemId, isLeftHandItemForMother ? Game.Current.DefaultMonster.MainHandItemBoneIndex : Game.Current.DefaultMonster.OffHandItemBoneIndex);
      }
      if (fatherItemId != "")
      {
        ItemObject itemObject = Game.Current.ObjectManager.GetObject<ItemObject>(fatherItemId);
        if (itemObject != null)
          equipment2.AddEquipmentToSlotWithoutAgent(isLeftHandItemForFather ? EquipmentIndex.WeaponItemBeginSlot : EquipmentIndex.Weapon1, new EquipmentElement(itemObject));
      }
      equipmentList.Add(equipment1);
      equipmentList.Add(equipment2);
      characterCreation.ChangeCharactersEquipment(equipmentList);
    }

    protected void ChangeParentsAnimation(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "anim_mother_" + this.SelectedParentType.ToString(),
      "anim_father_" + this.SelectedParentType.ToString()
    });

    protected void SetParentAndOccupationType(
      CharacterCreation characterCreation,
      int parentType,
      ScCharacterCreationContent.OccupationTypes occupationType,
      string fatherItemId = "",
      string motherItemId = "",
      bool isLeftHandItemForFather = true,
      bool isLeftHandItemForMother = true)
    {
      this.SelectedParentType = parentType;
      this._familyOccupationType = occupationType;
      characterCreation.ChangeFaceGenChars(new List<FaceGenChar>()
      {
        this.MotherFacegenCharacter,
        this.FatherFacegenCharacter
      });
      this.ChangeParentsAnimation(characterCreation);
      this.ChangeParentsOutfit(characterCreation, fatherItemId, motherItemId, isLeftHandItemForFather, isLeftHandItemForMother);
    }

    protected void RepublicLandlordsRetainerOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 1, ScCharacterCreationContent.OccupationTypes.Retainer);

    protected void RepublicMerchantOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 2, ScCharacterCreationContent.OccupationTypes.Merchant);

    protected void RepublicFreeholderOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 3, ScCharacterCreationContent.OccupationTypes.Farmer);

    protected void RepublicArtisanOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 4, ScCharacterCreationContent.OccupationTypes.Artisan);

    protected void RepublicWoodsmanOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 5, ScCharacterCreationContent.OccupationTypes.Hunter);

    protected void RepublicVagabondOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 6, ScCharacterCreationContent.OccupationTypes.Vagabond);

    protected void RepublicLandlordsRetainerOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void RepublicMerchantOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void RepublicFreeholderOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void RepublicArtisanOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void RepublicWoodsmanOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void RepublicVagabondOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void SeparatistBaronsRetainerOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 1, ScCharacterCreationContent.OccupationTypes.Retainer);

    protected void SeparatistMerchantOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 2, ScCharacterCreationContent.OccupationTypes.Merchant);

    protected void SeparatistYeomanOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 3, ScCharacterCreationContent.OccupationTypes.Farmer);

    protected void SeparatistBlacksmithOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 4, ScCharacterCreationContent.OccupationTypes.Artisan);

    protected void SeparatistHunterOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 5, ScCharacterCreationContent.OccupationTypes.Hunter);

    protected void SeparatistMercenaryOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 6, ScCharacterCreationContent.OccupationTypes.Mercenary);

    protected void SeparatistBaronsRetainerOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void SeparatistMerchantOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void SeparatistYeomanOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void SeparatistBlacksmithOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void SeparatistHunterOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void SeparatistMercenaryOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void MandalorianBoyarsCompanionOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 1, ScCharacterCreationContent.OccupationTypes.Retainer);

    protected void MandalorianTraderOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 2, ScCharacterCreationContent.OccupationTypes.Merchant);

    protected void MandalorianFreemanOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 3, ScCharacterCreationContent.OccupationTypes.Farmer);

    protected void MandalorianArtisanOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 4, ScCharacterCreationContent.OccupationTypes.Artisan);

    protected void MandalorianHunterOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 5, ScCharacterCreationContent.OccupationTypes.Hunter);

    protected void MandalorianVagabondOnConsequence(CharacterCreation characterCreation) => this.SetParentAndOccupationType(characterCreation, 6, ScCharacterCreationContent.OccupationTypes.Vagabond);

    protected void MandalorianBoyarsCompanionOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void MandalorianTraderOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void MandalorianFreemanOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void MandalorianArtisanOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void MandalorianHunterOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected void MandalorianVagabondOnApply(CharacterCreation characterCreation) => this.FinalizeParents();

    protected bool RepublicParentsOnCondition() => this.GetSelectedCulture().StringId == "galactic_republic";

    protected bool SeparatistParentsOnCondition() => this.GetSelectedCulture().StringId == "separatistalliance";

    protected bool MandalorianParentsOnCondition() => this.GetSelectedCulture().StringId == "mandalorians";

    protected void FinalizeParents()
    {
      CharacterObject character1 = Game.Current.ObjectManager.GetObject<CharacterObject>("main_hero_mother");
      CharacterObject character2 = Game.Current.ObjectManager.GetObject<CharacterObject>("main_hero_father");
      character1.HeroObject.ModifyPlayersFamilyAppearance(this.MotherFacegenCharacter.BodyProperties.StaticProperties);
      character2.HeroObject.ModifyPlayersFamilyAppearance(this.FatherFacegenCharacter.BodyProperties.StaticProperties);
      character1.HeroObject.Weight = this.MotherFacegenCharacter.BodyProperties.Weight;
      character1.HeroObject.Build = this.MotherFacegenCharacter.BodyProperties.Build;
      character2.HeroObject.Weight = this.FatherFacegenCharacter.BodyProperties.Weight;
      character2.HeroObject.Build = this.FatherFacegenCharacter.BodyProperties.Build;
      EquipmentHelper.AssignHeroEquipmentFromEquipment(character1.HeroObject, this.MotherFacegenCharacter.Equipment);
      EquipmentHelper.AssignHeroEquipmentFromEquipment(character2.HeroObject, this.FatherFacegenCharacter.Equipment);
      character1.Culture = Hero.MainHero.Culture;
      character2.Culture = Hero.MainHero.Culture;
      StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter);
      TextObject text1 = GameTexts.FindText("str_player_father_name", Hero.MainHero.Culture.StringId);
      character2.HeroObject.SetName(text1, text1);
      TextObject parent1 = new TextObject("{=XmvaRfLM}{PLAYER_FATHER.NAME} was the father of {PLAYER.LINK}. He was slain when raiders attacked the inn at which his family was staying.");
      StringHelpers.SetCharacterProperties("PLAYER_FATHER", character2, parent1);
      character2.HeroObject.EncyclopediaText = parent1;
      TextObject text2 = GameTexts.FindText("str_player_mother_name", Hero.MainHero.Culture.StringId);
      character1.HeroObject.SetName(text2, text2);
      TextObject parent2 = new TextObject("{=hrhvEWP8}{PLAYER_MOTHER.NAME} was the mother of {PLAYER.LINK}. She was slain when raiders attacked the inn at which her family was staying.");
      StringHelpers.SetCharacterProperties("PLAYER_MOTHER", character1, parent2);
      character1.HeroObject.EncyclopediaText = parent2;
      character1.HeroObject.UpdateHomeSettlement();
      character2.HeroObject.UpdateHomeSettlement();
      character1.HeroObject.SetHasMet();
      character2.HeroObject.SetHasMet();
    }

    protected static List<FaceGenChar> ChangePlayerFaceWithAge(float age, string actionName = "act_childhood_schooled")
    {
      List<FaceGenChar> faceGenCharList = new List<FaceGenChar>();
      BodyProperties originalBodyProperties = CharacterObject.PlayerCharacter.GetBodyProperties(CharacterObject.PlayerCharacter.Equipment, -1);
      originalBodyProperties = FaceGen.GetBodyPropertiesWithAge(ref originalBodyProperties, age);
      faceGenCharList.Add(new FaceGenChar(originalBodyProperties, 0, new Equipment(), CharacterObject.PlayerCharacter.IsFemale, actionName));
      return faceGenCharList;
    }

    protected Equipment ChangePlayerOutfit(CharacterCreation characterCreation, string outfit)
    {
      List<Equipment> equipmentList = new List<Equipment>();
      Equipment defaultEquipment = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>(outfit)?.DefaultEquipment;
      if (defaultEquipment == null)
      {
        Debug.FailedAssert("item shouldn't be null! Check this!", "C:\\Develop\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CharacterCreationContent\\ScCharacterCreationContent.cs", nameof (ChangePlayerOutfit), 1039);
        defaultEquipment = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>("player_char_creation_default").DefaultEquipment;
      }
      equipmentList.Add(defaultEquipment);
      characterCreation.ChangeCharactersEquipment(equipmentList);
      return defaultEquipment;
    }

    protected static void ChangePlayerMount(CharacterCreation characterCreation, Hero hero)
    {
      if (!hero.CharacterObject.HasMount())
        return;
      EquipmentElement equipmentElement = hero.CharacterObject.Equipment[EquipmentIndex.ArmorItemEndSlot];
      MountCreationKey randomMountKey = MountCreationKey.GetRandomMountKey(equipmentElement.Item, hero.CharacterObject.GetMountKeySeed());
      equipmentElement = hero.CharacterObject.Equipment[EquipmentIndex.ArmorItemEndSlot];
      ItemObject horseItem = equipmentElement.Item;
      equipmentElement = hero.CharacterObject.Equipment[EquipmentIndex.HorseHarness];
      ItemObject harnessItem = equipmentElement.Item;
      FaceGenMount newMount = new FaceGenMount(randomMountKey, horseItem, harnessItem, "act_horse_stand_1");
      characterCreation.SetFaceGenMount(newMount);
    }

    protected static void ClearMountEntity(CharacterCreation characterCreation) => characterCreation.ClearFaceGenMounts();

    protected void ChildhoodOnInit(CharacterCreation characterCreation)
    {
      characterCreation.IsPlayerAlone = true;
      characterCreation.HasSecondaryCharacter = false;
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge((float) this.ChildhoodAge));
      string outfit = "player_char_creation_childhood_age_" + this.GetSelectedCulture().StringId + "_" + this.SelectedParentType.ToString() + (Hero.MainHero.IsFemale ? "_f" : "_m");
      this.ChangePlayerOutfit(characterCreation, outfit);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_schooled"
      });
      ScCharacterCreationContent.ClearMountEntity(characterCreation);
    }

    protected static void ChildhoodYourLeadershipSkillsOnConsequence(
      CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_leader"
      });
    }

    protected static void ChildhoodYourBrawnOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_athlete"
    });

    protected static void ChildhoodAttentionToDetailOnConsequence(
      CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_memory"
      });
    }

    protected static void ChildhoodAptitudeForNumbersOnConsequence(
      CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_numbers"
      });
    }

    protected static void ChildhoodWayWithPeopleOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_manners"
    });

    protected static void ChildhoodSkillsWithHorsesOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_animals"
    });

    protected static void ChildhoodGoodLeadingOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void ChildhoodGoodAthleticsOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void ChildhoodGoodMemoryOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void ChildhoodGoodMathOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void ChildhoodGoodMannersOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void ChildhoodAffinityWithAnimalsOnApply(CharacterCreation characterCreation)
    {
    }

    protected void EducationOnInit(CharacterCreation characterCreation)
    {
      characterCreation.IsPlayerAlone = true;
      characterCreation.HasSecondaryCharacter = false;
      characterCreation.ClearFaceGenPrefab();
      TextObject textObject1 = new TextObject("{=WYvnWcXQ}Like all village children you helped out in the fields. You also...");
      TextObject textObject2 = new TextObject("{=DsCkf6Pb}Growing up, you spent most of your time...");
      this._educationIntroductoryText.SetTextVariable("EDUCATION_INTRO", this.RuralType() ? textObject1 : textObject2);
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge((float) this.EducationAge));
      string outfit = "player_char_creation_education_age_" + this.GetSelectedCulture().StringId + "_" + this.SelectedParentType.ToString() + (Hero.MainHero.IsFemale ? "_f" : "_m");
      this.ChangePlayerOutfit(characterCreation, outfit);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_schooled"
      });
      ScCharacterCreationContent.ClearMountEntity(characterCreation);
    }

    protected bool RuralType() => this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Retainer || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Farmer || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Hunter || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Bard || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Herder || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Vagabond || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Healer || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Artisan;

    protected bool RichParents() => this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Retainer || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Merchant;

    protected bool RuralAdolescenceOnCondition() => this.RuralType();

    protected bool UrbanAdolescenceOnCondition() => !this.RuralType();

    protected bool UrbanRichAdolescenceOnCondition() => !this.RuralType() && this.RichParents();

    protected bool UrbanPoorAdolescenceOnCondition() => !this.RuralType() && !this.RichParents();

    protected void RefreshPropsAndClothing(
      CharacterCreation characterCreation,
      bool isChildhoodStage,
      string itemId,
      bool isLeftHand,
      string secondItemId = "")
    {
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ClearCharactersEquipment();
      string outfit = (isChildhoodStage ? "player_char_creation_childhood_age_" + this.GetSelectedCulture().StringId + "_" + this.SelectedParentType.ToString() : "player_char_creation_education_age_" + this.GetSelectedCulture().StringId + "_" + this.SelectedParentType.ToString()) + (Hero.MainHero.IsFemale ? "_f" : "_m");
      Equipment equipment = this.ChangePlayerOutfit(characterCreation, outfit).Clone();
      if (Game.Current.ObjectManager.GetObject<ItemObject>(itemId) != null)
      {
        ItemObject itemObject1 = Game.Current.ObjectManager.GetObject<ItemObject>(itemId);
        equipment.AddEquipmentToSlotWithoutAgent(isLeftHand ? EquipmentIndex.WeaponItemBeginSlot : EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
        if (secondItemId != "")
        {
          ItemObject itemObject2 = Game.Current.ObjectManager.GetObject<ItemObject>(secondItemId);
          equipment.AddEquipmentToSlotWithoutAgent(isLeftHand ? EquipmentIndex.Weapon1 : EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(itemObject2));
        }
      }
      else
        characterCreation.ChangeCharacterPrefab(itemId, isLeftHand ? Game.Current.DefaultMonster.MainHandItemBoneIndex : Game.Current.DefaultMonster.OffHandItemBoneIndex);
      characterCreation.ChangeCharactersEquipment(new List<Equipment>()
      {
        equipment
      });
    }

    protected void RuralAdolescenceHerderOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_streets"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "carry_bostaff_rogue1", true);
    }

    protected void RuralAdolescenceSmithyOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_militia"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "peasant_hammer_1_t1", true);
    }

    protected void RuralAdolescenceRepairmanOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_grit"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "carry_hammer", true);
    }

    protected void RuralAdolescenceGathererOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_peddlers"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "_to_carry_bd_basket_a", true);
    }

    protected void RuralAdolescenceHunterOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_sharp"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "composite_bow", true);
    }

    protected void RuralAdolescenceHelperOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_peddlers_2"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "_to_carry_bd_fabric_c", true);
    }

    protected void UrbanAdolescenceWatcherOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_fox"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "", true);
    }

    protected void UrbanAdolescenceMarketerOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_manners"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "", true);
    }

    protected void UrbanAdolescenceGangerOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_athlete"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "", true);
    }

    protected void UrbanAdolescenceDockerOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_peddlers"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "_to_carry_bd_basket_a", true);
    }

    protected void UrbanAdolescenceHorserOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_peddlers_2"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "_to_carry_bd_fabric_c", true);
    }

    protected void UrbanAdolescenceTutorOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_book"
      });
      this.RefreshPropsAndClothing(characterCreation, false, "character_creation_notebook", false);
    }

    protected static void RuralAdolescenceHerderOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void RuralAdolescenceSmithyOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void RuralAdolescenceRepairmanOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void RuralAdolescenceGathererOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void RuralAdolescenceHunterOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void RuralAdolescenceHelperOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void UrbanAdolescenceWatcherOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void UrbanAdolescenceMarketerOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void UrbanAdolescenceGangerOnApply(CharacterCreation characterCreation)
    {
    }

    protected static void UrbanAdolescenceDockerOnApply(CharacterCreation characterCreation)
    {
    }

    protected void YouthOnInit(CharacterCreation characterCreation)
    {
      characterCreation.IsPlayerAlone = true;
      characterCreation.HasSecondaryCharacter = false;
      characterCreation.ClearFaceGenPrefab();
      TextObject textObject1 = new TextObject("{=F7OO5SAa}As a youngster growing up in Calradia, war was never too far away. You...");
      TextObject textObject2 = new TextObject("{=5kbeAC7k}In wartorn Calradia, especially in frontier or tribal areas, some women as well as men learn to fight from an early age. You...");
      this._youthIntroductoryText.SetTextVariable("YOUTH_INTRO", CharacterObject.PlayerCharacter.IsFemale ? textObject2 : textObject1);
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge((float) this.YouthAge));
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_schooled"
      });
      if (this.SelectedTitleType < 1 || this.SelectedTitleType > 10)
        this.SelectedTitleType = 1;
      this.RefreshPlayerAppearance(characterCreation);
    }

    protected void RefreshPlayerAppearance(CharacterCreation characterCreation)
    {
      string outfit = "player_char_creation_" + this.GetSelectedCulture().StringId + "_" + this.SelectedTitleType.ToString() + (Hero.MainHero.IsFemale ? "_f" : "_m");
      this.ChangePlayerOutfit(characterCreation, outfit);
      this.ApplyEquipments(characterCreation);
    }

    protected bool YouthCommanderOnCondition() => this.GetSelectedCulture().StringId == "empire" && this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Retainer;

    protected void YouthCommanderOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthGroomOnCondition() => this.GetSelectedCulture().StringId == "vlandia" && this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Retainer;

    protected void YouthCommanderOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 10;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_decisive"
      });
    }

    protected void YouthGroomOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 10;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_sharp"
      });
    }

    protected void YouthChieftainOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 10;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_ready"
      });
    }

    protected void YouthCavalryOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 9;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_apprentice"
      });
    }

    protected void YouthHearthGuardOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 9;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_athlete"
      });
    }

    protected void YouthOutridersOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 2;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_gracious"
      });
    }

    protected void YouthOtherOutridersOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 2;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_gracious"
      });
    }

    protected void YouthInfantryOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 3;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_fierce"
      });
    }

    protected void YouthSkirmisherOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 4;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_fox"
      });
    }

    protected void YouthGarrisonOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 1;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_vibrant"
      });
    }

    protected void YouthOtherGarrisonOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 1;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_sharp"
      });
    }

    protected void YouthKernOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 8;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_apprentice"
      });
    }

    protected void YouthCamperOnConsequence(CharacterCreation characterCreation)
    {
      this.SelectedTitleType = 5;
      this.RefreshPlayerAppearance(characterCreation);
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_militia"
      });
    }

    protected void YouthGroomOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthChieftainOnCondition() => (this.GetSelectedCulture().StringId == "battania" || this.GetSelectedCulture().StringId == "khuzait") && this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Retainer;

    protected void YouthChieftainOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthCavalryOnCondition() => this.GetSelectedCulture().StringId == "empire" || this.GetSelectedCulture().StringId == "khuzait" || this.GetSelectedCulture().StringId == "aserai" || this.GetSelectedCulture().StringId == "vlandia";

    protected void YouthCavalryOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthHearthGuardOnCondition() => this.GetSelectedCulture().StringId == "sturgia" || this.GetSelectedCulture().StringId == "battania";

    protected void YouthHearthGuardOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthOutridersOnCondition() => this.GetSelectedCulture().StringId == "empire" || this.GetSelectedCulture().StringId == "khuzait";

    protected void YouthOutridersOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthOtherOutridersOnCondition() => this.GetSelectedCulture().StringId != "empire" && this.GetSelectedCulture().StringId != "khuzait";

    protected void YouthOtherOutridersOnApply(CharacterCreation characterCreation)
    {
    }

    protected void YouthInfantryOnApply(CharacterCreation characterCreation)
    {
    }

    protected void YouthSkirmisherOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthGarrisonOnCondition() => this.GetSelectedCulture().StringId == "empire" || this.GetSelectedCulture().StringId == "vlandia";

    protected void YouthGarrisonOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthOtherGarrisonOnCondition() => this.GetSelectedCulture().StringId != "empire" && this.GetSelectedCulture().StringId != "vlandia";

    protected void YouthOtherGarrisonOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthSkirmisherOnCondition() => this.GetSelectedCulture().StringId != "battania";

    protected bool YouthKernOnCondition() => this.GetSelectedCulture().StringId == "battania";

    protected void YouthKernOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool YouthCamperOnCondition() => this._familyOccupationType != ScCharacterCreationContent.OccupationTypes.Retainer;

    protected void YouthCamperOnApply(CharacterCreation characterCreation)
    {
    }

    protected void AccomplishmentOnInit(CharacterCreation characterCreation)
    {
      characterCreation.IsPlayerAlone = true;
      characterCreation.HasSecondaryCharacter = false;
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge((float) this.AccomplishmentAge));
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_schooled"
      });
      this.RefreshPlayerAppearance(characterCreation);
    }

    protected void AccomplishmentDefeatedEnemyOnApply(CharacterCreation characterCreation)
    {
    }

    protected void AccomplishmentExpeditionOnApply(CharacterCreation characterCreation)
    {
    }

    protected bool AccomplishmentRuralOnCondition() => this.RuralType();

    protected bool AccomplishmentMerchantOnCondition() => this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Merchant;

    protected bool AccomplishmentPosseOnConditions() => this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Retainer || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Herder || this._familyOccupationType == ScCharacterCreationContent.OccupationTypes.Mercenary;

    protected bool AccomplishmentSavedVillageOnCondition() => this.RuralType() && this._familyOccupationType != ScCharacterCreationContent.OccupationTypes.Retainer && this._familyOccupationType != ScCharacterCreationContent.OccupationTypes.Herder;

    protected bool AccomplishmentSavedStreetOnCondition() => !this.RuralType() && this._familyOccupationType != ScCharacterCreationContent.OccupationTypes.Merchant && this._familyOccupationType != ScCharacterCreationContent.OccupationTypes.Mercenary;

    protected bool AccomplishmentUrbanOnCondition() => !this.RuralType();

    protected void AccomplishmentWorkshopOnApply(CharacterCreation characterCreation)
    {
    }

    protected void AccomplishmentSiegeHunterOnApply(CharacterCreation characterCreation)
    {
    }

    protected void AccomplishmentEscapadeOnApply(CharacterCreation characterCreation)
    {
    }

    protected void AccomplishmentTreaterOnApply(CharacterCreation characterCreation)
    {
    }

    protected void AccomplishmentDefeatedEnemyOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_athlete"
    });

    protected void AccomplishmentExpeditionOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_gracious"
    });

    protected void AccomplishmentMerchantOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_ready"
    });

    protected void AccomplishmentSavedVillageOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_vibrant"
    });

    protected void AccomplishmentSavedStreetOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_vibrant"
    });

    protected void AccomplishmentWorkshopOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_decisive"
    });

    protected void AccomplishmentSiegeHunterOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_tough"
    });

    protected void AccomplishmentEscapadeOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_clever"
    });

    protected void AccomplishmentTreaterOnConsequence(CharacterCreation characterCreation) => characterCreation.ChangeCharsAnimation(new List<string>()
    {
      "act_childhood_manners"
    });

    protected void StartingAgeOnInit(CharacterCreation characterCreation)
    {
      characterCreation.IsPlayerAlone = true;
      characterCreation.HasSecondaryCharacter = false;
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge((float) this._startingAge));
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_schooled"
      });
      this.RefreshPlayerAppearance(characterCreation);
    }

    protected void StartingAgeYoungOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge(20f));
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_focus"
      });
      this.RefreshPlayerAppearance(characterCreation);
      this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.YoungAdult;
      this.SetHeroAge(20f);
    }

    protected void StartingAgeAdultOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge(30f));
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_ready"
      });
      this.RefreshPlayerAppearance(characterCreation);
      this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.Adult;
      this.SetHeroAge(30f);
    }

    protected void StartingAgeMiddleAgedOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge(40f));
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_sharp"
      });
      this.RefreshPlayerAppearance(characterCreation);
      this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.MiddleAged;
      this.SetHeroAge(40f);
    }

    protected void StartingAgeElderlyOnConsequence(CharacterCreation characterCreation)
    {
      characterCreation.ClearFaceGenPrefab();
      characterCreation.ChangeFaceGenChars(ScCharacterCreationContent.ChangePlayerFaceWithAge(50f));
      characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_childhood_tough"
      });
      this.RefreshPlayerAppearance(characterCreation);
      this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.Elder;
      this.SetHeroAge(50f);
    }

    protected void StartingAgeYoungOnApply(CharacterCreation characterCreation) => this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.YoungAdult;

    protected void StartingAgeAdultOnApply(CharacterCreation characterCreation) => this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.Adult;

    protected void StartingAgeMiddleAgedOnApply(CharacterCreation characterCreation) => this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.MiddleAged;

    protected void StartingAgeElderlyOnApply(CharacterCreation characterCreation) => this._startingAge = ScCharacterCreationContent.SandboxAgeOptions.Elder;

    protected void ApplyEquipments(CharacterCreation characterCreation)
    {
      ScCharacterCreationContent.ClearMountEntity(characterCreation);
      MBEquipmentRoster instance = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>("player_char_creation_" + this.GetSelectedCulture().StringId + "_" + this.SelectedTitleType.ToString() + (Hero.MainHero.IsFemale ? "_f" : "_m"));
      this.PlayerStartEquipment = instance?.DefaultEquipment ?? MBEquipmentRoster.EmptyEquipment;
      this.PlayerCivilianEquipment = (instance != null ? instance.GetCivilianEquipments().FirstOrDefault<Equipment>() : (Equipment) null) ?? MBEquipmentRoster.EmptyEquipment;
      if (this.PlayerStartEquipment != null && this.PlayerCivilianEquipment != null)
      {
        CharacterObject.PlayerCharacter.Equipment.FillFrom(this.PlayerStartEquipment);
        CharacterObject.PlayerCharacter.FirstCivilianEquipment.FillFrom(this.PlayerCivilianEquipment);
      }
      ScCharacterCreationContent.ChangePlayerMount(characterCreation, Hero.MainHero);
    }

    protected void SetHeroAge(float age) => Hero.MainHero.SetBirthDay(CampaignTime.YearsFromNow(-age));

    protected enum SandboxAgeOptions
    {
      YoungAdult = 20, // 0x00000014
      Adult = 30, // 0x0000001E
      MiddleAged = 40, // 0x00000028
      Elder = 50, // 0x00000032
    }

    protected enum OccupationTypes
    {
      Artisan,
      Bard,
      Retainer,
      Merchant,
      Farmer,
      Hunter,
      Vagabond,
      Mercenary,
      Herder,
      Healer,
      NumberOfTypes,
    }
  }
}
