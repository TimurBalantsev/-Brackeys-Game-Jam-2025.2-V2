using UnityEngine;

public class AnimationHitBoxDelegator : MonoBehaviour
{
    [SerializeField] private AttackHitBox attackHitBox;

    public void DelegateResetHitsEvent(AnimationEvent animationEvent) // why the fuck do you NEED an argument for unity animation events to work.
    {
        attackHitBox.ResetHitsEvent();
    }
}