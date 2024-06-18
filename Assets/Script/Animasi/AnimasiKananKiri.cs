using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimasiKananKiri : MonoBehaviour
{
    public float kecepatan;
    public float batasKanan;
    public float batasKiri;

    private bool kanan;
    // Start is called before the first frame update
    void Start()
    {
        kanan = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (kanan)
        {
            Vector3 posisiAkhir = new Vector3();
            posisiAkhir.x = transform.position.x + kecepatan;
            posisiAkhir.y = transform.position.y;
            posisiAkhir.z = transform.position.z;
            transform.position = posisiAkhir;

            if (posisiAkhir.x > batasKanan){
                kanan = false;
            }
        }
        else
        {
            Vector3 posisiAkhir = new Vector3();
            posisiAkhir.x = transform.position.x - kecepatan;
            posisiAkhir.y = transform.position.y;
            posisiAkhir.z = transform.position.z;
            transform.position = posisiAkhir;
            
            if (posisiAkhir.x < batasKiri){
                kanan = true;
            }
        }
    }
}
