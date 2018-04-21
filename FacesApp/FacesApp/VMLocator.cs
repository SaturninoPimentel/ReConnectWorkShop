using Autofac;
using FacesApp.Services.Connectivity;
using FacesApp.Services.Media;
using FacesApp.Services.Navigation;
using FacesApp.Services.People;
using FacesApp.Services.Storage;
using FacesApp.ViewModels;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace FacesApp
{
    public static class VmLocator
    {
        private static readonly IContainer Container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(VmLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        static VmLocator()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<MainViewModel>();
            containerBuilder.RegisterType<NewImageViewModel>();
            containerBuilder.RegisterType<NavigationService>()
                .As<INavigationServices>();
            containerBuilder.RegisterType<MediaService>()
                .As<IMediaService>();
            containerBuilder.RegisterType<PeopleService>()
                .As<IPeopleService>();
            containerBuilder.RegisterType<StorageService>()
                .As<IStorageService>();
            containerBuilder.RegisterType<ConnectivityService>()
                .As<IConnectivityService>();
            Container = containerBuilder.Build();
        }

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        public static T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
            {
                return;
            }

            Type viewType = view.GetType();
            string viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            string viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            Type viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            object viewModel = Container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}