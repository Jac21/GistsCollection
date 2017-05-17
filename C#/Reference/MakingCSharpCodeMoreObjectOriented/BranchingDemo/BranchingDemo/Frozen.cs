using System;

namespace BranchingDemo
{
    class Frozen : IAccountState
    {
        private Action OnUnfreeze { get; set; }

        public Frozen(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;
        }

        public IAccountState Deposit(Action addToBalance)
        {
            this.OnUnfreeze();
            addToBalance();
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            this.OnUnfreeze();
            subtractFromBalance();
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Freeze()
        {
            return this;
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
