using System;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class SelectorTests {

		[Test]
		public void Selector_returns_failure_with_zero_children() {
			var selector = new Selector();
			Assert.AreEqual(TaskResult.Failure, selector.Tick(new Blackboard()));
		}

		[Test]
		public void Selector_returns_success_when_a_child_returns_success() {
			var selector = new Selector(new Success());
			Assert.AreEqual(TaskResult.Success, selector.Tick(new Blackboard()));

			selector = new Selector(new Failure(), new Success());
			Assert.AreEqual(TaskResult.Success, selector.Tick(new Blackboard()));

			selector = new Selector(new Running(), new Success());
			Assert.AreNotEqual(TaskResult.Success, selector.Tick(new Blackboard()));
		}

		[Test]
		public void Selector_returns_running_when_a_child_returns_running() {
			var selector = new Selector(new Running());
			Assert.AreEqual(TaskResult.Running, selector.Tick(new Blackboard()));

			selector = new Selector(new Failure(), new Running());
			Assert.AreEqual(TaskResult.Running, selector.Tick(new Blackboard()));

			selector = new Selector(new Success(), new Running());
			Assert.AreNotEqual(TaskResult.Running, selector.Tick(new Blackboard()));
		}

		[Test]
		public void Selector_runs_all_behaviors_until_success_or_running_result() {
			var counter1 = new FailureCounter();
			var counter2 = new FailureCounter();

			var selector = new Selector(counter1, new Success(), counter2);
			selector.Tick(new Blackboard());
			Assert.AreEqual(1, counter1.Count);
			Assert.AreEqual(0, counter2.Count);

			counter1 = new FailureCounter();
			counter2 = new FailureCounter();

			selector = new Selector(counter1, new Running(), counter2);
			selector.Tick(new Blackboard());
			Assert.AreEqual(1, counter1.Count);
			Assert.AreEqual(0, counter2.Count);
		}

		[Test]
		public void Selector_resumes_at_running_node() {
			var failureCounter = new FailureCounter();
			var runningCounter = new RunningCounter();
			var selector = new Selector(failureCounter, runningCounter);
			var blackboard = new Blackboard();
			selector.Tick(blackboard);
			failureCounter.Count.ShouldEqual(1);
			runningCounter.Count.ShouldEqual(1);
			selector.Tick(blackboard);
			failureCounter.Count.ShouldEqual(1);
			runningCounter.Count.ShouldEqual(2);
		}

		[Test]
		public void Selector_starts_at_first_node_on_next_tick_after_all_nodes_fail() {
			var failCounter = new FailureCounter();
			var failCounter2 = new FailureCounter();
			var selector = new Selector(failCounter, failCounter2);
			var blackboard = new Blackboard();
			selector.Tick(blackboard);
			failCounter.Count.ShouldEqual(1);
			failCounter2.Count.ShouldEqual(1);
			selector.Tick(blackboard);
			failCounter.Count.ShouldEqual(2);
			failCounter2.Count.ShouldEqual(2);
		}
	}
}