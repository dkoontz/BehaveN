using System;
using NUnit.Framework;
using BehaveN;

namespace Tests {
	[TestFixture]
	public class ActionTests {

		[Test]
		public void Action_returns_result_from_delegate() {
			var action = new ActionRunner( Blackboard => TaskResult.Success );
			Assert.AreEqual(TaskResult.Success, action.Tick(new Blackboard()));

			action = new ActionRunner( Blackboard => TaskResult.Failure );
			Assert.AreEqual(TaskResult.Failure, action.Tick(new Blackboard()));

			action = new ActionRunner( Blackboard => TaskResult.Running );
			Assert.AreEqual(TaskResult.Running, action.Tick(new Blackboard()));
		}

		[Test]
		public void Action_invokes_delegate_when_tick_is_called() {
			bool wasCalled = false;
			var action = new ActionRunner( Blackboard => {
				wasCalled = true;
				return TaskResult.Success;
			});
			action.Tick(new Blackboard());
			Assert.AreEqual(true, wasCalled);
		}

		[Test]
		public void Action_has_access_to_Blackboard_variables() {
			var Blackboard = new Blackboard();
			Blackboard.Set("score", 10);

			var action = new ActionRunner(ctx => {
				ctx.Set("score", ctx.Get<int>("score") + 1);
				return TaskResult.Success;
			});
			action.Tick(Blackboard);

			Assert.AreEqual(11, Blackboard.Get<int>("score"));
		}
	}
}