using System;

namespace BehaveN {
	public class BehaviorTree : IBehaviorTree {
		protected Blackboard Blackboard { get; private set; }
		protected ITask RootNode { get; set; }

		public BehaviorTree() {
			Blackboard = new Blackboard();
		}

		public virtual void Tick() {
			RootNode.Tick(Blackboard);
		}

		public void SetBlackboardValue(Enum name, object value) {
			Blackboard.Set(name, value);
		}

		public void SetBlackboardValue(string name, object value) {
			Blackboard.Set(name, value);
		}
	}
}