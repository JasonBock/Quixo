namespace Quixo;

[Serializable]
public record struct Point(int X, int Y)
{
   public static Point Empty => new(0, 0);
}