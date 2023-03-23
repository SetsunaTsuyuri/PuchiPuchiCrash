using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KairiKawasaki
{
    public class TensionGauge : MonoBehaviour
    {
        Slider _image = null;

        private void Awake()
        {
            _image = transform.GetComponentInChildren<Slider>();
        }

        private void LateUpdate()
        {
            float fillAmount = GameManager.Instance.GetTensionRate();
            _image.value = fillAmount;
        }
    }
}
