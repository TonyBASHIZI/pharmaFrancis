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
using System.Threading;

namespace ProjetBoutique.forms.pages
{
    public partial class formSyncro : Form
    {
        clsmvt mvt = new clsmvt();
        glossaire glos = new glossaire();
        formProgress pro = new formProgress();
       
        public formSyncro()
        {
            InitializeComponent();
        }
        //public void CallToChildThread()
        //{
            
        //    pro.ShowDialog();
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            int lastIdOFF = int.Parse(lblastIdOff.Text);
            int lastIdOn = int.Parse(lblastIdOn.Text);
            if (lastIdOFF == lastIdOn)
            {
                MessageBox.Show("LES DONNEES SONT A JOUR EN LIGNE!!");
            }
            else if (lastIdOFF > lastIdOn)
            {
                
                DialogResult result = MessageBox.Show("Voulez-vous vraiment passer cette syncronisation ? ca va prendre quelques minutes ", "ON LINE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //ThreadStart childref = new ThreadStart(CallToChildThread);

                    //Thread childThread = new Thread(childref);
                    //childThread.Start();

                    for (int i = lastIdOn + 1; i < dataGridView1.Rows.Count; i++)
                    {
                        mvt.Ref_art = dataGridView1.Rows[i].Cells["ref_art"].FormattedValue.ToString();
                        mvt.Lblmvt = dataGridView1.Rows[i].Cells["libeleMvt"].FormattedValue.ToString();
                        mvt.Typemvt = dataGridView1.Rows[i].Cells["typeMvt"].FormattedValue.ToString();
                        mvt.Nbpieces = dataGridView1.Rows[i].Cells["nbrpieces"].FormattedValue.ToString();
                        mvt.Prix_gro = dataGridView1.Rows[i].Cells["prix_gros"].FormattedValue.ToString();
                        mvt.Prix_U = dataGridView1.Rows[i].Cells["prix_u"].FormattedValue.ToString();
                        mvt.Totalpaie = dataGridView1.Rows[i].Cells["totalpaie"].FormattedValue.ToString();
                        mvt.Typepaie = dataGridView1.Rows[i].Cells["typepaie"].FormattedValue.ToString();
                        mvt.Ref_cl = dataGridView1.Rows[i].Cells["ref_client"].FormattedValue.ToString();
                        mvt.Ref_agent = dataGridView1.Rows[i].Cells["ref_agent"].FormattedValue.ToString();
                        mvt.VoirBoss = dataGridView1.Rows[i].Cells["voirBoss"].FormattedValue.ToString();

                        if (glos.syncroMvt(mvt) == true) {

                            //childThread.Abort();   
                            
                        }else{
                            MessageBox.Show("ERREUR DE CONNECTION");
                        }
                         //MessageBox.Show("" + mvt.Ref_art +" "+mvt.Lblmvt+ " "+mvt.Typemvt+" "+mvt.Nbpieces+" "+mvt.Prix_U);
                    }
                    MessageBox.Show("FIN DE TRAITEMENT DE LA SYNCRONISATION");
                }
            }
            
            lblastIdOff.Text = "" + glos.lastIDMvtOffLine();
            lblastIdOn.Text = "" + glos.lastIDMvtOnLine();

        }

        private void formSyncro_Load(object sender, EventArgs e)
        {
            lblastIdOff.Text = "" + glos.lastIDMvtOffLine();
            lblastIdOn.Text = "" +glos.lastIDMvtOnLine();
            glos.GetDatasSyncro(dataGridView1, "*", "mouvement");
            //glos.GetDatasSyncro(dataGridView2, "*", "article");

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int lastIdOFF = int.Parse(lblastIdOff.Text);
            //    int lastIdOn = int.Parse(lblastIdOn.Text);
            //    if (lastIdOFF == lastIdOn)
            //    {
            //        MessageBox.Show("LES DONNEES SONT A JOUR EN LIGNE!!");
            //    }
            //    else if (lastIdOFF > lastIdOn)
            //    {

            //        DialogResult result = MessageBox.Show("Voulez-vous vraiment passer cette syncronisation ?", "ON LINE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //        if (result == DialogResult.Yes)
            //        {
            //            for (int i = lastIdOn + 1; i < dataGridView1.Rows.Count; i++)
            //            {
            //                mvt.Ref_art = dataGridView2.Rows[i].Cells["ref_art"].FormattedValue.ToString();
            //                mvt.Lblmvt = dataGridView2.Rows[i].Cells["libeleMvt"].FormattedValue.ToString();
            //                mvt.Typemvt = dataGridView2.Rows[i].Cells["typeMvt"].FormattedValue.ToString();
            //                mvt.Nbpieces = dataGridView2.Rows[i].Cells["nbrpieces"].FormattedValue.ToString();
            //                mvt.Prix_gro = dataGridView2.Rows[i].Cells["prix_gros"].FormattedValue.ToString();
            //                mvt.Prix_U = dataGridView2.Rows[i].Cells["prix_u"].FormattedValue.ToString();
            //                mvt.Totalpaie = dataGridView2.Rows[i].Cells["totalpaie"].FormattedValue.ToString();
            //                mvt.Typepaie = dataGridView2.Rows[i].Cells["typepaie"].FormattedValue.ToString();
            //                mvt.Ref_cl = dataGridView2.Rows[i].Cells["ref_client"].FormattedValue.ToString();
            //                mvt.Ref_agent = dataGridView2.Rows[i].Cells["ref_agent"].FormattedValue.ToString();
            //                mvt.VoirBoss = dataGridView2.Rows[i].Cells["voirBoss"].FormattedValue.ToString();

            //                glos.syncroMvt(mvt);
            //                //MessageBox.Show("" + mvt.Ref_art +" "+mvt.Lblmvt+ " "+mvt.Typemvt+" "+mvt.Nbpieces+" "+mvt.Prix_U);
            //            }
            //        }
            //    }

            //    lblastIdOff.Text = "" + glos.lastIDMvtOffLine();
            //    lblastIdOn.Text = "" + glos.lastIDMvtOnLine();
            //}catch(Exception ex)
            //{

            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
