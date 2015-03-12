namespace BehaveN {
	[BehaviorTreeNodeCollection]
	public static class ResumingSelector {

		const string CURRENT_INDEX = "CurrentIndex";

		[BehaviorTreeNode("Composite/Resuming Selector")]
		public static Node Node(params Node[] nodes) {
			return new Node {
				OnInitialize = Init(),
				OnTick = Tick(nodes)
			};
		}

		public static InitializeFunction Init() {
			return (nodeDictionary, nodeState) => nodeState[CURRENT_INDEX] = 0;
		}

		public static TickFunction Tick(params Node[] nodes) {
			return (nodeDictionary, nodeState) => {
				var index = (int)nodeState[CURRENT_INDEX];

				for (var i = index; i < nodes.Length; ++i) {
					var status = BehaviorTree.Run(nodes[i], nodeDictionary);
					if (status == NodeStatus.Running || status == NodeStatus.Success) {
						nodeState[CURRENT_INDEX] = i;
						return status;
					}
				}

				nodeState[CURRENT_INDEX] = 0;
				return NodeStatus.Failure;
			};
		}
	}
}