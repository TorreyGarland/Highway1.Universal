namespace Highway1.Universal.UI.Controls
{

    using System;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>Uniform grid class.</summary>
    public sealed class UniformGrid : Panel
    {

        #region Fields

        private int _columns;

        private int _rows;

        #endregion

        #region Properties

        /// <summary>Gets or sets the columns.</summary>
        /// <value>The columns.</value>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>Gets the columns property.</summary>
        /// <value>The columns property.</value>
        public static DependencyProperty ColumnsProperty { get; }
            = DependencyProperty.Register(nameof(Columns), typeof(int), typeof(UniformGrid), new PropertyMetadata(0, OnPropertyChanged));

        /// <summary>Gets or sets the first column.</summary>
        /// <value>The first column.</value>
        public int FirstColumn
        {
            get { return (int)GetValue(FirstColumnProperty); }
            set { SetValue(FirstColumnProperty, value); }
        }

        /// <summary>Gets the first column property.</summary>
        /// <value>The first column property.</value>
        public static DependencyProperty FirstColumnProperty { get; }
            = DependencyProperty.Register(nameof(FirstColumn), typeof(int), typeof(UniformGrid), new PropertyMetadata(0, OnPropertyChanged));

        /// <summary>Gets or sets the rows.</summary>
        /// <value>The rows.</value>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>Gets the rows property.</summary>
        /// <value>The rows property.</value>
        public static DependencyProperty RowsProperty { get; }
            = DependencyProperty.Register(nameof(Rows), typeof(int), typeof(UniformGrid), new PropertyMetadata(0, OnPropertyChanged));

        #endregion

        #region Methods

        /// <summary>Arranges the override.</summary>
        /// <param name="finalSize">The final size.</param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var childBounds = new Rect(0, 0, finalSize.Width / _columns, finalSize.Height / _rows);
            var step = childBounds.Width;
            var bound = finalSize.Width - 1.0;
            childBounds.X += childBounds.Width * FirstColumn;
            foreach (var child in Children)
            {
                child.Arrange(childBounds);
                if (child.Visibility != Visibility.Collapsed)
                {
                    childBounds.X += step;
                    if (childBounds.X > bound)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
            }
            return finalSize;
        }

        /// <summary>Measures the override.</summary>
        /// <param name="availableSize">Size of the available.</param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            UpdateValues();
            var childConstraint = new Size(availableSize.Width / _columns, availableSize.Height / _rows);
            var maxChildDesiredWidth = 0d;
            var maxChildDesiredHeight = 0d;
            for (int i = 0, count = Children.Count; i < count; ++i)
            {
                var child = Children[i];
                child.Measure(childConstraint);
                var childDesiredSize = child.DesiredSize;
                if (maxChildDesiredWidth < childDesiredSize.Width)
                    maxChildDesiredWidth = childDesiredSize.Width;
                if (maxChildDesiredHeight < childDesiredSize.Height)
                    maxChildDesiredHeight = childDesiredSize.Height;
            }
            return new Size((maxChildDesiredWidth * _columns), (maxChildDesiredHeight * _rows));
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is int) || (int)e.NewValue < 0)
                d.SetValue(e.Property, e.OldValue);
            else
                ((UniformGrid)d).InvalidateMeasure();
        }

        private void UpdateValues()
        {
            _columns = Columns;
            _rows = Rows;
            if (FirstColumn >= _columns)
                FirstColumn = 0;
            if ((_rows == 0) || (_columns == 0))
            {
                var nonCollapsedCount = 0;
                for (int i = 0, count = Children.Count; i < count; ++i)
                {
                    var child = Children[i];
                    if (child.Visibility != Visibility.Collapsed)
                        nonCollapsedCount++;
                }
                if (nonCollapsedCount == 0)
                    nonCollapsedCount = 1;
                if (_rows == 0)
                {
                    if (_columns > 0)
                        _rows = (nonCollapsedCount + FirstColumn + (_columns - 1)) / _columns;
                    else
                    {
                        _rows = (int)Math.Sqrt(nonCollapsedCount);
                        if ((_rows * _rows) < nonCollapsedCount)
                            _rows++;
                        _columns = _rows;
                    }
                }
                else if (_columns == 0)
                    _columns = (nonCollapsedCount + (_rows - 1)) / _rows;
            }
        }

        #endregion

    }

}