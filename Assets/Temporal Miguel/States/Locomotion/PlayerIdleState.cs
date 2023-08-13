using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerLocomotionBaseState
{
    public override void StartState(PlayerLocomotionController _controller)
    {
        _controller.NewSpeed = 0;
    }

    public override void UpdateState(PlayerLocomotionController _controller)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _controller.Jump();

        if (_controller.HorizontalInputValue != 0 || _controller.VerticalInputValue != 0)
            if (Input.GetKeyDown(KeyCode.LeftShift))
                _controller.ChangeState(_controller.RunState);
            else
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
