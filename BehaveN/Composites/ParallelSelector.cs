using System;
using System.Collections.Generic;

namespace BehaveN {
	/// <summary>
	/// Runs each Behavior until one return Success or Running.
	/// Returns Failure if no Behaviors return Success.
	/// </summary>
	public class Parallel : Composite {

		public Parallel(params Task[] behaviors) {
			children = new List<Task>(behaviors);
		}

		public override void OnInitialize(Blackboard blackboard) {

		}

		protected override TaskResult Update(Blackboard blackboard) {
			var result = TaskResult.Failure;
			foreach (var child in children) {
				var childResult = child.Tick(blackboard);
				if (childResult == TaskResult.Success) {
					// stop other children
					return TaskResult.Success;
				} else if (childResult == TaskResult.Running) {
					result = TaskResult.Running;
				}
			}
			return result;
		}
	}
}