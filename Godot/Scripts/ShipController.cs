using Godot;

/// <summary>
/// Control de nave porteado desde Unity (ShipController.cs).
/// Avance constante, turbo con Espacio, giro con A/D o flechas, roll visual.
/// </summary>
public partial class ShipController : Node3D
{
	[ExportGroup("Movimiento")]
	[Export] public float NormalSpeed = 20f;
	[Export] public float BoostSpeed = 35f;
	[Export] public float TurnSpeed = 100f;

	[ExportGroup("Inclinación (Roll)")]
	[Export] public float MaxRoll = 35f;
	[Export] public float RollSpeed = 5f;

	private float _currentSpeed;

	public override void _Ready()
	{
		_currentSpeed = NormalSpeed;
	}

	public override void _Process(double delta)
	{
		float d = (float)delta;

		// Turbo con Espacio (igual que Unity: Input.GetKey(KeyCode.Space))
		if (Input.IsKeyPressed(Key.Space))
			_currentSpeed = BoostSpeed;
		else
			_currentSpeed = NormalSpeed;

		// Mover hacia adelante constantemente (-Z es forward en Godot)
		Translate(new Vector3(0, 0, -1) * _currentSpeed * d);

		// Giro con A/D, flechas (acciones ui_left/ui_right por defecto)
		float steer = Input.GetAxis("ui_left", "ui_right");
		RotateY(Mathf.DegToRad(steer * TurnSpeed * d * -1f));

		// Roll visual basado en el giro
		float targetRoll = -steer * MaxRoll;
		Vector3 rot = RotationDegrees;
		rot.Z = Mathf.Lerp(rot.Z, targetRoll, RollSpeed * d);
		RotationDegrees = rot;
	}
}
