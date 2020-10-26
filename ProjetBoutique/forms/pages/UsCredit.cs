using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetBoutique.classes;

namespace ProjetBoutique.forms.pages
{
    public partial class UsCredit : UserControl
    {
        glossaire glos = new glossaire();
        public UsCredit()
        {
            InitializeComponent();
        }

        private void UsCredit_Load(object sender, EventArgs e)
        {
            glos.GetDataTableCdredit(gridControl1);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
