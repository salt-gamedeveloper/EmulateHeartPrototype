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

        // �e�{�^���ɁA���̃{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo�������A�N�V������o�^
        if (tabButtons != null)
        {
            for (int i = 0; i < tabButtons.Count; i++)
            {
                if (tabButtons[i] != null)
                {
                    // �{�^�����N���b�N���ꂽ��A�Ή�����C���f�b�N�X�� Action ���Ăяo���w���p�[���\�b�h��o�^
                    int index = i; // �N���[�W���̂��߂Ƀ��[�J���ϐ��ɃR�s�[
                    tabButtons[index].SubscribeToButtonClick(() => OnTabButtonClickedInternal(index));
                }
            }
        }
    }

    // �{�^�����N���b�N���ꂽ�Ƃ��Ɏ��ۂɌĂ΂��������\�b�h
    private void OnTabButtonClickedInternal(int buttonIndex)
    {
        // Debug.Log($"�{�^�� {buttonIndex} ���N���b�N����܂����B");
        // tabClickActions ���X�g�ɓK�؂� Action �����݂��Anull �łȂ����Ƃ��m�F���ČĂяo��
        if (buttonIndex >= 0 && buttonIndex < tabClickActions.Count && tabClickActions[buttonIndex] != null)
        {
            tabClickActions[buttonIndex]?.Invoke();
        }
        else
        {
            Debug.LogWarning($"OnTabButtonClickedInternal: buttonIndex {buttonIndex} �ɑΉ�����A�N�V������������Ȃ���null�ł��B");
        }
    }

    public void SetImage(string fileId)
    {
        changer.ChangeImageImmediate(fileId);
    }



    public void SubscribeToTabClick(List<System.Action> listeners)
    {
        // ���X�i�[�̐����{�^���̐��ƈ�v���Ȃ��ꍇ�͏����𒆒f
        if (tabButtons.Count != listeners.Count)
        {
            Debug.LogError("SubscribeToTabClick: ���X�i�[�̐��ƃ^�u�{�^���̐�����v���܂���B");
            return;
        }

        tabClickActions.Clear();
        tabClickActions.AddRange(listeners);
    }
}
