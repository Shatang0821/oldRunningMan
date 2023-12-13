using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerState_Jump : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;

    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 5f;

    public override void Enter()
    {
        Debug.Log("ENTER JUMP");

        AudioManager.Instance.PlaySFX(player.jumpSFX);

        input.HasJumpInputBuffer = false;
        base.Enter();
        player.jumpParticle.Play();
        player.SetVelocityY(jumpForce);
    }

    public override void LogicUpdate()
    {
        if(input.StopJump || player.IsFalling || input.Move && player.MoveSpeedY == 0)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

        if(player.IsWall && input.Climb)
        {
            stateMachine.SwitchState(typeof(PlayerState_Climb));
        }
        if(player.IsWall && input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_WallJump));
        }

    }

    public override void PhysicUpdate()
    {
        player.Move(player.IsWall ? 0f : moveSpeed);
    }
}
