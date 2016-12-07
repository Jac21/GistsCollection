using System;

namespace BranchingDemo
{
    class Active : IAccountState
    {
        private Action OnUnfreeze { get; set; }

        public Active(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;
        }

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            subtractFromBalance();
            return this;
        }

        public IAccountState Freeze()
        {
            return new Frozen(this.OnUnfreeze);
        }

        public IAccountState HolderVerified()
        {
            return this;
        }

        public IAccountState Close()
        {
            return new Closed();
        }
    }
}
