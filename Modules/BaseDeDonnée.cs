using CrispyLo.Views.Info;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
//Ajout des using pour la Base de donnée
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace CrispyLo.Modules
{
    internal class BaseDeDonnée
    {
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;

        public string DefinirCheminBD()
        {
            return PagePrincipale.ChemminDB;
        }
        public string DefinirCheminCommun()
        {
            return "Server=localhost;Database=crispy_pizza_commun;port=3306;User Id=root;password=root";
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

        public void ModificationMDPById(object id, string nouveauMdp)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "UPDATE utilisateur SET motdepasse = @mdp WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@mdp", nouveauMdp);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void StatutById(int id, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM statut WHERE id = @id";
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

        /*public void RestaurentById(int id, out DataTable dataT)
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
        }*/

        public void AllProduitByTypeProduit(int id, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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

        public bool ClientEstLieACommande(int idClient)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "SELECT COUNT(*) FROM commande WHERE idClient = @idClient";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@idClient", idClient);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                maConnexion.Close();
                return count > 0;
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

        public int AjouterAdresse(int codePostal, string nomRue, string numeroRue, string localite)
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

        public int NewCommande(string nom, string prenom, Boolean livraison, DateTime date, decimal prixTotal, string commentaire, int idStatut, int? idClient)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"INSERT INTO commande(numeroCommande, nom, prenom, estLivraison, dateHeure, prixTotal, commetaire, idStatut, idClient, estVu, estCloture)
                        VALUES(@numCo, @nom, @prenom, @livraison, @date, @prixTotal, @commentaire, @idStatut, @idClient, 1, 0)";

                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                int numCom = CountCommande(date) + 1;

                cmd.Parameters.AddWithValue("@numCo", numCom);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@livraison", livraison);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@prixTotal", prixTotal);
                cmd.Parameters.AddWithValue("@commentaire", commentaire);
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
        public int CountCommande(DateTime date)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            try
            {
                maConnexion.Open();
                string query = "SELECT COUNT(*) FROM commande WHERE estCloture = 0";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                maConnexion.Close();
                return count;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        public int NewLigne(decimal prixLigne, Boolean taille, string typePate, string note, int idProduit, int idCom)
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

        public void CommandeById(int id, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM commande WHERE id = @id";
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
        public void AllClient(out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM client";

                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
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

        // ---- UPDATE Commande ----
        public void UpdateCommande(int idCommande, string nom, string prenom, bool livraison, DateTime date, decimal prixTotal, string commentaire, int idStatut, int? idClient)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"UPDATE commande SET nom = @nom, prenom = @prenom, estLivraison = @livraison,
                    dateHeure = @date, prixTotal = @prixTotal, commetaire = @commentaire,
                    idStatut = @idStatut, idClient = @idClient WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@livraison", livraison);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@prixTotal", prixTotal);
                cmd.Parameters.AddWithValue("@commentaire", commentaire);
                cmd.Parameters.AddWithValue("@idStatut", idStatut);
                if (idClient.HasValue)
                    cmd.Parameters.AddWithValue("@idClient", idClient.Value);
                else
                    cmd.Parameters.AddWithValue("@idClient", DBNull.Value);
                cmd.Parameters.AddWithValue("@id", idCommande);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- UPDATE LigneCommande ----
        public void UpdateLigne(int idLigne, decimal prixProduit, bool tailleLarge, string typePate, string note, int idProduit)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"UPDATE ligneCommande SET prixProduit = @prix, tailleLarge = @taille,
                    typePate = @typePate, note = @note, idProduit = @idProduit WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@prix", prixProduit);
                cmd.Parameters.AddWithValue("@taille", tailleLarge);
                cmd.Parameters.AddWithValue("@typePate", typePate);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.Parameters.AddWithValue("@idProduit", idProduit);
                cmd.Parameters.AddWithValue("@id", idLigne);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- DELETE Ingredients d'une ligne ----
        public void SupprimerIngredientsByLigne(int idLigne)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "DELETE FROM ligneingredient WHERE idLigne = @idLigne";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@idLigne", idLigne);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- DELETE une ligne + ses ingrédients ----
        public void SupprimerLigne(int idLigne)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();

                string queryIng = "DELETE FROM ligneingredient WHERE idLigne = @idLigne";
                MySqlCommand cmdIng = new MySqlCommand(queryIng, maConnexion);
                cmdIng.Parameters.AddWithValue("@idLigne", idLigne);
                cmdIng.ExecuteNonQuery();

                string queryLigne = "DELETE FROM ligneCommande WHERE id = @idLigne2";
                MySqlCommand cmdLigne = new MySqlCommand(queryLigne, maConnexion);
                cmdLigne.Parameters.AddWithValue("@idLigne2", idLigne);
                cmdLigne.ExecuteNonQuery();

                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- UPDATE Client ----
        public void UpdateClient(int idClient, string nom, string prenom, string numero)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "UPDATE client SET nom = @nom, prenom = @prenom, numero = @numero WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@id", idClient);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- DELETE Commande (+ lignes + ingrédients, sans toucher client/adresse) ----
        public void SupprimerCommande(int idCommande)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();

                // Supprimer les ingrédients de toutes les lignes de cette commande
                string queryIng = @"DELETE li FROM ligneingredient li
                    INNER JOIN ligneCommande lc ON li.idLigne = lc.id
                    WHERE lc.idCommande = @idCommande";
                MySqlCommand cmdIng = new MySqlCommand(queryIng, maConnexion);
                cmdIng.Parameters.AddWithValue("@idCommande", idCommande);
                cmdIng.ExecuteNonQuery();

                // Supprimer les lignes de commande
                string queryLignes = "DELETE FROM ligneCommande WHERE idCommande = @idCommande2";
                MySqlCommand cmdLignes = new MySqlCommand(queryLignes, maConnexion);
                cmdLignes.Parameters.AddWithValue("@idCommande2", idCommande);
                cmdLignes.ExecuteNonQuery();

                // Supprimer la commande
                string queryCom = "DELETE FROM commande WHERE id = @idCommande3";
                MySqlCommand cmdCom = new MySqlCommand(queryCom, maConnexion);
                cmdCom.Parameters.AddWithValue("@idCommande3", idCommande);
                cmdCom.ExecuteNonQuery();

                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- UPDATE Adresse ----
        public void UpdateAdresse(int idAdresse, int codePostal, string nomRue, string numeroRue, string localite)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "UPDATE adresse SET codePostal = @codePostal, nomRue = @nomRue, numeroRue = @numeroRue, localite = @localite WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@codePostal", codePostal);
                cmd.Parameters.AddWithValue("@nomRue", nomRue);
                cmd.Parameters.AddWithValue("@numeroRue", numeroRue);
                cmd.Parameters.AddWithValue("@localite", localite);
                cmd.Parameters.AddWithValue("@id", idAdresse);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        // ---- CLOTURE : Total prixTotal par statut pour aujourd'hui ----
        public decimal TotalCommandeByDateAndStatut(DateTime date, int idStatut)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"SELECT IFNULL(SUM(prixTotal), 0) FROM commande
                    WHERE DATE(dateHeure) = @date AND estCloture = 0 AND idStatut = @idStatut";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idStatut", idStatut);
                decimal total = Convert.ToDecimal(cmd.ExecuteScalar());
                maConnexion.Close();
                return total;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- CLOTURE : Comptage produits vendus aujourd'hui ----
        public void CompteProduitsByDate(DateTime date, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = @"SELECT p.idTypeProduit, lc.tailleLarge, COUNT(*) AS nombre
                    FROM ligneCommande lc
                    INNER JOIN commande c ON lc.idCommande = c.id
                    INNER JOIN crispy_pizza_commun.produit p ON lc.idProduit = p.id
                    WHERE DATE(c.dateHeure) = @date AND c.estCloture = 0
                    GROUP BY p.idTypeProduit, lc.tailleLarge";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
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
        // ---- CLOTURE : Passer toutes les commandes du jour en estCloture = 1 ----
        public void CloturerCommandes(DateTime date)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"UPDATE commande SET estCloture = 1
                    WHERE DATE(dateHeure) = @date AND estCloture = 0";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        // ---- CLOTURE : Nombre de clôtures par restaurant ----
        public int NombreClotureByRestaurant()
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            try
            {
                maConnexion.Open();
                string query = "SELECT COUNT(*) FROM cloture";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                maConnexion.Close();
                return count;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        // ---- CLOTURE : Insérer une clôture ----
        public void NewCloture(DateTime date, decimal argentRestaurant, decimal argentInternet,
            decimal argentCarte, decimal argentLiquide, decimal argentDifference,
            int nbMaxiPizza, int nbPizza, int nbPate, int nbPain, int nbSalade, int nbSnacking,
            int nbBoisson, int nbDessert, int nbDipy, string signature, string signaturePrec)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();

                // ---- Calculer premier/dernier ticket + totaux depuis ticketVente ----
                string dateStr = date.ToString("yyyy-MM-dd");
                string queryStats = @"SELECT * FROM ticketVente WHERE DATE(dateValidation) = @date";
                MySqlCommand cmdStats = new MySqlCommand(queryStats, maConnexion);
                cmdStats.Parameters.AddWithValue("@date", dateStr);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmdStats);
                DataTable dataT = new DataTable();
                Data.Fill(dataT);
                int count = dataT.Rows.Count;
                Traitement.calculCloture(dataT, count, out int premierTicket, out int dernierTicket, out decimal total_ht, out decimal total_tva, out decimal total_ttc, out string heureDebut, out string heureFin, out int totalTicket, out int totalAnnuler);

                int numeroCloture = NombreClotureByRestaurant() + 1;
                string query = @"INSERT INTO cloture(numeroCloture, date, argentRestaurant, argentInternet,
                    argentCarte, argentLiquide, argentDifference,
                    nbMaxiPizza, nbPizza, nbPâte, nbPain, nbSalade, nbSnacking,
                    nbBoisson, nbDessert, nbDipy,
                    premierTicket, dernierTicket, total_ht, total_tva, total_ttc,
                    heureDebut, heureFin, totalTicket, totalAnnuler, signature, signature_prec)
                    VALUES(@numCloture, @date, @argentRest, @argentNet,
                    @argentCarte, @argentLiquide, @argentDiff,
                    @nbMaxiPizza, @nbPizza, @nbPate, @nbPain, @nbSalade, @nbSnacking,
                    @nbBoisson, @nbDessert, @nbDipy,
                    @premierTicket, @dernierTicket, @total_ht, @total_tva, @total_ttc,
                    @heureDebut, @heureFin, @totalTicket, @totalAnnuler, @signature, @signaturePrec)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@numCloture", numeroCloture);
                cmd.Parameters.AddWithValue("@date", dateStr);
                cmd.Parameters.AddWithValue("@argentRest", argentRestaurant);
                cmd.Parameters.AddWithValue("@argentNet", argentInternet);
                cmd.Parameters.AddWithValue("@argentCarte", argentCarte);
                cmd.Parameters.AddWithValue("@argentLiquide", argentLiquide);
                cmd.Parameters.AddWithValue("@argentDiff", argentDifference);
                cmd.Parameters.AddWithValue("@nbMaxiPizza", nbMaxiPizza);
                cmd.Parameters.AddWithValue("@nbPizza", nbPizza);
                cmd.Parameters.AddWithValue("@nbPate", nbPate);
                cmd.Parameters.AddWithValue("@nbPain", nbPain);
                cmd.Parameters.AddWithValue("@nbSalade", nbSalade);
                cmd.Parameters.AddWithValue("@nbSnacking", nbSnacking);
                cmd.Parameters.AddWithValue("@nbBoisson", nbBoisson);
                cmd.Parameters.AddWithValue("@nbDessert", nbDessert);
                cmd.Parameters.AddWithValue("@nbDipy", nbDipy);
                cmd.Parameters.AddWithValue("@premierTicket", premierTicket);
                cmd.Parameters.AddWithValue("@dernierTicket", dernierTicket);
                cmd.Parameters.AddWithValue("@total_ht", total_ht);
                cmd.Parameters.AddWithValue("@total_tva", total_tva);
                cmd.Parameters.AddWithValue("@total_ttc", total_ttc);
                cmd.Parameters.AddWithValue("@heureDebut", heureDebut);
                cmd.Parameters.AddWithValue("@heureFin", heureFin);
                cmd.Parameters.AddWithValue("@totalTicket", totalTicket);
                cmd.Parameters.AddWithValue("@totalAnnuler", totalAnnuler);
                cmd.Parameters.AddWithValue("@signature", signature);
                cmd.Parameters.AddWithValue("@signaturePrec", signaturePrec);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        public void DerniereCloture(out DataTable dataT, out int count)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM cloture ORDER BY id DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                MySqlDataAdapter Data = new MySqlDataAdapter(cmd);
                Data.Fill(dataT);
                count = dataT.Rows.Count;
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- HISTORIQUE : Toutes les clôtures par restaurant ----
        public void AllCloture(out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM cloture ORDER BY date DESC";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
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
        // ---- SUGGESTION : Toutes les suggestions ----
        public void AllSuggestions(out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());
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

        // ---- SUGGESTION : Ajouter ----
        public void AjouterSuggestion(string libelle, string description, decimal prix, decimal? prixLarge, int idTypeProduit)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());

            try
            {
                maConnexion.Open();
                string query = @"INSERT INTO produit(libelle, description, quantite, prix, prixLarge, centilitre, urlImage, idTypePizza, idTypeProduit, estSuggestion)
                    VALUES(@libelle, @description, NULL, @prix, @prixLarge, NULL, '/images/suggestions/suggestion', NULL, @idTypeProduit, 1)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@libelle", libelle);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@prix", prix);
                if (prixLarge.HasValue)
                    cmd.Parameters.AddWithValue("@prixLarge", prixLarge.Value);
                else
                    cmd.Parameters.AddWithValue("@prixLarge", DBNull.Value);
                cmd.Parameters.AddWithValue("@idTypeProduit", idTypeProduit);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- SUGGESTION : Modifier ----
        public void ModifierSuggestion(int id, string libelle, string description, decimal prix, decimal? prixLarge, int idTypeProduit)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());

            try
            {
                maConnexion.Open();
                string query = @"UPDATE produit SET libelle = @libelle, description = @description, prix = @prix,
                    prixLarge = @prixLarge, idTypeProduit = @idTypeProduit WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@libelle", libelle);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@prix", prix);
                if (prixLarge.HasValue)
                    cmd.Parameters.AddWithValue("@prixLarge", prixLarge.Value);
                else
                    cmd.Parameters.AddWithValue("@prixLarge", DBNull.Value);
                cmd.Parameters.AddWithValue("@idTypeProduit", idTypeProduit);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- SUGGESTION : Supprimer ----
        public void SupprimerSuggestion(int id)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminCommun());

            try
            {
                maConnexion.Open();
                string query = "DELETE FROM produit WHERE id = @id AND estSuggestion = 1";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        // ---- INTERNET : Récupérer les commandes non vues ----
        public void CommandesNonVues(out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM commande WHERE estVu = 0 AND idStatut = 2";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
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

        // ---- INTERNET : Marquer une commande comme vue ----
        public void MarquerCommandeVue(int idCommande)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "UPDATE commande SET estVu = 1 WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", idCommande);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void UpdateTicketidCom(int id)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "UPDATE ticketVente SET idCom = -1 WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void TicketByCommande(int idCom, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM ticketVente WHERE idCom = @idCom LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@idCom", idCom);
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

        public string DernierSignature()
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            try
            {
                maConnexion.Open();
                string query = "SELECT signature FROM ticketVente ORDER BY id DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                object result = cmd.ExecuteScalar();
                maConnexion.Close();
                return (result != null && result != DBNull.Value) ? result.ToString()! : "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }
        }

        public void newTicket(string type, DateTime date, decimal total_ht, decimal total_tva, decimal total_ttc, string signature, string signaturePrec,int ticketRef, int idCom)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"INSERT INTO ticketVente(type, caisse, dateValidation, total_ht, total_tva, total_ttc, signature, signature_prec, ticket_ref, idCom)
                        VALUES(@type, 1, @date, @total_ht, @total_tva, @total_ttc, @signature, @signaturePrec, @ticket_ref , @idCom)";

                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                int numCom = CountCommande(date) + 1;

                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@total_ht", total_ht);
                cmd.Parameters.AddWithValue("@total_tva", total_tva);
                cmd.Parameters.AddWithValue("@total_ttc", total_ttc);
                cmd.Parameters.AddWithValue("@signature", signature);
                cmd.Parameters.AddWithValue("@signaturePrec", signaturePrec);
                cmd.Parameters.AddWithValue("@ticket_ref", ticketRef);
                cmd.Parameters.AddWithValue("@idCom", idCom);
                cmd.ExecuteNonQuery();

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
