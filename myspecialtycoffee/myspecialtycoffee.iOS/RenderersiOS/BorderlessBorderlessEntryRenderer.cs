﻿using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using myspecialtycoffee.iOS.RenderersiOS;
using myspecialtycoffee.Renderer;

namespace myspecialtycoffee.iOS.RenderersiOS
{
    public class BorderlessBorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null) return;
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;

        }
    }
}