using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float coyoteTime = 0.1f;
    public override void Enter()
    {
        Debug.Log("CoyoteTime");
        base.Enter();

        player.SetUseGravity(0);
    }

    public override void Exit()
    {
        player.SetUseGravity(1);
    }

    public override void LogicUpdate()
    {
        if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (StateDuration> coyoteTime || !input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        player.Move(runSpeed);
    }

}
