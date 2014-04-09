using System;

namespace BehaveN {
	public class BehaviorTree : IBehaviorTree {
		protected Blackboard Blackboard { get; private set; }
		protected ITask RootNode { get; set; }

		public BehaviorTree() : this(new Blackboard()) { }

		public BehaviorTree(Blackboard blackboard) {
			Blackboard = blackboard;
		}

		public BehaviorTree(ITask rootNode) : this(rootNode, null) { }

		public BehaviorTree(ITask rootNode, Blackboard blackboard) : this(blackboard) {
			RootNode = rootNode;
		}

		public virtual TaskResult Tick() {
			return RootNode.Tick(Blackboard);
		}

		public void SetBlackboardValue(Enum name, object value) {
			Blackboard.Set(name, value);
		}

		public void SetBlackboardValue(string name, object value) {
			Blackboard.Set(name, value);
		}
	}
}