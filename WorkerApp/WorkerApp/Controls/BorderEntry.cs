using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WorkerApp.Controls
{
    [Preserve(AllMembers = true)]
    public class BorderEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(BorderEntry),
            Color.Gray);

        // Gets or sets BorderColor value
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }


        public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(int),
            typeof(BorderEntry),
            Device.OnPlatform<int>(1, 2, 2));

        // Gets or sets BorderWidth value
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }


        public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(BorderEntry),
            Device.OnPlatform<double>(6, 7, 7));

        // Gets or sets CornerRadius value
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
        BindableProperty.Create(
            nameof(IsCurvedCornersEnabled),
            typeof(bool),
            typeof(BorderEntry),
            true);

        public static readonly BindableProperty PaddingProperty =
        BindableProperty.Create(
            nameof(Padding),
            typeof(Thickness),
            typeof(BorderEntry),
            new Thickness(10));

        // Gets or sets CornerRadius value
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        //public bool isCustomPading { get; set; }

        //public int Top { get; set; }
        //public int Left { get; set; }
        //public int Right { get; set; }
        //public int Bottom { get; set; }

        // Gets or sets IsCurvedCornersEnabled value
        public bool IsCurvedCornersEnabled
        {
            get { return (bool)GetValue(IsCurvedCornersEnabledProperty); }
            set { SetValue(IsCurvedCornersEnabledProperty, value); }
        }
    }
}
