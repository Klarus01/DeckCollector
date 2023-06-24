using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ShopPanelAnim()
    {
        animator.SetTrigger("ShowTrigger");
    }
}
