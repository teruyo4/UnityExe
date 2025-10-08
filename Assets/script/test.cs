using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private List<int> intlist = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        intlist.Add(20);
        intlist.Add(30);
        intlist.Add(540);
        intlist.Add(30);

        Debug.Log($"{intlist[0]}");
        Debug.Log($"{intlist[1]}");
        Debug.Log($"{intlist[2]}");
        Debug.Log($"{intlist[3]}");

        intlist.Remove(20);
        
        Debug.Log($"{intlist[0]}");
        Debug.Log($"{intlist[1]}");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
