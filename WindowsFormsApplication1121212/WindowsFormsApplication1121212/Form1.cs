using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1121212
{
    public partial class Form1 : Form
    {
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
    (name, surname, birth_surname, date, birth_place, birth_country, nationality, nationality_birth, gender, family_status, representative_child, passport_id, travel_category, travel_id, travel_id_date_recieve, travel_id_date_expires, travel_id_source, home_adress_and_email, phone_number, is_current_country, residence_permit_id, residence_permit_expires, profession, employer_info_or_school_info, visiting_purpose, destination_country, first_country_admission, number_of_entries_requested, transit_duration, have_shengen_before, date_shengen_start, date_shengen_expired, did_you_have_fingerprints, fingerprints_date, entry_permission_issued, entry_permission_date_started, entry_permission_date_expired, entry_date, departure_date, invitationalar_info, invitationalar_email_and_address, invitationalar_phone, invitationalar_company_info, invitationalar_company_email_and_address, invitationalar_company_phone, expenses, expenses_details, family_surname, family_members_names, birth_date, citizenship, passport_id_again, relationship_with_ES_citizen, datetime, sign_children, sign)

    VALUES('{textBox3.Text}', '{textBox1.Text}', '{textBox2.Text}', '{textBox4.Text}', '{textBox5.Text}',
'{textBox6.Text}', '{textBox7.Text}', '{textBox8.Text}', '{getSex()}', '{getFamily_status()}', '{textBox9.Text}', 
'{textBox10.Text}', '{getPassportID()}', '', NOW(), NOW(), '', '', '', 0, 0, NOW(), '', '', '', '', '', 0, '', 0, NOW(), NOW(), 0, NOW(), '', NOW(), NOW(), NOW(), NOW(), '', '', '', '', '', '', '', '', '', '', NOW(), '', '', '', NOW(), '', '')";

            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doInsert();
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
            return "NOSEX";
        } 

        public string getFamily_status ()
        {
            string status = "";
            foreach (CheckBox rad in groupBox2.Controls.OfType<CheckBox>())
            {

                if (rad.Checked)
                {
                    status += rad.Text;
                }
            }

            return status;
        }

        public string getPassportID()
        {
            string result = "";

            foreach (CheckBox rad in groupBox3.Controls.OfType<CheckBox>())
            {

                if (rad.Checked)
                {
                    result += rad.Text;
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
    }
}
