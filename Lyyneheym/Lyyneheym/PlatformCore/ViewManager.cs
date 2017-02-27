﻿using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Transitionals;

using Yuri.Utils;

namespace Yuri.PlatformCore
{
    /// <summary>
    /// 视窗管理器：负责将画面管理器的内容渲染为前端视图
    /// </summary>
    internal class ViewManager
    {
        /// <summary>
        /// 工厂方法：获得唯一实例
        /// </summary>
        /// <returns>视窗管理器</returns>
        public static ViewManager GetInstance()
        {
            return ViewManager.synObject == null ? ViewManager.synObject = new ViewManager() : ViewManager.synObject;
        }

        /// <summary>
        /// 重绘整个画面
        /// </summary>
        public void ReDraw()
        {
            // 重绘精灵
            for (int i = 0; i < this.backgroundSpriteVec.Count; i++)
            {
                this.ReDrawSprite(i, this.backgroundSpriteVec, ResourceType.Background, Director.ScrMana.GetSpriteDescriptor(i, ResourceType.Background), false);
            }
            for (int i = 0; i < this.characterStandSpriteVec.Count; i++)
            {
                this.ReDrawSprite(i, this.characterStandSpriteVec, ResourceType.Stand, Director.ScrMana.GetSpriteDescriptor(i, ResourceType.Stand), false);
            }
            for (int i = 0; i < this.pictureSpriteVec.Count; i++)
            {
                this.ReDrawSprite(i, this.pictureSpriteVec, ResourceType.Pictures, Director.ScrMana.GetSpriteDescriptor(i, ResourceType.Pictures), false);
            }
            // 重绘文字层
            for (int i = 0; i < this.messageLayerVec.Count; i++)
            {
                this.ReDrawMessageLayer(i, Director.ScrMana.GetMsgLayerDescriptor(i), false);
            }
            // 重绘按钮
            for (int i = 0; i < this.buttonLayerVec.Count; i++)
            {
                this.ReDrawButton(i, Director.ScrMana.GetButtonDescriptor(i));
            }
            // 重绘选择项
            for (int i = 0; i < this.branchButtonVec.Count; i++)
            {
                this.ReDrawBranchButton(i, Director.ScrMana.GetBranchButtonDescriptor(i));
            }
        }

        /// <summary>
        /// 将描述子转化为相应前端项目并显示到前端
        /// </summary>
        /// <param name="id">控件id</param>
        /// <param name="rType">资源类型</param>
        public void Draw(int id, ResourceType rType)
        {
            switch (rType)
            {
                case ResourceType.Background:
                    this.ReDrawSprite(id, this.backgroundSpriteVec, ResourceType.Background, Director.ScrMana.GetSpriteDescriptor(id, ResourceType.Background), true);
                    break;
                case ResourceType.Stand:
                    this.ReDrawSprite(id, this.characterStandSpriteVec, ResourceType.Stand, Director.ScrMana.GetSpriteDescriptor(id, ResourceType.Stand), true);
                    break;
                case ResourceType.Pictures:
                    this.ReDrawSprite(id, this.pictureSpriteVec, ResourceType.Pictures, Director.ScrMana.GetSpriteDescriptor(id, ResourceType.Pictures), true);
                    break;
                case ResourceType.MessageLayerBackground:
                    this.ReDrawMessageLayer(id, Director.ScrMana.GetMsgLayerDescriptor(id), true);
                    break;
                case ResourceType.Button:
                    this.ReDrawButton(id, Director.ScrMana.GetButtonDescriptor(id));
                    break;
                case ResourceType.BranchButton:
                    this.ReDrawBranchButton(id, Director.ScrMana.GetBranchButtonDescriptor(id));
                    break;
            }
        }

        /// <summary>
        /// 获取画面上的精灵实例
        /// </summary>
        /// <param name="id">精灵id</param>
        /// <param name="rType">资源类型</param>
        /// <returns>精灵实例</returns>
        public YuriSprite GetSprite(int id, ResourceType rType)
        {
            switch (rType)
            {
                case ResourceType.Background:
                    return this.backgroundSpriteVec[id];
                case ResourceType.Stand:
                    return this.characterStandSpriteVec[id];
                default:
                    return this.pictureSpriteVec[id];
            }
        }

