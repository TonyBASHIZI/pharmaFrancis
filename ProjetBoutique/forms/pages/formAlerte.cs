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
    public partial class formAlerte : Form
    {
        glossaire glos = new glossaire();
        public formAlerte()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                etatBesoinStock j = new etatBesoinStock();
                j.DataSource = glossaire.Instance.sortieAlerteStock();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void formAlerte_Load(object sender, EventArgs e)
        {
            glos.GetDataTableFilter(gridControl1, "code_art,designation,nbpieces", "article");
        }
    }
}
