using System;
using Godot;

public class CameraSpin : CameraMode
{
    public override string Name => "Spin";

    public override bool Rankable => true;

    public override void Process(Attempt attempt, ReplayManager replayManager, Camera3D camera, MeshInstance3D cursor, Vector2 mouseDelta, float sensitivity)
    {
        var settings = attempt.Settings;

        if (!attempt.IsReplay)
        {
            camera.Rotation += new Vector3(-mouseDelta.Y / 120 * sensitivity / (float)Math.PI, -mouseDelta.X / 120 * sensitivity / (float)Math.PI, 0);
        }
        else
        {
            camera.Rotation += new Vector3(mouseDelta.Y / (float)Math.PI, -mouseDelta.X / (float)Math.PI, 0);
        }

        camera.Rotation = new Vector3((float)Math.Clamp(camera.Rotation.X, Mathf.DegToRad(-90), Mathf.DegToRad(90)), camera.Rotation.Y, camera.Rotation.Z);

        var origin = new Vector3(0, 0, 3.5f);
        var cursorLock = new Vector3(attempt.CursorPosition.X, attempt.CursorPosition.Y, 0);

        // The pivot is to mimic ROBLOX's orbital camera
        var pivot = camera.Basis.Z / 4f;

        // Proper Parallax Support
        camera.Position = origin + cursorLock * (float)settings.CameraParallax + pivot;

        var lookVector = camera.Basis.Z;
        var cameraVector2 = new Vector2(camera.Position.X, camera.Position.Y);
        var lookVector2 = new Vector2(lookVector.X, lookVector.Y);

        // Project Cursor from Camera's "ray cast"
        attempt.RawCursorPosition = cameraVector2 - lookVector2 * Mathf.Abs(camera.Position.Z / lookVector.Z);
        attempt.CursorPosition = attempt.RawCursorPosition.Clamp(-Constants.BOUNDS, Constants.BOUNDS);

        Vector3 cursorPos = new(attempt.CursorPosition.X, attempt.CursorPosition.Y, 0);

        if (cursorPos.IsFinite())
        {
            cursor.Position = cursorPos;
        }

        // var settings = attempt.Settings;

        // float sensitivity = (float)settings.Sensitivity.Value;
        // sensitivity *= (float)settings.FoV.Value / 70f;

        // camera.Rotation += new Vector3(-mouseDelta.Y / 120 * sensitivity / (float)Math.PI, -mouseDelta.X / 120 * sensitivity / (float)Math.PI, 0);
        // camera.Rotation = new Vector3(Math.Clamp(camera.Rotation.X, Mathf.DegToRad(-90), Mathf.DegToRad(90)), camera.Rotation.Y, camera.Rotation.Z);
        // camera.Position = new Vector3(attempt.CursorPosition.X * 0.25f, attempt.CursorPosition.Y * 0.25f, 3.5f) + camera.Basis.Z / 4;

        // attempt.CameraPosition = camera.Position;
        // attempt.CameraRotation = camera.Rotation;

        // float wtf = 0.95f;
        // float hypotenuse = (wtf + attempt.CameraPosition.Z) / camera.Basis.Z.Z;
        // float distance = (float)Math.Sqrt(Math.Pow(hypotenuse, 2) - Math.Pow(wtf + camera.Position.Z, 2));

        // attempt.RawCursorPosition = new Vector2(camera.Basis.Z.X, camera.Basis.Z.Y).Normalized() * -distance;
        // attempt.CursorPosition = attempt.RawCursorPosition.Clamp(-Constants.BOUNDS, Constants.BOUNDS);
    }
}
