using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startHook : MonoBehaviour
{
    
    public float timeTillHook = 20.664f;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(timeTillHook);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
