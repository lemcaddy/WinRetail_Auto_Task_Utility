using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinRetail_Auto_Task_Utility
{
    public partial class PasteBox : Form
    {
        string path_to_what_user_pasted = @"what_user_pasted_in.txt";
        public PasteBox()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
         
         using(StreamWriter sr = new StreamWriter(path_to_what_user_pasted))
            {
                sr.WriteLine(textBox_pasted_items.Text);
            }

        }
    }
}
