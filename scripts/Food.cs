using Godot;

public class Food
{
    public Vector2 position;
    public Vector2 size = Grid.CellSize;
    public Color color = Colors.HotPink;

    public Rect2 GetRectangle()
    {
        return new Rect2(position, size);
    }
}