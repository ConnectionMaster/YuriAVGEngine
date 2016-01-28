﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lyyneheym.SlyviaInterpreter.ILPackage
{
    /// <summary>
    /// <para>IL场景类：控制一个剧本章节的演出的IL包装类</para>
    /// <para>通常，一个场景拥有一个动作序列和生命在它上面的函数</para>
    /// </summary>
    [Serializable]
    internal class PackageScene
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="scenario">场景名称</param>
        /// <param name="mainSa">主动作序列</param>
        /// <param name="funcVec">函数向量</param>
        public PackageScene(string scenario, SceneAction mainSa, List<SceneFunction> funcVec)
        {
            this.scenario = scenario;
            this.mainSa = mainSa;
            this.funcContainer = funcVec;
        }

        /// <summary>
        /// 获取该场景的IL文件头
        /// </summary>
        /// <returns>IL文件头字符串</returns>
        public string GetILSign()
        {
            return String.Format(">>>SlyviaIL?{0}", this.scenario);
        }

        // 场景名称
        public string scenario;
        // 场景的主动作序列
        public SceneAction mainSa;
        // 场景的函数向量
        public List<SceneFunction> funcContainer;
    }
}
