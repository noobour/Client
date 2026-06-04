public class VerticalFlipModifier : Modifier, IMapModifier
{
    public override string Name => "VFlip";

    public override bool Rankable => true;

    public void ModifyMap(Map map, Attempt attempt)
    {
        foreach (var note in map.Notes)
        {
            note.Y = -note.Y;
        }
    }
}
