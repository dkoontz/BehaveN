namespace BehaveN {
	[BehaviorTreeNodeCollection]
	public static class Inverter {
		[BehaviorTreeNode("Decorator/Inverter")]
		public static Node Node(Node child) {
			return new Node {
				OnInitialize = child.OnInitialize,
				OnTick = (nodeDictionary, nodeState) => {
					var status = child.OnTick(nodeDictionary, nodeState);
					if (status == NodeStatus.Success) {
						return NodeStatus.Failure;
					}
					else if (status == NodeStatus.Failure) {
						return NodeStatus.Success;
					}

					return status;
				},
				OnSuccess = child.OnSuccess
			};
		}
	}
}