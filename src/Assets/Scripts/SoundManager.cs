using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource sfxSource;

    private Dictionary<string, AudioClip> audios = new Dictionary<string, AudioClip>();
    // Use this for initialization
    void Start()
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Audio");
        foreach (AudioClip clip in audioClips)
            audios.Add(clip.name, clip);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(string name)
    {
        if (audios.ContainsKey(name))
            sfxSource.PlayOneShot(audios[name]);
    }
}
