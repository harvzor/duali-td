using Godot;
using System;

public partial class Critter : CharacterBody2D
{
	public const int Speed = 500;
	public int Health = 5;

	//// Called when the node enters the scene tree for the first time.
	//public override void _Ready()
	//{
	//}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.GetParent<PathFollow2D>().Progress = this.GetParent<PathFollow2D>().Progress + Speed * (float)delta;
		
		if (this.Health <= 0)
			this.GetParent<PathFollow2D>().GetParent<Path2D>().QueueFree();
	}
}
