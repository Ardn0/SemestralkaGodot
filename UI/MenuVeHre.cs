using Godot;
using System;

public class MenuVeHre : Control
{
	public override void _Ready()
	{
		Visible = false;
	}

	public void _on_Pokracovat_pressed()
	{
		Visible = false;
		GetTree().Paused = false;
	}

	public void _on_ResetHry_pressed()
	{
		GetTree().ReloadCurrentScene();
		GetTree().Paused = false;
		Mapa1.skore = 0;
		Mapa2.skore = 0;
	}

	public void _on_Menu_pressed()
	{
		GetTree().ChangeScene("res://UI/Menu.tscn");
		GetTree().Paused = false;
		Mapa1.skore = 0;
		Mapa2.skore = 0;
	}
}
