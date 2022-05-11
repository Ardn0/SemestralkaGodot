using Godot;
using System;

public class Menu : Control
{
	public void _on_Start_pressed()
	{
		GetTree().ChangeScene("res://UI/Mapy.tscn");
	}

	public void _on_KonecHry_pressed()
	{
		GetTree().Quit(0);
	}
}
