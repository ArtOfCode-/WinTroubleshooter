using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private void AdvPerfCleanCache_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
