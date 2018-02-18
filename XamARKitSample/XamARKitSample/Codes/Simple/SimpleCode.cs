using System;
using ARKit;
using SceneKit;

namespace XamARKitSample.Codes.Simple
{
    public class SimpleCode : CodeBase
    {
        ARSCNView _arscnView { get; set; }

        public override void ViewDidLoad()
        {
            _arscnView = new ARSCNView();
            View.AddSubview(_arscnView);
            _arscnView.TranslatesAutoresizingMaskIntoConstraints = false;
            _arscnView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _arscnView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            _arscnView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _arscnView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            _arscnView.Scene = SCNScene.FromFile("art.scnassets/ship.scn");
        }

        public override void ViewWillAppear(bool animated)
        {
            _arscnView.Session.Run(new ARWorldTrackingConfiguration(), new ARSessionRunOptions());
        }

        public override void ViewWillDisappear(bool animated)
        {
            _arscnView.Session.Pause();
        }

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
