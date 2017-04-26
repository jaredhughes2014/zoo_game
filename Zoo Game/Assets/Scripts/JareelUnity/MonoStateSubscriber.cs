using UnityEngine;
using System;

namespace Jareel.Unity
{
	/// <summary>
	/// Mono state subscribers connect to a master controller to provide access to state
	/// data and access to the master controller's event manager.
	/// 
	/// While mono state subscribers can fire events, they CANNOT listen for events.
	/// </summary>
	public abstract class MonoStateSubscriber : MonoBehaviour
	{
		#region Properties

		/// <summary>
		/// Allows firing of events
		/// </summary>
		public EventManager Events { get; private set; }

		/// <summary>
		/// Provides access to the states this will subscribe to
		/// </summary>
		internal StateSubscriber AbstractSubscriber { get; set; }

		/// <summary>
		/// If true, this will not receive any state updates
		/// </summary>
		protected bool Blocked { get; private set; }

		#endregion

		#region Mono Fields

		/// <summary>
		/// The master controller this subscribes to. If this field is not set, the subscriber will
		/// ascend up the heirarchy to try to set this field
		/// </summary>
		[SerializeField] private MonoMasterController m_master;

		#endregion

		#region Setup

		/// <summary>
		/// Connects this subscriber to the master controller. If the master is not set, this is
		/// where the subscriber will attempt to connect by moving up the heirarchy
		/// 
		/// If overriding, the base function must be called, or else this will break horribly
		/// </summary>
		protected virtual void Start()
		{
            if (m_master == null) {
                var transform = this.transform;
                while (transform != null) {
                    transform = transform.parent;
                    var master = transform.gameObject.GetComponent<MonoMasterController>();

                    if (master != null) {
                        MountToMaster(master);
                        return;
                    }
                }
                Debug.LogError("Unable to connect to a master controller. Aborting...", this);
                Destroy(this);
            }
            else {
                MountToMaster(m_master);
            }
        }

        /// <summary>
        /// Connects this subscriber to a mono master controller and mounts this subscriber to the master
        /// </summary>
        /// <param name="master">The mono master to connect to</param>
        public void MountToMaster(MonoMasterController master)
        {
            m_master = master;

            try {
                AbstractSubscriber = GenerateStateSubscrber(m_master.AbstractMaster);
                Events = m_master.AbstractMaster.Events;
            }
            catch (ArgumentException e) {
                Debug.LogError(e);
                Debug.LogError("Unable to subscribe. Aborting...", this);
                Destroy(this);
            }
        }

        /// <summary>
        /// Dismounts this subscriber from its master controller. Has no effect
        /// if this subscriber is not mounted
        /// </summary>
        public void DismountFromMaster()
        {
            if (m_master != null && AbstractSubscriber != null) {
                m_master.AbstractMaster.DisconnectSubscriber(AbstractSubscriber);
            }
            else {
                Debug.LogWarning("Dismount had no effect. This subscriber is not currently mounted", this);
            }
        }

		/// <summary>
		/// Overload to generate the state subscriber
		/// </summary>
		/// <param name="master">The master controller the subscriber should come from</param>
		/// <returns>Subscriber to all necessary states from the master controller</returns>
		internal abstract StateSubscriber GenerateStateSubscrber(MasterController master);

		/// <summary>
		/// Disconnects this subscriber from the master
		/// 
		/// If overriding, the base function must be called, or this will break your master controller
		/// </summary>
		protected virtual void OnDestroy()
		{
            DismountFromMaster();
		}

		#endregion

		#region Updates

		/// <summary>
		/// Checks for updates to the state. If an update exists, OnStateChanged will execute.
		/// 
		/// If overriding, the base function must be called or your subscriber will never have
		/// access to the state
		/// </summary>
		protected virtual void Update()
		{
			CheckForStateChange();
		}

		/// <summary>
		/// If not blocked, checks if the subscribed state has updated. If it has, calls
		/// OnStateChanged
		/// </summary>
		private void CheckForStateChange()
		{
			if (!Blocked && AbstractSubscriber.Updated) {
				OnStateChanged();
			}
		}

		/// <summary>
		/// Executed every time the state changes. This is the most recent copy
		/// of your state. Note that this is a copy and changing it will not change
		/// the state in the master controller
		/// </summary>
		/// <param name="state">Deep copy of the state this subscriber is subscribed to</param>
		internal abstract void OnStateChanged();

		#endregion

		#region Blocking

