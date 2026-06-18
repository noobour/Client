/// <summary>
/// Must implement <see cref="UpdateSkin"/> and connect it to <see cref="SkinManager.Loaded"/>.
/// </summary>
public interface ISkinnable
{
    void UpdateSkin(SkinProfile skin);
}
