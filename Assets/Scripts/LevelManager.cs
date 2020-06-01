using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public int gameScore;
    public int collectedCoin;
    public Text scoreText;
    public Text coinText;

    public int jumpPowerUpTime;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(JumpPowerUpTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpPowerUpTime == 15)
        {
            TileScript.instance.isJumpMore = true;
            jumpPowerUpTime = 0;
        }
        scoreText.text = gameScore.ToString();
        coinText.text = collectedCoin.ToString();
    }


    internal IEnumerator JumpPowerUpTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            jumpPowerUpTime = jumpPowerUpTime + 1;
        }

    }
}
