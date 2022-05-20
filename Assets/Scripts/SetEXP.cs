using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEXP : MonoBehaviour
{
    /// <summary>�ǉ�����o���l�� </summary>
    int _addEXP = 0;
    /// <summary>�ǉ�����o���l�� </summary>
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
            player.GetEXP(_addEXP);
            Destroy(gameObject);
        }
    }
}
