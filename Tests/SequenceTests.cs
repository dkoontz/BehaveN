using System;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class SequenceTests {
		[Test]
		public void Sequence_returns_success_with_zero_children() {
			var sequence = new Sequence();
			sequence.Tick(new Context()).ShouldEqual(TaskResult.Success);
		}

		[Test]
		public void Sequence_returns_after_first_failure() {
			var counter = new SuccessCounter();
			var sequence = new Sequence(new Failure());
			sequence.Tick(new Context());
			counter.Count.ShouldEqual(0);
		}

		[Test]
		public void Sequence_runs_all_behaviors_until_a_non_success_result() {
			var counter1 = new SuccessCounter();
			var counter2 = new SuccessCounter();
			var counter3 = new SuccessCounter();
			var sequence = new Sequence(counter1, counter2, new Failure(), counter3);
			sequence.Tick(new Context());
			counter1.Count.ShouldEqual(1);
			counter2.Count.ShouldEqual(1);
			counter3.Count.ShouldEqual(0);
		}

		[Test]
		public void Sequence_returns_success_when_all_behaviors_are_successful() {
			var sequence = new Sequence(new Success(), new Success(), new Success());
			sequence.Tick(new Context()).ShouldEqual(TaskResult.Success);
		}

		[Test]
		public void Sequence_passes_along_result_from_behavior_if_not_successful() {
			var sequence = new Sequence(new Success(), new Failure(), new Success());
			sequence.Tick(new Context()).ShouldEqual(TaskResult.Failure);

			sequence = new Sequence(new Success(), new Running(), new Success());
			sequence.Tick(new Context()).ShouldEqual(TaskResult.Running);
		}

		[Test]
		public void Sequence_resumes_at_running_node() {
			var successCounter = new SuccessCounter();
			var runningCounter = new RunningCounter();
			var sequence = new Sequence(successCounter, runningCounter);
			var context = new Context();
			sequence.Tick(context);
			successCounter.Count.ShouldEqual(1);
			runningCounter.Count.ShouldEqual(1);
			sequence.Tick(context);
			successCounter.Count.ShouldEqual(1);
			runningCounter.Count.ShouldEqual(2);
		}
	}
}