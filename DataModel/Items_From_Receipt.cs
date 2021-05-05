using System;
using System.Collections.Generic;

namespace DataModel
{
    public class Items_From_Receipt
    {
        public string Config_item_ID { get; set; }
        public string Product_Name { get; set; }
        public string Company_Name { get; set; }
        public string Install_Date { get; set; }
        public string Serial_Number { get; set; }
        public string Reference_Name { get; set; }

        //public List<Items_From_Receipt> pattern_1 = null;
        // Pattern 1 looks for three lines, keyword is "Serial Number", takes it and the previous 2 lines
        //        WIN10 LICENCE                      75.00
        //  0000000000784
        //Serial No:       04248000841821
        //WIN10 LICENCE                      75.00
        //  0000000000784
        //Serial No:       04248000841830
        //WIN10 LICENCE                      75.00
        //  0000000000784
        //Serial No:       04248000841831
        //WIN10 LICENCE                      75.00
        //  0000000000784
        //Serial No:       04248000841832
        //public List<Items_From_Receipt> pattern_2 = null;
        //public List<Items_From_Receipt> pattern_3 = null;
        //public List<Items_From_Receipt> pattern_4 = null;

        public Items_From_Receipt ()
        {

            Config_item_ID = "";
            Product_Name = "";
            Company_Name = "";
            Install_Date = "";
            Serial_Number = "";
            Reference_Name = "";

            //pattern_1 = new List<Items_From_Receipt>();
            //pattern_2 = new List<Items_From_Receipt>();
            //pattern_3 = new List<Items_From_Receipt>();
            //pattern_4 = new List<Items_From_Receipt>();

        }



    }



}
