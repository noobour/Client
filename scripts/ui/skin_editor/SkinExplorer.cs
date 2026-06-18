using System.Collections.Generic;
using Godot;
using Skinning;

public partial class SkinExplorer : Panel
{
    /// <summary>
    /// Currently selected <see cref="SkinCategory"/>.
    /// </summary>
    public SkinCategory Category;

    /// <summary>
    /// Currently selected <see cref="SkinExplorerItem"/>.
    /// </summary>
    public SkinExplorerItem SelectedItem;

    [Export]
    private VBoxContainer itemContainer;

    private Stack<SkinExplorerItem> itemCache = [];

    private List<SkinExplorerItem> items = [];

    private readonly PackedScene itemTemplate = ResourceLoader.Load<PackedScene>("res://prefabs/ui/skin_editor/skin_explorer_item.tscn");

    /// <summary>
    /// Builds and displays the <see cref="SkinObject"/> hierarchy tree from <see cref="SkinCategory.Objects"/>.
    /// </summary>
	public void Build(SkinCategory skinCategory)
    {
        if (Category == skinCategory)
        {
            return;
        }

        Clear();

        Category = skinCategory;

        foreach (var skinObject in skinCategory.Objects)
        {
            buildObject(skinObject);
        }
    }

    /// <summary>
    /// Clears the displayed <see cref="SkinObject"/> hierarchy tree.
    /// </summary>
	public void Clear(bool cache = true)
    {
        SelectedItem = null;

        foreach (var item in items)
        {
            if (cache)
            {
                itemCache.Push(item);
                item.GetParent()?.RemoveChild(item);
            }
            else
            {
                item.QueueFree();
            }
        }

        items = [];
    }

    private SkinExplorerItem buildObject(SkinObject skinObject, SkinExplorerItem parent = null)
    {
        var item = createItem();

        item.SetObject(skinObject);
        items.Add(item);

        if (parent == null)
        {
            itemContainer.AddChild(item);
        }
        else
        {
            parent.AddChildItem(item);
        }

        foreach (var child in skinObject.Children)
        {
            buildObject(child, item);
        }

        return item;
    }

    private SkinExplorerItem createItem()
    {
        if (!itemCache.TryPop(out var item))
        {
            item = itemTemplate.Instantiate<SkinExplorerItem>();

            item.Selected += () =>
            {
                SelectedItem?.Deselect();

                SelectedItem = item;
                item.Select();

                SkinEditor.Instance.Properties.Build(item.SkinObject);
            };
        }

        return item;
    }
}
