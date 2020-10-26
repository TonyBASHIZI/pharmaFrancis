using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Collections;
using ProjetBoutique.classes.connections;
using ProjetBoutique.classes.models;
using System.Windows.Forms;



namespace ProjetBoutique.classes
{
    class glossaire
    {
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        Connection cnx;
        MySqlDataAdapter dt = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adpr = null;
        DataSet dste;
        private string server;
        private string database;
        private string uid;
        private string password;
        //clsDatebaseBackupRestor bd = new clsDatebaseBackupRestor();
        private string port;
        // private string str, code_isn;
        private static glossaire _instance = null;


        public static glossaire Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new glossaire();
                return _instance;
            }
        }

        #region Common

        public void InitializeConnection()
        {

            //try
            //{
            //    cnx = new Connection(); cnx.Connect();
            //    con = new MySqlConnection(cnx.path);
            //    con.Open();
            //}
            //catch (Exception)
            //{
            //    throw new Exception("l'un de vos fichiers de configuration est incorrect");
            //}

            try
            {
                string co = "Data Source=localhost;Initial Catalog=boutique_db; User Id=root; Password=root;";
 
                con = new MySqlConnection(co);
                con.Open();
                //MessageBox.Show("CONNECT OK");


                if (!con.State.ToString().ToLower().Equals("open"))
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Impossible de se connecter a un serveur!! contactez Administrateur");
            }
        }
        public void InitializeConnectionReplique()
        {

            try
            {
                //try
                //{
                //    cnx = new Connection(); cnx.Connect();
                //    con = new MySqlConnection(cnx.path);
                //    con.Open();
                //}
                //catch (Exception)
                //{
                //    throw new Exception("l'un de vos fichiers de configuration est incorrect");
                //}
                string co = "Data Source=zakuuza.com;Initial Catalog=c1144147c_boutique_db; User Id=c1144147c_admin; Password=!KVKhL93o&XZ;";
                con = new MySqlConnection(co);
                con.Open();
                //MessageBox.Show("CONNECT OK");


                if (!con.State.ToString().ToLower().Equals("open"))
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Impossible de se connecter a un serveur!! contactez Administrateur");
                Application.Exit();
            }
        }

        private void SetParameter(IDbCommand cmd, string name, DbType type, int length, object value)
        {
            IDbDataParameter param = cmd.CreateParameter();

            param.ParameterName = name;
            param.DbType = type;
            param.Size = length;

            if (value == null)
            {
                if (!param.IsNullable)
                {
                    param.DbType = DbType.String;
                }

                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            cmd.Parameters.Add(param);
        }

        public void GetDatas(GridControl grid, string field, string table)
        {
            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + "";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetDatasArt(GridControl grid, string field, string table)
        {
            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " ORDER BY updated_at DESC";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetDatasSyncro(DataGridView grid, string field, string table)
        {
            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " ";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetDataTableFilter(GridControl grid, string field, string table)
        {
            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " WHERE nbpieces <= 3 ";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetDataTableCdredit(GridControl grid)
        {
            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT code as code_client,nom,prenom,tel,designation as designation_Article,qte,montant,date_credit FROM `credit` INNER JOIN client_fidele on credit.ref_client = client_fidele.code INNER JOIN article on credit.ref_art = article.code_art order by credit.id desc limit 1000";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetDataTableBoss(GridControl grid)
        {
        
            try
            {
                InitializeConnection();
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT  id as ID,ref_art as ID_ART,libeleMvt as DESIGNATION,nbrpieces as QTE,prix_u as PRIX_U,totalpaie as TOTAL,typepaie,voirBoss as CREDIT_BOSS,date_mvt as DATE,ref_agent as AGENT FROM mouvement WHERE typepaie='Liquide avec credit Boss' order by id desc limit 1000";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public void GetCombosData(ComboBoxEdit combo, string field, string table)
        {
            combo.Properties.Items.Clear();

            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table;

                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        combo.Properties.Items.Add(dr[field]).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                dr.Dispose();
            }
        }
        #endregion
        #region article
        public void InsertArt(clsArticle art)
        {
            try
            {
                InitializeConnection();
                string q = "INSERT INTO article (code_art,designation,nbpieces,fss,provenance,prix_u,prix_gros,prix_total,image) VALUES (@code_art,@designation,@nbpieces,@fss,@provenance,@prix_u,@prix_gros,@prix_total,@image)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@code_art", art.CodeArt));
                cmd.Parameters.Add(new MySqlParameter("@designation", art.Designation));
                cmd.Parameters.Add(new MySqlParameter("@nbpieces", art.Nbpieces));
                cmd.Parameters.Add(new MySqlParameter("@fss", art.Fss));
                cmd.Parameters.Add(new MySqlParameter("@provenance", art.Provenance));
                cmd.Parameters.Add(new MySqlParameter("@prix_u", art.Prix_u));
                cmd.Parameters.Add(new MySqlParameter("@prix_gros", art.Prix_gros));
                cmd.Parameters.Add(new MySqlParameter("@prix_total", art.PrixTotal));
                cmd.Parameters.Add(new MySqlParameter("@image", art.image));

                if(cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Insertion article a reussi");
                    cmd.Dispose();
                    con.Close();
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MOdifierArt(clsArticle art)
        {
            try
            {
                InitializeConnection();
                string q = "update article set designation=@designation,nbpieces=@nbpieces,fss=@fss,provenance=@provenance,prix_u=@prix_u,prix_gros=@prix_gros,prix_total=@prix_total,image=@image,last_date=date_Format(Now(), '%Y-%m-%d') where code_art=@code_art";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@code_art", art.CodeArt));
                cmd.Parameters.Add(new MySqlParameter("@designation", art.Designation));
                cmd.Parameters.Add(new MySqlParameter("@nbpieces", art.Nbpieces));
                cmd.Parameters.Add(new MySqlParameter("@fss", art.Fss));
                cmd.Parameters.Add(new MySqlParameter("@provenance", art.Provenance));
                cmd.Parameters.Add(new MySqlParameter("@prix_u", art.Prix_u));
                cmd.Parameters.Add(new MySqlParameter("@prix_gros", art.Prix_gros));
                cmd.Parameters.Add(new MySqlParameter("@prix_total", art.PrixTotal));
                cmd.Parameters.Add(new MySqlParameter("@image", art.image));



                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Modification article a reussi");
                    cmd.Dispose();
                    con.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void insertTableFantome(clsArticle art)
        {
            try
            {
                InitializeConnection();
                string q = "insert into table_fantome(ref_art,designation,typemvt,nbpieces,prix_gros,prix_u,totalpaie,typepaie,ref_agent,action) values(@ref_art,@designation,@typemvt,@nbpieces,@prix_gros,@prix_u,@totalpaie,@typepaie,@ref_agent,@action)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@ref_art", art.CodeArt));
                cmd.Parameters.Add(new MySqlParameter("@designation", art.Designation));
                cmd.Parameters.Add(new MySqlParameter("@typemvt", art.Mvt));
                cmd.Parameters.Add(new MySqlParameter("@nbpieces", art.Nbpieces));
                cmd.Parameters.Add(new MySqlParameter("@prix_gros", art.Prix_gros));
                cmd.Parameters.Add(new MySqlParameter("@prix_u", art.Prix_u));
                cmd.Parameters.Add(new MySqlParameter("@totalpaie", art.PrixTotal));
                cmd.Parameters.Add(new MySqlParameter("@typepaie", art.TypePaie));
                cmd.Parameters.Add(new MySqlParameter("@ref_agent", art.Ref_agent));
                cmd.Parameters.Add(new MySqlParameter("@action", art.Action));

                if(cmd.ExecuteNonQuery() == 1)
                {
                    //MessageBox.Show("Insertion mvt");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void deleteArt(clsArticle art)
        {
            try
            {
                InitializeConnection();
                string q = "delete from article where code_art=@code_art";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@code_art", art.CodeArt));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment suprimer cet article ?", "Supression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Article suprimé");
                        cmd.Dispose();
                        con.Close();

                    }
                }
                

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void insertConne(clsConnecter c)
        {
            try
            {
                     InitializeConnection();
               

                    string q = "insert into connecter(ref_agent,statut) values(@ref_agent,@statut)";
                    cmd = new MySqlCommand(q,con);
                    cmd.Parameters.Add(new MySqlParameter("@ref_agent",c.Ref_agent));
                    cmd.Parameters.Add(new MySqlParameter("@statut", c.Statut));

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        //MessageBox.Show("prensent");
                    }


            }
            catch (Exception)
            {

            }
        }
        public int countArt()
        {
            string c = "";
            int ret = 0;
            try
            {

                InitializeConnection();

                string q = "select count(*) as nbart from article ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nbart");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                c = "0";
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            ret = int.Parse(c);

            return ret + 1;
        }
        public int lastIDMvtOffLine()
        {
            string c = "";
            int ret = 0;
            try
            {

                InitializeConnection();

                string q = "select max(id) as nb from mouvement ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nb");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            ret = int.Parse(c);

            return ret;
        }
        public int lastIDMvtOnLine()
        {
            string c = "";
            int ret = 0;
            try
            {

                InitializeConnectionReplique();

                string q = "select max(id) as nb from mouvement ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nb");
                    ret = int.Parse(c) - 1;
                }
                else
                {
                    ret = 0;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            

            return ret;
        }
        #endregion
        #region categorie
         
        public void InsertCategorie(clscategorie cat)
        {
            try
            {
                InitializeConnection();

                string q = "insert into categorie(designation) value(@designation)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@designation", cat.Designation));
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Insertion de la categorie!");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        #region Mvt

        public Boolean InsertMvt(clsmvt mvt)
        {
            try
            {
                InitializeConnection();
                string q = "insert into mouvement(ref_art,libeleMvt,typeMvt,nbrpieces,prix_gros,prix_u,totalpaie,typepaie,ref_client,ref_agent,voirBoss) values(@ref_art,@libeleMvt,@typeMvt,@nbrpieces,@prix_gros,@prix_u,@totalpaie,@typepaie,@ref_client,@ref_agent,@voirBoss)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@ref_art",mvt.Ref_art));
                cmd.Parameters.Add(new MySqlParameter("@libeleMvt", mvt.Lblmvt));
                cmd.Parameters.Add(new MySqlParameter("@typeMvt", mvt.Typemvt));
                cmd.Parameters.Add(new MySqlParameter("@nbrpieces", mvt.Nbpieces));
                cmd.Parameters.Add(new MySqlParameter("@prix_gros", mvt.Prix_gro));
                cmd.Parameters.Add(new MySqlParameter("@prix_u", mvt.Prix_U));
                cmd.Parameters.Add(new MySqlParameter("@totalpaie", mvt.Totalpaie));
                cmd.Parameters.Add(new MySqlParameter("@typepaie", mvt.Typepaie));
                cmd.Parameters.Add(new MySqlParameter("@ref_client", mvt.Ref_cl));
                cmd.Parameters.Add(new MySqlParameter("@ref_agent", mvt.Ref_agent));
                cmd.Parameters.Add(new MySqlParameter("@voirBoss", mvt.VoirBoss));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment passer cette vente ?", "VENTE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        clsArticle ar = new clsArticle();
                        ar.CodeArt = mvt.Ref_art;
                        //MessageBox.Show("" + ar.CodeArt);
                        updateStock(ar, mvt.Nbpieces);

                        if (mvt.Typepaie == "Credit")
                        {
                            clsCredit cr = new clsCredit();
                            cr.Ref_cl = mvt.Ref_cl;
                            cr.Ref_Art = mvt.Ref_art;
                            cr.Qte = mvt.Nbpieces;
                            cr.Montant = mvt.Totalpaie;

                            insertCredit(cr);
                        }

                        return true;

                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return false;
        }
        public bool editCreditBoss(clsmvt mvt)
        {
            try{
                InitializeConnection();
                string q = "update mouvement set voirBoss=@voirBoss, date_mvt=date_Format(Now(), '%Y-%m-%d') where id=@id";
                cmd = new MySqlCommand(q,con);
                cmd.Parameters.Add(new MySqlParameter("@id", mvt.Id));
                cmd.Parameters.Add(new MySqlParameter("@voirBoss", mvt.VoirBoss));
                DialogResult result = MessageBox.Show("Voulez-vous vraiment passer cette operation ?", "PAIEMENT CREDIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Paiement credit Boss effecté");
                        
                    }else{
                        return false;
                    }
                    
                }

            }catch(Exception ex){

                MessageBox.Show("Une erreur s'est produite lors du traitement de l'operation /n"+ex.Message);
            }

            return true;
            
        }

        public bool syncroMvt(clsmvt mvt)
        {
            try
            {
                InitializeConnectionReplique();
                string q = "insert into mouvement(ref_art,libeleMvt,typeMvt,nbrpieces,prix_gros,prix_u,totalpaie,typepaie,ref_client,ref_agent,voirBoss) values(@ref_art,@libeleMvt,@typeMvt,@nbrpieces,@prix_gros,@prix_u,@totalpaie,@typepaie,@ref_client,@ref_agent,@voirBoss)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@ref_art", mvt.Ref_art));
                cmd.Parameters.Add(new MySqlParameter("@libeleMvt", mvt.Lblmvt));
                cmd.Parameters.Add(new MySqlParameter("@typeMvt", mvt.Typemvt));
                cmd.Parameters.Add(new MySqlParameter("@nbrpieces", mvt.Nbpieces));
                cmd.Parameters.Add(new MySqlParameter("@prix_gros", mvt.Prix_gro));
                cmd.Parameters.Add(new MySqlParameter("@prix_u", mvt.Prix_U));
                cmd.Parameters.Add(new MySqlParameter("@totalpaie", mvt.Totalpaie));
                cmd.Parameters.Add(new MySqlParameter("@typepaie", mvt.Typepaie));
                cmd.Parameters.Add(new MySqlParameter("@ref_client", mvt.Ref_cl));
                cmd.Parameters.Add(new MySqlParameter("@ref_agent", mvt.Ref_agent));
                cmd.Parameters.Add(new MySqlParameter("@voirBoss", mvt.VoirBoss));

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        //clsArticle ar = new clsArticle();
                        //ar.CodeArt = mvt.Ref_art;
                        ////MessageBox.Show("" + ar.CodeArt);
                        //updateStock(ar, mvt.Nbpieces);

                        //if (mvt.Typepaie == "Credit")
                        //{
                        //    clsCredit cr = new clsCredit();
                        //    cr.Ref_cl = mvt.Ref_cl;
                        //    cr.Ref_Art = mvt.Ref_art;
                        //    cr.Qte = mvt.Nbpieces;
                        //    cr.Montant = mvt.Totalpaie;

                        //    insertCredit(cr);
                        //}

                        return true;

                    }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return false;
        }
        public string getQteastock(string codeArt)
        {
            string n1 = "0";
            try
            {
                InitializeConnection();
                string q = "select nbpieces from article where code_art='"+codeArt+"'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    n1 = dr.GetString("nbpieces");

                }
                //MessageBox.Show("" + n1);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return n1;

        }
        public void updateStock(clsArticle art, string nbv)
        {
            string c = art.CodeArt;
            float newv = float.Parse(getQteastock(c)) - float.Parse(nbv);
            try
            {
                InitializeConnection();
                string q = "update article set nbpieces=@nbpieces where code_art=@code_art";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@code_art", art.CodeArt));
                cmd.Parameters.Add(new MySqlParameter("@nbpieces", newv));
                if(cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Operation effectuee avec succes!!");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region client

        public void insertClient(clsclient cl)
        {
            try
            {
                InitializeConnection();
                string q = "insert into client_fidele(code,nom,prenom,tel,adresse) values(@code,@nom,@prenom,@tel,@adresse)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@code", cl.Codecl));
                cmd.Parameters.Add(new MySqlParameter("@nom", cl.Noms));
                cmd.Parameters.Add(new MySqlParameter("@prenom", cl.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@tel", cl.Tel));
                cmd.Parameters.Add(new MySqlParameter("@adresse", cl.Adresse));

                if(cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Operation effectuee avec succes du client '" + cl.Prenom + "'");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region
        public void insertCredit(clsCredit cre)
        {
            try
            {
                InitializeConnection();
                string q = "insert into credit(ref_client,ref_art,qte,montant) values(@ref_client,@ref_art,@qte,@montant)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@ref_client", cre.Ref_cl));
                cmd.Parameters.Add(new MySqlParameter("@ref_art", cre.Ref_Art));
                cmd.Parameters.Add(new MySqlParameter("@qte", cre.Qte));
                cmd.Parameters.Add(new MySqlParameter("@montant", cre.Montant));

                if (cmd.ExecuteNonQuery() == 1)
                {
                    
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion
        #region
          public void insertUser(clsutilisateurs user)
        {
            try
            {
                InitializeConnection();
                string q = "insert into utilisateurs(nom,tel,mot_de_passe,mot_secret) values(@nom,@tel,@mot_de_passe,@mot_secret)";
                cmd = new MySqlCommand(q,con);
                cmd.Parameters.Add(new MySqlParameter("@nom", user.Noms));
                cmd.Parameters.Add(new MySqlParameter("@tel", user.Tel));
                cmd.Parameters.Add(new MySqlParameter("@mot_de_passe", user.Mot_de_pasee));
                cmd.Parameters.Add(new MySqlParameter("@mot_secret", user.Mot_secret));

                if(cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Creation effectuee!! ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void suprimerUser(clsutilisateurs user)
          {
              try
              {
                  InitializeConnection();
                  string q = "delete from utilisateurs where tel=@tel";
                  cmd = new MySqlCommand(q, con);
                  cmd.Parameters.Add(new MySqlParameter("@tel", user.Tel));
                  if(cmd.ExecuteNonQuery() == 1)
                  {
                      MessageBox.Show("Operation effectuée avec succes!!!");
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

          }
        public string getMotSecret(string tel)
        {
            string res = "toto";
            try
            {
                InitializeConnection();
                string q = "select mot_secret from utilisateurs where tel='"+tel+"'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res = dr.GetString("mot_secret");
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                dr.Dispose();
               
                
            }

            return res;
        }

        public void modifierUsers(clsutilisateurs user)
        {
            try
            {
                //InitializeConnection();
                if (user.Mot_secret == getMotSecret(user.Tel))
                {
                    string q = "update utilisateurs set nom=@nom,mot_de_passe=@mot_de_passe,mot_secret=@mot_secret where tel=@tel";
                    cmd = new MySqlCommand(q,con);
                    cmd.Parameters.Add(new MySqlParameter("@nom", user.Noms));
                    cmd.Parameters.Add(new MySqlParameter("@tel", user.Tel));
                    cmd.Parameters.Add(new MySqlParameter("@mot_de_passe", user.Mot_de_pasee));
                    cmd.Parameters.Add(new MySqlParameter("@mot_secret", user.Mot_secret));

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Modification  effectuée!! ");
                    }


                }
                else
                {
                    MessageBox.Show("Le mot secret est incorrect !!");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        #endregion
        #region
          public DataSet sortieStock()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from article", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "article");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieAlerteStock()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from article where nbpieces <= 3", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "article");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieEtatIntervalle(string date1, string date2)
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from mouvement where Date_Format(date_mvt,'%d/%m/%Y') between '" + date1 + "' and '" + date2 + "' ", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "mouvement");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieVentes()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from mouvement WHERE date_Format(date_mvt, '%Y-%m-%d') = date_Format(Now(), '%Y-%m-%d')", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "mouvement");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet allVentes()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from mouvement WHERE date_Format(date_mvt, '%Y-%m-%d') = date_Format(Now(), '%Y-%m-%d')", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "mouvement");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieCreditBoss()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from mouvement WHERE typepaie='Liquide avec credit Boss' ", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "mouvement");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieCreditBossIntervalle(string date1)
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from mouvement WHERE typepaie='Liquide avec credit Boss' AND Date_Format(date_mvt,'%d/%m/%Y') = '" + date1 +"", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "mouvement");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieCreditClient()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from credit", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "mouvement");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieEtatFinal()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from etatstock", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "etatstock");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
          public DataSet sortieUsers()
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select nom,tel from utilisateurs", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "utilisateurs");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
        public string getVente()
          {
              string res="";
              try
              {
                  InitializeConnection();
                 
                  string q2 = "select sum(totalpaie) as alvente from mouvement WHERE date_Format(date_mvt, '%Y-%m-%d') = date_Format(Now(), '%Y-%m-%d')";
                 
                  cmd = new MySqlCommand(q2, con);

                  dr = cmd.ExecuteReader();
                  if (dr.Read())
                  {
                       res = dr.GetString("alvente");
                  }
                  else
                  {
                      res = "0";
                  }
              }
              catch (Exception ex)
              {
                  //MessageBox.Show(ex.Message);
              }

              return res;
          }
        public Boolean Login(string phone, string password)
        {
            Boolean b = false;

            try
            {
                InitializeConnection();

                cmd = new MySqlCommand("SELECT tel,mot_de_passe FROM utilisateurs where tel ='" + phone + "' AND mot_de_passe = '" + password + "'", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    b = true;
                }

                if (b == true)
                {
                    //MessageBox.Show("La connection a reussie !");
                    b = true;

                }
                else
                {
                    MessageBox.Show("Echec de Connexion");
                    b = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return b;
        }
        public string getCountVente()
        {
            string res = "";
            try
            {
                InitializeConnection();

                string q2 = "select count(totalpaie) as alvente from mouvement WHERE date_Format(date_mvt, '%Y-%m-%d') = date_Format(Now(), '%Y-%m-%d')";

                cmd = new MySqlCommand(q2, con);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res = dr.GetString("alvente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return res;
        }
        public string getAdmin()
        {
            string res = "";
            try
            {
                InitializeConnection();

                string q2 = "select count(*) as alAdmin from utilisateurs";

                cmd = new MySqlCommand(q2, con);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res = dr.GetString("alAdmin");
                }else
                {
                    res = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return res;
        }
        public string getCl()
        {
            string res = "";
            try
            {
                InitializeConnection();

                string q3 = "select count(*) as aluser from client_fidele";

                cmd = new MySqlCommand(q3, con);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res = dr.GetString("aluser");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return res;
        }
        public string getAlerteArt()
        {
            string lb = "";
            try
            {
                InitializeConnection();

                string q3 = "select count(*) as alert from article where nbpieces < 3";

                cmd = new MySqlCommand(q3, con);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lb = dr.GetString("alert");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lb;
        }
        
        
          public void getDash(Label lbart, Label lbvente, Label lbclient)
          {
              try
              {
                  InitializeConnection();
                  string q1 = "select count(*) as alart from article";
 
                  cmd = new MySqlCommand(q1, con);
                 
                  dr = cmd.ExecuteReader();
                  if (dr.Read())
                  {
                      lbart.Text = dr.GetString("alart");
                      lbvente.Text = getVente();
                      lbclient.Text = getCl();
                      
                      //lbmvt.Text = getCountVenteFilter();
                      //lbcred.Text = getCountVenteFilter();
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
          }
          public DataSet sortieFacture(string code)
          {

              try
              {
                  InitializeConnection();
                  if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                  cmd = new MySqlCommand("select * from mouvement where id = (select max(id) from mouvement)", con);
                  adpr = new MySqlDataAdapter(cmd);
                  dste = new DataSet();
                  adpr.Fill(dste, "article");

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

              return dste;
          }
        
        #endregion
        public string getBoss()
          {
            string n ="";
              try
              {
                  InitializeConnection();
                  string q = "select sum(voirBoss) as total from mouvement where typepaie='Liquide avec credit Boss' ";
                  cmd = new MySqlCommand(q, con);

                  dr = cmd.ExecuteReader();
                  if (dr.Read())
                  {
                      n = dr.GetString("total");
                  }
              }catch(Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
              finally
              {
                  cmd.Dispose();
                  con.Close();
              }
            return n;
          }

    }
}
       