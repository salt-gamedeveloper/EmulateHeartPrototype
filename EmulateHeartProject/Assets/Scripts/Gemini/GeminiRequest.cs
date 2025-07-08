using System;

[Serializable]
public class SimpleGeminiRequest
{
    public Content[] contents;

    public SimpleGeminiRequest(string text)
    {
        contents = new Content[]
        {
            new Content { role = "user", parts = new Part[] { new Part { text = text } } }
        };
    }
}

[Serializable]
public class Content
{
    public string role;
    public Part[] parts;
}

[Serializable]
public class Part
{
    public string text;
}

[Serializable]
public class GeminiResponse
{
    public Candidate[] candidates;
    public PromptFeedback promptFeedback;
}

[Serializable]
public class Candidate
{
    public Content content;
    public SafetyRating[] safetyRatings;
}

[Serializable]
public class SafetyRating
{
    public string category;
    public string probability;
}

[Serializable]
public class PromptFeedback
{
    public SafetyRating[] safetyRatings;
}

[Serializable]
public class GeminiErrorResponse
{
    public ErrorDetail error;
}

[Serializable]
public class ErrorDetail
{
    public int code;
    public string message;
    public string status;
}

[Serializable]
public class GeminiAPITestJson
{
    public string testText;
}