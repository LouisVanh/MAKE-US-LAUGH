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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _characterController.enabled = false;
            _playerBehaviour.HasControlOfMovement = false;
            _ridingUnicycle = true;

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
        //if(Mathf.Abs(this.gameObject.transform.rotation.z) > 10)
        //{
        //    this.GetComponent<Rigidbody>().
        //}
    }
}
