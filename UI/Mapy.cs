using Godot;
using System;

public class Mapy : Control
{
	private Button mapa2;
	private Button mapa3;
	
	public override void _Ready()
	{
		mapa2 = GetNode("MapyVyber").GetNode<Button>("Mapa2");
		mapa3 = GetNode("MapyVyber").GetNode<Button>("Mapa3");

		if (Mapa1.skore < 10 & Mapa1.vyhral == true)
		{
			mapa2.Disabled = false;
		}
		else
		{
			Mapa1.skore = 0;
		}

		if (Mapa2.skore < 20 & Mapa2.vyhral == true)
		{
			mapa3.Disabled = false;
		}
		else
		{
			Mapa2.skore = 0;
		}
	}

	public void _on_Mapa1_pressed()
	{
		GetTree().ChangeScene("res://Mapy/Mapa1.tscn");
	}

	public void _on_Mapa2_pressed()
	{
		GetTree().ChangeScene("res://Mapy/Mapa2.tscn");
	}

	public void _on_Mapa3_pressed()
	{
		GetTree().ChangeScene("res://Mapy/Mapa3.tscn");
	}
}
