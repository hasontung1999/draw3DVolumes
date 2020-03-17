using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    class Camera
    {
        public double eyex, eyey, eyez; //vị trí đặt của camera
        public double centerx, centery, centerz; //vị trí của vật
        public double upx, upy, upz;  //vecto chỉ hướng lên trên của camera
        public double radius, alpha, phi; //bán kính

        public Camera()
        {
            eyex = 6; eyey = 6; eyez = 6;
            centerx = 0; centery = 0; centerz = 0;

            newRadius();
            newAlpha();
            newPhi();
        }
        //tính khoảng cách từ camera đến vật khi thay đổi vị camera
        public void newRadius()
        {          
            radius = Math.Sqrt(Math.Pow(eyex - centerx, 2)
                   + Math.Pow(eyey - centery, 2)
                   + Math.Pow(eyez - centerz, 2));
        }
        //tính lại góc alpha, phi khi vị trí camera thay đổi
        public void newAlpha()
        {          
            alpha = Math.Atan((eyex - centerx) / (eyez - centerz));
        }

        public void newPhi()
        {
            phi = Math.Asin((eyey - centery) / radius);
        }

        //Di chuyển camera lại gần điểm nhìn(nhấn phím Z)
        public void zoomIn()
        {
            //Zoom lai gan thi khoang cach giua vi tri dat camera giam
            eyex += -0.005 * eyex;
            eyey += -0.005 * eyey;
            eyez += -0.005 * eyez;
            //Update lai ban kinh, va cac goc
            newRadius();
            newAlpha();
            newPhi();
        }

        //Di chuyển camera ra xa (nhấn phím X)
        public void zoomOut()
        {
            eyex += 0.005 * eyex;
            eyey += 0.005 * eyey;
            eyez += 0.005 * eyez;

            newRadius();
            newAlpha();
            newPhi();
        }

        //Di chuyển camera quay xung quanh điểm nhìn sang trái (nhấn phím mũi tên trái)
        public void moveLeft()
        {
            alpha -= 0.005;
            eyex = centerx + radius * Math.Cos(phi) * Math.Sin(alpha);
            eyez = centerz + radius * Math.Cos(phi) * Math.Cos(alpha);
        }

        //Di chuyển camera quay xung quanh điểm nhìn sang phải (nhấn phím mũi tên phải)
        public void moveRight()
        {
            alpha += 0.005;
            eyex = centerx + radius * Math.Cos(phi) * Math.Sin(alpha);
            eyez = centerz + radius * Math.Cos(phi) * Math.Cos(alpha);
        }

        //Di chuyển camera quay xung quanh điểm nhìn lên trên (nhấn phím mũi tên lên)
        public void moveUp()
        {
            phi += 0.005;
            eyex = centerx + radius * Math.Cos(phi) * Math.Sin(alpha);
            eyey = centery + radius * Math.Sin(phi);
            eyez = centerz + radius * Math.Cos(phi) * Math.Cos(alpha);
        }

        //Di chuyển camera quay xung quanh điểm nhìn xuống dưới(nhấn phím mũi tên xuống)
        public void moveDown()
        {
            phi -= 0.005;
            eyex = centerx + radius * Math.Cos(phi) * Math.Sin(alpha);
            eyey = centery + radius * Math.Sin(phi);
            eyez = centerz + radius * Math.Cos(phi) * Math.Cos(alpha);
        }

    }
}
