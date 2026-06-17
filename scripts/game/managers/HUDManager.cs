using System.Collections.Generic;
using Godot;
using Skinning;

public partial class HUDManager : Node
{
    [Export] public Runner Runner;

    // private List<IUIComponent> components = [];

    public void Init(SkinProfileNew skin)
    {
        Runner ??= GetParent<Runner>();

        GD.Print(skin.Name);

        // components = findAllComponents(this);

        // foreach (var component in components)
        // {
        //     component.Runner = Runner;
        //     component.Init();
        // }
    }

    public void DisplayModifier(Modifier mod)
    {
        // TODO: display activated modifiers & clear on Init
        GD.Print($"HUD: display {mod.Name}");
    }

    public override void _Process(double delta)
    {
        if (Runner?.Attempt == null) return;

        // foreach (var component in components)
        // {
        //     component.Process(delta, Runner.Attempt);
        // }
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
