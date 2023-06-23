// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.Patches.DeactivateVanillaCultureSelection
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace SeparatistCrisis.Patches
{
  public class DeactivateVanillaCultureSelection
  {
    private static bool GetCultures(ref IEnumerable<CultureObject> __result)
    {
      __result = (IEnumerable<CultureObject>) new List<CultureObject>();
      List<CultureObject> second = new List<CultureObject>();
      foreach (CultureObject objectType in (List<CultureObject>) MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
      {
        if (objectType.IsMainCulture && objectType.GetCultureCode() != CultureCode.Aserai && objectType.GetCultureCode() != CultureCode.Battania && objectType.GetCultureCode() != CultureCode.Empire && objectType.GetCultureCode() != CultureCode.Khuzait && objectType.GetCultureCode() != CultureCode.Sturgia && objectType.GetCultureCode() != CultureCode.Vlandia)
          second.Add(objectType);
      }
      __result = __result.Concat<CultureObject>((IEnumerable<CultureObject>) second);
      return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof (CharacterCreationCultureStageVM), "SortCultureList")]
    private static bool SortCultureList(
      MBBindingList<CharacterCreationCultureVM> listToWorkOn)
    {
      return listToWorkOn.Count > 3;
    }
  }
}
