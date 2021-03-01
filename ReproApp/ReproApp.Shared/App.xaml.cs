using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace ReproApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            ConfigureFilters(global::Uno.Extensions.LogExtensionPoint.AmbientLoggerFactory);

            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

#if NET5_0 && WINDOWS
            var window = new Window();
            window.Activate();
#else
            var window = Microsoft.UI.Xaml.Window.Current;
#endif

            var rootFrame = window.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.UWPLaunchActivatedEventArgs.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                window.Content = rootFrame;
            }

#if !(NET5_0 && WINDOWS)
            if (e.UWPLaunchActivatedEventArgs.PrelaunchActivated == false)
#endif
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                window.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }


        /// <summary>
        /// Configures global logging
        /// </summary>
        /// <param name="factory"></param>
        static void ConfigureFilters(ILoggerFactory factory)
        {
            factory
                .WithFilter(new FilterLoggerSettings
                    {
                        { "Uno", LogLevel.Warning },

                        { "Windows", LogLevel.Warning },
                        { "Microsoft", LogLevel.Warning },

                        // Debug JS interop
                        // { "Uno.Foundation.WebAssemblyRuntime", LogLevel.Debug },

                        // Generic Xaml events
                        // { "Microsoft.UI.Xaml", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.VisualStateGroup", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.StateTriggerBase", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.UIElement", LogLevel.Debug },

                        // Layouter specific messages
                        // { "Microsoft.UI.Xaml.Controls", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.Controls.Layouter", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.Controls.Panel", LogLevel.Debug },
                        // { "Windows.Storage", LogLevel.Debug },

                        // Binding related messages
                        // { "Microsoft.UI.Xaml.Data", LogLevel.Debug },

                        // DependencyObject memory references tracking
                        // { "ReferenceHolder", LogLevel.Debug },

                        // ListView-related messages
                        // { "Microsoft.UI.Xaml.Controls.ListViewBase", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.Controls.ListView", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.Controls.GridView", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.Controls.VirtualizingPanelLayout", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.Controls.NativeListViewBase", LogLevel.Debug },
                        // { "Microsoft.UI.Xaml.Controls.ListViewBaseSource", LogLevel.Debug }, //iOS
                        // { "Microsoft.UI.Xaml.Controls.ListViewBaseInternalContainer", LogLevel.Debug }, //iOS
                        // { "Microsoft.UI.Xaml.Controls.NativeListViewBaseAdapter", LogLevel.Debug }, //Android
                        // { "Microsoft.UI.Xaml.Controls.BufferViewCache", LogLevel.Debug }, //Android
                        // { "Microsoft.UI.Xaml.Controls.VirtualizingPanelGenerator", LogLevel.Debug }, //WASM
                    }
                )
#if DEBUG
                .AddConsole(LogLevel.Debug);
#else
                .AddConsole(LogLevel.Information);
#endif
        }
    }
}
