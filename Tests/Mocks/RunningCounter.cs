using System;
using BehaveN;

namespace Tests {
	public class RunningCounter : Task {
		public int Count { get; set; }

		protected override TaskResult Update(Context context) {
			++Count;
			return TaskResult.Running;
		}
	}
}