		/// <summary>
		/// Prevents this subscriber from receiving state updates
		/// </summary>
		protected void Block()
		{
			Blocked = true;
		}

		/// <summary>
		/// Allows this subscriber to receive state updates again. If updateNow is true
		/// and an update occurred while this was blocked, a state change will be registered immediately.
		/// 
		/// Note that this change will only be registered if a state change has occurred. This
		/// cannot be used to force a state change notification to occur again
		/// </summary>
		/// <param name="updateNow">If true, executes a state change if one has occurred</param>
		protected void Unblock(bool updateNow = true)
		{
			Blocked = false;
			if (updateNow) {
				CheckForStateChange();
			}
		}

		#endregion
	}

	/// <summary>
	/// MonoStateSubscriber which subscribes to 2 distinct types of states
	/// </summary>
	/// <typeparam name="A">The first state type</typeparam>
	/// <typeparam name="B">The second state type</typeparam>
	public abstract class MonoStateSubscriber<A> : MonoStateSubscriber
		where A : State, new()
	{
		#region Properties

		/// <summary>
		/// The first subscribed state
		/// </summary>
		protected A State { get; set; }

		/// <summary>
		/// Casts the subscriber to the actual types used by this mono state subscriber
		/// </summary>
		internal StateSubscriber<A> Subscriber { get { return (StateSubscriber<A>)AbstractSubscriber; } }

		#endregion

		/// <summary>
		/// Overriden to generate a subscriber to all subscribed states
		/// </summary>
		/// <param name="master">The master controller to spawn a subscriber from</param>
		/// <returns>Boxed state subscriber with references to all four subscribed state types</returns>
		internal override StateSubscriber GenerateStateSubscrber(MasterController master)
		{
			return master.SubscribeToStates<A>();
		}

		/// <summary>
		/// Every time one of the states change, caches references to all subscribed states and executes the
		/// user-defined state change handler
		/// </summary>
		internal override void OnStateChanged()
		{
			State = Subscriber.State1;

			OnStateChanged(State);
		}

		/// <summary>
		/// Executed every time the state changes. This passes references to the state for convenience.
		/// When this function is executed, the subscribed references to the individual states are guaranteed
		/// to have the latest version of the state in the subscription
		/// </summary>
		/// <param name="state1">The first subscribed state</param>
		protected abstract void OnStateChanged(A state1);
	}

	/// <summary>
	/// MonoStateSubscriber which subscribes to 2 distinct types of states
	/// </summary>
	/// <typeparam name="A">The first state type</typeparam>
	/// <typeparam name="B">The second state type</typeparam>
	public abstract class MonoStateSubscriber<A, B> : MonoStateSubscriber
		where A : State, new()
		where B : State, new()
	{
		#region Properties

		/// <summary>
		/// The first subscribed state
		/// </summary>
		protected A State1 { get; set; }

		/// <summary>
		/// The second subscribed state
		/// </summary>
		protected B State2 { get; set; }

		/// <summary>
		/// Casts the subscriber to the actual types used by this mono state subscriber
		/// </summary>
		internal StateSubscriber<A, B> Subscriber { get { return (StateSubscriber<A, B>)AbstractSubscriber; } }

		#endregion

		/// <summary>
		/// Overriden to generate a subscriber to all subscribed states
		/// </summary>
		/// <param name="master">The master controller to spawn a subscriber from</param>
		/// <returns>Boxed state subscriber with references to all four subscribed state types</returns>
		internal override StateSubscriber GenerateStateSubscrber(MasterController master)
		{
			return master.SubscribeToStates<A, B>();
		}

		/// <summary>
		/// Every time one of the states change, caches references to all subscribed states and executes the
		/// user-defined state change handler
		/// </summary>
		internal override void OnStateChanged()
		{
			State1 = Subscriber.State1;
			State2 = Subscriber.State2;

			OnStateChanged(State1, State2);
		}

		/// <summary>
		/// Executed every time the state changes. This passes references to the two states for convenience.
		/// When this function is executed, the subscribed references to the individual states are guaranteed
		/// to have the latest version of the state in the subscription
		/// </summary>
		/// <param name="state1">The first subscribed state</param>
		/// <param name="state2">The second subscribed state</param>
		protected abstract void OnStateChanged(A state1, B state2);
	}

