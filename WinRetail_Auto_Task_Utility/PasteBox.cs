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
        string pattern_1_output = @"Pattern_1_output_file.txt";
        
        public PasteBox()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {

            using (StreamReader SR = new StreamReader(path_to_what_user_pasted))
            {
                string[] lines = File.ReadAllLines(path_to_what_user_pasted);
                {
                    var target_text = lines.SkipWhile(x => x != "========================================")// skips everything before 
                        .Skip(1)//and the ====== itself
                                //.TakeWhile(x => x != "========================================")//up to =======
                        .TakeWhile(x => x != "                          --------------")
                        .ToList();


                    using (StreamWriter SW = new StreamWriter(pattern_1_output))
                    {

                        {
                            target_text.ForEach(i => SW.Write("{0}\r", i));
                        }
                    }
                }

            }

            /// get rid of line with    LAYAWAY RECALL in it
            var oldlines = File.ReadAllLines(pattern_1_output);
            var omit_line = oldlines.Where(line => !line.Contains("LAYAWAY RECALL"));
            File.WriteAllLines(pattern_1_output, omit_line);





        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
