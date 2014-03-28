using System;
using BehaveN;

namespace Tests {
	public class FailureCounter : Task {
		public int Count { get; set; }

		protected override TaskResult Update(Blackboard blackboard) {
			++Count;
			return TaskResult.Failure;
		}
	}
}