	/// <summary>
	/// MonoStateSubscriber which subscribes to 3 distinct types of states
	/// </summary>
	/// <typeparam name="A">The first state type</typeparam>
	/// <typeparam name="B">The second state type</typeparam>
	/// <typeparam name="C">The third state type</typeparam>
	public abstract class MonoStateSubscriber<A, B, C> : MonoStateSubscriber
		where A : State, new()
		where B : State, new()
		where C : State, new()
	{
		#region Properties

		/// <summary>
		/// The first subscribed state
		/// </summary>
		protected A State1 { get; set; }

		/// <summary>
		/// The second subscribed state
		/// </summary>
		protected B State2 { get; set; }

		/// <summary>
		/// The third subscribed state
		/// </summary>
		protected C State3 { get; set; }

		/// <summary>
		/// Casts the subscriber to the actual types used by this mono state subscriber
		/// </summary>
		internal StateSubscriber<A, B, C> Subscriber { get { return (StateSubscriber<A, B, C>)AbstractSubscriber; } }

		#endregion

		/// <summary>
		/// Overriden to generate a subscriber to all subscribed states
		/// </summary>
		/// <param name="master">The master controller to spawn a subscriber from</param>
		/// <returns>Boxed state subscriber with references to all four subscribed state types</returns>
		internal override StateSubscriber GenerateStateSubscrber(MasterController master)
		{
			return master.SubscribeToStates<A, B, C>();
		}

		/// <summary>
		/// Every time one of the states change, caches references to all subscribed states and executes the
		/// user-defined state change handler
		/// </summary>
		internal override void OnStateChanged()
		{
			State1 = Subscriber.State1;
			State2 = Subscriber.State2;
			State3 = Subscriber.State3;

			OnStateChanged(State1, State2, State3);
		}

		/// <summary>
		/// Executed every time the state changes. This passes references to the three states for convenience.
		/// When this function is executed, the subscribed references to the individual states are guaranteed
		/// to have the latest version of the state in the subscription
		/// </summary>
		/// <param name="state1">The first subscribed state</param>
		/// <param name="state2">The second subscribed state</param>
		/// <param name="state3">The third subscribed state</param>
		protected abstract void OnStateChanged(A state1, B state2, C state3);
	}

	/// <summary>
	/// MonoStateSubscriber which subscribes to 4 distinct types of states
	/// </summary>
	/// <typeparam name="A">The first state type</typeparam>
	/// <typeparam name="B">The second state type</typeparam>
	/// <typeparam name="C">The third state type</typeparam>
	/// <typeparam name="D">The fourth state type</typeparam>
	public abstract class MonoStateSubscriber<A, B, C, D> : MonoStateSubscriber
		where A : State, new()
		where B : State, new()
		where C : State, new()
		where D : State, new()
	{
		#region Properties

		/// <summary>
		/// The first subscribed state
		/// </summary>
		protected A State1 { get; set; }

		/// <summary>
		/// The second subscribed state
		/// </summary>
		protected B State2 { get; set; }

		/// <summary>
		/// The third subscribed state
		/// </summary>
		protected C State3 { get; set; }

		/// <summary>
		/// The fourth subscribed state
		/// </summary>
		protected D State4 { get; set; }

		/// <summary>
		/// Casts the subscriber to the actual types used by this mono state subscriber
		/// </summary>
		internal StateSubscriber<A, B, C, D> Subscriber { get { return (StateSubscriber<A, B, C, D>)AbstractSubscriber; } }

		#endregion

		/// <summary>
		/// Overriden to generate a subscriber to all subscribed states
		/// </summary>
		/// <param name="master">The master controller to spawn a subscriber from</param>
		/// <returns>Boxed state subscriber with references to all four subscribed state types</returns>
		internal override StateSubscriber GenerateStateSubscrber(MasterController master)
		{
			return master.SubscribeToStates<A, B, C, D>();
		}

		/// <summary>
		/// Every time one of the states change, caches references to all subscribed states and executes the
		/// user-defined state change handler
		/// </summary>
		internal override void OnStateChanged()
		{
			State1 = Subscriber.State1;
			State2 = Subscriber.State2;
			State3 = Subscriber.State3;
			State4 = Subscriber.State4;

			OnStateChanged(State1, State2, State3, State4);
		}

		/// <summary>
		/// Executed every time the state changes. This passes references to the four states for convenience.
		/// When this function is executed, the subscribed references to the individual states are guaranteed
		/// to have the latest version of the state in the subscription
		/// </summary>
		/// <param name="state1">The first subscribed state</param>
		/// <param name="state2">The second subscribed state</param>
		/// <param name="state3">The third subscribed state</param>
		/// <param name="state4">The fourth subscribed state</param>
		protected abstract void OnStateChanged(A state1, B state2, C state3, D state4);
	}
}
