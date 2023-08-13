using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSide : MonoBehaviour
{
    private Rigidbody myRigidBody;
    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;


    [SerializeField] private Transform pos;
    [SerializeField] private float radio;
    [SerializeField] private bool in_ground;
    [SerializeField] private LayerMask ground;

    //Animator animationManager;

    [Header("SALTO CONTROLADO")]
    public float _saltoAlto = 0.5f;
    public float _saltoBajo = 1f;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawSolidDisc(pos.position, pos.transform.forward, radio);
    }
#endif


    private void Start()
    {
        Debug.Log("MOVIMIENTO 2D");
        myRigidBody = GetComponentInChildren<Rigidbody>();
        //animationManager = GetComponentInChildren<Animator>();
    }


    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(horizontal * speed, myRigidBody.velocity.y);

        Collider[] colliders = Physics.OverlapSphere(pos.position, radio, ground);
        in_ground = colliders.Length >= 1;

        if (Input.GetButtonDown("Jump") && in_ground)
        {
            myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
        // Salto controlado
        if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector3.up * Physics.gravity.y * (_saltoAlto) * Time.deltaTime;
        }
        if (myRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myRigidBody.velocity += Vector3.up * Physics.gravity.y * (_saltoBajo) * Time.deltaTime;
        }

        //Animation
        //animationManager.SetBool("Camina", camina);
        //animationManager.SetBool("Salta", in_ground);

    }
}
