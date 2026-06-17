using System.Collections.Generic;

namespace Skinning;

/// <summary>
///
/// </summary>
public abstract class SkinCategory
{
    public virtual string Name { get; protected set; } = "SkinCategory";

    public virtual List<SkinObject> Objects { get; protected set; } = [];

    public SkinCategory()
    {
        Name = GetType().Name;
    }
}
