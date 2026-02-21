using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CrispyLo.Modules
{
    internal static class Signature
    {
        // ---- Clés hardcodées par restaurant (une par restaurant) ----
        // IMPORTANT : ne jamais partager ces clés
        private static readonly Dictionary<int, string> _clesApp = new()
        {
            { 1, "bX1UYWdQbzNLZThWZlJhN05jUWxIeEp2WXdBc0RpNm8=" }, // Meux
            { 2, "Ukc5eFkyeHZZMlZzYjJGa1lYa3ZkR1Z6ZEhKbFlXMD0=" }  // Ramillies
        };

        private static readonly string _cheminCle =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "CrispyLo", "key.dat");

        // ---- Génère la clé locale au premier lancement ----
        public static void GenererCleLocale()
        {
            if (File.Exists(_cheminCle)) return;

            Directory.CreateDirectory(Path.GetDirectoryName(_cheminCle)!);

            byte[] cle = new byte[32];
            RandomNumberGenerator.Fill(cle);

            byte[] cleChiffree = ProtectedData.Protect(cle, null, DataProtectionScope.LocalMachine);
            File.WriteAllBytes(_cheminCle, cleChiffree);
        }

        // ---- Lit et déchiffre la clé locale ----
        private static byte[] LireCleLocale()
        {
            byte[] cleChiffree = File.ReadAllBytes(_cheminCle);
            return ProtectedData.Unprotect(cleChiffree, null, DataProtectionScope.LocalMachine);
        }

        // ---- Combine clé app + clé locale → clé finale unique à cette machine ----
        private static byte[] ObtenirClefinale(int idRestaurant)
        {
            byte[] cleApp = Convert.FromBase64String(_clesApp[idRestaurant]);
            byte[] cleLocale = LireCleLocale();

            using var hmac = new HMACSHA256(cleApp);
            return hmac.ComputeHash(cleLocale);
        }

        // ---- Signe un ticket et retourne une signature courte ----
        public static string SignerTicket(int idRestaurant, int idTicket, DateTime date, decimal montant)
        {
            string data = $"{idTicket}|{date:yyyyMMddHHmmss}|{montant:F2}|{idRestaurant}";
            byte[] clefinale = ObtenirClefinale(idRestaurant);

            using var hmac = new HMACSHA256(clefinale);
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToHexString(hash)[..16];
        }

        // ---- Signe un ticket en chaîne (inclut la signature précédente) ----
        public static string SignerTicketAvecPrec(int idRestaurant, int idCommande, DateTime date, decimal montant, string signaturePrec)
        {
            string data = $"{idCommande}|{date:yyyyMMddHHmmss}|{montant:F2}|{idRestaurant}|{signaturePrec}";
            byte[] clefinale = ObtenirClefinale(idRestaurant);

            using var hmac = new HMACSHA256(clefinale);
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToHexString(hash)[..16];
        }
    }
}
