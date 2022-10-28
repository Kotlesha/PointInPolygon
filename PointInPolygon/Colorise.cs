using System.Drawing;

namespace PointInPolygon
{
    internal static class Colorise
    {
        public static ConsoleColor[] GetConsoleColors(List<Vector> vectors)
        {
            Random random = new();
            ConsoleColor[] consoleColors = new ConsoleColor[vectors.Count];
            int[] colors = new int[vectors.Count];

            for (int i = 0; i < colors.Length; i++)
            {
                int color = random.Next(1, 16);

                while (colors.Contains(color))
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

        public static Color[] GetColors(ConsoleColor[] colors)
        {
            Color[] all_colors = new Color[colors.Length];

            for (int i = 0; i < all_colors.Length; i++)
            {
                all_colors[i] = FromColor(colors[i]);
            }

            return all_colors;
        }

        private static Color FromColor(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    return Color.Black;
                case ConsoleColor.Blue:
                    return Color.Blue;
                case ConsoleColor.Cyan:
                    return Color.Cyan;
                case ConsoleColor.DarkBlue:
                    return Color.DarkBlue;
                case ConsoleColor.DarkGray:
                    return Color.DarkGray;
                case ConsoleColor.DarkGreen:
                    return Color.DarkGreen;
                case ConsoleColor.DarkMagenta:
                    return Color.DarkMagenta;
                case ConsoleColor.DarkRed:
                    return Color.DarkRed;
                case ConsoleColor.DarkYellow:
                    return Color.FromArgb(255, 128, 128, 0);
                case ConsoleColor.Gray:
                    return Color.Gray;
                case ConsoleColor.Green:
                    return Color.Green;
                case ConsoleColor.Magenta:
                    return Color.Magenta;
                case ConsoleColor.Red:
                    return Color.Red;
                case ConsoleColor.White:
                    return Color.White;
                default:
                    return Color.Yellow;
            }
        }
    }
}