        /// <summary>
        /// 获取画面上的文字层实例
        /// </summary>
        /// <param name="id">文字层id</param>
        /// <returns>文字层实例</returns>
        public MessageLayer GetMessageLayer(int id)
        {
            return this.messageLayerVec[id];
        }

        /// <summary>
        /// 获取画面上的按钮实例
        /// </summary>
        /// <param name="id">按钮id</param>
        /// <returns>按钮实例</returns>
        public SpriteButton GetSpriteButton(int id)
        {
            return this.buttonLayerVec[id];
        }

        /// <summary>
        /// 获取画面上的按钮实例
        /// </summary>
        /// <param name="id">选择支id</param>
        /// <returns>选择支实例</returns>
        public BranchButton GetBranchButton(int id)
        {
            return this.branchButtonVec[id];
        }

        /// <summary>
        /// 将指定类型的所有项目从画面移除
        /// </summary>
        public void RemoveView(ResourceType rType)
        {
            switch (rType)
            {
                case ResourceType.Button:
                    for (int bi = 0; bi < this.buttonLayerVec.Count; bi++)
                    {
                        if (this.buttonLayerVec[bi] != null)
                        {
                            this.RemoveButton(bi);
                        }
                    }
                    return;
                case ResourceType.Background:
                    for (int bi = 0; bi < this.backgroundSpriteVec.Count; bi++)
                    {
                        if (this.backgroundSpriteVec[bi] != null)
                        {
                            this.RemoveSprite(bi, ResourceType.Background);
                        }
                    }
                    return;
                case ResourceType.Stand:
                    for (int bi = 0; bi < this.characterStandSpriteVec.Count; bi++)
                    {
                        if (this.characterStandSpriteVec[bi] != null)
                        {
                            this.RemoveSprite(bi, ResourceType.Stand);
                        }
                    }
                    return;
                case ResourceType.Pictures:
                    for (int bi = 0; bi < this.pictureSpriteVec.Count; bi++)
                    {
                        if (this.pictureSpriteVec[bi] != null)
                        {
                            this.RemoveSprite(bi, ResourceType.Pictures);
                        }
                    }
                    return;
                case ResourceType.MessageLayerBackground:
                    for (int bi = 0; bi < this.messageLayerVec.Count; bi++)
                    {
                        if (this.messageLayerVec[bi] != null)
                        {
                            this.RemoveMessageLayer(bi);
                        }
                    }
                    return;
                case ResourceType.BranchButton:
                    for (int bi = 0; bi < this.branchButtonVec.Count; bi++)
                    {
                        if (this.branchButtonVec[bi] != null)
                        {
                            this.RemoveBranchButton(bi);
                        }
                    }
                    return;
                default:
                    break;
            }
            for (int bi = 0; bi < this.buttonLayerVec.Count; bi++)
            {
                if (this.buttonLayerVec[bi] != null)
                {
                    this.RemoveButton(bi);
                }
            }
            for (int bi = 0; bi < this.backgroundSpriteVec.Count; bi++)
            {
                if (this.backgroundSpriteVec[bi] != null)
                {
                    this.RemoveSprite(bi, ResourceType.Background);
                }
            }
            for (int bi = 0; bi < this.characterStandSpriteVec.Count; bi++)
            {
                if (this.characterStandSpriteVec[bi] != null)
                {
                    this.RemoveSprite(bi, ResourceType.Stand);
                }
            }
            for (int bi = 0; bi < this.pictureSpriteVec.Count; bi++)
            {
                if (this.pictureSpriteVec[bi] != null)
                {
                    this.RemoveSprite(bi, ResourceType.Pictures);
                }
            }
            for (int bi = 0; bi < this.messageLayerVec.Count; bi++)
            {
                if (this.messageLayerVec[bi] != null)
                {
                    this.RemoveMessageLayer(bi);
                }
            }
            for (int bi = 0; bi < this.branchButtonVec.Count; bi++)
            {
                if (this.branchButtonVec[bi] != null)
                {
                    this.RemoveBranchButton(bi);
                }
            }
        }

