using Godot;
using System;

public class Mapa1 : Node2D
{
	private PopupDialog gamerOver;
	private Label gamerOverLabel;
	public static int skore = 0;
	public static bool vyhral = false;
	private Label skoreLabel;

	public override void _Ready()
	{
		gamerOver = GetNode("Prohral").GetNode<PopupDialog>("Game_Over");
		gamerOverLabel = GetNode("Prohral").GetNode("Game_Over").GetNode<Label>("Text");
		skoreLabel = GetNode("SkorePozice").GetNode<Label>("Skore");
		AI.mapaCislo = 1;
	}

	public void _on_KonecAI_body_entered(RigidBody2D body)
	{
		gamerOverLabel.Text = "Prohral jsi skore " + skore;
		vyhral = false;
		GetTree().Paused = true;
		gamerOver.Show();
	}

	public void _on_KonecHrac_body_entered(RigidBody2D body)
	{
		gamerOverLabel.Text = "Vyhral jsi skore " + skore;
		vyhral = true;
		GetTree().Paused = true;
		gamerOver.Show();
	}

	public override void _Process(float delta)
	{
		skoreLabel.Text = skore.ToString();
	}
}
