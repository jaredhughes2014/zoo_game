using Jareel;
using Jareel.Unity;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoo.Controllers.Test
{
    /// <summary>
    /// Provides and sets up all data needed to executes state tests on a controller. In this game, controllers
    /// are implemented as StateSubscribers.
    /// </summary>
    public class ControllerTestFixture<T> where T : MonoStateSubscriber
    {
        #region Properties

        /// <summary>
        /// Stores all spawned objects so they can be destroyed afterwards
        /// </summary>
        private List<GameObject> m_spawnedObjects = new List<GameObject>();

        /// <summary>
        /// The controller object
        /// </summary>
        protected T Controller { get; set; }

        /// <summary>
        /// The master controller used by this project
        /// </summary>
        protected ZooGameMaster MonoMaster { get; set; }

        #endregion

        #region Setup

        /// <summary>
        /// Initializes the test fixture. This does not connect your controller to your views, but it
        /// does connect your controller to your master controller
        /// </summary>
        [SetUp]
        public virtual void InitializeController()
        {
            Controller = MakeBehaviour<T>();
            MonoMaster = MakeBehaviour<ZooGameMaster>();
            MonoMaster.Master = new ZooMaster();

            Controller.MountToMaster(MonoMaster);
        }

        /// <summary>
        /// Tears down the test fixture. This is necessary to prevent stray objects from staying in the scene
        /// </summary>
        [TearDown]
        public virtual void CleanupView()
        {
            foreach (var obj in m_spawnedObjects) {
                Object.DestroyImmediate(obj);
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// Convenience function to manually create a monobehaviour
        /// </summary>
        /// <typeparam name="B">The type of monobehaviour to create</typeparam>
        /// <returns>Working instance of B</returns>
        protected B MakeBehaviour<B>() where B : MonoBehaviour
        {
            var obj = new GameObject(string.Format("Simulated {0} Instance", typeof(B).Name));
            m_spawnedObjects.Add(obj);

            obj.AddComponent<B>();
            return obj.GetComponent<B>();
        }

        /// <summary>
        /// Executes the provided event with the provided arguments with the master controller
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments to provide with the event</param>
        protected void Execute(object ev, params object[] args)
        {
            MonoMaster.Master.Events.Execute(ev, args);
        }

        /// <summary>
        /// Executes the provided event in strict mode with the provided arguments with the master controller
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments to provide with the event</param>
        protected void ExecuteStrict(object ev, params object[] args)
        {
            MonoMaster.Master.Events.ExecuteStrict(ev, args);
        }

        /// <summary>
        /// Shortcut to execute a single event and move to the next frame. DO NOT use this if more than one
        /// event should be executed within a frame.
        /// 
        /// To use this function, simply yield it in a Unity test
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments provided with the event</param>
        /// <returns>Always returns null so this command can be yielded</returns>
        protected object ExecuteAndUpdate(object ev, params object[] args)
        {
            MonoMaster.Master.Events.Execute(ev, args);
            return null;
        }

        /// <summary>
        /// Shortcut to execute a single strict event and move to the next frame. DO NOT use this if more than one
        /// event should be executed within a frame.
        /// 
        /// To use this function, simply yield it in a Unity test
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments provided with the event</param>
        /// <returns>Always yields null so this command can be yielded</returns>
        protected object ExecuteStrictAndUpdate(object ev, params object[] args)
        {
            MonoMaster.Master.Events.ExecuteStrict(ev, args);
            return null;
        }

        /// <summary>
        /// Returns a distinct copy of the state of type S. Will throw an exception if
        /// the ZooMaster is not using the S type state
        /// </summary>
        /// <typeparam name="S">The type of state to get a copy of</typeparam>
        /// <returns>Copy of the S state</returns>
        protected S GetState<S>() where S : State, new()
        {
            return MonoMaster.Master.SubscribeToStates<S>().State1;
        }

        #endregion
    }
}