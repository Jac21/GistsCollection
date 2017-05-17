using System;

namespace BranchingDemo
{
    class Closed : IAccountState
    {
        public IAccountState Deposit(Action addToBalance)
        {
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
            return this;
        }

        public IAccountState Close()
        {
            return this;
        }
    }
}
