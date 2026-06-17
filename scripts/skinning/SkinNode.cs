using Godot;

namespace Skinning;

/// <summary>
///
/// </summary>
public interface ISkinNode
{
    /// <summary>
    ///
    /// </summary>
    Node BuildNode();

    /// <summary>
    ///
    /// </summary>
    Node InitNode(Node node = null);

    /// <summary>
    ///
    /// </summary>
    void SyncNode();

    /// <summary>
    ///
    /// </summary>
    void ProcessNode(double delta, Attempt attempt);
}

/// <summary>
/// <see cref="SkinObject"/> which holds a <see cref="T"/> Node and associated logic.
/// </summary>
public abstract partial class SkinNode<T> : SkinObject, ISkinNode
    where T : Node, new()
{
    /// <summary>
    ///
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

    public virtual Node InitNode(Node node = null) => InitNode(node as T);

    public virtual T InitNode(T node = null)
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
