using System;

namespace BehaveN {
	public class ActionRunner : Task {
		Func<Blackboard, TaskResult> action;

		public ActionRunner(Func<Blackboard, TaskResult> action) {
			this.action = action;
		}

		protected override TaskResult Update(Blackboard blackboard) {
			return action(blackboard);
		}
	}
}