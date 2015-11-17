﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using LyyneheymCore.SlyviaPile;

namespace LyyneheymCore.SlyviaCore
{
    /// <summary>
    /// <para>资源管理器类：负责维护游戏的资源</para>
    /// <para>她是一个单例类，只有唯一实例</para>
    /// </summary>
    public class ResourceManager
    {

       

        /// <summary>
        /// 获得一张指定背景图的实例
        /// </summary>
        /// <param name="sourceName">资源名称</param>
        /// <returns>该资源的实例</returns>
        public BitmapImage getBackgroundImage(string sourceName)
        {
            // 总是先查看是否有为封包的数据
            string furi = Consta.DevURI_RT_PICTUREASSETS + "\\" + Consta.DevURI_PA_BACKGROUND + "\\" + sourceName;
            if (File.Exists(IOUtils.parseURItoURL("\\" + furi)))
            {
                Uri bg = new Uri(furi, UriKind.RelativeOrAbsolute);
                BitmapImage bpi = new BitmapImage();
                bpi.BeginInit();
                bpi.UriSource = bg;
                bpi.EndInit();
                return bpi;
            }
            else if (this.resourceTable.ContainsKey(Consta.DevURI_PA_BACKGROUND) &&
                this.resourceTable[Consta.DevURI_PA_BACKGROUND].ContainsKey(sourceName))
            {
                KeyValuePair<long, long> sourceLocation = this.resourceTable[Consta.DevURI_PA_BACKGROUND][sourceName];
                byte[] ob = PackageUtils.getObjectBytes(IOUtils.parseURItoURL("\\" + Consta.PackURI_PA_BACKGROUND + Consta.PackPostfix),
                    sourceName, sourceLocation.Key, sourceLocation.Value);
                MemoryStream ms = new MemoryStream(ob);
                BitmapImage bpi = new BitmapImage();
                bpi.BeginInit();
                bpi.StreamSource = ms;
                bpi.EndInit();
                return bpi;
            }
            else
            {
                throw new Exception("缺失资源文件：" + sourceName);
            }
        }

        /// <summary>
        /// 获得一张指定立绘图的实例
        /// </summary>
        /// <param name="sourceName">资源名称</param>
        /// <returns>该资源的实例</returns>
        public BitmapImage getCharacterStandImage(string sourceName)
        {
            // 总是先查看是否有为封包的数据
            string furi = Consta.DevURI_RT_PICTUREASSETS + "\\" + Consta.DevURI_PA_CHARASTAND + "\\" + sourceName;
            if (File.Exists(IOUtils.parseURItoURL("\\" + furi)))
            {
                Uri bg = new Uri(furi, UriKind.RelativeOrAbsolute);
                BitmapImage bpi = new BitmapImage();
                bpi.BeginInit();
                bpi.UriSource = bg;
                bpi.EndInit();
                return bpi;
            }
            else if (this.resourceTable.ContainsKey(Consta.DevURI_PA_CHARASTAND) &&
                this.resourceTable[Consta.DevURI_PA_CHARASTAND].ContainsKey(sourceName))
            {
                KeyValuePair<long, long> sourceLocation = this.resourceTable[Consta.DevURI_PA_CHARASTAND][sourceName];
                byte[] ob = PackageUtils.getObjectBytes(IOUtils.parseURItoURL("\\" + Consta.PackURI_PA_CHARASTAND + Consta.PackPostfix),
                    sourceName, sourceLocation.Key, sourceLocation.Value);
                MemoryStream ms = new MemoryStream(ob);
                BitmapImage bpi = new BitmapImage();
                bpi.BeginInit();
                bpi.StreamSource = ms;
                bpi.EndInit();
                return bpi;
            }
            else
            {
                throw new Exception("缺失资源文件：" + sourceName);
            }
        }












        

        

        /// <summary>
        /// 初始化资源字典
        /// </summary>
        /// <returns>操作成功与否</returns>
        public bool initDictionary()
        {
            return true;
        }

        /// <summary>
        /// 工厂方法：获得类的唯一实例
        /// </summary>
        /// <returns>资源管理器的唯一实例</returns>
        public static ResourceManager getInstance()
        {
            return null == synObject ? synObject = new ResourceManager() : synObject;
        }

        /// <summary>
        /// 私有的构造器
        /// </summary>
        private ResourceManager()
        {
            resourceTable = new Dictionary<string, Dictionary<string, KeyValuePair<long, long>>>();
        }

        // 唯一实例量
        private static ResourceManager synObject = null;
        // 资源字典
        public Dictionary<string, Dictionary<string, KeyValuePair<long, long>>> resourceTable = null;
    }
}
