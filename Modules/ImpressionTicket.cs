using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrispyLo.Modules
{
    internal class ImpressionTicket
    {
        BaseDeDonnée sql = new BaseDeDonnée();
        DataRow commande;
        DataTable lignes;
        List<DataTable> ingredients = new List<DataTable>();
        List<DataRow> produits = new List<DataRow>();
        DataRow client;
        DataRow adresse;

        // Listes d'index par cuisine
        List<int> indexCuisine1 = new List<int>();
        List<int> indexCuisine2 = new List<int>();
        List<int> indexAutre = new List<int>();

        public void GeneralImpression(int idCommande)
        {
            // ---- Récupérer la commande ----
            DataTable dataCommande;
            sql.CommandeById(idCommande, out dataCommande);
            if (dataCommande.Rows.Count == 0) return;
            commande = dataCommande.Rows[0];

            // ---- Récupérer les lignes ----
            int nbrLignes;
            sql.AllLigneByCommande(idCommande, out lignes, out nbrLignes);

            // ---- Récupérer les ingrédients et produit de chaque ligne ----
            ingredients.Clear();
            produits.Clear();
            indexCuisine1.Clear();
            indexCuisine2.Clear();
            indexAutre.Clear();

            for (int i = 0; i < nbrLignes; i++)
            {
                int idLigne = Convert.ToInt32(lignes.Rows[i]["id"]);
                int idProduit = Convert.ToInt32(lignes.Rows[i]["idProduit"]);

                // Ingrédients
                DataTable dataIng;
                int nbrIng;
                sql.AllIngredientByLigne(idLigne, out dataIng, out nbrIng);
                ingredients.Add(dataIng);

                // Produit
                DataTable dataProduit;
                sql.ProduitById(idProduit, out dataProduit);
                DataRow produit = dataProduit.Rows[0];
                produits.Add(produit);

                // Trier par cuisine
                int idTypeProduit = Convert.ToInt32(produit["idTypeProduit"]);
                string libelle = produit["libelle"].ToString().ToLower();

                if (idTypeProduit == 1 || idTypeProduit == 2 || idTypeProduit == 4) // pizza, pâte, pain
                {
                    indexCuisine1.Add(i);
                }
                else if (idTypeProduit == 3 || idTypeProduit == 5 || idTypeProduit == 8) // salade, snacking, dipy
                {
                    indexCuisine2.Add(i);
                }
                else if (idTypeProduit == 6 && libelle == "moelleux au chocolat") // dessert spécial → cuisine2
                {
                    indexCuisine2.Add(i);
                }
                else // dessert (6), boisson (7)
                {
                    indexAutre.Add(i);
                }
            }

            // ---- Récupérer le client + adresse si existe ----
            client = null;
            adresse = null;
            if (commande["idClient"] != DBNull.Value)
            {
                int idClient = Convert.ToInt32(commande["idClient"]);
                DataTable dataClient;
                sql.ClientById(idClient, out dataClient);
                if (dataClient.Rows.Count > 0)
                {
                    client = dataClient.Rows[0];
                    if (client["idAdresse"] != DBNull.Value)
                    {
                        int idAdresse = Convert.ToInt32(client["idAdresse"]);
                        DataTable dataAdresse;
                        sql.AdresseByClient(idAdresse, out dataAdresse);
                        if (dataAdresse.Rows.Count > 0)
                            adresse = dataAdresse.Rows[0];
                    }
                }
            }

            // ---- Appeler les impressions ----
            ImpressionTicketGeneral();

            if (indexCuisine1.Count > 0)
                ImpressionCuisine1();

            if (indexCuisine2.Count > 0)
                ImpressionCuisine2();

            if (indexAutre.Count > 0)
                ImpressionAutre();
        }

        private void Imprimer(Action<Graphics> dessiner)
        {
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Ticket", 315, 1200); // 80mm ≈ 315 centièmes de pouce
            doc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
            doc.PrintPage += (s, e) =>
            {
                dessiner(e.Graphics);
                e.HasMorePages = false;
            };
            doc.Print();
        }

        private void ImpressionTicketGeneral()
        {
            // ici code impression ticket général
        }

        private void ImpressionCuisine1()
        {
            // ici code impression cuisine 1 (pizza, pâte, pain)
            // utiliser indexCuisine1 pour accéder aux lignes, ingrédients et produits
        }

        private void ImpressionCuisine2()
        {
            // ici code impression cuisine 2 (salade, snacking, dipy, moelleux au chocolat)
            // utiliser indexCuisine2 pour accéder aux lignes, ingrédients et produits
        }

        private void ImpressionAutre()
        {
            // ici code impression autre (dessert, boisson)
            // utiliser indexAutre pour accéder aux lignes, ingrédients et produits
        }
    }
}
