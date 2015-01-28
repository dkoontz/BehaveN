using System;
using System.Collections.Generic;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class InverterTests {

		[Test]
		public void Inverter_changes_success_to_failure() {
			var successNode = Inverter.Node(Mocks.AlwaysSucceedsNode);
			BehaviorTree.Run(successNode, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Failure);
		}

		[Test]
		public void Inverter_changes_failure_to_success() {
			var successNode = Inverter.Node(Mocks.AlwaysFailsNode);
			BehaviorTree.Run(successNode, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Success);
		}

		[Test]
		public void Inverter_does_not_change_running() {
			var successNode = Inverter.Node(Mocks.AlwaysRunningNode);
			BehaviorTree.Run(successNode, Mocks.EmptyDictionary).ShouldEqual(NodeStatus.Running);
		}
	}
}