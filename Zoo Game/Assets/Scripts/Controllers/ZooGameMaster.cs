using Jareel.Unity;
using UnityEngine;

namespace Zoo
{
    /// <summary>
    /// Unity provider of the ZooMaster controller
    /// </summary>
    public class ZooGameMaster : MonoMasterController<ZooMaster>
    {
        private void OnDestroy()
        {
            Debug.Log(Master.ExportDebugState());
        }
    }
}