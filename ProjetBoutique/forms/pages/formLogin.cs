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
    public partial class formLogin : Form
    {
        glossaire glos = new glossaire();
        public formLogin()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                if (glossaire.Instance.Login(textBox1.Text, textBox2.Text))
                {
                    clsConnecter c = new clsConnecter();
                    c.Ref_agent = textBox1.Text;
                    c.Statut = 1;
                    glos.insertConne(c);
                    this.Hide();
                    layout.formMain main = new layout.formMain(textBox1.Text);
                    main.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Remplissez tous les champs svp!!");
            }
          
            
        }

        private void formLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            
        }

        private void formLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
