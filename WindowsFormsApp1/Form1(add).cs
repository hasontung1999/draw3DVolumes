using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph.Assets;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //gl.LoadIdentity();
            string a = "Crate.bmp";
            texture.Create(gl, a);
        }
        Texture texture = new Texture();
        Camera camera = new Camera();
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            drawColouredCube(gl);
        }

        float rtri = 0;

        private void drawColouredCube(OpenGL gl)
        {
//            SharpGL.OpenGL gl = this.openGLControl.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            //gl.Perspective(60.0f,
            //openGLControl.Width / openGLControl.Height,
            //    0.01f, 100.0f);

            //set ma tran model view
            gl.LookAt(
                2, 2, 2,
                0, 0, 0,
                0, 1, 0);

            gl.Rotate(rtri, 0.0f, 1.0f, 0.0f);

            //  Bind the texture.
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture.Bind(gl);
            gl.LineWidth(5.0f);
            gl.Color(1.0f, 1.0f, 1.0f);
            gl.Begin(OpenGL.GL_QUADS);

            // Front Face
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(-0.5f, -0.5f, 0.5f); // Bottom Left Of The Texture and Quad
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(0.5f, -0.50f, 0.5f);  // Bottom Right Of The Texture and Quad
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(0.5f, 0.5f, 0.5f);   // Top Right Of The Texture and Quad
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(-0.5f, 0.5f, 0.5f);  // Top Left Of The Texture and Quad

            // Back Face
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);    // Bottom Right Of The Texture and Quad
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f); // Top Right Of The Texture and Quad
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);  // Top Left Of The Texture and Quad
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, -1.0f); // Bottom Left Of The Texture and Quad

            // Top Face
            //gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f); // Top Left Of The Texture and Quad
            //gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);  // Bottom Left Of The Texture and Quad
            //gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, 1.0f, 1.0f);   // Bottom Right Of The Texture and Quad
            //gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);  // Top Right Of The Texture and Quad

            // Bottom Face
            //gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);    // Top Right Of The Texture and Quad
            //gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, -1.0f, -1.0f); // Top Left Of The Texture and Quad
            //gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);  // Bottom Left Of The Texture and Quad
            //gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f); // Bottom Right Of The Texture and Quad

            // Right face
            //gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, -1.0f, -1.0f); // Bottom Right Of The Texture and Quad
            //gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);  // Top Right Of The Texture and Quad
            //gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, 1.0f, 1.0f);   // Top Left Of The Texture and Quad
            //gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);  // Bottom Left Of The Texture and Quad

            // Left Face
            //gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);    // Bottom Left Of The Texture and Quad
            //gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f); // Bottom Right Of The Texture and Quad
            //gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);  // Top Right Of The Texture and Quad
            //gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f);	// Top Left Of The Texture and Quad

            gl.TexCoord(0.0f, 0.0f); gl.Vertex(-0.5f, -0.5f, -0.5f);    // Bottom Left Of The Texture and Quad
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(-0.5f, -0.5f, 0.5f); // Bottom Right Of The Texture and Quad
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(-0.5f, 0.5f, 0.5f);  // Top Right Of The Texture and Quad
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(-0.5f, 0.5f, -0.5f);	// Top Left Of The Texture and Quad
            gl.End();
            //texture.Bind(0);
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f,0.0f,0.0f);
            gl.Vertex(1.0f, 0.0f, 0.0f);
            gl.Color(0.0f, 1f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 1.0f);
            
            gl.End();
            gl.LineWidth(1.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1.0f, 1.0f, 1.0f);
            for (int i = -10; i < 10; i++)
            {
                for (int j = -10; j < 10; j++)
                {
                    if (i != 0 && j != 0)
                    {
                        gl.Vertex(i * 0.1f,0.0f, j * 0.1f);
                        gl.Vertex((i + 1) * 0.1f,0.0f, j * 0.1f);
                        gl.Vertex(i * 0.1f, 0.0f,j * 0.1f);
                        gl.Vertex(i * 0.1f,0.0f, (j + 1) * 0.1f);
                    }
                    else if (i==0 && j != 0)
                    {
                        gl.Vertex(i * 0.1f, 0.0f,j * 0.1f);
                        gl.Vertex((i + 1) * 0.1f,0.0f, j * 0.1f);
                    }
                    else if (i != 0 && j == 0)
                    {
                        gl.Vertex(i * 0.1f,0.0f, j * 0.1f);
                        gl.Vertex(i * 0.1f, 0.0f,(j + 1) * 0.1f);
                    }
                }
            }
            gl.End();
            gl.LineWidth(2.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(-1.0f, 0.0f, -1.0f);
            gl.Vertex(-1.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, 0.0f, -1.0f);
            gl.Vertex(1.0f, 0.0f, -1.0f);
            gl.Vertex(-1.0f, 0.0f, -1.0f);

            gl.Vertex(-1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, -1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.End();
            gl.Flush();

            rtri += 1.0f;
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.ClearColor(0, 0, 0, 0);
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            //set ma tran viewport
            gl.Viewport(
                0, 0,
                openGLControl.Width,
                openGLControl.Height);

            //set ma tran phep chieu
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(60.0f,
            openGLControl.Width / openGLControl.Height,
                0.01f, 100.0f);

            //set ma tran model view
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LookAt(
                3.0f, 3.0f, 3.0f,
                0, 0, 0,
                0, 1, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  Show a file open dialog.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //  Destroy the existing texture.
                texture.Destroy(openGLControl.OpenGL);

                //  Create a new texture.
                texture.Create(openGLControl.OpenGL, openFileDialog1.FileName);

                //  Redraw.
                openGLControl.Invalidate();
            }
        }

        //xử lí nhận lệnh từ bàn phím
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
                camera.moveLeft();
            if (keyData == Keys.Right)
                camera.moveRight();
            if (keyData == Keys.Up)
                camera.moveUp();
            if (keyData == Keys.Down)
                camera.moveDown();
            if (keyData == Keys.Z)
                camera.zoomIn();
            if (keyData == Keys.X)
                camera.zoomOut();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}