namespace Gip_API.Interfaces;

//enum to store the colors of the products it can also be a string but this is more efficient
public enum Color
{
    Rood,
    Groen,
    Blauw
}

//converter to change string to color
//and no it's not easyier to just use a string :D
public abstract class ConvertColor
{
    public static Color ConvertStringToColor(string color)
    {
        return (Color)Enum.Parse(typeof(Color), color);
    }
}