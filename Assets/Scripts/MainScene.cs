using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScene : MonoBehaviour {

    private float totalHP;

    public float player1HP;
    private float player2HP;

    public Slider player1HP1;
    public Slider player1HP2;
    public Slider player2HP1;
    public Slider player2HP2;
    
    void Awake()
    {
        totalHP = 2048;
    }

    void Start()
    {
        player1HP = totalHP;
        player2HP = totalHP;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            HPReducce(1, 128);
        if (Input.GetMouseButtonDown(1))
            HPReducce(2, 512);
    }

    public void HPReducce(int id, int num)
    {
        if(id == 1) { 
            player1HP -= num;
            player1HP2.value = player1HP;
            StartCoroutine(HPReduceAnimation(player1HP, player1HP1));
        }
        else {
            player2HP -= num;
            player2HP2.value = player2HP;
            StartCoroutine(HPReduceAnimation(player2HP, player2HP1));
        }
    }

    IEnumerator HPReduceAnimation(float targetHP, Slider HPSlider)
    {
        float fromHP = HPSlider.value;
        for (int i = 0; i < 8; i++)
        {
            yield return 1;
            HPSlider.value = HPSlider.value - (fromHP - targetHP) / 8;
            yield return 1;
        }
        if(HPSlider.value > targetHP)
            HPSlider.value = targetHP;
    }
}
