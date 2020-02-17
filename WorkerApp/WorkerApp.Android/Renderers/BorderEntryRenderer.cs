using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using WorkerApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(BorderEntry), typeof(WorkerApp.Droid.Renderers.BorderEntryRenderer))]

namespace WorkerApp.Droid.Renderers
{
    class BorderEntryRenderer : EntryRenderer
    {
        public BorderEntryRenderer() : base(Application.Context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (BorderEntry)Element;

                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background
                    var _gradientBackground = new GradientDrawable();
                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line
                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                    // Radius for the curves
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                    // set the background of the label
                    Control.SetBackground(_gradientBackground);

                }
                Control.SetPadding(
                       (int)DpToPixels(this.Context, Convert.ToSingle((int)view.Padding.Left)),
                       (int)DpToPixels(this.Context, Convert.ToSingle((int)view.Padding.Top)),
                       (int)DpToPixels(this.Context, Convert.ToSingle((int)view.Padding.Right)),
                       (int)DpToPixels(this.Context, Convert.ToSingle((int)view.Padding.Bottom)));
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}