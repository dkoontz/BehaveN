using System;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class SelectorTests {

		[Test]
		public void Selector_returns_failure_with_zero_children() {
			var selector = new Selector();
			Assert.AreEqual(TaskResult.Failure, selector.Tick(new Context()));
		}

		[Test]
		public void Selector_returns_success_if_first_non_failing_child_returns_success() {
			var selector = new Selector(new Success());
			Assert.AreEqual(TaskResult.Success, selector.Tick(new Context()));

			selector = new Selector(new Failure(), new Success());
			Assert.AreEqual(TaskResult.Success, selector.Tick(new Context()));

			selector = new Selector(new Running(), new Success());
			Assert.AreNotEqual(TaskResult.Success, selector.Tick(new Context()));
		}

		[Test]
		public void Selector_returns_running_if_first_non_failing_child_returns_running() {
			var selector = new Selector(new Running());
			Assert.AreEqual(TaskResult.Running, selector.Tick(new Context()));

			selector = new Selector(new Failure(), new Running());
			Assert.AreEqual(TaskResult.Running, selector.Tick(new Context()));

			selector = new Selector(new Success(), new Running());
			Assert.AreNotEqual(TaskResult.Running, selector.Tick(new Context()));
		}

		[Test]
		public void Selector_runs_all_behaviors_until_success_or_running_result() {
			var counter1 = new FailureCounter();
			var counter2 = new FailureCounter();

			var selector = new Selector(counter1, new Success(), counter2);
			selector.Tick(new Context());
			Assert.AreEqual(1, counter1.Count);
			Assert.AreEqual(0, counter2.Count);

			counter1 = new FailureCounter();
			counter2 = new FailureCounter();

			selector = new Selector(counter1, new Running(), counter2);
			selector.Tick(new Context());
			Assert.AreEqual(1, counter1.Count);
			Assert.AreEqual(0, counter2.Count);
		}
	}
}