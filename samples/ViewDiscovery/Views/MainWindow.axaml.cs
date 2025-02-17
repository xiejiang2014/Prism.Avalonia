﻿using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Prism.Regions;

namespace ViewDiscovery.Views
{
    public partial class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;

        public MainWindow()
        {
        }

        // Issue Avalonia-v11-preview4:
        // In this example, ideally we want to use this constructor to register
        // the views with the Region. However, the 'ContentRegion' did not get registered yet.
        public MainWindow(IRegionManager regionManager)
        {
            this.InitializeComponent();
            this.AttachDevTools();

            //关键点 40 将视图注册到区域里面 
            _regionManager = regionManager;
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewA));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewB));

            // BREAKING CHANGE:
            //
            //  Due to changes in Avalonia v11-preview4(?) App.axaml.cs the MainWindow is
            //  being initialized before the RegionManager can register the "ContentRegion"
            //  The "Test()" method has been moved into "Show()" to give the system time to react.
            //
            // Test();
        }

        public override void Show()
        {
            base.Show();
            Test();
        }

        //关键点 50 
        private async void Test()
        {
            while (true)
            {
                //确认区域存在
                if (!_regionManager.Regions.ContainsRegionWithName(RegionNames.ContentRegion))
                {
                    // Catch error caused by framework changes
                    System.Diagnostics.Debugger.Break();
                    return;
                }

                //得到区域
                var region = _regionManager.Regions["ContentRegion"];

                //得到区域中已注册的视图(在关键点 40 注册的)
                var viewA  = region.Views.FirstOrDefault();
                var viewB  = region.Views.Skip(1).FirstOrDefault();

                //间隔2秒 切换视图

                await Task.Delay(2000);
                await Dispatcher.UIThread.InvokeAsync(() =>
                                                      {
                                                          // NOTE TO DEV:
                                                          //  If your view is not registered with the region
                                                          //  the following line will throw an error.
                                                          region.Activate(viewB);
                                                      });

                await Task.Delay(2000);
                await Dispatcher.UIThread.InvokeAsync(() => { region.Activate(viewA); });
            }
        }
    }
}
