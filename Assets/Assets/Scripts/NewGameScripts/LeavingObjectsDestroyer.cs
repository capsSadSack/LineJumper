using UnityEngine;
using UnityEngine.Events;

public class LeavingObjectsDestroyer : MonoBehaviour
{
    public UnityEvent OnPlayerLeaveField;

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            OnPlayerLeaveField.Invoke();
        }

        if (collider.CompareTag("Enemy"))
        {
            GameObject.Destroy(collider.gameObject);
        }
    }
}
