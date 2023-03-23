using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace KairiKawasaki
{
    public class FadeManagerLoader : SingletonBehaviourLoader<FadeManager>
    {
        public override FadeManager LoadPrefab()
        {
            return Resources.Load<FadeManager>("Fade");
        }
    }

    public class FadeManager : SingletonBehaviour<FadeManager, FadeManagerLoader>, IInitializable
    {
        [SerializeField]
        Image _image = null;

        [SerializeField]
        float _fadeDuration = 0.5f;

        bool _isFading = false;

        public static async UniTask FadeIn(CancellationToken token)
        {
            await Instance.Fade(1.0f, 0.0f, token);
        }

        public static async UniTask FadeOut(CancellationToken token)
        {
            await Instance.Fade(0.0f, 1.0f, token);
        }

        private async UniTask Fade(float startAlpha, float endAlpha, CancellationToken token)
        {
            if (_isFading)
            {
                return;
            }

            _isFading = true;
            _image.color = new Color(0.0f, 0.0f, 0.0f, startAlpha);

            float timer = 0.0f;
            while (timer < _fadeDuration)
            {
                float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / _fadeDuration);
                _image.color = new Color(0.0f, 0.0f, 0.0f, alpha);

                timer += Time.deltaTime;
                await UniTask.Yield(token);
            }
            
            _image.color = new Color(0.0f, 0.0f, 0.0f, endAlpha);
            _isFading = false;
        }
    }
}
