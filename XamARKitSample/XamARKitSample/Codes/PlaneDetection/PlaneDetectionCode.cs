using System;
using System.Linq;
using ARKit;
using CoreFoundation;
using Foundation;
using OpenTK;
using SceneKit;
using UIKit;

namespace XamARKitSample.Codes.PlaneDetection
{
    public class PlaneDetectionCode : CodeBase
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

            _arscnView.Delegate = new MyARSCNViewDelegate();
            _arscnView.Scene = new SCNScene();
            _arscnView.DebugOptions = ARSCNDebugOptions.ShowFeaturePoints;
        }

        public override void ViewWillAppear(bool animated)
        {
            _arscnView.Session.Run(new ARWorldTrackingConfiguration() { PlaneDetection = ARPlaneDetection.Horizontal }, new ARSessionRunOptions());
        }

        public override void ViewWillDisappear(bool animated)
        {
            _arscnView.Session.Pause();
        }

        public override void Close()
        {
            _arscnView.Session.Pause();
            _arscnView.Session.Dispose();
            _arscnView.Delegate.Dispose();
            _arscnView.Delegate = null;
            _arscnView.RemoveFromSuperview();
            _arscnView.Dispose();
            _arscnView = null;
        }

        // ARSCNViewDelegate の実装

        class MyARSCNViewDelegate : ARSCNViewDelegate
        {
            public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
            {
                var planeAnchor = anchor as ARPlaneAnchor ?? throw new Exception();
                AddPlaneNode(planeAnchor, node, UIColor.Blue.ColorWithAlpha(0.3f));
            }

            public override void DidUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
            {
                var planeAnchor = anchor as ARPlaneAnchor ?? throw new Exception();
                UpdatePlaneNode(planeAnchor, node);
            }

            void AddPlaneNode(ARPlaneAnchor anchor, SCNNode node, NSObject contents)
            {
                var geometry = SCNPlane.Create(anchor.Extent.X, anchor.Extent.Z);
                var material = geometry.Materials.FirstOrDefault() ?? throw new Exception();

                switch (contents)
                {
                    case SCNProgram program:
                        material.Program = program;
                        break;
                    default:
                        material.Diffuse.Contents = contents;
                        break;
                }

                var planeNode = SCNNode.FromGeometry(geometry);
                SceneKit.SCNMatrix4.CreateFromAxisAngle(new SCNVector3(1, 0, 0), (float)(-Math.PI / 2.0), out var m4);
                planeNode.Transform = m4;

                DispatchQueue.MainQueue.DispatchAsync(() => node.AddChildNode(planeNode));
            }

            SCNNode FindPlaneNode(SCNNode node)
            {
                foreach (var childNode in node.ChildNodes)
                    if ((childNode.Geometry as SCNPlane) != null)
                        return childNode;
                return null;
            }

            void UpdatePlaneNode(ARPlaneAnchor anchor, SCNNode node)
            {

                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    var plane = FindPlaneNode(node)?.Geometry as SCNPlane;
                    if (plane == null) return;
                    if (PlaneSizeEqualToExtent(anchor, plane, anchor.Extent)) return;

                    plane.Width = anchor.Extent.X;
                    plane.Height = anchor.Extent.Z;
                });
            }

            bool PlaneSizeEqualToExtent(ARPlaneAnchor anchor, SCNPlane plane, NVector3 extent)
            {
                if (plane.Width != (nfloat)anchor.Extent.X || plane.Height != (nfloat)anchor.Extent.Z)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

    }
}
