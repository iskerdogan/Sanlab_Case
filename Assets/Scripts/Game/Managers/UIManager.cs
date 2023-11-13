using TMPro;
using UnityEngine;

namespace Game.Managers
{
    public class UIManager: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI successText;

        public void OpenSuccessText()
        {
            successText.gameObject.SetActive(true);
        }
    }
}