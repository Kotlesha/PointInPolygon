using System.Drawing;
using System.Globalization;

namespace PointInPolygon
{
    internal static class WorkFiles
    {
        private static List<string> ReadFile(string path)
        {
            StreamReader reader = new(path);
            List<string> strings = new();

            while (!reader.EndOfStream)
            {
                string temp = reader.ReadLine().Trim();

                if (!string.IsNullOrEmpty(temp))
                {
                    strings.Add(temp);
                }
            }

            reader.Close();
            return strings;
        }

        public static PointF[] GetListOfPoints(string path)
        {
            List<string> strings = ReadFile(path);
            List<PointF> points = new();

            foreach (string line in strings)
            {
                string[] coordinates = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (coordinates.Length == 1)
                {
                    continue;
                }

                foreach (string coord in coordinates)
                {
                    string[] temp = coord.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    PointF point = new(float.Parse(temp[0], CultureInfo.InvariantCulture), float.Parse(temp[1], CultureInfo.InvariantCulture));
                    points.Add(point);
                }
            }

            return points.ToArray();
        }

        public static PointF GetPoint(string path)
        {
            List<string> strings = ReadFile(path);
            string[] temp = strings[^1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            return new(float.Parse(temp[0], CultureInfo.InvariantCulture), float.Parse(temp[1], CultureInfo.InvariantCulture));
        }
    }
}
