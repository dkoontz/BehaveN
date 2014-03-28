using System;
using BehaveN;

namespace Tests {
	public class Failure : Task {
		protected override TaskResult Update(Blackboard blackboard) {
			return TaskResult.Failure;
		}
	}
}