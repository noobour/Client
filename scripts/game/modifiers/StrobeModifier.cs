public class StrobeModifier : Modifier, IObjectRenderModifier<Note>
{
    public override string Name => "Strobe";

    public override bool Rankable => true;

    public override double ScoreMultiplier => 1.035;

    public void ModifyRenderObject(Note note, Attempt attempt)
    {
        // if (attempt.Progress / 1000 / attempt.Speed % 0.5 > 0.25)
        double at = attempt.Settings.ApproachTime;

        if (attempt.Progress / 1000 / attempt.Speed % at >= at / 2)
        {
            note.Opacity = 0;
        }
    }
}
