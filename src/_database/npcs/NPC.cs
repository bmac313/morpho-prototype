using Godot;
using System;

// This is a base class used to create NPCs and should NOT be instantiated directly.
// Instead, use one of its subclasses.

public class NPC : Area2D
{	
	// Member variables
	public int id;
	public string name;
	[Export]
	public bool interactable;
	[Export]
	public bool unique;  // Set to true or false based on whether the character is unique or generic.

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
