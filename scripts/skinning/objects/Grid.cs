using Godot;

namespace Skinning.Objects;

public partial class Grid : SkinNode<MeshInstance3D>
{
    [Skinnable]
    public Texture2D Image { get; set; } = new();

    public Grid()
    {
        Persistent = true;
        Decorability = DecorabilityType.Flat;
    }

    public override void SyncNode()
    {

    }

    public override void ProcessNode(double delta, Attempt attempt) { }
}
