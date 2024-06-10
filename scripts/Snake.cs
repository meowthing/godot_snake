using System;
using System.Net.Http;
using Godot;

public partial class Snake : Node2D
{
	public Minisnake head = new Minisnake();

	public override void _Ready()
	{
		head.size = Grid.cellSize;
		head.color = Colors.Red;
	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public override void _Draw()
	{
		DrawRect(head.GetRectangle(), head.color);
	}
}
