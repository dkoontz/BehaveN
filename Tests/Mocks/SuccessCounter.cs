using System;
using BehaveN;

namespace Tests {
	public class SuccessCounter : Task {
		public int Count { get; set; }

		protected override TaskResult Update(Blackboard blackboard) {
			++Count;
			return TaskResult.Success;
		}
	}
}