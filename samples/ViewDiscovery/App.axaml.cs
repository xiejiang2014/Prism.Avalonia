using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using ViewDiscovery.Views;

namespace ViewDiscovery;

public class App : PrismApplication  //关键点1 App 从 PrismApplication 继承
{
    public App()
    {
        System.Diagnostics.Debug.WriteLine("App.Constructor()");
    }

    public override void Initialize()
    {
        System.Diagnostics.Debug.WriteLine("Initialize()");
        AvaloniaXamlLoader.Load(this);

        base.Initialize(); //关键点10 执行 PrismApplication.Initialize() 方法

    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        System.Diagnostics.Debug.WriteLine("RegisterTypes(...)");
        // Wire-up services and navigation Views here.
    }

    /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
    /// <returns>Startup View.</returns>
    protected override AvaloniaObject CreateShell()
    {
        System.Diagnostics.Debug.WriteLine("CreateShell()");
        return Container.Resolve<MainWindow>();
    }

    protected override void OnInitialized()
    {
        System.Diagnostics.Debug.WriteLine("OnInitialized()");

        //替代方法：
        //您可以通过从容器中解析“IRegionManager”，在 OnInitialized()、CreateShell() 或 RegisterTypes(..) 中注册视图，
        //而不是在 MainWindow 中注册视图（如本示例所示）。
        //var regionManager = Container.Resolve<IRegionManager>();
        //regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewA));
        //regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewB));
    }
}
