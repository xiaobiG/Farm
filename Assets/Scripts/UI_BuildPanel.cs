using UnityEngine;
using UnityEngine.UI;

public class UI_BuildPanel : MonoBehaviour
{
    public static UI_BuildPanel Instance;
    private Button CloseButton;

    private void Awake()
    {
        Instance = this;
    }

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
