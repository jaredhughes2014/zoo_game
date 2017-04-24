﻿using Jareel;

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

            // Main Menu
            Use<MainMenuState, MainMenuController>();

            // UI
            Use<UIState, UIController>();
            Use<HUDState, HUDController>();
        }
    }
}