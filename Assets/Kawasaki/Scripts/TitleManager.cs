using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KairiKawasaki
{
    public class TitleManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneChanger.ChangeScene("Game");
            }
        }
    }
}
