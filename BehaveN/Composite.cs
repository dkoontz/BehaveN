using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BehaveN {
	public abstract class Composite : Task {
		protected List<Task> children;

		public Composite() {
			children = new List<Task>();
		}

		public override void ForcedReset(Blackboard blackboard) {
			base.ForcedReset(blackboard);
			foreach (var child in children) {
				child.ForcedReset(blackboard);
			}
		}
	}
}