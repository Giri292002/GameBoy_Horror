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
    private Animator _anim;

    [SerializeField]
    private Animator _flashLightAnimator;
    private Rigidbody2D _rb;
    private GameObject _mainChar;

    [SerializeField]
    private WayPoint[] PathsToGo;
    private int index = 0;

    //AI Variables
    private IAstarAI _ai;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ai = GetComponent<IAstarAI>();
        _mainChar = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        //Set Destination Variables
        StartCoroutine(SetWanderDestination());
        _ai.isStopped = true;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationValues();
        SetWanderDestination();
    }

    private void UpdateAnimationValues()
    {
        _anim.SetFloat("MoveX", _ai.velocity.x);
        _anim.SetFloat("MoveY", _ai.velocity.y);
        _flashLightAnimator.SetFloat("MoveX", _ai.velocity.x);
        _flashLightAnimator.SetFloat("MoveY", _ai.velocity.y);


    }

    private IEnumerator SetWanderDestination()
    {
        yield return new WaitForSeconds(1.0f);

        var pos = PathsToGo[index].position;

        _ai.destination = pos;

    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _randomPointRadius);
    }
}
