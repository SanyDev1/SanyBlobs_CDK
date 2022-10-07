using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemFollowHuman : MonoBehaviour
{
    private bool _isHumanNear;
    private Vector3 _direction;
    private Vector3 _particlePosition;
    private Vector3 _currentPosition;
    private MoveLeftRight _moveLeftRight;


    private void Start()
    {
        _moveLeftRight = GameObject.Find("Human").GetComponent<MoveLeftRight>();
        _particlePosition = transform.position;
        _direction = Vector3.zero;
    }

    void Update()
    {
        _currentPosition = transform.position;
        
        float humanPosX = _moveLeftRight.transform.position.x;
        _isHumanNear = _moveLeftRight.IsItNear();

        if (_isHumanNear == true )
        {
            if (_currentPosition.x < -3 )
            {
                transform.position = Vector3.MoveTowards(transform.position, _particlePosition, (float).1);
            }
            else if (_currentPosition.x > 3 )
            {
                transform.position = Vector3.MoveTowards(transform.position, _particlePosition, (float).1);
            }
            else _direction.x = humanPosX;

            _direction.z = _currentPosition.z;
            transform.position = Vector3.MoveTowards(transform.position, _direction, (float).1);
        }
        else transform.position = _particlePosition;
    }
}

