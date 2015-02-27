using System;
using System.Collections.Generic;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class ParallelTests {

		[Test]
		public void Parallel_returns_success_with_zero_children() {
			var parallel = Parallel.Node();
			BehaviorTree.Run(parallel, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Success);
		}

		[Test]
		public void Parallel_returns_success_with_one_succeeding_child() {
			var parallel = Parallel.Node(Mocks.AlwaysSucceedsNode);
			BehaviorTree.Run(parallel, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Success);
		}

		[Test]
		public void Parallel_returns_failure_with_one_failing_child() {
			var parallel = Parallel.Node(Mocks.AlwaysFailsNode);
			BehaviorTree.Run(parallel, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
		}

		[Test]
		public void Parallel_runs_all_children_regardless_of_failure_or_running_status() {
			var callCount = new int[1];
			var parallel = Parallel.Node(
				Mocks.AlwaysFailsNode, 
				Mocks.AlwaysRunningNode, 
				Mocks.SucceedCounterNode(callCount));
			BehaviorTree.Run(parallel, Mocks.EmptyDictionary);
			callCount[0].ShouldEqual(1);
		}
	}
}