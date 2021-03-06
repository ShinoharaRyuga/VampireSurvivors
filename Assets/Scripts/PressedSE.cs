using UnityEngine;

public class PressedSE : MonoBehaviour
{
    [SerializeField, Tooltip("µ½ÌSE")] AudioClip _pressSE = default;
    AudioSource _audioSource = default;
    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OutputSE()
    {
        _audioSource.PlayOneShot(_pressSE);
    }
}
