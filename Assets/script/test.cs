using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class test : MonoBehaviour {
    private CancellationTokenSource cts;

    List<int> lis = new List<int>() { 3000, 3000, 4000, 0 };

    void Start() {
        cts = new();
        testproc();
    }

    public async void testproc() {
        Debug.Log("start1");
        if (lis.Count > 1) {
            Debug.Log("start2");
            await UniTask.Delay(lis[0], cancellationToken: cts.Token).SuppressCancellationThrow();
            Debug.Log("finish");
            lis.RemoveAt(0);
            testproc();
            Debug.Log("finish testproc");
        }
    }
}
