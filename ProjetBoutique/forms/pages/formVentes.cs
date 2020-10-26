using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetBoutique.classes;
using ProjetBoutique.reports;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;

namespace ProjetBoutique.forms.pages
{
    public partial class formVentes : DevExpress.XtraEditors.XtraForm
    {
        glossaire glos = new glossaire();
        string typemvt = "";
        string typepair = "";
        public formVentes()
        {
            InitializeComponent();
        }
        public formVentes(string tel)
        {
            InitializeComponent();
            label17.Text = tel;
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void formVentes_Load(object sender, EventArgs e)
        {
            glos.GetDatas(gridControl1, "code_art,designation,nbpieces,fss,provenance,prix_u,prix_gros", "article");
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            lbcode.Text = gridView1.GetFocusedRowCellValue("code_art").ToString();
            lbdesign.Text = gridView1.GetFocusedRowCellValue("designation").ToString();
            lbprixd.Text = gridView1.GetFocusedRowCellValue("prix_u").ToString();
            lbprixg.Text = gridView1.GetFocusedRowCellValue("prix_gros").ToString();
            lbqte.Text = gridView1.GetFocusedRowCellValue("nbpieces").ToString();

        }

       

        private void txtnbpiece_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtnbpiece.Text.Length; i++)
            {
                if (!char.IsDigit(txtnbpiece.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres!!");
                    txtnbpiece.Text = "";
                }
                else
                {

                }
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cbTypePaie.Text == "Liquide")
            {
                typepair = "Liquide";
                txtCreditBoss.Text = "";
            }
            else if (cbTypePaie.Text == "Credit")
            {
                typepair = "Credit";
                txtCreditBoss.Text = "";
            }
            else if (cbTypePaie.Text == "Liquide avec credit Boss")
            {
                typepair = "Liquide avec credit Boss";
            }
            if (comboBox1.Text == "En gros")
            {
                typemvt = "En gros";
                lbtotal.Text = "" + float.Parse(txtnbpiece.Text) * float.Parse(lbprixg.Text);
            }
            else if (comboBox1.Text == "Detail")
            {
                typemvt = "Detail";
                lbtotal.Text = "" + float.Parse(txtnbpiece.Text) * float.Parse(lbprixd.Text);
            }
            else
            {
                MessageBox.Show("Completer tous les champs!!");
            }
            
           if(int.Parse(lbqte.Text) < int.Parse(txtnbpiece.Text) || cbTypePaie.Text == ""){

               MessageBox.Show("Impossible de continuer cette operation \n stock insuffisant pour cet article!! \n verifier type de paiement \n ou type de mouvement");

           }else{

               if(lbcode.Text =="" || lbdesign.Text == "" || lbprixd.Text == "" || lbprixd.Text == "" || lbprixg.Text == "" || lbtotal.Text == "" || txtnbpiece.Text =="")
               {

                   MessageBox.Show("Operation impossible les champs sont vides");

               }else{

                    clsmvt mvt = new clsmvt();
                    mvt.Ref_art = lbcode.Text;
                    mvt.Ref_cl = txtcodecl.Text;
                    mvt.Lblmvt = lbdesign.Text;
                    mvt.Nbpieces = txtnbpiece.Text;
                    mvt.Prix_gro = lbprixg.Text;
                    mvt.Prix_U = lbprixd.Text;
                    mvt.Totalpaie = lbtotal.Text;
                    mvt.Typemvt = typemvt;
                    mvt.Typepaie = typepair;
                    mvt.Ref_agent = label17.Text;
                    mvt.VoirBoss = txtCreditBoss.Text;
                   
                    if (glos.InsertMvt(mvt) == true)
                    {
                        try
                        {
                            facture j = new facture();
                            j.DataSource = glossaire.Instance.sortieFacture(mvt.Ref_art);
                            ReportPrintTool printTool = new ReportPrintTool(j);
                            printTool.ShowPreviewDialog();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    glos.GetDatas(gridControl1, "code_art,designation,nbpieces,fss,provenance,prix_u,prix_gros", "article");
                    lbtotal.Text = "00.0";
                    lbcode.Text = "";
                    lbdesign.Text = "";
                    lbprixd.Text = "";
                    lbprixg.Text = "";
                    lbqte.Text = "";
                    txtnbpiece.Text = "";
                    txtcodecl.Enabled = true;

               }
           
           }
            
  
        }

      

      

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "En gros")
            {
                

                typemvt = "En gros";
                lbtotal.Text = "" + float.Parse(txtnbpiece.Text) * float.Parse(lbprixg.Text);
            }
            else if (comboBox1.Text == "Detail")
            {
                typemvt = "Detail";
                lbtotal.Text = "" + float.Parse(txtnbpiece.Text) * float.Parse(lbprixd.Text);
            }
            else
            {
                MessageBox.Show("Completer tous les champs!!");
            }

            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            { 
                ficheDesventes j = new ficheDesventes();
                j.DataSource = glossaire.Instance.sortieVentes();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbTypePaie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbTypePaie.Text =="Liquide avec credit Boss")
            {
                txtCreditBoss.Enabled = true;
                
            }
            else if (cbTypePaie.Text == "Liquide")
            {
                txtCreditBoss.Enabled = false;
            }
            else if (cbTypePaie.Text == "Credit")
            {
                txtCreditBoss.Enabled = false;
                txtcodecl.Enabled = true;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            fromPaieCredit cre = new fromPaieCredit();
            cre.ShowDialog();
        }
    }
}