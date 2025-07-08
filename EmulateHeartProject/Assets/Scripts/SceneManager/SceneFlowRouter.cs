using System.Collections.Generic;
using UnityEngine;

public class SceneFlowRouter : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> flowControllers; // ‚±‚±‚É ISceneFlowController ŽÀ‘•‚ð“o˜^

    private Dictionary<SceneType, ISceneFlowController> controllerMap;

    private void Awake()
    {
        controllerMap = new Dictionary<SceneType, ISceneFlowController>();

        foreach (var mono in flowControllers)
        {
            if (mono is ISceneFlowController controller)
            {
                var sceneType = controller.GetSceneType();
                controllerMap[sceneType] = controller;
            }
            else
            {
                Debug.LogWarning($"{mono.name} does not implement ISceneFlowController.");
            }
        }
    }

    public ISceneFlowController Get(SceneType type)
    {
        return controllerMap.TryGetValue(type, out var controller) ? controller : null;
    }
}