using System;
using System.Collections.Generic;
using BehaveN;

namespace Tests {
	public class Mocks {
		public static Dictionary<Node, Dictionary<string, object>> EmptyDictionary {
			get { return new Dictionary<Node, Dictionary<string, object>>(); }
		}

		public static Node AlwaysSucceedsNode {
			get {
				return new Node {
					OnTick = (_, __) => NodeStatus.Success
				};
			}
		}

		public static Node AlwaysFailsNode {
			get {
				return new Node {
					OnTick = (_, __) => NodeStatus.Failure
				};
			}
		}

		public static Node AlwaysRunningNode {
			get {
				return new Node {
					OnTick = (_, __) => NodeStatus.Running
				};
			}
		}

		public static Node RunningOnceNode {
			get {
				return new Node {
					OnInitialize = (_, state) => state["TimesRun"] = 0,
					OnTick = (_, state) => {
						if ((int)state["TimesRun"] == 0) {
							state["TimesRun"] = 1;
							return NodeStatus.Running;
						}
  						return NodeStatus.Success;
					}
				};
			}
		}

		public static Node FailOnceNode {
			get {
				return new Node {
					OnInitialize = (_, state) => state["TimesRun"] = 0,
					OnTick = (_, state) => {
						if ((int)state["TimesRun"] == 0) {
							state["TimesRun"] = 1;
							return NodeStatus.Failure;
						}
						return NodeStatus.Success;
					}
				};
			}
		}

		public static Node SucceedCounterNode(int[] count) {
			return new Node {
				OnTick = (_, __) => {
					++count[0];
					return NodeStatus.Success;
				}
			};
		}

		public static Node FailCounterNode(int[] count) {
			return new Node {
				OnTick = (_, __) => {
					++count[0];
					return NodeStatus.Failure;
				}
			};
		}
	}
}