using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetBoutique.classes.models;
using ThoughtWorks.QRCode.Codec;
using System.IO;
using ProjetBoutique.reports;
using DevExpress.XtraReports.UI;
using ProjetBoutique.classes;

namespace ProjetBoutique.forms.pages
{
    public partial class formArticle : Form
    {
        classes.glossaire glos = new classes.glossaire();
        classes.models.clsArticle art = new classes.models.clsArticle();
        string imglocation = "";
        string user;
        DataTable dbdataset;
       
        private Byte[] convertImageTobyte(PictureBox pic)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmpImage = new Bitmap(pic.Image);
            Byte[] bytImage;
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytImage = ms.ToArray();
            ms.Close();
            return bytImage;
        }
        void QRCode(PictureBox pic_box, string data)
        {

            try
            {
                var objQRCode = new QRCodeEncoder();
                Image imgImage;
                objQRCode.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                objQRCode.QRCodeScale = 7;
                objQRCode.QRCodeVersion = 4;
                objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L;
                imgImage = objQRCode.Encode(data);
                pic_box.Image = imgImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void initialise()
        {
            
            txtdesign.Text = "";
            txtnbpieces.Text = "";
            txtpricU.Text = "";
            txtprixGros.Text = "";
            txtprovena.Text = "";
            txtcode.Text = getmat();
            QRCode(pictureArt, "");



        }
        public string getmat()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 1000);
            string mat;

            DateTime da = DateTime.Now;

            string lastid = "" + glos.countArt();
            //string lastid = "00";

            mat = "ART-" + da.Month + "" + "-" + lastid + "" + x;

            return mat;
        }    
        public formArticle(string connecter)
        {
            InitializeComponent();
            txtcode.Text = getmat();
            glos.GetDatas(gridControl1, "code_art,designation,nbpieces,fss,prix_u,prix_gros,provenance", "article");
            user = connecter;
            txtfss.Text = user;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (txtcode.Text == "" || txtdesign.Text == "" || txtnbpieces.Text == "" || txtpricU.Text == "" || txtfss.Text == "" || txtprixGros.Text == "")
            {
                MessageBox.Show("Remplissez tous les champs!!");
            }
            else
            {
                try
                {
                    label9.Text = "" + int.Parse(txtpricU.Text) * int.Parse(txtnbpieces.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verifier tous les champs!!");
                }

                art.CodeArt = txtcode.Text;
                art.Categorie = txtcode.Text;
                art.Designation = txtdesign.Text;
                art.Nbpieces = txtnbpieces.Text;
                art.Fss = txtfss.Text;
                art.Provenance = txtprovena.Text;
                art.Prix_u = txtpricU.Text;
                art.Prix_gros = txtprixGros.Text;
                art.PrixTotal = label9.Text;
                art.image = convertImageTobyte(pictureArt);
                glos.InsertArt(art);
                glos.GetDatas(gridControl1, "code_art,designation,nbpieces,fss,prix_u,prix_gros,provenance", "article");
                txtcode.Text = getmat();
                initialise();

            }
        }

        private void formArticle_Load(object sender, EventArgs e)
        {
           
           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtcode.Text == "" || txtdesign.Text == "" || txtnbpieces.Text == "" || txtpricU.Text == "" || txtfss.Text == "")
            {
                MessageBox.Show("Remplissez tous les champs!!");
            }
            else
            {
                art.CodeArt = txtcode.Text;
                art.Categorie = txtcode.Text;
                art.Designation = txtdesign.Text;

                art.Nbpieces = txtnbpieces.Text;
                art.Fss = txtfss.Text;
                art.PrixTotal = label9.Text;
                art.Provenance = txtprovena.Text;
                art.PrixTotal = label9.Text;
                art.Prix_u = txtpricU.Text;
                art.Prix_gros = txtprixGros.Text;
                art.image = convertImageTobyte(pictureArt);
                glos.MOdifierArt(art);
                glos.GetDatas(gridControl1, "code_art,designation,nbpieces,fss,prix_u,prix_gros,provenance", "article");
                txtcode.Text = getmat();
                initialise();

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            art.Designation = txtdesign.Text;

            art.Nbpieces = txtnbpieces.Text;
            art.Fss = txtfss.Text;
            art.PrixTotal = label9.Text;
            art.Provenance = txtprovena.Text;
            art.Nbpieces = txtnbpieces.Text;
            art.PrixTotal = label9.Text;
            art.Prix_u = txtpricU.Text;
            art.Prix_gros = txtprixGros.Text;
            art.CodeArt = txtcode.Text;
            art.Mvt = "Supression article";
            art.TypePaie = "Pas de vente";
            art.Ref_agent = user;
            art.Action = "Supression";
            glos.deleteArt(art);
            glos.insertTableFantome(art);
            initialise();
            txtcode.Text = getmat();
            glos.GetDatas(gridControl1, "code_art,designation,nbpieces,fss,prix_u,prix_gros,provenance", "article");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                FicheStock j = new FicheStock();
                j.DataSource = glossaire.Instance.sortieStock();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtnbpieces_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtnbpieces.Text.Length; i++)
            {
                if (!char.IsDigit(txtnbpieces.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres!!");
                    txtnbpieces.Text = "";
                }
                else
                {

                }
            }
        }

        private void txtpricU_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtpricU.Text.Length; i++)
            {
                if (!char.IsDigit(txtpricU.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres!!");
                    txtpricU.Text = "";
                }
                else
                {

                }
            }
        }

        private void txtprixGros_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtprixGros.Text.Length; i++)
            {
                if (!char.IsDigit(txtprixGros.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres!!");
                    txtprixGros.Text = "";
                }
                else
                {

                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtpricU_Move(object sender, EventArgs e)
        {
            try
            {
                label13.Text = "" + int.Parse(txtpricU.Text) * int.Parse(txtnbpieces.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCode(pictureArt, txtcode.Text);
        }

        private void txtpricU_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                label9.Text = "" + int.Parse(txtpricU.Text) * int.Parse(txtnbpieces.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                int to = int.Parse(gridView1.GetFocusedRowCellValue("prix_u").ToString());
                txtcode.Text = gridView1.GetFocusedRowCellValue("code_art").ToString();
                txtdesign.Text = gridView1.GetFocusedRowCellValue("designation").ToString();
                txtnbpieces.Text = gridView1.GetFocusedRowCellValue("nbpieces").ToString();
                label9.Text = "" + int.Parse(txtnbpieces.Text) * to;
                txtfss.Text = gridView1.GetFocusedRowCellValue("fss").ToString();
                txtpricU.Text = gridView1.GetFocusedRowCellValue("prix_u").ToString();
                txtprixGros.Text = gridView1.GetFocusedRowCellValue("prix_gros").ToString();
                txtprixGros.Text = gridView1.GetFocusedRowCellValue("prix_gros").ToString();
                txtprovena.Text = gridView1.GetFocusedRowCellValue("provenance").ToString();

                QRCode(pictureArt, txtcode.Text);
            }
            catch (Exception ex)
            {

            }
            
        }

        private void txtnbpieces_Move(object sender, EventArgs e)
        {
            if (txtpricU.Text !="")
            {
                try
                {
                    label9.Text = "" + int.Parse(txtpricU.Text) * int.Parse(txtnbpieces.Text);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void txtnbpieces_MouseLeave(object sender, EventArgs e)
        {
            if (txtpricU.Text !="")
            {
                try
                {
                    label9.Text = "" + int.Parse(txtpricU.Text) * int.Parse(txtnbpieces.Text);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
