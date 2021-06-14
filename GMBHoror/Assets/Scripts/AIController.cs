using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AIController : MonoBehaviour
{

    [SerializeField]
    private float _randomPointRadius = 5.0f;

    [SerializeField]
    private float freezeSeconds = 2.0f;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private Animator _flashLightAnimator;
    private Rigidbody2D _rb;

    public GameObject _player;

    [SerializeField]
    private WayPoint[] _pathsToGo;
    private int index = 0;

    //AI Variables
    public IAstarAI _ai;
    [SerializeField]
    public Animator _aiFSM;
    public AIStates _aiState = AIStates.idle;

    public float maxWalkSpeed = 0.3f;
    public float maxChaseSpeed = 1.1f;

    public Vector3 position
    {
        get
        {
            return transform.position;
        }
    }


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ai = GetComponent<IAstarAI>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _ai.maxSpeed = maxWalkSpeed;
    }
    void Start()
    {
        //Set Destination Variables
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationValues();
    }

    private void UpdateAnimationValues()
    {
        _anim.SetFloat("MoveX", _ai.velocity.x);
        _anim.SetFloat("MoveY", _ai.velocity.y);
        _flashLightAnimator.SetFloat("MoveX", _ai.velocity.x);
        _flashLightAnimator.SetFloat("MoveY", _ai.velocity.y);

        if (_aiState == AIStates.chase || _aiState == AIStates.freeze)
        {
            _anim.SetBool("isAttacking", true);
        }
        else
        {
            _anim.SetBool("isAttacking", false);
        }

    }

    public void SetPatrolDestination(Vector3 pos)
    {
        _ai.destination = pos;
    }



    public Vector3 GetNextPatrolPoint()
    {
        var pos = _pathsToGo[index].position;
        index++;
        if (index > _pathsToGo.Length - 1)
            index = 0;
        return pos;
    }

    public enum AIStates
    {
        idle,
        patrol,
        chase,
        freeze
    }

    public void FreezeTimer()
    {
        StartCoroutine(Freeze());
    }

    private IEnumerator Freeze()
    {
        yield return new WaitForSeconds(freezeSeconds);
        Debug.Log("OUT OF FREEZE");
    }










    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _randomPointRadius);
        var startPos = transform.position;
        foreach (var item in _pathsToGo)
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(startPos, item.position);
            startPos = item.position;
        }
    }


}
