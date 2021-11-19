using UnityEngine;
using UnityEngine.UI;

public class UI_MainPanel : MonoBehaviour
{

    private Button BuildButton;

    void Start()
    {
        BuildButton = transform.Find("BuildButton").GetComponent<Button>();
        BuildButton.onClick.AddListener(BuildButtonClick);
    }

    private void BuildButtonClick()
    {
        UIManager.Instance.ShowBuildPanel();
    }
}
