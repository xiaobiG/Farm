using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Text nameText;
    private Text countText;
    private Button buildButton;
    private GameObject prefab;


    private void Start()
    {
        nameText = transform.Find("Name").GetComponent<Text>();
        countText = transform.Find("Count").GetComponent<Text>();
        buildButton = transform.Find("Button").GetComponent<Button>();
        buildButton.onClick.AddListener(BuildButtonClick);

        prefab = Resources.Load<GameObject>("Build_Shop");
    }

    private void BuildButtonClick()
    {
        Player_C.Instance.Build(prefab);
        UI_BuildPanel.Instance.SetActive(false);
    }

}
