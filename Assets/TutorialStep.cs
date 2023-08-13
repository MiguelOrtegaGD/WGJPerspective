using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class TutorialStep : MonoBehaviour
{
    [SerializeField] TutorialInputEnum inputType;
    [SerializeField] string targetTag;
    [SerializeField] KeyCode key;
    [SerializeField] string axisName;
    [SerializeField] PlayableDirector director;
    [SerializeField] float timer;

    bool activeTimer = false;

    ParticleSystem particle;

    [SerializeField] UnityEvent initialEvents;
    [SerializeField] UnityEvent finishedEvents;

    private void Start()
    {
        initialEvents?.Invoke();

        if (gameObject.GetComponent<ParticleSystem>() && inputType == TutorialInputEnum.Collision)
            particle = gameObject.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (inputType == TutorialInputEnum.Input)
        {
            if (!string.IsNullOrEmpty(axisName))
            {
                if (Input.GetAxis(axisName) != 0)
                    NextStep();
            }
            else if (Input.GetKeyDown(key))
                NextStep();
        }

        if (inputType == TutorialInputEnum.Time || activeTimer == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
                NextStep();
        }
    }

    public void NextStep()
    {
        finishedEvents?.Invoke();
        GameDelegateHelper.nextTutorialStep?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inputType == TutorialInputEnum.Collision)
            if (other.CompareTag(targetTag))
                if (!particle)
                    NextStep();
                else
                {
                    particle.Stop();
                    activeTimer = true;
                }
    }

    public void CinematicFinished(PlayableDirector dir)
    {
        NextStep();
    }

    private void OnEnable()
    {
        if (inputType == TutorialInputEnum.Cinematic)
            director.stopped += CinematicFinished;
    }

    private void OnDisable()
    {
        if (inputType == TutorialInputEnum.Cinematic)
            director.stopped -= CinematicFinished;
    }
}
