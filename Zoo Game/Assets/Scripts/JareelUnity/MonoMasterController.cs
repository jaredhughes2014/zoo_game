using UnityEngine;

namespace Jareel.Unity
{
	/// <summary>
	/// MonoBehaviour-accessible source for a master controller. This manages the storage and connection
	/// of the master controller as well as executing updates every frame
	/// </summary>
	public abstract class MonoMasterController : MonoBehaviour
	{
		#region Properties

		/// <summary>
		/// Stores all states, the EventManager, and all state controllers for
		/// this system
		/// </summary>
		private MasterController m_master;
		internal MasterController AbstractMaster { get { return m_master; } }

		/// <summary>
		/// Manages execution of updates to the master controller
		/// </summary>
		protected SequentialExecutor m_executor;

		#endregion

		#region Setup

		/// <summary>
		/// Initializes the master controller
		/// </summary>
		protected virtual void Awake()
		{
			m_master = GenerateMasterController();
			m_executor = new SequentialExecutor(m_master);
		}

		/// <summary>
		/// Override to generate the master controller managed by this behavior
		/// </summary>
		/// <returns></returns>
		protected abstract MasterController GenerateMasterController();

		/// <summary>
		/// Checks for and applies updates to the master controller
		/// </summary>
		protected virtual void Update()
		{
			m_executor.Execute();
		}

		#endregion
	}

	/// <summary>
	/// Type-specific mono master controller. This behavior will automatically generate
	/// your master controller
	/// </summary>
	/// <typeparam name="T">The type of master controller to manage</typeparam>
	public abstract class MonoMasterController<T> : MonoMasterController where T : MasterController, new()
	{
		/// <summary>
		/// The master controller managed by this mono master controller
		/// </summary>
		public T Master { get { return (T)AbstractMaster; } }

		/// <summary>
		/// Returns a newly generated copy of a master controller of type T
		/// </summary>
		/// <returns>New boxed instance of type T</returns>
		protected sealed override MasterController GenerateMasterController()
		{
			return new T();
		}
	}
}