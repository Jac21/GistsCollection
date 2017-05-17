using System;

namespace BranchingDemo
{
    /// <summary>
    /// State Pattern! Class doesn't have to represent state anymore,
    /// or manage state transition logic!
    /// </summary>
    interface IAccountState
    {
        IAccountState Deposit(Action addToBalance);
        IAccountState Withdraw(Action subtractFromBalance);
        IAccountState Freeze();
        IAccountState HolderVerified();
        IAccountState Close();
    }
}
