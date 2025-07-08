using System.Collections.Generic;
using UnityEngine;

public class UITabView : MonoBehaviour
{
    [SerializeField]
    private List<UIButtonView> tabButtons;
    [SerializeField]
    private UIImageChanger changer;

    private List<System.Action> tabClickActions;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        tabClickActions = new List<System.Action>();
        InitializeButtonSubscriptions();
    }


    private void InitializeButtonSubscriptions()
    {

        // 各ボタンに、そのボタンがクリックされたときに呼び出す内部アクションを登録
        if (tabButtons != null)
        {
            for (int i = 0; i < tabButtons.Count; i++)
            {
                if (tabButtons[i] != null)
                {
                    // ボタンがクリックされたら、対応するインデックスの Action を呼び出すヘルパーメソッドを登録
                    int index = i; // クロージャのためにローカル変数にコピー
                    tabButtons[index].SubscribeToButtonClick(() => OnTabButtonClickedInternal(index));
                }
            }
        }
    }

    // ボタンがクリックされたときに実際に呼ばれる内部メソッド
    private void OnTabButtonClickedInternal(int buttonIndex)
    {
        // Debug.Log($"ボタン {buttonIndex} がクリックされました。");
        // tabClickActions リストに適切な Action が存在し、null でないことを確認して呼び出す
        if (buttonIndex >= 0 && buttonIndex < tabClickActions.Count && tabClickActions[buttonIndex] != null)
        {
            tabClickActions[buttonIndex]?.Invoke();
        }
        else
        {
            Debug.LogWarning($"OnTabButtonClickedInternal: buttonIndex {buttonIndex} に対応するアクションが見つからないかnullです。");
        }
    }

    public void SetImage(string fileId)
    {
        changer.ChangeImageImmediate(fileId);
    }



    public void SubscribeToTabClick(List<System.Action> listeners)
    {
        // リスナーの数がボタンの数と一致しない場合は処理を中断
        if (tabButtons.Count != listeners.Count)
        {
            Debug.LogError("SubscribeToTabClick: リスナーの数とタブボタンの数が一致しません。");
            return;
        }

        tabClickActions.Clear();
        tabClickActions.AddRange(listeners);
    }
}
