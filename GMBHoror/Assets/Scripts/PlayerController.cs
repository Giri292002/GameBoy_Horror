using UnityEngine;

// Ensure the component is present on the gameobject the script is attached to
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator _flashLightAnimator;

    // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    new Rigidbody2D rigidbody2D;
    private Animator _animator;

    float HorInput;
    float VertInput;

    public float movementSpeed = 1000.0f;

    public bool canMove = true;

    public bool hasKey
    {
        get
        {
            return _hasKey;
        }
        set
        {
            if (value == true)
                GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>()._currentExitDoor.ActivateExitDoor();
            _hasKey = value;
        }
    }

    private bool _hasKey;

    void Awake()
    {
        // Setup Rigidbody for frictionless top down movement and dynamic collision
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        rigidbody2D.isKinematic = false;
        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    void Update()
    {
        UpdateInputValues();
        UpdateAnimations();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Move();
    }

    void UpdateInputValues()
    {
        if (canMove)
        {
            HorInput = Input.GetAxis("Horizontal");
            VertInput = Input.GetAxis("Vertical");
        }
        else
        {
            HorInput = 0;
            VertInput = 0;
        }
    }

    void Move()
    {
        // Set rigidbody velocity
        Vector2 targetVelocity = new Vector2(HorInput, VertInput);
        rigidbody2D.velocity = (targetVelocity * movementSpeed) * Time.deltaTime; // Multiply the target by deltaTime to make movement speed consistent across different framerates
    }

    void UpdateAnimations()
    {
        _animator.SetFloat("MoveX", HorInput);
        _animator.SetFloat("MoveY", VertInput);
        _flashLightAnimator.SetFloat("MoveX", HorInput);
        _flashLightAnimator.SetFloat("MoveY", VertInput);
    }


}