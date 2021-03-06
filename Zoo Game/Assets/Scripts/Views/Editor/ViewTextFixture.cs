﻿using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

namespace Zoo.UI.Test
{
    /// <summary>
    /// Used to test a view component. This base class sets up all data necessary for test fixtures
    /// to operate
    /// </summary>
    /// <typeparam name="T">The type of view to test</typeparam>
    public class ViewTestFixture<T> where T : MonoBehaviour
    {
        /// <summary>
        /// The parent object of the view
        /// </summary>
        protected GameObject ViewObject { get; private set; }

        /// <summary>
        /// Stores all spawned objects so they can be destroyed afterwards
        /// </summary>
        private List<GameObject> m_spawnedObjects = new List<GameObject>();

        /// <summary>
        /// The view class
        /// </summary>
        protected T View { get { return ViewObject.GetComponent<T>(); } }

        #region Setup

        /// <summary>
        /// Initializes the test fixture
        /// </summary>
        [SetUp]
        public virtual void InitializeView()
        {
            ViewObject = MakeBehaviour<T>().gameObject;
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

        #endregion
    }
}