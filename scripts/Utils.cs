using Godot;

public class Utils
{
    public static float Repeat(float value, float length)
    {
        return Mathf.PosMod(value, length);
    }
}
