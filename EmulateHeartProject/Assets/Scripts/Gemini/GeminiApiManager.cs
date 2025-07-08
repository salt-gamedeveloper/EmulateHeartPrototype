using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

//TODO: クラスの分離。再設計
public class GeminiApiManager : MonoBehaviour
{
    private GeminiRequestBuilder requestBuilder;

    private string geminiApiKey;
    private string geminiModel;

    private void Awake()
    {
        requestBuilder = new GeminiRequestBuilder();
        geminiApiKey = "YOUR_GEMINI_API_KEY";
        geminiModel = "";
        SetGeminiModel("gemini-1.5-flash");
    }

    public void SetGeminiApiKey(string apiKey, Action<bool, string> onComplete)
    {
        geminiApiKey = apiKey;
        TestGeminiConnection(onComplete);
    }

    public void SetGeminiModel(string model)
    {
        geminiModel = model;
    }

    //TODO: CharacterTypeに合わせたテストを行う
    /// <summary>
    /// Geminiとの接続テストを行う（通常のテキスト応答を期待し、スキーマは含まない）
    /// </summary>
    /// <param name="onComplete">応答を処理するコールバック (成功/失敗, 応答テキスト)</param>
    public void TestGeminiConnection(Action<bool, string> onComplete)
    {
        string question = @"
API通信テスト。もし成功していれば「こんにちは（改行）テスト成功です」とのみ返信してください。
返信形式はJsonで
{
""testText"": ""<string>""
}
";

        if (string.IsNullOrEmpty(geminiApiKey) || geminiApiKey == "YOUR_GEMINI_API_KEY")
        {
            onComplete?.Invoke(false, "APIキーが設定されていないか、デフォルトのままです。有効なAPIキーを設定してください。");
            return;
        }

        if (string.IsNullOrEmpty(geminiModel))
        {
            onComplete?.Invoke(false, "Geminiモデルが設定されていません。");
            return;
        }

        SimpleGeminiRequest testRequest = new SimpleGeminiRequest(question); // スキーマを含まないことを明示
        StartCoroutine(SendToGemini(testRequest, onComplete)); // テキスト応答用コルーチン
    }

    /// <summary>
    /// 通常のチャットリクエストをGeminiに送信し、JSON形式の構造化応答を受け取る
    /// </summary>
    /// <param name="question">ユーザーからの質問</param>
    /// <param name="onComplete">応答を処理するコールバック (成功/失敗, ChatResponseDTOオブジェクト)</param>
    public void ChatGeminiConnection(string question, Action<bool, ChatResponseDTO> onComplete)
    {
        string promptText = requestBuilder.BuildChatRequest(question);
        // ★変更点：スキーマを含む SimpleGeminiRequest を作成するコンストラクタを呼び出す★
        SimpleGeminiRequest requestObj = new SimpleGeminiRequest(promptText); // スキーマを含めることを明示

        StartCoroutine(SendToGeminiForChatDTO(requestObj, onComplete)); // JSON構造化応答用コルーチン
    }

    /// <summary>
    /// チャット開始時のリクエストをGeminiに送信し、JSON形式の構造化応答を受け取る
    /// </summary>
    /// <param name="onComplete">応答を処理するコールバック (成功/失敗, ChatResponseDTOオブジェクト)</param>
    public void ChatStartGeminiConnection(Action<bool, ChatResponseDTO> onComplete)
    {
        string promptText = requestBuilder.BuildChatStartRequest();
        // ★変更点：スキーマを含む SimpleGeminiRequest を作成するコンストラクタを呼び出す★
        SimpleGeminiRequest requestObj = new SimpleGeminiRequest(promptText); // スキーマを含めることを明示

        StartCoroutine(SendToGeminiForChatDTO(requestObj, onComplete)); // JSON構造化応答用コルーチン
    }

