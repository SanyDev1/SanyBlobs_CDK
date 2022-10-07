using System;
using System.Collections;
using System.Collections.Generic;
using MudBun;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
 private Rigidbody rb;
 private Vector3 lastVelocity;
 private Vector3 _direction;
 private Vector3 _particlePosition;
 private Vector3 _bounceDirection;
 private MoveLeftRight _moveLeftRight;
 private bool _isHumanNear;
 private FloaterNew _mudFloater;
 private BlobFollow _blobFollow;
 
 [SerializeField] private GameObject _blobbyObject;
 private Vector3 _newDirection;



 void Start()
 {
  _moveLeftRight = GameObject.Find("Human").GetComponent<MoveLeftRight>();
  _blobFollow = _blobbyObject.GetComponent<BlobFollow>();
  _mudFloater = _blobbyObject.GetComponent<FloaterNew>();
  
  _particlePosition = transform.position;
  rb = GetComponent<Rigidbody>();
 }

 private void Update()
 {
  //StartCoroutine(CheckMoving());
  lastVelocity = rb.velocity;
  _isHumanNear = _moveLeftRight.IsItNear();
  _newDirection = _blobFollow._direction;
  if (_mudFloater.enabled == true)
  {
   StartCoroutine(CheckMoving());
  }
 }

 private void OnCollisionEnter(Collision collision)
 {
  var speed = lastVelocity.magnitude/4;
  _bounceDirection = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
  rb.velocity = _bounceDirection * Mathf.Max(speed, 0.05f);
  StartCoroutine(CheckMoving());
 }
 
 private IEnumerator CheckMoving()
 {
  yield return new WaitForSeconds(1f);
  //var speed = lastVelocity.magnitude/3;
  //bounceDirection = Vector3.Reflect(lastVelocity.normalized, _bounceDirection.normalized);
  //rb.velocity = _bounceDirection * Mathf.Max(speed, 0.5f);
  if (_isHumanNear == false)
  {
   transform.position = Vector3.MoveTowards(transform.position, _particlePosition, (float)0.0005);
  } 
  else transform.position = Vector3.MoveTowards(transform.position, _newDirection, (float)0.0008);
 }
 
}
