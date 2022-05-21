using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEXP : MonoBehaviour
{
    [SerializeField] int _testPoint = 0;
    /// <summary>追加する経験値量 </summary>
    int _addEXP = 0;
    /// <summary>追加する経験値量 </summary>
    public int AddEXP
    {
        get { return _addEXP; }
        set
        {
            if (value <= 0)
            {
                _addEXP = 1;
            }
            else
            {
                _addEXP = value;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.GetEXP(_testPoint);
            Destroy(gameObject);
        }
    }
}
