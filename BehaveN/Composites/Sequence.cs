using System;
using System.Collections.Generic;

namespace BehaveN {
	/// <summary>
	/// Runs each Behavior until one returns Failure or Running.
	/// Returns Success if all Behaviors return Success.
	/// </summary>
	public class Sequence : Composite {
		List<Task>.Enumerator childEnumerator;
		TaskResult previousStatus;
		bool atEndOfList;

		public Sequence(params Task[] behaviors) {
			children = new List<Task>(behaviors);
		}

		public override void OnInitialize(Blackboard blackboard) {
			childEnumerator = children.GetEnumerator();
			atEndOfList = false;
		}

		public override void OnReset(Blackboard blackboard) {
			OnInitialize(blackboard);
		}

		protected override TaskResult Update(Blackboard blackboard) {
			TaskResult currentStatus = TaskResult.Success;

			if (previousStatus == TaskResult.Running) {
				currentStatus = childEnumerator.Current.Tick(blackboard);
				previousStatus = currentStatus;
			}
				
			while (currentStatus == TaskResult.Success && !atEndOfList) {
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