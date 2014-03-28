using System;

namespace BehaveN {
	public interface IBehaviorTree {
		void Tick();
		void SetBlackboardValue(Enum name, object value);
		void SetBlackboardValue(string name, object value);
	}
}