﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RendererTest.Elements.Models;
using RendererTest.Models.Loader;
using RendererTest.Shaders.Elements;
using RendererTest.Shaders.Loader;
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
            ModelLoader mloader = new ModelLoader();
            ShaderLoader sLoader = new ShaderLoader();

            using (var game = new GameWindow())
            {
                Model model = null;
                ShaderProgram prog = null;
                Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(0.25f,1.0f,0.1f,100.0f);
                Matrix4 viewMatrix = Matrix4.LookAt(new Vector3(0.0f,5.0f,-15.0f),new Vector3(0.0f,0.0f,0.0f),new Vector3(0.0f,1.0f,0.0f));
                int vao = 0;

                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;
                    vao = GL.GenVertexArray();
                    GL.BindVertexArray(vao);

                    GL.EnableClientState(ArrayCap.VertexArray);
                    model = mloader.LoadModel("plane.obj");
                    prog = sLoader.loadShaderProgram("shader.vert", "shader.frag");
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
                    game.Title = "Render Time: "+game.RenderTime*1000.0;
                    model.Update();
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    Matrix4 VPmatrix = viewMatrix * projectionMatrix;
                    
                    model.Render(VPmatrix,prog);

                    game.SwapBuffers();
                };

                game.Closing += (sender, e) =>
                {
                    model.Dispose();
                    prog.Dispose();
                    GL.DeleteVertexArray(vao);
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
