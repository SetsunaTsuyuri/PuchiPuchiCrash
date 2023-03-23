using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KairiKawasaki
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// 現在のシーンに存在するインスタンス Awakeより後で有効
        /// </summary>
        public static GameManager Instance { get; private set; } = null;

        [SerializeField]
        int _badPuchiPuchiScore = 0;

        [SerializeField]
        int _normalPuchiPuchiScore = 100;

        [SerializeField]
        int _goldPuchiPuchiScore = 500;

        [SerializeField]
        float _playTime = 60.0f;

        [SerializeField]
        int _badPuchiPuchiTensionUp = 0;

        [SerializeField]
        int _normalPuchiPuchiTensionUp = 100;

        [SerializeField]
        int _goldPuchiPuchiTensionUp = 500;

        [SerializeField]
        int[] _powerUpThreshoulds = { };

        int _powerUpThreshouldIndex = 0;

        public int PlayerMode { get; private set; } = (int)Player.PlayerMode.Thumb;

        public int CurrentTension { get; private set; } = 0;

        public float RemainingTime { get; private set; } = 0.0f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ScoreManager.Instance.Initialize();
            RemainingTime = _playTime;
        }

        private void LateUpdate()
        {
            RemainingTime -= Time.deltaTime;

            if (RemainingTime <= 0.0f)
            {
                SceneChanger.ChangeScene("Result");
            }

            // デバッグ用！！！
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RemainingTime = 5.0f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                OnPlayerPowerUP();
            }
        }

        public int OnPuchiPuchiBashed(PuchiPuchi puchiPuchi)
        {
            //0:潰れ 1:ノーマル 2:金
            int state = puchiPuchi.getState();
            int score = state switch
            {
                0 => _badPuchiPuchiScore,
                1 => _normalPuchiPuchiScore,
                2 => _goldPuchiPuchiScore,
                _ => 0
            };

            ScoreManager.Score += score;

            if (_powerUpThreshouldIndex < _powerUpThreshoulds.Length)
            {
                int tensionUp = state switch
                {
                    0 => _badPuchiPuchiTensionUp,
                    1 => _normalPuchiPuchiTensionUp,
                    2 => _goldPuchiPuchiTensionUp,
                    _ => 0
                };

                CurrentTension += tensionUp;
                while (_powerUpThreshouldIndex < _powerUpThreshoulds.Length
                    && CurrentTension >= _powerUpThreshoulds[_powerUpThreshouldIndex])
                {
                    OnPlayerPowerUP();
                    CurrentTension = 0;
                    _powerUpThreshouldIndex++;
                }
            }

            return score;
        }

        private void OnPlayerPowerUP()
        {
            if (PlayerMode < (int)Player.PlayerMode.Max - 1)
            {
                ++PlayerMode;
            }
        }

        public float GetTensionRate()
        {
            return Mathf.Clamp01((float)CurrentTension / _powerUpThreshoulds[_powerUpThreshouldIndex]);
        }

        public void GameEndMethodForDebug()
        {
            RemainingTime = 5.0f;
        }
    }
}
