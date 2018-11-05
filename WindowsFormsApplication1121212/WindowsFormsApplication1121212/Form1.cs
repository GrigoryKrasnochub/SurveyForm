using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1121212
{
    /*
     *Дорогой читатель, Помни! 
     * Везде кроме пунка 33 все что касается выборов в групбоксах разделяется ";"
     * Пункт 33 это ад на земле, с ним разбирайтесь из кода, скорее всего, пока я его писал , я умер.
     * А Коммит сделал мой сосед, это было мое последнее желание.
     * Так что удачи!
     */
    public partial class Form1 : Form
    {
        bool correctDate = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void doInsert()
        {
            string connectionStr = "server=localhost;user=root;database=university_lab;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionStr);
            conn.Open();

            string query = $@"INSERT INTO Forms
    (name, surname, birth_surname, date, birth_place, birth_country, nationality, nationality_birth, gender, family_status, representative_child, passport_id, travel_category, travel_id, travel_id_date_recieve, travel_id_date_expires, travel_id_source, home_adress_and_email, phone_number, is_current_country, residence_permit_id, residence_permit_expires, profession, employer_info_or_school_info, visiting_purpose, destination_country, first_country_admission, number_of_entries_requested, transit_duration, have_shengen_before, date_shengen_start, date_shengen_expired, did_you_have_fingerprints, fingerprints_date, entry_permission_issued, entry_permission_date_started, entry_permission_date_expired, entry_date, departure_date, invitationalar_info, invitationalar_email_and_address, invitationalar_phone, invitationalar_company_info, invitationalar_company_email_and_address, invitationalar_company_phone, expenses, expenses_details, family_surname, family_members_names, birth_date, citizenship, passport_id_again, relationship_with_ES_citizen, datetime, sign_children, datetime2, sign_children_again, datetime3, sign)

    VALUES('{textBox3.Text}', '{textBox1.Text}', '{textBox2.Text}', '{textBox4.Text}', '{textBox5.Text}',
'{textBox6.Text}', '{textBox7.Text}', '{textBox8.Text}', '{getSex()}', '{getFamily_status()}', '{textBox9.Text}', 
'{textBox10.Text}', '{getPassportID()}', '{textBox14.Text}', '{DateFormater(textBox15.Text)}', '{DateFormater(textBox16.Text)}', '{textBox17.Text}', 
'{textBox19.Text}', '{textBox18.Text}', '{GetIsCurrentCountry()}', '{textBox11.Text}','{DateFormater(textBox12.Text)}',
'{textBox13.Text}', '{textBox20.Text}', '{GetVisitingPurpose()}', '{textBox22.Text}', '{textBox23.Text}',
'{GetInfoFromCheckBox(groupBox6.Controls.OfType<CheckBox>())}', '{textBox21.Text}',
'{GetInfoFromRadioButton(groupBox7.Controls.OfType<CheckBox>())}', '{DateFormater(textBox24.Text)}', '{DateFormater(textBox25.Text)}',
'{GetInfoFromRadioButton(groupBox8.Controls.OfType<CheckBox>())}', '{DateFormater(textBox26.Text)}', '{textBox27.Text}', '{DateFormater(textBox28.Text)}', '{DateFormater(textBox29.Text)}',

'{DateFormater(textBox30.Text)}', '{DateFormater(textBox31.Text)}', '{textBox32.Text}', '{textBox33.Text}', '{textBox34.Text}', '{textBox35.Text}', '{textBox36.Text}',
'{textBox37.Text}', '{textBox38.Text}', '{GetExpensesDetails()}',
'{textBox41.Text}', '{textBox42.Text}', '{DateFormater(textBox43.Text)}', '{textBox44.Text}', '{textBox45.Text}',
'{GetInfoFromRadioButton(groupBox13.Controls.OfType<RadioButton>())}', '{textBox46.Text}', '{textBox47.Text}', '{textBox48.Text}', '{textBox51.Text}','{textBox49.Text}', '{textBox50.Text}')";

            if (correctDate) {
                return;
            }
            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           doInsert();
           correctDate = false;
        }
        public string GetExpensesDetails ()
        {
            string result = "";

            result = GetInfoFromRadioButton(groupBox9.Controls.OfType<RadioButton>());
            //if (result.Contains("Сам заявитель")) { return result; }
            result += GetInfoFromCheckBox(groupBox10.Controls.OfType<CheckBox>());
            if (!result.Contains("Иные")) { return result; }
            //Разделитель "Средства" "||"
            result += "||";
            result += GetInfoFromCheckBox(groupBox11.Controls.OfType<CheckBox>());
            result += textBox39+";";
            //Разделитель "Средства на проживание" "||"
            result += "||";
            result += GetInfoFromCheckBox(groupBox12.Controls.OfType<CheckBox>());
            result += textBox40;
            Console.WriteLine(result);
            return result;
        }
        public string getSex()
        {
            
            foreach (RadioButton rad in groupBox1.Controls.OfType<RadioButton>())
            {
                if (rad.Checked)
                {
                    return rad.Text;
                }
            }
            return "";
        }

        public string GetVisitingPurpose()
        {
            string status = "";
            foreach (CheckBox rad in groupBox2.Controls.OfType<CheckBox>())
            {

                if (rad.Checked)
                {
                    status += rad.Text + ";";
                }
            }

            return status;
        }
        
        public string DateFormater( string str)
        {
            DateTime result;
            string str_res = "";

            try
            {
                result = DateTime.ParseExact(str, "dd.MM.yyyy", CultureInfo.GetCultureInfo("ru-RU"));
                str_res= String.Format("{0:yyyy-MM-dd}", result);

            }
            catch (Exception e)
            {
                if (!correctDate)
                {
                    MessageBox.Show("Дата должна быть задана в формате дд.мм.гггг");
                }
                correctDate = true;

                
            }


            return str_res;
        }

        public string getFamily_status ()
        {
            string status = "";
            foreach (CheckBox rad in groupBox2.Controls.OfType<CheckBox>())
            {

                if (rad.Checked)
                {
                    status += rad.Text+";";
                }
            }

            return status;
        }

        public string GetInfoFromCheckBox (System.Collections.IEnumerable checkBoxes)
        {
            string status = "";
            foreach (CheckBox rad in checkBoxes)
            {

                if (rad.Checked)
                {
                    status += rad.Text + ";";
                }
            }
            return status;
        }

        public string GetInfoFromRadioButton (System.Collections.IEnumerable checkBoxes)
        {
            string status = "";
            foreach (RadioButton rad in checkBoxes)
            {

                if (rad.Checked)
                {
                    status += rad.Text + ";";
                }
            }
            return status;
        }

        public string GetIsCurrentCountry ()
        {

            string result = "";

            foreach (RadioButton rad in groupBox4.Controls.OfType<RadioButton>())
            {

                if (rad.Checked)
                {
                    result += rad.Text + ";";
                }
            }

            return result;

        }
        public string getPassportID()
        {
            string result = "";

            foreach (CheckBox rad in groupBox3.Controls.OfType<CheckBox>())
            {

                if (rad.Checked)
                {
                    result += rad.Text+";";
                }
            }

            return result;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
