using System;
using System.Collections.Generic;

namespace BehaveN {
	public abstract class Composite : Task {
		protected List<Task> children;

		public Composite() {
			children = new List<Task>();
		}
	}
}