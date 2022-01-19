using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 方向
/// ターゲットの出現方角
/// </summary>
public enum Direction
{
    Left,
    Right,
}

/// <summary>
/// 衝突回数データ
/// </summary>
public class HitData
{
    /// <summary>
    /// 出現回数
    /// </summary>
    public uint Appeared { get; set; } = 0;

    /// <summary>
    /// 衝突回数
    /// </summary>
    public uint Collided { get; set; } = 0;

    /// <summary>
    /// 回避回数
    /// </summary>
    public uint Avoided { get => this.Appeared - this.Collided; }

    public HitData() { }

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
    List<(HitData left, HitData right)> _data = new List<(HitData, HitData)>(); 

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="kind_num">衝突物体の種類の数</param>
    public CollisionData(int kind_num)
    {
        _data = new List<(HitData, HitData)>(kind_num);

        // データ追加
        for (int i = 0; i < kind_num; ++i)
            _data.Add((new HitData(), new HitData()));
    }

    /// <summary>
    /// 出現カウントを行う
    /// </summary>
    /// <param name="id">ターゲットid</param>
    /// <param name="dir">ターゲットの出現方向</param>
    public void CountAppear(int id, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                _data[id].left.Appeared++;
                break;
            case Direction.Right:
                _data[id].right.Appeared++;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 衝突カウントを行う
    /// </summary>
    /// <param name="id">ターゲットid</param>
    /// <param name="dir">ターゲットの出現方向</param>
    public void CountHit(int id, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                _data[id].left.Collided++;
                break;
            case Direction.Right:
                _data[id].right.Collided++;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 指定ファイルへデータを書き込む
    /// </summary>
    /// <param name="filename">ファイル名</param>
    public bool Write(string filename, bool append = false)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<(HitData left, HitData right)>));
        using (var sw = new StreamWriter(filename, append, new System.Text.UTF8Encoding(false)))
        {
            // シリアル化
            serializer.Serialize(sw, _data);
        }

        return true;
    }
}
