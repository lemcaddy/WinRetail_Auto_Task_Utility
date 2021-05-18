namespace WinRetail_Auto_Task_Utility
{
    partial class Mapping
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Receipt_name = new System.Windows.Forms.TextBox();
            this.textBox_AT_name = new System.Windows.Forms.TextBox();
            this.button_New = new System.Windows.Forms.Button();
            this.button_del = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(360, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mapping Config Form";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(877, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "=================================================================================" +
    "================================================================";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 439);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(877, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "=================================================================================" +
    "================================================================";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 714);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(877, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "=================================================================================" +
    "================================================================";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(104, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "AutoTask Name";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(123, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Receipt Name";
            // 
            // textBox_Receipt_name
            // 
            this.textBox_Receipt_name.Location = new System.Drawing.Point(319, 157);
            this.textBox_Receipt_name.Name = "textBox_Receipt_name";
            this.textBox_Receipt_name.Size = new System.Drawing.Size(525, 20);
            this.textBox_Receipt_name.TabIndex = 6;
            // 
            // textBox_AT_name
            // 
            this.textBox_AT_name.Location = new System.Drawing.Point(319, 276);
            this.textBox_AT_name.Name = "textBox_AT_name";
            this.textBox_AT_name.Size = new System.Drawing.Size(525, 20);
            this.textBox_AT_name.TabIndex = 7;
            // 
            // button_New
            // 
            this.button_New.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_New.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_New.Location = new System.Drawing.Point(94, 355);
            this.button_New.Name = "button_New";
            this.button_New.Size = new System.Drawing.Size(188, 54);
            this.button_New.TabIndex = 9;
            this.button_New.Text = "&New";
            this.button_New.UseVisualStyleBackColor = false;
            this.button_New.Click += new System.EventHandler(this.button_New_Click);
            // 
            // button_del
            // 
            this.button_del.BackColor = System.Drawing.Color.Red;
            this.button_del.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_del.Location = new System.Drawing.Point(780, 355);
            this.button_del.Name = "button_del";
            this.button_del.Size = new System.Drawing.Size(188, 54);
            this.button_del.TabIndex = 10;
            this.button_del.Text = "&Delete";
            this.button_del.UseVisualStyleBackColor = false;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // button_ok
            // 
            this.button_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ok.Location = new System.Drawing.Point(94, 739);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(155, 79);
            this.button_ok.TabIndex = 11;
            this.button_ok.Text = "&Ok";
            this.button_ok.UseVisualStyleBackColor = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.Location = new System.Drawing.Point(813, 739);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(155, 79);
            this.button_Exit.TabIndex = 12;
            this.button_Exit.Text = "&Exit";
            this.button_Exit.UseVisualStyleBackColor = false;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(51, 473);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 25);
            this.label7.TabIndex = 16;
            this.label7.Text = "Receipt Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(771, 473);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "AutoTask Name";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(56, 514);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(893, 209);
            this.dataGridView1.TabIndex = 18;
            // 
            // Mapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 830);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.button_New);
            this.Controls.Add(this.textBox_AT_name);
            this.Controls.Add(this.textBox_Receipt_name);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Mapping";
            this.Text = "Mapping";
            this.Load += new System.EventHandler(this.Mapping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Receipt_name;
        private System.Windows.Forms.TextBox textBox_AT_name;
        private System.Windows.Forms.Button button_New;
        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}