using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MoveLeftRight : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Vector3 _direction;

    [SerializeField] private float _gravity = 0;
    [SerializeField] private float _strength = 0.1f;
    private bool _isNear = false;
    

    public void Start(){
        Vector3 position = transform.position;
        _direction = Vector3.zero;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            _direction = Vector3.left ;
            transform.position += _direction * Time.deltaTime;

        }
        if(Input.GetKey(KeyCode.RightArrow)){ 
            _direction = Vector3.right ;
            transform.position += _direction * Time.deltaTime;
        }
        IsItNear();
    }

    public bool IsItNear()
    {
        if ((transform.position.x >-3) & (transform.position.x < 3)){
            _isNear = true;
        }else  _isNear = false;
        return _isNear;
    }
}