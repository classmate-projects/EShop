using Microsoft.Maui.Controls.Shapes;

namespace EShop.Core.CustomViews
{
    public partial class BorderedEntry : ContentView
    {
        public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(BorderedEntry), default(string), BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(BorderedEntry), default(string));

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(BorderedEntry), Colors.Gray);

        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(double), typeof(BorderedEntry), 1.0);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(BorderedEntry), 8f);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BorderedEntry), Colors.Black);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(BorderedEntry), Colors.Gray);

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(BorderedEntry), false);

        public static readonly BindableProperty FocusBorderColorProperty =
            BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(BorderedEntry), Colors.DeepSkyBlue);


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public double BorderThickness
        {
            get => (double)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        public Color FocusBorderColor
        {
            get => (Color)GetValue(FocusBorderColorProperty);
            set => SetValue(FocusBorderColorProperty, value);
        }

        private readonly BorderedEntryDrawable _drawable;
        private readonly Entry _entry;
        private readonly GraphicsView _graphicsView;


        public BorderedEntry()
        {
            _drawable = new BorderedEntryDrawable();

            _graphicsView = new GraphicsView
            {
                Drawable = _drawable
            };

            _entry = new Entry
            {
                BackgroundColor = Colors.Transparent,
                Margin = new Thickness(8)
            };

            _entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this, mode: BindingMode.TwoWay));
            _entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
            _entry.SetBinding(Entry.TextColorProperty, new Binding(nameof(TextColor), source: this));
            _entry.SetBinding(Entry.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), source: this));
            _entry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));

            _entry.Focused += (s, e) =>
            {
                _drawable.IsFocused = true;
                _graphicsView.Invalidate();
            };

            _entry.Unfocused += (s, e) =>
            {
                _drawable.IsFocused = false;
                _graphicsView.Invalidate();
            };

            var layout = new Grid();
            layout.Children.Add(_graphicsView);
            layout.Children.Add(_entry);

            Content = layout;
        }
        private class BorderedEntryDrawable : IDrawable
        {
            public Color BorderColor { get; set; } = Colors.Gray;
            public Color FocusBorderColor { get; set; } = Colors.DeepSkyBlue;
            public float CornerRadius { get; set; } = 8f;
            public float BorderThickness { get; set; } = 1f;
            public bool IsFocused { get; set; } = false;

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                var strokeColor = IsFocused ? FocusBorderColor : BorderColor;
                canvas.StrokeColor = strokeColor;
                canvas.StrokeSize = BorderThickness;
                canvas.FillColor = Colors.Transparent;

                canvas.FillRoundedRectangle(dirtyRect, CornerRadius);
                canvas.DrawRoundedRectangle(dirtyRect, CornerRadius);
            }
        }
    }
}
