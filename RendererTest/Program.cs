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

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    model = loader.LoadModel("cube.obj");
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
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

                    model.Render();

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
