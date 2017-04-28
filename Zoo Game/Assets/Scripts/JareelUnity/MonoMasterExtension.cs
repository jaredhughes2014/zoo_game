using UnityEngine;

namespace Jareel.Unity
{
    /// <summary>
    /// Base class of mono master extensions. This class provides a type-agnostic abstraction
    /// of the master controller system
    /// </summary>
    public class MonoMasterExtension : MonoBehaviour
    {
        /// <summary>
        /// Reference to the actual master controller
        /// </summary>
        [SerializeField]
        private MonoMasterController m_monoMaster;

        /// <summary>
        /// On-use reference to the master controller. If the extended mono master has not yet been loaded,
        /// this will find the master before continuing
        /// </summary>
        public MasterController AbstractMaster
        {
            get
            {
                if (m_monoMaster == null) {
                    m_monoMaster = FindObjectOfType<MonoMasterController>();
                }

                return (m_monoMaster == null) ? null : m_monoMaster.AbstractMaster;
            }
        }
    }

    /// <summary>
    /// Extension of a MasterController. MonoStateSubscribers need access to a single, unified state. Therefore, 
    /// StateSubscribers should be children of master extensions, which should find the universal master controller object
    /// </summary>
    /// <typeparam name="T">The type of master controller this extension provides access to</typeparam>
    public class MonoMasterExtension<T> : MonoMasterExtension where T : MasterController, new()
    {
        #region Fields

        /// <summary>
        /// The master controller this extension provides access to
        /// </summary>
        public T Master { get { return AbstractMaster == null ? null : (T)AbstractMaster; } }


        #endregion
    }
}