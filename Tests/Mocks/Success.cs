using System;
using BehaveN;

namespace Tests {
	public class Success : Task {
		protected override TaskResult Update(Context context) {
			return TaskResult.Success;
		}
	}
}