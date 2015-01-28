using System;
using System.Collections.Generic;

namespace BehaveN {
	using NodeState = Dictionary<string, object>;
	using NodeDictionary = Dictionary<Node, Dictionary<string, object>>;
	public delegate void InitializeFunction(NodeDictionary nodeDictionary, NodeState state);
	public delegate NodeStatus TickFunction(NodeDictionary nodeDictionary, NodeState state);
	public delegate void SuccessFunction(NodeDictionary nodeDictionary, NodeState state);

	public enum NodeStatus {
		Running,
		Success,
		Failure,
	}

	public class Node {
		public InitializeFunction OnInitialize = NullFunctions.Initialize();
		public TickFunction OnTick = NullFunctions.Tick();
		public SuccessFunction OnSuccess = NullFunctions.Success();
//		// OnReset, used with parallel composites
	}

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

	public static class NullFunctions {
		public static InitializeFunction Initialize() {
			return (_, __) => { };
		}

		public static TickFunction Tick() {
			return (_, __) => NodeStatus.Success;
		}

		public static SuccessFunction Success() {
			return (_, __) => { };
		}
	}
		
	public static class Decorators {

//		Other possible decorators
//
//		Succeeder
//
//		Always return success, ignores actual return value of child. Useful to force a branch to execute without effecting the overall execution.
//
//		Repeater
//
//		Runs its child each time the child returns a result. Can have a max number of runs.
//
//		Repeat Until Fail
//
//		Like a repeater but continues until the child returns failure.


	}
}