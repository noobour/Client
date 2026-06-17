using Godot;

namespace Skinning.Objects;

public partial class Label : SkinNode<Godot.Label>
{
    [Skinnable]
    public string Text { get; set; } = "Label";

    [Skinnable]
    public int TextSize { get; set; } = 16;

    [Skinnable]
    public FontFile Font { get; set; } = new();

    public Label()
    {
        Decorability = DecorabilityType.Flat;
    }

    public override void SyncNode()
    {
        Node.Text = Text;
        Node.AddThemeFontSizeOverride("font_size", TextSize);
        Node.AddThemeFontOverride("font", Font);
    }

    public override void ProcessNode(double delta, Attempt attempt)
    {

    }
}
