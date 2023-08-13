using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerLocomotionBaseState
{
    public abstract void StartState(PlayerLocomotionController _controller);
    public abstract void UpdateState(PlayerLocomotionController _controller);
    public abstract void FixedState(PlayerLocomotionController _controller);
    public abstract void ExitState(PlayerLocomotionController _controller, PlayerLocomotionBaseState _newState);
}
