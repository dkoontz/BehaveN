namespace BehaveN {
	public static class ResumingSequence {

		const string CURRENT_INDEX = "CurrentIndex";

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
					if (status == NodeStatus.Running) {
						nodeState[CURRENT_INDEX] = i;
						return status;
					}
					else if (status == NodeStatus.Failure) {
						return status;
					}
				}

				return NodeStatus.Success;
			};
		}
	}
}