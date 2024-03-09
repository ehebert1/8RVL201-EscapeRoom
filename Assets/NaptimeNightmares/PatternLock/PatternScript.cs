using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PatternScript : MonoBehaviour
{
    public List<GameObject> pattern = new List<GameObject>();

    public Material notSelectedMaterial;

    public Material selectedMaterial;

    public UnityEvent onPatternSuccessful;

    public UnityEvent onPatternFailed;

    private readonly List<GameObject> _currentPattern = new List<GameObject>();

    private void Start()
    {
        onPatternSuccessful.AddListener(() => { Debug.Log("On pattern successful"); });
        onPatternFailed.AddListener(() => { Debug.Log("On Pattern Failed"); });
    }

    public void ClickObject(GameObject button)
    {
        if (!_currentPattern.Contains(button))
        {
            _currentPattern.Add(button);
            button.GetComponent<Renderer>().material = selectedMaterial;
        }
    }

    private void Dequeue(GameObject button)
    {
        var currentButton = _currentPattern.Last();

        _currentPattern.Remove(currentButton);

        currentButton.GetComponent<Renderer>().material = notSelectedMaterial;

        if (button != currentButton)
        {
            Dequeue(button);
        }
    }

    public void Confirm()
    {
        bool isPatternCorrect = pattern.Count == _currentPattern.Count;
        int currentIndex = 0;

        while (isPatternCorrect && currentIndex < pattern.Count && currentIndex < _currentPattern.Count)
        {
            isPatternCorrect = pattern[currentIndex] == _currentPattern[currentIndex];
            currentIndex++;
        }

        if (isPatternCorrect)
        {
            onPatternSuccessful.Invoke();
        }
        else
        {
            onPatternFailed.Invoke();
        }
        
        Reset();
    }

    public void Reset()
    {
        while (_currentPattern.Count > 0)
        {
            var currentButton = _currentPattern.Last();

            currentButton.GetComponent<Renderer>().material = notSelectedMaterial;

            _currentPattern.Remove(currentButton);
        }
    }
}
