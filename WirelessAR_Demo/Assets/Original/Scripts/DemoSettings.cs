using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSettings : MonoBehaviour
{
    // MEMO: インスペクタ上の値（.yamlに保存される）が優先されることに注意！
    // MEMO: 2019.x以下ではバッキングフィールドの名前がインスペクタに表示される

    #region Primary Config Params
    [field: Header("Primary Config Params")]

    /// <summary>
    /// 障害物の速度
    /// </summary>
    [field: SerializeField]
    public float Speed { get ; set; } = 30f;

    /// <summary>
    /// 総歩行距離
    /// </summary>
    [field: SerializeField]
    public int Distance { get; set; } = 4;

    /// <summary>
    /// 出現する色フラグ
    /// </summary>
    [field: SerializeField]
    public List<bool> ColorFlags { get; set; }
        = new List<bool>() { true, true };

    #endregion

    #region Orbit Config Params
    [field: Header("Orbit Config Params")]

    /// <summary>
    /// 軌道（障害物が飛んでくる軌跡）角度
    /// </summary>
    /// <value>Range: -45 to 45</value>
    [field: SerializeField, Range(-45, 45)]
    public int OrbitDegree { get; set; } = 15;

    /// <summary>
    /// 軌道角度をランダムにするか
    /// </summary>
    [field: SerializeField]
    public bool IsRandom { get; set; } = false;
    /// <summary>
    /// 最小の軌道角度
    /// ランダム生成がtrueのときのみ参照
    /// </summary>
    [field: SerializeField]
    public int MinOrbitDegree { get; set; } = -45;
    /// <summary>
    /// 最大の軌道角度
    /// ランダム生成がtrueのときのみ参照
    /// </summary>
    [field: SerializeField]
    public int MaxOrbitDegree { get; set; } = 45;

    #endregion

    #region Save Config Params
    [field: Header("Save Config Params")]

    /// <summary>
    /// 前回の記録データに上書きするか
    /// </summary>
    [field: SerializeField]
    public bool IsOverwrite { get; set; } = true;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // var a = this.Speed;
    }
}
