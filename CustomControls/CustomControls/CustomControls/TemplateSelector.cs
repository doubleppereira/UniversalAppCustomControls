using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomControls
{
    public abstract class TemplateSelector : ContentControl
    {
        public TemplateSelector()
        {
        }

        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
       
    }
}
