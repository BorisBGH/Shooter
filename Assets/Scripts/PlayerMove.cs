using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _mouseSensitivity = 5f;
    [SerializeField] private Transform _head;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _jumpHeight = 2f;
   private float _runSpeedMultiplier = 1f;

    private float _xEuler;
    private Vector3 _velocity;

    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * _mouseSensitivity, 0);
        _xEuler -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _xEuler = Mathf.Clamp(_xEuler, -90f, 90f);
        _head.localEulerAngles =  new Vector3(_xEuler,0,0);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputVector = new Vector3(h,0, v);   
        inputVector = Vector3.ClampMagnitude(inputVector, 1);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            _runSpeedMultiplier = 2.5f;
        }
        else
        {
            _runSpeedMultiplier = 1f;
        }

        Vector3 velocity = inputVector * _speed * _runSpeedMultiplier;
        Vector3 worldVelocity = transform.TransformVector(velocity);

        _velocity.y += _gravity * Time.deltaTime;
        _velocity = new Vector3(worldVelocity.x, _velocity.y, worldVelocity.z);

        if(_characterController.isGrounded)
        {
            if(_velocity.y < 0)
            {
                _velocity.y = -1f;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
           
        }

        _characterController.Move(_velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl) || !CheckAllowStay())
        {
            _characterController.height = Mathf.Lerp(_characterController.height, 0.8f, Time.deltaTime * 5f);
            _characterController.center = Vector3.Lerp(_characterController.center, new Vector3(0f, 0.4f, 0f), Time.deltaTime * 10f);
            _head.localPosition = Vector3.Lerp(_head.localPosition, new Vector3(0f, 0.7f, 0f), Time.deltaTime * 10f);

        }
        else
        {
            _characterController.height = Mathf.Lerp(_characterController.height, 1.8f, Time.deltaTime * 5f);
            _characterController.center = Vector3.Lerp(_characterController.center, new Vector3(0f, 0.9f, 0f), Time.deltaTime * 10f);
            _head.localPosition = Vector3.Lerp(_head.localPosition, new Vector3(0f, 1.6f, 0f), Time.deltaTime * 10f);
        }

       
    }

    private void Jump()
    {
        _velocity.y += Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }

    private bool CheckAllowStay()
    {
        RaycastHit hitDown;
        RaycastHit hitUp;
        bool isDownHited = Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hitDown);
        bool isUphitted = Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.up, out hitUp);

        if (isDownHited && isUphitted)
        {
            if(hitDown.distance + hitUp.distance > 1.8f)
            {
                return true;
            }
            else
            {
                return false;
            }
           

        }
        else
        {
            return true;
        }
    }
}
