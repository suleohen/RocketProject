using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrenght;
    [SerializeField] float rotationStrenght;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftboosterParticless;
    [SerializeField] ParticleSystem rightboosterParticless;
    [SerializeField] ParticleSystem mainboosterParticless;

    Rigidbody rb;
    AudioSource audioSource;

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
        if(thrust.IsPressed())
        {
           StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainboosterParticless.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up*thrustStrenght*Time.fixedDeltaTime);

            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine); 
            if(!mainboosterParticless.isPlaying)
                mainboosterParticless.Play();
    }

    private void ProcessRotation()
    {
        float rotationValue = rotation.ReadValue<float>();
        
        if(rotationValue < 0)
        {
            RotateRight();
        }

        else if(rotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        
        }
    }

    private void StopRotating()
    {
        rightboosterParticless.Stop();
        leftboosterParticless.Stop();
    }

    private void RotateLeft()
    {
        Applyrotation(-rotationStrenght);
        if (!leftboosterParticless.isPlaying)
            leftboosterParticless.Play();
    }

    private void RotateRight()
    {
        Applyrotation(rotationStrenght);

        if (!rightboosterParticless.isPlaying)
            rightboosterParticless.Play();
    }

    private void Applyrotation( float rotationThisFrame)
    {
        rb.freezeRotation=true;
        transform.Rotate(Vector3.forward*rotationThisFrame*Time.fixedDeltaTime);
        rb.freezeRotation=false;

    }



}
