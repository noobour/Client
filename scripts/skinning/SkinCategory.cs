using System.Collections.Generic;
using Godot;

namespace Skinning;

/// <summary>
/// Group of <see cref="SkinObject"/>s which may hold a <see cref="PackedScene"/> for previewing.
/// </summary>
public abstract class SkinCategory
{
    public virtual string Name { get; protected set; } = "SkinCategory";

    public virtual List<SkinObject> Objects { get; protected set; } = [];

    public virtual PackedScene PreviewScene { get; private set; }

    public SkinCategory()
    {
        Name = GetType().Name;
    }
}
