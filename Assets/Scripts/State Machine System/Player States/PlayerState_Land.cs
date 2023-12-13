using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]
public class PlayerState_Land : PlayerState
{
    [SerializeField] float stiffTime = 0.2f;

    public override void Enter()
    {
        //Debug.Log("ENTER LAND");
        player.canJump = true;
        player.fallParticle.Play();

    }

    public override void LogicUpdate()
    {
        if(input.HasJumpInputBuffer || input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        else if (input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
        else
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (StateDuration < stiffTime)
        {
            return;
        }
        //if (!input.HasJumpInputBuffer&&player.IsGrounded)
        //{
        //    stateMachine.SwitchState(typeof(PlayerState_Idle));
        //}
    }
}
