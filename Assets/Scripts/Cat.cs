using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cat : MonoBehaviour
{
    public GameObject _pigeonGroupPrefab;
    public List<GameObject> allPigeons;

    //constants
    private float _minRadius = 28;
    private float _maxRadius = 123;
    private float _minPigeonDistance = 25;

    //vars
    bool move = true;
    Vector3 direction;

    void Start()
    {
        allPigeons = new List<GameObject>();
        ChangeDirection();
        StartCoroutine(StopAndGo());
        StartCoroutine(Spawn());
    }

    void Update()
    {

        transform.position += direction * 10f * (move? 3f : 1) * Time.deltaTime;

        //bounds
        if (transform.position.magnitude < _minRadius)
        {
            transform.position = transform.position.normalized * _minRadius;
            ChangeDirection();
        }

        if (transform.position.magnitude > _maxRadius)
        {
            transform.position = transform.position.normalized * _maxRadius;
            ChangeDirection();
        }

        allPigeons = allPigeons.Where(x => x != null).ToList();
        foreach(GameObject pigeonGroup in allPigeons)
        {
            if(Vector3.Distance(pigeonGroup.transform.position, transform.position) < _minPigeonDistance)
            {
                ChangeDirection();
                transform.position = pigeonGroup.transform.position + ((transform.position - pigeonGroup.transform.position).normalized * _minPigeonDistance);
                
                break;
            }
        }
    }

    IEnumerator StopAndGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            move = !move;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (move)
            {
                int rand = Random.Range(0, 5);
                if (rand == 0 && Camera.main.depth > 0)
                {
                    allPigeons.Add(Instantiate(_pigeonGroupPrefab, transform.position, Quaternion.identity).gameObject);
                }
            }
        }
    }

    void ChangeDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        direction = direction.normalized;
    }
}
