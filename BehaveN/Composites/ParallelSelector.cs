using System;
using System.Collections.Generic;

namespace BehaveN {
	/// <summary>
	/// Runs each Behavior until one return Success or Running.
	/// Returns Failure if no Behaviors return Success.
	/// Starts over at the first child each time it is ticked
	/// </summary>
	public class ParallelSelector : Composite {

		public ParallelSelector(params Task[] behaviors) {
			children = new List<Task>(behaviors);
		}

		protected override TaskResult Update(Blackboard blackboard) {
			var result = TaskResult.Failure;
			foreach (var child in children) {
				var childResult = child.Tick(blackboard);
				if (childResult == TaskResult.Success) {
					foreach (var childToCheckIfRunning in children) {
						if (childToCheckIfRunning.Status == TaskResult.Running) {
							childToCheckIfRunning.ForcedReset(blackboard);
						}
					}

					return TaskResult.Success;
				} else if (childResult == TaskResult.Running) {
					result = TaskResult.Running;
				}
			}
			return result;
		}
	}
}