using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : SingletonCore<QuestManager>, ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    public event Action<string> QuestClear; // Quest Clear시 작동 될 함수를 갖는 액션
        
    [SerializeField] private Button[] questButtons;
    [SerializeField] private QuestData[] datas;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < questButtons.Length; i++)
        {
            int j = i;
            questButtons[i].onClick.AddListener(() => SetButton(j));
        }
    }

    private void SetButton(int index)
    {
        Quest quest = new Quest(datas[index]);
        
        // 수락한 퀘스트 버튼 반투명화 및 상호작용 끄기
        CanvasGroup canvasGroup = questButtons[index].GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.5f;
        canvasGroup.interactable = false;
    }
    
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
        Debug.Log($"퀘스트 {observer.QuestName}을 등록하였습니다.");
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
        QuestClear.Invoke(observer.QuestName);
        Debug.Log($"퀘스트 {observer.QuestName}을 삭제하였습니다.");
    }

    public void NotifyListener(string questName)
    {
        for (int i = observers.Count - 1; i >= 0; i--)
        {
            if (observers[i] != null)
                observers[i].Notify(questName);
        }
    }
}
