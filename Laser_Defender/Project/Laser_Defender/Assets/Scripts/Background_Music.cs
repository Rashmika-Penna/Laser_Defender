using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Music : MonoBehaviour
{
    [SerializeField] AudioClip background_music = null;
    [SerializeField] [Range(0,1)] float background_music_volume = 0.5f;

    private void Awake()
    {
        AudioSource.PlayClipAtPoint(background_music, Camera.main.transform.position, background_music_volume);
    }
}
