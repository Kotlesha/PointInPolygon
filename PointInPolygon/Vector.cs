﻿using System.Drawing;

namespace PointInPolygon
{
    internal class Vector
    {
        private float point_1;
        private float point_2;
        private const float epsilon = 0.01f;
        private const double angle = 60.0;

        private float LengthOfVector => (float)Math.Sqrt(Math.Pow(point_1, 2) + Math.Pow(point_2, 2));

        public PointF P1 { get; set; }

        public PointF P2 { get; set; }

        public Vector(PointF point_1, PointF point_2)
        {
            P1 = point_1;
            P2 = point_2;
            this.point_1 = point_2.X - point_1.X;
            this.point_2 = point_2.Y - point_1.Y;

            if (this.point_1 == 0 && this.point_2 == 0)
            {
                throw new ArgumentException("Отрезки неопределены!");
            }
        }

        public Vector(float x, float y)
        {
            point_1 = x;
            point_2 = y;
        }

        public static List<Vector> GetVectors(PointF[] points)
        {
            List<Vector> result = new();

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector vector = new(points[i], points[i + 1]);
                result.Add(vector);
            }

            return result;
        }

        private static Vector NormaliseVector(Vector vector) => new(vector.point_1 / vector.LengthOfVector, vector.point_2 / vector.LengthOfVector);

        private static bool Intersection(Vector vector_1, Vector vector_2, ref bool flag)
        {
            Vector v1 = NormaliseVector(vector_1), v2 = NormaliseVector(vector_2);

            if (Math.Abs(vector_1.P1.X - vector_2.P1.X) < epsilon && Math.Abs(vector_1.P1.Y - vector_2.P1.Y) < epsilon
                && Math.Abs(vector_1.P2.X - vector_2.P2.X) < epsilon && Math.Abs(vector_1.P2.Y - vector_2.P2.Y) < epsilon)
            {
                Console.WriteLine("Отрезки совпадают!");
                return false;
            }

            if (Math.Abs(v1.point_1 - v2.point_1) < epsilon && Math.Abs(v1.point_2 - v2.point_2) < epsilon)
            {
                Console.WriteLine("Отрезки параллельны!");
                return false;
            }

            float t2 = (-vector_1.point_2 * vector_2.P1.X + vector_1.point_2 * vector_1.P1.X + vector_1.point_1 * vector_2.P1.Y - vector_1.point_1 * vector_1.P1.Y) 
                / (vector_1.point_2 * vector_2.point_1 - vector_1.point_1 * vector_2.point_2);
            float t = (vector_2.P1.X - vector_1.P1.X + vector_2.point_1 * t2) / vector_1.point_1;

            if (t < 0 || t > 1 || t2 < 0 || t2 > 1)
            {
                Console.WriteLine("Отрезки не пересекаются!");
                return false;
            }

            float x = vector_2.P1.X + vector_2.point_1 * t2, y = vector_2.P1.Y + vector_2.point_2 * t2;
            Console.WriteLine($"Точка пересечения: x = {x}, y = {y}");

            if (IntersectionVertex(vector_2, x, y))
            {
                flag = false;
            }
            
            return true;
        }

        private static bool IntersectionVertex(Vector vector_1, float x, float y)
        {
            return (x >= vector_1.P1.X - epsilon && x <= vector_1.P1.X + epsilon &&
                y >= vector_1.P1.Y - epsilon && y <= vector_1.P1.Y + epsilon) ||
                (x >= vector_1.P2.X - epsilon && x <= vector_1.P2.X + epsilon &&
                y >= vector_1.P2.Y - epsilon && y <= vector_1.P2.Y + epsilon);
        }

        public void Rotate()
        {
            double angle_radians = Math.PI * angle / 180;
            float x = (float)(P1.X + point_1 * Math.Cos(angle_radians) - point_2 * Math.Sin(angle_radians));
            float y = (float)(P1.Y + point_1 * Math.Sin(angle_radians) + point_2 * Math.Cos(angle_radians));
            P2 = new(x, y);
        }

        public static void Run(List<Vector> vectors, Vector vector, bool flag = true)
        {
            int count = 0;

            foreach (Vector v in vectors)
            {
                if (!flag)
                {
                    Console.WriteLine("Отрезок проходит через вершину!");
                    vector.Rotate();
                    vector.point_1 = vector.P2.X - vector.P1.X;
                    vector.point_2 = vector.P2.Y - vector.P1.Y;
                    Console.WriteLine($"Происходит поворот отрезка на {angle} градусов");
                    Console.WriteLine($"Новые координаты точки F: x = {vector.P2.X}, y = {vector.P2.Y}");
                    Run(vectors, vector);
                    return;
                }

                if (Intersection(v, vector, ref flag))
                {
                    count++;
                }
            }

            Console.WriteLine($"Количество пересечений: {count}");

            if (count % 2 != 0)
            {
                Console.WriteLine($"Точка x = {vector.P1.X}, y = {vector.P1.Y} расположена внутри данного многоульника");
            }
            else
            {
                Console.WriteLine($"Точка x = {vector.P1.X}, y = {vector.P2.Y} расположена за пределами данного многоульника");
            }
        }
    }
}
