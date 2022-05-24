using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private int width= 0;
    [SerializeField] private int height= 0;
    private List<GameObject>positions = new List<GameObject>();

    public List<GameObject> Positions { get => positions; set => positions = value; }

    void Start()
    {
        bool isPair = false;
        for(float i = 0; i < height; i+=0.75f)
        {
            for(float j= 0; j < width; j+=0.85f)
            {
                if (isPair)
                {
                    GameObject go = Instantiate(prefab, new Vector3(j, i), Quaternion.identity);
                    go.transform.parent = transform;
                    Positions.Add(go);
                }
                else
                {
                    GameObject go = Instantiate(prefab, new Vector3(j+0.425f, i), Quaternion.identity);
                    go.transform.parent = transform;
                    Positions.Add(go);
                }
            }
            isPair = !isPair;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
