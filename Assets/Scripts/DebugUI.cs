using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugUI : MonoBehaviour
{
    public GameObject GoPanel;
    public GameObject GoDebugText;

    private Text _textDebug;

    void Start()
    {
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
        if (GameData.DebugInfo.Count > 0)
        {
            GoPanel.SetActive(true);
            string fullText = default (string);

            foreach (KeyValuePair<string, string> keyValuePair in GameData.DebugInfo)
            {
                fullText += keyValuePair.Key + ": " + keyValuePair.Value + "\n";
            }

            _textDebug.text = fullText;

            GameData.DebugInfo.Clear();
        }
        else
        {
            GoPanel.SetActive(false);
        }
    }
#endif
}