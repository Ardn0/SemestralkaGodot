using Godot;
using System;

public class HracDva : KinematicBody2D
{
	public static int rychlost;
	private bool zacalJsmeZatacet;
	private bool jsemLeva = true;
	private bool jsemPrava = false;
	private bool jsemHore;
	private bool jsemDole;
	private bool rovina = true;
	private int cisloSchopnosti = 0;
	private double x = 0;
	private double y = 0;
	private Vector2 smerPohybu;
	private double rotace = 0;
	private Timer casovac;
	private Control menuVeHre;
	private KinematicBody2D ai;
	private KinematicBody2D hrac;
	private CollisionShape2D kolizeMic;
	private CollisionShape2D kolizeMicArea;
	private CollisionShape2D kolizeHrace;
	private Sprite texturaPlosina;
	private Sprite texturaMic;
	private RigidBody2D micNode;

	public override void _Ready()
	{
		rychlost = 200;
		kolizeHrace = GetNode<CollisionShape2D>("Kolize");
		kolizeMic = GetParent().GetNode("Mic").GetNode<CollisionShape2D>("Kolize");
		kolizeMicArea = GetParent().GetNode("Mic").GetNode("Area").GetNode<CollisionShape2D>("KolizeArea");
		texturaMic = GetParent().GetNode("Mic").GetNode<Sprite>("Textura");
		micNode = GetParent().GetNode<RigidBody2D>("Mic");
		menuVeHre = GetParent().GetNode<Control>("MenuVeHre");
		ai = GetParent().GetNode<KinematicBody2D>("AI");
		hrac = GetParent().GetNode<KinematicBody2D>("HracDva");
		texturaPlosina = GetNode<Sprite>("Textura");
		casovac = GetNode<Timer>("Casovac");
		casovac.Start();
	}

	private void UtokSchopnostZryhleniMice()
	{
		if (AI.mapaCislo > 1)
		{
			casovac.Paused = false;
			casovac.Stop();
			if (Mic.smerX < 0)
			{
				Mic.NastavVlastnostiMice(-350, 0);
			}

			if (Mic.smerX > 0)
			{
				Mic.NastavVlastnostiMice(350, 0);
			}

			cisloSchopnosti = 1;
			casovac.WaitTime = 5;
			casovac.Start();
		}
	}

	private void UtokSchopnostZpomalHrace()
	{
		casovac.Paused = false;
		casovac.Stop();
		Hrac.rychlost = 100;
		Modulate = Colors.Fuchsia;
		cisloSchopnosti = 2;
		casovac.WaitTime = 2;
		casovac.Start();
	}

	private void UtokSchopnostProhozeni()
	{
		if (micNode.Position.x > 100 && micNode.Position.x < 1400 && AI.mapaCislo > 2)
		{
			Vector2 puvodniPozice = Position;
			Position = hrac.Position;
			hrac.Position = puvodniPozice;
			Mic.NarazAI = false;
		}
	}

	private void ObranaSchopnostZvetsitPlosinu()
	{
		if (Position.y > 170 && Position.y < 715 && AI.mapaCislo > 1)
		{
			casovac.Paused = false;
			casovac.Stop();
			kolizeHrace.Scale = new Vector2(1, 2);
			texturaPlosina.Scale = new Vector2(1, (float) 9.2);
			Modulate = Colors.Coral;
			cisloSchopnosti = 3;
			casovac.WaitTime = 2;
			casovac.Start();
		}
	}

	private void ObranaShopnostZvetsitMic()
	{
		casovac.Paused = false;
		casovac.Stop();
		kolizeMic.Scale = new Vector2(2, 2);
		kolizeMicArea.Scale = new Vector2(2, 2);
		texturaMic.Scale = new Vector2((float) 1.35, (float) 1.35);
		Modulate = Colors.Aqua;
		cisloSchopnosti = 4;
		casovac.WaitTime = 2;
		casovac.Start();
	}

	private void PohybHrace()
	{
		smerPohybu = new Vector2();

			if (Input.IsActionPressed("sipkaHore"))
			{
				smerPohybu.y -= 1;
			}

			if (Input.IsActionPressed("sipkaDole"))
			{
				smerPohybu.y += 1;
			}

			smerPohybu = smerPohybu.Normalized() * rychlost;
	}

	private void SchopnostiHrace()
	{
		if (casovac.TimeLeft == 0.0)
		{
			if (cisloSchopnosti == 1)
			{
				if (Mic.smerX < 0)
				{
					Mic.NastavVlastnostiMice(-150, 0);
				}

				if (Mic.smerX > 0)
				{
					Mic.NastavVlastnostiMice(150, 0);
				}
			}

			if (cisloSchopnosti == 2)
			{
				Hrac.rychlost = 250;
				Modulate = Colors.Black;
			}

			if (cisloSchopnosti == 3)
			{
				kolizeHrace.Scale = new Vector2(1, 1);
				texturaPlosina.Scale = new Vector2(1, (float) 4.6);
				Modulate = Colors.Black;
			}

			if (cisloSchopnosti == 4)
			{
				kolizeMic.Scale = new Vector2(1, 1);
				kolizeMicArea.Scale = new Vector2(1, 1);
				texturaMic.Scale = new Vector2((float) 0.875, (float) 0.875);
				Modulate = Colors.Black;
			}

			casovac.WaitTime = 50;
			casovac.Start();
			casovac.Paused = true;
		}

		if (casovac.Paused == true)
		{
			if (Input.IsActionJustPressed("sipkaLeva"))
			{
				UtokSchopnostZryhleniMice();
			}

			if (Input.IsActionJustPressed("sipkaPrava"))
			{
				UtokSchopnostZpomalHrace();
			}

			if (Input.IsActionJustPressed("j"))
			{
				ObranaSchopnostZvetsitPlosinu();
			}

			if (Input.IsActionJustPressed("k"))
			{
				ObranaShopnostZvetsitMic();
			}

			if (Input.IsActionJustPressed("l"))
			{
				UtokSchopnostProhozeni();
			}

			if (Input.IsActionJustPressed("p"))
			{
				GetTree().Paused = true;
				menuVeHre.Visible = true;
			}
		}
	}

	public override void _Process(float delta)
	{
		PohybHrace();
		MoveAndSlide(smerPohybu);
		SchopnostiHrace();
	}
}
