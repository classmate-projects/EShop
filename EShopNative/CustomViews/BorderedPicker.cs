namespace EShopNative.CustomViews
{
    public partial class BorderedPicker : ContentView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(BorderedPicker), default(IEnumerable<object>));

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(BorderedPicker), null, BindingMode.TwoWay);

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(BorderedPicker), default(string));

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(BorderedPicker), Colors.Gray);

        public static readonly BindableProperty FocusBorderColorProperty =
            BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(BorderedPicker), Colors.DeepSkyBlue);

        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(double), typeof(BorderedPicker), 1.0);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(BorderedPicker), 8f);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BorderedPicker), Colors.Black);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(BorderedPicker), Colors.Gray);
        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(BorderedPicker), Colors.Gray);

        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Color FocusBorderColor
        {
            get => (Color)GetValue(FocusBorderColorProperty);
            set => SetValue(FocusBorderColorProperty, value);
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
        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        private readonly BorderedPickerDrawable _drawable;
        private readonly Picker _picker;
        private readonly GraphicsView _graphicsView;

        public BorderedPicker()
        {
            _drawable = new BorderedPickerDrawable();

            _graphicsView = new GraphicsView
            {
                Drawable = _drawable
            };

            _picker = new Picker
            {
                BackgroundColor = Colors.Transparent,
                Margin = new Thickness(8),
                ItemDisplayBinding = new Binding("Name")
            };

            _picker.SetBinding(Picker.ItemsSourceProperty, new Binding(nameof(ItemsSource), source: this));
            _picker.SetBinding(Picker.SelectedItemProperty, new Binding(nameof(SelectedItem), source: this, mode: BindingMode.TwoWay));
            _picker.SetBinding(Picker.TitleProperty, new Binding(nameof(Title), source: this));
            _picker.SetBinding(Picker.TextColorProperty, new Binding(nameof(TextColor), source: this));
            _picker.SetBinding(Picker.TitleColorProperty, new Binding(nameof(TitleColor), source: this));

            _picker.Focused += (s, e) =>
            {
                _drawable.IsFocused = true;
                _graphicsView.Invalidate();
            };

            _picker.Unfocused += (s, e) =>
            {
                _drawable.IsFocused = false;
                _graphicsView.Invalidate();
            };

            var layout = new Grid();
            layout.Children.Add(_graphicsView);
            layout.Children.Add(_picker);

            Content = layout;

        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BorderColorProperty.PropertyName)
                _drawable.BorderColor = BorderColor;
            else if (propertyName == FocusBorderColorProperty.PropertyName)
                _drawable.FocusBorderColor = FocusBorderColor;
            else if (propertyName == BorderThicknessProperty.PropertyName)
                _drawable.BorderThickness = (float)BorderThickness;
            else if (propertyName == CornerRadiusProperty.PropertyName)
                _drawable.CornerRadius = CornerRadius;

            _graphicsView?.Invalidate();
        }
        public class BorderedPickerDrawable : IDrawable
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
