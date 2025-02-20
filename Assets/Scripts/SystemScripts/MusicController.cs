using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        StartCoroutine(myCoroutine());
    }
    private IEnumerator myCoroutine()
    {
        yield return new WaitForSeconds(5f);
        AudioClip track = tracks[Random.Range(0, tracks.Length)];
        source.clip = track;
        source.Play();

        yield return new WaitForSeconds(track.length);
        StartCoroutine(myCoroutine());
    }
}
