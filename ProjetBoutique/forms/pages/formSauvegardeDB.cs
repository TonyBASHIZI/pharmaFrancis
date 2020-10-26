using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetBoutique.classes.connections;

namespace ProjetBoutique.forms.pages
{
    public partial class formSauvegardeDB : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        Connection ap = new Connection();
        clsDatebaseBackupRestor bd = new clsDatebaseBackupRestor();
        public formSauvegardeDB()
        {
            InitializeComponent();
        }

        private void btnParcourir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    personalizePath.Text = dlg.SelectedPath;
                    btnSauvegarde.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void btnSauvegarde_Click(object sender, EventArgs e)
        {
            //string file = "E:\\backup.sql";//This is path to save the backup db file..
            //string file = ""+ bd.getBackupPath(radioButton3, personalizePath);//This is path to save the backup db file..

            string constring = "server=localhost;PORT=3306;user=root;pwd=root;database=boutique_db; Convert Zero Datetime=True;";//
            try
            { 
            //    using (MySqlConnection conn = new MySqlConnection(constring))
            //    {
            //        using (MySqlCommand cmd = new MySqlCommand())
            //        {
            //            using (MySqlBackup mb = new MySqlBackup(cmd))
            //            {
            //                cmd.Connection = conn;
            //                conn.Open();
            //                mb.ExportToFile(file);//This line will export the file to given path.
            //                conn.Close();
            //                MessageBox.Show("Backup Completed...!!!");
            //            }
            //        }
            //    }
                //ap.Connect();
                //con = new MySqlConnection(ap.path);
                //string database = con.Database.ToString();

                //if (bd.getBackupPath(radioButton3, personalizePath) == string.Empty)
                //{
                //    MessageBox.Show("Veuillez selectionner d'abord un emplacement s.v.p.!");
                //}
                //else
                //{

                //    string cmd = "BACKUP DATABASE " + database + " TO DISK='" + bd.getBackupPath(radioButton3, personalizePath) + "\\" + database + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                //    using (MySqlCommand command = new MySqlCommand(cmd, con))
                //    {
                //        if (con.State != ConnectionState.Open)
                //        {
                //            con.Open();
                //        }
                //        command.ExecuteNonQuery();
                //        con.Close();
                //        MessageBox.Show("Sauvegarde effectué avec succés", "Confirmation Sauvegarde");
                //    }
                //}

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void formSauvegardeDB_Load(object sender, EventArgs e)
        {
            try
            {
                defaultPath.Text = bd.getBackupPath(radioButton3, personalizePath);
            }
            catch (Exception)
            { }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            btnSauvegarde.Enabled = false;
            btnParcourir.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            btnParcourir.Enabled = false;
            btnSauvegarde.Enabled = true;
        }
    }
}
