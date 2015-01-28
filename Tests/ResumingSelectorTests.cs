using System;
using System.Collections.Generic;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class ResumingSelectorTests {
		[Test]
		public void ResumingSelector_returns_failure_with_zero_children() {
			var sequence = ResumingSelector.Node();
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
		}

		[Test]
		public void ResumingSelector_returns_success_with_one_succeeding_child() {
			var sequence = ResumingSelector.Node(Mocks.AlwaysSucceedsNode);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Success);
		}

		[Test]
		public void ResumingSelector_returns_failure_with_one_failing_child() {
			var sequence = ResumingSelector.Node(Mocks.AlwaysFailsNode);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
		}

		[Test]
		public void ResumingSelector_returns_running_with_one_running_child() {
			var sequence = ResumingSelector.Node(Mocks.AlwaysRunningNode);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Running);
		}

		[Test]
		public void ResumingSelector_returns_failure_with_multiple_failing_nodes() {
			var callCount = new int[1];
			var counter = Mocks.FailCounterNode(callCount);
			var sequence = ResumingSelector.Node(counter, 
												 counter, 
												 counter, 
												 counter, 
												 counter);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
			callCount[0].ShouldEqual(5);
		}

		[Test]
		public void ResumingSelector_stops_when_encountering_a_running_node() {
			var callCount = new int[1];
			var counter = Mocks.FailCounterNode(callCount);
			var sequence = ResumingSelector.Node(counter, 
												 counter, 
												 Mocks.AlwaysRunningNode,
												 counter);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Running);
			callCount[0].ShouldEqual(2);
		}

		[Test]
		public void ResumingSelector_stops_when_encountering_a_success_node() {
			var callCount = new int[1];
			var counter = Mocks.FailCounterNode(callCount);
			var sequence = ResumingSelector.Node(counter, 
												 counter, 
												 Mocks.AlwaysSucceedsNode,
												 counter);
			BehaviorTree.Run(sequence, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Success);
			callCount[0].ShouldEqual(2);
		}

		[Test]
		public void ResumingSelector_resumes_on_running_node() {
			var sequence = ResumingSelector.Node(Mocks.AlwaysFailsNode, 
												 Mocks.AlwaysFailsNode, 
												 Mocks.RunningOnceNode,
												 Mocks.AlwaysSucceedsNode);
			var dictionary = Mocks.EmptyDictionary;

			BehaviorTree.Run(sequence, dictionary).ShouldEqual(NodeStatus.Running);
			BehaviorTree.Run(sequence, dictionary).ShouldEqual(NodeStatus.Success);
		}
	}
}