        /// <summary>
        /// 将指定精灵从画面移除并释放
        /// </summary>
        /// <param name="id">精灵id</param>
        /// <param name="rType">类型</param>
        public void RemoveSprite(int id, ResourceType rType)
        {
            Director.ScrMana.RemoveSprite(id, rType);
            YuriSprite removeOne = null;
            switch (rType)
            {
                case ResourceType.Background:
                    removeOne = this.backgroundSpriteVec[id];
                    //this.backgroundSpriteVec[id] = null;
                    //if (removeOne.DisplayBinding == this.view.TransitionBox.Content)
                    //{
                    ScreenManager.GetInstance().Backlay();
                    this.ApplyTransition("FadeTransition");
                    //}
                    break;
                case ResourceType.Stand:
                    removeOne = this.characterStandSpriteVec[id];
                    this.characterStandSpriteVec[id] = null;
                    this.RemoveSprite(removeOne);
                    break;
                case ResourceType.Pictures:
                    removeOne = this.pictureSpriteVec[id];
                    this.pictureSpriteVec[id] = null;
                    this.RemoveSprite(removeOne);
                    break;
            }
        }

        /// <summary>
        /// 将指定按钮从画面移除并释放
        /// </summary>
        /// <param name="id">按钮id</param>
        public void RemoveButton(int id)
        {
            Director.ScrMana.RemoveButton(id);
            SpriteButton removeOne = this.buttonLayerVec[id];
            this.buttonLayerVec[id] = null;
            this.RemoveButton(removeOne);
        }

        /// <summary>
        /// 初始化文字层实例
        /// </summary>
        public void InitMessageLayer()
        {
            for (int i = 0; i < GlobalDataContainer.GAME_MESSAGELAYER_COUNT; i++)
            {
                this.ReDrawMessageLayer(i, Director.ScrMana.GetMsgLayerDescriptor(i), true);
            }
        }

        /// <summary>
        /// 执行过渡
        /// </summary>
        /// <param name="transTypeName">过渡类型的名字</param>
        public void ApplyTransition(string transTypeName)
        {
            // 刷新精灵
            var backDesc = Director.ScrMana.GetSpriteDescriptor(0, ResourceType.Background);
            var foreDesc = Director.ScrMana.GetSpriteDescriptor(1, ResourceType.Background);
            if (backDesc == null)
            {
                this.backgroundSpriteVec[0] = null;
            }
            if (foreDesc == null)
            {
                this.backgroundSpriteVec[1] = null;
            }
            Type transType = this.transitionTypes[0];
            foreach (var t in this.transitionTypes)
            {
                string[] nameItem = t.ToString().Split('.');
                if (nameItem[nameItem.Length - 1].ToLower() == transTypeName.ToLower())
                {
                    transType = t;
                    break;
                }
            }
            Transition transition = (Transition)Activator.CreateInstance(transType);
            CommonUtils.Swap<YuriSprite>(this.backgroundSpriteVec, (int)BackgroundPage.Fore, (int)BackgroundPage.Back);
            if (this.backgroundSpriteVec[(int)BackgroundPage.Fore] != null)
            {
                this.backgroundSpriteVec[(int)BackgroundPage.Fore].DisplayZ = (int)BackgroundPage.Fore + GlobalDataContainer.GAME_Z_BACKGROUND;
            }
            if (this.backgroundSpriteVec[(int)BackgroundPage.Back] != null)
            {
                this.backgroundSpriteVec[(int)BackgroundPage.Back].DisplayZ = (int)BackgroundPage.Back + GlobalDataContainer.GAME_Z_BACKGROUND;
            }
            Director.ScrMana.Backlay();
            this.view.TransitionDS.ObjectInstance = transition;
            var viewBinder = this.backgroundSpriteVec[(int)BackgroundPage.Fore] == null ?
                null : this.backgroundSpriteVec[(int)BackgroundPage.Fore].DisplayBinding;
            if (viewBinder != null && this.view.BO_MainGrid.Children.Contains(viewBinder))
            {
                this.view.BO_MainGrid.Children.Remove(viewBinder);
                Canvas.SetZIndex(this.view.TransitionBox, Canvas.GetZIndex(viewBinder));
            }
            this.view.TransitionBox.Content = viewBinder;
        }

