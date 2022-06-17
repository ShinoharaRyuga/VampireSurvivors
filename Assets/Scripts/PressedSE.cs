using UnityEngine;

public class PressedSE : MonoBehaviour
{
    [SerializeField, Tooltip("‰Ÿ‚µ‚½Žž‚ÌSE")] AudioClip _pressSE = default;
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
