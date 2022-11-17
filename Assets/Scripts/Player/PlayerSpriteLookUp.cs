using UnityEngine;

/// <summary>プレイヤースプライトを常に上を向き続けさせるクラス</summary>
public class PlayerSpriteLookUp : MonoBehaviour
{
    //常に上を向かせ続けないと進行方向を向いてしまい、見た目が悪くなる

    void Update()
    {
        transform.up = Vector2.up;
    }
}
