// Decompiled with JetBrains decompiler
// Type: SeparatistCrisis.Util.DebugRender
// Assembly: SeparatistCrisis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1CDD36C-482A-41B4-A2A5-494C6268BAE7
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Separatist Crisis Main\bin\Win64_Shipping_Client\SeparatistCrisis.dll

using TaleWorlds.Library;

namespace SeparatistCrisis.Util
{
  public static class DebugRender
  {
    public static void RenderCircle(Vec3 pos, float radius, int numVerts, float deltaTime)
    {
      float num = 6.28318548f / (float) numVerts;
      Vec3 vec3_1 = pos;
      for (int index = 0; index <= numVerts; ++index)
      {
        float x = (float) index * num;
        Vec3 vec3_2 = new Vec3(MathF.Cos(x), MathF.Sin(x));
        vec3_2 = pos + radius * vec3_2;
        if (index == 0)
          ;
        vec3_1 = vec3_2;
      }
    }
  }
}
