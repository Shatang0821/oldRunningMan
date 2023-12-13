using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Climb", fileName = "PlayerState_Climb")]
public class PlayerState_Climb : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;
    public override void Enter()
    {
        //Debug.Log("ENTER INWALL");
        player.touchParticle.Play();
        base.Enter();
        player.canClimbJump = true;
    }

    public override void LogicUpdate()
    {

        if(player.IsGrounded)
        {
            player.canClimbJump = false;
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if(input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_WallJump));
        }
        if(input.StopClimb && !player.IsGrounded || !player.IsWall&&!player.IsGrounded)
        {
            player.canClimbJump = false;
            stateMachine.SwitchState(typeof (PlayerState_Fall));
        }

        if (input.Move && ((player.TouchWallDirection && input.AxisX < 0) || 
           (!player.TouchWallDirection && input.AxisX > 0)))
        {
            player.canClimbJump = false;
            stateMachine.SwitchState(typeof(PlayerState_Look));
        }
    }

    public override void PhysicUpdate()
    {
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));
    }
}
