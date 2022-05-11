using Godot;
using System;
using System.Reflection;

public class AI : KinematicBody2D
{
    private int cisloSchopnosti;
    private Vector2 smerPohybu;
    public static int rychlost;
    private CollisionShape2D kolizeAI;
    private CollisionShape2D kolizeMic;
    private CollisionShape2D kolizeMicArea;
    private Sprite texturaPlosina;
    private Sprite texturaMic;
    private KinematicBody2D hrac;
    private Timer casovac;
    private double rotace = 0;
    private double x = 0;
    private double y = 0;
    private bool rovina = true;
    private RigidBody2D micNode;
    public static int mapaCislo;


    public override void _Ready()
    {
        rychlost = 250;
        micNode = GetParent().GetNode<RigidBody2D>("Mic");
        kolizeMic = GetParent().GetNode("Mic").GetNode<CollisionShape2D>("Kolize");
        texturaMic = GetParent().GetNode("Mic").GetNode<Sprite>("Textura");
        kolizeMicArea = GetParent().GetNode("Mic").GetNode("Area").GetNode<CollisionShape2D>("KolizeArea");
        kolizeAI = GetNode<CollisionShape2D>("Kolize");
        hrac = GetParent().GetNode<KinematicBody2D>("Hrac");
        texturaPlosina = GetNode<Sprite>("Textura");
        casovac = GetNode<Timer>("Casovac");
        casovac.Start();
    }

    public void PohybAI()
    {
        smerPohybu = new Vector2();

        if (Position.y + 45 > micNode.Position.y)
        {
            smerPohybu.y -= 1;
        }

        if (Position.y - 45 < micNode.Position.y)
        {
            smerPohybu.y += 1;
        }

        smerPohybu = smerPohybu.Normalized() * rychlost;
    }
    
    private void UtokSchopnostZpomalHrace()
    {
        GD.Print("Ano mic je daleko od hrace. Muzu ho zpomalit");
        casovac.Paused = false;
        casovac.Stop();
        Hrac.rychlost = 100;
        Modulate = Colors.Fuchsia;
        cisloSchopnosti = 2;
        casovac.WaitTime = 2;
        casovac.Start();
    }

    private void UtokSchopnostZryhleniMice()
    {
        if (mapaCislo > 1)
        {
            GD.Print("Ano hrac je daleko. Muzu utocit zrychleni mice");

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

    private void UtokSchopnostProhozeni()
    {
        if (micNode.Position.x > 100 && micNode.Position.x < 1400 && mapaCislo > 2)
        {
            Vector2 puvodniPozice = Position;
            Position = hrac.Position;
            hrac.Position = puvodniPozice;
            Mic.NarazAI = false;
        }
    }

    private void ObranaSchopnostZvetsitPlosinu()
    {
        if (Position.y > 170 && Position.y < 715 && mapaCislo > 1)
        {
            GD.Print("Nestiham, potrebuju se branit");

            casovac.Paused = false;
            casovac.Stop();
            kolizeAI.Scale = new Vector2(1, 2);
            texturaPlosina.Scale = new Vector2(1, (float) 9.2);
            Modulate = Colors.Coral;
            cisloSchopnosti = 3;
            casovac.WaitTime = 2;
            casovac.Start();
        }
    }

    private void ObranaShopnostZvetsitMic()
    {
        GD.Print("Nestiham, potrebuju se branit");
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

    private void SchopnostiAI()
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
                kolizeAI.Scale = new Vector2(1, 1);
                texturaPlosina.Scale = new Vector2(1, (float) 4.6);
                Modulate = Colors.Black;
            }

            if (cisloSchopnosti == 4)
            {
                kolizeMic.Scale = new Vector2(1, 1);
                kolizeMicArea.Scale = new Vector2(1, 1);
                Modulate = Colors.Black;
            }

            casovac.WaitTime = 50;
            casovac.Start();
            casovac.Paused = true;
        }

        if (casovac.Paused == true)
        {
            if ((hrac.Position.y > Position.y + 150 | hrac.Position.y < Position.y - 150) && Mic.NarazAI == false &&
                micNode.Position.x > 1000)
            {
                UtokSchopnostZryhleniMice();
            }

            if (Position.y + 80 < micNode.Position.y && Mic.NarazAI == false && micNode.Position.x > 1300)
            {
                ObranaSchopnostZvetsitPlosinu();
            }

            if (Position.y - 80 > micNode.Position.y && Mic.NarazAI == false && micNode.Position.x > 1300)
            {
                ObranaShopnostZvetsitMic();
            }

            if (Position.y + 50 < micNode.Position.y && Position.y + 80 > micNode.Position.y && Mic.NarazAI == false
                && micNode.Position.x > 1300)
            {
                ObranaShopnostZvetsitMic();
            }

            if (Position.y - 50 > micNode.Position.y && Position.y - 80 > micNode.Position.y && Mic.NarazAI == false
                && micNode.Position.x > 1300)
            {
                ObranaSchopnostZvetsitPlosinu();
            }

            if ((hrac.Position.y > micNode.Position.y + 100 | hrac.Position.y < micNode.Position.y - 100) &&
                Mic.NarazAI == true && micNode.Position.x < 800)
            {
                UtokSchopnostZpomalHrace();
            }

            if (Mapa3.skore % 10 == 0)
            {
                UtokSchopnostProhozeni();
            }
        }
    }

    public override void _Process(float delta)
    {
        if (Mic.NarazAI == false & mapaCislo < 3)
        {
            PohybAI();
            MoveAndSlide(smerPohybu);
        }
        else
        {
            PohybAI();
            MoveAndSlide(smerPohybu);
        }

        SchopnostiAI();
    }
}