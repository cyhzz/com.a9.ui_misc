using System;
using System.Collections;
using System.Collections.Generic;
using Com.A9.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace Com.A9.UI
{
    public class UITransistion : StaticInstance<UITransistion>
    {
        public CanvasGroup backdrop;
        public List<IEnumerator> entries = new List<IEnumerator>();
        Coroutine C;

        IEnumerator Go_(float time = 0.3f)
        {
            backdrop.blocksRaycasts = true;
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                backdrop.alpha = i / time;
                yield return null;
            }
            backdrop.alpha = 1.0f;
            yield return null;
        }

        IEnumerator Reverse_(float time = 0.3f)
        {
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                backdrop.alpha = 1 - i / time;
                yield return null;
            }
            backdrop.alpha = 0;
            backdrop.blocksRaycasts = false;
        }

        IEnumerator BlackEvent_(Color col, Action act = null, float entry_time = 0.35f, float time = 1f, float exit_time = 0.3f, Action end = null)
        {
            backdrop.GetComponent<Image>().color = col;
            yield return StartCoroutine(Go_(entry_time));
            act?.Invoke();
            yield return new WaitForSeconds(time);
            yield return StartCoroutine(Reverse_(exit_time));
            entries.RemoveAt(0);
            end?.Invoke();
            C = null;
        }

        IEnumerator BlackIn_(Color col, Action act = null, float entry_time = 0.35f)
        {
            backdrop.GetComponent<Image>().color = col;
            yield return StartCoroutine(Go_(entry_time));
            act?.Invoke();
            entries.RemoveAt(0);
            C = null;
        }


        IEnumerator BlackOut_(float exit_time = 0.3f)
        {
            yield return StartCoroutine(Reverse_(exit_time));
            entries.RemoveAt(0);
            C = null;
        }

        void Update()
        {
            if (C == null && entries.Count > 0)
            {
                C = StartCoroutine(entries[0]);
            }
        }

        public void BlackEvent(Color col, Action act = null, float entry_time = 0.35f, float time = 1f, float exit_time = 0.3f, Action end = null)
        {
            entries.Add(BlackEvent_(col, act, entry_time, time, exit_time, end));
        }

        public void BlackEventFirstHalf(Color col, Action act = null, float entry_time = 0.35f)
        {
            entries.Add(BlackIn_(col, act, entry_time));
        }

        public void BlackEventSecondHalf(float exit_time = 0.3f)
        {
            entries.Add(BlackOut_(exit_time));
        }
    }
}