        /// <summary>
        /// 为视窗管理器设置主窗体的引用
        /// </summary>
        /// <param name="mw">主窗体</param>
        public void SetMainWndReference(MainWindow mw)
        {
            this.view = mw;
        }

        /// <summary>
        /// 重绘精灵
        /// </summary>
        /// <param name="id">精灵id</param>
        /// <param name="vector">精灵所在向量</param>
        /// <param name="rType">资源类型</param>
        /// <param name="descriptor">精灵描述子</param>
        /// <param name="forceReload">是否强制重新载入资源文件</param>
        private void ReDrawSprite(int id, List<YuriSprite> vector, ResourceType rType, SpriteDescriptor descriptor, bool forceReload)
        {
            // 不需要重绘的情况
            if (descriptor == null)
            {
                if (vector[id] != null)
                {
                    switch (rType)
                    {
                        case ResourceType.Background:
                            this.RemoveSprite(id, ResourceType.Background);
                            break;
                        case ResourceType.Stand:
                            this.RemoveSprite(id, ResourceType.Stand);
                            break;
                        case ResourceType.Pictures:
                            this.RemoveSprite(id, ResourceType.Pictures);
                            break;
                    }
                }
                return;
            }
            YuriSprite sprite = vector[id], newSprite = null;
            // 强制重新载入或资源名称不同时重新加载资源文件
            if (sprite == null ||
                sprite.ResourceName != descriptor.ResourceName ||
                forceReload)
            {
                switch (rType)
                {
                    case ResourceType.Background:
                        vector[id] = newSprite = ResourceManager.GetInstance().GetBackground(descriptor.ResourceName, descriptor.CutRect);
                        break;
                    case ResourceType.Stand:
                        vector[id] = newSprite = ResourceManager.GetInstance().GetCharacterStand(descriptor.ResourceName, descriptor.CutRect);
                        break;
                    case ResourceType.Pictures:
                        vector[id] = newSprite = ResourceManager.GetInstance().GetPicture(descriptor.ResourceName, descriptor.CutRect);
                        break;
                }
            }
            else
            {
                newSprite = sprite;
            }
            // 重绘精灵
            this.RemoveSprite(sprite);
            this.DrawSprite(newSprite, descriptor);
        }

        /// <summary>
        /// 重绘文字层
        /// </summary>
        /// <param name="id">文字层id</param>
        /// <param name="descriptor">文字层描述子</param>
        /// <param name="forceReload">是否强制重新载入背景图资源文件</param>
        private void ReDrawMessageLayer(int id, MessageLayerDescriptor descriptor, bool forceReload)
        {
            // 不需要重绘的情况
            if (descriptor == null) { return; }
            MessageLayer msglay = this.messageLayerVec[id];
            if (msglay == null ||
                (msglay.BackgroundSprite != null && msglay.BackgroundSprite.ResourceName != descriptor.BackgroundResourceName) ||
                forceReload)
            {
                YuriSprite bgSprite = ResourceManager.GetInstance().GetPicture(descriptor.BackgroundResourceName, new Int32Rect(-1, 0, 0, 0));
                MessageLayer newLayer = new MessageLayer();
                newLayer.BackgroundSprite = bgSprite;
                newLayer.Id = id;
                this.messageLayerVec[id] = msglay = newLayer;
            }
            // 重绘文本层
            this.RemoveMessageLayer(msglay);
            this.DrawMessageLayer(msglay, descriptor);
        }

