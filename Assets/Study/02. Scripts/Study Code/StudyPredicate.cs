using System;
using UnityEngine;

public class StudyPredicate : MonoBehaviour
{
    public Predicate<float> flip;

    private void Start()
    {
        flip += (h) => h > 0;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");

        int dir = h > 0 ? 1 : -1;
        transform.localScale = new Vector3(dir, 1, 1);
    }
}
