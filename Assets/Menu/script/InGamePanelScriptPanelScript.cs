using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace Unity.VRTemplate
{
    public class InGamePanelScriptPanelScript : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Instance of pausemenu.")]
        GameObject PauseMenu;
        public void QuitGame()
        {
            Application.Quit();
        }

        public void Resume()
        {
            PauseMenu.SetActive(false);
        }
        public void PlayAgain()
        {
            SceneManager.LoadScene("Classroom");
        }
        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
