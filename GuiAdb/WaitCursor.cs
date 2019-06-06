using System;
using System.Windows.Forms;

namespace GuiAdb
{
    public class WaitCursor : IDisposable
    {
        private Cursor old = null;

        public static WaitCursor Start(Cursor c = null)
        {
            var instance = new WaitCursor();
            instance.old = Cursor.Current;
            Cursor.Current = c ?? Cursors.WaitCursor;
            return instance;
        }

        private WaitCursor()
        {
        }

        public void Dispose()
        {
            Cursor.Current = old;
        }
    }

    public class DispStatus : IDisposable
    {
        private ToolStripStatusLabel _label;
        private ToolStripProgressBar _progress;

        public DispStatus(ToolStripStatusLabel label, ToolStripProgressBar progress)
        {
            _label = label;
            _progress = progress;
        }

        public DispStatus On(string beginMessage)
        {
            _progress.Visible = true;
            _label.Visible = true;
            _label.Text = beginMessage;
            _progress.MarqueeAnimationSpeed = 50;
            _progress.Style = ProgressBarStyle.Marquee;

            return this;
        }

        public void Finish(string finishMessage)
        {
            _label.Text = finishMessage;
            _progress.Value = 100;
        }

        public void Dispose()
        {
            _label.Visible = false;
            _progress.Visible = false;
        }

    }
}