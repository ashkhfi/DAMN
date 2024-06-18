using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimasiNaikTurun : MonoBehaviour
{
    public float kecepatan;
    public float batasAtas;
    public float batasBawah;

    private bool naik;
    // Start is called before the first frame update
    void Start()
    {
        naik = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (naik)
        {
            Vector3 posisiAkhir = new Vector3();
            posisiAkhir.x = transform.position.x;
            posisiAkhir.y = transform.position.y + kecepatan;
            posisiAkhir.z = transform.position.z;
            transform.position = posisiAkhir;

            if (posisiAkhir.y > batasAtas){
                naik = false;
            }
        }
        else
        {
            Vector3 posisiAkhir = new Vector3();
            posisiAkhir.x = transform.position.x;
            posisiAkhir.y = transform.position.y - kecepatan;
            posisiAkhir.z = transform.position.z;
            transform.position = posisiAkhir;
            
            if (posisiAkhir.y < batasBawah){
                naik = true;
            }
        }
    }
}
