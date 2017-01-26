using System;
using System.Collections.Generic;

namespace SwitchDemo
{
    class CommonWarrantyRules : IWarrantyMapFactory
    {
        public IReadOnlyDictionary<DeviceStatus, Action<Action>> Create(
            Action<Action> claimMoneyBack,
            Action<Action> claimNotOperational,
            Action<Action> claimCircuitry) =>
            new Dictionary<DeviceStatus, Action<Action>>()
            {
                [DeviceStatus.AllFine()] =
                    claimMoneyBack,
                [DeviceStatus.AllFine().NotOperational()] =
                    claimNotOperational,
                [DeviceStatus.AllFine().WithVisibleDamage()] =
                    (action) => { },
                [DeviceStatus.AllFine().NotOperational().WithVisibleDamage()] =
                    claimNotOperational,
                [DeviceStatus.AllFine().CircuitryFailed()] =
                    claimCircuitry,
                [DeviceStatus.AllFine().NotOperational().CircuitryFailed()] =
                    claimNotOperational,
                [DeviceStatus.AllFine().CircuitryFailed().WithVisibleDamage()] =
                    claimCircuitry,
                [DeviceStatus.AllFine().NotOperational().WithVisibleDamage().CircuitryFailed()] =
                    claimNotOperational
            };

    }
}
