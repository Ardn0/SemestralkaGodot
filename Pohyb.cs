using Godot;
using System;

public class Pohyb : KinematicBody2D
{
    Random rnd = new Random();
    Vector2 smerAi = new Vector2();
    Vector2 novySmerAi;
    Vector2 naraz = new Vector2(336, 224);
    RayCast2D ray;

    Area2D pokus;

    int randomCisloX;
    int randomCisloY;
    int randomRotace;
    int speed = 200;

    bool jeBlizko = false;

    public override void _Ready()
    {
        randomRotace = rnd.Next(-360, 360);
        novySmerAi = new Vector2(speed, 0).Rotated(randomRotace);
        smerAi = Position.DirectionTo(novySmerAi) * speed;

        ray = GetNode<RayCast2D>("Ray");

    }

    public void _on_Area_area_entered(Area2D node)
    {
        jeBlizko = true;
        pokus = node;
    }
    public void _on_Area_area_exited(Area2D node)
    {
        jeBlizko = false;
        ray.SetCastTo(new Vector2(0, 0));
    }

    public override void _Process(float delta)
    {
        smerAi = MoveAndSlide(smerAi);

        if (Position.x > 1000 | Position.x - 5 < 15)
        {
            randomRotace = rnd.Next(-360, 360);
            novySmerAi = new Vector2(speed, 0).Rotated(randomRotace);
            smerAi = Position.DirectionTo(novySmerAi) * speed;
        }
        if (Position.y > 550 | Position.y - 5 < 15)
        {
            randomRotace = rnd.Next(-360, 360);
            novySmerAi = new Vector2(speed, 0).Rotated(randomRotace);
            smerAi = Position.DirectionTo(novySmerAi) * speed;
        }

        if (jeBlizko == true)
        {
            //cast na [0,0] dvojnasobek[x,y] x -1
            naraz = new Vector2(((Position.x * 2)) - pokus.GetGlobalPosition().x * 2, ((Position.y * 2)) - pokus.GetGlobalPosition().y * 2);
            ray.SetCastTo(naraz * (-1));
            //GD.Print(ray.GetCastTo().x + " ray cast");
            if ((ray.GetCastTo().x < 20 && ray.GetCastTo().x > 0) | (ray.GetCastTo().x > -20 && ray.GetCastTo().x < 0))
            {
                SetGlobalPosition(new Vector2(0,0));




            }
        }
        //GD.Print(naraz + " naraz");a

    }
}
