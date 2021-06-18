using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    private SpriteRenderer _sprite;

    [SerializeField]
    private Sprite[] _flashLightLevels;

    PolygonCollider2D _trigger;

    int index; //Level and also used to drive the sprite
    bool _fullTorch;
    int _torchCharge;
    int _torchChargeMax = 4;

    bool _cooldown = true;

    [SerializeField]
    private float _coolDownTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _trigger = GetComponent<PolygonCollider2D>();
        _trigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();

    }


    private void ReadInputs()
    {
        if (Input.GetButtonDown("Fire1") && _fullTorch == false)
        {
            _cooldown = false;
            _torchCharge++;
            if (_torchCharge >= _torchChargeMax)
            {
                _torchCharge = 0;
                var postChangeIdex = index + 1;
                if (postChangeIdex >= 4)
                {

                    index = 4;
                    SetSprite(index);
                    Invoke("CallReduceIndex", _coolDownTime);
                    IfFullTorch();

                }
                else
                {

                    index = postChangeIdex;
                    SetSprite(index);
                    Invoke("CallReduceIndex", _coolDownTime);
                }
            }

        }
    }

    private void SetSprite(int index)
    {
        if (index == -1)
        {
            _sprite.sprite = null;
            return;
        }
        _sprite.sprite = _flashLightLevels[index];
    }

    private void IfFullTorch()
    {
        _fullTorch = true;
        _trigger.enabled = true;
        _cooldown = true;
        Invoke("CallReduceIndex", _coolDownTime);

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AIController>())
        {
            if (other.GetComponent<AIController>()._aiState == AIController.AIStates.chase)
            {
                other.GetComponent<Animator>().SetTrigger("Freeze");
                ResetTorch();
            }
        }
    }

    void CallReduceIndex()
    {
        _cooldown = true;
        StartCoroutine(ReduceIndex());
    }

    IEnumerator ReduceIndex()
    {
        while (_cooldown == true)
        {
            index = index - 1 <= 0 ? 0 : index - 1;
            SetSprite(index);
            if (index == 0) ResetTorch();
            yield return new WaitForSeconds(1.0f);
        }
        yield break;
    }

    void ResetTorch()
    {
        _trigger.enabled = false;
        index = 0;
        _fullTorch = false;
        SetSprite(-1);
    }
}
