using UnityEngine;
using Jareel.Unity;
using Zoo.UI;
using System;

namespace Zoo.Controllers
{
    /// <summary>
    /// Updates zoo creation views based on the current state of the system
    /// </summary>
    public class ZooCreationUIController : MonoStateSubscriber<ZooCreationState>
    {
        #region Fields

        // Editor Fields
        [SerializeField] private ZooCreationView m_zooCreation;

        #endregion

        #region Properties
#if UNITY_EDITOR

        /// <summary>
        /// Used to manually access and set the zoo creation view. Used for testing purposes
        /// </summary>
        public ZooCreationView ZooCreation { get { return m_zooCreation; } set { m_zooCreation = value; } }

#endif
        #endregion

        /// <summary>
        /// Updates the views based on the current state of zoo creation
        /// </summary>
        /// <param name="zooCreation">The current state of zoo creation</param>
        protected override void OnStateChanged(ZooCreationState zooCreation)
        {
        }
    }
}
