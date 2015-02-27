using System;
using System.Collections.Generic;

namespace BehaveN {
	using NodeState = Dictionary<string, object>;
	using NodeDictionary = Dictionary<object, Dictionary<string, object>>;

	public static class BehaviorTree {
		
		public static NodeStatus Run(Node node, NodeDictionary nodeDictionary) {
			NodeState nodeState;
			
			if (!nodeDictionary.ContainsKey(node)) {
				nodeState = new Dictionary<string, object>();
				node.OnInitialize(nodeDictionary, nodeState);
				nodeDictionary[node] = nodeState;
			}
			else {
				nodeState = nodeDictionary[node];
			}
			
			var status = node.OnTick(nodeDictionary, nodeState);
			
			if (status == NodeStatus.Success) {
				node.OnSuccess(nodeDictionary, nodeState);
			}
			
			return status;
		}
	}
}