﻿
using Android.App;
using Android.Content;
using Android.Views;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using AView = Android.Views.View;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.Reflection;
using Android.Util;
using PetMobile.Views;
using PetMobile.Droid.Renderers;

[assembly: ExportRenderer(typeof(TransparentNavigationPage), typeof(CustomNavigationBarRenderer))]
namespace PetMobile.Droid.Renderers
{
    public class CustomNavigationBarRenderer : NavigationPageRenderer
    {
        private AToolbar _toolbar;

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            var memberInfo = typeof(CustomNavigationBarRenderer).BaseType;
            if (memberInfo != null)
            {
                var field = memberInfo.GetField(nameof(_toolbar), BindingFlags.Instance | BindingFlags.NonPublic);
                _toolbar = field.GetValue(this) as AToolbar;
                _toolbar.SetBackgroundColor(Color.Transparent.ToAndroid());
            }
        }

        IPageController PageController => Element;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            var navigationPage = Element as NavigationPage;

            if (navigationPage.BarBackgroundColor != Color.Transparent)
            {
                if (navigationPage.CurrentPage.GetType() != typeof(PetProfilePage))
                {
                    base.OnLayout(changed, l, t + ActionBarHeight(), r, b + ActionBarHeight());
                }
            }
            else
            {
                base.OnLayout(changed, l, t, r, b + ActionBarHeight());
            }

            AToolbar bar = _toolbar;
            bar.BringToFront();

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                var pageContainer = child.GetType().GetProperty("Child")?.GetValue(child) as IVisualElementRenderer;
                Page childPage = pageContainer?.Element as Page;

                if (childPage == null)
                    return;

                bool childHasNavBar = NavigationPage.GetHasNavigationBar(childPage);

                if (childHasNavBar)
                {
                    if (navigationPage.BarBackgroundColor != Color.Transparent)
                    {
                        if (navigationPage.CurrentPage.GetType() != typeof(PetProfilePage))
                        {
                            child.Layout(0, ActionBarHeight(), r, b + ActionBarHeight());
                        }
                    }
                    else
                    {
                        child.Layout(0, 0, r, b + ActionBarHeight());
                    }
                }
            }
        }

        private int ActionBarHeight()
        {
            int attr = Resource.Attribute.actionBarSize;

            int actionBarHeight;
            using (var tv = new TypedValue())
            {
                actionBarHeight = 0;
                if (Context.Theme.ResolveAttribute(attr, tv, true))
                    actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, Resources.DisplayMetrics);
            }

            return actionBarHeight;
        }
    }
}