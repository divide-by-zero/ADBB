using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBB
{
    public partial class Form1 : Form
    {
        private List<PackageData> dispData;
        private AdbCommand _adb;
        private Device _targetDevice;
        private PackageData _selectPackage;
        private DispStatus _dispStatus;

        public Form1()
        {
            InitializeComponent();

            _dispStatus = new DispStatus(toolStripStatusLabel1, toolStripProgressBar1);

            _adb = new AdbCommand("adb");

            Observable.FromEventPattern(textBox1, "TextChanged")
                .Select(pattern => textBox1.Text)
                .DistinctUntilChanged()
                .Throttle(TimeSpan.FromSeconds(0.5f))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(FilterPackage);

            dataGridView1.CellContextMenuStripNeeded += DataGridView1_CellContextMenuStripNeeded;

        }

        private void DataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.RowCount) return;

            var dataRow = dataGridView1.Rows[e.RowIndex];
            dataRow.Cells[0].Selected = true;
            _selectPackage = dataRow.DataBoundItem as PackageData;
            e.ContextMenuStrip = this.PackageCellContextMenuStrip;
        }

        private void FilterPackage(string text)
        {
            if (dispData == null || dispData.Any() == false) return;
            dataGridView1.DataSource = dispData.Where(data => data.Name.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
        }

        private async Task ConnectIp()
        {
            await _adb.ConnectIp(_targetDevice);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDeviceList();
        }

        private async Task UpdateDeviceList()
        {
            var result = await _adb.GetDeviceList();

            if (result == null)
            {
                MessageBox.Show("デバイス情報取得に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            comboBox1.DataSource = result.ToList();
            if (result.Any() == false)
            {
                MessageBox.Show("デバイスが見つかりません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void アンインストールToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uninstall(_targetDevice, _selectPackage);
        }

        private async Task Uninstall(Device device, PackageData package)
        {
            var result = MessageBox.Show($"{_selectPackage.Name} をアンインストールしてよろしいですか？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel) return;

            var unInstallResult = await _adb.UnInstallPackage(device, package);
            if (unInstallResult == false)
            {
                MessageBox.Show("アンインストールに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await UpdatePackage();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _targetDevice = comboBox1.SelectedItem as Device;
            await UpdatePackage();
        }

        private async void 起動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await LaunchApp(_selectPackage);
        }

        private async Task UpdatePackage()
        {
            var package = await _adb.GetPackageList(_targetDevice);
            if (package == null)
            {
                MessageBox.Show("パッケージ情報取得に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dispData = package.ToList();
            dataGridView1.DataSource = dispData;
        }

        private async Task LaunchApp(PackageData targetPackage)
        {
            using (_dispStatus.On($"{_selectPackage.Name}起動中"))
            {
                var result = await Task.Run(() => _adb.LunchPackage(_targetDevice, targetPackage));
                if (result == false)
                {
                    MessageBox.Show("起動に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _dispStatus.Finish("起動完了");
                await Task.Delay(1000);
            }
        }

        private async void Device更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (_dispStatus.On($"デバイス検索中"))
            {
                await UpdateDeviceList();
            }
        }

        private async void 開始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (_dispStatus.On("IP接続中"))
            {
                if (_targetDevice == null)
                {
                    MessageBox.Show("デバイスが選択されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await ConnectIp();
                await Task.Delay(1000);//IP接続した直後はUSB接続が出てこないときがある
                await UpdateDeviceList();
                _dispStatus.Finish("IP接続完了");
                await Task.Delay(1000);
            }
        }

        private async void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _adb.DisconnectIp();
            await UpdateDeviceList();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.RowCount) return;

            var dataRow = dataGridView1.Rows[e.RowIndex];
            dataRow.Cells[0].Selected = true;
            _selectPackage = dataRow.DataBoundItem as PackageData;

            var result = MessageBox.Show($"{_selectPackage.Name} を起動してよろしいですか？", "起動確認", MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel) return;

            LaunchApp(_selectPackage);
        }

        private async void APKインストールToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Androidファイル|*.apk";
            var result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK) return;

            using (_dispStatus.On($"{openFileDialog.FileName}インストール中"))
            {
                await Task.Run(() => _adb.InstallPackage(_targetDevice, openFileDialog.FileName));
            }
        }

        private void 端末シャットダウンToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _adb.Shutdown();
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
