using System;
using System.Collections.Generic;

using Foundation;
using SceneKit;
using UIKit;
using XamARKitSample.Codes;

namespace XamARKitSample
{
    public partial class ViewController : UIViewController
    {
        CodeBase _code { get; set; }

        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var stack = CreateAndAddButtonsStack();
            var viewForARCode = CreateAndAddViewForARCode();
            var back = CreateAndAddBackButtons(stack, viewForARCode);

            // ARKit Sample Buttons
            CreateAndAddButton("Simple", () => new Codes.Simple.SimpleCode(), stack, viewForARCode, back);
            CreateAndAddButton("PlaneDetection", () => new Codes.PlaneDetection.PlaneDetectionCode(), stack, viewForARCode, back);
            CreateAndAddButton("Simple", () => new Codes.Simple.SimpleCode(), stack, viewForARCode, back);
        }

        UIStackView CreateAndAddButtonsStack()
        {
            var stack = new UIStackView();
            stack.Axis = UILayoutConstraintAxis.Vertical;
            stack.Distribution = UIStackViewDistribution.EqualSpacing;
            stack.SetAnchor(View, 10f, 100f);
            return stack;
        }

        UIView CreateAndAddViewForARCode()=> new UIView() { Hidden = true }.FillParent(View);

        UIButton CreateAndAddBackButtons(UIStackView stack, UIView viewForARCode)
        {
            var button = new UIButton() { BackgroundColor = UIColor.Blue, Hidden = true };
            button.SetTitle($"  < Back  ", UIControlState.Normal);
            button.TouchUpInside += (sender, e) =>
            {
                button.Hidden = true;
                _code?.Close();
                viewForARCode.Hidden = true;
                stack.Hidden = false;
            };
            button.SetAnchorTopLeft(View, 0f, 20f);
            return button;
        }

        void CreateAndAddButton(string title, Func<CodeBase> codeFactory, UIStackView stack, UIView viewForARCode, UIButton back)
        {
            var button = new UIButton() { BackgroundColor = UIColor.Blue };
            button.SetTitle($"  {title}  ", UIControlState.Normal);
            button.TouchUpInside += (sender, e) =>
            {
                stack.Hidden = true;
                viewForARCode.Hidden = false;
                _code = codeFactory.Invoke().Init(viewForARCode);
                back.Hidden = false;
            };
            stack.AddArrangedSubview(button);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _code?.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _code?.ViewWillDisappear(animated);
        }
    }
}
