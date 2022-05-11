using Godot;
using System;
using Semestralka.Save;

public class Mapa3 : Node2D
{
	private PopupDialog gamerOver;
	private Label gamerOverLabel;
	public static int skore = 0;
	private int maxSkore = 0;
	public static bool vyhral = false;
	private Label skoreLabel;
	private Save saveLoad;

	public override void _Ready()
	{
		saveLoad = new Save();
		gamerOver = GetNode("Prohral").GetNode<PopupDialog>("Game_Over");
		gamerOverLabel = GetNode("Prohral").GetNode("Game_Over").GetNode<Label>("Text");
		skoreLabel = GetNode("SkorePozice").GetNode<Label>("Skore");
		maxSkore = saveLoad.LoadGame();
		AI.mapaCislo = 3;
	}

	public void _on_KonecAI_body_entered(RigidBody2D body)
	{
		int rozdil = skore - maxSkore;
		
		if (skore > maxSkore)
			gamerOverLabel.Text = "Prohral jsi. Skore je o " + rozdil + " vyssi";
		else
			gamerOverLabel.Text = "Prohral jsi. Skore je o " + rozdil*-1 + " nizsi";
		
		saveLoad.SaveGame(skoreLabel.Text);
		vyhral = false;
		GetTree().Paused = true;
		gamerOver.Show();
	}

	public void _on_KonecHrac_body_entered(RigidBody2D body)
	{
		int rozdil = skore - maxSkore;
		
		if (skore > maxSkore)
			gamerOverLabel.Text = "Prohral jsi. Skore je o" + rozdil + " vyssi";
		else
			gamerOverLabel.Text = "Prohral jsi. Skore je o" + rozdil*-1 + "nizsi";
		
		saveLoad.SaveGame(skoreLabel.Text);
		vyhral = true;
		GetTree().Paused = true;
		gamerOver.Show();
	}

	public override void _Process(float delta)
	{
		skoreLabel.Text = skore.ToString();
	}
}
