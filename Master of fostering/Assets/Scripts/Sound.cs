using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0.0f, 1.0f)]
    public float volumn;
    [Range(0.1f, 3.0f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
