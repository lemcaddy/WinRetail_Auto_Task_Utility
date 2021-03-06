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
using TransLog;



namespace WinRetail_Auto_Task_Utility
{
    public partial class ATUtility : Form
    {
        BindingList<Items_From_Receipt> global_list = new BindingList<Items_From_Receipt>();
        BindingList<Items_From_Receipt> filtered = new BindingList<Items_From_Receipt>();
        string Name_of_user = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;
        //string User = Name_of_user;
        string current_timestamp = Timestamp();
        bool bFiltered = false;



        public ATUtility()//
        {
            InitializeComponent();
        }
        //
        private void ATUtility_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Logwriter.writelog("#LOGIN:, User,Time");
            Logwriter.writelog("LOGIN:" + Name_of_user + "," + current_timestamp);

            dataGridView_products.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView_products.Refresh();
            System.Threading.Thread.Sleep(1000);
            button_re_load.PerformClick();
           



        }
        //private void dataGridView_products_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    foreach (DataGridViewRow Myrow in dataGridView_products.Rows)
        //        if (Myrow.Cells[8].Value.ToString() == "VOIDED")
        //        {
        //            Myrow.DefaultCellStyle.BackColor = Color.Red;
        //        }
        //        else
        //        {
        //            Myrow.DefaultCellStyle.BackColor = Color.White;
        //        }

        //}






