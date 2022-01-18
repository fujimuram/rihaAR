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
    
    Vector3 _init_pos;
    Vector3 _next_pos;
    bool _fin;

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
            if (this.transform.localPosition.z > _settings.Distdata)
            {
                _fin = true;
            }
            else
            {
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
