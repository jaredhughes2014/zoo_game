using Jareel;

namespace Zoo
{
    /// <summary>
    /// Connects all controllers used by this application
    /// </summary>
    public class ZooMaster : MasterController
    {
        protected override void UseControllers()
        {
            Use<AccountState, AccountController>();
            Use<MainMenuState, MainMenuController>();
            Use<UIState, UIController>();
        }
    }
}