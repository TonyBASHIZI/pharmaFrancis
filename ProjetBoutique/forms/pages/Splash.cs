using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using ProjetBoutique.classes;

namespace ProjetBoutique.forms.pages
{
    public partial class Splash : SplashScreen
    {
        int cmt = 0;
        public Splash()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void Splash_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Start();
            //timer1.Enabled = true;
            label4.Text = "" + cmt++;
            cmt++;
            if (cmt == 200)
            {
                
                this.Hide();
                formLogin log = new formLogin();
                log.ShowDialog();
                cmt = 0;
                timer1.Enabled = false;
                timer1.Stop();
            }
        }
      
    }
}