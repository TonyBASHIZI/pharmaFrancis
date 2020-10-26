using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetBoutique.classes.models
{
    class clsArticle
    {
        public string CodeArt { get; set; }
        public string Designation { get; set; }
        public string Categorie { get; set; }
        public string Nbpieces { get; set; }
        public string Qte { get; set; }
        public string Fss { get; set; }
        public string Provenance { get; set; }
        public string Ref_agent { get; set; }
        public string Action { get; set; }
        public string Prix_u { get; set; }
        public string Prix_gros { get; set; }
        public byte[] image { get; set; }
        public string PrixTotal { get; set; }
        public string Mvt { get; set; }
        public string TypePaie { get; set; }


    }
}
