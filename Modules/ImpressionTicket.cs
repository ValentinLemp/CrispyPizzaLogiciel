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
            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            doc.Print();
        }

        private void ImpressionTicketGeneral()
        {
            Imprimer(g =>
            {
                float y = 10;
                float pageWidth = 295; // largeur utile (315 - marges)
                float xGauche = 5;
                float xDroite = pageWidth - 5;

                // ---- Polices ----
                Font fTitre = new Font("Arial", 11, FontStyle.Bold);
                Font fNormal = new Font("Arial", 8, FontStyle.Regular);
                Font fBold = new Font("Arial", 8, FontStyle.Bold);
                Font fGrandBold = new Font("Arial", 13, FontStyle.Bold);
                Font fMoyenBold = new Font("Arial", 10, FontStyle.Bold);

                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };
                StringFormat sfLeft = new StringFormat { Alignment = StringAlignment.Near };

                RectangleF rect = new RectangleF(xGauche, y, pageWidth, 20);

                // ---- En-tête restaurant ----
                int idRestaurant = Convert.ToInt32(commande["idRestaurant"]);
                string nomRestau, adresseR, villeR, tel;
                string tva = "N° TVA : BE1028813177";

                if (idRestaurant == 2)
                {
                    nomRestau = "Crispy Pizza Ramillies";
                    adresseR = "Avenue des Déportés 45A";
                    villeR = "1367 Ramillies";
                    tel = "Tel : 081/340.628";
                }
                else
                {
                    nomRestau = "Crispy Pizza Meux";
                    adresseR = "Rue Binia 62";
                    villeR = "5081 Meux";
                    tel = "Tel : 081/340.628";
                }

                g.DrawString(nomRestau, fTitre, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 20), sfCenter); y += 18;
                g.DrawString(adresseR, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(villeR, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tel, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tva, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 18;

                // ---- Type commande ----
                bool estLivraison = Convert.ToBoolean(commande["estLivraison"]);
                string titreCmd = estLivraison ? "COMMANDE LIVRAISON" : "COMMANDE SUR PLACE";
                g.DrawString(titreCmd, fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 26;

                // ---- Type statut ----
                int idStatut = Convert.ToInt32(commande["idStatut"]);
                string typeStatut = (idStatut == 2) ? "Internet" : "Restaurant";
                g.DrawString(typeStatut, fBold, Brushes.Black, xGauche, y); y += 14;

                // ---- Date impression ----
                g.DrawString("Prise de commande :", fNormal, Brushes.Black, xGauche, y);
                g.DrawString($"{DateTime.Now:dd/MM/yyyy  HH:mm:ss}", fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight); y += 14;

                // ---- Heure commande ----
                DateTime dateCommande = Convert.ToDateTime(commande["dateHeure"]);
                g.DrawString($"{dateCommande:HH:mm}", fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 26;

                // ---- Infos client ----
                string nomCmd = commande["nom"].ToString();
                string prenomCmd = commande["prenom"].ToString();
                g.DrawString(nomCmd, fBold, Brushes.Black, xGauche, y); y += 14;
                if (!string.IsNullOrEmpty(prenomCmd))
                { g.DrawString(prenomCmd, fNormal, Brushes.Black, xGauche, y); y += 14; }

                if (client != null)
                {
                    string telClient = client["numero"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(telClient))
                    { g.DrawString(telClient, fNormal, Brushes.Black, xGauche, y); y += 14; }

                    if (estLivraison && adresse != null)
                    {
                        string rue = $"{adresse["nomRue"]} {adresse["numeroRue"]}";
                        string codeVille = $"{adresse["codePostal"]} {adresse["localite"]}";
                        g.DrawString(rue, fNormal, Brushes.Black, xGauche, y); y += 14;
                        g.DrawString(codeVille, fNormal, Brushes.Black, xGauche, y); y += 14;
                    }
                }
                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                // ---- En-tête colonnes ----
                g.DrawString("ARTICLE", fBold, Brushes.Black, xGauche, y);
                g.DrawString("TOT", fBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 14;
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                // ---- Lignes de commande ----
                for (int i = 0; i < lignes.Rows.Count; i++)
                {
                    DataRow ligne = lignes.Rows[i];
                    DataRow produit = produits[i];
                    DataTable ings = ingredients[i];

                    string libelle = produit["libelle"].ToString().ToUpper();
                    int idTypeProduit = Convert.ToInt32(produit["idTypeProduit"]);
                    bool tailleLarge = Convert.ToBoolean(ligne["tailleLarge"]);
                    decimal prixLigne = Convert.ToDecimal(ligne["prixProduit"]);

                    // Formater le nom
                    string nomProduit;
                    if (idTypeProduit == 1) // pizza
                        nomProduit = tailleLarge ? $"MAXI PIZZA {libelle}" : $"PIZZA {libelle}";
                    else if (idTypeProduit == 2) // pâte
                    {
                        string typePate = ligne["typePate"]?.ToString().ToUpper() ?? "";
                        nomProduit = $"{typePate} {libelle}";
                    }
                    else if (produit["quantite"] != DBNull.Value)
                        nomProduit = $"{libelle} {produit["quantite"]}PC";
                    else
                        nomProduit = libelle;

                    // Ligne principale
                    g.DrawString(nomProduit, fNormal, Brushes.Black, xGauche, y);
                    g.DrawString(prixLigne.ToString("F2").Replace(".", ","), fNormal, Brushes.Black,
                                 new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                    y += 14;

                    // Ingrédients supprimés / ajoutés
                    foreach (DataRow ing in ings.Rows)
                    {
                        bool estSupp = Convert.ToBoolean(ing["ingredientEnPlus"]);
                        string prefix = estSupp ? "SUPP" : "SS";
                        string nomIng = ing["ingredient"].ToString();
                        string ligneIng = $"   {prefix} {nomIng}";
                        g.DrawString(ligneIng, fNormal, Brushes.Black, xGauche, y); y += 12;
                    }

                    // Note
                    string note = ligne["note"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(note))
                    {
                        string texteNote = $"   {note}";
                        SizeF tailleNote = g.MeasureString(texteNote, fNormal, (int)pageWidth);
                        g.DrawString(texteNote, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, tailleNote.Height));
                        y += tailleNote.Height;
                    }
                }
                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                // ---- Total ----
                decimal totalCommande = Convert.ToDecimal(commande["prixTotal"]);

                g.DrawString("TOTAL :", fBold, Brushes.Black, xGauche, y);
                g.DrawString($"{totalCommande:F2} €".Replace(".", ","), fBold, Brushes.Black,
                             new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 18;

                // ---- TVA ----
                g.DrawString("", fNormal, Brushes.Black, xGauche + 60, y);
                g.DrawString("HT", fNormal, Brushes.Black, xGauche + 130, y);
                g.DrawString("TVA", fNormal, Brushes.Black, xGauche + 180, y);
                g.DrawString("TTC", fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 14;

                decimal taux = 0.06m;
                decimal ht = Math.Round(totalCommande / (1 + taux), 2);
                decimal tvaVal = Math.Round(totalCommande - ht, 2);

                g.DrawString("6%(A)", fNormal, Brushes.Black, xGauche, y);
                g.DrawString(ht.ToString("F2").Replace(".", ","), fNormal, Brushes.Black, xGauche + 130, y);
                g.DrawString(tvaVal.ToString("F2").Replace(".", ","), fNormal, Brushes.Black, xGauche + 180, y);
                g.DrawString(totalCommande.ToString("F2").Replace(".", ","), fNormal, Brushes.Black,
                             new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 14;

                g.DrawLine(Pens.Black, xGauche + 120, y, xDroite, y); y += 6;

                g.DrawString(ht.ToString("F2").Replace(".", ","), fNormal, Brushes.Black, xGauche + 130, y);
                g.DrawString(tvaVal.ToString("F2").Replace(".", ","), fNormal, Brushes.Black, xGauche + 180, y);
                g.DrawString(totalCommande.ToString("F2").Replace(".", ","), fNormal, Brushes.Black,
                             new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 18;

                // ---- Pied de page ----
                g.DrawString("< MERCI DE VOTRE VISITE >", fBold, Brushes.Black,
                             new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 18;

                // ---- Signature (hash) - à intégrer avec le module fiscal (FDM) ----
                // string sign = commande["signature"]?.ToString() ?? "";
                // if (!string.IsNullOrEmpty(sign))
                // {
                //     Font fTiny = new Font("Arial", 6);
                //     g.DrawString($"Sign : {sign}", fTiny, Brushes.Black,
                //                  new RectangleF(xGauche, y, pageWidth, 12), sfCenter);
                // }
            });
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
