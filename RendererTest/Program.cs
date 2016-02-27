using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RendererTest.Elements.Models;
using RendererTest.Models.Loader;
using System;
using System.Drawing;

[assembly: log4net.Config.XmlConfigurator(Watch = false)]
namespace RendererTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ModelLoader loader = new ModelLoader();
            Model model = null;
            double angle = 0.0;

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    GL.EnableClientState(ArrayCap.VertexArray);
                    model = loader.LoadModel("planet.obj");
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    angle++;
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    Matrix4 projMatrix = Matrix4.CreatePerspectiveFieldOfView(0.5f, 1.0f, 0.1f, 100.0f);

                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadMatrix(ref projMatrix);
                    GL.MatrixMode(MatrixMode.Modelview);
                    GL.LoadIdentity();
                    GL.Translate(0.0, 0.0, -10.0);
                    GL.Rotate(angle, 1.0, 1.0, 1.0);

                    model.Render();

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
