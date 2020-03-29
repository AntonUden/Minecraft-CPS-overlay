using System;
using System.Windows.Forms;

namespace CPSOverlay {
	public class CPSCounter {
		private int cps;
		private int cpsCounter;
		private Timer timer;

		public CPSCounter() {
			cps = 0;
			cpsCounter = 0;

			timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += timerEvent;
			timer.Start();
		}

		private void timerEvent(Object source, EventArgs e) {
			cps = cpsCounter;
			cpsCounter = 0;
		}

		public void stop() {
			if (timer != null) {
				timer.Stop();
				timer.Dispose();
			}
		}

		public int getCps() {
			return cps;
		}

		public void click() {
			cpsCounter++;
		}
	}
}