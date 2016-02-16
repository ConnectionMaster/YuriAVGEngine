﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.IO;

using Lyyneheym.LyyneheymCore.Utils;
using Lyyneheym.LyyneheymCore.SlyviaCore;
using Lyyneheym.LyyneheymCore.ILPackage;
using Lyyneheym.LyyneheymCore;
using Lyyneheym.SlyviaInterpreter;


namespace Lyyneheym
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Slyvia core = Slyvia.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
            this.testFontEffect(this.BO_MainText);
            core.SetMainWindow(this);
            //this.window.BO_MainGrid.Width = this.window.Width = 1900;
            //this.window.BO_MainGrid.Height = this.window.Height = 1000;

            //SolidColorBrush scb = new SolidColorBrush(Colors.Red);
            //this.BO_MainText.Foreground = scb;
            //timer.Interval = TimeSpan.FromMilliseconds(1000);
            //timer.Tick += timer_Tick;


            //myBitmapImage = new BitmapImage();
            //myBitmapImage.BeginInit();
            //myBitmapImage.UriSource = new Uri(@"PictureAssets\pictures\MenuItems2.png", UriKind.RelativeOrAbsolute);
            //myBitmapImage.SourceRect = new Int32Rect(187, 2, 226, 226);
            //myBitmapImage.EndInit();
            //mytestbutton.Width = myBitmapImage.PixelWidth;
            //mytestbutton.Height = myBitmapImage.PixelHeight;
            //mytestbutton.Source = myBitmapImage;
            //mytestbutton.Margin = new Thickness(0, 0, 0, 0);
            //mytestbutton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //mytestbutton.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    Musician.getInstance().Update();
        //}

        public void DoEvent()
        {
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.Background);
        }

        bool flag = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {

                this.BO_MainGrid.Background = new ImageBrush(core.testBitmapImage("bg1.png").myImage);
                flag = true;
            }
            else
            {
                this.BO_MainGrid.Background = new ImageBrush(core.testBitmapImage("bg2.png").myImage);
                flag = false;
            }
        //    MessageBox.Show(RuntimeManager.KS_MOUSE_RIGHT.ToString());
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

        MySprite leftChara;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //BitmapImage myBitmapImage = new BitmapImage();
            //myBitmapImage.BeginInit();
            //myBitmapImage.UriSource = new Uri(@"PictureAssets\character\CA01.png", UriKind.RelativeOrAbsolute);
            //myBitmapImage.EndInit();
            leftChara = core.testCharaStand("CA01.png");
            BitmapImage myBitmapImage = leftChara.myImage;
            leftChara.displayBinding = this.BO_LeftChara;
            this.BO_LeftChara.Width = myBitmapImage.PixelWidth;
            this.BO_LeftChara.Height = myBitmapImage.PixelHeight;
            this.BO_LeftChara.Source = myBitmapImage;
        }

        MySprite rightChara;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //BitmapImage myBitmapImage = new BitmapImage();
            //myBitmapImage.BeginInit();
            //myBitmapImage.UriSource = new Uri(@"PictureAssets\character\CA02.png", UriKind.RelativeOrAbsolute);
            //myBitmapImage.EndInit();
            rightChara = core.testCharaStand("CA02.png");
            BitmapImage myBitmapImage = rightChara.myImage;
            rightChara.displayBinding = this.BO_RightChara;
            this.BO_RightChara.Width = myBitmapImage.PixelWidth;
            this.BO_RightChara.Height = myBitmapImage.PixelHeight;
            this.BO_RightChara.Source = myBitmapImage;
        }

        bool flag2 = false;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (flag2 == false)
            {
                Panel.SetZIndex(this.BO_MessageBoxLayer, 91);
                flag2 = true;
            }
            else
            {
                Panel.SetZIndex(this.BO_MessageBoxLayer, -1);
                flag2 = false;
            }
            
        }

        bool pauseFlag = false;

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Image img = new Image();
            ////BitmapImage myBitmapImage = new BitmapImage();
            ////myBitmapImage.BeginInit();
            ////myBitmapImage.UriSource = new Uri(@"PictureAssets\pictures\uuz.jpg", UriKind.RelativeOrAbsolute);
            ////myBitmapImage.EndInit();

            //MySprite msp = ResourceManager.GetInstance().GetPicture("uuz.jpg");
            //BitmapImage myBitmapImage = msp.myImage;
            //msp.displayBinding = img;
            //img.Width = myBitmapImage.PixelWidth;
            //img.Height = myBitmapImage.PixelHeight;
            //img.Source = myBitmapImage;
            ////img.Margin = new Thickness(0, 0, 0, 0);
            ////img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ////img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            ////img.Name = "UBO_PIC_01";
            //this.BO_MainGrid.Children.Add(img);

            MySprite sprite = ResourceManager.GetInstance().GetCharacterStand("穹_校服普通.png", new Int32Rect(-1, 0, 0, 0));

            Image spriteImage = new Image();
            BitmapImage bmp = sprite.myImage;

            spriteImage.Width = bmp.PixelWidth;
            spriteImage.Height = bmp.PixelHeight;
            spriteImage.Source = bmp;

            //spriteImage.Opacity = descriptor.Opacity;
            sprite.displayBinding = spriteImage;
            //sprite.anchor = descriptor.anchorType;
            //Canvas.SetLeft(spriteImage, 10);
            //Canvas.SetTop(spriteImage, 30);
            //Canvas.SetZIndex(spriteImage, 999);
            //SpriteAnimation.RotateAnimation(sprite, TimeSpan.FromMilliseconds(0), descriptor.Angle, 0);
            spriteImage.Visibility = Visibility.Visible;
            this.BO_MainGrid.Children.Add(spriteImage);
            sprite.InitAnimationRenderTransform();
            sprite.anchor = SpriteAnchorType.Center;
            SpriteAnimation.ScaleAnimation(sprite, TimeSpan.FromSeconds(1), 0.3, 0.3, 0, 0);
            SpriteAnimation.RotateAnimation(sprite, TimeSpan.FromSeconds(2), 180, 1);
            


            //if (!pauseFlag)
            //{
            //    Musician.GetInstance().PauseBGM();
            //}
            //else
            //{
            //    Musician.GetInstance().ResumeBGM();
            //}
            //pauseFlag = !pauseFlag;
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //foreach (UIElement c in this.BO_MainGrid.Children)
            //{
            //    if (c is Image)
            //    {
            //        Image obc = (Image)c;
            //        if (obc.Name == "UBO_PIC_01")
            //        {
            //            obc.Visibility = obc.Visibility == System.Windows.Visibility.Collapsed ?
            //                System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            //            this.BO_MainGrid.Children.Remove(c);
            //            break;
            //        }
            //    }
            //}
            //rightChara = core.testCharaStand("CA02.png");
            //BitmapImage myBitmapImage = rightChara.myImage;
            //this.BO_LeftChara.Width = myBitmapImage.PixelWidth;
            //this.BO_LeftChara.Height = myBitmapImage.PixelHeight;
            //this.BO_LeftChara.Source = myBitmapImage;
            SpriteAnimation.SkipAnimation(this.rightChara);
        }


        string preStr = String.Empty;
        string desStr = String.Empty;

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string pstr = "测试文本测试文本测试文本测试文本" + Environment.NewLine + "233333 here is new line without pause" + Environment.NewLine
                + "\\|" + "Here third line, with pause" + "\\|" + Environment.NewLine + "444666888";
            string[] strRun = pstr.Split(new string[] {"\\|"}, StringSplitOptions.None);
            foreach (var s in strRun) { this.strRuns.Enqueue(s); }
            
            //for (int i = 0; i < strRun.Length; i++)
            //{
                
            //    DateTime beginTime = DateTime.Now;
            //    TimeSpan ts = TimeSpan.FromMilliseconds(1000.0 / 60.0);

            //    while (clickFlag == false)
            //    {
            //        if (DateTime.Now - beginTime > ts)
            //        {
            //            this.DoEvent();
            //            beginTime = DateTime.Now;
            //        }
            //    }

            //    clickFlag = false;

                
            //}

        }

        private Queue<string> strRuns = new Queue<string>();

        private bool clickFlag = false;

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            //TypewriteTextblock("测试文本测试文本测试文本测试文本", Environment.NewLine + "2333333", this.BO_MainText, 30);
            clickFlag = true;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Interpreter ip = new Interpreter("TestProj", @"C:\Users\Kako\Desktop\testDir");
            ip.Dash(InterpreterType.RELEASE_WITH_IL, 8);
            ip.GetILFile(@"Scenario\main.sil");

            ILConvertor ilc = ILConvertor.GetInstance();
            List<Scene> rS = ilc.Dash(@"Scenario");
        }




        private void Button_Click_8(object sender, RoutedEventArgs e)
        {

            Musician m = Musician.GetInstance();
            m.PlayBGM(@"Boss01.wav", @"Sound\bgm\Boss01.wav", 1000);
            //timer.Start();
            
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            core.testBGM("车椅子の未来宇宙.mp3");
            //core.testBGM("Boss01.wav");
            
            //timer.Start();
        }



        private void callback_typing(object sender, EventArgs e)
        {
            this.BO_MsgTria.Visibility = Visibility.Visible;
            this.BO_MsgTria.RenderTransform = new TranslateTransform();
            this.ApplyUpDownAnimation(this.BO_MsgTria.Name);
        }
        



        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //mp.Stop();
            //System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"Sound\se\se01.wav");
            //sp.Play();
            core.testVocal("Alice002.mp3");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {

            //timer.Stop();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Point p = e.MouseDevice.GetPosition((Image)sender);
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"PictureAssets\pictures\MenuItems2.png", UriKind.RelativeOrAbsolute);
            myBitmapImage.SourceRect = new Int32Rect(187, 2, 226, 226);
            myBitmapImage.EndInit();

            Color hitC = this.GetPixelColor(myBitmapImage, (int)p.X, (int)p.Y);

            if (hitC.A > 10)
            {
                MessageBox.Show(p.ToString() + " == " + hitC.ToString());
            }
            
        }

        public Color GetPixelColor(BitmapSource source, int x, int y)
        {
            Color c = Colors.White;
            if (source != null)
            {
                try
                {
                    CroppedBitmap cb = new CroppedBitmap(source, new Int32Rect(x, y, 1, 1));
                    var pixels = new byte[4];
                    cb.CopyPixels(pixels, 4, 0);
                    c = Color.FromArgb(pixels[3] ,pixels[2], pixels[1], pixels[0]);
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            return c;
        }

        bool aniInit = false;
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            //this.mytestbutton.RenderTransform = new TranslateTransform();
            if (aniInit == false)
            {
                TransformGroup transformGroup = new TransformGroup();
                TranslateTransform tt = new TranslateTransform();
                //tt.Y = 50;
                transformGroup.Children.Add(tt);

                ScaleTransform sc = new ScaleTransform();
                //sc.ScaleX = 1.5;
                //sc.ScaleY = 1.5;
                transformGroup.Children.Add(sc);

                RotateTransform rore = new RotateTransform();
                rore.CenterX = this.mytestbutton.Width / 2;
                rore.CenterY = this.mytestbutton.Height / 2;
                transformGroup.Children.Add(rore);


                //transformGroup.Children.Add(new TranslateTransform());
                //transformGroup.Children.Add(new RotateTransform());

                //this.mytestbutton.RenderTransform = transformGroup;
                this.mytestbutton.RenderTransform = transformGroup;
                //this.ApplyUpDownAnimation(this.mytestbutton.Name);

                aniInit = true;
            }


            RotateTransform rtt = ((TransformGroup)(this.mytestbutton.RenderTransform)).Children[2] as RotateTransform;

            double ang = rtt.Angle;
            

            this.testAni(this.mytestbutton, Canvas.GetTop(this.mytestbutton), Canvas.GetTop(this.mytestbutton) + 50, ang, ang + 90);
            
        }

        private void testAni(DependencyObject icCurrent, double from, double to, double st, double et)
        {

            Storyboard story = new Storyboard();

            DoubleAnimation da = new DoubleAnimation(from, to, TimeSpan.FromSeconds(1));
            da.AccelerationRatio = 0.8;

            DoubleAnimation rora = new DoubleAnimation(1, 1, TimeSpan.FromSeconds(1));
            DoubleAnimation rora2 = new DoubleAnimation(1, 1, TimeSpan.FromSeconds(1));
            DoubleAnimation rore = new DoubleAnimation(st, et, TimeSpan.FromSeconds(1));
            DoubleAnimation ap = new DoubleAnimation(1, 0.5, TimeSpan.FromSeconds(1));


            Storyboard.SetTarget(da, icCurrent);
            Storyboard.SetTarget(rora, icCurrent);
            Storyboard.SetTarget(rora2, icCurrent);
            Storyboard.SetTarget(rore, icCurrent);
            Storyboard.SetTarget(ap, icCurrent);
            
            Storyboard.SetTargetProperty(da, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTargetProperty(rora, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(rora2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleY)"));
            Storyboard.SetTargetProperty(rore, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
            Storyboard.SetTargetProperty(ap, new PropertyPath(Image.OpacityProperty));

            //DependencyProperty[] propertyChain = new DependencyProperty[]
            //{
            //    Button.RenderTransformProperty,
            //    TranslateTransform.XProperty
            //};

            //Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)", propertyChain));


            story.Children.Add(da);
            story.Children.Add(rora);
            story.Children.Add(rora2);
            story.Children.Add(rore);
            story.Children.Add(ap);

            story.Begin(this);
        }

        private void ApplyUpDownAnimation(string dependence)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(0, 10, new Duration(TimeSpan.FromMilliseconds(500)));
            da.RepeatBehavior = RepeatBehavior.Forever;
            da.AutoReverse = true;
            da.AccelerationRatio = 0.8;
            Storyboard.SetTargetName(da, dependence);
            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                Image.RenderTransformProperty,
                TranslateTransform.YProperty,
            };
            Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)", propertyChain));
            sb.Children.Add(da);

            
            sb.Begin(this);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            SpriteAnimation.XYMoveAnimation(this.rightChara, TimeSpan.FromSeconds(3), -370, 0, 0.8);
            SpriteAnimation.OpacityAnimation(this.rightChara, TimeSpan.FromSeconds(10), -0.7);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            SpriteAnimation.XYMoveAnimation(this.leftChara, TimeSpan.FromSeconds(0.5), 30, 0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            core.DisposeResource();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Musician.GetInstance().isBGMPlaying)
            {
                Musician.GetInstance().SetBGMVolume((float)e.NewValue);
            }
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Musician.GetInstance().isBGMPlaying)
            {
                Musician.GetInstance().SetBGMStereo((float)e.NewValue);
            }
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

        private void BO_LeftChara_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void BO_LeftChara_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void BO_LeftChara_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void BO_LeftChara_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        




    }
}
