using System;
using BehaveN;

namespace Tests {
	public class Running : Task {
		protected override TaskResult Update(Context context) {
			return TaskResult.Running;
		}
	}
}