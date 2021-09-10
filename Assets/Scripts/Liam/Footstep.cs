using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioClip m_footstepSound;
    public AudioSource m_audioSource;

    public void PlayFootstepSound()
    {
        m_audioSource.PlayOneShot(m_footstepSound);
    }
}
