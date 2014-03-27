using System;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class ActionTests {

		[Test]
		public void Action_returns_result_from_delegate() {
			var action = new ActionRunner( context => TaskResult.Success );
			Assert.AreEqual(TaskResult.Success, action.Tick(new Context()));

			action = new ActionRunner( context => TaskResult.Failure );
			Assert.AreEqual(TaskResult.Failure, action.Tick(new Context()));

			action = new ActionRunner( context => TaskResult.Running );
			Assert.AreEqual(TaskResult.Running, action.Tick(new Context()));
		}

		[Test]
		public void Action_invokes_delegate_when_tick_is_called() {
			bool wasCalled = false;
			var action = new ActionRunner( context => {
				wasCalled = true;
				return TaskResult.Success;
			});
			action.Tick(new Context());
			Assert.AreEqual(true, wasCalled);
		}

		[Test]
		public void Action_has_access_to_context_variables() {
			var context = new Context();
			context.Set("score", 10);

			var action = new ActionRunner(ctx => {
				ctx.Set("score", ctx.Get<int>("score") + 1);
				return TaskResult.Success;
			});
			action.Tick(context);

			Assert.AreEqual(11, context.Get<int>("score"));
		}
	}
}