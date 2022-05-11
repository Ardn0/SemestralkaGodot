using Godot;
using System;

public class Prohral : Control
{
	public void _on_Pokracovat_pressed()
	{
		GetTree().ChangeScene("res://UI/Mapy.tscn");
		GetTree().Paused = false;
	}

	public void _on_Reset_pressed()
	{
		GetTree().ReloadCurrentScene();
		GetTree().Paused = false;
		Mapa1.skore = 0;
		Mapa2.skore = 0;
	}
}
