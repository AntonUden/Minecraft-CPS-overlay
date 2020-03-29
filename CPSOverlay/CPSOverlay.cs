using System;
using System.Drawing;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace CPSOverlay {
	public partial class CPSOverlay : Form {
		private IKeyboardMouseEvents m_GlobalHook;

		private CPSCounter rmbCPSCounter;
		private CPSCounter lmbCPSCounter;

		private Timer updateTimer;

		public CPSOverlay() {
			InitializeComponent();
		}

		private void CPSOverlay_Load(object sender, EventArgs e) {
			Screen screen = Screen.PrimaryScreen;

			this.TopMost = true;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.ControlBox = false;
			this.Text = String.Empty;
			this.Location = new Point(screen.Bounds.X, screen.Bounds.Y);
			this.Width = screen.Bounds.Width;
			this.Height = screen.Bounds.Height;
			this.ShowInTaskbar = true;

			lbl_rmbCps.BackColor = Color.Transparent;
			lbl_lmbCps.BackColor = Color.Transparent;

			lbl_rmb.BackColor = Color.Transparent;
			lbl_lmb.BackColor = Color.Transparent;

			lbl_w.BackColor = Color.Transparent;
			lbl_a.BackColor = Color.Transparent;
			lbl_s.BackColor = Color.Transparent;
			lbl_d.BackColor = Color.Transparent;

			rmbCPSCounter = new CPSCounter();
			lmbCPSCounter = new CPSCounter();

			m_GlobalHook = Hook.GlobalEvents();

			m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
			m_GlobalHook.MouseUpExt += GlobalHookMouseUpExt;
			m_GlobalHook.KeyDown += GlobalHookKeyDown;
			m_GlobalHook.KeyUp += GlobalHookKeyUp;

			updateTimer = new Timer();
			updateTimer.Tick += timerEvent;
			updateTimer.Interval = 500;
			updateTimer.Start();
		}

		private void timerEvent(Object source, EventArgs e) {
			lbl_rmbCps.Text = "RMB CPS: " + rmbCPSCounter.getCps();
			lbl_lmbCps.Text = "LMB CPS: " + lmbCPSCounter.getCps();
		}

		private void GlobalHookKeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.W) {
				lbl_w.ForeColor = Color.Green;
			} else if (e.KeyCode == Keys.A) {
				lbl_a.ForeColor = Color.Green;
			} else if (e.KeyCode == Keys.S) {
				lbl_s.ForeColor = Color.Green;
			} else if (e.KeyCode == Keys.D) {
				lbl_d.ForeColor = Color.Green;
			} else if (e.KeyCode == Keys.Space) {
				lbl_space.ForeColor = Color.Green;
			}
		}

		private void GlobalHookKeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.W) {
				lbl_w.ForeColor = Color.Red;
			} else if (e.KeyCode == Keys.A) {
				lbl_a.ForeColor = Color.Red;
			} else if (e.KeyCode == Keys.S) {
				lbl_s.ForeColor = Color.Red;
			} else if (e.KeyCode == Keys.D) {
				lbl_d.ForeColor = Color.Red;
			} else if (e.KeyCode == Keys.Space) {
				lbl_space.ForeColor = Color.Red;
			}
		}

		private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e) {
			if (e.Button == MouseButtons.Left) {
				lbl_lmb.ForeColor = Color.Green;
				lmbCPSCounter.click();
			} else if (e.Button == MouseButtons.Right) {
				lbl_rmb.ForeColor = Color.Green;
				rmbCPSCounter.click();
			}
		}

		private void GlobalHookMouseUpExt(object sender, MouseEventExtArgs e) {
			if (e.Button == MouseButtons.Left) {
				lbl_lmb.ForeColor = Color.Red;
			} else if (e.Button == MouseButtons.Right) {
				lbl_rmb.ForeColor = Color.Red;
			}
		}

		public void unsubscribe() {
			m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
			m_GlobalHook.MouseUpExt -= GlobalHookMouseUpExt;
			m_GlobalHook.KeyDown -= GlobalHookKeyDown;
			m_GlobalHook.KeyUp -= GlobalHookKeyUp;

			m_GlobalHook.Dispose();

			updateTimer.Stop();

			rmbCPSCounter.stop();
			lmbCPSCounter.stop();
		}

		private void CPSOverlay_FormClosing(object sender, FormClosingEventArgs e) {
			unsubscribe();
		}
	}
}