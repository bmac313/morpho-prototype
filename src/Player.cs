using Godot;
using System;

public class Player : Area2D
{
	// Represents the event for an enemy colliding with the player
	[Signal]
	public delegate void Hit();
	
	// Member variables
	[Export]
	public int Speed = 400;     // Player's movement speed (px/s)
	public Vector2 ScreenSize;  // Size of the game window

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();  // Player should be hidden at the beginning of the game
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
		
		// Set player animations based on speed and direction
		if(velocity.x != 0)
		{
			animatedSprite.Animation = "walk";
			animatedSprite.FlipV = false;
			animatedSprite.FlipH = velocity.x < 0;
		}
		else if(velocity.y != 0)
		{
			animatedSprite.Animation = "up";
			animatedSprite.FlipV = velocity.y > 0;
		}
	}
	
	// Call this function when starting a new game
	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("PlayerCollision").Disabled = false;
	}
	
	// This function calls when an enemy collides with the player
	// using the "body_entered" signal.
	// This will emit the "Hit" signal (declared at the top of this class)
	public void OnPlayerBodyEntered(PhysicsBody2D body)
	{
		
		Hide();  // Player vanishes after colliding
		EmitSignal(nameof(Hit));
		// Disables player collisions so that "Hit" is not triggererd more than once.
		// SetDeferred ensures that Godot will wait so that collisions are not disabled during collision processing.
		GetNode<CollisionShape2D>("PlayerCollision").SetDeferred("disabled", true);
	}
	
}
