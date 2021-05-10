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
        BindingList<Items_From_Receipt> global_list = new BindingList<Items_From_Receipt>();
        
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
            PasteBox pasteForm = new PasteBox();
            pasteForm.ShowDialog(this);

            //give me the list of paste form and ad it to this form

            List<Items_From_Receipt> pasted_in = pasteForm.Item_list_123;

            // add to utilties list, it needs  global list.
            foreach(Items_From_Receipt ifr in pasted_in)
            {
                global_list.Add(ifr);
            }

            // show the revised datagrid
            var source = global_list;
            dataGridView_products.DataSource = source;
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

        private void dataGridView_products_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView_products.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox_Product_name.Text = row.Cells[1].Value.ToString();
                textBox_Company_details.Text = row.Cells[2].Value.ToString();
                textBox_install_date.Text = row.Cells[3].Value.ToString();
                textBox_serial_number.Text = row.Cells[4].Value.ToString();
                textBox_reference_name.Text = row.Cells[5].Value.ToString();
            }
                
            
        }

        private void button_update_Click(object sender, EventArgs e)
        {

            Items_From_Receipt currob = global_list[dataGridView_products.CurrentRow.Index];
            



            currob.Product_Name = textBox_Product_name.Text;
            currob.Company_Name = textBox_Company_details.Text;
            currob.Install_Date = textBox_install_date.Text;
            currob.Serial_Number = textBox_serial_number.Text;
            currob.Reference_Name = textBox_reference_name.Text;

            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;


        }

        private void button_set_all_company_Click(object sender, EventArgs e)

        {
            foreach(Items_From_Receipt currrob in global_list)
            {
                currrob.Company_Name= textBox_Company_details.Text;
            }

            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;

        }

        private void button_new_Click(object sender, EventArgs e)
        {
            string configId = "";
            string Prodname = textBox_Product_name.Text;
            string comNmse= textBox_Company_details.Text;
            string indate = textBox_install_date.Text;
            string seNo = textBox_serial_number.Text;
            string refNo = textBox_reference_name.Text;

            global_list.Add(new Items_From_Receipt
            {
                Config_item_ID = "",
                Product_Name = Prodname,
                Company_Name = comNmse,
                Install_Date = indate,
                Serial_Number = seNo,
                Reference_Name = refNo

            });
            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
           global_list.RemoveAt(dataGridView_products.CurrentRow.Index);
           dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
           var source = global_list;
           dataGridView_products.DataSource = source;
        }

        private void button_set_all_install_date_Click(object sender, EventArgs e)
        {
            foreach (Items_From_Receipt currrob in global_list)
            {
                currrob.Install_Date = textBox_install_date.Text;
            }

            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;

        }
    }
    }
