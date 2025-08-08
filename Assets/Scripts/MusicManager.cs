using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private AudioSource SFX; // might be needed for future

    // For now for main menu
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool playOnLoad;

    private string currentMusicClipName;

    private void Awake()
    {
        music.volume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        if (playOnLoad)
        {
            music.clip = clip;
            music.Play();
        }
    }

    public void setMusic(string musicFile)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Music/{musicFile}");

        if (!clip) return;

        currentMusicClipName = musicFile;

        music.clip = clip;
        music.Play();
    }

    public void setMusicClipName(string name)
    {
        currentMusicClipName = name;
    }

    public string getMusicClipName()
    {
        return currentMusicClipName;
    }
}
