using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleBonus : MonoBehaviour
{
    private Image timerBar;
    public float time;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timerBar = GameObject.FindGameObjectWithTag("TimeLine").GetComponent<Image>();
        timeLeft = time;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.5f, 0);

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / time;
        }
        else
        {
            Destroy(this.gameObject);
            timerBar.gameObject.SetActive(false);
        }

    }

}
