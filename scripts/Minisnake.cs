using Godot;
public class Minisnake
{
    public Minisnake() { }

    public Vector2 curr_position;
    public Vector2 size;
    public Color color;

    public Rect2 GetRectangle()
    {
        return new Rect2(curr_position, size);
    }

}