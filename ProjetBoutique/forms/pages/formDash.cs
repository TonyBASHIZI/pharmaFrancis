using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetBoutique.forms.pages
{
    public partial class formDash : Form
    {
        classes.glossaire glos = new classes.glossaire();
        string imglocation = "";
        DataTable dbdataset;
        public formDash()
        {
            InitializeComponent();
        }

        private void formDash_Load(object sender, EventArgs e)
        {
            glos.getDash(label7, label10, label15);
            //glos.getAlerteArt(lbalertArt);
            label16.Text = glos.getAdmin();
            label6.Text = glos.getAlerteArt();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            glos.getDash(label7, label10, label15);
            //glos.getAlerteArt(lbalertArt);
            label16.Text = glos.getAdmin();
            label6.Text = glos.getAlerteArt();
        }
    }
}
