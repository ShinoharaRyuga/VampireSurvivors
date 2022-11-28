# VampireSurvivor

## 開発環境
Unity 2021.3.0f1<br>
Visual Studio 2022<br>
Sourcetree<br>

## ゲーム紹介
制作当時にハマっていた「Vampire Survivors」というゲームを作成してみたいと思い、制作しました。<br>
元にしたVampire Survivorsは大量の敵をランダムで選ばれた武器を使用して倒していき、一定時間生き残ることを目的としたゲームです。

## 頑張ったところ
この開発で頑張ったところが二つあります。<br>
一つ目は一時停止・再開の機能作成です。<br>
このゲームではプレイヤーのレベルが上がると武器を獲得することができます。<br>
武器選択時はプレイヤーが不利にならないように敵・プレイヤー・武器の動きを止める必要がありました。<br>
一時停止・再開の機能作成を作成する為に今回は一時停止したいオブジェクトにIPauseインターフェイスを継承し、止めたい時に関数を呼び出すという方法を使用しました。<br>
二つ目はオブジェクトプールを使用した敵と経験値アイテムの生成です。<br>
このゲームでは敵と経験値アイテムが現れます。<br>
大量に出てくるオブジェクトを生成・削除を行っているとゲームが重くなるのでならべくゲームが軽くなるようにオブジェクトプールを使用しました。<br>
今回はObjectPoolという基底クラスを作成し、派生クラスで敵の生成を行うクラスと経験値アイテムを生成するクラスを作成しました。<br>

## プレイ動画
https://youtu.be/EwfPUQZrOuQ
