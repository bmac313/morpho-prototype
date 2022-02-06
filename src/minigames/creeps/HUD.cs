using Godot;
using System;

public class HUD : CanvasLayer
{
	// Member variables
	[Signal]
	public delegate void StartGame();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();
		GetNode<Timer>("MsgTimer").Start();
	}
	
	async public void ShowGameOver()
	{
		ShowMessage("Game Over");
		
		var msgTimer = GetNode<Timer>("MsgTimer");
		await ToSignal(msgTimer, "timeout");
		
		var message = GetNode<Label>("Message");
		message.Text = "Dodge the\nCreeps!!";
		message.Show();
		
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		GetNode<Button>("StartButton").Show();
	}
	
	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
	private void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal("StartGame");
	}

	private void OnMsgTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}
}
