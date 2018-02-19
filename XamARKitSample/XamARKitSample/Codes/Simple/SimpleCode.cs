using System;
using ARKit;
using SceneKit;

namespace XamARKitSample.Codes.Simple
{
    /// <summary>
    /// 空間にモデルを置くだけのシンプルなサンプル
    /// </summary>
    public class SimpleCode : CodeBase
    {
        /// <summary>
        /// ARSCNView の参照を取得または設定します
        /// </summary>
        ARSCNView _arscnView { get; set; }

        /// <summary>
        /// 初期化処理を行います
        /// </summary>
        public override void ViewDidLoad()
        {
            // ARSCNView のインスタンスを生成し画面にセット、レイアウトを指定します
            _arscnView = new ARSCNView();
            View.AddSubview(_arscnView);
            _arscnView.TranslatesAutoresizingMaskIntoConstraints = false;
            _arscnView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _arscnView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            _arscnView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _arscnView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            // ARSCNView のシーンをモデルデータで作成します
            _arscnView.Scene = SCNScene.FromFile("art.scnassets/ship.scn");
        }

        /// <summary>
        /// セッションを開始します
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            _arscnView.Session.Run(new ARWorldTrackingConfiguration(), new ARSessionRunOptions());
        }

        /// <summary>
        /// セッションを停止します
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear(bool animated)
        {
            _arscnView.Session.Pause();
        }

        /// <summary>
        /// サンプルを閉じます
        /// </summary>
        public override void Close()
        {
            _arscnView.Session.Pause();
            _arscnView.Session.Dispose();
            _arscnView.RemoveFromSuperview();
            _arscnView.Dispose();
            _arscnView = null;
        }

    }
}
