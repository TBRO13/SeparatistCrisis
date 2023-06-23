using SeparatistCrisis.Util;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
  public class PartyTeleportation : ScriptComponentBehavior
  {
    public float radius = 1f;
    public bool showDebugInfo = true;
    public int numVerts = 12;

    private Vec3 TargetPosition { get; set; } = Vec3.Zero;

    private Vec3 Position { get; set; }

    public MobilePartyAi Ai { get; private set; }

    protected override void OnInit()
    {
      base.OnInit();
      this.SetScriptComponentToTick(this.GetTickRequirement());
      this.Setup();
    }

    protected override void OnEditorInit()
    {
      base.OnEditorInit();
      this.SetScriptComponentToTick(this.GetTickRequirement());
      this.Setup();
    }

    public override ScriptComponentBehavior.TickRequirement GetTickRequirement() => ScriptComponentBehavior.TickRequirement.Tick;

    protected override void OnTick(float dt)
    {
      foreach (MobileParty party in new List<MobileParty>())
      {
        if (this.IsPartyInArea(party))
        {
          party.Position2D = this.TargetPosition.AsVec2;
          party.Ai.SetMoveModeHold();
        }
      }
    }

    protected override void OnEditorTick(float dt)
    {
      this.Setup();
      if (!this.showDebugInfo)
        return;
      Vec3 vec3 = this.TargetPosition - this.Position;
      DebugRender.RenderCircle(this.Position, this.radius, this.numVerts, dt);
    }

    private void Setup()
    {
      this.Position = this.GameEntity.GetFrame().origin;
      if (this.GameEntity.ChildCount <= 0)
        return;
      this.TargetPosition = this.GameEntity.GetChild(0).GlobalPosition;
    }

    private bool IsPartyInArea(MobileParty party)
    {
      float num1 = party.GetPosition2D.x - this.Position.x;
      float num2 = party.GetPosition2D.y - this.Position.y;
      return (double) num1 * (double) num1 + (double) num2 * (double) num2 <= (double) this.radius * (double) this.radius;
    }
  }
}
