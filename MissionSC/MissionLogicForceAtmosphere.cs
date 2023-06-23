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
