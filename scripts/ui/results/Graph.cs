using Godot;

public partial class Graph : ColorRect
{
    public override void _Draw()
    {
        Color hitColor = new(0x00ff00ff);
        Color missColor = new(0xff000044);

        var attempt = Game.Attempt;
        float[] hitsInfo = attempt.IsReplay ? attempt.Replays[0].Notes : attempt.HitsInfo;
        float deathTime = (float)(attempt.IsReplay ? attempt.MaxReplayLength : attempt.DeathTime);

        for (ulong i = 0; i < (ulong)hitsInfo.Length; i++)
        {
            float offset = hitsInfo[i];
            float ms = attempt.Map.Notes[i + attempt.FirstNote].Millisecond;
            float noteProgress = ms / attempt.Map.Length;

            if (ms > deathTime)
            {
                break;
            }

            if (offset < 0)
            {
                int position = (int)(Size.X * noteProgress);
                DrawLine(Vector2.Right * position, new(position, Size.Y), missColor, 1);
            }
            else
            {
                DrawRect(new(Size.X * noteProgress, Size.Y * (offset / 55), Vector2.One), hitColor);
            }
        }

        if (deathTime >= 0)
        {
            int position = (int)(Size.X * deathTime / attempt.Map.Length);
            DrawLine(Vector2.Right * position, new(position, Size.Y), new(0xffff00), 3);
        }
    }
}
