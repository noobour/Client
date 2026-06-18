using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Godot;

namespace Skinning;

/// <summary>
/// Hierarchical data container which holds standard logic for the <see cref="SkinEditor"/>.
/// </summary>
public partial class SkinObject : RefCounted
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SkinnableAttribute : Attribute;

    public enum DecorabilityType
    {
        None,
        Flat,
        Spatial,
        All
    }

    /// <summary>
    /// Unique identifier for the <see cref="SkinObject"/> to be used during serialization.
    /// </summary>
    public Guid GUID { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Name of the <see cref="SkinObject"/> to be displayed by the <see cref="SkinExplorer"/>.
    /// </summary>
    public virtual string Name { get; set; } = "Object";

    /// <summary>
    /// 24x24 icon of the <see cref="SkinObject"/> to be displayed by the <see cref="SkinExplorer"/>.
    /// </summary>
    public virtual Texture2D Icon { get; protected set; } = new();

    /// <summary>
    /// Whether or not the <see cref="SkinObject"/> is always present in a <see cref="SkinProfileNew"/>.
    /// Persistent objects cannot be renamed or deleted.
    /// </summary>
    public virtual bool Persistent { get; protected set; } = false;

    /// <summary>
    /// Whether or not other <see cref="SkinObject"/>s can be added as children under the <see cref="SkinObject"/>.
    /// Supports either 2D, 3D, both or none, as per <see cref="DecorabilityType"/>.
    /// </summary>
    public virtual DecorabilityType Decorability
    {
        get;
        protected set { field = value; Shadeable = value == DecorabilityType.Flat || value == DecorabilityType.All; }
    } = DecorabilityType.None;

    /// <summary>
    /// Whether or not shaders may be applied to the <see cref="SkinObject"/>.
    /// </summary>
    public bool Shadeable { get; private set; } = false;

    /// <summary>
    /// <see cref="SkinObject"/> directly above the <see cref="SkinObject"/> in the <see cref="SkinCategory"/> hierarchy.
    /// </summary>
    public SkinObject Parent { get; set; }

    /// <summary>
    /// <see cref="SkinObject"/>s directly under the <see cref="SkinObject"/> in the <see cref="SkinCategory"/> hierarchy.
    /// </summary>
    public List<SkinObject> Children { get; set; } = [];

    public SkinObject()
    {
        Name = GetType().Name;

        string iconPath = $"res://textures/ui/skinning/{Name.ToSnakeCase()}.png";

        if (ResourceLoader.Exists(iconPath))
        {
            Icon = ResourceLoader.Load<Texture2D>(iconPath);
        }
    }

    /// <summary>
    /// Adds a child <see cref="SkinObject"/> to <see cref="Children"/>.
    /// </summary>
    public void AddChild(SkinObject child)
    {
        child.Parent?.RemoveChild(child);
        child.Parent = this;

        if (!Children.Exists(x => x == child))
        {
            Children.Add(child);
        }
    }

    /// <summary>
    /// Adds multiple <see cref="SkinObject"/>s to <see cref="Children"/>.
    /// </summary>
    public void AddChildren(IEnumerable<SkinObject> children)
    {
        foreach (var child in children)
        {
            AddChild(child);
        }
    }

    /// <summary>
    /// Removes a child <see cref="SkinObject"/> from <see cref="Children"/>.
    /// </summary>
    public void RemoveChild(SkinObject child)
    {
        child = Children.Find(x => x == child);

        if (child == null || child.Parent != this)
        {
            return;
        }

        child.Parent = null;
        Children.Remove(child);
    }

    /// <summary>
    /// Safely updates the <see cref="SkinObject"/>'s <see cref="Name"/>.
    /// </summary>
    public void Rename(string name)
    {
        if (Persistent)
        {
            throw new("Cannot rename a persistent skin object");
        }

        Regex nameRegex = new("[^a-zA-Z0-9()-]");

        Name = nameRegex.Replace(name, "_");
    }

    /// <summary>
    /// Disposes the <see cref="SkinObject"/> if not <see cref="Persistent"/>.
    /// </summary>
    public void Delete()
    {
        if (Persistent)
        {
            throw new("Cannot delete a persistent skin object");
        }


    }

    /// <summary>
    /// Retrieves <see cref="SkinnableAttribute"/> properties of the <see cref="SkinObject"/>.
    /// </summary>
    public IEnumerable<PropertyInfo> GetProperties()
    {
        return GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(SkinnableAttribute)));
    }
}
