using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hareket : MonoBehaviour
{

    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed;
    float normalFov;
    public float SprintFov;

    float inputX;
    float inputY;
    float maxSpeed;

    public Transform Model;

    Animator Anim;
    Vector3 StickDirection;
    Camera MainCam;

    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.LeftControl;

    void Start()
    {
        Anim = GetComponent<Animator>();
        MainCam = Camera.main;
        normalFov = MainCam.fieldOfView;
    }


    private void LateUpdate()
    {

        StickDirection = new Vector3(inputX, 0, inputY);

        InputMove();
        InputRotation();
        Movement();

    }

    
    void Movement()
    {
        StickDirection = new Vector3(inputX, 0, inputY);
        
        if (Input.GetKey(SprintButton))
        {
            MainCam.fieldOfView = Mathf.Lerp(MainCam.fieldOfView, SprintFov, Time.deltaTime * 2);

            maxSpeed = 2;
            inputX = 2 * Input.GetAxis("Horizontal");
            inputY = 2 * Input.GetAxis("Vertical");
        }

        else if (Input.GetKey(WalkButton))
        {
            MainCam.fieldOfView = Mathf.Lerp(MainCam.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 0.2f;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }

        else
        {
            MainCam.fieldOfView = Mathf.Lerp(MainCam.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 1;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }




    }

    void InputMove()
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }


    void InputRotation()
    {
        Vector3 rotOfset = MainCam.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Model.transform.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed);
    }
}