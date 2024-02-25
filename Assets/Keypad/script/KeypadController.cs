using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class KeypadController : MonoBehaviour
{
    public List<int> correctPassword = new List<int>();
    public int passwordLength = 4;

    private List<int> inputPasswordList = new List<int>();

    [SerializeField] private TMP_Text codeDisplay;
    [SerializeField] private float resetTime = 1f;
    [SerializeField] private string SuccessText;
    [SerializeField] private AudioSource audioBeep;
    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;

    private bool hasBeenUsed = false;

    public void UserNumberEntry(int selectedNumber)
    {        
        audioBeep.Play();


        if (inputPasswordList.Count >= passwordLength)
            return;

        inputPasswordList.Add(selectedNumber);        

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        codeDisplay.text = string.Empty;
        
        foreach (int i in inputPasswordList)
        {
            codeDisplay.text += i.ToString();
        }
    }

    public void DeleteEntry()
    {
        audioBeep.Play();

        if (inputPasswordList.Count > 0 && !hasBeenUsed)
        {
            inputPasswordList.RemoveAt(inputPasswordList.Count - 1);
            UpdateDisplay();
        }            
    }

    public void CheckPassword()
    {
        audioBeep.Play();

        if (correctPassword.Count == inputPasswordList.Count)
        {
            for (int i = 0; i < correctPassword.Count; i++)
            {
                if (inputPasswordList[i] != correctPassword[i])
                {
                    IncorrectPassword();
                    return;
                }
            }

            CorrectPassword();
        }
        else
            IncorrectPassword();

    }

    private void CorrectPassword()
    {
        onCorrectPassword.Invoke();
        codeDisplay.text = SuccessText;
        SetDisplayColor(Color.green);
        hasBeenUsed = true;
    }

    private void IncorrectPassword()
    {
        SetDisplayColor(Color.red);
        onIncorrectPassword.Invoke();        

        StartCoroutine(ResetKeyCode());
    }

    IEnumerator ResetKeyCode()
    {
        yield return new WaitForSeconds(resetTime);

        inputPasswordList.Clear();
        SetDisplayColor(Color.white);
        codeDisplay.text = "Enter code ...";
    }

    private void SetDisplayColor(Color color)
    {
        codeDisplay.color = color;
    }

    public void AnimateButton(Animator animator)
    {
        animator.Play("keypressed", 0, 0.0f);
    }
}
