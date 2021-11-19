using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UI_BuildPanel buildPanel;
    private UI_MainPanel mainPanel;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        mainPanel = transform.Find("MainPanel").GetComponent<UI_MainPanel>();
        buildPanel = transform.Find("BuildPanel").GetComponent<UI_BuildPanel>();
    }

    // 打开建造面板
    public void ShowBuildPanel()
    {
        buildPanel.SetActive(true);
    }
}
