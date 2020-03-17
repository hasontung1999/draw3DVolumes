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
        public class POINT3D
        {
            public float x, y, z;
            public POINT3D(float _x, float _y, float _z)
            {
                this.x = _x;
                this.y = _y;
                this.z = _z;
            }
            public POINT3D()
            {
                x = y = z = 0;
            }
        }
        public class EDGE
        {
            public POINT3D p_start, p_end;
            public EDGE()
            {
                p_start = new POINT3D();
                p_end = new POINT3D();
            }
            public EDGE(POINT3D start, POINT3D end)
            {
                this.p_start = start;
                this.p_end = end;
            }
        }
        public class POLYGON2D
        {
            //public int NumVertex;
            public List<POINT3D> aVertex;
            public POLYGON2D()
            {
                aVertex = new List<POINT3D>();
            }
            public POLYGON2D(List<POINT3D> listVertex)
            {
                this.aVertex = listVertex;
            }
        }
        public class WIREFRAME
        {
            // public int NumVerts; // số đỉnh trong khối
            // public int NumEdges; // số cạnh trong khối
            
            public List<POINT3D> Vert;
            public List<EDGE> Edge;
            public List<POLYGON2D> Polygon;
            public WIREFRAME()
            {
                Vert = new List<POINT3D>();
                Edge = new List<EDGE>();
                Polygon = new List<POLYGON2D>();
            }
            public WIREFRAME(List<POINT3D> vert, List<EDGE> edge, List<POLYGON2D> polygon)
            {
                this.Vert = vert;
                this.Edge = edge;
                this.Polygon = polygon;
            }


        }

        bool isDrawCube = false;
        bool isDrawPyramid = false;
        bool isDrawPrism = false;
        bool isTexture = false;
        string kind3D;
        WIREFRAME Cube = new WIREFRAME();
        WIREFRAME Pyramid = new WIREFRAME();
        WIREFRAME Prism = new WIREFRAME();
        Texture texture = new Texture();
        Camera camera = new Camera();
        float rtri = 0;
        List<string> nameShapes = new List<string>();

        public Form1()
        {
            InitializeComponent();
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //gl.LoadIdentity();
            string a = "Crate.bmp";
            texture.Create(gl, a);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.LookAt(camera.eyex, camera.eyey, camera.eyez, camera.centerx, camera.centery, camera.centerz, 0, 1, 0);
            /*gl.LookAt(
                2, 2, 2,
                0, 0, 0,
                0, 1, 0);*/

            //gl.Rotate(rtri, 0.0f, 1.0f, 0.0f);
            ShowGrid(gl);
            if (isDrawCube)
            {
                drawCube(gl);
            }
            else if (isDrawPyramid)
            {
                drawPyramid(gl);
            }
            else if (isDrawPrism)
            {
                drawPrism(gl);
            }
            rtri += 1.0f;
        }

        
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
        
        private void ShowGrid(OpenGL gl)
        {
            double l = 20.0f;
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.LineWidth(4.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(l, 0.0f, 0.0f);
            gl.Color(0.0f, 1f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, l, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, l);

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
                        gl.Vertex(i * 0.1f*l, 0.0f, j * 0.1f*l);
                        gl.Vertex((i + 1) * 0.1f*l, 0.0f, j * 0.1f *l);
                        gl.Vertex(i * 0.1f*l, 0.0f, j * 0.1f*l);
                        gl.Vertex(i * 0.1f*l, 0.0f, (j + 1) * 0.1f*l);
                    }
                    else if (i == 0 && j != 0)
                    {
                        gl.Vertex(i * 0.1f*l, 0.0f, j * 0.1f*l);
                        gl.Vertex((i + 1) * 0.1f*l, 0.0f, j * 0.1f*l);
                    }
                    else if (i != 0 && j == 0)
                    {
                        gl.Vertex(i * 0.1f*l, 0.0f, j * 0.1f*l);
                        gl.Vertex(i * 0.1f*l, 0.0f, (j + 1) * 0.1f*l);
                    }
                }
            }
            gl.End();
            gl.LineWidth(2.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(-l, 0.0f, -l);
            gl.Vertex(-l, 0.0f, l);
            gl.Vertex(-l, 0.0f, l);
            gl.Vertex(l, 0.0f, l);
            gl.Vertex(l, 0.0f, l);
            gl.Vertex(l, 0.0f, -l);
            gl.Vertex(l, 0.0f, -l);
            gl.Vertex(-l, 0.0f, -l);

            gl.Vertex(-l, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, -l);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.End();
            gl.Flush();
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
                5, 5, 10,
                0, 0, 0,
                0, 1, 0);
        }
        


        private void drawCube(OpenGL gl)
        {
            if (isTexture)
            {
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                texture.Bind(gl);
            }
            else {
                gl.Disable(OpenGL.GL_TEXTURE_2D);
                gl.Color(1.0f, 1.0f, 1.0f);    // Color white
            }
            gl.Begin(OpenGL.GL_QUADS);
            for (int i = 0; i < 6; i++)
            {
                gl.TexCoord(0.0f,0.0f);
                gl.Vertex(Cube.Polygon[i].aVertex[0].x, Cube.Polygon[i].aVertex[0].y, Cube.Polygon[i].aVertex[0].z);// front face
                gl.TexCoord(1.0f, 0.0f);
                gl.Vertex(Cube.Polygon[i].aVertex[1].x, Cube.Polygon[i].aVertex[1].y, Cube.Polygon[i].aVertex[1].z);// front face
                gl.TexCoord(1.0f, 1.0f);
                gl.Vertex(Cube.Polygon[i].aVertex[2].x, Cube.Polygon[i].aVertex[2].y, Cube.Polygon[i].aVertex[2].z);// front face
                gl.TexCoord(0.0f, 1.0f);
                gl.Vertex(Cube.Polygon[i].aVertex[3].x, Cube.Polygon[i].aVertex[3].y, Cube.Polygon[i].aVertex[3].z);// front face
            }
            gl.End();
            gl.Flush();
        }
        private void drawPyramid(OpenGL gl)
        {
            if (isTexture)
            {
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                texture.Bind(gl);
            }
            else
            {
                gl.Disable(OpenGL.GL_TEXTURE_2D);
                gl.Color(1.0f, 1.0f, 1.0f);    // Color white
            }
            gl.Begin(OpenGL.GL_TRIANGLES);


            for (int i = 0; i < 4; i++)
            {
                gl.TexCoord(0.0f, 1.0f);
                gl.Vertex(Pyramid.Polygon[i].aVertex[0].x, Pyramid.Polygon[i].aVertex[0].y, Pyramid.Polygon[i].aVertex[0].z);
                gl.TexCoord(0.5f, 0.0f);
                gl.Vertex(Pyramid.Polygon[i].aVertex[1].x, Pyramid.Polygon[i].aVertex[1].y, Pyramid.Polygon[i].aVertex[1].z);
                gl.TexCoord(1.0f, 1.0f);
                gl.Vertex(Pyramid.Polygon[i].aVertex[2].x, Pyramid.Polygon[i].aVertex[2].y, Pyramid.Polygon[i].aVertex[2].z);
            }
            gl.End();
            gl.Begin(OpenGL.GL_QUADS);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(Pyramid.Polygon[4].aVertex[0].x, Pyramid.Polygon[4].aVertex[0].y, Pyramid.Polygon[4].aVertex[0].z);// front face
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(Pyramid.Polygon[4].aVertex[1].x, Pyramid.Polygon[4].aVertex[1].y, Pyramid.Polygon[4].aVertex[1].z);// front face
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(Pyramid.Polygon[4].aVertex[2].x, Pyramid.Polygon[4].aVertex[2].y, Pyramid.Polygon[4].aVertex[2].z);// front face
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(Pyramid.Polygon[4].aVertex[3].x, Pyramid.Polygon[4].aVertex[3].y, Pyramid.Polygon[4].aVertex[3].z);// front face
            gl.End();
            gl.Flush();
        }
        private void drawPrism(OpenGL gl)
        {
            if (isTexture)
            {
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                texture.Bind(gl);
            }
            else
            {
                gl.Disable(OpenGL.GL_TEXTURE_2D);
                gl.Color(1.0f, 1.0f, 1.0f);    // Color white
            }
            //gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
            gl.Begin(OpenGL.GL_QUADS);

            
            for (int i = 0; i < 3; i++)
            {
                gl.TexCoord(0.0f, 0.0f);
                gl.Vertex(Prism.Polygon[i].aVertex[0].x, Prism.Polygon[i].aVertex[0].y, Prism.Polygon[i].aVertex[0].z);// front face
                gl.TexCoord(1.0f, 0.0f);
                gl.Vertex(Prism.Polygon[i].aVertex[1].x, Prism.Polygon[i].aVertex[1].y, Prism.Polygon[i].aVertex[1].z);// front face
                gl.TexCoord(1.0f, 1.0f);
                gl.Vertex(Prism.Polygon[i].aVertex[2].x, Prism.Polygon[i].aVertex[2].y, Prism.Polygon[i].aVertex[2].z);// front face
                gl.TexCoord(0.0f, 1.0f);
                gl.Vertex(Prism.Polygon[i].aVertex[3].x, Prism.Polygon[i].aVertex[3].y, Prism.Polygon[i].aVertex[3].z);// front face
            }
            gl.End();
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(Prism.Polygon[3].aVertex[0].x, Prism.Polygon[3].aVertex[0].y, Prism.Polygon[3].aVertex[0].z);
            gl.TexCoord(0.5f, 0.0f);
            gl.Vertex(Prism.Polygon[3].aVertex[1].x, Prism.Polygon[3].aVertex[1].y, Prism.Polygon[3].aVertex[1].z);
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(Prism.Polygon[3].aVertex[2].x, Prism.Polygon[3].aVertex[2].y, Prism.Polygon[3].aVertex[2].z);


            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(Prism.Polygon[4].aVertex[0].x, Prism.Polygon[4].aVertex[0].y, Prism.Polygon[4].aVertex[0].z);
            gl.TexCoord(0.5f, 0.0f);
            gl.Vertex(Prism.Polygon[4].aVertex[1].x, Prism.Polygon[4].aVertex[1].y, Prism.Polygon[4].aVertex[1].z);
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(Prism.Polygon[4].aVertex[2].x, Prism.Polygon[4].aVertex[2].y, Prism.Polygon[4].aVertex[2].z);
            gl.End();
            gl.Flush();
        }

        private void Cube_Button_Click(object sender, EventArgs e)
        {
            kind3D = "Cube";
            Cube.Vert.Clear();
            Cube.Edge.Clear();
            Cube.Polygon.Clear();
            isDrawCube = true;
            isDrawPyramid = false;
            isDrawPrism = false;
            float edgeLength = 3;
            float halfLength = edgeLength / 2;
            nameShapes.Add("Cube\n");
            //Clear font bold another button
            this.Pyramid_Button.Font = new Font(Pyramid_Button.Name, Pyramid_Button.Font.Size, FontStyle.Regular);
            this.Prism_Button.Font = new Font(Prism_Button.Name, Prism_Button.Font.Size, FontStyle.Regular);
            //Change bold button
            this.Cube_Button.Font = new Font(Cube_Button.Name, Cube_Button.Font.Size, FontStyle.Bold);

            //add vertex to WIREFRAME
            Cube.Vert.Add(new POINT3D(-halfLength, halfLength, halfLength));// front top left
            Cube.Vert.Add(new POINT3D(halfLength, halfLength, halfLength));//front top right
            Cube.Vert.Add(new POINT3D(halfLength, -halfLength, halfLength));//front bottom right
            Cube.Vert.Add(new POINT3D(-halfLength, -halfLength, halfLength));//front bottom left
            Cube.Vert.Add(new POINT3D(-halfLength, halfLength, -halfLength)); //back top left
            Cube.Vert.Add(new POINT3D(halfLength, halfLength, -halfLength));//back top right
            Cube.Vert.Add(new POINT3D(halfLength, -halfLength, -halfLength));//back bottom right
            Cube.Vert.Add(new POINT3D(-halfLength, -halfLength, -halfLength));//back bottom left

            // add Edge to WIREFRAME
            Cube.Edge.Add(new EDGE(Cube.Vert[0], Cube.Vert[1])); // front top edge
            Cube.Edge.Add(new EDGE(Cube.Vert[1], Cube.Vert[2])); // front right edge
            Cube.Edge.Add(new EDGE(Cube.Vert[2], Cube.Vert[3])); // front bottom edge
            Cube.Edge.Add(new EDGE(Cube.Vert[3], Cube.Vert[0])); // front left edge

            Cube.Edge.Add(new EDGE(Cube.Vert[4], Cube.Vert[5])); // back top edge
            Cube.Edge.Add(new EDGE(Cube.Vert[5], Cube.Vert[6])); // back right edge
            Cube.Edge.Add(new EDGE(Cube.Vert[6], Cube.Vert[7])); // back bottom edge
            Cube.Edge.Add(new EDGE(Cube.Vert[7], Cube.Vert[4])); // back left edge

            Cube.Edge.Add(new EDGE(Cube.Vert[0], Cube.Vert[4])); // top left edge
            Cube.Edge.Add(new EDGE(Cube.Vert[1], Cube.Vert[5])); // top right edge
            Cube.Edge.Add(new EDGE(Cube.Vert[2], Cube.Vert[6])); //bottom right edge
            Cube.Edge.Add(new EDGE(Cube.Vert[3], Cube.Vert[7])); // bottom left edge

            //add Polygon to WIREFRAME
            List<POINT3D> front = new List<POINT3D>();
            List<POINT3D> back = new List<POINT3D>();
            List<POINT3D> left = new List<POINT3D>();
            List<POINT3D> right = new List<POINT3D>();
            List<POINT3D> top = new List<POINT3D>();
            List<POINT3D> bottom = new List<POINT3D>();

            front.Add(Cube.Vert[0]); front.Add(Cube.Vert[1]); front.Add(Cube.Vert[2]); front.Add(Cube.Vert[3]);
            back.Add(Cube.Vert[4]); back.Add(Cube.Vert[5]); back.Add(Cube.Vert[6]); back.Add(Cube.Vert[7]);
            left.Add(Cube.Vert[0]); left.Add(Cube.Vert[3]); left.Add(Cube.Vert[7]); left.Add(Cube.Vert[4]);
            right.Add(Cube.Vert[1]); right.Add(Cube.Vert[2]); right.Add(Cube.Vert[6]); right.Add(Cube.Vert[5]);
            top.Add(Cube.Vert[0]); top.Add(Cube.Vert[1]); top.Add(Cube.Vert[5]); top.Add(Cube.Vert[4]);
            bottom.Add(Cube.Vert[2]); bottom.Add(Cube.Vert[3]); bottom.Add(Cube.Vert[7]); bottom.Add(Cube.Vert[6]);

            Cube.Polygon.Add(new POLYGON2D(front));
            Cube.Polygon.Add(new POLYGON2D(back));
            Cube.Polygon.Add(new POLYGON2D(left));
            Cube.Polygon.Add(new POLYGON2D(right));
            Cube.Polygon.Add(new POLYGON2D(top));
            Cube.Polygon.Add(new POLYGON2D(bottom));
        }

        private void Pyramid_Button_Click(object sender, EventArgs e)
        {
            // hình chóp đáy hình vuông, giả sử hình chóp đều.
            kind3D = "Pyramid";
            Pyramid.Vert.Clear();
            Pyramid.Edge.Clear();
            Pyramid.Polygon.Clear();
            isDrawCube = false;
            isDrawPyramid = true;
            isDrawPrism = false;
            nameShapes.Add("Pyramid\n");
            float a_bottom = 3;
            float a_around = 3;
            float high = Convert.ToSingle(Math.Sqrt(a_around * a_around - a_bottom * a_bottom / 2));
            //Clear
            this.Cube_Button.Font = new Font(Cube_Button.Name, Cube_Button.Font.Size, FontStyle.Regular);
            this.Prism_Button.Font = new Font(Prism_Button.Name, Prism_Button.Font.Size, FontStyle.Regular);
            //Change bold button
            this.Pyramid_Button.Font = new Font(Pyramid_Button.Name, Cube_Button.Font.Size, FontStyle.Bold);
            //add vertex to WIREFRAME
            Pyramid.Vert.Add(new POINT3D(-a_bottom / 2, -high / 2, -a_bottom / 2)); // bottom face - back left vert; 
            Pyramid.Vert.Add(new POINT3D(a_bottom / 2, -high / 2, -a_bottom / 2));  // bottom face - back right vert;
            Pyramid.Vert.Add(new POINT3D(a_bottom / 2, -high / 2, a_bottom / 2));   // bottom face - front right vert;
            Pyramid.Vert.Add(new POINT3D(-a_bottom / 2, -high / 2, a_bottom / 2));  // bottom face - front left vert;
            Pyramid.Vert.Add(new POINT3D(0, high / 2, 0));        // top vert;

            // add Edge to WIREFRAME
            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[2], Pyramid.Vert[3])); // bottom face - front edge
            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[1], Pyramid.Vert[2])); // bottom face - right edge
            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[0], Pyramid.Vert[1])); // bottom face - back edge
            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[3], Pyramid.Vert[0])); // bottom face - left edge

            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[3], Pyramid.Vert[4])); // front left edge
            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[2], Pyramid.Vert[4])); // front right edge
            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[1], Pyramid.Vert[4])); // back right edge
            Pyramid.Edge.Add(new EDGE(Pyramid.Vert[0], Pyramid.Vert[4])); // back left edge

            //add Polygon to WIREFRAME
            List<POINT3D> front = new List<POINT3D>();
            List<POINT3D> back = new List<POINT3D>();
            List<POINT3D> left = new List<POINT3D>();
            List<POINT3D> right = new List<POINT3D>();
            List<POINT3D> bottom = new List<POINT3D>();

            front.Add(Pyramid.Vert[4]); front.Add(Pyramid.Vert[2]); front.Add(Pyramid.Vert[3]);
            back.Add(Pyramid.Vert[4]); back.Add(Pyramid.Vert[0]); back.Add(Pyramid.Vert[1]);
            left.Add(Pyramid.Vert[4]); left.Add(Pyramid.Vert[0]); left.Add(Pyramid.Vert[3]);
            right.Add(Pyramid.Vert[4]); right.Add(Pyramid.Vert[1]); right.Add(Pyramid.Vert[2]);
            bottom.Add(Pyramid.Vert[0]); bottom.Add(Pyramid.Vert[1]); bottom.Add(Pyramid.Vert[2]); bottom.Add(Pyramid.Vert[3]);

            Pyramid.Polygon.Add(new POLYGON2D(front));
            Pyramid.Polygon.Add(new POLYGON2D(back));
            Pyramid.Polygon.Add(new POLYGON2D(left));
            Pyramid.Polygon.Add(new POLYGON2D(right));
            Pyramid.Polygon.Add(new POLYGON2D(bottom));
        }

        private void Prism_Button_Click(object sender, EventArgs e)
        {
            // Lăng trụ đáy là tam gác đều, giả sử lăng trụ đều.
            kind3D = "Prism";
            Prism.Vert.Clear();
            Prism.Edge.Clear();
            Prism.Polygon.Clear();
            isDrawCube = false;
            isDrawPyramid = false;
            isDrawPrism = true;
            nameShapes.Add("Prism\n");
            float a_bottom = 3;
            float a_around = 3;

            this.Cube_Button.Font = new Font(Cube_Button.Name, Cube_Button.Font.Size, FontStyle.Regular);
            this.Pyramid_Button.Font = new Font(Pyramid_Button.Name, Pyramid_Button.Font.Size, FontStyle.Regular);
            //Change bold button
            this.Prism_Button.Font = new Font(Prism_Button.Name, Cube_Button.Font.Size, FontStyle.Bold);
            //add vertex to WIREFRAME
            Prism.Vert.Add(new POINT3D(-a_bottom / 2, -a_around / 2, 0));// back bottom left
            Prism.Vert.Add(new POINT3D(a_bottom / 2, -a_around / 2, -a_bottom / 2));//back bottom right
            Prism.Vert.Add(new POINT3D(0, -a_around / 2, a_bottom / 2));//front bottom
            Prism.Vert.Add(new POINT3D(-a_bottom / 2, a_around / 2, 0));//back top left
            Prism.Vert.Add(new POINT3D(a_bottom / 2, a_around / 2, -a_bottom / 2)); //back top right
            Prism.Vert.Add(new POINT3D(0, a_around / 2, a_bottom / 2));//front top

            // add Edge to WIREFRAME
            Prism.Edge.Add(new EDGE(Prism.Vert[0], Prism.Vert[1])); // back bottom edge
            Prism.Edge.Add(new EDGE(Prism.Vert[1], Prism.Vert[2])); // bottom right edge
            Prism.Edge.Add(new EDGE(Prism.Vert[2], Prism.Vert[0])); // bottom left edge
            Prism.Edge.Add(new EDGE(Prism.Vert[3], Prism.Vert[4])); // back top edge
            Prism.Edge.Add(new EDGE(Prism.Vert[4], Prism.Vert[5])); // top right edge
            Prism.Edge.Add(new EDGE(Prism.Vert[5], Prism.Vert[3])); // top left edge
            Prism.Edge.Add(new EDGE(Prism.Vert[0], Prism.Vert[3])); // back left edge
            Prism.Edge.Add(new EDGE(Prism.Vert[2], Prism.Vert[5])); // front edge
            Prism.Edge.Add(new EDGE(Prism.Vert[1], Prism.Vert[4])); // back right edge


            //add Polygon to WIREFRAME
            List<POINT3D> back = new List<POINT3D>();
            List<POINT3D> left = new List<POINT3D>();
            List<POINT3D> right = new List<POINT3D>();
            List<POINT3D> top = new List<POINT3D>();
            List<POINT3D> bottom = new List<POINT3D>();

            back.Add(Prism.Vert[0]); back.Add(Prism.Vert[1]); back.Add(Prism.Vert[4]); back.Add(Prism.Vert[3]);
            left.Add(Prism.Vert[0]); left.Add(Prism.Vert[2]); left.Add(Prism.Vert[5]); left.Add(Prism.Vert[3]);
            right.Add(Prism.Vert[2]); right.Add(Prism.Vert[1]); right.Add(Prism.Vert[4]); right.Add(Prism.Vert[5]);
            top.Add(Prism.Vert[3]); top.Add(Prism.Vert[4]); top.Add(Prism.Vert[5]);
            bottom.Add(Prism.Vert[0]); bottom.Add(Prism.Vert[1]); bottom.Add(Prism.Vert[2]);

            Prism.Polygon.Add(new POLYGON2D(back));
            Prism.Polygon.Add(new POLYGON2D(left));
            Prism.Polygon.Add(new POLYGON2D(right));
            Prism.Polygon.Add(new POLYGON2D(top));
            Prism.Polygon.Add(new POLYGON2D(bottom));
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            textBox.Clear();
            foreach (string index in nameShapes)
            {
                this.textBox.Text += index;
            }
        }

        private void Transform_Button_Click(object sender, EventArgs e)
        {
            // Button biến đổi hình
            Form2 f2 = new Form2();
            f2.ShowDialog();

            //Tịnh tiến
            POINT3D newCoord = new POINT3D();
            newCoord.x = f2.positionX;
            newCoord.y = f2.positionY;
            newCoord.z = f2.positionZ;

            //Xoay quanh trục
            float alphaX, alphaY, alphaZ;
            alphaX = f2.rotateX;
            alphaY = f2.rotateY;
            alphaZ = f2.rotateZ;

            //Zoom ảnh
            float scaleX, scaleY, scaleZ;
            scaleX = f2.scaleX;
            scaleY = f2.scaleY;
            scaleZ = f2.scaleZ;

            switch (kind3D)
            {
                case "Cube":
                    TranslationWireframe(ref Cube, new POINT3D(0, 0, 0), newCoord);
                    RotateWireframe(ref Cube, alphaX, alphaY, alphaZ);
                    ScaleWireframe(ref Cube, scaleX, scaleY, scaleZ);
                    break;
                case "Pyramid":
                    TranslationWireframe(ref Pyramid, new POINT3D(0, 0, 0), newCoord);
                    RotateWireframe(ref Pyramid, alphaX, alphaY, alphaZ);
                    ScaleWireframe(ref Pyramid, scaleX, scaleY, scaleZ);
                    break;
                case "Prism":
                    TranslationWireframe(ref Prism, new POINT3D(0, 0, 0), newCoord);
                    RotateWireframe(ref Prism, alphaX, alphaY, alphaZ);
                    ScaleWireframe(ref Prism, scaleX, scaleY, scaleZ);
                    break;
            }
        }
        private void RotateWireframe(ref WIREFRAME wf, float alphaX, float alphaY, float alphaZ)
        {
            // Thực hiện phép xoay biến đổi khối 3D
            // Biến đổi tất cả các điểm
            for (int i = 0; i < wf.Vert.Count(); i++)
            {
                wf.Vert[i] = RotatePoint(wf.Vert[i], alphaX, alphaY, alphaZ);
            }
            // Biến đổi tất cả các cạnh
            for (int j = 0; j < wf.Edge.Count(); j++)
            {
                wf.Edge[j].p_start = RotatePoint(wf.Edge[j].p_start, alphaX, alphaY, alphaZ);
                wf.Edge[j].p_end = RotatePoint(wf.Edge[j].p_end, alphaX, alphaY, alphaZ);
            }
            // Biến đổi tất cả các đa giác
            for (int k = 0; k < wf.Polygon.Count(); k++)
            {
                for (int l = 0; l < wf.Polygon[k].aVertex.Count(); l++)
                {
                    wf.Polygon[k].aVertex[l] = RotatePoint(wf.Polygon[k].aVertex[l], alphaX, alphaY, alphaZ);
                }
            }
        }
        private POINT3D TranslationPoint(POINT3D p, POINT3D oldCoord, POINT3D newCoord)
        {
            // Thực hiện tịnh tiến biến điểm p thành điểm kết quả
            POINT3D result = new POINT3D();
            result.x = p.x + newCoord.x - oldCoord.x;
            result.y = p.y + newCoord.y - oldCoord.y;
            result.z = p.z + newCoord.z - oldCoord.z;
            return result;
        }

        private void TranslationWireframe(ref WIREFRAME wf, POINT3D oldCoord, POINT3D newCoord)
        {
            // Thực hiện phép tịnh tiến cho khối 3D
            // Biến đổi tất cả các đỉnh
            for (int i = 0; i < wf.Vert.Count(); i++)
            {
                wf.Vert[i] = TranslationPoint(wf.Vert[i], oldCoord, newCoord);
            }
            // Biến đổi tất cả các cạnh
            for (int j = 0; j < wf.Edge.Count(); j++)
            {
                wf.Edge[j].p_start = TranslationPoint(wf.Edge[j].p_start, oldCoord, newCoord);
                wf.Edge[j].p_end = TranslationPoint(wf.Edge[j].p_end, oldCoord, newCoord);
            }
            // Biến đổi tất cả các đa giác
            for (int k = 0; k < wf.Polygon.Count(); k++)
            {
                for (int l = 0; l < wf.Polygon[k].aVertex.Count(); l++)
                {
                    wf.Polygon[k].aVertex[l] = TranslationPoint(wf.Polygon[k].aVertex[l], oldCoord, newCoord);
                }
            }
        }

        private POINT3D ScalePoint(POINT3D p, float aX, float aY, float aZ)
        {
            // Thực hiện phép Scale biến điểm p thành điểm kết quả
            POINT3D result = new POINT3D();
            result.x = p.x * aX;
            result.y = p.y * aY;
            result.z = p.z * aZ;
            return result;
        }

        private void ScaleWireframe(ref WIREFRAME wf, float aX, float aY, float aZ)
        {
            // Thực hiện phép Scale cho các khối 3D

            // Biến đổi tất cả các đỉnh
            for (int i = 0; i < wf.Vert.Count(); i++)
            {
                wf.Vert[i] = ScalePoint(wf.Vert[i], aX, aY, aZ);
            }

            // Biến đổi tất cả các cạnh
            for (int j = 0; j < wf.Edge.Count(); j++)
            {
                wf.Edge[j].p_start = ScalePoint(wf.Edge[j].p_start, aX, aY, aZ);
                wf.Edge[j].p_end = ScalePoint(wf.Edge[j].p_end, aX, aY, aZ);
            }

            // Biến đổi tất cả các đa giác
            for (int k = 0; k < wf.Polygon.Count(); k++)
            {
                for (int l = 0; l < wf.Polygon[k].aVertex.Count(); l++)
                {
                    wf.Polygon[k].aVertex[l] = ScalePoint(wf.Polygon[k].aVertex[l], aX, aY, aZ);
                }
            }
        }

        private POINT3D RotatePoint_xCoord(POINT3D p, float alphaX)
        {
            // Hàm xoay theo trục x
            // Ma trận xoay theo trục x
            // [[1, 0, 0, 0],
            //  [0, cos(alpha), -sin(alpha), 0],
            //  [0, sin(alpha), cos(alpha), 0],
            //  [0, 0, 0, 1]]
            POINT3D result = new POINT3D();
            result.x = p.x;
            result.y = Convert.ToSingle(p.y * Math.Cos(alphaX) - p.z * Math.Sin(alphaX));
            result.z = Convert.ToSingle(p.y * Math.Sin(alphaX) + p.z * Math.Cos(alphaX));
            return result;
        }

        private POINT3D RotatePoint_yCoord(POINT3D p, float alphaY)
        {
            // Hàm xoay theo trục y
            // Ma trận xoay theo trục y
            // [[cos(alpha), 0, sin(alpha), 0],
            //  [0, 1, 0, 0],
            //  [-sin(alpha), 0, cos(alpha), 0],
            //  [0, 0, 0, 1]]
            POINT3D result = new POINT3D();
            result.x = Convert.ToSingle(p.x * Math.Cos(alphaY) + p.z * Math.Sin(alphaY));
            result.y = p.y;
            result.z = Convert.ToSingle(-p.x * Math.Sin(alphaY) + p.z * Math.Cos(alphaY));
            return result;
        }

        private POINT3D RotatePoint_zCoord(POINT3D p, float alphaZ)
        {
            // Hàm xoay theo trục z
            // Ma trận xoay theo trục z
            // [[cos(alpha), -sin(alpha), 0, 0],
            //  [sin(alpha), cos(alpha), 0, 0],
            //  [0, 0, 1, 0],
            //  [0, 0, 0, 1]]
            POINT3D result = new POINT3D();
            result.x = Convert.ToSingle(p.x * Math.Cos(alphaZ) - p.y * Math.Sin(alphaZ));
            result.y = Convert.ToSingle(p.x * Math.Sin(alphaZ) + p.y * Math.Cos(alphaZ));
            result.z = p.z;
            return result;
        }

        private POINT3D RotatePoint(POINT3D p, float alphaX, float alphaY, float alphaZ)
        {
            // Thực hiện phép xoay biến điểm p thành điểm kết quả.
            POINT3D result = new POINT3D();
            result = RotatePoint_xCoord(p, alphaX); // xoay theo trục x

            result = RotatePoint_yCoord(result, alphaY); //xoay theo trục y

            result = RotatePoint_zCoord(result, alphaZ); // xoay theo trục z
            return result;
        }

        private void Texture_Click(object sender, EventArgs e)
        {
            //  Show a file open dialog.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                isTexture = true;
                //  Destroy the existing texture.
                texture.Destroy(openGLControl.OpenGL);

                //  Create a new texture.
                texture.Create(openGLControl.OpenGL, openFileDialog1.FileName);

                //  Redraw.
                openGLControl.Invalidate();

            }
        }
    }
}