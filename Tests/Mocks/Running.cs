using System;
using BehaveN;

namespace Tests {
	public class Running : Task {
		protected override TaskResult Update(Blackboard blackboard) {
			return TaskResult.Running;
		}
	}
}