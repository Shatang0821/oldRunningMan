using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/DoubleJump", fileName = "PlayerState_DoubleJump")]
public class PlayerState_DoubleJump : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;

    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 5f;
    public override void Enter()
    {
        Debug.Log("ENTER DOUBLEJUMP");

        AudioManager.Instance.PlaySFX(player.jumpSFX);

        player.canJump = false;
        base.Enter();
        player.jumpParticle.Play();
        player.SetVelocityY(jumpForce);
    }

    public override void LogicUpdate()
    {
        if (input.StopJump || player.IsFalling || input.Move && player.MoveSpeedY == 0)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

        if (player.IsWall && input.Climb)
        {
            stateMachine.SwitchState(typeof(PlayerState_Climb));
        }

    }


    public override void PhysicUpdate()
    {
        player.Move(player.IsWall ? 0f : moveSpeed);
    }
}
