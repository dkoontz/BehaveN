using System;

namespace BehaveN {
	public enum TaskResult {
		NotInitialized,
		Running,
		Success,
		Failure,
	}

	public interface ITask {
		TaskResult Tick(Blackboard blackboard);
		void OnInitialize(Blackboard blackboard);
		void OnReset(Blackboard blackboard);
	}
}