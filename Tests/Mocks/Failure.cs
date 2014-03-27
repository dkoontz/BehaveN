using System;
using BehaveN;

namespace Tests {
	public class Failure : Task {
		protected override TaskResult Update(Context context) {
			return TaskResult.Failure;
		}
	}
}