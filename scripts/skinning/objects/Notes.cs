using Godot;

namespace Skinning.Objects;

public partial class Notes : SkinNode<MultiMeshInstance3D>
{
    [Skinnable]
    public double Size { get; set; } = 7/8;

    [Skinnable]
    public ArrayMesh Mesh { get; set; }

    public Notes()
    {
        Persistent = true;
    }

    public override void InitNode()
    {
        Node.Multimesh.Mesh = Mesh;
    }

    public override void UpdateNode(double delta = 0) { }
}
