using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetBoutique.classes;
using ProjetBoutique.classes.models;


namespace ProjetBoutique.forms.pages
{
    public partial class formClients : Form
    {
        glossaire glos = new glossaire();
        clsclient cl = new clsclient();
        public formClients()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs svp!!");
            }
            else
            {
                cl.Codecl = textBox1.Text;
                cl.Noms = textBox2.Text;
                cl.Prenom = textBox3.Text;
                cl.Tel = textBox4.Text;
                cl.Adresse = textBox5.Text;
                glos.insertClient(cl);
                glos.GetDatas(gridControl1, "*", "client_fidele");
            }
        }
        private string getCodeCl()
        {
            Random rd = new Random();
            int x = rd.Next(1, 10000);
            string code = "CL"+x;

            return code;

        }
        private void formClients_Load(object sender, EventArgs e)
        {
            textBox1.Text = "" + getCodeCl();
            glos.GetDatas(gridControl1, "*", "client_fidele");
        }
    }
}
