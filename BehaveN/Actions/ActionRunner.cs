using System;

namespace BehaveN {
	public class ActionRunner : Task {
		Func<Context, TaskResult> action;

		public ActionRunner(Func<Context, TaskResult> action) {
			this.action = action;
		}

		protected override TaskResult Update(Context context) {
			return action(context);
		}
	}
}