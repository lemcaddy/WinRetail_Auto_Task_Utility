namespace WinRetail_Auto_Task_Utility
{
    partial class PasteBox
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
            this.textBox_pasted_items = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Paste the Receipt below (Ctr + V)....";
            // 
            // textBox_pasted_items
            // 
            this.textBox_pasted_items.Location = new System.Drawing.Point(75, 81);
            this.textBox_pasted_items.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_pasted_items.Multiline = true;
            this.textBox_pasted_items.Name = "textBox_pasted_items";
            this.textBox_pasted_items.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_pasted_items.Size = new System.Drawing.Size(376, 483);
            this.textBox_pasted_items.TabIndex = 1;
            // 
            // button_ok
            // 
            this.button_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ok.Location = new System.Drawing.Point(505, 198);
            this.button_ok.Margin = new System.Windows.Forms.Padding(2);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(67, 110);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "Ok";
            this.button_ok.UseVisualStyleBackColor = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_exit
            // 
            this.button_exit.BackColor = System.Drawing.Color.Red;
            this.button_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_exit.Location = new System.Drawing.Point(505, 454);
            this.button_exit.Margin = new System.Windows.Forms.Padding(2);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(67, 110);
            this.button_exit.TabIndex = 3;
            this.button_exit.Text = "Exit";
            this.button_exit.UseVisualStyleBackColor = false;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // PasteBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(810, 862);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_pasted_items);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PasteBox";
            this.Text = "PasteBox";
            this.Load += new System.EventHandler(this.PasteBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_pasted_items;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_exit;
    }
}