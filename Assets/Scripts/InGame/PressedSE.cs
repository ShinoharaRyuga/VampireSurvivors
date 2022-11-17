using UnityEngine;

/// <summary>ボタンを押した時にSEを鳴らすクラス</summary>
[RequireComponent(typeof(AudioSource))]
public class PressedSE : MonoBehaviour
{
    [SerializeField, Tooltip("押した時のSE")] AudioClip _pressSE = default;
    AudioSource _audioSource => gameObject.AddComponent<AudioSource>();
   
    /// <summary>音を鳴らす </summary>
    public void OutputSE()
    {
        _audioSource.PlayOneShot(_pressSE);
    }
}
