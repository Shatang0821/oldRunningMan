using UnityEngine;

public class Player : Character
{
    [SerializeField] PlayerGroundDetector groundDetector;
    [SerializeField] PlayerGroundDetector wallDetector;

    [Header("==== PARTICLE ====")]
    public ParticleSystem movementParticle;
    public ParticleSystem fallParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem touchParticle;

    [Header("==== SFX ====")]
    public AudioData jumpSFX;

    [Header("==========")]
    [Range(0, 10)] 
    public int occurAfterVelocity;
    [Range(0, 0.2f)]
    public float dustFormationPeriod;

    PlayerInput input;

    public Animator animator;

    new Rigidbody2D rigidbody;

    public bool IsWall => wallDetector.isOn;

    public bool IsGrounded => groundDetector.isOn;

    public bool IsFalling => rigidbody.velocity.y < 0f && !IsGrounded;

    public bool canClimbJump = false;

    public bool canJump = true;

    /// <summary>
    /// trueは右
    /// falseは左
    /// </summary>
    public bool TouchWallDirection => transform.localScale.x > 0f;

    public float MoveSpeedX => Mathf.Abs(rigidbody.velocity.x);
    public float MoveSpeedY => Mathf.Abs(rigidbody.velocity.y);

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnGUI()
    {
        Rect rect = new Rect(200, 150, 200, 200);
        string message = "canClimbJump:" + canClimbJump;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.fontStyle = FontStyle.Bold;
        GUI.Label(rect, message, style);
    }

    void Start()
    {
        input.EnableGamePlayInput();
    }

    public void Move(float speed)
    {
        if(input.Move)
        {
            transform.localScale = new Vector2(input.AxisX, 1f);
        }
        SetVelocityX(speed * input.AxisX);
    }

    public void Move(float speed,float AxisX)
    {
        if (input.Move)
        {
            transform.localScale = new Vector2(AxisX, 1f);
        }
        SetVelocityX(speed * AxisX);
    }

    /// <summary>
    /// XY速度設定
    /// </summary>
    /// <param name="velocity">xy含む速度</param>
    public void SetVelocity(Vector2 velocity)
    {
        rigidbody.velocity = velocity;
    }

    /// <summary>
    /// X速度設定
    /// </summary>
    /// <param name="velocityX">X速度</param>
    public void SetVelocityX(float velocityX)
    {
        rigidbody.velocity = new Vector2(velocityX, rigidbody.velocity.y);
    }

    /// <summary>
    /// Y速度設定
    /// </summary>
    /// <param name="velocityY">Y速度</param>
    public void SetVelocityY(float velocityY)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, velocityY);
    }

    public void SetUseGravity(float value)
    {
        rigidbody.gravityScale = value;
    }

    public override void Die()
    {
        GameManager.onGameOver?.Invoke();
        GameManager.GameState = GameState.GameOver;
        base.Die();
    }
}

