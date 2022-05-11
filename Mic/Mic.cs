using Godot;
using System;

public class Mic : RigidBody2D
{
	public static int smerX = 0;
	public static int smerY = 0;
	public static bool NarazAI { get; set; }
	private Timer casovac;
	private Timer casovacAI;
	int pomocnaCasovac = 1;

	public override void _Ready()
	{
		smerX = -150;
		smerY = 0;
		AppliedForce = new Vector2(smerX, smerY);
		casovac = GetParent().GetNode("Hrac").GetNode<Timer>("Casovac");
		casovacAI = GetParent().GetNode("AI").GetNode<Timer>("Casovac");
		casovacAI.Paused = true;
		casovac.Paused = true;
		NarazAI = true;
	}

	public static void NastavVlastnostiMice(int x, int y)
	{
		smerX = x;
		smerY = y;
	}

	public void _on_Area2D_body_entered(RigidBody2D body)
	{
		if (body.Name == "Hrac")
		{
			smerX *= -1;
			NarazAI = false;
			if (AI.mapaCislo == 1)
			{
				Mapa1.skore++;
			}
			else if (AI.mapaCislo == 2)
			{
				Mapa2.skore++;
			}
			else
			{
				Mapa3.skore++;
			}
		}

		if (body.Name == "AI" | body.Name == "HracDva")
		{
			smerX *= -1;
			NarazAI = true;
			if (AI.mapaCislo == 1)
			{
				Mapa1.skore++;
			}
			else if (AI.mapaCislo == 2)
			{
				Mapa2.skore++;
			}
			else
			{
				Mapa3.skore++;
			}
		}

		if (body.Name == "Prekazka" | body.Name == "Prekazka2" | body.Name == "Prekazka3")
		{
			smerX *= -1;
		}

		AppliedForce = new Vector2(smerX, smerY);
	}
}
