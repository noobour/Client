using Godot;

namespace Skinning;

/// <summary>
/// Non-generic interface for <see cref="SkinNode{T}"/>.
/// </summary>
public interface ISkinNode
{
    /// <summary>
    /// Builds a <see cref="Node"/> for the associated <see cref="SkinNode{T}"/>.
    /// </summary>
    Node BuildNode();

    /// <summary>
    /// Updates and synchronizes a <see cref="SkinNode{T}.Node"/>.
    /// Disposes of the previous <see cref="Node"/> and may use <see cref="BuildNode"/> if necessary.
    /// </summary>
    Node InitNode(Node node = null);

    /// <summary>
    /// One-time update of the <see cref="SkinNode{T}.Node"/> applying <see cref="SkinObject.SkinnableAttribute"/> properties.
    /// </summary>
    void SyncNode();

    /// <summary>
    /// Per-frame update of the <see cref="SkinNode{T}.Node"/>. Only visual or rendering logic should be implemented.
    /// </summary>
    void ProcessNode(double delta, Attempt attempt);
}

/// <summary>
/// <see cref="SkinObject"/> which holds a <see cref="Godot.Node"/> of type <see cref="T"/>.
/// Associated pre-processing visual logic is implemented through <see cref="SyncNode"/>, or <see cref="ProcessNode"/> for each frame.
/// </summary>
public abstract partial class SkinNode<T> : SkinObject, ISkinNode
    where T : Node, new()
{
    /// <summary>
    /// Associated <see cref="Godot.Node"/> of type <see cref="T"/> to the <see cref="SkinNode{T}"/>.
    /// </summary>
    public virtual T Node { get; private set; }

    public SkinNode()
    {
        if (!Persistent)
        {
            Node = BuildNode() as T;
        }
    }

    public virtual Node BuildNode() => new T();

    public Node InitNode(Node node = null) => InitNode(node as T);

    public T InitNode(T node = null)
    {
        if (Persistent && node == null) return null;

        Node?.QueueFree();
        Node = node ?? BuildNode() as T;

        SyncNode();

        return Node;
    }

    public abstract void SyncNode();

    public abstract void ProcessNode(double delta, Attempt attempt);
}
