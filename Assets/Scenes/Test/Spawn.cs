using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Spawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            var a = Instantiate(prefab, parent.transform.position + new Vector3(
                transform.position.x, Random.Range(-5f, 1f)), quaternion.identity);
            Game.sekor += 1;
            Destroy(a, 8);

            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
        }
    }
}