        /// <summary>
        /// 重绘按钮
        /// </summary>
        /// <param name="id">按钮id</param>
        /// <param name="descriptor">按钮描述子</param>
        private void ReDrawButton(int id, SpriteButtonDescriptor descriptor)
        {
            // 不需要重绘的情况
            if (descriptor == null) { return; }
            SpriteButton oldButton = this.buttonLayerVec[id];
            SpriteButton sbutton = new SpriteButton(id);
            sbutton.ImageNormal = descriptor.NormalDescriptor == null ? null : ResourceManager.GetInstance().GetPicture(descriptor.NormalDescriptor.ResourceName, new Int32Rect(-1, 0, 0, 0));
            sbutton.ImageMouseOver = descriptor.OverDescriptor == null ? null : ResourceManager.GetInstance().GetPicture(descriptor.OverDescriptor.ResourceName, new Int32Rect(-1, 0, 0, 0));
            sbutton.ImageMouseOn = descriptor.OnDescriptor == null ? null : ResourceManager.GetInstance().GetPicture(descriptor.OnDescriptor.ResourceName, new Int32Rect(-1, 0, 0, 0));
            this.buttonLayerVec[id] = sbutton;
            // 重绘
            this.RemoveButton(oldButton);
            this.DrawButton(sbutton, descriptor);
        }

        /// <summary>
        /// 重绘选择支
        /// </summary>
        /// <param name="id">选择支id</param>
        /// <param name="descriptor">选择支描述子</param>
        private void ReDrawBranchButton(int id, BranchButtonDescriptor descriptor)
        {
            // 不需要重绘的情况
            if (descriptor == null) { return; }
            BranchButton oldButton = this.branchButtonVec[id];
            BranchButton sbutton = new BranchButton(id);
            sbutton.ImageNormal = descriptor.NormalDescriptor == null ? null : ResourceManager.GetInstance().GetPicture(descriptor.NormalDescriptor.ResourceName, new Int32Rect(-1, 0, 0, 0));
            sbutton.ImageMouseOver = descriptor.OverDescriptor == null ? null : ResourceManager.GetInstance().GetPicture(descriptor.OverDescriptor.ResourceName, new Int32Rect(-1, 0, 0, 0));
            sbutton.ImageMouseOn = descriptor.OnDescriptor == null ? null : ResourceManager.GetInstance().GetPicture(descriptor.OnDescriptor.ResourceName, new Int32Rect(-1, 0, 0, 0));
            this.branchButtonVec[id] = sbutton;
            // 重绘
            this.RemoveBranchButton(oldButton);
            this.DrawBranchButton(sbutton, descriptor);
        }

        /// <summary>
        /// 为主窗体描绘一个精灵
        /// </summary>
        /// <param name="sprite">精灵</param>
        /// <param name="descriptor">精灵描述子</param>
        private void DrawSprite(YuriSprite sprite, SpriteDescriptor descriptor)
        {
            if (sprite == null) { return; }
            Image spriteImage = new Image();
            BitmapImage bmp = sprite.SpriteBitmapImage;
            spriteImage.Width = bmp.PixelWidth;
            spriteImage.Height = bmp.PixelHeight;
            spriteImage.Source = bmp;
            spriteImage.Opacity = descriptor.Opacity;
            sprite.CutRect = descriptor.CutRect;
            sprite.DisplayBinding = spriteImage;
            sprite.Anchor = descriptor.AnchorType;
            Canvas.SetLeft(spriteImage, descriptor.X);
            Canvas.SetTop(spriteImage, descriptor.Y);
            Canvas.SetZIndex(spriteImage, descriptor.Z);
            spriteImage.Visibility = Visibility.Visible;
            this.view.BO_MainGrid.Children.Add(spriteImage);
            sprite.InitAnimationRenderTransform();
            SpriteAnimation.RotateToAnimation(sprite, TimeSpan.FromMilliseconds(0), descriptor.Angle, 0);
            SpriteAnimation.ScaleToAnimation(sprite, TimeSpan.FromMilliseconds(0), descriptor.ScaleX, descriptor.ScaleY, 0, 0);
        }

