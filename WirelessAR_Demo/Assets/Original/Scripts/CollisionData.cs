using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

/// <summary>
/// 衝突回数データ
/// </summary>
public struct HitData
{
    /// <summary>
    /// 出現回数
    /// </summary>
    public uint Appeared { get; set; }

    /// <summary>
    /// 衝突回数
    /// </summary>
    public uint Collided { get; set; }

    /// <summary>
    /// 回避回数
    /// </summary>
    public uint Avoided { get => this.Appeared - this.Collided; }

    public HitData(uint appear, uint collision)
    {
        this.Appeared = appear;
        this.Collided = collision;
    }
}

/// <summary>
/// オブジェクトの衝突データ
/// </summary>
public class CollisionData
{
    // 衝突データ
    // (left, right)の衝突回数データを種類数だけ保持
    List<(HitData, HitData)> _data = new List<(HitData, HitData)>(); 

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="kind_num">衝突物体の種類の数</param>
    public CollisionData(int kind_num)
    {
        _data = new List<(HitData left, HitData right)>(kind_num);
    }

    /// <summary>
    /// 指定ファイルへデータを書き込む
    /// </summary>
    /// <param name="path">ファイルパス</param>
    void Write(string path, bool append = false, string encode = "shift-jis")
    {
        using (StreamWriter sw = new StreamWriter(path, append, Encoding.GetEncoding(encode)))
        {
            // foreach (string line in lines)
            // {
            //     sw.WriteLine(line);
            // }
        }
    }
}
