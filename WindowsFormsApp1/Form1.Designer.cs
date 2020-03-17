namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLControl = new SharpGL.OpenGLControl();
            this.Texture_Button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Cube_Button = new System.Windows.Forms.Button();
            this.Pyramid_Button = new System.Windows.Forms.Button();
            this.Prism_Button = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.Transform_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(93, 12);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(695, 426);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            // 
            // Texture_Button
            // 
            this.Texture_Button.Location = new System.Drawing.Point(12, 12);
            this.Texture_Button.Name = "Texture_Button";
            this.Texture_Button.Size = new System.Drawing.Size(75, 23);
            this.Texture_Button.TabIndex = 1;
            this.Texture_Button.Text = "Texture";
            this.Texture_Button.UseVisualStyleBackColor = true;
            this.Texture_Button.Click += new System.EventHandler(this.Texture_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Cube_Button
            // 
            this.Cube_Button.Location = new System.Drawing.Point(12, 59);
            this.Cube_Button.Name = "Cube_Button";
            this.Cube_Button.Size = new System.Drawing.Size(75, 23);
            this.Cube_Button.TabIndex = 2;
            this.Cube_Button.Text = "Cube";
            this.Cube_Button.UseVisualStyleBackColor = true;
            this.Cube_Button.Click += new System.EventHandler(this.Cube_Button_Click);
            // 
            // Pyramid_Button
            // 
            this.Pyramid_Button.Location = new System.Drawing.Point(12, 88);
            this.Pyramid_Button.Name = "Pyramid_Button";
            this.Pyramid_Button.Size = new System.Drawing.Size(75, 23);
            this.Pyramid_Button.TabIndex = 3;
            this.Pyramid_Button.Text = "Pyramid";
            this.Pyramid_Button.UseVisualStyleBackColor = true;
            this.Pyramid_Button.Click += new System.EventHandler(this.Pyramid_Button_Click);
            // 
            // Prism_Button
            // 
            this.Prism_Button.Location = new System.Drawing.Point(12, 117);
            this.Prism_Button.Name = "Prism_Button";
            this.Prism_Button.Size = new System.Drawing.Size(75, 23);
            this.Prism_Button.TabIndex = 4;
            this.Prism_Button.Text = "Prism";
            this.Prism_Button.UseVisualStyleBackColor = true;
            this.Prism_Button.Click += new System.EventHandler(this.Prism_Button_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 146);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(75, 20);
            this.textBox.TabIndex = 5;
            this.textBox.Click += new System.EventHandler(this.textBox_Click);
            // 
            // Transform_Button
            // 
            this.Transform_Button.Location = new System.Drawing.Point(12, 380);
            this.Transform_Button.Name = "Transform_Button";
            this.Transform_Button.Size = new System.Drawing.Size(75, 23);
            this.Transform_Button.TabIndex = 6;
            this.Transform_Button.Text = "Transform";
            this.Transform_Button.UseVisualStyleBackColor = true;
            this.Transform_Button.Click += new System.EventHandler(this.Transform_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Transform_Button);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.Prism_Button);
            this.Controls.Add(this.Pyramid_Button);
            this.Controls.Add(this.Cube_Button);
            this.Controls.Add(this.Texture_Button);
            this.Controls.Add(this.openGLControl);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button Texture_Button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Cube_Button;
        private System.Windows.Forms.Button Pyramid_Button;
        private System.Windows.Forms.Button Prism_Button;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button Transform_Button;
    }
}

