using System;
using Godot;

public class VortexModifier : Modifier, IObjectRenderModifier<Note>
{
    public override string Name => "Vortex";

    public override bool Rankable => true;

    public override double ScoreMultiplier => 1.01;

    public void ModifyRenderObject(Note note, Attempt attempt)
    {
        float depth = Math.Max(0, -note.Transform.Origin.Z);

        if (depth > 0)
        {
            depth /= (float)attempt.Settings.ApproachDistance;

            float rotation = (float)Math.Pow(depth * 15, 1.5) / 60;
            float sine = (float)Math.Sin(attempt.Progress / attempt.Speed / 1000 * attempt.Settings.ApproachTime);
            rotation *= (float)(Math.Sign(sine) * Math.Pow(Math.Abs(sine), 0.75));

            note.Transform = note.Transform.Rotated(Vector3.Back, rotation);
        }
    }
}
