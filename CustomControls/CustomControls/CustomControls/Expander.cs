using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace CustomControls
{
    [TemplateVisualState(Name = "Collapsed", GroupName = "ViewStates")]
    [TemplateVisualState(Name = "Expanded", GroupName = "ViewStates")]
    [TemplatePart(Name = "Content", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "ExpandCollapseButton", Type = typeof(Button))]
    public class Expander : ContentControl
    {
        private bool _useTransitions = true;
        private VisualState _collapsedState;
        private Button _toggleExpander;
        private FrameworkElement _contentElement;

        public static readonly DependencyProperty HeaderContentProperty =
        DependencyProperty.Register("HeaderContent", typeof(object),
        typeof(Expander), null);

        public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register("IsExpanded", typeof(bool),
        typeof(Expander), new PropertyMetadata(true));

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius),
        typeof(Expander), null);

         public static readonly DependencyProperty ArrowBackgroundProperty =
            DependencyProperty.Register("ArrowBackground", typeof(Brush), typeof(Expander), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));
       
        public Brush ArrowBackground
        {
            get { return (Brush)GetValue(ArrowBackgroundProperty); }
            set { SetValue(ArrowBackgroundProperty, value); }
        }

        public object HeaderContent
        {
            get { return GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public Expander()
        {
            DefaultStyleKey = typeof(Expander);
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (IsExpanded)
            {
                if (_contentElement != null)
                {
                    _contentElement.Visibility = Visibility.Visible;
                }
                VisualStateManager.GoToState(this, "Expanded", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Collapsed", useTransitions);
                _collapsedState = (VisualState)GetTemplateChild("Collapsed");
                if (_collapsedState == null)
                {
                    if (_contentElement != null)
                    {
                        _contentElement.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            IsExpanded = !IsExpanded;
            //_toggleExpander.IsChecked = IsExpanded;
            ChangeVisualState(_useTransitions);
        }

        private void Collapsed_Completed(object sender, EventArgs e)
        {
            _contentElement.Visibility = Visibility.Collapsed;
        }
        
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _toggleExpander = (Button)GetTemplateChild("ExpandCollapseButton");
            if (_toggleExpander != null)
            {
                _toggleExpander.Tapped += Toggle_Click;
            }
            _contentElement = (FrameworkElement)GetTemplateChild("Content");
            if (_contentElement != null)
            {
                _collapsedState = (VisualState)GetTemplateChild("Collapsed");
                if ((_collapsedState != null) && (_collapsedState.Storyboard != null))
                {
                    _collapsedState.Storyboard.Completed += Storyboard_Completed;
                }
            }
            ChangeVisualState(false);
        }

        void Storyboard_Completed(object sender, object e)
        {
            _contentElement.Visibility = Visibility.Collapsed;
        }

    }
}
