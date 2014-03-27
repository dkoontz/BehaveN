using System;
using System.Linq;
using System.Collections.Generic;

namespace BehaveN {
	/// <summary>
	/// Runs each Behavior until one returns Failure.
	/// Returns Success if all Behaviors return Success.
	/// </summary>
	public class Sequence : Composite {
		List<Task>.Enumerator childEnumerator;
		TaskResult previousStatus;

		public Sequence(params Task[] behaviors) {
			children = new List<Task>(behaviors);
		}

		public override void OnInitialize() {
			childEnumerator = children.GetEnumerator();
		}

		protected override TaskResult Update(Context context) {
			bool atEndOfList = false;
			TaskResult currentStatus = TaskResult.Success;

			if (previousStatus == TaskResult.Failure) {
				OnInitialize();
			}

			if (previousStatus == TaskResult.Running) {
				currentStatus = childEnumerator.Current.Tick(context);
				previousStatus = currentStatus;
			}
				
			while (currentStatus == TaskResult.Success && !atEndOfList) {
				atEndOfList = !childEnumerator.MoveNext();
				if (!atEndOfList) {
					currentStatus = childEnumerator.Current.Tick(context);
					previousStatus = currentStatus;
				}
			}

			return currentStatus;
		}
	}
}