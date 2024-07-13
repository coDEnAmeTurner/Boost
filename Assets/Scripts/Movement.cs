using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    private AudioSource audioSource;

    [SerializeField] float thrustPower = 1000;
    [SerializeField] float rotationAngle = 69;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem sideThrusterA;
    [SerializeField] ParticleSystem sideThrusterB;
    [SerializeField] ParticleSystem sideThrusterC;
    [SerializeField] ParticleSystem sideThrusterD;
    [SerializeField] ParticleSystem jetBooster;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
            ApplyRotation(true);
        else if (Input.GetKey(KeyCode.D))
            ApplyRotation(false);
    }

    private void ApplyRotation(bool forward)
    {
        rb.freezeRotation = true;
        transform.Rotate((forward?1:-1) * Vector3.forward * rotationAngle * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustPower);
            if (!jetBooster.isPlaying)
                jetBooster.Play();
            if (!sideThrusterA.isPlaying)
                PlaySideThrusterEffect();
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);

        }
        else
        {
            jetBooster.Stop();
            StopSideThrusterEffect();
            audioSource.Stop();
        }
    }

    private void StopSideThrusterEffect()
    {
        sideThrusterA.Stop();
        sideThrusterB.Stop();
        sideThrusterC.Stop();
        sideThrusterD.Stop();
    }

    private void PlaySideThrusterEffect()
    {
        sideThrusterA.Play();
        sideThrusterB.Play();
        sideThrusterC.Play();
        sideThrusterD.Play();
    }
}
