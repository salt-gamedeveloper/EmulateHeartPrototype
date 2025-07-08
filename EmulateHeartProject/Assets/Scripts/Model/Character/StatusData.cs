using System;
using UnityEngine;

[System.Serializable]
public class StatusData
{
    // �m�I�T���S: �Ȃ�(-100) �` ����(+100)
    public int intellectualCuriosity;

    // �z����: �Ȃ�(-100) �` �L��(+100)
    public int imagination;

    // �ǐS��: �Ȃ�(-100) �` ����(+100)
    public int conscientiousness;

    // �ӔC��: �Ȃ�(-100) �` ����(+100)
    public int responsibility;

    // �Ќ�: �����I(-100) �` �O���I(+100)
    public int sociability;

    // �ϋɐ�: ���ɓI(-100) �` �ϋɓI(+100)
    public int proactiveness;

    // ������: �Ȃ�(-100) �` ����(+100)
    public int empathy;

    // �v�����: �Ȃ�(-100) �` ����(+100)
    public int compassion;

    // �X�g���X�ϐ�: �Ȃ�(-100) �` ����(+100)
    public int stressTolerance;

    // �s��: �Ȃ�(-100) �` ����(+100)
    public int anxiety;

    // �m��: �Ȃ�(-100) �` ����(+100)
    public int intelligence;

    // �l�Ԃ炵���F�Ⴂ(-100) �` ����(+100)
    public int humanLikeness;

    // �ˑ��x�F�Ȃ�(-100) �` ����(+100)
    public int dependency;

    // �D���x�F����(-100) �` �D��(+100) 
    public int affection;

    public int normalProbability;
    public int joyProbability;
    public int angerProbability;
    public int sorrowProbability;
    public int funProbability;


    // �͈͒�`
    private static readonly int MinValue = -100;
    private static readonly int MaxValue = 100;

    /// <summary>
    /// �e�X�e�[�^�X�̒l��-100?100�͈̔͂ɐ���
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
    /// �X�e�[�^�X��\���i�f�o�b�O�p�j
    /// </summary>
    public void debug()
    {
        Debug.Log(
            $"StatusData:\n" +
            $"�m�I�T���S: {intellectualCuriosity}\n" +
            $"�z����: {imagination}\n" +
            $"�ǐS��: {conscientiousness}\n" +
            $"�ӔC��: {responsibility}\n" +
            $"�Ќ�: {sociability}\n" +
            $"�ϋɐ�: {proactiveness}\n" +
            $"������: {empathy}\n" +
            $"�v�����: {compassion}\n" +
            $"�X�g���X�ϐ�: {stressTolerance}\n" +
            $"�s��: {anxiety}\n" +
            $"�m��: {intelligence}\n" +
            $"�l�Ԃ炵��: {humanLikeness}\n" +
            $"�ˑ��x: {dependency}\n" +
            $"�D���x: {affection}"
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
