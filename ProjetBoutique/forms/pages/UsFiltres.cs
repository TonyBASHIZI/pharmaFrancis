using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetBoutique.reports;
using ProjetBoutique.classes;
using DevExpress.XtraReports.UI;

namespace ProjetBoutique.forms.pages
{
    public partial class UsFiltres : UserControl
    {
        glossaire glos = new glossaire();
        public UsFiltres()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                fichemvt j = new fichemvt();
                j.DataSource = glossaire.Instance.sortieEtatIntervalle(date1.Text, date2.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UsFiltres_Load(object sender, EventArgs e)
        {

        }
    }
}
