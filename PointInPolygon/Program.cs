using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Drawing;

namespace PointInPolygon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PointF[] points = WorkFiles.GetListOfPoints("points.txt");
            PointF P = WorkFiles.GetPoint(@"C:\Labs\points.txt");
            List<Vector> vectors = Vector.GetVectors(points);
            //PointF F = new(193.786f, -158.445f);
            PointF F = Vector.GeneratePointF(points);
            Console.WriteLine($"Точка P {P}, точка F {F}");
            Vector vector = new(P, F);
            bool sign = false;
            Vector.Run(vectors, vector, ref sign);
            PointF F1 = vector.P2;

            NativeWindowSettings settings = new()
            {
                Size = new(500, 500),
                WindowBorder = WindowBorder.Fixed,
                Title = "Draw Polygon",
                Profile = ContextProfile.Compatability
            };
            OpenGL Gl;

            if (!sign)
            {
                Gl = new(GameWindowSettings.Default, settings, points, P, F, sign);
            }
            else
            {
                Gl = new(GameWindowSettings.Default, settings, points, P, F, F1, sign);
            }

            Gl.Run();
        }
    }
}