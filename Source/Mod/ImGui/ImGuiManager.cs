using Celeste64.Mod.Editor;
using ImGuiNET;

namespace Celeste64.Mod;

public class ImGuiManager
{
	/// <summary>
	/// Whether the keyboard input was consumed by Dear ImGui. 
	/// </summary>
	public static bool WantCaptureKeyboard { get; private set; }

	/// <summary>
	/// Whether the mouse input  was consumed by Dear ImGui. 
	/// </summary>
	public static bool WantCaptureMouse { get; private set; }

	private readonly ImGuiRenderer renderer;
	private static FujiDebugMenu debugMenu = new FujiDebugMenu();
	private static IEnumerable<ImGuiHandler> Handlers => ModManager.Instance.EnabledMods.SelectMany(mod => mod.ImGuiHandlers);

	internal ImGuiManager()
	{
		renderer = new ImGuiRenderer();
		renderer.RebuildFontAtlas();
	}

	internal void UpdateHandlers()
	{
		renderer.Update();

		if (debugMenu.Active)
			debugMenu.Update();
        
        if (Game.Scene is EditorScene editor)
        {
	        foreach (var handler in editor.Handlers)
	        {
		        if (handler.Active) handler.Update();
	        }			
        }

		foreach (var handler in Handlers)
		{
			if (handler.Active) handler.Update();
		}
	}

	internal void RenderHandlers()
	{
		renderer.BeforeRender();
        
		if (debugMenu.Visible)
			debugMenu.Render();
		
		if (Game.Scene is EditorScene editor)
		{
			foreach (var handler in editor.Handlers)
			{
				if (handler.Visible) handler.Render();
			}			
		}

		foreach (var handler in Handlers)
		{
			if (handler.Visible) handler.Render();
		}
		renderer.AfterRender();

		var io = ImGui.GetIO();
		WantCaptureKeyboard = io.WantCaptureKeyboard;
		WantCaptureMouse = io.WantCaptureMouse;
	}

	internal void RenderTexture(Batcher batch)
	{
		if (renderer.target == null) return;
		batch.Image(renderer.target, Vec2.Zero, Color.White);
	}
}