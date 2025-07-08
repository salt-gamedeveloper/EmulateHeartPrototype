public interface ICallbackUIView : IUIView
{
    /// <summary>
    /// コールバック付きでUIを表示します。
    /// </summary>
    /// <param name="onComplete">表示アニメーション完了時に呼び出すコールバック</param>
    void ShowWithCallback(System.Action onComplete);

    /// <summary>
    /// コールバック付きでUIを非表示にします。
    /// </summary>
    /// <param name="onComplete">非表示アニメーション完了時に呼び出すコールバック</param>
    void HideWithCallback(System.Action onComplete);

    /// <summary>
    /// 表示アニメーションをスキップし、UIを即時に表示状態にします。
    /// </summary>
    void ShowImmediate();

    /// <summary>
    /// 非表示アニメーションをスキップし、UIを即時に非表示状態にします。
    /// </summary>
    void HideImmediate();
}
