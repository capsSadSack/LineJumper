namespace Assets.Scripts.NewGameScripts
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.Events;

    public class AudioFinish : MonoBehaviour
    {
        public UnityEvent OnFinishSound;
        public AudioSource audiosource;

        private float duration;


        private void Awake()
        {
            Initialize();
            StartCoroutine(WaitForSound());
        }

        public void Initialize()
        {
            audiosource.Play();
            duration = audiosource.clip.length;
        }

        IEnumerator WaitForSound()
        {
            yield return new WaitForSeconds(duration);
            OnFinishSound.Invoke();
        }
    }
}
