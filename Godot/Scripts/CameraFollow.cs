using Godot;

/// <summary>
/// Cámara que sigue a la nave, porteado desde Unity (CameraFollow.cs).
/// Asigna Target en el editor a la nave.
/// </summary>
public partial class CameraFollow : Camera3D
{
	[Export] public Node3D Target;
	[Export] public Vector3 Offset = new Vector3(0f, 3f, 6f);
	[Export] public float SmoothSpeed = 10f;

	public override void _Process(double delta)
	{
		if (Target == null)
			return;

		float d = (float)delta;

		// Posición deseada detrás de la nave (en espacio global de la nave)
		Vector3 desiredPosition = Target.GlobalTransform * Offset;

		// Movimiento suave
		GlobalPosition = GlobalPosition.Lerp(desiredPosition, SmoothSpeed * d);

		// Mirar a la nave (un poco arriba, como en Unity + Vector3.up)
		LookAt(Target.GlobalPosition + Vector3.Up * 1f, Vector3.Up);
	}
}
