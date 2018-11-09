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
    public partial class Form1 : Form
    {
        bool correctDate = false;
        //Костыли костылики 
        //Даты для пунктов 26, 27
        String ShengenStart = "";
        String ShengenEnd = "";
        String FingersPringDate = "";
        //Костыли костылики все
        public Form1()
        {
            InitializeComponent();
        }

        private void doInsert()
        {
            string connectionStr = "server=localhost;user=root;database=university_lab;CharSet=utf8;password=root;";
            MySqlConnection conn = new MySqlConnection(connectionStr);
            conn.Open();

            string query = $@"INSERT INTO Forms
    (name, surname, birth_surname, date, birth_place, birth_country, nationality, nationality_birth, gender, family_status, representative_child, passport_id, travel_category, travel_id, travel_id_date_recieve, travel_id_date_expires, travel_id_source, home_adress_and_email, phone_number, is_current_country, residence_permit_id, residence_permit_expires, profession, employer_info_or_school_info, visiting_purpose, destination_country, first_country_admission, number_of_entries_requested, transit_duration, have_shengen_before, date_shengen_start, date_shengen_expired, did_you_have_fingerprints, fingerprints_date, entry_permission_issued, entry_permission_date_started, entry_permission_date_expired, entry_date, departure_date, invitationalar_info, invitationalar_email_and_address, invitationalar_phone, invitationalar_company_info, invitationalar_company_email_and_address, invitationalar_company_phone, expenses, expenses_details, family_surname, family_members_names, birth_date, citizenship, passport_id_again, relationship_with_ES_citizen, datetime, sign_children, datetime2, sign_children_again, datetime3, sign)

    VALUES('{textBox3.Text}', '{textBox1.Text}', '{textBox2.Text}', '{textBox4.Text}', '{textBox5.Text}',
'{textBox6.Text}', '{textBox7.Text}', '{textBox8.Text}', '{GetInfoFromRadioButton(groupBox1.Controls.OfType<RadioButton>())}', '{GetInfoFromRadioButton(groupBox2.Controls.OfType<RadioButton>())}', '{textBox9.Text}', 
'{textBox10.Text}', '{GetInfoFromRadioButton(groupBox3.Controls.OfType<RadioButton>())}', '{textBox14.Text}', '{DateFormat(textBox15.Text)}', '{DateFormat(textBox16.Text)}', '{textBox17.Text}', 
'{textBox19.Text}', '{textBox18.Text}', '{GetInfoFromRadioButton(groupBox4.Controls.OfType<RadioButton>())}', '{textBox11.Text}','{DateFormat(textBox12.Text)}',
'{textBox13.Text}', '{textBox20.Text}', '{GetInfoFromRadioButton(groupBox5.Controls.OfType<RadioButton>())}', '{textBox22.Text}', '{textBox23.Text}',
'{GetInfoFromRadioButton(groupBox6.Controls.OfType<RadioButton>())}', '{textBox21.Text}',
'{GetShengenInfo()}', '{ShengenStart}', '{ShengenEnd}',
'{GetFingersPrint()}', '{FingersPringDate}', '{textBox27.Text}', '{DateFormat(textBox28.Text)}', '{DateFormat(textBox29.Text)}',

'{DateFormat(textBox30.Text)}', '{DateFormat(textBox31.Text)}', '{textBox32.Text}', '{textBox33.Text}', '{textBox34.Text}', '{textBox35.Text}', '{textBox36.Text}',
'{textBox37.Text}', '{textBox38.Text}', '{GetExpensesDetails()}',
'{textBox41.Text}', '{textBox42.Text}', '{DateFormat(textBox43.Text)}', '{textBox44.Text}', '{textBox45.Text}',
'{GetInfoFromRadioButton(groupBox13.Controls.OfType<RadioButton>())}', '{textBox46.Text}', '{textBox47.Text}', '{textBox48.Text}', '{textBox51.Text}','{textBox49.Text}', '{textBox50.Text}')";

            if (correctDate) {
                return;
            }

            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        private List<string [] > doSelect(string surname)
        {
            string connectionStr = "server=localhost;user=root;database=university_lab;CharSet=utf8;password=root;Convert Zero Datetime=True;Allow Zero Datetime=True;";

            string query = $@"SELECT * FROM Forms WHERE surname='{surname}'";

            List<string []> resultList = new List<string []>();
            MySqlConnection conn = new MySqlConnection(connectionStr);
            MySqlCommand myCommand = new MySqlCommand(query, conn);
            conn.Open();

            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();

            while (MyDataReader.Read())
            {
                string[] str = new string[60];
                for (int i = 0; i < str.Length; i++)
                //for (int i = 0; i < 14; i++)
                {
                    try
                    {
                        str[i] = MyDataReader.GetString(i);
                    }
                    catch (MySql.Data.Types.MySqlConversionException)
                    {
                        str[i] = "";
                    }
                }
                resultList.Add(str); //Получаем строку
            }
            MyDataReader.Close();

            conn.Close();
            return resultList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           doInsert();
           correctDate = false;
        }
        public string GetExpensesDetails()
        {
            string result = "";

            result = GetInfoFromRadioButton(groupBox9.Controls.OfType<RadioButton>());
            if (result.Contains("Сам заявитель")) {
                result += "||";
                result += GetInfoFromCheckBox(groupBox11.Controls.OfType<CheckBox>());
                if (!result.Contains("Иные")) { return result; }
                result += textBox39.Text + ";";
            }
            else
            { 
            result += GetInfoFromCheckBox(groupBox10.Controls.OfType<CheckBox>());
            if (!result.Contains("Иные")) { return result; }
            result += "||";
                string str = GetInfoFromCheckBox(groupBox12.Controls.OfType<CheckBox>());
                result += str;
                if (!str.Contains("Иные")) { return result; }
                result += textBox40.Text;
            }
            Console.WriteLine(result);
            return result;
        }
       
        public string DateFormat( string str)
        {
            DateTime result;
            string str_res = "";

            try
            {
                result = DateTime.ParseExact(str, "dd.MM.yyyy", CultureInfo.GetCultureInfo("ru-RU"));
                str_res = String.Format("{0:yyyy-MM-dd}", result);

            }
            catch (Exception e)
            {
                if (!correctDate)
                {
                    MessageBox.Show("Измените формат даты на дд.мм.гггг");
                }
                correctDate = true;  
            }

            return str_res;
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
        private string GetShengenInfo ()
        {
            string status = "";
            foreach (RadioButton rad in groupBox7.Controls.OfType<RadioButton>())
            {

                if (rad.Checked)
                {
                    status += rad.Text;
                }
            }
            if (status.Contains("Нет")) { return status; }
            ShengenStart = DateFormat(textBox24.Text);
            ShengenEnd = DateFormat(textBox25.Text);
            return status;
        }

        private string GetFingersPrint()
        {
            string status = "";
            foreach (RadioButton rad in groupBox8.Controls.OfType<RadioButton>())
            {

                if (rad.Checked)
                {
                    status += rad.Text;
                }
            }
            if (status.Contains("Нет")) { return status; }
            FingersPringDate = DateFormat(textBox26.Text);
            return status;
        }
        public string GetInfoFromRadioButton (System.Collections.IEnumerable radioButton)
        {
            string status = "";
            foreach (RadioButton rad in radioButton)
            {

                if (rad.Checked)
                {
                    status += rad.Text;
                }
            }
            return status;
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberCkecking(e);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberCkecking(e);
        }

        private void NumberCkecking(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
        private List<string> getHeaders()
        {
            string connectionStr = "server=localhost;user=root;database=university_lab;CharSet=utf8;password=root;";

            string query = $@"select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='forms'";

            List<string> resultList = new List<string>();
            MySqlConnection conn = new MySqlConnection(connectionStr);
            MySqlCommand myCommand = new MySqlCommand(query, conn);
            conn.Open();

            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();

            while (MyDataReader.Read())
            {
                resultList.Add(MyDataReader.GetString(0)); //Получаем строку 
            }
            MyDataReader.Close();

            conn.Close();
            return resultList;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form2 form = new Form2(doSelect(textBox1.Text), getHeaders());
            form.ShowDialog();

        }
    }
}