        /// <summary>
        /// 为主窗体描绘一个文字层
        /// </summary>
        /// <param name="msglay">文字层</param>
        /// <param name="descriptor">文字层描述子</param>
        private void DrawMessageLayer(MessageLayer msglay, MessageLayerDescriptor descriptor)
        {
            TextBlock msgBlock = new TextBlock();
            msglay.DisplayBinding = msgBlock;
            if (msglay.BackgroundSprite != null && msglay.BackgroundSprite.SpriteBitmapImage != null)
            {
                ImageBrush ib = new ImageBrush(msglay.BackgroundSprite.SpriteBitmapImage);
                ib.Stretch = Stretch.None;
                ib.AlignmentX = AlignmentX.Left;
                ib.AlignmentY = AlignmentY.Top;
                msgBlock.Background = ib;
            }
            msglay.Width = descriptor.Width;
            msglay.Height = descriptor.Height;
            msglay.Opacity = descriptor.Opacity;
            msglay.Padding = (Thickness)descriptor.Padding;
            msglay.LineHeight = descriptor.LineHeight;
            msglay.HorizontalAlignment = descriptor.HorizonAlign;
            msglay.VerticalAlignment = descriptor.VertiAlign;
            msglay.FontColor = Color.FromRgb(descriptor.FontColorR, descriptor.FontColorG, descriptor.FontColorB);
            msglay.FontSize = descriptor.FontSize;
            msglay.FontName = descriptor.FontName;
            msglay.FontShadow = descriptor.FontShadow;
            msglay.DisplayBinding.TextWrapping = TextWrapping.Wrap;
            msglay.DisplayBinding.TextAlignment = TextAlignment.Left;
            Canvas.SetLeft(msgBlock, descriptor.X);
            Canvas.SetTop(msgBlock, descriptor.Y);
            Canvas.SetZIndex(msgBlock, descriptor.Z);
            msglay.Visibility = descriptor.Visible ? Visibility.Visible : Visibility.Hidden;
            this.view.BO_MainGrid.Children.Add(msgBlock);
        }

        /// <summary>
        /// 为主窗体描绘一个按钮
        /// </summary>
        /// <param name="sbutton">按钮实例</param>
        /// <param name="descriptor">按钮描述子</param>
        private void DrawButton(SpriteButton sbutton, SpriteButtonDescriptor descriptor)
        {
            Image buttonImage = new Image();
            BitmapImage bmp = sbutton.ImageNormal.SpriteBitmapImage;
            buttonImage.Width = bmp.PixelWidth;
            buttonImage.Height = bmp.PixelHeight;
            buttonImage.Source = bmp;
            buttonImage.Opacity = descriptor.Opacity;
            sbutton.DisplayBinding = buttonImage;
            sbutton.Eternal = descriptor.Eternal;
            sbutton.Enable = descriptor.Enable;
            sbutton.Ntr = new Interrupt()
            {
                detail = "ButtonNTRInterrupt",
                interruptSA = null,
                type = InterruptType.ButtonJump,
                interruptFuncSign = descriptor.InterruptFuncSign,
                returnTarget = descriptor.JumpLabel,
                pureInterrupt = false,
                exitWait = !descriptor.Eternal
            };
            Canvas.SetLeft(buttonImage, descriptor.X);
            Canvas.SetTop(buttonImage, descriptor.Y);
            Canvas.SetZIndex(buttonImage, descriptor.Z);
            buttonImage.Visibility = Visibility.Visible;
            buttonImage.MouseDown += sbutton.MouseOnHandler;
            buttonImage.MouseEnter += sbutton.MouseEnterHandler;
            buttonImage.MouseLeave += sbutton.MouseLeaveHandler;
            buttonImage.MouseUp += sbutton.MouseUpHandler;
            this.view.BO_MainGrid.Children.Add(buttonImage);
            sbutton.InitAnimationRenderTransform();
        }

