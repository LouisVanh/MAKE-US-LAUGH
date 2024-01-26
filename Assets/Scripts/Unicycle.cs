using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicycle : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private bool _ridingUnicycle;
    [SerializeField] private float _speed;
    [SerializeField] private float _forwardSpeed;

    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _rigidbody.automaticCenterOfMass = false;
        _rigidbody.centerOfMass = - Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _characterController.enabled = false;
            _playerBehaviour.HasControlOfMovement = false;
            _ridingUnicycle = true;
            _characterController.gameObject.transform.position = this.transform.position + new Vector3(0, 1, 0);
            _characterController.gameObject.transform.SetParent((this.gameObject.transform));
        }
        if (_ridingUnicycle)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
            {
                //this.gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
                _rigidbody.AddTorque(new Vector3(0, 0, Time.deltaTime * _speed));
                this.gameObject.transform.position += this.gameObject.transform.parent.forward * Time.deltaTime * _forwardSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //this.gameObject.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
                _rigidbody.AddTorque(new Vector3(0, 0, -Time.deltaTime * _speed));

                this.gameObject.transform.position += this.transform.parent.forward * Time.deltaTime * _forwardSpeed;
            }
        }
        if(Mathf.Abs(this.gameObject.transform.rotation.z) > 40)
        {
            //    this.GetComponent<Rigidbody>().
        }
    }
}
