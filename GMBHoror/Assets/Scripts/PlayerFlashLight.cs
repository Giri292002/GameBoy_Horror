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
            _torchCharge++;
            if (_torchCharge >= _torchChargeMax)
            {
                _torchCharge = 0;
                var postChangeIdex = index + 1;
                if (postChangeIdex > 4)
                {
                    index = 4;
                    SetSprite(index);
                    IfFullTorch();
                }
                else
                {
                    index = postChangeIdex;
                    SetSprite(index);
                }
            }
        }
    }

    private void SetSprite(int index)
    {
        _sprite.sprite = _flashLightLevels[index];
    }

    private void IfFullTorch()
    {
        _fullTorch = true;
        if (index == 4)
        {
            _trigger.enabled = true;
        }
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
            other.GetComponent<Animator>().SetTrigger("Freeze");
        }
    }
}
