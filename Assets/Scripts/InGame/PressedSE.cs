using UnityEngine;

/// <summary>�{�^��������������SE��炷�N���X</summary>
[RequireComponent(typeof(AudioSource))]
public class PressedSE : MonoBehaviour
{
    [SerializeField, Tooltip("����������SE")] AudioClip _pressSE = default;
    AudioSource _audioSource => gameObject.AddComponent<AudioSource>();
   
    /// <summary>����炷 </summary>
    public void OutputSE()
    {
        _audioSource.PlayOneShot(_pressSE);
    }
}
