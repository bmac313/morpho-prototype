using Godot;
using System;

public enum ChestStates
{
	CLOSED,
	OPEN
}

public class Chest : Area2D
{
	// Member variables
	public Texture chestOpen = (Texture)ResourceLoader.Load("res://art/chestOpen.png");
	public Texture chestClosed = (Texture)ResourceLoader.Load("res://art/chestClosed.png");
	public bool interactable;
	[Export]
	public ChestStates state;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set default values for member variables
		interactable = true;
		state = ChestStates.CLOSED;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// checks if the chest has been interacted with every frame. interactable flag is set to false if it has.
		if(state == ChestStates.OPEN){
			GetNode<Sprite>("Sprite").Texture = chestOpen;
			interactable = false;
		}
		
	}
}
