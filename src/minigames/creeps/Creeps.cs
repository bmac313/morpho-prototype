using Godot;
using System;

public class Creeps : Node
{
	// Member variables
	public int Score;
	// Disable warning on MobScene as it is assigned in the editor
	#pragma warning disable 649
		[Export]
		public PackedScene MobScene;
	#pragma warning restore 649

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Generates a new random number on each run of the game.
		GD.Randomize();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void NewGame()
	{
		GetTree().CallGroup("mobs", "queue_free");
		
		var hud = GetNode<HUD>("HUD");
		hud.UpdateScore(Score);
		hud.ShowMessage("Get Ready!");
		
		Score = 0;
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
		GetNode<Timer>("StartTimer").Start();
		
		GetNode<AudioStreamPlayer>("Music").Play();
	}

	public void GameOver()
	{
		GetNode<HUD>("HUD").ShowGameOver();
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		
		GetNode<AudioStreamPlayer>("SfxDie").Play();
		GetNode<AudioStreamPlayer>("Music").Stop();
	}
	
	public void OnMobTimerTimeout()
	{
		// Choose a random location on Path2D
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.Offset = GD.Randi();
		
		// Create an instance of Mob, add it to the scene
		// Cast the instance as a Mob (as opposed to a PackedScene)
		var mob = (Mob)MobScene.Instance();
		AddChild(mob);
		
		// Set the mob's direction perpendicular to the path direction
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		
		// Set the mob's position to a random location
		mob.Position = mobSpawnLocation.Position;
		
		// Add some randomness to the direction
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;
		
		// Choose the velocity
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);
	}


	public void OnScoreTimerTimeout()
	{
		Score++;
		GetNode<HUD>("HUD").UpdateScore(Score);
	}


	public void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
		
}
