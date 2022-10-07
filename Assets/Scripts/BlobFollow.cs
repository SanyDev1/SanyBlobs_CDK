using System;
using System.Collections;
using System.Collections.Generic;
using MudBun;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BlobFollow : MonoBehaviour
{
    private bool _isHumanNear;
    public Vector3 _direction;
    private Vector3 _particlePosition;
    private Vector3 _currentPosition;
    private MoveLeftRight _moveLeftRight;
    
    //Used to determine the speed the blobs travel to the desired location.
    //Use values between 0.0002 to 0.0006 otherwise the blobs start flickering
    [Range(0, 1), SerializeField]  private float _maxDistanceDelta = 0;   
    
    [SerializeField] private GameObject _blobObject;
    private FloaterNew _mudFloater;
    //private BallBounce _ballBouncer;
    
    [Range(-2,2), SerializeField]  private float _offsetToBlob = 0;   //Used to give some space between the blobs when they move so they don't merge into one big blob
    private bool _isNotMoving = false;
    private float _humanPosX;
    
    private void Start()
    {
        _mudFloater = _blobObject.GetComponent<FloaterNew>();
        //_ballBouncer = _blobObject.GetComponent<BallBounce>();
        _moveLeftRight = GameObject.Find("Human").GetComponent<MoveLeftRight>();
        _particlePosition = transform.position;
        _direction = Vector3.zero;
    }

    void Update()
    {
        _currentPosition = transform.position;
        _humanPosX = _moveLeftRight.transform.position.x;
        _isHumanNear = _moveLeftRight.IsItNear();

        StartCoroutine(CheckMoving());

        if (_isHumanNear == true & _isNotMoving == false)
        {
            this.enabled = true;
            _mudFloater.enabled = false;
            //_ballBouncer.enabled = false;

            switch (_currentPosition.x)
            {
                case < -3:
                case > 3:
                    transform.position = Vector3.MoveTowards(transform.position, _particlePosition, _maxDistanceDelta);
                    break;
                default:
                    _direction.x = _humanPosX + _offsetToBlob;
                    _direction.y = _particlePosition.y;
                    _direction.z = _particlePosition.z;
                    transform.position = Vector3.MoveTowards(transform.position, _direction, _maxDistanceDelta);
                    break;
            }
            _direction.y = _particlePosition.y;
            _direction.z = _particlePosition.z;
            transform.position = Vector3.MoveTowards(transform.position, _direction, _maxDistanceDelta);

            
        }
        else if (_isHumanNear == true & _isNotMoving == true)
        {
            _direction.y = _particlePosition.y;
            _direction.z = _particlePosition.z;
            _direction.x = _humanPosX + _offsetToBlob;
            transform.position = Vector3.MoveTowards(transform.position, _direction, _maxDistanceDelta );
            if(transform.position == _direction) _mudFloater.enabled = true;
            //_ballBouncer.enabled = true;

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _particlePosition, _maxDistanceDelta);
        }
          
        if (_currentPosition == _particlePosition & _isHumanNear == false )
        {
            _mudFloater.enabled = true;
            //_ballBouncer.enabled = true;
        }
    }
    
    private IEnumerator CheckMoving()
    {
        var startPosX = _humanPosX;
        yield return new WaitForSeconds(1f);
        var finalPosX = _humanPosX;
        const double tolerance = 0.000000001;
        _isNotMoving = Math.Abs(startPosX - finalPosX) < tolerance; 
    }
    
}

