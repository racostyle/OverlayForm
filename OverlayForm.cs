using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overlays
{
    public partial class OverlayForm : Form
    {
        private bool isActive;

        public OverlayForm(Form formToCover)
        {
            InitializeComponent();

            this.BackColor = Color.Black;
            this.Opacity = .85f;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.AutoScaleMode = AutoScaleMode.None;
            this.Location = formToCover.PointToScreen(Point.Empty);
            this.ClientSize = formToCover.ClientSize;
            this.FormClosing += OverlayForm_FormClosing;

            var size = this.Width / 9;
            var posY = this.Height / 2 - size;

            pnlProgress.Width = size;
            pnlProgress.Height = size;
            pnlProgress.Location = new Point(size, posY);

            var size2 = size * 1.5f;
            lblProgress.Text = "Working...";
            lblProgress.ForeColor = Color.White;
            lblProgress.Location = new Point(this.Width / 2 - lblProgress.Width / 2, pnlProgress.Location.Y + (int)size2);

            isActive = true;
            var counter = 0;

            _ = Task.Run(async () =>
            {
                while (isActive)
                {
                    try
                    {
                        switch (counter)
                        {
                            case 0:
                                SetPanelPosition(pnlProgress, size, posY);
                                break;
                            case 1:
                                SetPanelPosition(pnlProgress, size * 3, posY);
                                break;
                            case 2:
                                SetPanelPosition(pnlProgress, size * 5, posY);
                                break;
                            default:
                                SetPanelPosition(pnlProgress, size * 7, posY);
                                break;
                        }
                        counter = counter < 3 ? counter + 1 : 0;
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync(ex.Message);
                    }
                    finally
                    {
                        await Task.Delay(150);
                    }
                }
            });
        }

        private void OverlayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isActive = false;
        }

        private void SetPanelPosition(Panel panel, int posX, int posY)
        {
            if (panel.InvokeRequired)
            {
                panel.BeginInvoke((MethodInvoker)delegate ()
                {
                    panel.Location = new Point(posX, posY);
                });
            }
            else
            {
                panel.Location = new Point(posX, posY);
            }
        }
    }
}
