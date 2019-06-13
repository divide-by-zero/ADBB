using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBB
{
    public partial class MainForm : Form
    {
        private List<PackageData> dispData;
        private AdbCommand _adb;

        private Device _targetDevice;
        private PackageData _selectPackage;
        private Progress<AdbCommand.AdbProgressData> progress;

        public MainForm()
        {
            InitializeComponent();

            _adb = new AdbCommand(Properties.Settings.Default.adbPath);

            //ADB.exeへのパスが設定されたら作り直し
            Properties.Settings.Default.PropertyChanged += (sender, args) => {
                _adb = new AdbCommand(Properties.Settings.Default.adbPath);
            };
            
            Observable.FromEventPattern(textBox1, "TextChanged")
                .Select(pattern => textBox1.Text)
                .DistinctUntilChanged()
                .Throttle(TimeSpan.FromSeconds(0.5f))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(FilterPackage);

            packageDataGrid.CellContextMenuStripNeeded += PackageDataGridCellContextMenuStripNeeded;

            progress = new Progress<AdbCommand.AdbProgressData>(async data => {
                if (data.IsRequireDialog)
                {
                    if (data.IsError)
                    {
                        MessageBox.Show(data.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (data.IsSuccess)
                    {
                        MessageBox.Show(data.Message, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = data.Message;
                toolStripProgressBar1.MarqueeAnimationSpeed = 50;
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                if (data.IsSuccess | data.IsError)
                {
                    await Task.Delay(2000);
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel1.Visible = false;
                }
            });
        }

        /// <summary>
        /// Deviceが選択されている時のみ有効になるメニューの更新処理
        /// </summary>
        private void UpdateToolbarMenuEnable()
        {
            iPConnectToolStripMenuItem.Enabled = _targetDevice != null;
            apkInstallToolStripMenuItem.Enabled = _targetDevice != null;
            DeviceToolStripMenuItem.Enabled = _targetDevice != null;
        }

        private void PackageDataGridCellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= packageDataGrid.RowCount) return;

            var dataRow = packageDataGrid.Rows[e.RowIndex];
            dataRow.Cells[0].Selected = true;
            _selectPackage = dataRow.DataBoundItem as PackageData;
            e.ContextMenuStrip = this.PackageCellContextMenuStrip;
        }

        private void FilterPackage(string text)
        {
            if (dispData == null || dispData.Any() == false) return;
            packageDataGrid.DataSource = dispData.Where(data => data.Name.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.adbPath))
            {
                ConfigToolStripMenuItem_Click(null, null);
            }
            UpdateDeviceList();
        }

        private async void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"{_selectPackage.Name} をアンインストールしてよろしいですか？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel) return ;
            var unInstallResult = await _adb.UnInstallPackage(_targetDevice,_selectPackage, progress);
            if (unInstallResult == false) return ;
            await UpdatePackageList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _targetDevice = comboBox1.SelectedItem as Device;
            UpdateToolbarMenuEnable();
            UpdatePackageList();
        }

        private void 起動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _adb.LunchPackage(_targetDevice, _selectPackage,progress);
        }

        private void DeviceUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDeviceList();
        }

        private async void IpConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = await _adb.ConnectIp(_targetDevice,progress);
            if (result == false) return;
            await Task.Delay(1000);//IP接続した直後はUSB接続が出てこないときがある
            await UpdateDeviceList();
        }

        private async void IpDisconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = await _adb.DisconnectIp(progress);
            if (result == false) return;
            await UpdateDeviceList();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= packageDataGrid.RowCount) return;

            var dataRow = packageDataGrid.Rows[e.RowIndex];
            dataRow.Cells[0].Selected = true;
            _selectPackage = dataRow.DataBoundItem as PackageData;

            var result = MessageBox.Show($"{_selectPackage.Name} を起動してよろしいですか？", "起動確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel) return;

            _adb.LunchPackage(_targetDevice, _selectPackage, progress);
        }

        private async void APKInstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog{
                Filter = "Androidファイル|*.apk",
                Title = "インストールするAPKファイルを選択"
            };
            var result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK) return;

            var installResult = await _adb.InstallPackage(_targetDevice, openFileDialog.FileName,progress);
            if (installResult == false) return;
            await UpdatePackageList();
        }

        private void DeviceShutdownToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Device:{_targetDevice.Name} を強制シャットダウンしてよろしいですか？", "強制シャットダウン確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel) return;
            _adb.Shutdown(_targetDevice, progress);
        }

        private void DeviceRebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Device:{_targetDevice.Name} を強制再起動してよろしいですか？", "強制再起動確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel) return;
            _adb.Reboot(_targetDevice,progress);
        }

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog{
                Filter = "adb.exe|adb.exe",
                Title = "adb.exeのパス設定"
            };
            var result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK) return;

            Properties.Settings.Default.adbPath = openFileDialog.FileName;
            Properties.Settings.Default.Save();
        }

        private void abortToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _adb.StopPackage(_targetDevice, _selectPackage,progress);
        }

        private async Task<bool> UpdatePackageList()
        {
            var package = await _adb.GetPackageList(_targetDevice,progress);
            if (package == null) return false;
            dispData = package.ToList();
            packageDataGrid.DataSource = dispData;
            return true;
        }

        private async Task<bool> UpdateDeviceList()
        {
            packageDataGrid.DataSource = new List<PackageData>();
            comboBox1.DataSource = new List<Device>();
            var result = await _adb.GetDeviceList(progress);

            if (result?.Any() != true)
            {
                MessageBox.Show("デバイスが見つかりません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _targetDevice = null;
                UpdateToolbarMenuEnable();
                return false;
            }
            comboBox1.DataSource = result.ToList();
            return true;
        }

        private async void PackageDataGrid_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var file in files)
            {
                await _adb.InstallPackage(_targetDevice, file, progress);
            }
            await UpdatePackageList();
        }

        private void PackageDataGrid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void APKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog(){
                AddExtension = true,
                DefaultExt = "apk",
                Title = "ファイルダウンロード先を選択してください",
                Filter = "Androidファイル|*.apk"

            };

            var result = saveFileDialog.ShowDialog();

            if (result != DialogResult.OK) return;
            _adb.DownloadApk(_targetDevice, _selectPackage,  saveFileDialog.FileName, progress);
        }
    }
}
