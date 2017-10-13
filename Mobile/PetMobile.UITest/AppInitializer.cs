using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace PetMobile.UITest
{
    public class AppInitializer
    {
        const string apkFilePath = @"D:\Projects\Pet Project\Mobile\PetMobile.Android\bin\Debug\PetMobile.Android-Signed.apk";

        private static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppInitializer.App' not set. Call 'AppInitializer.StartApp(platform)' before trying to access it.");
                return app;
            }
        }

        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                app = ConfigureApp
                        .Android
                        .Debug()
                        .ApkFile(apkFilePath)
                        .StartApp();
            }
            else
            {
                app = ConfigureApp
                        .iOS
                        .StartApp();
            }
            return app;
        }
    }
}

