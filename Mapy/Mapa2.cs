using Godot;
using System;

public class Mapa2 : Node2D
{
	private PopupDialog gamerOver;
	private Label gamerOverLabel;
	public static int skore = 0;
	public static bool vyhral = false;
	private Label skoreLabel;
	private CollisionShape2D kolizeMic;
	private Sprite texturaMic;
	private CollisionShape2D kolizeMicArea;

	public override void _Ready()
	{
		AI.mapaCislo = 2;
		gamerOver = GetNode("Prohral").GetNode<PopupDialog>("Game_Over");
		gamerOverLabel = GetNode("Prohral").GetNode("Game_Over").GetNode<Label>("Text");
		skoreLabel = GetNode("SkorePozice").GetNode<Label>("Skore");
		kolizeMic = GetNode("Mic").GetNode<CollisionShape2D>("Kolize");
		texturaMic = GetNode("Mic").GetNode<Sprite>("Textura");
		kolizeMicArea = GetNode("Mic").GetNode("Area").GetNode<CollisionShape2D>("KolizeArea");

		kolizeMic.Shape = new RectangleShape2D();
		kolizeMicArea.Shape = new RectangleShape2D();
		kolizeMic.Scale = new Vector2((float)1.7, (float)1.7);
		kolizeMicArea.Scale = new Vector2((float)2.6, (float)2.6);

		var texture = new ImageTexture();
		var image = new Image();
		
		image.Load("res://Textury/plosina.png");
		
		texture.CreateFromImage(image);
		texturaMic.Texture = texture;
		texturaMic.Scale = new Vector2((float) 1.3, (float) 1.3);
	}

	private void _on_Konec_body_entered(RigidBody2D body)
	{
		if (Mic.NarazAI == true)
		{
			gamerOverLabel.Text = "Prohral jsi skore " + skore;
			vyhral = false;
			GetTree().Paused = true;
			gamerOver.Show();
		}
		else
		{
			gamerOverLabel.Text = "Vyhral jsi skore " + skore;
			vyhral = true;
			GetTree().Paused = true;
			gamerOver.Show();
		}
	}

	public void _on_Konec2_body_entered(RigidBody2D body)
	{
		if (Mic.NarazAI == true)
		{
			gamerOverLabel.Text = "Prohral jsi skore " + skore;
			GetTree().Paused = true;
			gamerOver.Show();
		}
		else
		{
			gamerOverLabel.Text = "Vyhral jsi skore " + skore;
			GetTree().Paused = true;
			gamerOver.Show();
		}
	}

	public override void _Process(float delta)
	{
		skoreLabel.Text = skore.ToString();
	}
}
