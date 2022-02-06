using Godot;
using System;

public class Main : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	public void NewGame()
	{
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
