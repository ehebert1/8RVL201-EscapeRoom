using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instance of EndGameMenu.")]
    GameObject EndGame;


    public void SetVisible()
    {
        
        EndGame.SetActive(true);
    }
}
