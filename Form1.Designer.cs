namespace Karishin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnGO = new Button();
            btnOne = new Button();
            btnRestart = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnGO
            // 
            btnGO.Location = new Point(831, 49);
            btnGO.Margin = new Padding(3, 2, 3, 2);
            btnGO.Name = "btnGO";
            btnGO.Size = new Size(102, 34);
            btnGO.TabIndex = 0;
            btnGO.Text = "Старт/Стоп";
            btnGO.UseVisualStyleBackColor = true;
            btnGO.Click += btnGO_Click;
            // 
            // btnOne
            // 
            btnOne.Location = new Point(714, 49);
            btnOne.Margin = new Padding(3, 2, 3, 2);
            btnOne.Name = "btnOne";
            btnOne.Size = new Size(96, 34);
            btnOne.TabIndex = 1;
            btnOne.Text = "Хід";
            btnOne.UseVisualStyleBackColor = true;
            btnOne.Click += btnOne_Click;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(951, 49);
            btnRestart.Margin = new Padding(3, 2, 3, 2);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(101, 34);
            btnRestart.TabIndex = 2;
            btnRestart.Text = "Спочатку";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 20);
            label1.Name = "label1";
            label1.Size = new Size(0, 21);
            label1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 80);
            label2.Name = "label2";
            label2.Size = new Size(56, 21);
            label2.TabIndex = 4;
            label2.Text = "Вовки:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 126);
            label3.Name = "label3";
            label3.Size = new Size(71, 21);
            label3.TabIndex = 5;
            label3.Text = "Вовчиці:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(66, 35);
            label4.Name = "label4";
            label4.Size = new Size(74, 21);
            label4.TabIndex = 6;
            label4.Text = "Кролики:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox3);
            groupBox1.Controls.Add(pictureBox2);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            groupBox1.Location = new Point(714, 168);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(193, 162);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Тварини";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(5, 113);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(42, 34);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(5, 68);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(43, 33);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(5, 25);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(43, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 645);
            Controls.Add(groupBox1);
            Controls.Add(btnRestart);
            Controls.Add(btnOne);
            Controls.Add(btnGO);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Вовчий острів   | by Karishin Oleksandr";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnGO;
        private Button btnOne;
        private Button btnRestart;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
    }
}