        /// <summary>
        /// 为主窗体描绘一个选择支
        /// </summary>
        /// <param name="sbutton">按钮实例</param>
        /// <param name="descriptor">按钮描述子</param>
        private void DrawBranchButton(BranchButton bbutton, BranchButtonDescriptor descriptor)
        {
            TextBlock buttonTextView = new TextBlock();
            BitmapImage bmp = bbutton.ImageNormal.SpriteBitmapImage;
            buttonTextView.Width = bmp.PixelWidth;
            buttonTextView.Height = bmp.PixelHeight;
            ImageBrush ib = new ImageBrush(bmp);
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
            ib.TileMode = TileMode.None;
            ib.Stretch = Stretch.Fill;
            buttonTextView.FontSize = GlobalDataContainer.GAME_BRANCH_FONTSIZE;
            buttonTextView.Foreground = new SolidColorBrush(GlobalDataContainer.GAME_BRANCH_FONTCOLOR);
            buttonTextView.FontFamily = new FontFamily(GlobalDataContainer.GAME_BRANCH_FONTNAME);
            buttonTextView.TextAlignment = TextAlignment.Center;
            buttonTextView.Padding = new Thickness(0, GlobalDataContainer.GAME_BRANCH_TOPPAD, 0, 0);
            buttonTextView.Background = ib;
            bbutton.DisplayBinding = buttonTextView;
            bbutton.Eternal = false;
            bbutton.Enable = true;
            bbutton.Text = descriptor.Text;
            bbutton.Ntr = new Interrupt()
            {
                detail = "BranchButtonNTRInterrupt",
                interruptSA = null,
                type = InterruptType.ButtonJump,
                returnTarget = descriptor.JumpTarget,
                exitWait = true
            };
            Canvas.SetLeft(buttonTextView, descriptor.X);
            Canvas.SetTop(buttonTextView, descriptor.Y);
            Canvas.SetZIndex(buttonTextView, descriptor.Z);
            buttonTextView.Visibility = Visibility.Visible;
            buttonTextView.MouseDown += bbutton.MouseOnHandler;
            buttonTextView.MouseEnter += bbutton.MouseEnterHandler;
            buttonTextView.MouseLeave += bbutton.MouseLeaveHandler;
            buttonTextView.MouseUp += bbutton.MouseUpHandler;
            this.view.BO_MainGrid.Children.Add(buttonTextView);
            bbutton.InitAnimationRenderTransform();
        }

        /// <summary>
        /// 将指定文字层从画面移除并释放
        /// </summary>
        /// <param name="id">文字层id</param>
        private void RemoveMessageLayer(int id)
        {
            Director.ScrMana.RemoveMsgLayer(id);
            MessageLayer removeOne = this.messageLayerVec[id];
            this.messageLayerVec[id] = null;
            this.RemoveMessageLayer(removeOne);
        }

        /// <summary>
        /// 将选择支从画面移除
        /// </summary>
        /// <param name="bbutton">选择支实例</param>
        private void RemoveBranchButton(BranchButton bbutton)
        {
            if (bbutton != null)
            {
                var bbView = bbutton.DisplayBinding;
                if (bbView != null && this.view.BO_MainGrid.Children.Contains(bbView))
                {
                    this.view.BO_MainGrid.Children.Remove(bbView);
                }
                bbutton.DisplayBinding = null;
            }
        }

        /// <summary>
        /// 将指定选择支从画面移除并释放
        /// </summary>
        /// <param name="id">选择支id</param>
        private void RemoveBranchButton(int id)
        {
            Director.ScrMana.RemoveBranchButton(id);
            BranchButton removeOne = this.branchButtonVec[id];
            this.branchButtonVec[id] = null;
            this.RemoveBranchButton(removeOne);
        }

        /// <summary>
        /// 将精灵从画面移除
        /// </summary>
        /// <param name="sprite">精灵实例</param>
        private void RemoveSprite(YuriSprite sprite)
        {
            if (sprite != null)
            {
                Image spriteView = sprite.DisplayBinding;
                if (spriteView != null)
                {
                    if (this.view.BO_MainGrid.Children.Contains(spriteView))
                    {
                        this.view.BO_MainGrid.Children.Remove(spriteView);
                    }
                }
                sprite.DisplayBinding = null;
            }
        }

        /// <summary>
        /// 将文字层从画面移除
        /// </summary>
        /// <param name="msglay">文字层实例</param>
        private void RemoveMessageLayer(MessageLayer msglay)
        {
            if (msglay != null)
            {
                TextBlock msglayView = msglay.DisplayBinding;
                if (msglayView != null && this.view.BO_MainGrid.Children.Contains(msglayView))
                {
                    this.view.BO_MainGrid.Children.Remove(msglayView);
                }
                msglay.DisplayBinding = null;
            }
        }

