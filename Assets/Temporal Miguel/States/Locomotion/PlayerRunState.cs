using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerLocomotionBaseState
{
    public override void StartState(PlayerLocomotionController _controller)
    {
        _controller.NewSpeed = _controller.RunSpeed;
    }
    public override void UpdateState(PlayerLocomotionController _controller)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _controller.Jump();

        if (Input.GetKeyUp(KeyCode.LeftShift))
            _controller.ChangeState(_controller.WalkState);
    }
    public override void FixedState(PlayerLocomotionController _controller)
    {
        _controller.Movement();
    }
    public override void ExitState(PlayerLocomotionController _controller)
    {

    }
}
