using System;
using Foundation;
using UIKit;

namespace XamARKitSample.Codes
{
    public abstract class CodeBase : NSObject
    {
        public UIView View { get; set; }

        public CodeBase Init(UIView view)
        {
            View = view;
            ViewDidLoad();
            ViewWillAppear(true);
            return this;
        }

        public virtual void ViewDidLoad()
        {
        }

        public virtual void ViewWillAppear(bool animated)
        {
        }

        public virtual void ViewWillDisappear(bool animated)
        {
        }

        public abstract void Close();
    }
}
