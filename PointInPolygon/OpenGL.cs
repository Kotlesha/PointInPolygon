using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace PointInPolygon
{
    internal class OpenGL : GameWindow
    {
        private PointF[] points;
        private PointF P;
        private PointF F;
        private List<PointF> ArrayF;
        private bool sign;

        public OpenGL(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, PointF[] points, PointF P, PointF F, bool sign) : base(gameWindowSettings, nativeWindowSettings)
        {
            this.points = points;
            this.P = P;
            this.F = F;
            this.sign = sign;
        }

        public OpenGL(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, PointF[] points, PointF P, PointF F, List<PointF> ArrayF, bool sign) : this(gameWindowSettings, nativeWindowSettings, points, P, F, sign)
        {
            this.ArrayF = ArrayF;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            PointF[] new_points = Lists.GetNewCoordinate(points), new_points_line = Lists.GetNewCoordinate(P, F);

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(Color.Aqua);

            for (int i = 0; i < new_points.Length; i++)
            {
                GL.Vertex2(new_points[i].X, new_points[i].Y);
            }

            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);

            for (int i = 0; i < new_points_line.Length; i++)
            {
                GL.Vertex2(new_points_line[i].X, new_points_line[i].Y);
            }

            if (sign)
            {
                GL.Color3(Color.Yellow);
                for (int j = 0; j < ArrayF.Count; j++)
                {
                    PointF[] new_points_line1 = Lists.GetNewCoordinate(P, ArrayF[j]);
                    for (int i = 0; i < new_points_line1.Length; i++)
                    {
                        GL.Vertex2(new_points_line1[i].X, new_points_line1[i].Y);
                    }
                }
            }

            GL.End();
            SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
