using UnityEngine;

public class PlayerState : ScriptableObject,IState
{
    [SerializeField] string stateName;

    [SerializeField,Range(0f,1f)] protected float transitionDuration = 0.1f;

    float stateStartTime;

    protected int stateHash;
    
    protected float currentSpeed;

    protected Animator animator;

    protected Player player;

    protected PlayerInput input;
    protected PlayerStateMachine stateMachine;

    //protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;

    protected float StateDuration => Time.time - stateStartTime;

    void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="animator">�A�j���[�^</param>
    /// <param name="player">�v���C���[�X�N���v�g</param>
    /// <param name="input">�C���v�b�g�X�N���v�g</param>
    /// <param name="stateMachine">�v���C���[�X�e�[�^�X�}�V��</param>
    public void Initialize(Animator animator,Player player,PlayerInput input, PlayerStateMachine stateMachine)
    {
        this.animator = animator;
        this.player = player;
        this.input = input;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        animator.CrossFade(stateHash, transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit()
    {
       
    }

    public virtual void LogicUpdate()
    {
       
    }

    public virtual void PhysicUpdate()
    {
        
    }

}
