using System;
using System.Collections.Generic;

namespace BehaveN {
	/// <summary>
	/// Runs each Behavior until one return Success or Running.
	/// Returns Failure if no Behaviors return Success or Running.
	/// </summary>
	public class Selector : Composite {
		List<Task>.Enumerator childEnumerator;

		public Selector(params Task[] behaviors) {
			children = new List<Task>(behaviors);
		}

		public override void OnInitialize() {
			childEnumerator = children.GetEnumerator();
		}

		protected override TaskResult Update(Context context) {
			TaskResult currentStatus;
			while (childEnumerator.MoveNext()) {
				currentStatus = childEnumerator.Current.Tick(context);

				if (currentStatus == TaskResult.Success || currentStatus == TaskResult.Running) {
					return currentStatus;
				}
			}
			return TaskResult.Failure;
		}
	}
}