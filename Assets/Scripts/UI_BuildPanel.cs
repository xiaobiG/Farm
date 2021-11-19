using UnityEngine;
using UnityEngine.UI;

public class UI_BuildPanel : MonoBehaviour
{

    private Button CloseButton;

    void Start()
    {
        CloseButton = transform.Find("BG/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(CloseButtonClick);
        CloseButtonClick();
    }

    private void CloseButtonClick()
    {
        SetActive(false);
    }

    // 切换显示
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
