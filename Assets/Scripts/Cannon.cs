using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private Transform _shootingDir;
    [SerializeField] private Vector3 _initialRigidbodyPosition;

    [SerializeField] private bool _inCannon;
    private bool _isShooting;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _explosionForce;


    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInChildren<Rigidbody>();
        _initialRigidbodyPosition = _rigidbody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _characterController.enabled = false;
            _playerBehaviour.HasControlOfMovement = false;
            _inCannon = true;
            _rigidbody.freezeRotation = false;
            _characterController.gameObject.transform.position = this.transform.position + new Vector3(0, 1, 0);
            _characterController.gameObject.transform.SetParent((_rigidbody.gameObject.transform));
        }
        if (_inCannon && !_isShooting)
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                this.gameObject.transform.Rotate(new Vector3(Time.deltaTime * _rotationSpeed, 0, 0));
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                this.gameObject.transform.Rotate(new Vector3( -Time.deltaTime * _rotationSpeed, 0, 0));
            }
            if (Input.GetKey(KeyCode.Space) && !_isShooting)
            {
                _rigidbody.AddForce(_explosionForce * _shootingDir.transform.forward, ForceMode.Impulse);
                _rigidbody.AddTorque(new Vector3(50,0.5f,0));
                _isShooting = true;
                //this.gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
                //this.gameObject.transform.position += this.gameObject.transform.parent.forward * Time.deltaTime * _forwardSpeed;
            }
        }
        if(_inCannon && Mathf.Abs(_rigidbody.position.y) < 0.5f)
        {
            _rigidbody.transform.DetachChildren();
            _characterController.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            _characterController.enabled = true;
            _playerBehaviour.HasControlOfMovement = true;
            _inCannon = false;
            _isShooting = false;
            _rigidbody.Sleep();
            _rigidbody.freezeRotation = true;
            _rigidbody.transform.position = _initialRigidbodyPosition;
            //play landing sound TODO AUDIO
        }
    }
}
