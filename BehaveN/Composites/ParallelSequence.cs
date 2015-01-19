using System;
using System.Collections.Generic;

namespace BehaveN {
	/// <summary>
	/// Runs each Behavior until one returns Failure or Running.
	/// Returns Success if all Behaviors return Success.
	/// Starts over at the first child each time it is ticked
	/// </summary>
	public class ParallelSequence : Composite {

		public ParallelSequence(params Task[] behaviors) {
			children = new List<Task>(behaviors);
		}

		protected override TaskResult Update(Blackboard blackboard) {
			var result = TaskResult.Success;
			foreach (var child in children) {
				var childResult = child.Tick(blackboard);
				if (childResult == TaskResult.Failure) {
					foreach (var childToCheckIfRunning in children) {
						if (childToCheckIfRunning.Status == TaskResult.Running) {
							childToCheckIfRunning.ForcedReset(blackboard);
						}
					}

					return TaskResult.Failure;
				} else if (childResult == TaskResult.Running) {
					result = TaskResult.Running;
				}
			}
			return result;
		}
	}
}