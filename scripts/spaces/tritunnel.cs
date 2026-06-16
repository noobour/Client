using Godot;

namespace Spaces;

public partial class tritunnel : BaseSpace
{
    private SettingsProfile settings;
    private MeshInstance3D tunnel;
    private StandardMaterial3D tunnelTexture;
    private const float tunnel_loop_end = 148f;
    private Vector3 tunnelPosReset;
    private Color tunnelColorReset = new(255, 255, 255);

    public override void _Ready()
    {
        base._Ready();
        settings = SettingsManager.Instance.Settings;
        tunnel = GetNode<MeshInstance3D>("Tunnel");
        tunnelPosReset = tunnel.Position;
        tunnelTexture = tunnel.MaterialOverride as StandardMaterial3D;

    }

    public override void _Process(double delta)
    {
        base._Process(delta);


        // Tunnel movement
        if (tunnel.Position.Z < tunnel_loop_end)
        {
            tunnel.Position += Vector3.Back * (float)(settings.ApproachRate * (float)delta / 6);
        }
        else
        {
            tunnel.Position = tunnelPosReset;
        }
        ;

        // hit sfx
        tunnelTexture.AlbedoColor = settings.SpaceHitEffects ? NoteHitColor : tunnelColorReset;
    }
}
