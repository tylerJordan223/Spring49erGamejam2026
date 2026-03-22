using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField] GameObject heart;
    private List<GameObject> hearts = new List<GameObject>();

    private void Update()
    {
        Debug.Log(hearts.Count);

        if(hearts.Count > MagicianController.instance.health && hearts.Count > 0)
        {
            GameObject g = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(g);
        }else if(hearts.Count < MagicianController.instance.health)
        {
            GameObject g = Instantiate(heart);
            g.transform.SetParent(transform, false);
            hearts.Add(g);
        }
    }
}
