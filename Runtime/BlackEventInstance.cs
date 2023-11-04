using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.A9.UI
{
    public class BlackEventInstance : MonoBehaviour
    {
        public UnityEvent OnBlack;
        public UnityEvent OnEnd;
        public Color col = Color.black;
        public float entry_time = 0.8f;
        public float time = 0.8f;
        public float exit_time = 1.5f;

        public void Go()
        {
            UITransistion.instance.BlackEvent(col, () => { OnBlack?.Invoke(); }, entry_time, time, exit_time, () => { OnEnd?.Invoke(); });
        }

        public void FirstHalf()
        {
            UITransistion.instance.BlackEventFirstHalf(col, () => { OnBlack?.Invoke(); }, entry_time);
        }

        public void SecondHalf()
        {
            UITransistion.instance.BlackEventSecondHalf(exit_time);
        }

    }
}

