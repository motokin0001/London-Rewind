using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LidController : MonoBehaviour
{
    public RectTransform Lid;
    public Button STARTButton;
    public Button RETRYButton;
    private bool isMenuVisible = false;
    public Vector3 hiddenPosition;
    public Vector3 visiblePosition;



    void Start()
    {
        // メニューを隠れた位置に設定
        Lid.anchoredPosition = hiddenPosition;

        // ボタンにクリックイベントを登録
        STARTButton.onClick.AddListener(ToggleMenu);
        RETRYButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        if (isMenuVisible)
        {
            // メニューを隠す
            Lid.DOAnchorPos(hiddenPosition, 0.5f);
        }
        else
        {
            // メニューを表示
            Lid.DOAnchorPos(visiblePosition, 0.5f);
        }
        isMenuVisible = !isMenuVisible;
        

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Lid.DOAnchorPos(hiddenPosition, 0.5f);
        }
    }
}