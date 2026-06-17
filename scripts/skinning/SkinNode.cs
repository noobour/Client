using Godot;

namespace Skinning;

public abstract partial class SkinNode<T> : SkinObject
    where T : Node
{
    /// <summary>
    ///
    /// </summary>
    public virtual T Node {
        get;
        set { field = value; InitNode(); }
    }

    /// <summary>
    ///
    /// </summary>
    public abstract void InitNode();

    /// <summary>
    ///
    /// </summary>
    public abstract void UpdateNode(double delta = 0);
}
