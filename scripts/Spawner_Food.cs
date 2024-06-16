using Godot;
//using System;

public partial class Spawner_Food : Node2D
{
	public Food food = new Food();

	private Snake snake;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		snake = (Snake)GetNode("%Snake");
		SpawnFood();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
		if (food.GetRectangle().Intersects(snake.head.GetRectangle()))
		{
			snake.Grow();
			SpawnFood();
		}
	}

	public override void _Draw()
	{
		DrawRect(food.GetRectangle(), food.color);
	}

	public void SpawnFood()
	{
		bool IsOnOccupiedPosition = true;
		while (IsOnOccupiedPosition)
		{
			Vector2 random_position = new Vector2(
				(float)GD.RandRange(0, Grid.GridSize.X - Grid.CellSize.X),
				(float)GD.RandRange(0, Grid.GridSize.Y - Grid.CellSize.Y)
				);
			food.position = random_position.Snapped(Grid.CellSize);

			foreach (Minisnake ms in snake.minisnakes)
			{
				if (food.GetRectangle().Intersects(ms.GetRectangle()))
				{
					IsOnOccupiedPosition = true;
					break;
				}
				else
				{
					IsOnOccupiedPosition = false;
				}
			}
		}

	}
}
