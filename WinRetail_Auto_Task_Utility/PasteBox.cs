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
using DataModel;

namespace WinRetail_Auto_Task_Utility
{
    public partial class PasteBox : Form
    {
        string contents_of_textbox =  @"what_user_pasted_in.txt";
        string pattern_output = @"Pattern_output_file.txt";
        string resulting_file = @"Final_output_file.txt";

        public PasteBox()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(contents_of_textbox))
            {
                sw.WriteLine(textBox_pasted_items.Text);
            }

            using (StreamReader SR = new StreamReader(contents_of_textbox))
            {
                string[] lines = File.ReadAllLines(contents_of_textbox);
                {
                    var target_text = lines.SkipWhile(x => x != "========================================")// skips everything before 
                        .Skip(1)//and the ====== itself
                                //.TakeWhile(x => x != "========================================")//up to =======
                        .TakeWhile(x => x != "                          --------------")
                        .ToList();


                    using (StreamWriter SW = new StreamWriter(pattern_output))
                    {

                        {
                            target_text.ForEach(i => SW.Write("{0}\r", i));
                        }
                    }
                }

            }

            /// get rid of line with    LAYAWAY RECALL in it
            var oldlines = File.ReadAllLines(pattern_output);
            var omit_line = oldlines.Where(line => !line.Contains("LAYAWAY RECALL"));
            File.WriteAllLines(pattern_output, omit_line);


            List<Items_From_Receipt> list = new List<Items_From_Receipt>();

            List<string> Lines = new List<string>(File.ReadAllLines(pattern_output));

            List<string> new_list = new List<string>();

           
            for (int i = 0; i < Lines.Count; i++)

            {
                new_list.Add(Lines[i].ToString());// add all the lines to the list


                if (Lines[i].ToString().Contains("Serial No:"))
                {
                    new_list.Add(Lines[i - 2]+"," + (Lines[i - 1] + "," + (Lines[i])));



                    list.Add(new Items_From_Receipt
                    {
                        Config_item_ID = "",
                        Product_Name = Lines[i - 2].Substring(0, 30),
                        Serial_Number = Lines[i].Substring(16)

                    }); ;

                }
                
            }


            using (StreamWriter sr = new StreamWriter(resulting_file, false))//Example_2_Final.txt
            {

                new_list.ForEach(i => sr.Write("{0}\n", i));

            }



        }
    
            



        

        private void button_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasteBox_Load(object sender, EventArgs e)
        {

        }

        private void textBox_pasted_items_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
