using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public class AudioTrigger : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private bool autoPlay = true;
    [SerializeField] private bool loop = true;
    [SerializeField] private AudioClip sound;
    [SerializeField] private float fadeSpeed = 1.0f;
    [SerializeField] private float maxVolume = 1.0f;

    private AudioSource audioSource;
    private bool isPlayerInside = false;
    private bool isFadingIn = false;
    private bool isFadingOut = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.loop = loop;
        audioSource.volume = 0f;
        audioSource.playOnAwake = false;

        // Make sure the collider is a trigger
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        if (autoPlay)
        {
            PlaySound();
        }
    }

    private void Update()
    {
        if (isFadingIn)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, maxVolume, fadeSpeed * Time.deltaTime);
            if (audioSource.volume >= maxVolume)
                isFadingIn = false;
        }

        if (isFadingOut)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, fadeSpeed * Time.deltaTime);
            if (audioSource.volume <= 0f)
            {
                isFadingOut = false;
                audioSource.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (autoPlay || !other.CompareTag("Player")) return;

        isPlayerInside = true;
        PlaySound();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInside = false;
        StopSound();
    }

    public void PlaySound()
    {
        if (sound == null) return;

        if (!audioSource.isPlaying)
            audioSource.Play();

        isFadingIn = true;
        isFadingOut = false;
    }

    public void StopSound()
    {
        isFadingOut = true;
        isFadingIn = false;
    }
}
