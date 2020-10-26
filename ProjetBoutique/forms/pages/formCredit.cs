using DevExpress.XtraReports.UI;
using ProjetBoutique.classes;
using ProjetBoutique.reports;
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
    public partial class formCredit : Form
    {
        glossaire glos = new glossaire();
        public formCredit()
        {
            InitializeComponent();
        }

        private void formCredit_Load(object sender, EventArgs e)
        {
            glos.GetDataTableCdredit(gridControl1);
            glos.GetDataTableBoss(gridControl2);
            lbBoss.Text = glos.getBoss();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                ficheCreditBoss j = new ficheCreditBoss();
                j.DataSource = glossaire.Instance.sortieCreditBoss();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ficheCreditClient j = new ficheCreditClient();
                j.DataSource = glossaire.Instance.sortieCreditClient();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                ficheCreditBoss j = new ficheCreditBoss();
                j.DataSource = glossaire.Instance.sortieCreditBossIntervalle(dateTimePicker1.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            fromPaieCredit cre = new fromPaieCredit();
            cre.ShowDialog();
        }
    }
}
