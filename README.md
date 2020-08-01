## 概要
ビデオシースルー型ARの欠点の1つである有線接続を取っ払った完全Wirelessのシースルー型ARシステム  
ステレオカメラからの情報をPCへ無線で飛ばし, PCから無線でHMDにVRアプリを送信する  

#### 光学シースルーデバイスの代用性
医療用検査や日常的なリハビリにおいて視野角の狭さは×  
Oculus系 > ZED mini >> Hololense 2 であるためARを用いる  

ZED miniの視野角: 110°(D)×90°(H)×60°(V)  
Hololense 2の視野角: 52°(D)×29°(H)×43°(V)  

## 使用機器
- Oculus Quest  
StandaloneのHMD

- ZED mini  
6-Dofのステレオカメラ  
Third-partyソフトとの連携へのサポートが手厚い  

- Jetson Xavier NX  
NVIDIA開発のシングルボードコンピュータ  
ZED miniの情報をエンコードしPCへストリーミングする

- PC  
Oculus, SteamVR, ALVR用にWindowsを搭載  
ZED mini情報の受信時のデコード, ALVRでのサーバを担当する  
GPUはGTX 2080以上を推奨(?)  

## 仕様ソフトウェア
- Unity  
VRアプリ開発用

- SideQuest  
Oculus Questへのapkファイルのインストール  
今回はALVRアプリのインストールに用いる  

- SteamVR  
様々なVR機器に対応したゲーム配信プラットフォーム  

- ALVR  
SteamVR連携でVRアプリをWifi経由でOculus Questにストリーミングするフリーソフト  
https://github.com/JackD83/ALVR

## 構成案
![wireless_ar_paththrough_proto](https://user-images.githubusercontent.com/41296626/89109011-6aba9c00-d478-11ea-87aa-471d8edbce14.PNG)

ストリーミングの情報量が多いため高速な無線環境が必要  
最低でもIEEE802.11acの2*2 MIMO対応のAP推奨  

## 問題点
- 機器取り付け  
HMD側はZED mini, Jetson, バッテリーを携帯して持つ  
ZED miniは前面取り付け前提だが他の機器はどうするか  

- バッテリーの重さ  
バッテリー駆動時間と被験者への負担のトレードオフ  

- ネットワーク速度  
高いビットレートを要求するため通信が安定しないと映像が途切れる可能性あり  
許容できる着地点を見つける必要があるかも  

- 同一LAN内のみ使用可  
WANから同空間に入るといったことは無理  
ローカルの被験者についてのみ考える場合は問題ないと考えられる  
