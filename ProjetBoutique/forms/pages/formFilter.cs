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
    public partial class formFilter : Form
    {
        public formFilter()
        {
            InitializeComponent();
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
    }
}
