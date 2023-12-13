using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WallJump", fileName = "PlayerState_WallJump")]
public class PlayerState_WallJump : PlayerState
{
    [SerializeField] float jumpYForce = 7f;
    [SerializeField] float jumpXForce = 5f;//x•ûŒü
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float xMoveTime = 0.5f;

    [SerializeField] private float initialJumpDirection;
    public override void Enter()
    {
        Debug.Log("ENTER WALLJUMP");
        
        AudioManager.Instance.PlaySFX(player.jumpSFX);

        player.jumpParticle.Play();
        base.Enter();

        initialJumpDirection = player.TouchWallDirection ? -1 : 1;

        player.SetVelocityY(jumpYForce);
    }

    public override void LogicUpdate()
    {

        if (input.StopJump ||player.IsFalling)
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

        if(StateDuration < xMoveTime && !player.canClimbJump)
        {
            player.SetVelocityX(jumpXForce * initialJumpDirection);
        }
    }

    public override void Exit()
    {
        player.canClimbJump = false;
    }
}
