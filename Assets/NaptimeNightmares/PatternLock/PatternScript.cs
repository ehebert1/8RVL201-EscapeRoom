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

    public Material successMaterial;

    public Material failMaterial;

    public UnityEvent onPatternSuccessful;

    public UnityEvent onPatternFailed;

    public List<GameObject> lines = new List<GameObject>();

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
            
            if (_currentPattern.Count >= 2) CreateLine();
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
            foreach (var btn in _currentPattern)
            {
                btn.GetComponent<MeshRenderer>().material = successMaterial;
            }

            foreach (var line in lines)
            {
                line.GetComponent<MeshRenderer>().material = successMaterial;
            }
        }
        else
        {
            onPatternFailed.Invoke();
            foreach (var btn in _currentPattern)
            {
                btn.GetComponent<MeshRenderer>().material = failMaterial;
            }

            foreach (var line in lines)
            {
                line.GetComponent<MeshRenderer>().material = failMaterial;
            }
        }
        
        Invoke("Reset", 0.5f);
        //Reset();
    }

    public void Reset()
    {
        while (_currentPattern.Count > 0)
        {
            var currentButton = _currentPattern.Last();

            currentButton.GetComponent<Renderer>().material = notSelectedMaterial;

            _currentPattern.Remove(currentButton);
        }

        foreach (var line in lines)
        {
            line.transform.localScale = new Vector3(
                line.transform.localScale.x,
                line.transform.localScale.y,
                0.18f
            );
            line.SetActive(false);
        }
    }

    public void CreateLine()
    {
        int lineIndex = _currentPattern.Count - 2;

        var line = lines[lineIndex];

        line.SetActive(true);
        line.GetComponent<MeshRenderer>().material = selectedMaterial;

        var obj1 = _currentPattern[^2];
        var obj2 = _currentPattern[^1];

        line.transform.position = obj1.transform.position;
        line.transform.position = new Vector3(
                line.transform.position.x + ((obj2.transform.position.x - obj1.transform.position.x) / 2),
                line.transform.position.y + ((obj2.transform.position.y - obj1.transform.position.y) / 2),
                line.transform.position.z + 0.001f
            );

        float yDiff = (float)Math.Round(obj2.transform.position.y - obj1.transform.position.y, 2);
        float xDiff = (float)Math.Round(obj2.transform.position.x - obj1.transform.position.x, 2);
        float xyDiff = xDiff - yDiff;
        Debug.Log(xyDiff);

        float xOffset = 0f;
        
        // Angles and scale

        if (xDiff == 0f)
        {
            xOffset = 90;
            if (yDiff > 0.06 || yDiff < -0.06)
            {
                line.transform.localScale = new Vector3(
                    line.transform.localScale.x,
                    line.transform.localScale.y,
                    line.transform.localScale.z * 1.8f
                );
            }
        } 
        else if (yDiff == 0f)
        {
            xOffset = 0;
            if (xDiff > 0.06 || xDiff < -0.06)
            {
                line.transform.localScale = new Vector3(
                    line.transform.localScale.x,
                    line.transform.localScale.y,
                    line.transform.localScale.z * 2
                );
            }
        }
        else if (Math.Abs(xDiff) == Math.Abs(yDiff))
        {
            xOffset = (xDiff < 0 && yDiff < 0) || (xDiff > 0 && yDiff > 0) ? -37 : 37;
        }
        else if ((xyDiff < 0.02f && xyDiff > 0) || (xyDiff > 0.18f) || (xyDiff < -0.18f) || (xyDiff > -0.01 && xyDiff < 0))
        {
            xOffset = (xDiff < 0 && yDiff < 0) || (xDiff > 0 && yDiff > 0) ? -37 : 37;
            line.transform.localScale = new Vector3(
                line.transform.localScale.x,
                line.transform.localScale.y,
                line.transform.localScale.z * 2.5f
            );
        }
        else if (Math.Abs(yDiff) < Math.Abs(xDiff))
        {
            xOffset = (xDiff < 0 && yDiff < 0) || (xDiff > 0 && yDiff > 0) ? -18.5f : 18.5f;
            line.transform.localScale = new Vector3(
                line.transform.localScale.x,
                line.transform.localScale.y,
                line.transform.localScale.z * 2
            );
        }
        else if (Math.Abs(yDiff) > Math.Abs(xDiff))
        {
            xOffset = (xDiff < 0 && yDiff < 0) || (xDiff > 0 && yDiff > 0) ? -55.5f : 55.5f;
            line.transform.localScale = new Vector3(
                line.transform.localScale.x,
                line.transform.localScale.y,
                line.transform.localScale.z * 2
            );
        }
        
        line.transform.rotation = Quaternion.Euler(line.transform.rotation.x + xOffset, line.transform.rotation.y + 89, line.transform.rotation.z + 90);
    }
}
