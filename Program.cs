using System;
using System.Numerics;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Program
    {
        struct Vector {
            public float x, y, z;

            public Vector(float x, float y, float z) {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public Vector(float x, float y) {
                this.x = x;
                this.y = y;
                this.z = 0;
            }

            public static Vector operator *(Vector v, float f) {
                return new Vector(v.x * f, v.y * f, v.z * f);
            }
        
            
        }
        static Vector[] vertices = new float[]
        {
            new Vector(-.1f,-.1f)


        };




            static void Main(string[] args)
            {
                var window = CreatWindow();


                LoadTriangleIntoBuffer();


                CreatShaderProgram();


                while (!Glfw.WindowShouldClose(window))
                {
                    Glfw.PollEvents(); // react to window changes .
                    glDrawArrays(GL_TRIANGLES, 0, 3);
                    Glfw.SwapBuffers(window);

                    Glfw.SwapBuffers(window);

                }
            }

            private static unsafe void LoadTriangleIntoBuffer()
            {
                var vertexArray = glGenVertexArray();
                var vertexBuffer = glGenBuffer();
                glBindVertexArray(vertexArray);
                glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
                unsafe
                {
                    fixed (float* vertex = &vertices[0])
                    {
                        glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, vertex, GL_STATIC_DRAW);
                    }

                    glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), NULL);
                }

                glEnableVertexAttribArray(0);
            }

            private static void CreatShaderProgram()
            {
                string vertexShaderSource = @"
#version 330 core
in vec3 pos;

void main()
{
    gl_Position = vec4(pos.x, pos.y, pos.z, 1.0);
}
";
                string fragmentShaderSource = @"
#version 330 core
out vec4 result;

void main()
{
    result = vec4(0, 0, 1, 1);
}
";
                // create vertex shader
                var vertexShader = glCreateShader(GL_VERTEX_SHADER);
                glShaderSource(vertexShader, vertexShaderSource);
                glCompileShader(vertexShader);

                //create fragment shader

                var fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
                glShaderSource(fragmentShader, fragmentShaderSource);
                glCompileShader(fragmentShader);

                var program = glCreateProgram();
                glAttachShader(program, vertexShader);
                glAttachShader(program, fragmentShader);
                glLinkProgram(program);
                glUseProgram(program);
            }

            private static Window CreatWindow()
            {
                Glfw.Init();
                Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
                Glfw.WindowHint(Hint.ContextVersionMajor, 3);
                Glfw.WindowHint(Hint.ContextVersionMinor, 3);
                Glfw.WindowHint(Hint.Decorated, true);
                Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
                Glfw.WindowHint(Hint.OpenglForwardCompatible, Constants.True);
                Glfw.WindowHint(Hint.Doublebuffer, Constants.True);


                var window = Glfw.CreateWindow(1024, 768, "SharpEngine", Monitor.None, Window.None);
                Glfw.MakeContextCurrent(window);
                Import(Glfw.GetProcAddress);
                return window;
            }
        }
    }