    /// <summary>
    /// Geminiにリクエストを送信し、JSON形式の構造化応答を受け取る
    /// </summary>
    /// <param name="promptType">送信するプロンプトのタイプ (ChatStart, DefaultChat, ChatEndなど)</param>
    /// <param name="question">ユーザーからの質問 (DefaultChat の場合のみ使用)</param>
    /// <param name="onComplete">応答を処理するコールバック (成功/失敗, ChatResponseDTOオブジェクト)</param>
    public void RequestGeminiConnection(PromptType promptType, string question, Action<bool, ChatResponseDTO> onComplete)
    {
        string promptText;

        switch (promptType)
        {
            case PromptType.ChatStart:
                promptText = requestBuilder.BuildChatStartRequest();
                break;
            case PromptType.Chat: // または単にChatなど
            case PromptType.ChatTimeProgress:
                promptText = requestBuilder.BuildChatRequest(question);
                Debug.Log(promptText);
                break;
            case PromptType.ChatRechooseOptions:
                promptText = requestBuilder.BuildChatRechooseOptionsRequest(question);
                break;
            case PromptType.ChatEnd:
                promptText = requestBuilder.BuildChatEndRequest(question);
                break;
            // その他のPromptTypeがあればここに追加
            default:
                Debug.LogError($"未定義のPromptType: {promptType}");
                onComplete?.Invoke(false, null); // 失敗を通知
                return;
        }

        // スキーマを含む SimpleGeminiRequest を作成するコンストラクタを呼び出す
        SimpleGeminiRequest requestObj = new SimpleGeminiRequest(promptText);

        // JSON構造化応答用コルーチンを開始
        StartCoroutine(SendToGeminiForChatDTO(requestObj, onComplete));
    }

    IEnumerator SendToGemini(SimpleGeminiRequest SimpleGeminiRequest, Action<bool, string> onResult)
    {
        string url = $"https://generativelanguage.googleapis.com/v1beta/models/{geminiModel}:generateContent?key={geminiApiKey}";
        string jsonPayload = JsonUtility.ToJson(SimpleGeminiRequest);

        //Debug.Log($"[DEBUG] Sent JSON Payload from TestGeminiConnection: {jsonPayload}");

        byte[] postData = Encoding.UTF8.GetBytes(jsonPayload);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                string errorMessage = request.error;

                try
                {
                    GeminiErrorResponse errorResponse = JsonUtility.FromJson<GeminiErrorResponse>(request.downloadHandler.text);
                    if (errorResponse != null && errorResponse.error != null)
                    {
                        errorMessage = $"APIエラー: {errorResponse.error.message} (コード: {errorResponse.error.code})";
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"エラーレスポンス解析失敗: {e.Message}");
                }

                onResult?.Invoke(false, errorMessage);
                yield break;
            }

