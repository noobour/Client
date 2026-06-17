using System.Collections.Generic;
using Skinning.Objects;

namespace Skinning.Categories;

public partial class HUD : SkinCategory
{
    public Screen Screen { get; private set; } = new();

    public World World { get; private set; } = new();

    public override List<SkinObject> Objects => [Screen, World, new Label()];
}
