using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCounter : MonoBehaviour
{
    public Text HasilKilled; // Referensi ke UI Text untuk menampilkan jumlah kill
    public Text Status;
    public Text SisaWaktu;
    public Text BonusPoin;
    public Text MaxHantu;

    public GameObject star0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    // Start is called before the first frame update
    void Start()
    {
        int kills = PlayerPrefs.GetInt("TotalKills", 0);
        int levelNow = PlayerPrefs.GetInt("LevelNow", 0);
        float timeremaining = PlayerPrefs.GetFloat("TimeRemaining", 0);
        int timeint = System.Convert.ToInt32(timeremaining); 
        int totalenemy = PlayerPrefs.GetInt("TotalEnemy", 0);
        int star1total = totalenemy / 3;
        int star2total = totalenemy / 2;
        int winstatus = PlayerPrefs.GetInt("WinStatus", 0);
        int bonus_poin = timeint * 10;

        if (winstatus == 0)
        {
            Status.text = "Kamu Kalah";

            PlayerPrefs.SetInt("StarLevel"+levelNow, 0);
            PlayerPrefs.SetInt("ScoreLevel"+levelNow, 0);
            PlayerPrefs.SetInt("WinStatusLevel"+levelNow, 0);

        }else {
            Status.text = "Kamu Menang";

            if (bonus_poin >= 300){

                star0.gameObject.SetActive(false);
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(true);

                PlayerPrefs.SetInt("StarLevel"+levelNow, 3);

            }
            else if (bonus_poin >= 200){

                star0.gameObject.SetActive(false);
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(false);

                PlayerPrefs.SetInt("StarLevel"+levelNow, 2);

            }else if (bonus_poin >= 100){

                star0.gameObject.SetActive(false);
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);

                PlayerPrefs.SetInt("StarLevel"+levelNow, 1);

            }else{

                star0.gameObject.SetActive(true);
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);

                PlayerPrefs.SetInt("StarLevel"+levelNow, 0);

            }

                PlayerPrefs.SetInt("ScoreLevel"+levelNow, bonus_poin);
                PlayerPrefs.SetInt("WinStatusLevel"+levelNow, 1);
        }

        HasilKilled.text = kills.ToString();
        SisaWaktu.text = timeint.ToString();
        MaxHantu.text = totalenemy.ToString();
        BonusPoin.text =  bonus_poin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
