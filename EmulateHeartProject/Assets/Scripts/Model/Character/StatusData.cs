using System;
using UnityEngine;

[System.Serializable]
public class StatusData
{
    // 知的探求心: ない(-100) ～ 高い(+100)
    public int intellectualCuriosity;

    // 想像力: ない(-100) ～ 豊か(+100)
    public int imagination;

    // 良心性: ない(-100) ～ 強い(+100)
    public int conscientiousness;

    // 責任感: ない(-100) ～ 強い(+100)
    public int responsibility;

    // 社交性: 内向的(-100) ～ 外向的(+100)
    public int sociability;

    // 積極性: 消極的(-100) ～ 積極的(+100)
    public int proactiveness;

    // 共感性: ない(-100) ～ 強い(+100)
    public int empathy;

    // 思いやり: ない(-100) ～ 強い(+100)
    public int compassion;

    // ストレス耐性: ない(-100) ～ 強い(+100)
    public int stressTolerance;

    // 不安: ない(-100) ～ 強い(+100)
    public int anxiety;

    // 知性: ない(-100) ～ 高い(+100)
    public int intelligence;

    // 人間らしさ：低い(-100) ～ 高い(+100)
    public int humanLikeness;

    // 依存度：ない(-100) ～ 強い(+100)
    public int dependency;

    // 好感度：嫌い(-100) ～ 好き(+100) 
    public int affection;

    public int normalProbability;
    public int joyProbability;
    public int angerProbability;
    public int sorrowProbability;
    public int funProbability;


    // 範囲定義
    private static readonly int MinValue = -100;
    private static readonly int MaxValue = 100;

    /// <summary>
    /// 各ステータスの値を-100?100の範囲に制限
    /// </summary>
    public void AllClamp()
    {
        intellectualCuriosity = Clamp(intellectualCuriosity);
        imagination = Clamp(imagination);
        conscientiousness = Clamp(conscientiousness);
        responsibility = Clamp(responsibility);
        sociability = Clamp(sociability);
        proactiveness = Clamp(proactiveness);
        empathy = Clamp(empathy);
        compassion = Clamp(compassion);
        stressTolerance = Clamp(stressTolerance);
        anxiety = Clamp(anxiety);
        intelligence = Clamp(intelligence);
        humanLikeness = Clamp(humanLikeness);
        dependency = Clamp(dependency);
        affection = Clamp(affection);
    }

    private static int Clamp(int value)
    {
        return Math.Clamp(value, MinValue, MaxValue);
    }

    /// <summary>
    /// ステータスを表示（デバッグ用）
    /// </summary>
    public void debug()
    {
        Debug.Log(
            $"StatusData:\n" +
            $"知的探求心: {intellectualCuriosity}\n" +
            $"想像力: {imagination}\n" +
            $"良心性: {conscientiousness}\n" +
            $"責任感: {responsibility}\n" +
            $"社交性: {sociability}\n" +
            $"積極性: {proactiveness}\n" +
            $"共感性: {empathy}\n" +
            $"思いやり: {compassion}\n" +
            $"ストレス耐性: {stressTolerance}\n" +
            $"不安: {anxiety}\n" +
            $"知性: {intelligence}\n" +
            $"人間らしさ: {humanLikeness}\n" +
            $"依存度: {dependency}\n" +
            $"好感度: {affection}"
        );
    }
    public int IntellectualCuriosity => intellectualCuriosity;
    public int Imagination => imagination;
    public int Conscientiousness => conscientiousness;
    public int Responsibility => responsibility;
    public int Sociability => sociability;
    public int Proactiveness => proactiveness;
    public int Empathy => empathy;
    public int Compassion => compassion;
    public int StressTolerance => stressTolerance;
    public int Anxiety => anxiety;
    public int Intelligence => intelligence;
    public int HumanLikeness => humanLikeness;
    public int Dependency => dependency;
    public int Affection => affection;
}
