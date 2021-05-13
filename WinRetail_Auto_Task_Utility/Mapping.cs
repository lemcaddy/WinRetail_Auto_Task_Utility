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
            string fileToOPen = @"Mapping.txt";
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
    }
}
