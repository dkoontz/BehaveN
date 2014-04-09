using System;

namespace BehaveN {
	public interface IBehaviorTree {
		TaskResult Tick();
		void SetBlackboardValue(Enum name, object value);
		void SetBlackboardValue(string name, object value);
	}
}