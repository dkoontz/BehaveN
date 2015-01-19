using System;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class ParallelSequenceTests {
		[Test]
		public void ParallelSequence_returns_success_with_zero_children() {
			var sequence = new ParallelSequence();
			sequence.Tick(new Blackboard()).ShouldEqual(TaskResult.Success);
		}

		[Test]
		public void ParallelSequence_returns_after_first_failure() {
			var counter = new SuccessCounter();
			var sequence = new ParallelSequence(new Failure());
			sequence.Tick(new Blackboard());
			counter.Count.ShouldEqual(0);
		}

		[Test]
		public void ParallelSequence_runs_all_behaviors_until_a_non_success_result() {
			var counter1 = new SuccessCounter();
			var counter2 = new SuccessCounter();
			var counter3 = new SuccessCounter();
			var sequence = new ParallelSequence(counter1, counter2, new Failure(), counter3);
			sequence.Tick(new Blackboard());
			counter1.Count.ShouldEqual(1);
			counter2.Count.ShouldEqual(1);
			counter3.Count.ShouldEqual(0);
		}

		[Test]
		public void ParallelSequence_returns_success_when_all_behaviors_are_successful() {
			var sequence = new ParallelSequence(new Success(), new Success(), new Success());
			sequence.Tick(new Blackboard()).ShouldEqual(TaskResult.Success);
		}

		[Test]
		public void ParallelSequence_passes_along_result_from_behavior_if_not_successful() {
			var sequence = new ParallelSequence(new Success(), new Failure(), new Success());
			sequence.Tick(new Blackboard()).ShouldEqual(TaskResult.Failure);

			sequence = new ParallelSequence(new Success(), new Running(), new Success());
			sequence.Tick(new Blackboard()).ShouldEqual(TaskResult.Running);
		}

		[Test]
		public void ParallelSequence_starts_at_first_node_each_tick() {
			var successCounter = new SuccessCounter();
			var runningCounter = new RunningCounter();
			var sequence = new ParallelSequence(successCounter, runningCounter);
			var blackboard = new Blackboard();
			sequence.Tick(blackboard);
			successCounter.Count.ShouldEqual(1);
			runningCounter.Count.ShouldEqual(1);
			sequence.Tick(blackboard);
			successCounter.Count.ShouldEqual(2);
			runningCounter.Count.ShouldEqual(2);
		}

		[Test]
		public void ParallelSequence_starts_at_first_node_on_next_tick_after_successfully_running_all_nodes() {
			var successCounter = new SuccessCounter();
			var successCounter2 = new SuccessCounter();
			var sequence = new ParallelSequence(successCounter, successCounter2);
			var blackboard = new Blackboard();
			sequence.Tick(blackboard);
			successCounter.Count.ShouldEqual(1);
			successCounter2.Count.ShouldEqual(1);
			sequence.Tick(blackboard);
			successCounter.Count.ShouldEqual(2);
			successCounter2.Count.ShouldEqual(2);
		}
	}
}