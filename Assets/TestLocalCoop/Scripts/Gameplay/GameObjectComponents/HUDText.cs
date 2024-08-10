using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TestLocalCoop.Scripts.Gameplay.GameObjectComponents
{
    public class HUDText : MonoBehaviour
    {
        private TextMeshProUGUI _currentText;
        private void OnEnable()
        {
            _currentText = GetComponent<TextMeshProUGUI>();
        }
        public void ChangeText(int numberThings)
        {
            _currentText.text = "Осталось собрать: " + numberThings;
        }
    }
}
