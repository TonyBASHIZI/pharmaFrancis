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
using ProjetBoutique.reports;
using DevExpress.XtraReports.UI;

namespace ProjetBoutique.forms.pages
{
    public partial class UsAlerte : UserControl
    {
        glossaire glos = new glossaire();
        
        public UsAlerte()
        {
            InitializeComponent();
        }

        private void UsAlerte_Load(object sender, EventArgs e)
        {
            glos.GetDataTableFilter(gridControl1, "code_art,designation,nbpieces", "article");
            //glos.GetDatas(gridControl1, "*", "article");
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
    }
}
