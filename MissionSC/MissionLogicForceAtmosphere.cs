// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.MissionSC.MissionLogicForceAtmosphere
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

using System;
using System.IO;
using TaleWorlds.DotNet;
using TaleWorlds.MountAndBlade;

namespace SeparatistCrisis.MissionSC
{
  public class MissionLogicForceAtmosphere : MissionLogic
  {
    private readonly string forceAtmosphereSuffix = "geonosis";
    private readonly string defaultAtmosphere = "geonosis";

    public override void EarlyStart()
    {
      if (!((NativeObject) this.Mission.Scene != (NativeObject) null) || !this.Mission.SceneName.StartsWith(this.forceAtmosphereSuffix, StringComparison.Ordinal))
        return;
      if (File.Exists("../../Modules/SeparatistCrisisGeonosisAssetsandMaps/Atmospheres/" + this.Mission.SceneName + ".xml"))
        this.Mission.Scene.SetAtmosphereWithName(this.Mission.SceneName);
      else
        this.Mission.Scene.SetAtmosphereWithName(this.defaultAtmosphere);
    }
  }
}
