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
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem mainEngineParticles;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrenght * Time.fixedDeltaTime);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngineSFX);
        if (!mainEngineParticles.isPlaying) mainEngineParticles.Play();
    }
    private void StopThrusting()
    {
        mainEngineParticles.Stop();
        audioSource.Stop();
    }

    

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopTurning();
        }

    }
    private void RotateRight()
    {
        ApplyRotation(rotationStrenght);
        //Prepei na vgainei Thetiko to ANGLE afou tha pigainei sto +Z (kanonas deksiou xerioy) (opws to roloi)
        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(-rotationStrenght);
        //prepei na einai arnitiko ANGLE afou paei sto -z (anapoda apo to roloi)
        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }
    private void StopTurning()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
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
