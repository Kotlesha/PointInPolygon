using System.Drawing;

namespace PointInPolygon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bitmap image = new(500, 500);
            Graphics g = Graphics.FromImage(image);
            //Vector a = new(new PointF(50.0f, 50.0f), new PointF(150.0f, 150.0f));
            //Vector b = new(new PointF(-2.4f, 0.0f), new PointF(-1.8f, 1.2f));
            //g.TranslateTransform(250.0f, 250.0f);
            //g.DrawLine(new(Color.Aqua), new PointF(50.0f, 50.0f), new PointF(150.0f, 150.0f));
            //Console.WriteLine(a);
            //a.Rotate();
            //g.DrawLine(new(Color.Aqua), a.P1, a.P2);
            //image.Save(@"C:\Labs\b.png");
            //Console.WriteLine(a);
            PointF[] points = WorkFiles.GetListOfPoints(@"C:\Labs\points.txt");
            //g.TranslateTransform(0.0f, 0.0f);
            //g.DrawPolygon(new(Color.AliceBlue), points);
            PointF P = WorkFiles.GetPoint(@"C:\Labs\points.txt");
            List<Vector> vectors = Vector.GetVectors(points);
            ConsoleColor[] colors = Colorise.GetColors(vectors);
            image = Colorise.Paint(points, image, colors);
            //for (int i = 0; i < points.Length - 1; i++)
            //{
            //    g.DrawLine(new(Color.Aquamarine), points[i], points[i + 1]);
            //}
            PointF F = new(107.484f, 59.5671f);
            //g.DrawLine(new(Color.Aqua), P, F);
            //231,56573, y = 39,825043
            //g.DrawLine(new(Color.Yellow), P, new(231.56573f, 39.825043f));
            Vector vector = new(P, F);
            //image = Colorise.Paint(points, image, colors);
            image.Save(@"C:\Labs\b.png");
            Vector.Run(vectors, vector, colors);
            Console.ResetColor();
        }
    }
}