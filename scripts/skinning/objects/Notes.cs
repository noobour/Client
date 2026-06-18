using Godot;

namespace Skinning.Objects;

public partial class Notes : SkinNode<MultiMeshInstance3D>
{
    [Skinnable]
    public double Size { get; set; } = 7 / 8;

    [Skinnable]
    public ArrayMesh Mesh { get; set; } = new();

    public Notes()
    {
        Persistent = true;
    }

    public override void SyncNode()
    {
        // Node.Multimesh.Mesh = Mesh;
        Node.Multimesh.Mesh = SkinManager.Instance.Skin.NoteMesh;
    }

    public override void ProcessNode(double delta, Attempt attempt) { }
}
