using Godot;
using System;

public partial class Critter : CharacterBody2D
{
	public static int Speed = 1000;

	//// Called when the node enters the scene tree for the first time.
	//public override void _Ready()
	//{
	//}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.GetParent<PathFollow2D>().Progress = this.GetParent<PathFollow2D>().Progress + Speed * (float)delta;
	}
}
