using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Tests the behaviors of the LogIn state system
    /// </summary>
    [TestFixture(Description = "The LogIn state contains the state of the LogIn view")]
    public class LogInTests : StateTestFixture<LogInState>
    {
        /// <summary>
        /// Tests that the default values of the log in state are set correctly
        /// </summary>
        [Test]
        public void TestDefaultValues()
        {
            Assert.AreEqual("", State.Email);
            Assert.AreEqual("", State.Password);
            Assert.IsFalse(State.Submitted);
        }

        /// <summary>
        /// Tests the behavior of the update email text event
        /// </summary>
        [Test]
        public void TestUpdateEmailText()
        {
            var prev = State.Email;
            var expected = prev + "_Test";

            ExecuteStrictAndUpdate(LogInEvents.UpdateEmailText, expected);

            Assert.AreNotEqual(prev, State.Email);
            Assert.AreEqual(expected, State.Email);

            var copy = MakeCopy();
            Assert.AreEqual(State.Email, copy.Email);
        }

        /// <summary>
        /// Tests the behavior of the update password text event
        /// </summary>
        [Test]
        public void TestUpdatePasswordText()
        {
            var prev = State.Password;
            var expected = prev + "_Test";

            ExecuteStrictAndUpdate(LogInEvents.UpdatePasswordText, expected);

            Assert.AreNotEqual(prev, State.Password);
            Assert.AreEqual(expected, State.Password);

            var copy = MakeCopy();
            Assert.AreEqual(State.Password, copy.Password);
        }

        /// <summary>
        /// Tests the behavior of the Submit event
        /// </summary>
        [Test]
        public void TestSubmit()
        {
            ExecuteStrictAndUpdate(LogInEvents.Submit);

            Assert.IsTrue(State.Submitted);

            var copy = MakeCopy();
            Assert.AreEqual(State.Submitted, copy.Submitted);
        }

        /// <summary>
        /// Tests the behavior of the Submit event
        /// </summary>
        [Test]
        public void TestReset()
        {
            // Set all state fields first to prevent false positives from default values
            ExecuteStrictAndUpdate(LogInEvents.UpdateEmailText, "Test");
            ExecuteStrictAndUpdate(LogInEvents.UpdatePasswordText, "Test");
            ExecuteStrictAndUpdate(LogInEvents.Submit);

            ExecuteStrictAndUpdate(LogInEvents.Reset);

            Assert.IsEmpty(State.Password);
            Assert.IsEmpty(State.Email);
            Assert.IsFalse(State.Submitted);

            var copy = MakeCopy();
            Assert.AreEqual(State.Password, copy.Password);
            Assert.AreEqual(State.Email, copy.Email);
            Assert.AreEqual(State.Submitted, copy.Submitted);
        }
    }
}