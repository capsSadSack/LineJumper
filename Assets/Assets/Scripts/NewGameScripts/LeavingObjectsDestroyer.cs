using UnityEngine;
using UnityEngine.Events;

public class LeavingObjectsDestroyer : MonoBehaviour
{
    public UnityEvent OnPlayerLeaveField;
    public AchievementUnlockedEvent OnRunawayAchievementUnlocked;


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            UnlockRunawayAchievement();

            OnPlayerLeaveField.Invoke();
        }

        if (collider.CompareTag("Enemy"))
        {
            GameObject.Destroy(collider.gameObject);
        }
    }

    private void UnlockRunawayAchievement()
    {
        AchievementUnlockedArgs args = new AchievementUnlockedArgs()
        {
            Achievement = Achievement.Runaway
        };

        OnRunawayAchievementUnlocked.Invoke(args);
    }
}
