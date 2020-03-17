using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public float positionX, positionY, positionZ;
        public float scaleX, scaleY, scaleZ;
        public float rotateX, rotateY, rotateZ;
        public Form2()
        {
            InitializeComponent();
        }

        private void bt_Finish_Click(object sender, EventArgs e)
        {
            positionX = float.Parse(Text_position_x.Text);
            positionY = float.Parse(Text_position_y.Text);
            positionZ = float.Parse(Text_position_z.Text);

            rotateX = float.Parse(Text_rotation_x.Text);
            rotateY = float.Parse(Text_rotation_y.Text);
            rotateZ = float.Parse(Text_rotation_z.Text);

            scaleX = float.Parse(Text_scale_x.Text);
            scaleY = float.Parse(Text_scale_y.Text);
            scaleZ = float.Parse(Text_scale_z.Text);
            this.Close();
        }
    }
}
