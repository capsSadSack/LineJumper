using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy
{
    public class EnemyAuraController : MonoBehaviour
    {
        public GameObject objectToFollow;

        private void Update()
        {
            if (WasDestroyed(objectToFollow))
            {
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                this.gameObject.transform.position = objectToFollow.transform.position;
            }
        }

        private bool WasDestroyed(GameObject objectToFollow)
        {
            return objectToFollow == null;
        }
    }
}
