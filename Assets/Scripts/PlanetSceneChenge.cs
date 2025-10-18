using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSceneChenge : MonoBehaviour
{
    // シーン遷移に使用する変数
    [Header("遷移先のシーン名")]
    [SerializeField] private string nextSceneName; // 遷移先のシーン名
    [Header("遷移までの待機時間（秒）")]
    [SerializeField] private float delayTime; // 遷移までの待機時間（秒）
     // キーと対応するシーン名を設定するためのDictionary
   [System.Serializable]
   public struct KeyScenePair
   {
        [Header("シーン遷移をさせるキーとシーン名")]
       public KeyCode key;       // 対応するキー
       public string sceneName;  // 遷移先のシーン名
   }
   [SerializeField]
   private List<KeyScenePair> keyScenePairs = new List<KeyScenePair>();
   // Singletonパターンでこのオブジェクトが1つだけ存在するようにする
   private static PlanetSceneChenge instance;
   void Awake()
   {
       // Singletonの設定
       if (instance == null)
       {
           instance = this;
           DontDestroyOnLoad(gameObject); // シーン間でオブジェクトを保持
       }
       else
       {
           Destroy(gameObject); // 既に存在する場合は破棄
       }
   }
   void Update()
   {
       // 設定されたキーを監視してシーン遷移
       foreach (var pair in keyScenePairs)
       {
           if (Input.GetKeyDown(pair.key))
           {
               LoadScene(pair.sceneName);
               break;
           }
       }
   }
   private void LoadScene(string sceneName)
   {
       if (!string.IsNullOrEmpty(sceneName))
       {
           SceneManager.LoadScene(sceneName);
       }
       else
       {
           Debug.LogWarning("シーン名が設定されていません。");
       }
   }

    // シーンが読み込まれたときに呼ばれる
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer();
    }

    // タイマーをリセットする
    private void ResetTimer()
    {
        delayTime = 0f;
    }
    void Start()
    {
        // コルーチンを開始
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        // 指定秒数待機
        yield return new WaitForSeconds(delayTime);

        // シーンをロード
        SceneManager.LoadScene(nextSceneName);
    }
}
