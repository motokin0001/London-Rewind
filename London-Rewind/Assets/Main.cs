using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour
{
    public float rotationSpeed = 360f; // 1秒で360度回転
    private bool isRotating = false; // 回転するかどうかのフラグ
    public Text timeText; // テキスト表示用のUIオブジェクト
    public Button startButton; // スタートボタン
    public Text resultText; // 結果表示用のテキスト
    public Button retryButton; // リトライボタン
    private int randomHour; // ランダムな時間

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);
        retryButton.gameObject.SetActive(false); // リトライボタンを非表示
        resultText.text = "";
    }

    void OnStartButtonClicked()
    {
        randomHour = Random.Range(1, 13); // 1から12までのランダムな数を生成
        timeText.text = $"{randomHour}時";
        startButton.gameObject.SetActive(false); // スタートボタンを非表示
        StartCoroutine(StartRotationAfterDelay(5f)); // 10秒後に回転を開始
    }

    IEnumerator StartRotationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isRotating = true;
    }

    void Update()
    {
        // スペースキーが押されたら回転を停止
        if (Input.GetKeyDown(KeyCode.Space) && isRotating)
        {
            isRotating = false;
            CheckResult();
        }

        // 回転処理
        if (isRotating)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime); // 反時計回り
        }

        // 現在の回転角度に基づいて時間を計算
        float zRotation = transform.eulerAngles.z;
        int hours = Mathf.FloorToInt((12 - (zRotation / 30f)) % 12);
        if (hours == 0) hours = 12;

        // 時間をテキストに表示
        timeText.text = string.Format("探偵：{0}時に戻ってくれ", randomHour);
    }

    void CheckResult()
    {
        float zRotation = transform.eulerAngles.z;
        int stoppedHour = Mathf.FloorToInt((12 - (zRotation / 30f)) % 12);
        if (stoppedHour == 0) stoppedHour = 12;

        if (stoppedHour == randomHour)
        {
            resultText.text = "Good";
        }
        else
        {
            resultText.text = "Miss";
        }

        retryButton.gameObject.SetActive(true); // リトライボタンを表示
    }

    void OnRetryButtonClicked()
    {
        retryButton.gameObject.SetActive(false); // リトライボタンを非表示
        startButton.gameObject.SetActive(true); // スタートボタンを表示
        resultText.text = "";
        timeText.text = "";
        isRotating = false;
        transform.rotation = Quaternion.identity; // 針の回転をリセット
    }
}
