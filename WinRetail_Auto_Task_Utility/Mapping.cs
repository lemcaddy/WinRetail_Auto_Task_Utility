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
using TransLog;

namespace WinRetail_Auto_Task_Utility
{
    public partial class Mapping : Form
    {
        string current_timestamp = Timestamp();
        private static string Timestamp()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
        }

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
            this.CenterToScreen();
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
                    Receipt_Name = textBox_Receipt_name.Text,
                    AutoTask_Name = textBox_AT_name.Text

                });
            dataGridView1.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = List_of_what_is_Mapped;
            bool isUnique = List_of_what_is_Mapped.Distinct().Count() == List_of_what_is_Mapped.Count();
            if(isUnique==false)
            {
                MessageBox.Show("Error Duplictes");
            }
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
            Logwriter.writelog("#NEW MAPPING:Time,>>>>>Item Updated:Receipt Name, AT Name<<<<<");
            Logwriter.writelog("NEW MAPPING:" + current_timestamp + "," + ">>>>>" + textBox_Receipt_name.Text +","+ textBox_AT_name.Text);
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are You Sure You Wish To Delete This Record?", "Warning",
            MessageBoxButtons.OK, MessageBoxIcon.Error);

            string for_del_receipt_name = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string for_del_AT_name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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

            Logwriter.writelog("#DELETE  MAPPING:Time,>>>>>Item DELETED:Receipt Name, AT Name<<<<<");
            Logwriter.writelog("DELETE MAPPING:" + current_timestamp + "," + ">>>>>" + for_del_receipt_name +","+ for_del_AT_name+ "<<<<<");

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
            checkmappings();

            this.Close();
        }

        private void checkmappings()
        {
            Dictionary<string, string> Mappings = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(@"Mapping_dgv_Entries.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] wholeLine = line.Split(',');
                    if (wholeLine.Length > 1)
                    {
                        try
                        {
                            Mappings.Add(wholeLine[0], wholeLine[1]);
                        }
                        catch (ArgumentException)
                        {
                            //MessageBox.Show("Warning Check You Mapping Config Form\n You Have Duplicate Entries!!");
                            MessageBox.Show("Warning: Duplicte entry:"+wholeLine[0], "Mapping Config Form",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                            Mapping form = new Mapping();
                            
                            form.Show();
                            form.CenterToScreen();
                            form.BringToFront();
                            form.TopMost = true;
                            form.Focus();
                            
                            
                        }
                    }

                }


            }
        }

        ///////////////////////////importbutton/////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        private void button_Import_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            List_of_what_is_Mapped = new BindingList<Mapped>();

            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                using (StreamReader sr = new StreamReader(fileToOpen))
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
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    int count = List_of_what_is_Mapped.Count();
                    Logwriter.writelog("#MAPPING IMPORT CSV:Time,Filename,Time lapsed,Count");
                    Logwriter.writelog("MAPPING IMPORT CSV:" + current_timestamp + "," + FD.FileName + "," + elapsedMs.ToString() + "," + count.ToString());
                }
                var source1 = List_of_what_is_Mapped;
                dataGridView1.DataSource = source1;
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 430;
                DataGridViewColumn columns = dataGridView1.Columns[1];
                columns.Width = 430;

            }
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

        private void button_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "csv files (*.csv)|*.csv";
            dlg.Title = "Export in CSV format";
            dlg.CheckPathExists = true;
            dlg.InitialDirectory = Application.StartupPath;
            dlg.ShowDialog();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (dlg.FileName != "")
            {
                using (StreamWriter sw = new StreamWriter(dlg.FileName))
                {



                    foreach (Mapped item in List_of_what_is_Mapped)
                    {
                        sw.WriteLine(item.Receipt_Name +
                            "," + item.AutoTask_Name
                            );
                    }
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    int count = List_of_what_is_Mapped.Count();

                    Logwriter.writelog("#FILE EXPORTED:Time,Filename,Item Count,Time Elapsed");
                    Logwriter.writelog("FILE EXPORTED:" + current_timestamp + "," + dlg.FileName + "," + count + "," + elapsedMs);
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
