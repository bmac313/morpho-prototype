using Godot;
using System;

public class Player : Area2D
{
	// Member variables
	[Export]
	public int Speed = 400;     // Player's movement speed (px/s)
	public Vector2 ScreenSize;  // Size of the game window

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var velocity = Vector2.Zero;  // Init player's movement vector
		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		
		if(Input.IsActionPressed("move_right"))
		{
			velocity.x += 1;
		}
		if(Input.IsActionPressed("move_left"))
		{
			velocity.x -= 1;
		}
		if(Input.IsActionPressed("move_up"))
		{
			velocity.y -= 1;
		}
		if(Input.IsActionPressed("move_down"))
		{
			velocity.y += 1;
		}
		
		if(velocity.Length() > 0)  // If the player is moving
		{
			// Use Speed to calculate movement vector (normalized i.e. set to a length of 1)
			// Then begin movement with Play().
			// Normalize is use to prevent the player from moving faster than normal diagonally (i.e. two keys being pressed at once)
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		}
		else  // Have the player stop if the movement vector is zero
		{
			animatedSprite.Stop();
		}
		
		// "Clamp" the player position to prevent player from leaving screen.
		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
			y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
		);
	}
}
