using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamers : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private int delayTime = 1;
    private float time;
    private GameObject player;
    private bool zoomStatus;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ScriptedCamera());
    }

    void Update()
    {
        CameraZoomStatus(zoomStatus);
        Debug.Log(zoomStatus);
    }

    private void CameraZoomStatus(bool status)
    {
        if (!status) return;
        time += 0.001f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, time);

        if (transform.position.y == 1.90999997 && transform.position.z == -2.38000011)
        {
            zoomStatus = false;
        }

    }


    private IEnumerator ScriptedCamera()
    {
        yield return new WaitForSeconds(delayTime);

        zoomStatus = true;

    }
}
