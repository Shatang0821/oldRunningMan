using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;

    [SerializeField] float moveSpeed = 5f;

    public override void Enter()
    {
        Debug.Log("ENTER FALL");
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if(player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }

        if(input.Jump && !player.IsWall)
        {
            if (player.canJump)
            {
                stateMachine.SwitchState(typeof(PlayerState_DoubleJump));
            }
            input.SetJumpInputBufferTimer();
        }

        if(player.IsWall&& input.Climb)
        {
            stateMachine.SwitchState(typeof(PlayerState_Climb));
        }

        if (player.IsWall && input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_WallJump));
        }
    }

    public override void PhysicUpdate()
    {
        player.Move(player.IsWall ? 0f : moveSpeed);
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));
    }
}
