using UnityEngine;
using VContainer;

public class TransitionBehaviour : MonoBehaviour
{
    [Inject] readonly ITransitionService m_transitionService; //--> from ProjectLifetimeScope
    //[Inject] readonly IAnchorService m_anchorService; --> from ProjectLifetimeScope

    // Todo: How to make sure, that these dependencies come from mvvm lts?
    // Maybe we need to put the script execution order of this script to after the Mvvm LTS
    // Or we combine the transition bevahiour with the lts script, so we can trigger the Awake on container build callback
    //[Inject] readonly OffsetToSpawnPose m_offsetToSpawnPose; --> from MvvmLifetimeScope
    //[Inject] readonly UIDocument m_UiDocument; --> from MvvmLifetimeScope

    public Awaitable TransitionHandle { get; private set; }

    private void Awake()
    {
        //m_anchorService.AnchorChangedEvent += HandleAnchorChangedEvent;
        //m_UiDocument.Interactable = false;
        TransitionHandle = m_transitionService.TransitionIn(/*m_transitionService.AnchorPose, offsetToSpawnPose*/);
        //await TransitionHandle;
        //m_UiDocument.Interactable = true;
    }

    private void HandleAnchorChangedEvent()
    {
        //m_UiDocument.Interactable = false;
        TransitionHandle = m_transitionService.Transition();
        //await TransitionHandle;
        //m_UiDocument.Interactable = true;
    }

    private void OnDestroy()
    {
        //m_UiDocument.Interactable = false;
        TransitionHandle = m_transitionService.TransitionOut();
    }
}
