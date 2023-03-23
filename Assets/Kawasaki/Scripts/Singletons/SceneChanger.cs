using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KairiKawasaki
{
    public class SceneChanger : Singleton<SceneChanger>, IInitializable
    {
        bool _isChangingScene = false;

        public static void ChangeScene(string sceneName)
        {
            Instance.ChangeSceneInner(sceneName);
        }

        private void ChangeSceneInner(string sceneName)
        {
            if (_isChangingScene)
            {
                return;
            }

            _isChangingScene = true;

            CancellationToken token = FadeManager.Instance.GetCancellationTokenOnDestroy();
            ChangeSceneAsync(sceneName, token).Forget();
        }

        private async UniTask ChangeSceneAsync(string sceneName, CancellationToken token)
        {
            await FadeManager.FadeOut(token);

            SceneManager.LoadScene(sceneName);

            await FadeManager.FadeIn(token);

            _isChangingScene = false;
        }
    }
}
