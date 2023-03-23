using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KairiKawasaki
{
    public class ResultManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneChanger.ChangeScene("Title");
            }
        }
    }
}
