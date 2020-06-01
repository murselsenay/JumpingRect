using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float boundY = 6f;
    public bool movingTileLeft, movingTileRight, isSpike, isTile;
    Player player = new Player();
    public int randomCoinCount;
    public bool isJumpMore;

    public GameObject coinPrefab;
    public GameObject jumpMorePrefab;
    GameObject newJumpPowerUp;
    public List<GameObject> newCoin = new List<GameObject>();

    public static TileScript instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    void Awake()
    {



        randomCoinCount = Random.Range(1, 20);
        Vector3 temp = transform.position;


        if (isSpike == false)
        {



            if (isJumpMore == true)
            {
                newJumpPowerUp = Instantiate(jumpMorePrefab, temp, Quaternion.identity);
                isJumpMore = false;
            }
            if (randomCoinCount % 2 == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    newCoin.Add(Instantiate(coinPrefab, temp, Quaternion.identity));
                }
            }
            else
            {
                newCoin.Add(Instantiate(coinPrefab, temp, Quaternion.identity));
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Vector3 temp = transform.position;

        if (isJumpMore == true)
        {
            for (int i = 0; i < newCoin.Count; i++)
            {
                Destroy(newCoin[i].gameObject);
            }
            newJumpPowerUp = Instantiate(jumpMorePrefab, temp, Quaternion.identity);
            isJumpMore = false;
        }

       
        if (isSpike == false)
        {


            float coinPos = transform.position.x;

            if (newJumpPowerUp != null)
                newJumpPowerUp.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f);

            if (randomCoinCount % 2 == 0)
            {
                for (int i = 0; i < newCoin.Count; i++)
                {
                    if (newCoin[i] != null)
                    {
                        newCoin[i].transform.position = new Vector3(coinPos - 0.3f, transform.position.y + 0.4f);
                        coinPos = coinPos + 0.3f;
                    }
                }
            }
            else
            {
                if (newCoin[0] != null)
                    newCoin[0].transform.position = new Vector3(coinPos, transform.position.y + 0.4f);
            }

        }
    }
    void Move()
    {
        Vector2 temp = transform.position;
        temp.y += moveSpeed * Time.deltaTime;
        transform.position = temp;

        if (temp.y >= boundY)
        {
            gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (isTile)
            {

                //SoundManager.instance.LandSound();
            }
            if (target.contacts[0].normal.y < 1f)
            {
                LevelManager.instance.gameScore += 1;


            }
        }
    }
    private void OnCollisionStay2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (movingTileLeft)
            {
                target.gameObject.GetComponent<Player>().PlatformMove(-1f);
            }
            if (movingTileRight)
            {
                target.gameObject.GetComponent<Player>().PlatformMove(1f);
            }
        }
    }

}
