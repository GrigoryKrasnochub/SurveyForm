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

        public Form2(List<string []> datas)
        {
            this.datas = datas;
            InitializeComponent();
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
