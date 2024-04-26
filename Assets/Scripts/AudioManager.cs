using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance()
    {
        if (instance == null)
        {
            instance = new AudioManager();
        }

        return instance;
    }

    public static Dictionary<string, AudioSource> audioDict = new Dictionary<string, AudioSource>();

    private void Start()
    {
        audioDict.Clear();

        List<AudioSource> audioSources = GetComponents<AudioSource>().ToList();
        List<AudioClip> audioClips = new List<AudioClip>();
        foreach(AudioSource audioSource in audioSources) 
        {
            audioClips.Add(audioSource.clip);
        }
        for(int i = 0; i < audioClips.Count; i++)
        {
            audioDict.Add(audioClips[i].name, audioSources[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (audioDict["OST_1"].isPlaying)
            {
                audioDict["OST_1"].Stop();
            }
            else
            {
                audioDict["OST_1"].Play();
            }
        }
    }
}
