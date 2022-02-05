using Godot;
using System;

public class Mob : RigidBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		animSprite.Playing = true;
		string[] mobTypes = animSprite.Frames.GetAnimationNames();
		// The below will pick a random animation for the enemy sprite
		animSprite.Animation = mobTypes[GD.Randi() % mobTypes.Length];
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

public void OnVisibilityNotifier2DScreenExited()
{
	QueueFree();  // Tells mob to delete itself after leaving the screen.
}

}
