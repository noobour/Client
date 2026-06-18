using System.Collections.Generic;
using Godot;
using Skinning;

public partial class SkinProperties : Panel
{
    /// <summary>
    /// Currently selected <see cref="Skinning.SkinObject"/> to edit properties of.
    /// </summary>
    public SkinObject SkinObject;

    [Export]
    private VBoxContainer itemContainer;

    private Dictionary<string, Stack<SkinPropertyItem>> itemCache = [];

    private List<SkinPropertyItem> items = [];

    private readonly PackedScene itemTemplate = ResourceLoader.Load<PackedScene>("res://prefabs/ui/skin_editor/skin_property_item.tscn");

    public override void _Ready()
    {

    }

    /// <summary>
    /// Builds and displays the <see cref="Skinning.SkinObject"/>'s properties which have <see cref="Skinning.SkinObject.SkinnableAttribute"/>.
    /// </summary>
	public void Build(SkinObject skinObject)
    {
        if (SkinObject == skinObject)
        {
            return;
        }

        SkinObject = skinObject;

        Clear();

        if (skinObject != null)
        {
            foreach (var p in skinObject.GetProperties())
            {
                buildProperty(p.Name, p.PropertyType.Name, p.GetValue(skinObject));
            }
        }
    }

    /// <summary>
    /// Clears the displayed <see cref="Skinning.SkinObject"/>'s properties.
    /// </summary>
	public void Clear(bool cache = true)
    {
        foreach (var item in items)
        {
            if (cache)
            {
                itemCache[item.Type].Push(item);
                item.GetParent()?.RemoveChild(item);
            }
            else
            {
                item.QueueFree();
            }
        }

        items = [];
    }

    private SkinPropertyItem buildProperty(string name, string type, object value)
    {
        var item = createItem(type);

        item.SetProperty(name, type, value);
        items.Add(item);
        itemContainer.AddChild(item);

        return item;
    }

    private SkinPropertyItem createItem(string type)
    {
        if (!itemCache.TryGetValue(type, out var typeCache))
        {
            typeCache = [];
            itemCache[type] = typeCache;
        }

        if (typeCache.TryPop(out var item))
        {
            return item;
        }

        item = itemTemplate.Instantiate<SkinPropertyItem>();



        return item;
    }
}