        private static string Timestamp()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
        }

        private void button_Paste_in_receipt_Click(object sender, EventArgs e)
        {
            bFiltered = false;
            PasteBox pasteForm = new PasteBox();
            pasteForm.ShowDialog(this);
            List<Items_From_Receipt> pasted_in = pasteForm.Item_list_123;
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            mappings = load_mappings();
            foreach (Items_From_Receipt ifr in pasted_in)
            {
                if (ifr.status == "REFUNDED")
                {
                    //find the item in global list that is not already refunded
                    //and mark it now as refunded, do not add this item to global list

                    bool bFound = false;
                    foreach(Items_From_Receipt gi in global_list)

                    {
                        if (gi.status!="REFUNDED"&& gi.status!="VOIDED")
                        {
                            string Mapped_product = ifr.Product_Name;// assuming product name is not mapped yet
                            if(mappings.ContainsKey(ifr.Product_Name))
                            {
                                Mapped_product = mappings[ifr.Product_Name];// ah the product is mapped!
                            }
                            if ((gi.Product_Name.Trim() == ifr.Product_Name.Trim() || gi.Product_Name.Trim()==Mapped_product.Trim()) &&
                                ((gi.Serial_Number.Length == 0 && ifr.Serial_Number.Length == 0)
                                ||(gi.Serial_Number.Length>0 && gi.Serial_Number.Trim()==ifr.Serial_Number.Trim())))// should this be >0

                            {
                                gi.status="REFUNDED";
                                bFound = true;
                                break;
                            }

                        }



                    }
                    if(!bFound)
                    {
                        List<string> not_founds = new List<string>();
                        not_founds.Add(ifr.Product_Name);
                        not_founds.Add(",");
                        not_founds.Add(ifr.Serial_Number);
                        MessageBox.Show("Not Found ; Product;"+
                            ifr.Product_Name+
                            "\n Serial Number;"+
                            ifr.Serial_Number,"Refund Item Not Found");

                        
                        
                        Logwriter.writelog("#REFUND ERRORS:TIME, ERROR DETAILS");
                            Logwriter.writelog((Timestamp()+
                                ","+
                                string.Format("REFUND ERROR: ({0}).", string.Join(", ", not_founds))));
                    }
                }
                else
                {
                    global_list.Add(ifr);
                }
            }


            button_re_load.PerformClick(); 

            

            
            var list_count = global_list.Count();
            Logwriter.writelog("#NUMBER OF ITEMS PASTED IN:,Count");
            Logwriter.writelog("NUMBER OF ITEMS PASTED IN:"+list_count.ToString());
        }
        private void dataGridView_products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //foreach (DataGridViewRow r in dataGridView_products.Rows)
            //    if (r.Cells[8].ToString() == "VOIDED")
            //        //if (r.Cells.Value.ToString() == "") - also trying
            //        r.DefaultCellStyle.BackColor = Color.Red;

        }
        private void dataGridView_products_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView_products.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox_Product_name.Text = row.Cells[2].Value.ToString();
                textBox_Company_details.Text = row.Cells[3].Value.ToString();
                textBox_install_date.Text = row.Cells[4].Value.ToString();
                textBox_warrrenty.Text = row.Cells[5].Value.ToString();
                textBox_serial_number.Text = row.Cells[6].Value.ToString();
                textBox_reference_name.Text = row.Cells[7].Value.ToString();
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {


            Items_From_Receipt currob;
            if(bFiltered == true)
            {
                int rowindex = filtered[dataGridView_products.CurrentRow.Index].rowIndex;
                currob = global_list[rowindex];
            }
            else
            {

               currob = global_list[dataGridView_products.CurrentRow.Index];
            }
                
            
            currob.Product_Name = textBox_Product_name.Text;
            currob.Company_Name = textBox_Company_details.Text;
            currob.Install_Date = textBox_install_date.Text;
            currob.Warranty_Expiration = textBox_warrrenty.Text;
            currob.Serial_Number = textBox_serial_number.Text;
            currob.Reference_Name = textBox_reference_name.Text;

            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;
            Logwriter.writelog("#UPDATE:Time,>>>>>Item Updated:Config ID,Product,Company,Install Date,Warranty,Serial No,Ref Name <<<<<");
            Logwriter.writelog("UPDATE:" + current_timestamp + ","+">>>>>" + currob.Config_item_ID + ","+
                currob.Product_Name + "," +currob.Company_Name + "," + currob.Install_Date + ","+ currob.Warranty_Expiration
                + "," +currob.Serial_Number + "," + currob.Reference_Name+"<<<<<");

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
            
            Logwriter.writelog("#SET ALL COMPANY NAME:Value ");
            Logwriter.writelog("SET ALL COMPANY NAME:"+ textBox_Company_details.Text);


        }

        private void button_new_Click(object sender, EventArgs e)
        {

            global_list.Add(new Items_From_Receipt
            {
                Config_item_ID = "",
                Product_Name = textBox_Product_name.Text,
                Company_Name = textBox_Company_details.Text,
                Install_Date = textBox_install_date.Text,
                Warranty_Expiration = textBox_warrrenty.Text,
                Serial_Number = textBox_serial_number.Text,
                Reference_Name = textBox_reference_name.Text,
                status = ""

            }) ;
            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;

            dataGridView_products.FirstDisplayedScrollingRowIndex = dataGridView_products.RowCount - 1;
            dataGridView_products.Rows[dataGridView_products.RowCount - 1].Selected = true;
            Logwriter.writelog("#NEW:Time,>>>>>VALUE:Item Updated:Product,Company,Install Date,Warranty,Serial No,Ref Name<<<<<<");
            Logwriter.writelog("#NEW:"+current_timestamp+">>>>>"+ textBox_Product_name.Text+","+
                textBox_Company_details.Text + "," +
                textBox_install_date.Text + "," +
                textBox_warrrenty.Text + "," +
                textBox_serial_number.Text + "," +
                textBox_reference_name.Text+"<<<<<<");
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            global_list.RemoveAt(dataGridView_products.CurrentRow.Index);
            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;
            Logwriter.writelog("#DELETE:Time,>>>>>VALUE:Item Updated:Product,Company,Install Date,Warranty,Serial No,Ref Name<<<<<<");
            Logwriter.writelog("DELETE:" + current_timestamp + "," + ">>>>>" +
                dataGridView_products.CurrentRow.Cells[1].Value.ToString() + ","
                + dataGridView_products.CurrentRow.Cells[2].Value.ToString() + ","
                + dataGridView_products.CurrentRow.Cells[3].Value.ToString() + ","
                + dataGridView_products.CurrentRow.Cells[4].Value.ToString() + ","
                + dataGridView_products.CurrentRow.Cells[5].Value.ToString() + ","
                 + dataGridView_products.CurrentRow.Cells[6].Value.ToString() + ","
                + dataGridView_products.CurrentRow.Cells[7].Value.ToString()+"<<<<<");
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
            Logwriter.writelog("#SET ALL INSTALL DATE:Value ");
            Logwriter.writelog("SET ALL INSTALL DATE:" + textBox_install_date.Text);

        }

        private void button_search_Click(object sender, EventArgs e)
        {
             filtered = new BindingList<Items_From_Receipt>();

            if (string.IsNullOrEmpty(textBox_Product_name.Text.Trim())
                && string.IsNullOrEmpty(textBox_serial_number.Text.Trim())
                && string.IsNullOrEmpty(textBox_reference_name.Text.Trim()))
            {
                MessageBox.Show("You need a value in the either the" +
                    "  Product Name, " +
                    "Serial Number, " +
                    "Reference Name " +
                    " textboxes above");
                Logwriter.writelog("#SEARCH:Status, Reason");
                Logwriter.writelog("SEARCH:Failed, as Values not Entered in either" +
                    "\n Product Name," +
                    "\n Serial Number" +
                    "\n or Reference Name textboxes");
                return;
            }

            bool s1 = string.IsNullOrEmpty(textBox_Product_name.Text.Trim());
            bool s2 = string.IsNullOrEmpty(textBox_serial_number.Text.Trim());
            bool s3 = string.IsNullOrEmpty(textBox_reference_name.Text.Trim());

            Search_by_Product_name(filtered, s1, s2, s3);
            Search_By_serial_Number(filtered, s1, s2, s3);
            Search_By_refernce_Name(filtered, s1, s2, s3);

            bFiltered = true;

        }

        private void Search_By_refernce_Name(BindingList<Items_From_Receipt> filtered, bool s1, bool s2, bool s3)
        {
            if (s1 == true && s2 == true && s3 == false)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                int index = 0;
                foreach (Items_From_Receipt currOb in global_list)
                {

                    if (currOb.Reference_Name.Contains(textBox_reference_name.Text))
                    {
                        currOb.rowIndex = index;
                        filtered.Add(currOb);
                    }
                    index++;

                }
                watch.Stop();

                var elapsedMs = watch.ElapsedMilliseconds;
                string searchTerm = textBox_reference_name.Text;
                int count = filtered.Count();
                string searchType = "Reference Name";

                dataGridView_products.DataSource = null;
                var source = filtered;
                dataGridView_products.DataSource = source;

                Logwriter.writelog("#SEARCH:Time,Search by type, Search Term, Time Search Took, No of Items Returned");
                Logwriter.writelog("SEARCH:" + current_timestamp + "," + searchType + "," + searchTerm + "," + elapsedMs + "," + count);
            }
        }

        private void Search_By_serial_Number(BindingList<Items_From_Receipt> filtered, bool s1, bool s2, bool s3)
        {
            if (s1 == true && s2 == false && s3 == true)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                int index = 0;
                foreach (Items_From_Receipt currOb in global_list)
                {

                    if (currOb.Serial_Number.Contains(textBox_serial_number.Text))
                    {
                        currOb.rowIndex = index;
                        filtered.Add(currOb);
                    }
                    index++;

                }
                watch.Stop();
                
                var elapsedMs = watch.ElapsedMilliseconds;
                string searchTerm = textBox_serial_number.Text;
                int count = filtered.Count();
                string searchType = "Serial Number";


                dataGridView_products.DataSource = null;
                var source = filtered;
                dataGridView_products.DataSource = source;

                Logwriter.writelog("#SEARCH:Time,Search by type, Search Term, Time Search Took, No of Items Returned");
                Logwriter.writelog("SEARCH:" + current_timestamp + "," + searchType + "," + searchTerm + "," + elapsedMs + "," + count);

            }
        }

        private void Search_by_Product_name(BindingList<Items_From_Receipt> filtered, bool s1, bool s2, bool s3)
        {
            if (s1 == false && s2 == true && s3 == true)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                int index = 0;
                foreach (Items_From_Receipt currOb in global_list)
                {

                    if (currOb.Product_Name.Contains(textBox_Product_name.Text))
                    {
                        currOb.rowIndex = index;
                        filtered.Add(currOb);
                    }

                    index++;
                }
                watch.Stop();

                var elapsedMs = watch.ElapsedMilliseconds;
                dataGridView_products.DataSource = null;
                var source = filtered;
                dataGridView_products.DataSource = source;

                string searchType = "Product Name";
                string searchTerm = textBox_Product_name.Text;
                int count = filtered.Count();
                Logwriter.writelog("#SEARCH:Time,Search by type, Search Term, Time Search Took, No of Items Returned");
                Logwriter.writelog("SEARCH:" + current_timestamp + "," + searchType + "," + searchTerm + "," + elapsedMs + "," + count);

            }
        }

        private void button_clear_fields_Click(object sender, EventArgs e)
        {
            textBox_Product_name.Clear();
            textBox_Company_details.Clear();
            textBox_install_date.Clear();
            textBox_warrrenty.Clear();
            textBox_serial_number.Clear();
            textBox_reference_name.Clear();
            Logwriter.writelog("#CLEAR FIELDS:Time");
            Logwriter.writelog("CLEAR FIELDS:"+ current_timestamp);

            return;
          
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            bFiltered = false;
            //MessageBox.Show("Warning!!! Reset will clear all work done");
            textBox_Product_name.Clear();
            textBox_Company_details.Clear();
            textBox_install_date.Clear();
            textBox_warrrenty.Clear();
            textBox_serial_number.Clear();
            textBox_reference_name.Clear();

            dataGridView_products.DataSource = null;
            var source = global_list;
            string fsource = source.ToString();
            dataGridView_products.DataSource = source;

            Logwriter.writelog("#RE-LOAD:Time,Source Reset To:, Count");
            Logwriter.writelog("RE-LOAD:"+ current_timestamp+","+ "Global List"+","+global_list.Count() );


        }

        private void button_re_set_Click(object sender, EventArgs e)
        {
            bFiltered = false;
            MessageBox.Show("Warning!!! Reset will clear all work done");
            textBox_Product_name.Clear();
            textBox_Company_details.Clear();
            textBox_install_date.Clear();
            textBox_warrrenty.Clear();
            textBox_serial_number.Clear();
            textBox_reference_name.Clear();

            dataGridView_products.DataSource = null;
            global_list = new BindingList<Items_From_Receipt>();
            //global_list = null;
            Logwriter.writelog("#RESET:Time");
            Logwriter.writelog("RESET:" + current_timestamp);
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            
            foreach (Items_From_Receipt currOb in global_list)
            {
                bool empty_company_name = !currOb.Company_Name.Any();
                bool empty_Inatall_date = !currOb.Install_Date.Any();

                if (empty_company_name)
                {
                    MessageBox.Show("Error With A Company Name Entry\n Are One or More Entries Blank?");
                    return;

                }
                
                if (empty_Inatall_date)
                {
                    MessageBox.Show("Error With An Install Date Entry\n" +
                        "You Can Not Proceed Without an Install Date\n" +
                        "Are One or More Entries Blank?");
                    return;

                }
               


            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "|*.csv";
            saveFileDialog1.Title = "Save CSV File";
            saveFileDialog1.ShowDialog();
            //Configuration Item ID[updates only],[required] Product Name,[required] Company,Configuration Item Category,Configuration Item Type,[required] Install Date, Warranty Expiration,Serial Number, Reference Number,Reference Name, Number of Users, Contact, Location, Area, Contract, Service, Service Bundle,Billing Product, Billing Product Effective Date,Billing Product Expiration Date, Vendor, Service Level Agreement, Parent Configuration Item Serial Number, Description, Hourly Cost,Monthly Cost, Daily Cost,Per - Use Cost,Setup Fee, Company Link,Material Code(required if creating product),Active / Inactive,Subscription Name, Reviewed for Contract, Subscription Description, Subscription Period Type[required if creating subscription],Subscription Effective Date[required if creating subscription],Subscription Expiration Date[required if creating subscription],Subscription Period Price[required if creating subscription],Subscription Material Code[required if creating subscription],Subscription Purchase Order Number, Subscription Period Cost, Subscription Active,Subscription Vendor, Domain (Required if Category = Domain),SSL Source(Required if Category = SSL Certificate),UDF: 29682852 Username,UDF: 29682853 Password,UDF: 29682854 IP Address, UDF:29682861 OS,UDF: 29682862 Name,UDF: 29682864 Roles,UDF: 29682865 WAN IP:,UDF: 29682866 LAN IP:,UDF: 29682867 Brand,UDF: 29682868 SSID,UDF: 29682869 Security,UDF: 29682870 Location,UDF: 29682871 Make & Model,UDF: 29682872 Battery Life, UDF:29682873 Version,UDF: 29682874 URL,UDF: 29682875 Registrar,UDF: 29682913 AEM_DeviceID,UDF: 29682914 AEM_DeviceUID,UDF: 29682915 AEM_Description,UDF: 29682916 AEM_Manufacturer,UDF: 29682917 AEM_Model,UDF: 29682918 AEM_OperatingSystem,UDF: 29682919 AEM_IPAddress,UDF: 29682920 User - defined field 3,UDF: 29682921 User - defined field 2,UDF: 29682922 User - defined field 1,UDF: 29682923 User - defined field 10,UDF: 29682924 User - defined field 7,UDF: 29682925 User - defined field 6,UDF: 29682926 User - defined field 5,UDF: 29682927 User - defined field 4,UDF: 29682928 User - defined field 9,UDF: 29682929 User - defined field 8,UDF: 29682981 User - defined field 19,UDF: 29682982 User - defined field 17,UDF: 29682983 User - defined field 18,UDF: 29682984 User - defined field 15,UDF: 29682985 User - defined field 16,UDF: 29682986 User - defined field 13,UDF: 29682987 User - defined field 14,UDF: 29682988 User - defined field 11,UDF: 29682989 User - defined field 12,UDF: 29682990 User - defined field 21,UDF: 29682991 User - defined field 20,UDF: 29682992 User - defined field 22,UDF: 29682993 User - defined field 23,UDF: 29682994 User - defined field 24,UDF: 29682995 User - defined field 25,UDF: 29682996 User - defined field 26,UDF: 29682997 User - defined field 27,UDF: 29682998 User - defined field 28,UDF: 29682999 User - defined field 29,UDF: 29683000 Server Type -PixelPOS,UDF: 29683001 User - defined field 30,UDF: 29683002 Bit - Locker,UDF: 29683003 PixelPoint - Backup,UDF: 29683004 Wholesaler,UDF: 29683005 Store ID, UDF:29683006 Symbol Group
            //                                                                                                                                            , WIN10 IOT ENT 2019 LTSC MULTILANG, Centra Roscommon 2054,,,15 / 03 / 2021,,04248000841821,,Till 1,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
            //,WIN10 IOT ENT 2019 LTSC MULTILANG, Centra Roscommon 2054,,,15 / 03 / 2021,,04248000841830,,Till 2,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
            //,WIN10 IOT ENT 2019 LTSC MULTILANG, Centra Roscommon 2054,,,15 / 03 / 2021,,04248000841831,,Till 3,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = saveFileDialog1.FileName;
            if (saveFileDialog1.FileName != "")
            {
                Dictionary<string, string> Mappings = new Dictionary<string, string>();

                Mappings = load_mappings();

                using (var file = File.CreateText(path))
                {   // next line creates AT header/////
                    file.WriteLine("\"Configuration Item ID [updates only]\",\"[required] Product Name\",\"[required] Company\",\"Configuration Item Category\",\"Configuration Item Type\",\"[required] Install Date\",\"Warranty Expiration\",\"Serial Number\",\"Reference Number\",\"Reference Name\",\"Number of Users\",\"Contact\",\"Location\",\"Area\",\"Contract\",\"Service\",\"Service Bundle\",\"Billing Product\",\"Billing Product Effective Date\",\"Billing Product Expiration Date\",\"Vendor\",\"Service Level Agreement\",\"Parent Configuration Item Serial Number\",\"Description\",\"Hourly Cost\",\"Monthly Cost\",\"Daily Cost\",\"Per-Use Cost\",\"Setup Fee\",\"Company Link\",\"Material Code (required if creating product)\",\"Active/Inactive\",\"Subscription Name\",\"Reviewed for Contract\",\"Subscription Description\",\"Subscription Period Type [required if creating subscription]\",\"Subscription Effective Date [required if creating subscription]\",\"Subscription Expiration Date [required if creating subscription]\",\"Subscription Period Price [required if creating subscription]\",\"Subscription Material Code [required if creating subscription]\",\"Subscription Purchase Order Number\",\"Subscription Period Cost\",\"Subscription Active\",\"Subscription Vendor\",\"Domain (Required if Category = Domain)\",\"SSL Source (Required if Category = SSL Certificate)\",\"UDF:29682852 Username\",\"UDF:29682853 Password\",\"UDF:29682854 IP Address\",\"UDF:29682861 OS\",\"UDF:29682862 Name\",\"UDF:29682864 Roles\",\"UDF:29682865 WAN IP:\",\"UDF:29682866 LAN IP:\",\"UDF:29682867 Brand\",\"UDF:29682868 SSID\",\"UDF:29682869 Security\",\"UDF:29682870 Location\",\"UDF:29682871 Make & Model\",\"UDF:29682872 Battery Life\",\"UDF:29682873 Version\",\"UDF:29682874 URL\",\"UDF:29682875 Registrar\",\"UDF:29682913 AEM_DeviceID\",\"UDF:29682914 AEM_DeviceUID\",\"UDF:29682915 AEM_Description\",\"UDF:29682916 AEM_Manufacturer\",\"UDF:29682917 AEM_Model\",\"UDF:29682918 AEM_OperatingSystem\",\"UDF:29682919 AEM_IPAddress\",\"UDF:29682920 User-defined field 3\",\"UDF:29682921 User-defined field 2\",\"UDF:29682922 User-defined field 1\",\"UDF:29682923 User-defined field 10\",\"UDF:29682924 User-defined field 7\",\"UDF:29682925 User-defined field 6\",\"UDF:29682926 User-defined field 5\",\"UDF:29682927 User-defined field 4\",\"UDF:29682928 User-defined field 9\",\"UDF:29682929 User-defined field 8\",\"UDF:29682981 User-defined field 19\",\"UDF:29682982 User-defined field 17\",\"UDF:29682983 User-defined field 18\",\"UDF:29682984 User-defined field 15\",\"UDF:29682985 User-defined field 16\",\"UDF:29682986 User-defined field 13\",\"UDF:29682987 User-defined field 14\",\"UDF:29682988 User-defined field 11\",\"UDF:29682989 User-defined field 12\",\"UDF:29682990 User-defined field 21\",\"UDF:29682991 User-defined field 20\",\"UDF:29682992 User-defined field 22\",\"UDF:29682993 User-defined field 23\",\"UDF:29682994 User-defined field 24\",\"UDF:29682995 User-defined field 25\",\"UDF:29682996 User-defined field 26\",\"UDF:29682997 User-defined field 27\",\"UDF:29682998 User-defined field 28\",\"UDF:29682999 User-defined field 29\",\"UDF:29683000 Server Type - PixelPOS\",\"UDF:29683001 User-defined field 30\",\"UDF:29683002 Bit-Locker\",\"UDF:29683003 PixelPoint - Backup\",\"UDF:29683004 Wholesaler\",\"UDF:29683005 Store ID\",\"UDF:29683006 Symbol Group\"");

                    //file.WriteLine("Configuration Item ID[updates only],[required] Product Name,[required] Company,Configuration Item Category,Configuration Item Type,[required] Install Date, Warranty Expiration,Serial Number, Reference Number,Reference Name, Number of Users, Contact, Location, Area, Contract, Service, Service Bundle,Billing Product, Billing Product Effective Date,Billing Product Expiration Date, Vendor, Service Level Agreement, Parent Configuration Item Serial Number, Description, Hourly Cost,Monthly Cost, Daily Cost,Per - Use Cost,Setup Fee, Company Link,Material Code(required if creating product),Active / Inactive,Subscription Name, Reviewed for Contract, Subscription Description, Subscription Period Type[required if creating subscription],Subscription Effective Date[required if creating subscription],Subscription Expiration Date[required if creating subscription],Subscription Period Price[required if creating subscription],Subscription Material Code[required if creating subscription],Subscription Purchase Order Number, Subscription Period Cost, Subscription Active,Subscription Vendor, Domain (Required if Category = Domain),SSL Source(Required if Category = SSL Certificate),UDF: 29682852 Username,UDF: 29682853 Password,UDF: 29682854 IP Address, UDF:29682861 OS,UDF: 29682862 Name,UDF: 29682864 Roles,UDF: 29682865 WAN IP:,UDF: 29682866 LAN IP:,UDF: 29682867 Brand,UDF: 29682868 SSID,UDF: 29682869 Security,UDF: 29682870 Location,UDF: 29682871 Make & Model,UDF: 29682872 Battery Life, UDF:29682873 Version,UDF: 29682874 URL,UDF: 29682875 Registrar,UDF: 29682913 AEM_DeviceID,UDF: 29682914 AEM_DeviceUID,UDF: 29682915 AEM_Description,UDF: 29682916 AEM_Manufacturer,UDF: 29682917 AEM_Model,UDF: 29682918 AEM_OperatingSystem,UDF: 29682919 AEM_IPAddress,UDF: 29682920 User - defined field 3,UDF: 29682921 User - defined field 2,UDF: 29682922 User - defined field 1,UDF: 29682923 User - defined field 10,UDF: 29682924 User - defined field 7,UDF: 29682925 User - defined field 6,UDF: 29682926 User - defined field 5,UDF: 29682927 User - defined field 4,UDF: 29682928 User - defined field 9,UDF: 29682929 User - defined field 8,UDF: 29682981 User - defined field 19,UDF: 29682982 User - defined field 17,UDF: 29682983 User - defined field 18,UDF: 29682984 User - defined field 15,UDF: 29682985 User - defined field 16,UDF: 29682986 User - defined field 13,UDF: 29682987 User - defined field 14,UDF: 29682988 User - defined field 11,UDF: 29682989 User - defined field 12,UDF: 29682990 User - defined field 21,UDF: 29682991 User - defined field 20,UDF: 29682992 User - defined field 22,UDF: 29682993 User - defined field 23,UDF: 29682994 User - defined field 24,UDF: 29682995 User - defined field 25,UDF: 29682996 User - defined field 26,UDF: 29682997 User - defined field 27,UDF: 29682998 User - defined field 28,UDF: 29682999 User - defined field 29,UDF: 29683000 Server Type -PixelPOS,UDF: 29683001 User - defined field 30,UDF: 29683002 Bit - Locker,UDF: 29683003 PixelPoint - Backup,UDF: 29683004 Wholesaler,UDF: 29683005 Store ID, UDF:29683006 Symbol Group");
                    foreach (Items_From_Receipt item in global_list)
                    {
                        if (item.status=="")
                        {
                            string mapped_product_name = item.Product_Name.Trim();
                            if (Mappings.ContainsKey(mapped_product_name))
                            {
                                mapped_product_name = Mappings[mapped_product_name];

                            }

                            file.WriteLine(item.Config_item_ID + ","
                                + "\"" + mapped_product_name + "\"" + ","
                                + "\"" + item.Company_Name + "\"" + ","
                                + ","                   //Configuration Item Category BLANK 
                                + ","                   //Configuration Item Type  BLANK

                                + "\"" + item.Install_Date + "\"" + ","
                                 + "\"" + item.Warranty_Expiration + "\"" + ","

                                + "\"" + item.Serial_Number + "\"" + ","
                                + ","                    //Reference Number BALNK

                                + "\"" + item.Reference_Name + "\"" + ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,"
                                );
                        }
                    }
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    Write_to_current_working_file();
                    int count = global_list.Count();
                    Logwriter.writelog("#FILE EXPORTED:Time,Filename,Item Count,Time Elapsed");
                    Logwriter.writelog("FILE EXPORTED:" + current_timestamp + "," + path + "," + count + "," + elapsedMs);
                }
               
            }
          

            
        }

        private Dictionary<string, string> load_mappings()
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
                            MessageBox.Show("Warning: Check You Mapping Config Form.\n You May Have Duplicate Entries!!", "Mapping Config Form",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return Mappings;
                        }
                    }

                }


            }
            return Mappings;
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
                        + currOb.Warranty_Expiration.ToString() + ","
                        + currOb.Serial_Number.ToString() + ","
                        + currOb.Reference_Name.ToString()+","
                        + currOb.status.ToString()
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

                var watch = System.Diagnostics.Stopwatch.StartNew();

                using (StreamReader sr = new StreamReader(fileToOpen))
                {
                    string line;
                    line = sr.ReadLine();//headertext which needs to be skipped, doing nothing with header

                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Replace("\"", "");
                        string[] wholeLine = line.Split(',');
                        if(wholeLine.Length > 9)
                        {
                            global_list.Add(new Items_From_Receipt()
                            {
                                Config_item_ID = wholeLine[0],
                                Product_Name = wholeLine[1],
                                Company_Name = wholeLine[2],
                                Install_Date = wholeLine[5],
                                Warranty_Expiration =wholeLine[6],
                                Serial_Number = wholeLine[7],
                                Reference_Name = wholeLine[9],
                                status=""
                                

                            });

                        }

                        
                    }
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    int count = global_list.Count();
                    Logwriter.writelog("#IMPORT CSV:Time,Filename,Time lapsed,Count");
                    Logwriter.writelog("IMPORT CSV:"+ current_timestamp+","+ FD.FileName+","+ elapsedMs.ToString()+","+count.ToString());
                }
                dataGridView_products.DataSource = null;
                var source = global_list;
                dataGridView_products.DataSource = source;
                //Process.Start(@"current_working_file.csv");
            }
           
        }
        
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Logwriter.writelog("#LOGOUT:User,Time");
            Logwriter.writelog("LOGOUT:" + Name_of_user + "," + current_timestamp);

        }

        private void button_Config_Click(object sender, EventArgs e)
        {
            Mapping form = new Mapping();
            form.ShowDialog(this);
            
            
        }

        private void button_setall_warrenty_Click(object sender, EventArgs e)
        {
            foreach (Items_From_Receipt currrob in global_list)
            {
                currrob.Warranty_Expiration = textBox_warrrenty.Text;
            }

            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;

            Logwriter.writelog("#SET ALL WARRANTY EXPIRATION DATE:Value ");
            Logwriter.writelog("SET ALL WARRANTY EXPIRATION DATE:" + textBox_warrrenty.Text);
        }

       

        private void textBox_install_date_TextChanged(object sender, EventArgs e)
        {

        }

        private void RunNotepad(string settingsFile, bool bwaitforexit = false)
        {
            var fileToOpen = "notepad.exe";
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = fileToOpen,
                Arguments = settingsFile
            };

            process.Start();
            if (bwaitforexit)
            {
                process.WaitForExit();

            }

        }

        private void button_log_Click(object sender, EventArgs e)
        {
            RunNotepad(Logwriter.logfile);
        }

        private void button_setall_reference_Click(object sender, EventArgs e)
        {
            foreach (Items_From_Receipt currrob in global_list)
            {
                currrob.Reference_Name = textBox_reference_name.Text;
            }

            dataGridView_products.DataSource = null;////nblIAM RESET DATASOUCE!!!!!!!
            var source = global_list;
            dataGridView_products.DataSource = source;

            Logwriter.writelog("#SET ALL REFERENCE NAME DATE:Date,Value ");
            Logwriter.writelog("SET ALL REFERENCE NAME DATE:"+ current_timestamp+","+ textBox_reference_name.Text);
        }
    }
}
