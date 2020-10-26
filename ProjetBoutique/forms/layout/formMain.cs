using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ProjetBoutique.forms.pages;
using ProjetBoutique.reports;
using ProjetBoutique.classes;
using DevExpress.XtraReports.UI;

namespace ProjetBoutique.forms.layout
{
    public partial class formMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private static formMain main;
        private UserControl uc = null;
        private Form form = null;
        glossaire glos = new glossaire();

        public static formMain Instance
        {
            get
            {
                if (main == null)
                {
                    main = new formMain();
                }

                return main;
            }

            set
            {
                value = main;
            }
        }

        public formMain()
        {
            InitializeComponent();
        }
        public formMain(string user)
        {
            InitializeComponent();
            string users = user;
            lbTel.Text = users;
        }
        private void formMain_Load(object sender, EventArgs e)
        {
            formSplash da = new formSplash();
            da.MdiParent = this;
            da.Show();

            //LoadUserControles(da);
        }
        //private void LoadUserControles(UserControl uc)
        //{
        //    Cursor = Cursors.WaitCursor;
        //    PnlMain.Controls.Clear();
        //    uc.Dock = DockStyle.Fill;
        //    PnlMain.Controls.Add(uc);
        //    uc.Show();

        //    if (uc.Visible == true)
        //    {
        //        Cursor = Cursors.Default;
        //    }
        //}

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            //UsStock st = new UsStock(lbTel.Text);
            //LoadUserControles(st);

            formArticle p = new formArticle(lbTel.Text);
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            formVentes v = new formVentes(lbTel.Text);
            v.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            formCredit cr = new formCredit();
            cr.MdiParent = this;
            cr.Show();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            formClients cl = new formClients();
            cl.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            formDash da = new formDash();
            da.MdiParent = this;
            da.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            formAlerte da = new formAlerte();
            da.MdiParent = this;
            da.Show();
            
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            formUsers user = new formUsers(lbTel.Text);
            user.ShowDialog();
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            formFilter da = new formFilter();
            da.MdiParent = this;
            da.Show();
            

        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                EtatStock j = new EtatStock();
                j.DataSource = glossaire.Instance.sortieEtatFinal();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
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

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                EtatStock j = new EtatStock();
                j.DataSource = glossaire.Instance.sortieEtatFinal();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            formSauvegardeDB sau = new formSauvegardeDB();
            sau.ShowDialog();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            UsHelp da = new UsHelp();
            //LoadUserControles(da);
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            formLogin log = new formLogin();
            log.ShowDialog();
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            formSyncro da = new formSyncro();
            da.MdiParent = this;
            da.Show();
        }
    }
}