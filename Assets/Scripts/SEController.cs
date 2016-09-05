using UnityEngine;
using System.Collections;

public class SEController : MonoBehaviour {

    public AudioClip[] audioClips;

    static public AudioClip[] clips = new AudioClip[100];

    void Start()
    {
        for(int i = 0; i < audioClips.Length; i++)
        {
            clips[i] = audioClips[i];
        }
    }
}
