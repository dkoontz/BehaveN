using System;
using System.Collections.Generic;

namespace BehaveN
{
	public abstract class Task : ITask
	{
		TaskResult status = TaskResult.NotInitialized;

		public TaskResult Tick(Blackboard blackboard) {
			if (status == TaskResult.NotInitialized) {
				OnInitialize(blackboard);
			}

			status = Update(blackboard);

			if (status != TaskResult.Running) {
				OnReset(blackboard);
			}

			return status;
		}

		public virtual void OnInitialize(Blackboard blackboard) { }
		public virtual void OnReset(Blackboard blackboard) { }

		protected abstract TaskResult Update(Blackboard blackboard);
	}
}