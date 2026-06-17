using Godot;

namespace Skinning.Objects;

public partial class Cursor : SkinNode<MeshInstance3D>
{
    [Skinnable]
    public Texture2D Image { get; set; } = new();

    [Skinnable]
    public Texture2D TrailImage { get; set; } = new();

    [Skinnable]
    public Vector2 Size { get; set; } = Vector2.One;

    [Skinnable]
    public double Rotation { get; set; } = 0;

    public Cursor()
    {
        Persistent = true;
    }

    public override void InitNode()
    {
        (Node.GetActiveMaterial(0) as StandardMaterial3D).AlbedoTexture = Image;
    }

    public override void UpdateNode(double delta = 0)
    {
        Node.RotationDegrees += Vector3.Back * (float)Rotation * (float)delta;
    }
}
