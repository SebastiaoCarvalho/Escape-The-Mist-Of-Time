using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length == 1)
        {
            DontDestroyOnLoad(transform.gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying)
        {
            return;
        }
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void Start()
    {
        PlayMusic();
    }
}