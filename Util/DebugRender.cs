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
