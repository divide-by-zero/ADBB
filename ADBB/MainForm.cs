using System;
using System.Collections.Generic;
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

            _adb = new AdbCommand("adb");

            Observable.FromEventPattern(textBox1, "TextChanged")
                .Select(pattern => textBox1.Text)
                .DistinctUntilChanged()
                .Throttle(TimeSpan.FromSeconds(0.5f))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(FilterPackage);

            dataGridView1.CellContextMenuStripNeeded += DataGridView1_CellContextMenuStripNeeded;

            progress = new Progress<AdbCommand.AdbProgressData>(async data => {
                if (data.IsError)
                {
                    MessageBox.Show(data.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //TODO Exception情報を表示するかどうか
                }
                else
                {
                    toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = data.Message;
                    toolStripProgressBar1.MarqueeAnimationSpeed = 50;
                    toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                }

                if (data.IsSuccess | data.IsError)
                {
                    await Task.Delay(2000);
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel1.Visible = false;
                }
            });
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

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDeviceList();
        }

        private void アンインストールToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uninstall(_targetDevice, _selectPackage);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _targetDevice = comboBox1.SelectedItem as Device;
            UpdatePackageList(_targetDevice);
        }

        private void 起動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchApp(_selectPackage);
        }

        private async void Device更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await UpdateDeviceList();
        }

        private async void 開始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_targetDevice == null)
            {
                MessageBox.Show("デバイスが選択されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await _adb.ConnectIp(_targetDevice,progress);
            await Task.Delay(1000);//IP接続した直後はUSB接続が出てこないときがある
            await UpdateDeviceList();
        }

        private async void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _adb.DisconnectIp(progress);
            await UpdateDeviceList();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.RowCount) return;

            var dataRow = dataGridView1.Rows[e.RowIndex];
            dataRow.Cells[0].Selected = true;
            _selectPackage = dataRow.DataBoundItem as PackageData;

            var result = MessageBox.Show($"{_selectPackage.Name} を起動してよろしいですか？", "起動確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel) return;

            LaunchApp(_selectPackage);
        }

        private async void APKインストールToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Androidファイル|*.apk";
            var result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK) return;

            await _adb.InstallPackage(_targetDevice, openFileDialog.FileName,progress);
        }

        private void 端末シャットダウンToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _adb.Shutdown(progress);
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 終了ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _adb.StopPackage(_targetDevice, _selectPackage,progress);
        }

        private async Task<bool> Uninstall(Device device, PackageData package)
        {
            var result = MessageBox.Show($"{_selectPackage.Name} をアンインストールしてよろしいですか？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel) return false;

            var unInstallResult = await _adb.UnInstallPackage(device, package,progress);
            if (unInstallResult == false)
            {
                MessageBox.Show("アンインストールに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            await UpdatePackageList(_targetDevice);
            return true;
        }
        
        private async Task<bool> UpdatePackageList(Device _targetDevice)
        {
            var package = await _adb.GetPackageList(_targetDevice,progress);
            if (package == null)
            {
                MessageBox.Show("パッケージ情報取得に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            dispData = package.ToList();
            dataGridView1.DataSource = dispData;
            return true;
        }

        private async Task<bool> LaunchApp(PackageData targetPackage)
        {
            var result = await _adb.LunchPackage(_targetDevice, targetPackage,progress);
            if (result == false)
            {
                MessageBox.Show("起動に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task<bool> UpdateDeviceList()
        {
            comboBox1.DataSource = null;
            var result = await _adb.GetDeviceList(progress);

            if (result == null)
            {
                MessageBox.Show("デバイス情報取得に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (result.Any() == false)
            {
                MessageBox.Show("デバイスが見つかりません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            comboBox1.DataSource = result.ToList();
            return true;
        }
    }
}
