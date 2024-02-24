using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class KeypadController : MonoBehaviour
{
    public List<int> password = new List<int>();
    public int passwordLength = 4;

    private List<int> inputPasswordList = new List<int>();

    [SerializeField] private TMP_Text codeDisplay;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private string SuccessText;
    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;

    public bool allowMultipleActivation = true;
    private bool hasUsedCorrectCode = false;
    public bool HasUsedCorrectCode { get { return  hasUsedCorrectCode; }}

    public void UserNumberEntry(int selectedNumber)
    {
        Debug.Log("usernumber entry");
        Debug.Log(selectedNumber);
        if (inputPasswordList.Count >= passwordLength)
            return;

        inputPasswordList.Add(selectedNumber);

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        Debug.Log("update dysplay");
        codeDisplay.text = string.Empty;
        
        foreach (int i in inputPasswordList)
        {
            codeDisplay.text += i.ToString();
        }
    }
}
