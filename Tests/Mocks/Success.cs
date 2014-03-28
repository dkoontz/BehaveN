using System;
using BehaveN;

namespace Tests {
	public class Success : Task {
		protected override TaskResult Update(Blackboard blackboard) {
			return TaskResult.Success;
		}
	}
}