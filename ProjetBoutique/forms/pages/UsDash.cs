using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO;

namespace ProjetBoutique.forms.pages
{
    public partial class UsDash : UserControl
    {
        classes.glossaire glos = new classes.glossaire();
        string imglocation = "";
        DataTable dbdataset;
        public UsDash()
        {
            InitializeComponent();
        }
        
        private void UsDash_Load(object sender, EventArgs e)
        {
           
                glos.getDash(lbArt, lbVente, lbCl);
               // glos.getAlerteArt(lbalertArt);
                label26.Text = glos.getAdmin();
                label6.Text = glos.getAlerteArt();
                
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glos.getDash(lbArt, lbVente, lbCl);
            //glos.getAlerteArt(lbalertArt);
            label26.Text = glos.getAdmin();
            label6.Text = glos.getAlerteArt();
        }
    }
}
