using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Ajout des using pour la Base de donnée
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace CrispyLo.Modules
{
    internal class BaseDeDonnée
    {
        public string DefinirCheminBD()
        {
            return "Server=localhost;Database=crispypizzadb;port=3306;User Id=root;password=root";
        }
        //---------------- Vérifier le mot de passe ----------------
        public void VerificationMDP(out string mdp1, out string mdp2, out string mdpAdmin)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            DataTable dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM utilisateur";
                MySqlDataAdapter Data = new MySqlDataAdapter(query, maConnexion);
                Data.Fill(dataT);
                maConnexion.Close();
                mdp1 = dataT.Rows[0]["motdepasse"].ToString();
                mdp2 = dataT.Rows[1]["motdepasse"].ToString();
                mdpAdmin = dataT.Rows[2]["motdepasse"].ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void AllProduitByTypeProduit(int id, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM produit WHERE idTypeProduit =" + id;
                MySqlDataAdapter Data = new MySqlDataAdapter(query, maConnexion);
                Data.Fill(dataT);
                nbrRow = dataT.Rows.Count;
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void AllTypePrduit(out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM typeproduit";
                MySqlDataAdapter Data = new MySqlDataAdapter(query, maConnexion);
                Data.Fill(dataT);
                nbrRow = dataT.Rows.Count;
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void AllPizzaByTypeProduit(int id, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM produit WHERE idTypePizza = " + id  + " AND idTypeProduit = 1";
                MySqlDataAdapter Data = new MySqlDataAdapter(query, maConnexion);
                Data.Fill(dataT);
                nbrRow = dataT.Rows.Count;
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void ProduitById(int id, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM produit WHERE id = " + id;
                MySqlDataAdapter Data = new MySqlDataAdapter(query, maConnexion);
                Data.Fill(dataT);
                nbrRow = dataT.Rows.Count;
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void ProduitByLibelle(string libelle, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM produit WHERE libelle = '" + libelle + "'";
                MySqlDataAdapter Data = new MySqlDataAdapter(query, maConnexion);
                Data.Fill(dataT);
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
