using UnityEngine;

namespace Assets.Scripts.PickUps
{
    public class NukeExplosionBehaviour : MonoBehaviour
    {
        public AudioSource hugeBombBoomSound;
        public Animator anim;


        public void ShowExplosion()
        {
            hugeBombBoomSound.Play();
            anim.SetBool("isExploding", true);
        }
    }
}
