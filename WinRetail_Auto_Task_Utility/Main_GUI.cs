using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinRetail_Auto_Task_Utility
{
    public partial class ATUtility : Form
    {
        public ATUtility()//
        {
            InitializeComponent();
        }
        //
        private void ATUtility_Load(object sender, EventArgs e)
        {

        }

        private void button_Paste_in_receipt_Click(object sender, EventArgs e)
        {
            Form pasteForm = new PasteBox();
            pasteForm.Show();
        }
    }
}
