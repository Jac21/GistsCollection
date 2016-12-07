using System;

namespace BranchingDemo
{
    /// <summary>
    /// Requirements: Money can be deposited at any time,
    /// money can be withdrawn only after verification.
    /// Holder can close account at any time, cannot perfrom
    /// actions after that.
    /// Demonstrates Single-Responsibility Principle
    /// </summary>
    class Account
    {
        public decimal Balance { get; private set; }

        private IAccountState State { get; set; }

        public Account(Action onUnfreeze)
        {
            this.State = new NotVerified(onUnfreeze);
        }

        /// <summary>
        /// #1: Interaction - Deposit was invoked on the State
        /// #2: Behavior - Result of State.Deposit is new State
        /// </summary>
        /// <param name="amount"></param>
        public void Deposit(decimal amount)
        {
            this.State = this.State.Deposit(() => { this.Balance += amount; });
        }

        /// <summary>
        /// Same tests as Deposit()
        /// </summary>
        /// <param name="amount"></param>
        public void Withdraw(decimal amount)
        {
            this.State = this.State.Withdraw(() => { this.Balance -= amount; });
            
        }

        /// <summary>
        /// Depositing or Withdrawing unfreezes account,
        /// needs callback function on unfreeze
        /// </summary>
        public void Freeze()
        {
            this.State = this.State.Freeze();
        }

        public void HolderVerified()
        {
            this.State = this.State.HolderVerified();
        }

        public void Close()
        {
            this.State = this.State.Close();
        }
    }
}