using System;
using System.Collections.Generic;

namespace BehaveN
{
	public enum TaskResult {
		NotInitialized,
		Running,
		Success,
		Failure,
	}

	public abstract class Task
	{
		TaskResult status = TaskResult.NotInitialized;

		public TaskResult Tick(Context context) {
			if (status == TaskResult.NotInitialized) {
				OnInitialize();
			}

			status = Update(context);

			if (status != TaskResult.Running) {
				OnMovingToNextNode(status);
			}

			return status;
		}

		public virtual void OnInitialize() { }
		public virtual void OnMovingToNextNode(TaskResult status) { }

		protected abstract TaskResult Update(Context context);
	}
}