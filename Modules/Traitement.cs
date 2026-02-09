using CrispyLo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrispyLo.Modules
{
    static class Traitement
    {
        
        private static readonly HttpClient client = new HttpClient();

        static Traitement()
        {
            client.DefaultRequestHeaders.Add("User-Agent", "CrispyLo/1.0");
        }

        public static string[] descToIngredient(string description)
        {
            description = description.Replace("(", "").Replace(")", "");
            return description.Split(", ");
        }

        public static double prixTotal()
        {
            MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;

            double montantTotal = 0;
            foreach (Produit produit in PagePrincipale.Panier)
            {
                montantTotal += produit.prixTotal;
            }

            return montantTotal;
        }

        public static async Task<List<Adresse>> RechercherAdresse(string recherche)
        {
            MainWindow PagePrincipale = (MainWindow)App.Current.MainWindow;
            string lat, lon;
            if (PagePrincipale.Restaurent == "meux")
            {
                // Centre : Rue du Chainia 62, 5081 Meux
                lat = "50.5515";
                lon = "4.8016";
            }
            else
            {
                // Centre : Avenue des déportés 45A, 1367 Ramillies
                lat = "50.6457";
                lon = "4.8733";
            }

            List<Adresse> resultats = new List<Adresse>();
            if (string.IsNullOrWhiteSpace(recherche) || recherche.Length < 3)
                return resultats;

            double latCentre = double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
            double lonCentre = double.Parse(lon, System.Globalization.CultureInfo.InvariantCulture);

            string url = "https://photon.komoot.io/api/?q="
                + Uri.EscapeDataString(recherche)
                + "&lat=" + lat + "&lon=" + lon
                + "&limit=20&lang=fr";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    if (doc.RootElement.TryGetProperty("features", out JsonElement features))
                    {
                        foreach (JsonElement feature in features.EnumerateArray())
                        {
                            if (resultats.Count >= 5) break;

                            // Vérifier la distance
                            if (feature.TryGetProperty("geometry", out JsonElement geometry)
                                && geometry.TryGetProperty("coordinates", out JsonElement coords))
                            {
                                double lonResult = coords[0].GetDouble();
                                double latResult = coords[1].GetDouble();
                                double distance = CalculerDistance(latCentre, lonCentre, latResult, lonResult);
                                if (distance > 10) continue; // Plus de 10km, on ignore
                            }

                            if (feature.TryGetProperty("properties", out JsonElement props))
                            {
                                Adresse adr = new Adresse();
                                adr.Rue = props.TryGetProperty("street", out JsonElement street) ? street.GetString()
                                    : props.TryGetProperty("name", out JsonElement name) ? name.GetString() : "";
                                adr.Numero = props.TryGetProperty("housenumber", out JsonElement num) ? num.GetString() : "";
                                adr.CodePostal = props.TryGetProperty("postcode", out JsonElement cp) ? cp.GetString() : "";
                                adr.Ville = props.TryGetProperty("city", out JsonElement city) ? city.GetString()
                                    : props.TryGetProperty("town", out JsonElement town) ? town.GetString()
                                    : props.TryGetProperty("village", out JsonElement village) ? village.GetString() : "";
                                adr.Affichage = adr.Rue + " " + adr.Numero + ", " + adr.CodePostal + " " + adr.Ville;
                                resultats.Add(adr);
                            }
                        }
                    }
                }
            }
            return resultats;
        }
        private static double CalculerDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Rayon de la Terre en km
            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLon = (lon2 - lon1) * Math.PI / 180;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                + Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180)
                * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
    }
}
