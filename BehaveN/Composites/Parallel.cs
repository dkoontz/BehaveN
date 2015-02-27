namespace BehaveN {
	public static class Parallel {
		public static Node Node(params Node[] nodes) {
			return new Node {
				OnTick = Tick(nodes)
			};
		}

		public static TickFunction Tick(params Node[] nodes) {
			var status = NodeStatus.Success;
			return (nodeDictionary, nodeState) => {
				foreach (var node in nodes) {
					if (BehaviorTree.Run(node, nodeDictionary) != NodeStatus.Success) {
						status = NodeStatus.Failure;
					}
				}

				return status;
			};
		}
	}
}