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

            this.listView1 = new System.Windows.Forms.ListView();

            List<ColumnHeader> columnHeaders = new List<ColumnHeader>();
            foreach (string hh in headers)
            {

            ColumnHeader columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader0.Text = hh;
            columnHeader0.Width = 125;

            columnHeaders.Add(columnHeader0);
            
            }

            this.SuspendLayout();

            this.listView1.Columns.AddRange(columnHeaders.ToArray());


            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(860, 635);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;

            this.Controls.Add(this.listView1);

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
