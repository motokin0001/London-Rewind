using UnityEngine;
using UnityEngine.UI;

public class ClockHand : MonoBehaviour
{
    public float rotationSpeed = 360f; // 1秒で360度回転
    private bool isRotating = true; // 回転するかどうかのフラグ
    public Text timeText; // テキスト表示用のUIオブジェクト

    void Update()
    {
        // クリックされたら回転を停止
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = !isRotating;
        }

        // 回転処理
        if (isRotating)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime); // 反時計回り
        }

        // 現在の回転角度に基づいて時間を計算
        float zRotation = transform.eulerAngles.z;
        int hours = Mathf.FloorToInt((12 - (zRotation / 30f)) % 12);
        int minutes = Mathf.FloorToInt(60 - ((zRotation % 30) * 2f));

        // 12時間表記に調整
        if (hours == 0) hours = 12;
        if (minutes == 60) minutes = 0; // 分が60の場合は0にリセット

        // 時間をテキストに表示
        timeText.text = string.Format("{0:D2}:{1:D2}", hours, minutes);
    }
}
