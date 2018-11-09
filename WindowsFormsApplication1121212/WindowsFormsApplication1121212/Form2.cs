using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1121212
{
    public partial class Form2 : Form
    {
        private List<string [] > datas = new List<string []>();

        public Form2(List<string []> datas, List<string> headers)
        {
            this.datas = datas;
            InitializeComponent();

            List<ColumnHeader> columnHeaders = new List<ColumnHeader>();
            foreach (string hh in headers)
            {

                ColumnHeader columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                columnHeader0.Text = hh;
                columnHeader0.Width = 125;

                columnHeaders.Add(columnHeader0);
            }
            this.listView1.Columns.AddRange(columnHeaders.ToArray());

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            foreach (string [] name in datas) {

                ListViewItem lvi = new ListViewItem(name);
                listView1.Items.Add(lvi);
            }
            
        }
    }
}
