﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using Lyyneheym.LyyneheymCore.Utils;
using Lyyneheym.LyyneheymCore.SlyviaCore;
using Lyyneheym.LyyneheymCore.ILPackage;

namespace Lyyneheym
{
    /// <summary>
    /// <para>导演类：管理整个游戏生命周期的类</para>
    /// <para>她是一个单例类，只有唯一实例</para>
    /// </summary>
    public class Slyvia
    {

        #region debug用
        public MySprite testBitmapImage(string filename)
        {
            return this.ResMana.GetBackground(filename);
        }

        public MySprite testCharaStand(string filename)
        {
            return this.ResMana.GetCharacterStand(filename);
        }


        public void testBGM(string sourceName)
        {
            Musician m = Musician.getInstance();
            var r = this.ResMana.GetBGM(sourceName);
            m.PlayBGM(sourceName, r.Key, r.Value, 1000);

        }

        public void testVocal(string vocalName)
        {
            Musician m = Musician.getInstance();
            var r = this.ResMana.GetVocal(vocalName);
            m.PlayVocal(r.Key, r.Value, 1000);
        }
        #endregion

        #region 初次进入时的初始化相关函数
        /// <summary>
        /// 初始化游戏设置
        /// </summary>
        private void InitConfig()
        {
            // 读入游戏设定INI
            
            // 把设定写到GlobalDataContainer

            // 应用设置到窗体

        }

        /// <summary>
        /// 初始化运行时环境，并指定脚本的入口
        /// </summary>
        private void InitRuntime()
        {
            this.RunMana.CallScene(this.ResMana.GetScene(GlobalDataContainer.Script_Main));
        }
        #endregion

        #region 前端更新后台相关函数
        /// <summary>
        /// 提供由前端更新后台键盘按键信息的方法
        /// </summary>
        /// <param name="e">键盘事件</param>
        public void UpdateKeyboard(KeyEventArgs e)
        {
            this.updateRender.SetKeyboardState(e.Key, e.KeyStates);
            DebugUtils.ConsoleLine(String.Format("Keyboard event: {0} <- {1}", e.Key.ToString(), e.KeyStates.ToString()),
                "Director", OutputStyle.Normal);
        }

        /// <summary>
        /// 提供由前端更新后台鼠标按键信息的方法
        /// </summary>
        /// <param name="e">鼠标事件</param>
        public void UpdateMouse(MouseButtonEventArgs e)
        {
            this.updateRender.SetMouseButtonState(e.ChangedButton, e.ButtonState);
        }

        /// <summary>
        /// 提供由前端更新后台鼠标滚轮信息的方法
        /// </summary>
        /// <param name="delta">鼠标滚轮差分，上滚为正，下滚为负</param>
        public void UpdateMouseWheel(int delta)
        {
            this.updateRender.SetMouseWheelDelta(delta);
        }
        #endregion



        /// <summary>
        /// 处理消息循环
        /// </summary>
        private void UpdateContext(object sender, EventArgs e)
        {
            // 取得调用堆栈顶部状态
            GameStackMachineState stackState = this.RunMana.GameState();
            switch (stackState)
            {
                case GameStackMachineState.Interpreting:
                case GameStackMachineState.FunctionCalling:
                case GameStackMachineState.Await:
                    this.curState = GameState.Performing;
                    this.curStableState = GameStableState.Unstable;
                    break;
                case GameStackMachineState.WaitUser:
                    this.curState = GameState.UserPanel;
                    this.curStableState = GameStableState.Stable;
                    break;
                case GameStackMachineState.NOP:
                case GameStackMachineState.Interrupt:
                    this.curState = GameState.Loading;
                    this.curStableState = GameStableState.Unknown;
                    break;
            }
            // 取得当前IO操作

            // 根据状态和操作刷新后台数据

            // 更新到前端


            if (this.updateRender.GetKeyboardState(Key.Z) == KeyStates.Down ||
                this.updateRender.GetKeyboardState(Key.Z) == (KeyStates.Down | KeyStates.Toggled))
            {
                Musician.getInstance().PauseBGM();
            }
            if (this.updateRender.GetKeyboardState(Key.X) == KeyStates.Down ||
                this.updateRender.GetKeyboardState(Key.X) == (KeyStates.Down | KeyStates.Toggled))
            {
                Musician.getInstance().ResumeBGM();
            }
        }

        private GameState curState;

        private GameStableState curStableState;
        



        #region 导演类自身资源相关
        /// <summary>
        /// 在游戏结束时释放所有资源
        /// </summary>
        public void DisposeResource()
        {
            DebugUtils.ConsoleLine(String.Format("Begin dispose resource"), "Director", OutputStyle.Important);
            BassPlayer.GetInstance().Dispose();
            DebugUtils.ConsoleLine(String.Format("Finished dispose resource, program will shutdown"), "Director", OutputStyle.Important);
        }

        /// <summary>
        /// 工厂方法：获得唯一实例
        /// </summary>
        /// <returns>导演类唯一实例</returns>
        public static Slyvia getInstance()
        {
            return null == synObject ? synObject = new Slyvia() : synObject;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private Slyvia()
        {
            this.ResMana = ResourceManager.getInstance();
            this.RunMana = new RuntimeManager();
            this.updateRender = new UpdateRender();
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(GlobalDataContainer.DirectorTimerInterval);
            this.timer.Tick += UpdateContext;
            this.timer.Start();
        }

        /// <summary>
        /// 消息循环计时器
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// 运行时环境
        /// </summary>
        private RuntimeManager RunMana;

        /// <summary>
        /// 资源管理器
        /// </summary>
        private ResourceManager ResMana;

        /// <summary>
        /// 画面刷新器
        /// </summary>
        public UpdateRender updateRender;

        /// <summary>
        /// 唯一实例
        /// </summary>
        private static Slyvia synObject = null;
        #endregion
    }
}
