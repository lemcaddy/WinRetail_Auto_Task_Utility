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
        string path_to_what_user_pasted = @"what_user_pasted_in.txt";
        string pattern_1_output = @"Pattern_1_output_file.txt";
        string company_name;



        public PasteBox()
        {
            InitializeComponent();
        }
         
      



        private void button_ok_Click(object sender, EventArgs e)
        {

            //using (StreamReader sr = new StreamReader(path_to_what_user_pasted))
            //{
            //    string[] lines = File.ReadAllLines(path_to_what_user_pasted);
            //    if (lines.StartsWith("Company:"))
            //    {
            //        string[] aparts = lines.Split(':');
            //        company_name = aparts[1];
            //    }
            //}

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

            


                //pull out pattern 1
                List<string> Lines_from_what_user_pasted_in = new List<string>(File.ReadAllLines(pattern_1_output));
            List<string> Pattern_1_List = new List<string>();
            for (int i = 0; i < Lines_from_what_user_pasted_in.Count; i++)
            {
                if (Lines_from_what_user_pasted_in[i].ToString().Contains("Serial No:"))
                {
                    Pattern_1_List.Add(Lines_from_what_user_pasted_in[i - 2]
                        +","
                        + (Lines_from_what_user_pasted_in[i - 1])+
                        ","
                        + Lines_from_what_user_pasted_in[i]);
                    
                    
                    
                }
                
            }
            using (StreamWriter sr = new StreamWriter(pattern_1_output))
            {

                Pattern_1_List.ForEach(i => sr.Write("{0}\n", i));

            }

            // output looks like this.....
            //WIN10 LICENCE                      75.00,  0000000000784,Serial No:       04248000841821
            //WIN10 LICENCE                      75.00,  0000000000784,Serial No:       04248000841830
            //WIN10 LICENCE                      75.00,  0000000000784,Serial No:       04248000841831
            //WIN10 LICENCE                      75.00,  0000000000784,Serial No:       04248000841832

            ///parse pattern 1 and add it datamodel
            ///

            using (StreamReader sr = new StreamReader(pattern_1_output))
            {
                while(true)
                {
                    string fullline = sr.ReadLine();

                    if (fullline == null)
                        break;

                    string[] aparts = fullline.Split(',');
                    string product_des = aparts[0];

                    List<Items_From_Receipt> list = new List<Items_From_Receipt>();
                    list.Add(new Items_From_Receipt
                    {
                        Config_item_ID = "",
                        Product_Name = product_des,
                        Company_Name = company_name

                    }) ;

                }

            }
            
            



        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasteBox_Load(object sender, EventArgs e)
        {

        }
    }
}
