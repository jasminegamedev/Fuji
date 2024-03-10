namespace Celeste64.Mod.Editor;

public class TestDefinition : Definition
{
	private readonly Mesh mesh = new();
	private readonly EditorMaterial material = new();
	
	public TestDefinition()
	{
		Vec3 size = Vec3.One * 3.0f;
		Vec3 color = Vec3.One;
		
		mesh.SetVertices<EditorVertex>([
			// Front
            /* 0*/ new(new Vec3(0,      0,      0),      Vec2.Zero,                color, -Vec3.UnitY),
            /* 1*/ new(new Vec3(size.X, 0,      0),      Vec2.UnitX * size.X,      color, -Vec3.UnitY),
            /* 2*/ new(new Vec3(0,      0,      size.Z), Vec2.UnitY * size.Z,      color, -Vec3.UnitY),
            /* 3*/ new(new Vec3(size.X, 0,      size.Z), new Vec2(size.X, size.Z), color, -Vec3.UnitY),
            // Back
            /* 4*/ new(new Vec3(0,      size.Y, 0),      Vec2.UnitX * size.X,      color,  Vec3.UnitY),
            /* 5*/ new(new Vec3(size.X, size.Y, 0),      Vec2.Zero,                color,  Vec3.UnitY),
            /* 6*/ new(new Vec3(0,      size.Y, size.Z), new Vec2(size.X, size.Z), color,  Vec3.UnitY),
            /* 7*/ new(new Vec3(size.X, size.Y, size.Z), Vec2.UnitY * size.Z,      color,  Vec3.UnitY),
            // Left
            /* 8*/ new(new Vec3(0,      0,      0),      Vec2.UnitX * size.Y,      color, -Vec3.UnitX),
            /* 9*/ new(new Vec3(0,      0,      size.Z), new Vec2(size.Y, size.Z), color, -Vec3.UnitX),
            /*10*/ new(new Vec3(0,      size.Y, 0),      Vec2.Zero,                color, -Vec3.UnitX),
            /*11*/ new(new Vec3(0,      size.Y, size.Z), Vec2.UnitY * size.Z,      color, -Vec3.UnitX),
            // Right
            /*12*/ new(new Vec3(size.X, 0,      0),      Vec2.Zero,                color,  Vec3.UnitX),
            /*13*/ new(new Vec3(size.X, 0,      size.Z), Vec2.UnitY * size.Z,      color,  Vec3.UnitX),
            /*14*/ new(new Vec3(size.X, size.Y, 0),      Vec2.UnitX * size.Y,      color,  Vec3.UnitX),
            /*15*/ new(new Vec3(size.X, size.Y, size.Z), new Vec2(size.Y, size.Z), color,  Vec3.UnitX),
            // Top
            /*16*/ new(new Vec3(0,      0,      size.Z), Vec2.Zero,                color,  Vec3.UnitZ),
            /*17*/ new(new Vec3(size.X, 0,      size.Z), Vec2.UnitX * size.X,      color,  Vec3.UnitZ),
            /*18*/ new(new Vec3(0,      size.Y, size.Z), Vec2.UnitY * size.Y,      color,  Vec3.UnitZ),
            /*19*/ new(new Vec3(size.X, size.Y, size.Z), new Vec2(size.X, size.Y), color,  Vec3.UnitZ),
            // Bottom
            /*20*/ new(new Vec3(0,      0,      0),      Vec2.UnitX * size.X,      color, -Vec3.UnitZ),
            /*21*/ new(new Vec3(size.X, 0,      0),      Vec2.Zero,                color, -Vec3.UnitZ),
            /*22*/ new(new Vec3(0,      size.Y, 0),      new Vec2(size.X, size.Y), color, -Vec3.UnitZ),
            /*23*/ new(new Vec3(size.X, size.Y, 0),      Vec2.UnitY * size.Y,      color, -Vec3.UnitZ),
		]);
		mesh.SetIndices<int>([
			// Front
			0, 1, 2,
			2, 1, 3,
			// Back
			5, 4, 7,
			7, 4, 6,
			// Left
			10, 8, 11,
			11, 8, 9,
			// Right
			12, 14, 13,
			13, 14, 15,
			// Top
			16, 17, 18,
			18, 17, 19,
			// Bottom
			22, 23, 20,
			20, 23, 21
		]);
	}
	
	public override void Render(ref EditorRenderState state)
	{
		state.ApplyToMaterial(material, Matrix.Identity);
		
		new DrawCommand(state.Camera.Target, mesh, material)
		{
			DepthCompare = state.DepthCompare,
			DepthMask = state.DepthMask,
			CullMode = CullMode.None,
		}.Submit();
		state.Calls++;
		state.Triangles += mesh.IndexCount / 3;
	}
}