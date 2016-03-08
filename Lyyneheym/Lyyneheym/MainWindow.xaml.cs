﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO;

using Transitionals;

using Yuri;
using Yuri.Utils;
using Yuri.PlatformCore;
using Yuri.ILPackage;
using Yuri.YuriInterpreter;


namespace Yuri
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Director core = Director.GetInstance();

        public ObjectDataProvider TransitionDS = new ObjectDataProvider();

        public MainWindow()
        {
            InitializeComponent();
            this.Width = this.BO_MainGrid.Width = GlobalDataContainer.GAME_WINDOW_WIDTH;
            this.Height = GlobalDataContainer.GAME_WINDOW_ACTUALHEIGHT;
            this.BO_MainGrid.Height = GlobalDataContainer.GAME_WINDOW_HEIGHT;
            this.Title = GlobalDataContainer.GAME_PROJECT_NAME;
            this.ResizeMode = GlobalDataContainer.GAME_WINDOW_RESIZEABLE ? System.Windows.ResizeMode.CanResize : System.Windows.ResizeMode.NoResize;
            core.SetMainWindow(this);
            this.TransitionBox.DataContext = this.TransitionDS;
        }


        public void DoEvent()
        {
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.Background);
        }


        private void testFontEffect(TextBlock label)
        {
            //LinearGradientBrush brush = new LinearGradientBrush();

            //GradientStop gradientStop1 = new GradientStop();
            //gradientStop1.Offset = 0;
            //gradientStop1.Color = Color.FromArgb(255, 251, 100, 17);
            //brush.GradientStops.Add(gradientStop1);

            //GradientStop gradientStop2 = new GradientStop();
            //gradientStop2.Offset = 1;
            //gradientStop2.Color = Color.FromArgb(255, 247, 238, 52);
            //brush.GradientStops.Add(gradientStop2);

            
            //brush.StartPoint = new Point(0.5, 0);
            //brush.EndPoint = new Point(0.5, 1);
            //label.Foreground = brush;
            System.Windows.Media.Effects.DropShadowEffect ds = new System.Windows.Media.Effects.DropShadowEffect();
            ds.ShadowDepth = 2;
            ds.Opacity = 0.5;
            label.Effect = ds;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Interpreter ip = new Interpreter("TestProj", @"C:\Users\Kako\Desktop\testDir");
            ip.Dash(InterpreterType.RELEASE_WITH_IL, 8);
            ip.GetILFile(@"Scenario\main.sil");

            ILConvertor ilc = ILConvertor.GetInstance();
            List<Scene> rS = ilc.Dash(@"Scenario");
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            core.DisposeResource();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            this.core.UpdateKeyboard(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            this.core.UpdateKeyboard(e);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.core.UpdateMouse(e);
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.core.UpdateMouse(e);
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.core.UpdateMouseWheel(e.Delta);
        }

    }
}
