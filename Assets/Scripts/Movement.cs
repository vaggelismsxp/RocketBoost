using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning , typically set in the editor
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrenght = 100f;
    [SerializeField] float rotationStrenght = 100f;
    [SerializeField] AudioClip mainEngine;
    

    //CACHE - eg. references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

    //STATE - private instances (member) variables
    // state like : bool isAlive

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrenght * Time.fixedDeltaTime);
            if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
        }
        else 
        {
            audioSource.Stop(); 
        }
    }
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            //Prepei na vgainei Thetiko to ANGLE afou tha pigainei sto +Z (kanonas deksiou xerioy) (opws to roloi)
            ApplyRotation(rotationStrenght);
        }
        else if (rotationInput > 0)
        { //prepei na einai arnitiko ANGLE afou paei sto -z (anapoda apo to roloi)
            ApplyRotation(-rotationStrenght);
        }

    }
    private void ApplyRotation(float rotationThisFrame)
    {
        //rb.MoveRotation(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime, 1);
        //Quaternion deltaRotation = Quaternion.Euler(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);
        
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
