using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PigeonGroup : MonoBehaviour
{

    List<Transform> pigeons;
    void Start()
    {
        pigeons = new List<Transform>();
        foreach(Transform pigeon in transform)
        {
            pigeons.Add(pigeon);
            pigeon.gameObject.SetActive(false);
        }
        StartCoroutine(PigeonsFlyCoroutine());
    }

    IEnumerator PigeonsFlyCoroutine()
    {
        List<Vector3> positionsEnd = pigeons.Select(x => x.position).ToList();
        List<Vector3> positionsStart = pigeons.Select(x => transform.position + (Vector3.up * 50f) + (80f*new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)))).ToList();
        List<Quaternion> rotationsEnd = pigeons.Select(x => x.rotation).ToList();
        for (int i = 0; i < pigeons.Count; i++)
        {
            pigeons[i].gameObject.SetActive(true);
            pigeons[i].LookAt(positionsStart[1]);//rotate them properly
            pigeons[i].transform.position = positionsStart[i];//position them properly
        }
        List<Quaternion> rotationsStart = pigeons.Select(x => x.rotation).ToList();

        float t = 0;
        bool barked = false;
        while(t < 1f) //fly backwards
        {
            for(int i = 0; i < pigeons.Count; i++)
            {
                //pigeons[i].rotation = Quaternion.Slerp(rotationsStart[i], rotationsEnd[i], t);
                pigeons[i].position = Vector3.Lerp(positionsStart[i], positionsEnd[i], t);
            }
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime * 1.8f;
            
            if (!barked && t > 0.5f)
            {
                barked = true;
                AudioManager.ShootBark();
            }

        }
        


        for (int i = 0; i < pigeons.Count; i++) //position them properly
        {
            pigeons[i].rotation = rotationsEnd[i]; 
        }

        int j = 0, k = 0;
        while(pigeons.Any())
        {
            j++;
            j = j % pigeons.Count;
            k = j + 4;
            k = k % pigeons.Count;
            yield return new WaitForSeconds(0.5f);
            pigeons[j].eulerAngles = new Vector3(pigeons[j].eulerAngles.x, Random.Range(0,360f),pigeons[j].eulerAngles.z);
            pigeons[j].position += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            pigeons[k].eulerAngles = new Vector3(pigeons[k].eulerAngles.x, Random.Range(0, 360f), pigeons[k].eulerAngles.z);
            pigeons[k].position += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            int rand = Random.Range(0, 5);
            if (rand == 0)
            {
                pigeons[j].gameObject.SetActive(false);
                pigeons.Remove(pigeons[j]);
            }
        }

        Destroy(this.gameObject);
    }
}
