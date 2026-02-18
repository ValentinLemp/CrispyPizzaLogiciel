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

        public int AjouterClient(string nom, string prenom, string numero, int idAdresse, int idRest)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = "INSERT INTO client(nom, prenom, numero, idAdresse, idRestaurant) VALUES(@nom, @prenom, @numero, @idAdresse, @idRest)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@idAdresse", idAdresse);
                cmd.Parameters.AddWithValue("@idRest", idRest);
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

        public void AllCommandeByDateAndType(DateTime date, bool estLivraison, int idRest, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM commande WHERE DATE(dateHeure) = @date AND estLivraison = @estLivraison AND idRestaurant = @idRest";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@estLivraison", estLivraison);
                cmd.Parameters.AddWithValue("@idRest", idRest);
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
        public void AllClientsByIdRestaurant(int id, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = @"SELECT c.id, c.nom, c.prenom, c.numero,
                    a.nomRue, a.numeroRue, a.codePostal, a.localite
                    FROM client c LEFT JOIN adresse a ON c.idAdresse = a.id
                    WHERE c.idRestaurant = @id";
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

        // ---- UPDATE Commande ----
        public void UpdateCommande(int idCommande, string nom, string prenom, bool livraison, DateTime date, double prixTotal, string commentaire, int idStatut, int? idClient)
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
        public void UpdateLigne(int idLigne, double prixProduit, bool tailleLarge, string typePate, string note, int idProduit)
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
        public void UpdateAdresse(int idAdresse, double codePostal, string nomRue, string numeroRue, string localite)
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
        public double TotalCommandeByDateAndStatut(DateTime date, int idStatut, int idRest)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"SELECT IFNULL(SUM(prixTotal), 0) FROM commande
                    WHERE DATE(dateHeure) = @date AND idStatut != 3 AND idStatut = @idStatut AND idRestaurant = @idRest";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idStatut", idStatut);
                cmd.Parameters.AddWithValue("@idRest", idRest);
                double total = Convert.ToDouble(cmd.ExecuteScalar());
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
        public void CompteProduitsByDate(DateTime date, int idRest, out DataTable dataT)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = @"SELECT p.idTypeProduit, lc.tailleLarge, COUNT(*) AS nombre
                    FROM ligneCommande lc
                    INNER JOIN commande c ON lc.idCommande = c.id
                    INNER JOIN produit p ON lc.idProduit = p.id
                    WHERE DATE(c.dateHeure) = @date AND c.idStatut != 3 AND c.idRestaurant = @idRest
                    GROUP BY p.idTypeProduit, lc.tailleLarge";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idRest", idRest);
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
        // ---- CLOTURE : Passer toutes les commandes du jour en idStatut = 3 ----
        public void CloturerCommandes(DateTime date, int idRest)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"UPDATE commande SET idStatut = 3
                    WHERE DATE(dateHeure) = @date AND idStatut != 3 AND idRestaurant = @idRest";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idRest", idRest);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        // ---- CLOTURE : Insérer une clôture ----
        public void InsererCloture(DateTime date, double argentRestaurant, double argentInternet,
            double argentCarte, double argentLiquide, double argentDifference,
            int nbMaxiPizza, int nbPizza, int nbPate, int nbPain, int nbSalade, int nbSnacking,
            int nbBoisson, int nbDessert, int nbDipy, int idRestaurant)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

            try
            {
                maConnexion.Open();
                string query = @"INSERT INTO cloture(date, argentRestaurant, argentInternet,
                    argentCarte, argentLiquide, argentDifference,
                    nbMaxiPizza, nbPizza, nbPâte, nbPain, nbSalade, nbSnacking,
                    nbBoisson, nbDessert, nbDipy, idRestaurant)
                    VALUES(@date, @argentRest, @argentNet,
                    @argentCarte, @argentLiquide, @argentDiff,
                    @nbMaxiPizza, @nbPizza, @nbPate, @nbPain, @nbSalade, @nbSnacking,
                    @nbBoisson, @nbDessert, @nbDipy, @idRest)";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
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
                cmd.Parameters.AddWithValue("@idRest", idRestaurant);
                cmd.ExecuteNonQuery();
                maConnexion.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        // ---- HISTORIQUE : Toutes les clôtures par restaurant ----
        public void AllClotureByRestaurant(int idRest, out DataTable dataT, out int nbrRow)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());
            dataT = new DataTable();

            try
            {
                string query = "SELECT * FROM cloture WHERE idRestaurant = @idRest ORDER BY date DESC";
                MySqlCommand cmd = new MySqlCommand(query, maConnexion);
                cmd.Parameters.AddWithValue("@idRest", idRest);
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

        // ---- SUGGESTION : Ajouter ----
        public void AjouterSuggestion(string libelle, string description, double prix, double? prixLarge, int idTypeProduit)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

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
        public void ModifierSuggestion(int id, string libelle, string description, double prix, double? prixLarge, int idTypeProduit)
        {
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

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
            MySqlConnection maConnexion = new MySqlConnection(DefinirCheminBD());

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
    }
}
