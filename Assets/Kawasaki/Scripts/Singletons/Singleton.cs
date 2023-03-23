﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KairiKawasaki
{
    /// <summary>
    /// シングルトン
    /// </summary>
    /// <typeparam name="T">シングルトンの型</typeparam>
    public class Singleton<T> where T : class, IInitializable, new()
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        private static T instance = null;

        /// <summary>
        /// インスタンス
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new T();
                    instance.Initialize();
                }

                return instance;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Singleton(){}

        /// <summary>
        /// 初期化する
        /// </summary>
        public virtual void Initialize() { }
    }
}
