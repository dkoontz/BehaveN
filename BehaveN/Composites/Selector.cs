using System;
using System.Collections.Generic;

namespace BehaveN {
	/// <summary>
	/// Runs each Behavior until one return Success or Running.
	/// Returns Failure if no Behaviors return Success.
	/// </summary>
	public class Selector : Composite {
		List<Task>.Enumerator childEnumerator;
		TaskResult previousStatus;
		bool atEndOfList;

		public Selector(params Task[] behaviors) {
			children = new List<Task>(behaviors);
		}

		public override void OnInitialize(Blackboard blackboard) {
			childEnumerator = children.GetEnumerator();
			atEndOfList = false;
		}

		protected override TaskResult Update(Blackboard blackboard) {
			TaskResult currentStatus = TaskResult.Failure;

			if (previousStatus == TaskResult.Success || atEndOfList) {
				OnInitialize(blackboard);
			}

			if (previousStatus == TaskResult.Running) {
				currentStatus = childEnumerator.Current.Tick(blackboard);
				previousStatus = currentStatus;
			}

			while (currentStatus == TaskResult.Failure && !atEndOfList) {
				atEndOfList = !childEnumerator.MoveNext();
				if (!atEndOfList) {
					currentStatus = childEnumerator.Current.Tick(blackboard);
					previousStatus = currentStatus;
				}
			}

			return currentStatus;
		}
	}
}