using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableBehaviour : MonoBehaviour
{
}

public class PauseManager : MonoBehaviour
{
        private PausableBehaviour[] pausableBehaviours;
 
        private void Awake()
        {
            pausableBehaviours = FindObjectsOfType<PausableBehaviour>();
            Debug.Log("Found " + pausableBehaviours.Length + "pauseable behaviours");
        }
 
        public void PauseGame()
        {
            foreach (PausableBehaviour behaviour in pausableBehaviours)
            {
                behaviour.StopAllCoroutines();
                behaviour.enabled = false;
            }
        }
 
        public void PlayGame()
        {
            foreach (PausableBehaviour behaviour in pausableBehaviours)
            {
                behaviour.enabled = true;
            }
        }
}
