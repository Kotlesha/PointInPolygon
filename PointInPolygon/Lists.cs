using System.Drawing;

namespace PointInPolygon
{
    internal static class Lists
    {
        private static List<int> GetList(float bMinY)
        {
            List<int> list = new();

            for (int i = 0; i < Convert.ToInt32(bMinY); i++)
            {
                list.Add(i);
            }

            return list;
        }

        private static List<int> GetList(float bMaxY, int height)
        {
            List<int> list = new();

            for (int i = Convert.ToInt32(bMaxY) + 1; i < height; i++)
            {
                list.Add(i);
            }

            return list;
        }

        private static int GetElement(float bMinY, float bMaxY, int height)
        {
            Random random = new();
            List<int> list_1 = GetList(bMinY);
            List<int> list_2 = GetList(bMaxY, height);
            list_1.AddRange(list_2);
            return list_1[random.Next(0, list_1.Count())];
        }

        public static void GetMaxMinValue(PointF[] points, ref float bMinY, ref float bMaxY, ref float bMinX, ref float bMaxX)
        {
            List<float> pointsX = new(), pointsY = new();

            foreach (var point in points)
            {
                pointsX.Add(point.X);
                pointsY.Add(point.Y);
            }

            bMinY = pointsY.Min();
            bMaxY = pointsY.Max();
            bMinX = pointsX.Min();
            bMaxX = pointsX.Max();
        }

        public static float GetCoordinateY(PointF[] points, Bitmap image)
        {
            float bMinY = 0.0f, bMaxY = 0.0f, bMinX = 0.0f, bMaxX = 0.0f;
            GetMaxMinValue(points, ref bMinY, ref bMaxY, ref bMinX, ref bMaxX);
            return GetElement(bMinY, bMaxY, image.Height);
        }
    }
}
