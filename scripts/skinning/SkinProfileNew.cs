using System.Collections.Generic;
using Godot;
using Skinning.Categories;

namespace Skinning;

/// <summary>
///
/// </summary>
public partial class SkinProfileNew : RefCounted
{
    public string Name { get; set; }

    public string Path { get; set; }

    public HUD HUD { get; private set; } = new();

    // public SkinCategory Menu { get; private set; }

    // public SkinCategory Themes { get; private set; }

    // public Dictionary<string, List<Resource>> Assets { get; private set; } = [];

    public SkinProfileNew(string name)
    {
        Name = name ?? "Skin";
    }

    public override string ToString() => Name;
}
