
using Jareel;

namespace Zoo
{
    /// <summary>
    /// Events that can be executed to mutate the account state
    /// </summary>
    public enum AccountEvents
    {
        SetAccountData,
    }

    /// <summary>
    /// Contains the state of the player's account. Since this is a client-side container
    /// for data maintained server-side, this state is not persisted
    /// </summary>
    [StateContainer("account")]
    public class AccountState : State
    {
        #region State Data

        /// <summary>
        /// The player's name
        /// </summary>
        [StateData] public string Alias { get; set; }

        /// <summary>
        /// The player's email address
        /// </summary>
        [StateData] public string Email { get; set; }

        #endregion

        /// <summary>
        /// Sets default values
        /// </summary>
        public AccountState()
        {
            Alias = "";
            Email = "";
        }
    }

    /// <summary>
    /// Controls mutation and cloning of the Account state
    /// </summary>
    public class AccountController : StateController<AccountState>
    {
        /// <summary>
        /// Sets the user's account data
        /// </summary>
        /// <param name="alias">The player's alias</param>
        /// <param name="email">The player's email address</param>
        [EventListener(AccountEvents.SetAccountData)]
        private void SetAccountData(string alias, string email)
        {
            State.Alias = alias;
            State.Email = email;
        }

        /// <summary>
        /// Creates a deep copy of the Account state
        /// </summary>
        /// <returns>Deep copy of the account state</returns>
        public override AccountState CloneState()
        {
            return new AccountState()
            {
                Alias = State.Alias,
                Email = State.Email,
            };
        }
    }

}