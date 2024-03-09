using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
            Application.LoadLevel("Classroom");
        }
        public void BackToMainMenu()
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
