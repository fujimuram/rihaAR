using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 先頭の当たり判定
/// </summary>
public class SensorA : MonoBehaviour
{
    /// <summary>
    /// 歩行中であるか
    /// </summary>
    public bool IsWalking { get; private set; }

    /// <summary>
    /// A地点からB地点到達までにかかった時間
    /// </summary>
    public float Duration { get; private set; }


    [SerializeField] DemoSettings _settings;
    [SerializeField] GameObject _msg_box;
    [SerializeField] GameObject _target;
    
    Vector3 _init_pos;
    Vector3 _next_pos;

    [field: SerializeField]
    public bool IsFin { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        _init_pos = this.transform.localPosition;
        _next_pos = _init_pos;
        this.IsWalking = false;
        this.Duration = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsWalking)
        {
            this.Duration += Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 総歩行距離歩いたか
            if (this.transform.localPosition.z > _settings.Distdata ||
                this.IsFin)
            {
                // if (!)
                // {
                    // メッセージボックスを正面に表示し終了
                    _msg_box.SetActive(true);
                    this.IsFin = true;
                // }
            }
            else
            {
                _target.SetActive(false);

                // 歩行開始とみなす
                this.IsWalking = true;
                this.Duration = 0f;
            }
        }
    }

    /// <summary>
    /// B地点まで歩いたとみなす
    /// </summary>
    public void StopPlayer()
    {
        this.IsWalking = false;
    }

    public void Move(float dist)
    {
        _next_pos.z += dist + 1f;
        this.transform.localPosition = _next_pos;
    }
}