        /// <summary>
        /// 将按钮从画面移除
        /// </summary>
        /// <param name="sbutton">按钮实例</param>
        private void RemoveButton(SpriteButton sbutton)
        {
            if (sbutton != null)
            {
                Image buttonView = sbutton.DisplayBinding;
                if (buttonView != null && this.view.BO_MainGrid.Children.Contains(buttonView))
                {
                    this.view.BO_MainGrid.Children.Remove(buttonView);
                    if (sbutton.ImageNormal != null)
                    {
                        sbutton.ImageNormal.DisplayBinding = null;
                    }
                    if (sbutton.ImageMouseOver != null)
                    {
                        sbutton.ImageMouseOver.DisplayBinding = null;
                    }
                    if (sbutton.ImageMouseOn != null)
                    {
                        sbutton.ImageMouseOn.DisplayBinding = null;
                    }
                }
                sbutton.DisplayBinding = null;
            }
        }

        /// <summary>
        /// 加载过渡样式
        /// </summary>
        /// <param name="assembly">过渡所在的程序集</param>
        private void LoadTransitions(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                // Must not already exist
                if (transitionTypes.Contains(type)) { continue; }

                // Must not be abstract.
                if ((typeof(Transition).IsAssignableFrom(type)) && (!type.IsAbstract))
                {
                    transitionTypes.Add(type);
                }
            }
        }

        /// <summary>
        /// 背景精灵向量
        /// </summary>
        private List<YuriSprite> backgroundSpriteVec;
        
        /// <summary>
        /// 立绘精灵向量
        /// </summary>
        private List<YuriSprite> characterStandSpriteVec;
        
        /// <summary>
        /// 图片精灵向量
        /// </summary>
        private List<YuriSprite> pictureSpriteVec;

        /// <summary>
        /// 文字层向量
        /// </summary>
        private List<MessageLayer> messageLayerVec;

        /// <summary>
        /// 按钮层向量
        /// </summary>
        private List<SpriteButton> buttonLayerVec;

        /// <summary>
        /// 选择支按钮向量
        /// </summary>
        private List<BranchButton> branchButtonVec;

        /// <summary>
        /// 过渡类型容器
        /// </summary>
        private ObservableCollection<Type> transitionTypes = new ObservableCollection<Type>();

        /// <summary>
        /// 私有的构造器
        /// </summary>
        private ViewManager()
        {
            this.LoadTransitions(Assembly.GetAssembly(typeof(Transition)));
            this.backgroundSpriteVec = new List<YuriSprite>();
            this.characterStandSpriteVec = new List<YuriSprite>();
            this.pictureSpriteVec = new List<YuriSprite>();
            this.messageLayerVec = new List<MessageLayer>();
            this.branchButtonVec = new List<BranchButton>();
            this.buttonLayerVec = new List<SpriteButton>();
            for (int i = 0; i < GlobalDataContainer.GAME_BACKGROUND_COUNT; i++)
            {
                this.backgroundSpriteVec.Add(null);
            }
            for (int i = 0; i < GlobalDataContainer.GAME_CHARACTERSTAND_COUNT; i++)
            {
                this.characterStandSpriteVec.Add(null);
            }
            for (int i = 0; i < GlobalDataContainer.GAME_IMAGELAYER_COUNT; i++)
            {
                this.pictureSpriteVec.Add(null);
            }
            for (int i = 0; i < GlobalDataContainer.GAME_BUTTON_COUNT; i++)
            {
                this.buttonLayerVec.Add(null);
            }
            for (int i = 0; i < GlobalDataContainer.GAME_BRANCH_COUNT; i++)
            {
                this.branchButtonVec.Add(null);
            }
            for (int i = 0; i < GlobalDataContainer.GAME_MESSAGELAYER_COUNT; i++)
            {
                this.messageLayerVec.Add(null);
            }
        }

        /// <summary>
        /// 主窗体引用
        /// </summary>
        private MainWindow view = null;

        /// <summary>
        /// 唯一实例
        /// </summary>
        private static ViewManager synObject = null;
    }

    /// <summary>
    /// 枚举：背景图所在的层
    /// </summary>
    internal enum BackgroundPage
    {
        Back = 0,
        Fore = 1
    }
}
