
using System.Windows.Markup;
using Godot;
public class Minisnake
{
    public Minisnake() { }

    private Vector2 _curr_position;
    public Vector2 curr_position
    {
        get { return _curr_position; }
        set
        {
            prev_position = _curr_position;
            _curr_position = value;
        }
    }

    public Vector2 prev_position;
    public Vector2 size;
    public Color color;

    public Rect2 GetRectangle()
    {
        return new Rect2(curr_position, size);
    }

}