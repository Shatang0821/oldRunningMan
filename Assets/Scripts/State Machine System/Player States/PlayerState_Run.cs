using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    float counter;//Particle‚Ì—¬‚µ•û‚ðƒJƒEƒ“ƒg

    [Header("==== RUN ====")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float acceleration = 5f;
    

    public override void Enter()
    {
        //Debug.Log("ENTER RUN");
        base.Enter();

        currentSpeed = player.MoveSpeedX;
    }

    public override void LogicUpdate()
    {
        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if(!player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_CoyoteTime));
        }

        
    }

    public override void PhysicUpdate()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.fixedDeltaTime);
        player.Move(currentSpeed);
        counter += Time.deltaTime;
        if (player.IsGrounded)
        {
            if(counter > player.dustFormationPeriod)
            {
                player.movementParticle.Play();
                counter = 0f;
            }
        }
    }
}
