using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

//TODO: �N���X�̕����B�Đ݌v
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

    //TODO: CharacterType�ɍ��킹���e�X�g���s��
    /// <summary>
    /// Gemini�Ƃ̐ڑ��e�X�g���s���i�ʏ�̃e�L�X�g���������҂��A�X�L�[�}�͊܂܂Ȃ��j
    /// </summary>
    /// <param name="onComplete">��������������R�[���o�b�N (����/���s, �����e�L�X�g)</param>
    public void TestGeminiConnection(Action<bool, string> onComplete)
    {
        string question = @"
API�ʐM�e�X�g�B�����������Ă���΁u����ɂ��́i���s�j�e�X�g�����ł��v�Ƃ̂ݕԐM���Ă��������B
�ԐM�`����Json��
{
""testText"": ""<string>""
}
";

        if (string.IsNullOrEmpty(geminiApiKey) || geminiApiKey == "YOUR_GEMINI_API_KEY")
        {
            onComplete?.Invoke(false, "API�L�[���ݒ肳��Ă��Ȃ����A�f�t�H���g�̂܂܂ł��B�L����API�L�[��ݒ肵�Ă��������B");
            return;
        }

        if (string.IsNullOrEmpty(geminiModel))
        {
            onComplete?.Invoke(false, "Gemini���f�����ݒ肳��Ă��܂���B");
            return;
        }

        SimpleGeminiRequest testRequest = new SimpleGeminiRequest(question); // �X�L�[�}���܂܂Ȃ����Ƃ𖾎�
        StartCoroutine(SendToGemini(testRequest, onComplete)); // �e�L�X�g�����p�R���[�`��
    }

    /// <summary>
    /// �ʏ�̃`���b�g���N�G�X�g��Gemini�ɑ��M���AJSON�`���̍\�����������󂯎��
    /// </summary>
    /// <param name="question">���[�U�[����̎���</param>
    /// <param name="onComplete">��������������R�[���o�b�N (����/���s, ChatResponseDTO�I�u�W�F�N�g)</param>
    public void ChatGeminiConnection(string question, Action<bool, ChatResponseDTO> onComplete)
    {
        string promptText = requestBuilder.BuildChatRequest(question);
        // ���ύX�_�F�X�L�[�}���܂� SimpleGeminiRequest ���쐬����R���X�g���N�^���Ăяo����
        SimpleGeminiRequest requestObj = new SimpleGeminiRequest(promptText); // �X�L�[�}���܂߂邱�Ƃ𖾎�

        StartCoroutine(SendToGeminiForChatDTO(requestObj, onComplete)); // JSON�\���������p�R���[�`��
    }

    /// <summary>
    /// �`���b�g�J�n���̃��N�G�X�g��Gemini�ɑ��M���AJSON�`���̍\�����������󂯎��
    /// </summary>
    /// <param name="onComplete">��������������R�[���o�b�N (����/���s, ChatResponseDTO�I�u�W�F�N�g)</param>
    public void ChatStartGeminiConnection(Action<bool, ChatResponseDTO> onComplete)
    {
        string promptText = requestBuilder.BuildChatStartRequest();
        // ���ύX�_�F�X�L�[�}���܂� SimpleGeminiRequest ���쐬����R���X�g���N�^���Ăяo����
        SimpleGeminiRequest requestObj = new SimpleGeminiRequest(promptText); // �X�L�[�}���܂߂邱�Ƃ𖾎�

        StartCoroutine(SendToGeminiForChatDTO(requestObj, onComplete)); // JSON�\���������p�R���[�`��
    }

    /// <summary>
    /// Gemini�Ƀ��N�G�X�g�𑗐M���AJSON�`���̍\�����������󂯎��
    /// </summary>
    /// <param name="promptType">���M����v�����v�g�̃^�C�v (ChatStart, DefaultChat, ChatEnd�Ȃ�)</param>
    /// <param name="question">���[�U�[����̎��� (DefaultChat �̏ꍇ�̂ݎg�p)</param>
    /// <param name="onComplete">��������������R�[���o�b�N (����/���s, ChatResponseDTO�I�u�W�F�N�g)</param>
    public void RequestGeminiConnection(PromptType promptType, string question, Action<bool, ChatResponseDTO> onComplete)
    {
        string promptText;

        switch (promptType)
        {
            case PromptType.ChatStart:
                promptText = requestBuilder.BuildChatStartRequest();
                break;
            case PromptType.Chat: // �܂��͒P��Chat�Ȃ�
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
            // ���̑���PromptType������΂����ɒǉ�
            default:
                Debug.LogError($"����`��PromptType: {promptType}");
                onComplete?.Invoke(false, null); // ���s��ʒm
                return;
        }

        // �X�L�[�}���܂� SimpleGeminiRequest ���쐬����R���X�g���N�^���Ăяo��
        SimpleGeminiRequest requestObj = new SimpleGeminiRequest(promptText);

        // JSON�\���������p�R���[�`�����J�n
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
                        errorMessage = $"API�G���[: {errorResponse.error.message} (�R�[�h: {errorResponse.error.code})";
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"�G���[���X�|���X��͎��s: {e.Message}");
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
                    onResult?.Invoke(false, "Gemini����̉���������܂���ł����B");
                    yield break;
                }

                Candidate firstCandidate = response.candidates[0];
                string rawJsonOutput = firstCandidate?.content?.parts?[0]?.text;

                if (string.IsNullOrEmpty(rawJsonOutput))
                {
                    onResult?.Invoke(false, $"Gemini�̉�����JSON�e�L�X�g���܂܂�Ă��܂���ł����B(Test JSON Prompt). Content parts: {firstCandidate?.content?.parts?.Length ?? 0}");
                    yield break;
                }

                // ������ JSON���o���W�b�N�iTest JSON Prompt�p�j ������
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
                    //Debug.LogWarning("JSON�R�[�h�u���b�N��������܂���ł����B(Test Text Response) ���̏o�͂��p�[�X���s���܂��B");
                    extractedJson = rawJsonOutput.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("\u00A0", "").Trim();
                    extractedJson = Regex.Replace(extractedJson, @"\s+", "");
                }
                // ������ JSON���o���W�b�N�����܂� ������

                // ������ ������ GeminiAPITestJson �Ƀp�[�X�I ������
                //Debug.Log($"Attempting to parse GeminiAPITestJson from: {extractedJson}");
                GeminiAPITestJson testJsonParsed = JsonUtility.FromJson<GeminiAPITestJson>(extractedJson);


                if (testJsonParsed != null)
                {
                    //Debug.Log($"GeminiAPITestJson successfully parsed! testText: {testJsonParsed.testText}");
                    onResult?.Invoke(true, testJsonParsed.testText); // Success with parsed value
                }
                else
                {
                    onResult?.Invoke(false, $"GeminiAPITestJson�̃p�[�X�Ɏ��s���܂����B��MJSON: {extractedJson}");
                }
            }
            catch (Exception e)
            {
                onResult?.Invoke(false, $"���X�|���X��̓G���[ (Test JSON Prompt): {e.Message}. ����JSON: {request.downloadHandler.text}");
            }
        }
    }

    /// <summary>
    /// Gemini API�Ƀ��N�G�X�g�𑗐M����R���[�`���i�v�����v�g�ɂ��JSON�����p�j
    /// </summary>
    /// <param name="simpleRequest">���M����Gemini���N�G�X�g�I�u�W�F�N�g�i�v�����v�g��JSON�`���̎w�����܂ށj</param>
    /// <param name="onResult">���ʂ���������R�[���o�b�N (����/���s, ChatResponseDTO�I�u�W�F�N�g)</param>
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
                            errorMessage = $"API�G���[: {errorResponse.error.message} (�R�[�h: {errorResponse.error.code})";
                            Debug.LogError($"API Error Raw Response (Chat DTO): {downloadHandler.text}");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"�G���[���X�|���X��͎��s (Chat DTO): {e.Message}. Raw Error Text: {downloadHandler.text}");
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
                        Debug.LogError("Gemini����̉���������܂���ł����A�܂��͌�₪��ł��B(Chat DTO)");
                        onResult?.Invoke(false, null);
                        yield break;
                    }

                    string rawJsonOutput = response.candidates[0]?.content?.parts?[0]?.text;

                    if (string.IsNullOrEmpty(rawJsonOutput))
                    {
                        Debug.LogError($"Gemini�̉�����JSON�e�L�X�g���܂܂�Ă��܂���ł����B(Chat DTO)");
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
                        yield break; // ����I��
                    }
                    else
                    {
                        throw new Exception("ChatResponseDTO�̃p�[�X�Ɏ��s���܂����Bnull�ł����B");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"���X�|���X��̓G���[ (ChatResponseDTO): {e.Message}");

                    if (retryCount < maxRetries)
                    {
                        retryCount++;
                        Debug.LogWarning("���X�|���X��͂Ɏ��s�������߁A���g���C���܂�...");
                        continue; // while���[�v�̐擪��
                    }
                    else
                    {
                        Debug.LogError("���g���C����ɒB���܂����B��͎��s�B(Chat DTO)");
                        onResult?.Invoke(false, null);
                        yield break;
                    }
                }
            }
        }
    }
}


