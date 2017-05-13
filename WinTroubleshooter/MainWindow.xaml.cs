using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinTroubleshooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Random Generator = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private static async Task WaitRandom(int min = 500, int max = 3000)
        {
            await Task.Delay(Generator.Next(min, max));
        }

        private void ShowCodedMessageBox(string content, string header = "WinTroubleshooter")
        {
            string message = $"{content}\n\nT={DateTime.UtcNow:o}\nRID={Guid.NewGuid()}";
            MessageBox.Show(this, message, header);
        }

        private async void CpuOptSyncClock_OnClick(object sender, RoutedEventArgs e)
        {
            CpuOptSyncClock.IsEnabled = false;
            object originalLabel = CpuOptSyncClock.Content;
            CpuOptSyncClock.Content = "Just a second...";

            await WaitRandom();

            CpuOptSyncClock.Content = originalLabel;

            ShowCodedMessageBox("LLCR → HLCR sync performed successfully.");
        }

        private async void CpuOptL1Cache_OnClick(object sender, RoutedEventArgs e)
        {
            CpuOptL1Cache.IsEnabled = false;
            object originalLabel = CpuOptL1Cache.Content;
            CpuOptL1Cache.Content = "Repairing...";
            CpuOptL1Cache.Cursor = Cursors.Wait;

            await WaitRandom(3000, 10000);

            CpuOptL1Cache.Content = originalLabel;

            int affected = Generator.Next(4096, 131072);
            ShowCodedMessageBox($"L1 die cache repaired ({affected} bytes identified & reinitialized).");
        }

        private async void DiskOptRebuildIndex_OnClick(object sender, RoutedEventArgs e)
        {
            DiskOptRebuildIndex.IsEnabled = false;
            object originalLabel = DiskOptRebuildIndex.Content;
            DiskOptRebuildIndex.Content = "Rebuilding...";

            for (int i = 0; i < 100; i++)
            {
                await WaitRandom();
                DiskOptRebuildIndexProgress.Value += 1;
            }

            DiskOptRebuildIndex.Content = originalLabel;
            DiskOptRebuildIndexProgress.Value = 0;

            int entries = Generator.Next(16777216, 1073741824);
            ShowCodedMessageBox($"Disk indexes successfully rebuilt ({entries} entries now in index).");
        }

        private async void AdvPerfCleanCache_OnClick(object sender, RoutedEventArgs e)
        {
            AdvPerfCleanCache.IsEnabled = false;
            object originalLabel = AdvPerfCleanCache.Content;
            AdvPerfCleanCache.Content = "Cleaning...";

            await WaitRandom();

            AdvPerfCleanCache.Content = "Refreshing...";
            await WaitRandom();

            AdvPerfCleanCache.Content = "Repopulating...";
            await WaitRandom();

            AdvPerfCleanCache.Content = "Revalidating...";
            await WaitRandom();

            AdvPerfCleanCache.Content = originalLabel;
            ShowCodedMessageBox("Caches succesfully cleaned and repopulated.");
        }

        private async void AdvPerfReinitDatabases_OnClick(object sender, RoutedEventArgs e)
        {
            AdvPerfReinitDatabases.IsEnabled = false;
            object originalLabel = AdvPerfReinitDatabases.Content;
            AdvPerfReinitDatabases.Content = "Reinitializing...";

            await WaitRandom(3000, 8000);

            AdvPerfReinitDatabases.Content = originalLabel;
            int affected = Generator.Next(4, 32);
            ShowCodedMessageBox($"{affected} databases successfully reinitialized; optimization procedures have been set to run in the background.");
        }

        private async void AdvPerfResetLatency_OnClick(object sender, RoutedEventArgs e)
        {
            AdvPerfResetLatency.IsEnabled = false;
            object originalLabel = AdvPerfResetLatency.Content;
            AdvPerfResetLatency.Content = "Removing...";

            await WaitRandom(2000, 5000);
            AdvPerfResetLatency.Content = "Compiling...";

            await WaitRandom(7000, 15000);
            AdvPerfResetLatency.Content = "Resetting...";

            await WaitRandom(500, 2000);

            AdvPerfResetLatency.Content = originalLabel;
            ShowCodedMessageBox("Removed overridden configurations, and renewed and reset LSR engine.");
        }

        private void AdvDefOptimizeDB_OnClick(object sender, RoutedEventArgs e)
        {
            AdvDefOptimizeDB.IsEnabled = false;
            ShowCodedMessageBox("AM database optimization procedures set to run in the background.");
        }

        private async void AdvDefCheckAirgap_OnClick(object sender, RoutedEventArgs e)
        {
            List<ResponseStage> stages = new List<ResponseStage>
            {
                new ResponseStage("Checking procedure: IdentifyComponents...", 1000, 3000),
                new ResponseStage("Checking procedure: IsolateIdentifiedSingle...", 500, 2000),
                new ResponseStage("Checking procedure: IsolateIdentifiedMultiplex...", 2000, 5000),
                new ResponseStage("Checking procedure: HandleIsolatedComms...", 500, 2000),
                new ResponseStage("Checking procedure: UnexpectedIsolationBreakout...", 2000, 5000),
                new ResponseStage("Checking procedure: LegalIsolationBreakout...", 1000, 3000),
                new ResponseStage("Checking full process run...", 4000, 8000)
            };

            AdvDefCheckAirgap.IsEnabled = false;
            object originalLabel = AdvDefCheckAirgap.Content;

            foreach (ResponseStage stage in stages)
            {
                AdvDefCheckAirgapStatus.Text = stage.DisplayText;
                await WaitRandom(stage.MinDelay, stage.MaxDelay);
            }

            AdvDefCheckAirgap.Content = originalLabel;
            AdvDefCheckAirgapStatus.Text = "";
            ShowCodedMessageBox("Airgap procedures validated and certified.");
        }

        private void AdvRunAll_OnClick(object sender, RoutedEventArgs e)
        {
            List<Button> actions = new List<Button>
            {
                (Button)FindName("AdvPerfCleanCache"),
                (Button)FindName("AdvPerfReinitDatabases"),
                (Button)FindName("AdvPerfResetLatency"),
                (Button)FindName("AdvDefOptimizeDB"),
                (Button)FindName("AdvDefCheckAirgap")
            };

            foreach (Button action in actions)
            {
                if (action.IsEnabled)
                {
                    action.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
            }
        }
    }
}
