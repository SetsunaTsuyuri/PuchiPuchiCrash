using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KairiKawasaki
{
    /// <summary>
    /// スコアの管理者
    /// </summary>
    public class ScoreManager : Singleton<ScoreManager>, IInitializable
    {
        /// <summary>
        /// スコア
        /// </summary>
        int _score = 0;

        /// <summary>
        /// スコア
        /// </summary>
        public static int Score
        {
            get => Instance._score;
            set => Instance._score = value;
        }

        public override void Initialize()
        {
            _score = 0;
        }
    }
}
