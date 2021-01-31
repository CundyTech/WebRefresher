using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace WebRefresher.Services
{
    /// <summary>
    /// https://damienaicheh.github.io/xamarin/xamarin.forms/2019/05/17/how-to-add-command-to-a-switch-in-xamarin-forms-en.html
    /// </summary>
    public class SwitchBehaviour : Behavior<Switch> 
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SwitchBehaviour), null);
        public Switch Bindable { get; private set; }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(Switch bindable)
        {
            base.OnAttachedTo(bindable);
            Bindable = bindable;
            Bindable.BindingContextChanged += OnBindingContextChanged;
            Bindable.Toggled += OnSwitchToggled;
        }

        protected override void OnDetachingFrom(Switch bindable)
        {
            base.OnDetachingFrom(bindable);
            Bindable.BindingContextChanged -= OnBindingContextChanged;
            Bindable.Toggled -= OnSwitchToggled;
            Bindable = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
            BindingContext = Bindable.BindingContext;
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            Command?.Execute(e.Value);
        }
    }
}
