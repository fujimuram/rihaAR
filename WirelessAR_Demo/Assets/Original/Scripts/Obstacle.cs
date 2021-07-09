using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum MoveType
    {
        // 矢印キーで操作
        Controllable,

        // 指定位置から直線移動
        Linear,
    }

    // 障害物の動き方
    public MoveType moveType = MoveType.Controllable;
    // 障害物の動く速さ
    public float speed = 100;
    // 障害物の向き
    public Vector3 direction = Vector3.back;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.moveType)
        {
            case MoveType.Controllable:
                this.MoveByKeyboard();
                break;
            
            case MoveType.Linear:
                this.MoveByLinear(direction);
                break;

            default:
                break;
        }
    }

    // キーボードで障害物を操作する
    void MoveByKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            GetComponent<Rigidbody>().AddForce(Vector3.left * speed, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            GetComponent<Rigidbody>().AddForce(Vector3.right * speed, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            GetComponent<Rigidbody>().AddForce(Vector3.back * speed, ForceMode.Force);
        }
    }

    // 現在の位置から指定方向へ等速直線運動させる
    void MoveByLinear(Vector3 vec)
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Rigidbody>().AddForce(vec * speed * 0.01f, ForceMode.Impulse);
        }
    }
}
