using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClownMeter : MonoBehaviour
{
    public float Value;
    private Clown[] _clownList;
    public Text text;
    public static ClownMeter Instance;
    public GameObject RunCanvas;
    public bool IsAlreadyChasing;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Value = 50; // start at neutral meter
        _clownList = GameObject.FindObjectsByType<Clown>(FindObjectsSortMode.None);
        RunCanvas = GameObject.Find("RunCanvas");
        RunCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if()
        text.text = $"Clown meter score: {Value} / 100";
        Value -= Time.deltaTime * 0.3f;
    }

    public void ChangeMeter(float a)
    {
        Value += a;
        ChangeClownBehaviour();
    }
    private void ChangeClownBehaviour()
    {
        if (Value < 20 && !IsAlreadyChasing)
        {
            RunCanvas.SetActive(true);
            var curtain = GameObject.Find("Curtains");
            curtain.SetActive(false);
            IsAlreadyChasing = true;

            foreach (Clown clown in _clownList)
            {
                // change head mesh
                clown.Chase();
            }
        }
        if (Value < 40)
        {
            foreach (Clown clown in _clownList)
            {

            }
        }
        if (Value < 60)
        {
            foreach (Clown clown in _clownList)
            {

            }
        }
        if (Value < 80)
        {
            foreach (Clown clown in _clownList)
            {

            }
        }
        if (Value < 100)
        {
            foreach (Clown clown in _clownList)
            {

            }
        }
    }
}
