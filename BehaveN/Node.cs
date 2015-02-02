using System;
using System.Collections.Generic;

namespace BehaveN {
	using NodeState = Dictionary<string, object>;
	using NodeDictionary = Dictionary<object, Dictionary<string, object>>;
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