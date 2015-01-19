using NUnit.Framework;
using System;
using BehaveN;

namespace Tests {
	[TestFixture()]
	public class ParallelParallelSelectorTests {

		[Test]
		public void ParallelSelector_returns_failure_with_zero_children() {
			var parallelSelector = new ParallelSelector();
			Assert.AreEqual(TaskResult.Failure, parallelSelector.Tick(new Blackboard()));
		}

		[Test]
		public void ParallelSelector_returns_success_when_a_child_returns_success() {
			var parallelSelector = new ParallelSelector(new Success());
			Assert.AreEqual(TaskResult.Success, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Failure(), new Success());
			Assert.AreEqual(TaskResult.Success, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Running(), new Success());
			Assert.AreEqual(TaskResult.Success, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Success(), new Running());
			Assert.AreEqual(TaskResult.Success, parallelSelector.Tick(new Blackboard()));
		}

		[Test]
		public void ParallelSelector_returns_failure_if_all_children_returned_failure() {
			var parallelSelector = new ParallelSelector(new Failure());
			Assert.AreEqual(TaskResult.Failure, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Failure(), new Running());
			Assert.AreNotEqual(TaskResult.Failure, parallelSelector.Tick(new Blackboard()));
		}

		[Test]
		public void ParallelSelector_returns_running_if_no_child_returned_success_and_at_least_one_is_running() {
			var parallelSelector = new ParallelSelector(new Running());
			Assert.AreEqual(TaskResult.Running, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Running(), new Failure());
			Assert.AreEqual(TaskResult.Running, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Failure(), new Running());
			Assert.AreEqual(TaskResult.Running, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Success(), new Running());
			Assert.AreNotEqual(TaskResult.Running, parallelSelector.Tick(new Blackboard()));

			parallelSelector = new ParallelSelector(new Running(), new Success());
			Assert.AreNotEqual(TaskResult.Running, parallelSelector.Tick(new Blackboard()));

		}

		[Test]
		public void ParallelSelector_runs_all_behaviors_until_success() {
			var counter1 = new FailureCounter();
			var counter2 = new FailureCounter();

			var parallelSelector = new ParallelSelector(counter1, new Success(), counter2);
			parallelSelector.Tick(new Blackboard());
			Assert.AreEqual(1, counter1.Count);
			Assert.AreEqual(0, counter2.Count);

			counter1 = new FailureCounter();
			counter2 = new FailureCounter();

			parallelSelector = new ParallelSelector(counter1, new Running(), counter2);
			parallelSelector.Tick(new Blackboard());
			Assert.AreEqual(1, counter1.Count);
			Assert.AreEqual(1, counter2.Count);
		}

		[Test]
		public void ParallelSelector_starts_at_first_node_each_tick() {
			var failureCounter = new FailureCounter();
			var runningCounter = new RunningCounter();
			var failureCounter2 = new FailureCounter();
			var parallelSelector = new ParallelSelector(failureCounter, runningCounter, failureCounter2);
			var blackboard = new Blackboard();
			parallelSelector.Tick(blackboard);
			failureCounter.Count.ShouldEqual(1);
			runningCounter.Count.ShouldEqual(1);
			failureCounter2.Count.ShouldEqual(1);
			parallelSelector.Tick(blackboard);
			failureCounter.Count.ShouldEqual(2);
			runningCounter.Count.ShouldEqual(2);
			failureCounter2.Count.ShouldEqual(2);
		}

		[Test]
		public void ParallelSelector_starts_at_first_node_on_next_tick_after_all_nodes_fail() {
			var failCounter = new FailureCounter();
			var failCounter2 = new FailureCounter();
			var parallelSelector = new ParallelSelector(failCounter, failCounter2);
			var blackboard = new Blackboard();
			parallelSelector.Tick(blackboard);
			failCounter.Count.ShouldEqual(1);
			failCounter2.Count.ShouldEqual(1);
			parallelSelector.Tick(blackboard);
			failCounter.Count.ShouldEqual(2);
			failCounter2.Count.ShouldEqual(2);
		}
	}
}

