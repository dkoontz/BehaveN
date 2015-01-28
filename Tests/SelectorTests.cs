using System;
using System.Collections.Generic;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class SelectorTests {

		[Test]
		public void Selector_returns_failure_with_zero_children() {
			var sequence = Selector.Node();
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
		}

		[Test]
		public void Selector_returns_success_with_one_succeeding_child() {
			var sequence = Selector.Node(Mocks.AlwaysSucceedsNode);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Success);
		}

		[Test]
		public void Selector_returns_failure_with_one_failing_child() {
			var sequence = Selector.Node(Mocks.AlwaysFailsNode);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
		}

		[Test]
		public void Selector_returns_running_with_one_running_child() {
			var sequence = Selector.Node(Mocks.AlwaysRunningNode);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Running);
		}

		[Test]
		public void Selector_continues_past_failing_nodes() {
			var callCount = new int[1];
			var counter = Mocks.FailCounterNode(callCount);
			var sequence = Selector.Node(counter, 
										 counter, 
										 counter, 
										 counter);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
			callCount[0].ShouldEqual(4);
		}

		[Test]
		public void Selector_stops_at_success_nodes() {
			var callCount = new int[1];
			var counter = Mocks.FailCounterNode(callCount);
			var sequence = Selector.Node(counter, 
										 counter, 
										 counter, 
										 Mocks.AlwaysSucceedsNode, 
										 counter);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Success);
			callCount[0].ShouldEqual(3);
		}

		[Test]
		public void Selector_stops_at_running_nodes() {
			var callCount = new int[1];
			var counter = Mocks.FailCounterNode(callCount);
			var sequence = Selector.Node(counter, 
										 counter, 
										 counter, 
										 Mocks.AlwaysRunningNode, 
										 counter);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Running);
			callCount[0].ShouldEqual(3);
		}
	}
}