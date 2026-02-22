using Org.BouncyCastle.Bcpg.Sig;
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
        MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;

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
                    if (idTypeProduit == 2)
                    {
                        indexCuisine2.Add(i);
                    }
                }
                else if (idTypeProduit == 3 || idTypeProduit == 5 || idTypeProduit == 8) // pate, salade, snacking, dipy
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
            doc.DefaultPageSettings.PaperSize = new PaperSize("Ticket", 299, 1200); // 76mm ≈ 315 centièmes de pouce
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
            Imprimer(g =>
            {
                float y = 10;
                float pageWidth = 289; // largeur utile (315 - marges)
                float xGauche = 5;
                float xArticle = xGauche + 35;
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
                string nomRestau, adresseR, villeR, tel;
                string tva = "N° TVA : BE1028813177";

                if (PagePrincipale.Restaurant == 2)
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
                g.DrawString(titreCmd, fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 18;

                // ---- Commande N ----
                g.DrawString($"N° : {commande["numeroCommande"]}", fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 26;

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

                y += 6;

                // ---- Commentaire ----
                string commentaireCmd = commande["commetaire"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(commentaireCmd))
                {
                    g.DrawString("Commentaire :", fBold, Brushes.Black, xGauche, y); y += 14;
                    SizeF tailleComm = g.MeasureString(commentaireCmd, fNormal, (int)pageWidth);
                    g.DrawString(commentaireCmd, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, tailleComm.Height));
                    y += tailleComm.Height + 4;
                }

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                // ---- En-tête colonnes ----
                g.DrawString("Qte", fBold, Brushes.Black, xGauche, y);
                g.DrawString("ARTICLE", fBold, Brushes.Black, xArticle, y);
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
                        string typePate = (ligne["typePate"].ToString() != "" ? ligne["typePate"].ToString().ToUpper() + " " : "");
                        nomProduit = $"{typePate}{libelle}";
                    }
                    else if (produit["quantite"] != DBNull.Value)
                        nomProduit = $"{libelle} {produit["quantite"]}PC";
                    else
                        nomProduit = libelle;

                    // Ligne principale
                    g.DrawString("1", fNormal, Brushes.Black, xGauche, y);
                    g.DrawString(nomProduit, fNormal, Brushes.Black, xArticle, y);
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
                        g.DrawString(ligneIng, fNormal, Brushes.Black, xArticle, y); y += 12;
                    }

                    // Note
                    string note = ligne["note"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(note))
                    {
                        string texteNote = $"   {note}";
                        SizeF tailleNote = g.MeasureString(texteNote, fNormal, (int)pageWidth);
                        g.DrawString(texteNote, fNormal, Brushes.Black, new RectangleF(xArticle, y, pageWidth, tailleNote.Height));
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

                // ---- Récupérer ticket depuis DB ----
                int idCom = Convert.ToInt32(commande["id"]);
                sql.TicketByCommande(idCom, out DataTable dataTicket);
                decimal ht, tvaVal;
                string signature = "";
                if (dataTicket.Rows.Count > 0)
                {
                    ht = Convert.ToDecimal(dataTicket.Rows[0]["total_ht"]);
                    tvaVal = Convert.ToDecimal(dataTicket.Rows[0]["total_tva"]);
                    signature = dataTicket.Rows[0]["signature"]?.ToString() ?? "";
                }
                else
                {
                    decimal taux = 0.06m;
                    ht = Math.Round(totalCommande / (1 + taux), 2);
                    tvaVal = Math.Round(totalCommande - ht, 2);
                }

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

                // ---- Signature ----
                if (!string.IsNullOrEmpty(signature))
                {
                    Font fTiny = new Font("Arial", 6);
                    g.DrawString($"sign : {signature}", fTiny, Brushes.Black,
                                 new RectangleF(xGauche, y, pageWidth, 12), sfCenter);
                }
            });
        }

        private void ImpressionCuisine1()
        {
            Imprimer(g =>
            {
                float y = 10;
                float pageWidth = 289;
                float xGauche = 5;
                float xArticle = xGauche + 35;
                float xDroite = pageWidth - 5;

                Font fNormal = new Font("Arial", 8, FontStyle.Regular);
                Font fBold = new Font("Arial", 8, FontStyle.Bold);
                Font fGrandBold = new Font("Arial", 14, FontStyle.Bold);
                Font fNumero = new Font("Arial", 36, FontStyle.Bold);
                Font fTitre = new Font("Arial", 13, FontStyle.Bold);
                Font fEmporte = new Font("Arial", 11, FontStyle.Bold);

                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };

                // ---- Date + Heure sur la même ligne ----
                g.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), fNormal, Brushes.Black, xGauche, y);
                g.DrawString(DateTime.Now.ToString("HH:mm:ss"), fNormal, Brushes.Black,
                                new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 20;

                // ---- Titre Restaurant / Internet ----
                int idStatut = Convert.ToInt32(commande["idStatut"]);
                string titreCuisine = (idStatut == 2) ? "INTERNET" : "RESTAURANT";
                RectangleF rectTitre = new RectangleF(xGauche, y, pageWidth, 22);
                g.FillRectangle(Brushes.Black, rectTitre);
                g.DrawString(titreCuisine, fTitre, Brushes.White, rectTitre, sfCenter);
                y += 28;

                // ---- Commande N ----
                g.DrawString($"Commande N : {commande["numeroCommande"]}", fBold, Brushes.Black, xGauche, y); y += 18;

                // ---- Heure commande ----
                DateTime dateCommande = Convert.ToDateTime(commande["dateHeure"]);
                g.DrawString($"{dateCommande:HH:mm}", fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 26;

                // ---- Nom + Prénom client ----
                string nomCmd = commande["nom"].ToString();
                string prenomCmd = commande["prenom"].ToString();
                g.DrawString(nomCmd, fBold, Brushes.Black, xGauche, y); y += 14;
                if (!string.IsNullOrEmpty(prenomCmd))
                { g.DrawString(prenomCmd, fNormal, Brushes.Black, xGauche, y); y += 14; }
                
                y += 4;

                y += 6;

                // ---- Commentaire ----
                string commentaireCmd = commande["commetaire"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(commentaireCmd))
                {
                    g.DrawString("Commentaire :", fBold, Brushes.Black, xGauche, y); y += 14;
                    SizeF tailleComm = g.MeasureString(commentaireCmd, fNormal, (int)pageWidth);
                    g.DrawString(commentaireCmd, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, tailleComm.Height));
                    y += tailleComm.Height + 4;
                }

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                // ---- En-tête colonnes ----
                g.DrawString("Qte", fBold, Brushes.Black, xGauche, y);
                g.DrawString("ARTICLE", fBold, Brushes.Black, xArticle, y);
                y += 14;
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Lignes articles (cuisine1 seulement) ----
                foreach (int i in indexCuisine1)
                {
                    DataRow ligne = lignes.Rows[i];
                    DataRow produit = produits[i];
                    DataTable ings = ingredients[i];

                    string libelle = produit["libelle"].ToString().ToUpper();
                    int idTypeProduit = Convert.ToInt32(produit["idTypeProduit"]);
                    bool tailleLarge = Convert.ToBoolean(ligne["tailleLarge"]);

                    // Formater le nom
                    string nomProduit;
                    if (idTypeProduit == 1)
                        nomProduit = tailleLarge ? $"MAXI PIZZA {libelle}" : $"PIZZA {libelle}";
                    else if (idTypeProduit == 2)
                    {
                        string typePate = (ligne["typePate"].ToString() != "" ? ligne["typePate"].ToString().ToUpper() + " " : "");
                        nomProduit = $"{typePate}{libelle}";
                    }
                    else
                        nomProduit = libelle;

                    g.DrawString("1", fNormal, Brushes.Black, xGauche, y);
                    g.DrawString(nomProduit, fNormal, Brushes.Black, xArticle, y);
                    y += 16;

                    // Ingrédients modifiés
                    foreach (DataRow ing in ings.Rows)
                    {
                        bool estSupp = Convert.ToBoolean(ing["ingredientEnPlus"]);
                        string prefix = estSupp ? "SUPP" : "SS";
                        string nomIng = ing["ingredient"].ToString();
                        g.DrawString($"   {prefix} {nomIng}", fNormal, Brushes.Black, xArticle, y);
                        y += 14;
                    }

                    // Note
                    string note = ligne["note"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(note))
                    {
                        string texteNote = $"   {note}";
                        SizeF tailleNote = g.MeasureString(texteNote, fNormal, (int)pageWidth);
                        g.DrawString(texteNote, fNormal, Brushes.Black, new RectangleF(xArticle, y, pageWidth, tailleNote.Height));
                        y += tailleNote.Height;
                    }
                }

                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                y += 6;
                // ---- Type commande encadré ----
                bool estLivraison = Convert.ToBoolean(commande["estLivraison"]);
                string typeCmd = estLivraison ? "LIVRAISON" : "EMPORTER";
                RectangleF rectType = new RectangleF(xGauche + 60, y, pageWidth - 120, 22);
                g.DrawRectangle(Pens.Black, rectType.X, rectType.Y, rectType.Width, rectType.Height);
                g.DrawString(typeCmd, fEmporte, Brushes.Black, rectType, sfCenter);
                y += 28;
            });
        }

        private void ImpressionCuisine2()
        {
            Imprimer(g =>
            {
                float y = 10;
                float pageWidth = 289;
                float xGauche = 5;
                float xArticle = xGauche + 35;
                float xDroite = pageWidth - 5;

                Font fNormal = new Font("Arial", 8, FontStyle.Regular);
                Font fBold = new Font("Arial", 8, FontStyle.Bold);
                Font fGrandBold = new Font("Arial", 14, FontStyle.Bold);
                Font fNumero = new Font("Arial", 36, FontStyle.Bold);
                Font fTitre = new Font("Arial", 13, FontStyle.Bold);
                Font fEmporte = new Font("Arial", 11, FontStyle.Bold);

                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };

                // ---- Date + Heure sur la même ligne ----
                g.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), fNormal, Brushes.Black, xGauche, y);
                g.DrawString(DateTime.Now.ToString("HH:mm:ss"), fNormal, Brushes.Black,
                                new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 20;

                // ---- Titre Restaurant / Internet ----
                int idStatut = Convert.ToInt32(commande["idStatut"]);
                string titreCuisine = (idStatut == 2) ? "INTERNET" : "RESTAURANT";
                RectangleF rectTitre = new RectangleF(xGauche, y, pageWidth, 22);
                g.FillRectangle(Brushes.Black, rectTitre);
                g.DrawString(titreCuisine, fTitre, Brushes.White, rectTitre, sfCenter);
                y += 28;

                // ---- Commande N ----
                g.DrawString($"Commande N : {commande["numeroCommande"]}", fBold, Brushes.Black, xGauche, y); y += 18;

                // ---- Heure commande ----
                DateTime dateCommande = Convert.ToDateTime(commande["dateHeure"]);
                g.DrawString($"{dateCommande:HH:mm}", fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 26;

                // ---- Nom + Prénom client ----
                string nomCmd = commande["nom"].ToString();
                string prenomCmd = commande["prenom"].ToString();
                g.DrawString(nomCmd, fBold, Brushes.Black, xGauche, y); y += 14;
                if (!string.IsNullOrEmpty(prenomCmd))
                { g.DrawString(prenomCmd, fNormal, Brushes.Black, xGauche, y); y += 14; }
                y += 4;

                y += 6;

                // ---- Commentaire ----
                string commentaireCmd = commande["commetaire"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(commentaireCmd))
                {
                    g.DrawString("Commentaire :", fBold, Brushes.Black, xGauche, y); y += 14;
                    SizeF tailleComm = g.MeasureString(commentaireCmd, fNormal, (int)pageWidth);
                    g.DrawString(commentaireCmd, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, tailleComm.Height));
                    y += tailleComm.Height + 4;
                }

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                // ---- En-tête colonnes ----
                g.DrawString("Qte", fBold, Brushes.Black, xGauche, y);
                g.DrawString("ARTICLE", fBold, Brushes.Black, xArticle, y);
                y += 14;
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Lignes articles (cuisine2 seulement) ----
                foreach (int i in indexCuisine2)
                {
                    DataRow ligne = lignes.Rows[i];
                    DataRow produit = produits[i];
                    DataTable ings = ingredients[i];

                    string libelle = produit["libelle"].ToString().ToUpper();
                    int idTypeProduit = Convert.ToInt32(produit["idTypeProduit"]);
                    string nomProduit;

                    if (produit["quantite"] != DBNull.Value)
                        nomProduit = $"{libelle} {produit["quantite"]}PC";
                    else if (idTypeProduit == 2)
                    {
                        string typePate = (ligne["typePate"].ToString() != "" ? ligne["typePate"].ToString().ToUpper() + " " : "");
                        nomProduit = $"{typePate}{libelle}";
                    }
                    else
                        nomProduit = libelle;

                    g.DrawString("1", fNormal, Brushes.Black, xGauche, y);
                    g.DrawString(nomProduit, fNormal, Brushes.Black, xArticle, y);
                    y += 16;

                    // Ingrédients modifiés
                    foreach (DataRow ing in ings.Rows)
                    {
                        bool estSupp = Convert.ToBoolean(ing["ingredientEnPlus"]);
                        string prefix = estSupp ? "SUPP" : "SS";
                        string nomIng = ing["ingredient"].ToString();
                        g.DrawString($"   {prefix} {nomIng}", fNormal, Brushes.Black, xArticle, y);
                        y += 14;
                    }

                    // Note
                    string note = ligne["note"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(note))
                    {
                        string texteNote = $"   {note}";
                        SizeF tailleNote = g.MeasureString(texteNote, fNormal, (int)pageWidth);
                        g.DrawString(texteNote, fNormal, Brushes.Black, new RectangleF(xArticle, y, pageWidth, tailleNote.Height));
                        y += tailleNote.Height;
                    }
                }

                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                y += 6;
                // ---- Type commande encadré ----
                bool estLivraison = Convert.ToBoolean(commande["estLivraison"]);
                string typeCmd = estLivraison ? "LIVRAISON" : "EMPORTER";
                RectangleF rectType = new RectangleF(xGauche + 60, y, pageWidth - 120, 22);
                g.DrawRectangle(Pens.Black, rectType.X, rectType.Y, rectType.Width, rectType.Height);
                g.DrawString(typeCmd, fEmporte, Brushes.Black, rectType, sfCenter);
                y += 28;
            });
        }

        private void ImpressionAutre()
        {
            Imprimer(g =>
            {
                float y = 10;
                float pageWidth = 289;
                float xGauche = 5;
                float xArticle = xGauche + 35;
                float xDroite = pageWidth - 5;

                Font fNormal = new Font("Arial", 8, FontStyle.Regular);
                Font fBold = new Font("Arial", 8, FontStyle.Bold);
                Font fGrandBold = new Font("Arial", 14, FontStyle.Bold);
                Font fNumero = new Font("Arial", 36, FontStyle.Bold);
                Font fTitre = new Font("Arial", 13, FontStyle.Bold);
                Font fEmporte = new Font("Arial", 11, FontStyle.Bold);

                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };

                // ---- Date + Heure sur la même ligne ----
                g.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), fNormal, Brushes.Black, xGauche, y);
                g.DrawString(DateTime.Now.ToString("HH:mm:ss"), fNormal, Brushes.Black,
                                new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 20;

                // ---- Titre Restaurant / Internet ----
                int idStatut = Convert.ToInt32(commande["idStatut"]);
                string titreCuisine = (idStatut == 2) ? "INTERNET" : "RESTAURANT";
                RectangleF rectTitre = new RectangleF(xGauche, y, pageWidth, 22);
                g.FillRectangle(Brushes.Black, rectTitre);
                g.DrawString(titreCuisine, fTitre, Brushes.White, rectTitre, sfCenter);
                y += 28;

                // ---- Commande N ----
                g.DrawString($"Commande N : {commande["numeroCommande"]}", fBold, Brushes.Black, xGauche, y); y += 18;

                // ---- Heure commande ----
                DateTime dateCommande = Convert.ToDateTime(commande["dateHeure"]);
                g.DrawString($"{dateCommande:HH:mm}", fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 26;

                // ---- Nom + Prénom client ----
                string nomCmd = commande["nom"].ToString();
                string prenomCmd = commande["prenom"].ToString();
                g.DrawString(nomCmd, fBold, Brushes.Black, xGauche, y); y += 14;
                if (!string.IsNullOrEmpty(prenomCmd))
                { g.DrawString(prenomCmd, fNormal, Brushes.Black, xGauche, y); y += 14; }

                y += 4;

                y += 6;

                // ---- Commentaire ----
                string commentaireCmd = commande["commetaire"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(commentaireCmd))
                {
                    g.DrawString("Commentaire :", fBold, Brushes.Black, xGauche, y); y += 14;
                    SizeF tailleComm = g.MeasureString(commentaireCmd, fNormal, (int)pageWidth);
                    g.DrawString(commentaireCmd, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, tailleComm.Height));
                    y += tailleComm.Height + 4;
                }
                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                // ---- En-tête colonnes ----
                g.DrawString("Qte", fBold, Brushes.Black, xGauche, y);
                g.DrawString("ARTICLE", fBold, Brushes.Black, xArticle, y);
                y += 14;
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Lignes articles (autre seulement) ----
                foreach (int i in indexAutre)
                {
                    DataRow ligne = lignes.Rows[i];
                    DataRow produit = produits[i];
                    string nomProduit = produit["libelle"].ToString().ToUpper();
                    g.DrawString("1", fNormal, Brushes.Black, xGauche, y);
                    g.DrawString(nomProduit, fNormal, Brushes.Black, xArticle, y);
                    y += 16;

                    // Note
                    string note = ligne["note"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(note))
                    {
                        string texteNote = $"   {note}";
                        SizeF tailleNote = g.MeasureString(texteNote, fNormal, (int)pageWidth);
                        g.DrawString(texteNote, fNormal, Brushes.Black, new RectangleF(xArticle, y, pageWidth, tailleNote.Height));
                        y += tailleNote.Height;
                    }
                }

                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 6;

                y += 6;
                // ---- Type commande encadré ----
                bool estLivraison = Convert.ToBoolean(commande["estLivraison"]);
                string typeCmd = estLivraison ? "LIVRAISON" : "EMPORTER";
                RectangleF rectType = new RectangleF(xGauche + 60, y, pageWidth - 120, 22);
                g.DrawRectangle(Pens.Black, rectType.X, rectType.Y, rectType.Width, rectType.Height);
                g.DrawString(typeCmd, fEmporte, Brushes.Black, rectType, sfCenter);
                y += 28;
            });
        }

        public void ImpressionTicketVente(int idTicket)
        {
            sql.TicketById(idTicket, out DataTable dataTicket);
            if (dataTicket.Rows.Count == 0) return;
            DataRow tk = dataTicket.Rows[0];

            Imprimer(g =>
            {
                float y = 10;
                float pageWidth = 289;
                float xGauche = 5;
                float xDroite = pageWidth - 5;

                Font fTitre    = new Font("Arial", 11, FontStyle.Bold);
                Font fNormal   = new Font("Arial", 8,  FontStyle.Regular);
                Font fBold     = new Font("Arial", 8,  FontStyle.Bold);
                Font fGrandBold= new Font("Arial", 13, FontStyle.Bold);
                Font fTiny     = new Font("Arial", 6,  FontStyle.Regular);

                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat sfRight  = new StringFormat { Alignment = StringAlignment.Far };

                // ---- En-tête restaurant ----
                string nomRestau, adresseR, villeR, tel;
                string tva = "N° TVA : BE1028813177";
                if (PagePrincipale.Restaurant == 2)
                {
                    nomRestau = "Crispy Pizza Ramillies";
                    adresseR  = "Avenue des Déportés 45A";
                    villeR    = "1367 Ramillies";
                    tel       = "Tel : 081/340.628";
                }
                else
                {
                    nomRestau = "Crispy Pizza Meux";
                    adresseR  = "Rue Binia 62";
                    villeR    = "5081 Meux";
                    tel       = "Tel : 081/340.628";
                }
                g.DrawString(nomRestau, fTitre,  Brushes.Black, new RectangleF(xGauche, y, pageWidth, 20), sfCenter); y += 18;
                g.DrawString(adresseR,  fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(villeR,    fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tel,       fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tva,       fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 18;

                // ---- Date impression gauche / Heure droite ----
                DateTime dateVal = Convert.ToDateTime(tk["dateValidation"]);
                g.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), fNormal, Brushes.Black, xGauche, y);
                g.DrawString(DateTime.Now.ToString("HH:mm:ss"), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 18;

                // ---- Date validation centré ----
                g.DrawString(dateVal.ToString("dd/MM/yyyy HH:mm"), fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 24;

                // ---- Type ticket centré ----
                string type = tk["type"].ToString().ToUpper();
                g.DrawString(type, fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 24;

                // ---- N° ticket ----
                g.DrawString($"N° {tk["id"]}", fBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 20;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Totaux ----
                decimal totalHT  = Convert.ToDecimal(tk["total_ht"]);
                decimal totalTVA = Convert.ToDecimal(tk["total_tva"]);
                decimal totalTTC = Convert.ToDecimal(tk["total_ttc"]);

                void Ligne(string label, string valeur)
                {
                    g.DrawString(label,  fBold,   Brushes.Black, xGauche, y);
                    g.DrawString(valeur, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                    y += 14;
                }

                Ligne("Total HT :",  totalHT.ToString("0.00")  + " €");
                Ligne("Total TVA :", totalTVA.ToString("0.00") + " €");
                Ligne("Total TTC :", totalTTC.ToString("0.00") + " €");
                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Ticket réf ----
                int ticketRef = Convert.ToInt32(tk["ticket_ref"]);
                if (ticketRef != -1)
                    Ligne("Ticket réf :", ticketRef.ToString());

                int caisse = Convert.ToInt32(tk["caisse"]);
                Ligne("Caisse :", caisse.ToString());
                y += 4;

                // ---- Signatures ----
                string signature     = tk["signature"]?.ToString() ?? "";
                string signaturePrec = tk["signature_prec"]?.ToString() ?? "";

                if (!string.IsNullOrEmpty(signature))
                {
                    g.DrawString($"sign : {signature}", fTiny, Brushes.Black,
                                 new RectangleF(xGauche, y, pageWidth, 12), sfCenter);
                    y += 12;
                }
                if (!string.IsNullOrEmpty(signaturePrec))
                {
                    g.DrawString($"sign. préc : {signaturePrec}", fTiny, Brushes.Black,
                                 new RectangleF(xGauche, y, pageWidth, 12), sfCenter);
                }
            });
        }

        public void ImpressionTousTickets(List<DataRow> tickets)
        {
            if (tickets.Count == 0) return;

            // Estimer la hauteur : ~160 unités par ticket + 150 pour l'en-tête
            int hauteurPage = tickets.Count * 160 + 250;

            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Ticket", 299, hauteurPage);
            doc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
            doc.PrintPage += (s, e) =>
            {
                Graphics g = e.Graphics;
                float y = 10;
                float pageWidth = 289;
                float xGauche = 5;
                float xDroite = pageWidth - 5;

                Font fNormal    = new Font("Arial", 8,  FontStyle.Regular);
                Font fBold      = new Font("Arial", 8,  FontStyle.Bold);
                Font fGrandBold = new Font("Arial", 11, FontStyle.Bold);
                Font fTiny      = new Font("Arial", 6,  FontStyle.Regular);

                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat sfRight  = new StringFormat { Alignment = StringAlignment.Far };

                // ---- En-tête sur le premier ticket seulement ----
                string nomRestau, adresseR, villeR, tel;
                string tva = "N° TVA : BE1028813177";
                Font fTitre = new Font("Arial", 11, FontStyle.Bold);
                if (PagePrincipale.Restaurant == 2)
                { nomRestau = "Crispy Pizza Ramillies"; adresseR = "Avenue des Déportés 45A"; villeR = "1367 Ramillies"; tel = "Tel : 081/340.628"; }
                else
                { nomRestau = "Crispy Pizza Meux"; adresseR = "Rue Binia 62"; villeR = "5081 Meux"; tel = "Tel : 081/340.628"; }

                g.DrawString(nomRestau, fTitre,  Brushes.Black, new RectangleF(xGauche, y, pageWidth, 20), sfCenter); y += 18;
                g.DrawString(adresseR,  fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(villeR,    fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tel,       fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tva,       fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 18;

                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 2;
                g.DrawLine(Pens.Black, xGauche, y + 2, xDroite, y + 2); y += 12;

                foreach (DataRow tk in tickets)
                {
                    DateTime dateVal = Convert.ToDateTime(tk["dateValidation"]);
                    string type      = tk["type"].ToString().ToUpper();
                    decimal totalHT  = Convert.ToDecimal(tk["total_ht"]);
                    decimal totalTVA = Convert.ToDecimal(tk["total_tva"]);
                    decimal totalTTC = Convert.ToDecimal(tk["total_ttc"]);
                    int ticketRef    = Convert.ToInt32(tk["ticket_ref"]);
                    string signature     = tk["signature"]?.ToString() ?? "";
                    string signaturePrec = tk["signature_prec"]?.ToString() ?? "";

                    // Date impression gauche / heure droite
                    g.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), fNormal, Brushes.Black, xGauche, y);
                    g.DrawString(DateTime.Now.ToString("HH:mm:ss"), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                    y += 16;

                    // DateValidation centré
                    g.DrawString(dateVal.ToString("dd/MM/yyyy HH:mm"), fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 20), sfCenter); y += 20;

                    // Type + N°
                    g.DrawString($"{type}  N° {tk["id"]}", fBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 18;

                    // Totaux
                    g.DrawString("Total HT :",  fBold, Brushes.Black, xGauche, y);
                    g.DrawString(totalHT.ToString("0.00") + " €",  fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight); y += 14;
                    g.DrawString("Total TVA :", fBold, Brushes.Black, xGauche, y);
                    g.DrawString(totalTVA.ToString("0.00") + " €", fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight); y += 14;
                    g.DrawString("Total TTC :", fBold, Brushes.Black, xGauche, y);
                    g.DrawString(totalTTC.ToString("0.00") + " €", fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight); y += 14;

                    // Caisse
                    int caisse = Convert.ToInt32(tk["caisse"]);
                    g.DrawString("Caisse :", fBold, Brushes.Black, xGauche, y);
                    g.DrawString(caisse.ToString(), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight); y += 14;

                    // Ticket réf si pas -1
                    if (ticketRef != -1)
                    {
                        g.DrawString("Ticket réf :", fBold, Brushes.Black, xGauche, y);
                        g.DrawString(ticketRef.ToString(), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight); y += 14;
                    }

                    // Signatures
                    if (!string.IsNullOrEmpty(signature))
                    {
                        g.DrawString($"sign : {signature}", fTiny, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 12), sfCenter); y += 12;
                    }
                    if (!string.IsNullOrEmpty(signaturePrec))
                    {
                        g.DrawString($"sign. préc : {signaturePrec}", fTiny, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 12), sfCenter); y += 12;
                    }

                    // Séparateur entre tickets
                    y += 6;
                    g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 2;
                    g.DrawLine(Pens.Black, xGauche, y + 2, xDroite, y + 2); y += 10;
                }

                e.HasMorePages = false;
            };
            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            doc.Print();
        }

        public void ImpressionCloture(int idCloture)
        {
            sql.ClotureById(idCloture, out DataTable dataCloture);
            if (dataCloture.Rows.Count == 0) return;
            DataRow cl = dataCloture.Rows[0];

            Imprimer(g =>
            {
                float y = 10;
                float pageWidth = 289;
                float xGauche = 5;
                float xDroite = pageWidth - 5;

                Font fTitre    = new Font("Arial", 11, FontStyle.Bold);
                Font fNormal   = new Font("Arial", 8,  FontStyle.Regular);
                Font fBold     = new Font("Arial", 8,  FontStyle.Bold);
                Font fGrandBold= new Font("Arial", 13, FontStyle.Bold);
                Font fMoyenBold= new Font("Arial", 10, FontStyle.Bold);

                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat sfRight  = new StringFormat { Alignment = StringAlignment.Far };
                StringFormat sfLeft   = new StringFormat { Alignment = StringAlignment.Near };

                // ---- En-tête restaurant ----
                string nomRestau, adresseR, villeR, tel;
                string tva = "N° TVA : BE1028813177";
                if (PagePrincipale.Restaurant == 2)
                {
                    nomRestau = "Crispy Pizza Ramillies";
                    adresseR  = "Avenue des Déportés 45A";
                    villeR    = "1367 Ramillies";
                    tel       = "Tel : 081/340.628";
                }
                else
                {
                    nomRestau = "Crispy Pizza Meux";
                    adresseR  = "Rue Binia 62";
                    villeR    = "5081 Meux";
                    tel       = "Tel : 081/340.628";
                }
                g.DrawString(nomRestau, fTitre,  Brushes.Black, new RectangleF(xGauche, y, pageWidth, 20), sfCenter); y += 18;
                g.DrawString(adresseR,  fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(villeR,    fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tel,       fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 14;
                g.DrawString(tva,       fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 16), sfCenter); y += 18;

                // ---- Date gauche / Heure impression droite ----
                DateTime dateCloture = Convert.ToDateTime(cl["date"]);
                g.DrawString(dateCloture.ToString("dd/MM/yyyy"), fNormal, Brushes.Black, xGauche, y);
                g.DrawString(DateTime.Now.ToString("HH:mm:ss"), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 18;

                // ---- Date clôture centré ----
                g.DrawString(dateCloture.ToString("dd/MM/yyyy"), fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 24;

                // ---- Numéro clôture centré ----
                g.DrawString($"CLÔTURE N° {cl["numeroCloture"]}", fGrandBold, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 24), sfCenter); y += 26;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Argent ----
                decimal argentRest  = Convert.ToDecimal(cl["argentRestaurant"]);
                decimal argentNet   = Convert.ToDecimal(cl["argentInternet"]);
                decimal argentCarte = Convert.ToDecimal(cl["argentCarte"]);
                decimal argentLiq   = Convert.ToDecimal(cl["argentLiquide"]);
                decimal argentDiff  = Convert.ToDecimal(cl["argentDifference"]);
                decimal totalHT     = Convert.ToDecimal(cl["total_ht"]);
                decimal totalTVA    = Convert.ToDecimal(cl["total_tva"]);
                decimal totalTTC    = Convert.ToDecimal(cl["total_ttc"]);

                void LigneArgent(string label, string valeur)
                {
                    g.DrawString(label,  fBold,   Brushes.Black, xGauche, y);
                    g.DrawString(valeur, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                    y += 14;
                }

                LigneArgent("Restaurant :",   argentRest.ToString("0.00")  + " €");
                LigneArgent("Internet :",     argentNet.ToString("0.00")   + " €");
                LigneArgent("Carte :",        argentCarte.ToString("0.00") + " €");
                LigneArgent("Liquide :",      argentLiq.ToString("0.00")   + " €");
                LigneArgent("Différence :",   argentDiff.ToString("0.00")  + " €");
                y += 4;
                LigneArgent("Total HT :",     totalHT.ToString("0.00")     + " €");
                LigneArgent("Total TVA :",    totalTVA.ToString("0.00")    + " €");
                LigneArgent("Total TTC :",    totalTTC.ToString("0.00")    + " €");
                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Produits ----
                int nbMaxiPizza = Convert.ToInt32(cl["nbMaxiPizza"]);
                int nbPizza     = Convert.ToInt32(cl["nbPizza"]);
                int nbPate      = Convert.ToInt32(cl["nbPâte"]);
                int nbPain      = Convert.ToInt32(cl["nbPain"]);
                int nbSalade    = Convert.ToInt32(cl["nbSalade"]);
                int nbSnacking  = Convert.ToInt32(cl["nbSnacking"]);
                int nbBoisson   = Convert.ToInt32(cl["nbBoisson"]);
                int nbDessert   = Convert.ToInt32(cl["nbDessert"]);
                int nbDipy      = Convert.ToInt32(cl["nbDipy"]);

                // Pizza (total) avec sous-lignes
                g.DrawString("Pizza :", fBold, Brushes.Black, xGauche, y);
                g.DrawString((nbMaxiPizza + nbPizza).ToString(), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 14;
                g.DrawString("   Maxi :", fNormal, Brushes.Black, xGauche, y);
                g.DrawString(nbMaxiPizza.ToString(), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 13;
                g.DrawString("   Normal :", fNormal, Brushes.Black, xGauche, y);
                g.DrawString(nbPizza.ToString(), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                y += 14;

                void LigneProduit(string label, int nb)
                {
                    g.DrawString(label, fBold, Brushes.Black, xGauche, y);
                    g.DrawString(nb.ToString(), fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                    y += 14;
                }

                LigneProduit("Pâte :",     nbPate);
                LigneProduit("Pain :",     nbPain);
                LigneProduit("Salade :",   nbSalade);
                LigneProduit("Snacking :", nbSnacking);
                LigneProduit("Boisson :",  nbBoisson);
                LigneProduit("Dessert :",  nbDessert);
                LigneProduit("Dipy :",     nbDipy);
                y += 4;

                // ---- Séparateur ----
                g.DrawLine(Pens.Black, xGauche, y, xDroite, y); y += 8;

                // ---- Infos tickets ----
                string heureDebut    = cl["heureDebut"].ToString();
                string heureFin      = cl["heureFin"].ToString();
                int premierTicket    = Convert.ToInt32(cl["premierTicket"]);
                int dernierTicket    = Convert.ToInt32(cl["dernierTicket"]);
                int totalTicket      = Convert.ToInt32(cl["totalTicket"]);
                int totalAnnuler     = Convert.ToInt32(cl["totalAnnuler"]);
                string signature     = cl["signature"]?.ToString() ?? "";
                string signaturePrec = cl["signature_prec"]?.ToString() ?? "";

                void LigneInfo(string label, string valeur)
                {
                    g.DrawString(label,  fBold,   Brushes.Black, xGauche, y);
                    g.DrawString(valeur, fNormal, Brushes.Black, new RectangleF(xGauche, y, pageWidth, 14), sfRight);
                    y += 14;
                }

                LigneInfo("Heure début :",      heureDebut);
                LigneInfo("Heure fin :",        heureFin);
                LigneInfo("Premier ticket :",   premierTicket.ToString());
                LigneInfo("Dernier ticket :",   dernierTicket.ToString());
                LigneInfo("Total tickets :",    totalTicket.ToString());
                LigneInfo("Total annulations :", totalAnnuler.ToString());
                y += 6;

                // ---- Signature ----
                Font fTiny = new Font("Arial", 6);
                if (!string.IsNullOrEmpty(signature))
                {
                    g.DrawString($"sign : {signature}", fTiny, Brushes.Black,
                                 new RectangleF(xGauche, y, pageWidth, 12), sfCenter);
                    y += 12;
                }
                if (!string.IsNullOrEmpty(signaturePrec))
                {
                    g.DrawString($"sign. préc : {signaturePrec}", fTiny, Brushes.Black,
                                 new RectangleF(xGauche, y, pageWidth, 12), sfCenter);
                }
            });
        }
    }
}
