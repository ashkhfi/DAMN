using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapManager : MonoBehaviour
{

    public Image starLvl1;
    public Image starLvl2;
    public Image starLvl3;
    public Image starLvl4;
    public Image starLvl5;

    public Sprite Rating0;
    public Sprite Rating1;
    public Sprite Rating2;
    public Sprite Rating3;

    public Text XP;
    // Start is called before the first frame update
    void Start()
    {
        int t1 = PlayerPrefs.GetInt("StarLevel1", 0);
        int w1 = PlayerPrefs.GetInt("WinStatusLevel1", 0);

        int s1 = PlayerPrefs.GetInt("ScoreLevel1", 0);
        int s2 = PlayerPrefs.GetInt("ScoreLevel2", 0);
        int s3 = PlayerPrefs.GetInt("ScoreLevel3", 0);
        int s4 = PlayerPrefs.GetInt("ScoreLevel4", 0);
        int s5 = PlayerPrefs.GetInt("ScoreLevel5", 0);

        int total = s1 + s2 + s3 + s4 + s5;

        if (w1 == 1){
            if(t1 == 1){
                starLvl1.sprite = Rating1;
            }else if(t1 == 2){
                starLvl1.sprite = Rating2;
            }else if(t1 == 3){
                starLvl1.sprite = Rating3;
            }else{
                starLvl1.sprite = Rating0;
            }
        }

        XP.text = $"{total}";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
