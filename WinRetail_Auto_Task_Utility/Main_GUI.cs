using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel;

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
            
            //give me the list of paste form and ad it to this form

            //List<Items_From_Receipt> pasted_in = pasteForm.li

            // add to utilties list, it needs  global list.
        }

        private void dataGridView_products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            
            //var source = items_From_Receipt.Pasted_in;
            //dataGridView_products.DataSource = source;
            //this.Refresh();
        }
    }
}
