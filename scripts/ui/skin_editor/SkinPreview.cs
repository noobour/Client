using Godot;
using Skinning;
using Skinning.Categories;

public partial class SkinPreview : Panel
{
    [Export] public Runner Runner;

    /// <summary>
    /// Builds a preview scene from the <see cref="SkinCategory"/>.
    /// </summary>
    public void Build(SkinCategory category)
    {
        if (category is HUD)
        {
            buildHUD(SkinEditor.Instance.Skin);
        }
    }

    private void buildHUD(SkinProfileNew skin)
    {
        Runner.Skin = skin;
        Runner.HUDManager.Init(skin);
    }
}
