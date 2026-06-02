using System;
using System.Collections.Generic;
using Godot;

public partial class ModifierButton : Button
{
    [Export]
    public string Modifier = "";

    public override void _Ready()
    {
        base._Ready();

        TooltipText = Modifier;

        updateState(Lobby.Modifiers);

        // Lobby.Instance.ModifiersChanged += updateState;
    }

    public override void _Pressed()
    {
        base._Pressed();

        // if (Lobby.Modifiers.TryGetValue(Modifier, out bool active))
        // {
        //     Lobby.SetModifier(Modifier, !active);
        // }
    }

    private void updateState(List<Mod> mods)
    {
        // if (IsInstanceValid(this) && mods.TryGetValue(Modifier, out bool active))
        // {
        //     ButtonPressed = active;
        // }
    }
}
