using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    public GameObject litter;

    [SerializeField]
    public float timerMax;
    [SerializeField]
    public float timerMin;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(timerMin, timerMax);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            DropLitter();
        }
    }

    void DropLitter()
    {
        GameObject instance = Instantiate(litter);
        instance.transform.position = transform.position;
        timer = Random.Range(timerMin, timerMax);
    }
}
