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
using ProjetBoutique.reports;
using DevExpress.XtraReports.UI;

namespace ProjetBoutique.forms.pages
{
    public partial class formUsers : Form
    {
        glossaire glos = new glossaire();
        clsutilisateurs us = new clsutilisateurs();
        string users = "";
        public formUsers(string user)
        {
            InitializeComponent();
            users = user;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(txtnom.Text !="" || txtpasse.Text != "" || txttel.Text != "" || txtmotsecret.Text != "")
            {
                if (users == "0992974813")
                {
                    us.Noms = txtnom.Text.ToUpper();
                    us.Mot_de_pasee = txtpasse.Text;
                    us.Mot_secret = txtmotsecret.Text;
                    us.Tel = txttel.Text;
                    glos.insertUser(us);
                    glos.GetDatas(gridControl1, "nom,tel", "utilisateurs");
                    txttel.Text = "";
                    txtnom.Text = "";
                    txtmotsecret.Text = "";
                    txtpasse.Text = "";
                }
                else
                {
                    MessageBox.Show("Vous avez pas de niveau suffisant pour passer cette operation\n contactez Administrateur");
                }
                
            }
        }

        private void formUsers_Load(object sender, EventArgs e)
        {
            glos.GetDatas(gridControl1, "nom,tel", "utilisateurs");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (txtnom.Text != "" || txtpasse.Text != "" || txttel.Text != "" || txtmotsecret.Text != "")
            {
                us.Tel = txttel.Text;
                glos.suprimerUser(us);
                glos.GetDatas(gridControl1, "nom,tel", "utilisateurs");
                txttel.Text = "";
                txtnom.Text = "";
                txtmotsecret.Text = "";
                txtpasse.Text = "";
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                txtnom.Text = gridView1.GetFocusedRowCellValue("nom").ToString();
                txttel.Text = gridView1.GetFocusedRowCellValue("tel").ToString();
               

            }catch(Exception ex)
            {

            }
            
        }

        private void txtmotsecret_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (txtnom.Text != "" || txtpasse.Text != "" || txttel.Text != "" || txtmotsecret.Text != "")
            {
                us.Noms = txtnom.Text.ToUpper();
                us.Mot_de_pasee = txtpasse.Text;
                us.Mot_secret = txtmotsecret.Text;
                us.Tel = txttel.Text;
                glos.modifierUsers(us);
                glos.GetDatas(gridControl1, "nom,tel", "utilisateurs");
                txttel.Text = "";
                txtnom.Text = "";
                txtmotsecret.Text = "";
                txtpasse.Text = "";
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Utilisateurs j = new Utilisateurs();
                j.DataSource = glossaire.Instance.sortieUsers();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
