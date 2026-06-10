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
            float rotation = (float)Math.Pow(depth, 1.5) / 60;
            rotation *= (float)Math.Sin(attempt.Progress / attempt.Speed / 1000);

            note.Transform = note.Transform.Rotated(Vector3.Back, rotation);
        }
    }
}
