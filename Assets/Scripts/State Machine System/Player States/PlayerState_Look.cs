using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Look", fileName = "PlayerState_Look")]
public class PlayerState_Look : PlayerState
{
    //[SerializeField] float moveSpeed = 5f;
    [SerializeField] AnimationCurve speedCurve;
    public override void Enter()
    {
        //Debug.Log("ENTER INWALL");
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        if (input.Jump)
        {
            //stateMachine.SwitchState(typeof(PlayerState_WallJump));
            //ClimbJump
        }

        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Climb));
        }
        if (input.StopClimb || !player.IsWall && !player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_WallJump));
        }
        
    }

    public override void PhysicUpdate()
    {
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));
    }
}
