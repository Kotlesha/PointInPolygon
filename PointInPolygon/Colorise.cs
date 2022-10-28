using System.Drawing;

namespace PointInPolygon
{
    internal static class Colorise
    {
        public static ConsoleColor[] GetColors(List<Vector> vectors)
        {
            Random random = new();
            ConsoleColor[] consoleColors = new ConsoleColor[vectors.Count];
            int[] colors = new int[vectors.Count];

            for (int i = 0; i < colors.Length; i++)
            {
                int color = random.Next(1, 16);

                while (colors.Contains(color) && !CheckValue((ConsoleColor)color))
                {
                    color = random.Next(1, 16);
                }

                colors[i] = color;
            }

            for (int i = 0; i < consoleColors.Length; i++)
            {
                consoleColors[i] = (ConsoleColor)colors[i];
            }

            return consoleColors;
        }

        private static bool CheckValue(ConsoleColor color)
        {
            List<Color> colors = GetAllColors();

            foreach (var element in colors)
            {
                if (element.ToString().Equals(color.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private static List<Color> GetAllColors()
        {
            List<Color> colors = new();

            foreach (var colorValue in Enum.GetValues(typeof(KnownColor)))
            {
                Color color = Color.FromKnownColor((KnownColor)colorValue);
                colors.Add(color);
            }

            return colors;
        }

        public static Bitmap Paint(PointF[] points, Bitmap bitmap, ConsoleColor[] color)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            Color[] colors = new Color[color.Length];

            for (int i = 0; i < points.Length - 1; i++)
            {
                graphics.DrawLine(new(Color.FromName(Convert.ToString(color[i]))), points[i], points[i + 1]);
            }

            return bitmap;
        }
    }
}
