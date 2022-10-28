using System.Drawing;

namespace PointInPolygon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bitmap image = new(500, 500);
            Graphics graphics = Graphics.FromImage(image);
            PointF[] points = WorkFiles.GetListOfPoints("points.txt");
            PointF P = WorkFiles.GetPoint(@"C:\Labs\points.txt");
            List<Vector> vectors = Vector.GetVectors(points);
            ConsoleColor[] ConsoleColors = Colorise.GetConsoleColors(vectors);
            Color[] colors = Colorise.GetColors(ConsoleColors);
            graphics.TranslateTransform(image.Width / 2.0f, image.Height / 2.0f);

            for (int i = 0; i < points.Length - 1; i++)
            {
                graphics.DrawLine(new(colors[i]), points[i], points[i + 1]);
            }

            PointF F = Vector.GeneratePointF(points, image);
            //PointF F = new(193.786f, -158.445f);
            Console.WriteLine($"Точка P {P}, точка F {F}");
            graphics.DrawLine(new(Color.Yellow), P, F);
            Vector vector = new(P, F);
            Vector.Run(vectors, vector, ConsoleColors, graphics);
            image.Save(@"C:\Labs\polygon.png");
            Console.ResetColor();
        }
    }
}