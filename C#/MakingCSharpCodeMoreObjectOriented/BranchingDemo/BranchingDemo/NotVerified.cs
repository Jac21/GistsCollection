using System;

namespace BranchingDemo
{
    class NotVerified : IAccountState
    {
        private Action OnUnfreeze { get; set; }

        public NotVerified(Action onUnfreeze)
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
            return this;
        }

        public IAccountState Freeze()
        {
            return this;
        }

        public IAccountState HolderVerified()
        {
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Close()
        {
            return new Closed();
        }
    }
}
