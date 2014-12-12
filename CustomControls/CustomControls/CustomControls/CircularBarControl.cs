using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace CustomControls
{
    [TemplatePart(Name = "progressBar", Type = typeof(ProgressBar))]
    [TemplatePart(Name = "ContentContainer", Type = typeof(ContentControl))]
    public class CircularBarControl : ContentControl
    {
        private bool loaded = false;
        private Storyboard story;
        private ProgressBar progressBar;
        private ContentControl contentContainer;


        public CircularBarControl()
        {
            DefaultStyleKey = typeof(CircularBarControl);
            Loaded += CircularBarControl_Loaded;
        }

        void CircularBarControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.loaded = true;

            this.story = new Storyboard();

            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            animation.From = 0;
            animation.To = this.Value >= 100 ? 99.99 : this.Value;
            Storyboard.SetTarget(animation, this.progressBar);

            animation.EnableDependentAnimation = true;
            Storyboard.SetTargetProperty(animation, "(RangeBase.Value)");
            
            this.story.Children.Add(animation);

            this.story.Begin();

            var grid = this.progressBar.Descendants<Grid>().First();

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("InnerDiameter");

            myBinding.Source = ((Grid)grid).DataContext;
            contentContainer.SetBinding(FrameworkElement.HeightProperty, myBinding);
            contentContainer.SetBinding(FrameworkElement.WidthProperty, myBinding);
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            progressBar = (ProgressBar)GetTemplateChild("progressBar");
            contentContainer = (ContentControl)GetTemplateChild("ContentContainer");
        }

        #region Properties

        /// <summary>
        /// The value of this control
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(CircularBarControl), new PropertyMetadata(50.0, OnDependencyPropertyChanged));

        public Brush BarFill
        {
            get { return (Brush)GetValue(BarFillProperty); }
            set { SetValue(BarFillProperty, value); }
        }

        public static readonly DependencyProperty BarFillProperty = DependencyProperty.Register("BarFill", typeof(Brush), typeof(CircularBarControl), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

        private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularBarControl source = d as CircularBarControl;
            if (source.loaded)
            {
                source.story = new Storyboard();
                DoubleAnimation animation = new DoubleAnimation();
                animation.Duration = new Duration(TimeSpan.FromSeconds(1));
                animation.From = 0;
                animation.To = source.Value >= 100 ? 99.99 : source.Value;
                Storyboard.SetTarget(animation, source.progressBar);
                animation.EnableDependentAnimation = true;
                Storyboard.SetTargetProperty(animation, "(RangeBase.Value)");
                
                source.story.Children.Add(animation);
                source.story.Begin();
            }
        }
        #endregion
    }
}
