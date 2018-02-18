using System;
using UIKit;

namespace XamARKitSample
{
    public static class UIViewExtensions
    {
        public static UIView FillParent(this UIView child, UIView parent)
        {
            parent.AddSubview(child);
            child.TranslatesAutoresizingMaskIntoConstraints = false;
            child.LeftAnchor.ConstraintEqualTo(parent.LeftAnchor).Active = true;
            child.TopAnchor.ConstraintEqualTo(parent.TopAnchor).Active = true;
            child.RightAnchor.ConstraintEqualTo(parent.RightAnchor).Active = true;
            child.BottomAnchor.ConstraintEqualTo(parent.BottomAnchor).Active = true;
            return child;
        }

        public static UIView SetAnchor(this UIView child, UIView parent, nfloat left, nfloat top, nfloat right, nfloat bottom)
        {
            parent.AddSubview(child);
            child.TranslatesAutoresizingMaskIntoConstraints = false;
            child.LeftAnchor.ConstraintEqualTo(parent.LeftAnchor, left).Active = true;
            child.TopAnchor.ConstraintEqualTo(parent.TopAnchor, top).Active = true;
            child.RightAnchor.ConstraintEqualTo(parent.RightAnchor, right).Active = true;
            child.BottomAnchor.ConstraintEqualTo(parent.BottomAnchor, bottom).Active = true;
            return child;
        }

        public static UIView SetAnchor(this UIView child, UIView parent, nfloat horizontal, nfloat vertical)
        {
            return SetAnchor(child, parent, horizontal, vertical, -horizontal, -vertical);
        }

        public static UIView SetAnchorTopLeft(this UIView child, UIView parent, nfloat left, nfloat top)
        {
            parent.AddSubview(child);
            child.TranslatesAutoresizingMaskIntoConstraints = false;
            child.LeftAnchor.ConstraintEqualTo(parent.LeftAnchor, left).Active = true;
            child.TopAnchor.ConstraintEqualTo(parent.TopAnchor, top).Active = true;
            return child;
        }
    }
}
