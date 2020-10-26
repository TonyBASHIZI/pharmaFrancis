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

namespace ProjetBoutique.forms.pages
{
    public partial class fromPaieCredit : Form
    {
        public fromPaieCredit()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(lbID.Text =="0")
            {
                MessageBox.Show("Vous devez selectionner un credit dans la liste");
                
             }else{
                 if (int.Parse(txtpaie.Text) > int.Parse(lbmontant.Text))
                 {

                     MessageBox.Show("Le montant a payé ne peut pas depasser le montant de credit");
                 }
                 else
                 {

                     //mofidication mvt 
                     clsmvt mvt = new clsmvt();

                     int montantavant = int.Parse(lbmontant.Text);
                     int montantapres = montantavant - (int.Parse(txtpaie.Text));
                     mvt.VoirBoss = "" + montantapres;
                     mvt.Id = int.Parse(lbID.Text);
                     glossaire.Instance.editCreditBoss(mvt);
                     lbdesign.Text = "";
                     lbdate.Text = "";
                     lbID.Text = "0";
                     lbmontant.Text = "";
                     txtpaie.Text = "";
                     glossaire.Instance.GetDataTableBoss(gridControl1);
            }

            }
        }

        private void txtpaie_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtpaie.Text.Length; i++)
            {
                if (!char.IsDigit(txtpaie.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres!!");
                    txtpaie.Text = "";
                }
                else
                {

                }
            }
        }

        private void fromPaieCredit_Load(object sender, EventArgs e)
        {
            glossaire.Instance.GetDataTableBoss(gridControl1);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            lbdesign.Text = gridView1.GetFocusedRowCellValue("DESIGNATION").ToString();
            lbdate.Text = gridView1.GetFocusedRowCellValue("DATE").ToString();
            lbID.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            lbmontant.Text = gridView1.GetFocusedRowCellValue("CREDIT_BOSS").ToString();
        }
    }
}
