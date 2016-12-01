using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    private GameController GC;

    public GameObject GoPanel;
    public GameObject GoDebugText;

    private Text _textDebug;

    void Start()
    {
        try
        {
            GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        catch (Exception)
        {
            throw;
        }

#if DEBUG 
        _textDebug = GoDebugText.GetComponent<Text>();
#else
        GoPanel.SetActive(false);
#endif
    }

#if DEBUG

    void Update()
    {
    }

    void LateUpdate()
    {
        if (GC.DebugInfo.Count > 0)
        {
            GoPanel.SetActive(true);
            string fullText = default (string);

            foreach (KeyValuePair<string, string> keyValuePair in GC.DebugInfo)
            {
                fullText += keyValuePair.Key + ": " + keyValuePair.Value + "\n";
            }

            _textDebug.text = fullText;

            GC.DebugInfo.Clear();
        }
        else
        {
            GoPanel.SetActive(false);
        }
    }
#endif
}