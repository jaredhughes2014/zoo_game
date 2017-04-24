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
            // Player Data
            Use<AccountState, AccountController>();

            // Non-standard UI
            Use<MainMenuState, MainMenuController>();
            Use<LogInState, LogInController>();

            // UI
            Use<UIState, UIController>();
            Use<HUDState, HUDController>();
            Use<ZooCreationState, ZooCreationController>();
        }
    }
}