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
            string fileToOPen =  @"Mapping.txt";

            
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
                            winreatail_name = wholeLine[0],
                            autoTask_name = wholeLine[1]

                        });

                    }


                }
            }
            dataGridView1.DataSource = null;
            var source = List_of_what_is_Mapped;
            dataGridView1.DataSource = source;
        }

        private void button_New_Click(object sender, EventArgs e)
        {
            List_of_what_is_Mapped.Add(new Mapped
            {
                winreatail_name= textBox_Receipt_name.Text,
                autoTask_name= textBox_AT_name.Text
              

            });
            dataGridView1.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = List_of_what_is_Mapped;
            dataGridView1.DataSource = source;

          dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;


            using (StreamWriter sw = new StreamWriter(@"Mapping.txt"))
            {

            
            
                foreach (Mapped item in List_of_what_is_Mapped)
                {
                    sw.WriteLine(item.winreatail_name +
                        "," + item.autoTask_name
                        );
                }
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            List_of_what_is_Mapped.RemoveAt(dataGridView1.CurrentRow.Index);
            dataGridView1.DataSource = null;
            var source = List_of_what_is_Mapped;
            dataGridView1.DataSource = source;

            using (StreamWriter sw = new StreamWriter(@"Mapping.txt"))
            {



                foreach (Mapped item in List_of_what_is_Mapped)
                {
                    sw.WriteLine(item.winreatail_name +
                        "," + item.autoTask_name
                        );
                }
            }

            //dataGridView1.DataSource = null;
            //var source1 = List_of_what_is_Mapped;
            //dataGridView1.DataSource = source1;
        }
    }
}
