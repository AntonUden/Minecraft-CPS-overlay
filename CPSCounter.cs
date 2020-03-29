using System;
using System.Timers;

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
			timer.Elapsed += timerEvent;
		}

		private void timerEvent(Object source, ElapsedEventArgs e) {
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
			return this.cps;
		}

		public void click() {
			this.cpsCounter++;
		}
	}
}