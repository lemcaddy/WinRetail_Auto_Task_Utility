using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel;
using NPOI.SS.Formula.Functions;
using TransLog;



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

           string timeStamp= DateTime.Now.ToString("h:mm:ss tt");
           Logwriter.writelog("Login:"+ timeStamp);

        }

        


        private void button_Paste_in_receipt_Click(object sender, EventArgs e)
        {
            PasteBox pasteForm = new PasteBox();
            pasteForm.ShowDialog(this);

            //give me the list of paste form and ad it to this form

            List<Items_From_Receipt> pasted_in = pasteForm.Item_list_123;

            // add to utilties list, it needs  global list.
            foreach (Items_From_Receipt ifr in pasted_in)
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
            foreach (Items_From_Receipt currrob in global_list)
            {
                currrob.Company_Name = textBox_Company_details.Text;
            }

            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;
           

        }

        private void button_new_Click(object sender, EventArgs e)
        {

            global_list.Add(new Items_From_Receipt
            {
                Config_item_ID = "",
                Product_Name = textBox_Product_name.Text,
                Company_Name = textBox_Company_details.Text,
                Install_Date = textBox_install_date.Text,
                Serial_Number = textBox_serial_number.Text,
                Reference_Name = textBox_reference_name.Text

            });
            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;

            dataGridView_products.FirstDisplayedScrollingRowIndex = dataGridView_products.RowCount - 1;
            dataGridView_products.Rows[dataGridView_products.RowCount - 1].Selected = true;
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

        private void button_search_Click(object sender, EventArgs e)
        {
            List<Items_From_Receipt> filtered = new List<Items_From_Receipt>();

            if (string.IsNullOrEmpty(textBox_Product_name.Text)
                && string.IsNullOrEmpty(textBox_serial_number.Text)
                && string.IsNullOrEmpty(textBox_reference_name.Text))
            {
                MessageBox.Show("You need a value in the either the" +
                    "  product Description" +
                    "Serial Number" +
                    "refernece number" +
                    " textboxes above");
                return;
            }
            
            
                bool s1 = string.IsNullOrEmpty(textBox_Product_name.Text);
                bool s2 = string.IsNullOrEmpty(textBox_serial_number.Text);
                bool s3 = string.IsNullOrEmpty(textBox_reference_name.Text);



            if (s1 == false && s2 == true && s3 == true)
            {
                foreach (Items_From_Receipt currOb in global_list)
                {

                    if (currOb.Product_Name.Contains(textBox_Product_name.Text))
                    {
                        filtered.Add(currOb);
                    }

                }
                dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
                var source = filtered;
                dataGridView_products.DataSource = source;

            }
            if (s1 == true && s2 == false && s3 == true)
            {
                foreach (Items_From_Receipt currOb in global_list)
                {

                    if (currOb.Serial_Number.Contains(textBox_serial_number.Text))
                    {
                        filtered.Add(currOb);
                    }

                }
                dataGridView_products.DataSource = null;
                var source = filtered;
                dataGridView_products.DataSource = source;
            }

            if (s1 == true && s2 == true && s3 == false)
            {
                foreach (Items_From_Receipt currOb in global_list)
                {

                    if (currOb.Reference_Name.Contains(textBox_reference_name.Text))
                    {
                        filtered.Add(currOb);
                    }
                   
                }
                dataGridView_products.DataSource = null;
                var source = filtered;
                dataGridView_products.DataSource = source;
            }


        }

        private void button_clear_fields_Click(object sender, EventArgs e)
        {
            textBox_Product_name.Clear();
            textBox_Company_details.Clear();
            textBox_install_date.Clear();
            textBox_serial_number.Clear();
            textBox_reference_name.Clear();
            return;
           
        }

        private void button_reset_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("Warning!!! Reset will clear all work done");
            textBox_Product_name.Clear();
            textBox_Company_details.Clear();
            textBox_install_date.Clear();
            textBox_serial_number.Clear();
            textBox_reference_name.Clear();

            dataGridView_products.DataSource = null;
            var source = global_list;
            dataGridView_products.DataSource = source;
                

        }

        private void button_re_set_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Warning!!! Reset will clear all work done");
            textBox_Product_name.Clear();
            textBox_Company_details.Clear();
            textBox_install_date.Clear();
            textBox_serial_number.Clear();
            textBox_reference_name.Clear();

            dataGridView_products.DataSource = null;
            //global_list = null;
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            Write_to_current_working_file();
            Logwriter.writelog("File Exported:");
        }

        private void Write_to_current_working_file()
        {
            using (StreamWriter sr = new StreamWriter(@"current_working_file.csv"))
                foreach (Items_From_Receipt currOb in global_list)
                {

                    sr.WriteLine(currOb.Config_item_ID.ToString() + ","
                        + currOb.Product_Name.ToString() + ","
                        + currOb.Company_Name.ToString() + ","
                        + currOb.Install_Date.ToString() + ","
                        + currOb.Serial_Number.ToString() + ","
                        + currOb.Reference_Name.ToString()
                       );

                }
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                label_current_file.Text = "File in Use: "+fileToOpen.ToString();


                //                ,WIN10 LICENCE, John Hegarty xxx,11 / 12 / 20, 04248000841821,
                //,WIN10 LICENCE, John Hegarty xxx,11 / 12 / 20, 04248000841830,
                //,WIN10 LICENCE, John Hegarty xxx,11 / 12 / 20, 04248000841831,
                //,WIN10 LICENCE, John Hegarty xxx,11 / 12 / 20, 04248000841832,
                //,AP - 3665                       , John Hegarty xxx,11 / 12 / 20,66500P011200308,till1
                //,AP - 3665                       , John Hegarty xxx,11 / 12 / 20,66500P011200264,
                //,AP - 3665                       , John Hegarty xxx,11 / 12 / 20,66500P011200268,
                //,AM - 1015 NO TOUCH, John Hegarty xxx,11 / 12 / 20,015CBG212200061,

                using (StreamReader sr = new StreamReader(fileToOpen))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] wholeLine = line.Split(',');

                        global_list.Add(new Items_From_Receipt()
                        {
                            Config_item_ID = wholeLine[0],
                            Product_Name = wholeLine[1],
                            Company_Name = wholeLine[2],
                            Install_Date = wholeLine[3],
                            Serial_Number = wholeLine[4],
                            Reference_Name = wholeLine[5]

                        });
                    }

                }
                dataGridView_products.DataSource = null;
                var source = global_list;
                dataGridView_products.DataSource = source;
                //Process.Start(@"current_working_file.csv");
            }
        }


        //string timeStamp = DateTime.Now.ToString("h:mm:ss tt");
        //Logwriter.writelog("Save:"+ timeStamp);
        //    Logwriter.Store_Name = textBox_Company_details.Text;
    }
}
