using System.Collections.Generic;
using Godot;
using Skinning;

public partial class HUDManager : Node
{
    [Export] public Runner Runner;

    public SkinProfileNew Skin;

    // private List<IUIComponent> components = [];

    public void Init(SkinProfileNew skin)
    {
        Skin = skin;
        Runner ??= GetParent<Runner>();

        Skin.HUD.World.Grid.InitNode(Runner.Grid);
        Skin.HUD.World.Cursor.InitNode(Runner.Cursor);
        Skin.HUD.World.Notes.InitNode(Runner.GetRenderer<NoteRenderer>().NoteMultiMesh);

        foreach (var skinObject in Skin.HUD.Objects)
        {
            if (skinObject.Persistent) continue;

            if (skinObject is ISkinNode skinNode)
            {
                AddChild(skinNode.InitNode());
            }
        }
    }

    public void DisplayModifier(Modifier mod)
    {
        // TODO: display activated modifiers & clear on Init
        GD.Print($"HUD: display {mod.Name}");
    }

    public override void _Process(double delta)
    {
        if (Runner?.Attempt == null) return;

        foreach (var skinObject in Skin.HUD.Objects)
        {
            if (skinObject is ISkinNode skinNode)
            {
                skinNode.ProcessNode(delta, Runner.Attempt);
            }
        }
    }

    // private List<IUIComponent> findAllComponents(Node root)
    // {
    //     List<IUIComponent> comps = new();

    //     foreach (Node child in root.GetChildren())
    //     {
    //         if (child is IUIComponent component)
    //             comps.Add(component);

    //         comps.AddRange(findAllComponents(child));
    //     }

    //     return comps;
    // }
}
