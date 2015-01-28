using System;

namespace BehaveN {
	public static class Selector {
		public static Node Node(params Node[] nodes) {
			return new Node {
				OnTick = Tick(nodes)
			};
		}

		public static TickFunction Tick(params Node[] nodes) {
			return (nodeDictionary, nodeState) => {
				foreach (var node in nodes) {
					var status = BehaviorTree.Run(node, nodeDictionary);
					if (status == NodeStatus.Running || status == NodeStatus.Success) {
						return status;
					}
				}

				return NodeStatus.Failure;
			};
		}
	}
}