namespace Lagrange_interpolation
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.canvas2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chord_rb = new System.Windows.Forms.RadioButton();
            this.custom_rb = new System.Windows.Forms.RadioButton();
            this.zero_lbl = new System.Windows.Forms.Label();
            this.one_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas2)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Location = new System.Drawing.Point(12, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(776, 426);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // canvas2
            // 
            this.canvas2.BackColor = System.Drawing.Color.White;
            this.canvas2.Location = new System.Drawing.Point(12, 467);
            this.canvas2.Name = "canvas2";
            this.canvas2.Size = new System.Drawing.Size(776, 37);
            this.canvas2.TabIndex = 2;
            this.canvas2.TabStop = false;
            this.canvas2.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas2_Paint);
            this.canvas2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas2_MouseDown);
            this.canvas2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas2_MouseMove);
            this.canvas2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas2_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(821, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 37);
            this.label3.TabIndex = 5;
            this.label3.Text = "T VALUES";
            // 
            // chord_rb
            // 
            this.chord_rb.AutoSize = true;
            this.chord_rb.Checked = true;
            this.chord_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chord_rb.Location = new System.Drawing.Point(828, 108);
            this.chord_rb.Name = "chord_rb";
            this.chord_rb.Size = new System.Drawing.Size(124, 24);
            this.chord_rb.TabIndex = 6;
            this.chord_rb.TabStop = true;
            this.chord_rb.Text = "Chord Length";
            this.chord_rb.UseVisualStyleBackColor = true;
            this.chord_rb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chord_rb_MouseClick);
            // 
            // custom_rb
            // 
            this.custom_rb.AutoSize = true;
            this.custom_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.custom_rb.Location = new System.Drawing.Point(828, 151);
            this.custom_rb.Name = "custom_rb";
            this.custom_rb.Size = new System.Drawing.Size(135, 24);
            this.custom_rb.TabIndex = 7;
            this.custom_rb.Text = "Custom Values";
            this.custom_rb.UseVisualStyleBackColor = true;
            this.custom_rb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.custom_rb_MouseClick);
            // 
            // zero_lbl
            // 
            this.zero_lbl.AutoSize = true;
            this.zero_lbl.BackColor = System.Drawing.Color.Transparent;
            this.zero_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zero_lbl.Location = new System.Drawing.Point(55, 507);
            this.zero_lbl.Name = "zero_lbl";
            this.zero_lbl.Size = new System.Drawing.Size(19, 20);
            this.zero_lbl.TabIndex = 8;
            this.zero_lbl.Text = "0";
            // 
            // one_lbl
            // 
            this.one_lbl.AutoSize = true;
            this.one_lbl.BackColor = System.Drawing.Color.Transparent;
            this.one_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.one_lbl.Location = new System.Drawing.Point(727, 507);
            this.one_lbl.Name = "one_lbl";
            this.one_lbl.Size = new System.Drawing.Size(19, 20);
            this.one_lbl.TabIndex = 9;
            this.one_lbl.Text = "1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 546);
            this.Controls.Add(this.one_lbl);
            this.Controls.Add(this.zero_lbl);
            this.Controls.Add(this.custom_rb);
            this.Controls.Add(this.chord_rb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.canvas2);
            this.Controls.Add(this.canvas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.PictureBox canvas2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton chord_rb;
        private System.Windows.Forms.RadioButton custom_rb;
        private System.Windows.Forms.Label zero_lbl;
        private System.Windows.Forms.Label one_lbl;
    }
}

