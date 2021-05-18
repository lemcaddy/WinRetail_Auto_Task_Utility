using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinRetail_Auto_Task_Utility
{
    public partial class Mapping : Form
    {

        BindingList<Mapped> List_of_what_is_Mapped = new BindingList<Mapped>();
        public Mapping()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Mapping_Load(object sender, EventArgs e)
        {
            string fileToOPen =  @"Mapping_dgv_Entries.txt";

            
            using (StreamReader sr = new StreamReader(fileToOPen))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] wholeLine = line.Split(',');
                    if (wholeLine.Length > 1)
                    {
                        List_of_what_is_Mapped.Add(new Mapped()
                        {
                            Receipt_Name = wholeLine[0],
                            AutoTask_Name = wholeLine[1]

                        });

                    }


                }
            }
            dataGridView1.DataSource = null;
            var source = List_of_what_is_Mapped;
            dataGridView1.DataSource = source;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 430;
            DataGridViewColumn columns = dataGridView1.Columns[1];
            columns.Width = 430;
        }

        private void button_New_Click(object sender, EventArgs e)
        {
            List_of_what_is_Mapped.Add(new Mapped
            {
                Receipt_Name= textBox_Receipt_name.Text,
                AutoTask_Name= textBox_AT_name.Text
              

            });
            dataGridView1.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = List_of_what_is_Mapped;
            dataGridView1.DataSource = source;
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 430;
            DataGridViewColumn columns = dataGridView1.Columns[1];
            columns.Width = 430;

            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;


            using (StreamWriter sw = new StreamWriter(@"Mapping_dgv_Entries.txt"))
            {



                foreach (Mapped item in List_of_what_is_Mapped)
                {
                    sw.WriteLine(item.Receipt_Name +
                        "," + item.AutoTask_Name
                        );
                }
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are You Sure You Wish To Delete This Record?", "Warning",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            List_of_what_is_Mapped.RemoveAt(dataGridView1.CurrentRow.Index);
            dataGridView1.DataSource = null;
            var source = List_of_what_is_Mapped;
            dataGridView1.DataSource = source;

            using (StreamWriter sw = new StreamWriter(@"Mapping_dgv_Entries.txt"))
            {



                foreach (Mapped item in List_of_what_is_Mapped)
                {
                    sw.WriteLine(item.Receipt_Name +
                        "," + item.AutoTask_Name
                        );
                }
            }
            dataGridView1.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source2 = List_of_what_is_Mapped;
            dataGridView1.DataSource = source2;
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 430;
            DataGridViewColumn columns = dataGridView1.Columns[1];
            columns.Width = 430;

            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
            textBox_Receipt_name.Text = null;
            textBox_AT_name.Text = null;

            //dataGridView1.DataSource = null;
            //var source1 = List_of_what_is_Mapped;
            //dataGridView1.DataSource = source1;
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Have You Finished Mapping?\nYou Sure you Want to Exit?", "Warning",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"Mapping_dgv_Entries.txt"))
            {



                foreach (Mapped item in List_of_what_is_Mapped)
                {
                    sw.WriteLine(item.Receipt_Name +
                        "," + item.AutoTask_Name
                        );
                }
            }
            this.Close();
        }
    }
}
