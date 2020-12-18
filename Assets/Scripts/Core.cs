using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] float dayLenghtMins = 1f;
    public float dayLenght;
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        dayLenght = dayLenghtMins * 60;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
