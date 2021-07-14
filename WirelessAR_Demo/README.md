## <font color="DodgerBlue">概要</font>
無線ARパススルーを使用した, 半側空間無視のリハビリのための物体衝突デモアプリケーション.  
HMD装着者は, 実空間上を動く3DCGオブジェクトに衝突しないように避ける必要がある.  

## <font color="DodgerBlue">手順書</font>
### <font color="OrangeRed">注意</font>
以下の実行手順は, Unityエディタ上で実行する際の手順であり, ビルド後の手順はまだ示していない  
また, この手順は他のUnityプロジェクトでも共通の設定であるため, いずれプロジェクトを増やした際に移動する  

### 準備物
#### ハードウェア
| 機器 | 一般名称 | 用途 |
| - | - | - |
| Oculus Quest | スタンドアロンHMD | 合成済み映像取得 |
| ZED mini | ステレオカメラ | ステレオ映像取得 |
| Jetson Xavier NX | Jetsonボード | ステレオ映像の無線転送 |
| PC (Win10 pro) | 〃 | ARアプリケーション実行 |
| TP-Link A10 | 無線LANルータ | 専用LANの構築 |

#### ソフトウェア
| ソフト名 | バージョン | 用途 |
| - | - | - |
| Unity App | 2019.4.19f1(Unity) | ステレオ映像とCGの合成 |
| ALVR | v15.2.1 | 合成済み映像の無線転送 |
| SteamVR | 1.17.16 | ALVRのバックエンド |

#### SDK等
| 名称 | バージョン | 備考 |
| - | - | - |
| CUDA Toolkit | v11.0 Update 1 | 他SDKの依存度大 |
| JetPack | 4.5.1 |  |
| ZED SDK for Windows 10 | 3.5.0 | JetPackバージョンに依存 |
| ZED SDK for Jetpack 4.5 | 3.5.0 | JetPackバージョンに依存 |
| ZED Plugin for Unity | v3.5.0 | ZED SDKバージョンに依存 |

### システム概要
システム概要図を次に示す.  
![Wireless_AR_construction_smp](https://user-images.githubusercontent.com/41296626/125598402-1c3a2fce-e359-49e5-a1fe-abd920ff2f6c.png)

補足）  
<font color="Red">赤線</font>は現実のステレオ映像データの流れ.  
<font color="RoyalBlue">半透明青枠</font>はプライベートネットワークの範囲.  

### 実行手順
以下, 上から順番に実行する.  

#### ルータ側
（ルータの5G[Hz]回線 SSID: TP-Link_FB31_5G）  

1. 無線LANルータの電源を入れる.
1. PCとルータを有線で接続する.
1. 「ルータ設定ツール」を使用するため http://192.168.0.1 へアクセスし, “研究室パスワード”でログインできることを確認する.

補足）  

* ルータIPアドレスは192.168.0.9で固定（2021/07/14現在）.
* ルータ設定ツールでは次の画像のように接続されている機器の数や情報を得られる.

    ![TPLink_Rooter_SettingTool](https://user-images.githubusercontent.com/41296626/125598418-bc6868f0-589b-41bc-b5ce-9d315d71a7e2.PNG)  

#### PC側（Unity）

1. 本Unityプロジェクトを立ち上げる.
1. Unity上のインスペクタにて, “ZED_Rig_Stereo”コンテナの”ZED Manager”の”Input”の設定が次のようになっていることを確認する.  

    ```
    Input Type: Stream（ストリーミング形式で入力）  
    IP: 192.168.0.153（Jetsonの固定IP）  
    Port: 30000  
    ```

    ![Inspector_settings](https://user-images.githubusercontent.com/41296626/125598411-2df56e48-1f3d-4c66-bbd2-a5775c2486ef.PNG)

#### PC側（ALVR）& Oculus Quest側

1. [Quest] Oculus Questを無線で接続する.
1. [Quest] アプリパネルのコンボボックスから「提供元不明」を選択し, “ALVR”を起動.

    ![Quest_ALVR_not_trusted](https://user-images.githubusercontent.com/41296626/125598414-9163a352-5dbf-4d1c-b540-f2b4ed54f54d.jpg)

1. [PC] ALVRを起動する.
1. [PC] ダッシュボード内のNew Clients内のTrustボタンを押下し, Truested Clients内へQuestが移動するのを確認する.

    ![ALVR_trusted](https://user-images.githubusercontent.com/41296626/125598409-18388ecd-a004-4f78-8976-0f4b31fc5896.PNG)

1. [Quest] 白いデフォルトの空間からSteamVRの画面に遷移することを確認する.

補足）  

* 2回目以降は既にALVRのTrusted ClientsとしてOculus Questが登録されているため, 4の操作は必要なくなる. 別のQuestを使用したり設定を変更したりする際には, 必要となる.
* 5の操作において, 正しくストリーミングが行われていれば, ALVR上の「（左端）Monitorタブ」-> 「（上端）Statisticsタブ」の”Streaming Statistics”にパケットロスなどの詳細な情報がリアルタイムで表示される.

#### Jetson側
Jetsonに割り振られるIPアドレスはルータで192.168.0.153に固定（2021/07/14現在）.  

1. Jetsonの電源を入れる.
1. PC側でシェルを立ち上げる（Windows Terminalを推奨）.
1. Jetsonの固定IP 192.168.0.153 へSSH接続を行う. [ssh fujimura@192.168.0.153]

    ![ssh_connection](https://user-images.githubusercontent.com/41296626/125598417-80ecf623-c302-49d2-9a00-42bbd07219ce.PNG)

1. JetsonへZED miniをUSBで接続する（どのポートでも可）.
1. ~/kawakami へcdコマンドで移動. [cd ./kawakami]
1. ~/kawakami/stream_senderを実行. [./stream_sender]

補足）  

* SSH接続ができなかったら？  
-> Jetsonの電源を投入してから, 30秒ほど待ってから再度接続. パスワードが聞かれればOKで, その後の応答では時間がかかる可能性あり.
* stream_sender実行時に, エラー文 “[Sample][Error] Camera Open  | CAMERA NOT DETECTED : Camera not detected. Replug your device or connect to another USB port. Exit program.” が出たら？  
-> カメラがJetsonへ正しく差し込まれていないので, USBの向きを確かめて再度接続. もともと正しい向きであったとしてもこのエラーは出やすいので注意.

#### PC側

1. 音声をストリーミングするために, タスクバーのサウンド設定より"Oculus Virtual Audio Device"を選択.
    ![audio_device_streaming](https://user-images.githubusercontent.com/41296626/125607947-105456d3-6a71-4724-af3a-f6adfa260a31.PNG)
1. Unity上でプレイボタンを押下し, Oculus Questを被って動作を確認.
