﻿using Aura.CommonCore;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Aura.UI.UIExtensions;
using Aura.UI.Controls;
using AuraDraw.App.Windows;
using Avalonia.Media;
using System.Threading.Tasks;
using System.Threading;
using Avalonia.Controls.Primitives;
using AuraDraw.App.Functions;
using AuraDraw.App.Views;

namespace AuraDraw.App
{
    public class AppData 
    {

        public AppData()
        {
            
        }
        
        public static void SetMainWorkSpace(TabControl tabControl)
        {
            AppData.MainWorkSpace = tabControl;
            tabControl.SelectionChanged += TabControl_SelectionChanged;    
        }

        private static void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((MainWorkSpace.SelectedItem as AuraTabItem).Content != null & ((MainWorkSpace.SelectedItem as AuraTabItem).Content as DocumentView) != null)
            {
                AppData.CurrentDocument = ((MainWorkSpace.SelectedItem as AuraTabItem).Content as DocumentView).DocumentInUse;
            }
        }

        public static TabControl MainWorkSpace 
        {
            get; 
            set; 
        }

        public static Document CurrentDocument { get; set; }

        public static TemplatedControl SelectedObject { get; protected set; }

        public  static Brush SelectedColor { get; protected set; }
        public static void NewDocumentOnWorkSpace(Window OwnerWindow)
        {
            var newdocwin = new NewDocumentWindow();
            newdocwin.ShowDialog(OwnerWindow);
        }

        public async static Task SetGlobalColor(Brush color)
        {
            var thread_sc = new Thread(() => AppData.SetGlobalColorSync(color));
            await Task.Run(thread_sc.Start);  

        }

        public static void SetGlobalColorSync(Brush color) => AppData.SelectedColor = color;

        public static Brush GetGlobalColor()
        {
            return AppData.SelectedColor;
        }

        public static Control GetSelectedControl() => AppData.SelectedObject;

        public async static Task SetSelectedControl(TemplatedControl control)
        {
            var th_sc = new Thread(() => AppData.SetSelectedControlSync(control));
            await Task.Run(th_sc.Start);
        }
        public static void SetSelectedControlSync(TemplatedControl control) => AppData.SelectedObject = control;

        
    }

}
