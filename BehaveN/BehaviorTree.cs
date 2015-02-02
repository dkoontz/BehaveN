using System;
using System.Collections.Generic;

namespace BehaveN {
	using NodeState = Dictionary<string, object>;
	using NodeDictionary = Dictionary<object, Dictionary<string, object>>;

	public static class BehaviorTree {
		public static Func<Dictionary<string, object>> ObtainNodeState = () => new Dictionary<string, object>();
		public static Action<Dictionary<string, object>> ReleaseNodeState = state => { };
		
		public static NodeStatus Run(Node node, NodeDictionary nodeDictionary) {
			NodeState nodeState;
			
			if (!nodeDictionary.ContainsKey(node)) {
				nodeState = ObtainNodeState();
				node.OnInitialize(nodeDictionary, nodeState);
				nodeDictionary[node] = nodeState;
			}
			else {
				nodeState = nodeDictionary[node];
			}
			
			var status = node.OnTick(nodeDictionary, nodeState);
			
			if (status == NodeStatus.Success) {
				node.OnSuccess(nodeDictionary, nodeState);
				nodeDictionary.Remove(node);
			}
			else if (status == NodeStatus.Failure) {
				var state = nodeDictionary[node];
				state.Clear();
				nodeDictionary.Remove(node);
				ReleaseNodeState(state);
			}
			
			return status;
		}
	}
}