            try
            {
                string json = request.downloadHandler.text;
                GeminiResponse response = JsonUtility.FromJson<GeminiResponse>(json);

                if (response == null || response.candidates == null || response.candidates.Length == 0)
                {
                    onResult?.Invoke(false, "Geminiからの応答がありませんでした。");
                    yield break;
                }

                Candidate firstCandidate = response.candidates[0];
                string rawJsonOutput = firstCandidate?.content?.parts?[0]?.text;

                if (string.IsNullOrEmpty(rawJsonOutput))
                {
                    onResult?.Invoke(false, $"Geminiの応答にJSONテキストが含まれていませんでした。(Test JSON Prompt). Content parts: {firstCandidate?.content?.parts?.Length ?? 0}");
                    yield break;
                }

                // ★★★ JSON抽出ロジック（Test JSON Prompt用） ★★★
                string extractedJson = "";
                Match match = Regex.Match(rawJsonOutput, @"```json\s*(\{.*\})\s*```", RegexOptions.Singleline);

                if (match.Success)
                {
                    extractedJson = match.Groups[1].Value; // Extract the JSON part itself
                    extractedJson = extractedJson.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("\u00A0", "").Trim();
                    extractedJson = Regex.Replace(extractedJson, @"\s+", ""); // Final minify
                }
                else
                {
                    //Debug.LogWarning("JSONコードブロックが見つかりませんでした。(Test Text Response) 生の出力をパース試行します。");
                    extractedJson = rawJsonOutput.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("\u00A0", "").Trim();
                    extractedJson = Regex.Replace(extractedJson, @"\s+", "");
                }
                // ★★★ JSON抽出ロジックここまで ★★★

                // ★★★ ここで GeminiAPITestJson にパース！ ★★★
                //Debug.Log($"Attempting to parse GeminiAPITestJson from: {extractedJson}");
                GeminiAPITestJson testJsonParsed = JsonUtility.FromJson<GeminiAPITestJson>(extractedJson);


                if (testJsonParsed != null)
                {
                    //Debug.Log($"GeminiAPITestJson successfully parsed! testText: {testJsonParsed.testText}");
                    onResult?.Invoke(true, testJsonParsed.testText); // Success with parsed value
                }
                else
                {
                    onResult?.Invoke(false, $"GeminiAPITestJsonのパースに失敗しました。受信JSON: {extractedJson}");
                }
            }
            catch (Exception e)
            {
                onResult?.Invoke(false, $"レスポンス解析エラー (Test JSON Prompt): {e.Message}. 問題のJSON: {request.downloadHandler.text}");
            }
        }
    }

    /// <summary>
    /// Gemini APIにリクエストを送信するコルーチン（プロンプトによるJSON応答用）
    /// </summary>
    /// <param name="simpleRequest">送信するGeminiリクエストオブジェクト（プロンプトにJSON形式の指示を含む）</param>
    /// <param name="onResult">結果を処理するコールバック (成功/失敗, ChatResponseDTOオブジェクト)</param>
    IEnumerator SendToGeminiForChatDTO(SimpleGeminiRequest simpleRequest, Action<bool, ChatResponseDTO> onResult)
    {
        int retryCount = 0;
        const int maxRetries = 1;

        while (true)
        {
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{geminiModel}:generateContent?key={geminiApiKey}";
            string jsonPayload = JsonUtility.ToJson(simpleRequest);
            byte[] postData = Encoding.UTF8.GetBytes(jsonPayload);

            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            using (UploadHandlerRaw uploadHandler = new UploadHandlerRaw(postData))
            using (DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer())
            {
                request.uploadHandler = uploadHandler;
                request.downloadHandler = downloadHandler;
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    string errorMessage = request.error;
                    try
                    {
                        GeminiErrorResponse errorResponse = JsonUtility.FromJson<GeminiErrorResponse>(downloadHandler.text);
                        if (errorResponse?.error != null)
                        {
                            errorMessage = $"APIエラー: {errorResponse.error.message} (コード: {errorResponse.error.code})";
                            Debug.LogError($"API Error Raw Response (Chat DTO): {downloadHandler.text}");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"エラーレスポンス解析失敗 (Chat DTO): {e.Message}. Raw Error Text: {downloadHandler.text}");
                    }

                    onResult?.Invoke(false, null);
                    yield break;
                }

                string json = downloadHandler.text;
                string extractedJson = "";
                try
                {
                    GeminiResponse response = JsonUtility.FromJson<GeminiResponse>(json);
                    if (response?.candidates == null || response.candidates.Length == 0)
                    {
                        Debug.LogError("Geminiからの応答がありませんでした、または候補が空です。(Chat DTO)");
                        onResult?.Invoke(false, null);
                        yield break;
                    }

                    string rawJsonOutput = response.candidates[0]?.content?.parts?[0]?.text;

                    if (string.IsNullOrEmpty(rawJsonOutput))
                    {
                        Debug.LogError($"Geminiの応答にJSONテキストが含まれていませんでした。(Chat DTO)");
                        onResult?.Invoke(false, null);
                        yield break;
                    }

                    Match match = Regex.Match(rawJsonOutput, @"```json\s*(\{.*\})\s*```", RegexOptions.Singleline);
                    if (match.Success)
                    {
                        extractedJson = match.Groups[1].Value.Trim().Replace("\u00A0", "").Replace("\u200B", "");
                    }
                    else
                    {
                        extractedJson = rawJsonOutput.Trim().Replace("\u00A0", "").Replace("\u200B", "");
                    }

                    ChatResponseDTO chatResponse = JsonConvert.DeserializeObject<ChatResponseDTO>(extractedJson);

                    if (chatResponse != null)
                    {
                        onResult?.Invoke(true, chatResponse);
                        yield break; // 正常終了
                    }
                    else
                    {
                        throw new Exception("ChatResponseDTOのパースに失敗しました。nullでした。");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"レスポンス解析エラー (ChatResponseDTO): {e.Message}");

                    if (retryCount < maxRetries)
                    {
                        retryCount++;
                        Debug.LogWarning("レスポンス解析に失敗したため、リトライします...");
                        continue; // whileループの先頭へ
                    }
                    else
                    {
                        Debug.LogError("リトライ上限に達しました。解析失敗。(Chat DTO)");
                        onResult?.Invoke(false, null);
                        yield break;
                    }
                }
            }
        }
    }
}


