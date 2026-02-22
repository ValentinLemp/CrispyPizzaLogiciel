using System.Windows;
using Velopack;
using Velopack.Sources;

namespace CrispyLo
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            VelopackApp.Build().Run();
            base.OnStartup(e);
            _ = MiseAJour();
        }

        private static async Task MiseAJour()
        {
            try
            {
                var mgr = new UpdateManager(new GithubSource("https://github.com/TON_COMPTE/TON_REPO", null, false));
                var update = await mgr.CheckForUpdatesAsync();
                if (update == null) return;

                var result = MessageBox.Show(
                    $"Une nouvelle version ({update.TargetFullRelease.Version}) est disponible.\nVoulez-vous mettre à jour maintenant ?",
                    "Mise à jour disponible", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result != MessageBoxResult.Yes) return;

                await mgr.DownloadUpdatesAsync(update);
                mgr.ApplyUpdatesAndRestart(update);
            }
            catch { /* pas de connexion ou pas de repo = on ignore */ }
        }
    }
}
