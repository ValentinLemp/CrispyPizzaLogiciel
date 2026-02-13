using CrispyLo.Views.Info;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
//Ajout des using pour la Base de donnée
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CrispyLo.Modules
{
    internal class BaseDeDonnée
    {
        public string DefinirCheminBD()
        {
            return "Server=localhost;Database=crispypizzadb;port=3306;User Id=root;password=root";
        }
        //---------------- Vérifier le mot de passe ----------------
        public void VerificationMDP(out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM utilisateur";
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

        public void RestaurentById(int id, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM restaurant WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
                maConnexion.Close();
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
                string query = "SELECT * FROM produit WHERE idTypeProduit = @id AND estSuggestion = 0";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
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
        public void AllProduitByEstSuggestion(out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM produit WHERE estSuggestion = 1";
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

        public void AllTypeProduit(out DataTable dataT, out int nbrRow)
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
                string query = "SELECT * FROM produit WHERE idTypePizza = @id AND idTypeProduit = 1 AND estSuggestion = 0";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
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

        public void ProduitById(int id, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM produit WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
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
                string query = "SELECT * FROM produit WHERE libelle = @libelle";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@libelle", libelle);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void AllIngredientByCategorieIng(int id, string libelle, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM ingredient WHERE idCategorieIngredient = @id AND " + libelle + " = 1";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
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

        public void IngredientById(int id, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM ingredient WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void IngredientByLibelle(string libelle, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM ingredient WHERE libelle = @libelle";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@libelle", libelle);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void AllClientByTelephone(string phone, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM client WHERE numero = @phone";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@phone", phone);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
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

        public void AdresseByClient(int id, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM adresse WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void SupprimerClient(int idClient)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();

                string queryAdresse = "SELECT idAdresse FROM client WHERE id = @idClient";
                MySqlCommand cmdAdresse = new MySqlCommand(queryAdresse, maConnexion);
                cmdAdresse.Parameters.AddWithValue("@idClient", idClient);
                int idAdresse = Convert.ToInt32(cmdAdresse.ExecuteScalar());

                string queryClient = "DELETE FROM client WHERE id = @idClient2";
                MySqlCommand cmdClient = new MySqlCommand(queryClient, maConnexion);
                cmdClient.Parameters.AddWithValue("@idClient2", idClient);
                cmdClient.ExecuteNonQuery();

                string queryDeleteAdresse = "DELETE FROM adresse WHERE id = @idAdresse";
                MySqlCommand cmdDeleteAdresse = new MySqlCommand(queryDeleteAdresse, maConnexion);
                cmdDeleteAdresse.Parameters.AddWithValue("@idAdresse", idAdresse);
                cmdDeleteAdresse.ExecuteNonQuery();

                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public int AjouterAdresse(double codePostal, string nomRue, string numeroRue, string localite)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "INSERT INTO adresse(codePostal, nomRue, numeroRue, localite) VALUES(@codePostal, @nomRue, @numeroRue, @localite)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@codePostal", codePostal);
                cmd.Parameters.AddWithValue("@nomRue", nomRue);
                cmd.Parameters.AddWithValue("@numeroRue", numeroRue);
                cmd.Parameters.AddWithValue("@localite", localite);
                cmd.ExecuteNonQuery();
                int id = (int)cmd.LastInsertedId;
                maConnexion.Close();
                return id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public int AjouterClient(string nom, string prenom, string numero, int idAdresse)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "INSERT INTO client(nom, prenom, numero, idAdresse) VALUES(@nom, @prenom, @numero, @idAdresse)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@idAdresse", idAdresse);
                cmd.ExecuteNonQuery();
                int id = (int)cmd.LastInsertedId;
                maConnexion.Close();
                return id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public int NewCommande(string nom, string prenom, Boolean livraison, DateTime date, double prixTotal, string commentaire, int idRest, int idStatut, int? idClient)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"INSERT INTO commande(nom, prenom, estLivraison, dateHeure, prixTotal, commetaire, idRestaurant, idStatut, idClient) 
                        VALUES(@nom, @prenom, @livraison, @date, @prixTotal, @commentaire, @idRest, @idStatut, @idClient)";

                MySqlCommand cmd = new MySqlCommand(query, maConnexion);

                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@livraison", livraison);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@prixTotal", prixTotal);
                cmd.Parameters.AddWithValue("@commentaire", commentaire);
                cmd.Parameters.AddWithValue("@idRest", idRest);
                cmd.Parameters.AddWithValue("@idStatut", idStatut);
                if (idClient.HasValue)
                    cmd.Parameters.AddWithValue("@idClient", idClient.Value);
                else
                    cmd.Parameters.AddWithValue("@idClient", DBNull.Value);
                cmd.ExecuteNonQuery();

                int id = (int)cmd.LastInsertedId;
                maConnexion.Close();
                return id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public int NewLigne(double prixLigne, Boolean taille, string typePate, string note, int idProduit, int idCom)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "INSERT INTO ligneCommande(prixProduit, tailleLarge, typePate, note, idProduit, idCommande) VALUES(@prixLigne, @taille, @typePate, @note, @idProduit, @idCom)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@prixLigne", prixLigne);
                cmd.Parameters.AddWithValue("@taille", taille);
                cmd.Parameters.AddWithValue("@typePate", typePate);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.Parameters.AddWithValue("@idProduit", idProduit);
                cmd.Parameters.AddWithValue("@idCom", idCom);
                cmd.ExecuteNonQuery();
                int id = (int)cmd.LastInsertedId;
                maConnexion.Close();
                return id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        public void NewLigneIngredient(int idLigne, string ing, Boolean estSupp)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "INSERT INTO ligneingredient(idLigne, ingredient, ingredientEnPlus) VALUES(@idLigne, @ing, @estSupp)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@idLigne", idLigne);
                cmd.Parameters.AddWithValue("@ing", ing);
                cmd.Parameters.AddWithValue("@estSupp", estSupp);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void AllCommandeByDateAndType(DateTime date, bool estLivraison, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM commande WHERE DATE(dateHeure) = @date AND estLivraison = @estLivraison";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@estLivraison", estLivraison);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
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

        public void ClientById(int id, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM client WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void AllLigneByCommande(int idCommande, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM ligneCommande WHERE idCommande = @idCommande";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
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

        public void AllIngredientByLigne(int idLigne, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM ligneingredient WHERE idLigne = @idLigne";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@idLigne", idLigne);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
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
    }
}
