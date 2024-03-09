using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unity.VRTemplate
{
    public class MainMenuScript : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }

        public void PlayGame()
        {
            Application.LoadLevel("Classroom");
        }
    }
}
