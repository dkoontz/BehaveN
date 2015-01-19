using System;
using System.Collections.Generic;

namespace BehaveN
{
	public abstract class Task : ITask
	{
		public TaskResult Status { get; private set; }

		protected Task() {
			Status = TaskResult.NotInitialized;
		}

		public TaskResult Tick(Blackboard blackboard) {
			if (Status == TaskResult.NotInitialized) {
				OnInitialize(blackboard);
			}

			Status = Update(blackboard);

			if (Status != TaskResult.Running) {
				OnReset(blackboard);
			}

			return Status;
		}

		public virtual void ForcedReset(Blackboard blackboard) {
			OnReset(blackboard);
		}

		public virtual void OnInitialize(Blackboard blackboard) { }
		public virtual void OnReset(Blackboard blackboard) { }

		protected abstract TaskResult Update(Blackboard blackboard);
	}
}