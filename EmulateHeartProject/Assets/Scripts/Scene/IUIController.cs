/// <summary>
/// UIの制御を行うためのインターフェースです。
/// 各UI要素はこのインターフェースを実装することで、共通のライフサイクル管理や表示操作を統一的に扱うことができます。
/// </summary>
public interface IUIController
{
    /// <summary>
    /// UIの初期化処理を行います。
    /// 通常は内部状態の初期化やイベント登録などを行います。
    /// </summary>
    void Initialize();

    /// <summary>
    /// ViewModelを注入します。
    /// MVVMパターンに従い、UIとデータの接続に用いられます。
    /// </summary>
    /// <param name="viewModel">UIにバインドするViewModel。</param>
    void InjectViewModel(object viewModel);

    /// <summary>
    /// UIを開きます（表示と初期化のトリガーなど）。
    /// アニメーションやデータ読み込みなども含まれる場合があります。
    /// </summary>
    void Open();

    /// <summary>
    /// UIを閉じます。
    /// アニメーションや後処理を伴うことがあります。
    /// </summary>
    void Close();

    /// <summary>
    /// UIを表示します。
    /// 一時的な表示/非表示の切り替えに使用します（Openとは異なる意味で）。
    /// </summary>
    void Show();

    /// <summary>
    /// UIを非表示にします。
    /// 一時的に画面から隠す用途で使用されます。
    /// </summary>
    void Hide();
}