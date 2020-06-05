using Assets.Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Text messageText;

    [SerializeField]
    private Text scoresText;
    private int _score;
    private int score
    {
        get { return _score; }
        set 
        {
            scoresText.text = $" Score : { value }";
            _score = value; 
        }
    }

    [SerializeField]
    private GameObject playerGO;
    private PlayerItem playerItem;

    private int enemiesCount = 4;
    private List<EnemyItem> enemies = new List<EnemyItem>();
    private GameState gameState = GameState.BeforeStart;

    public void StartGame()
    {
        Awake();
    }

    private async Task Awake()
    {
        var p = restartButton.transform.position;
        restartButton.transform.position = new Vector3( -100000f, p.y, p.z);

        ArrangeGame();

        await MessageHelper.CountDown(messageText);
        gameState = GameState.Play;
    }

    private void ArrangeGame()
    {
        ClearGame();

        playerGO.transform.position = new Vector3(-Sizes.EDGE_X, 0, 0);
        playerItem = new PlayerItem(playerGO, -Sizes.EDGE_X, 0);

        GameObject enemyPrefab = Resources.Load<GameObject>("Item");
        System.Random random = new System.Random();
        for (int i = 0; i < enemiesCount; i++)
        {
            GameObject enemyGO = Instantiate(enemyPrefab);
            enemyGO.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            enemyGO.transform.position = new Vector3(Sizes.EDGE_X, i - 2, 0);
            EnemyItem enemy = new EnemyItem(enemyGO, Sizes.EDGE_X, i - 2);
            enemies.Add(enemy);
            enemy.OnEndJump += SetNextEnemyJump;
            SetNextEnemyJump(enemy, random);
        }
    }

    private void ClearGame()
    {
        enemies.ForEach(x => x.Destroy());
        enemies.Clear();
        score = 0;
    }

    private void Update()
    {
        if (gameState == GameState.Play)
        {
            HandleUserInput();

            UpdateItemsPosition();

            CheckNewScores();

            CreateNewEnemies();

            CheckGameOver();
        }
    }

    private void HandleUserInput()
    {
        if (Input.GetMouseButtonDown(0) && playerItem.State == ItemState.Run)
        {
            StartPlayerJump();
        }
    }

    private void StartPlayerJump()
    {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 direction = (mouseWorldPosition - playerItem.Position);
        direction.z = 0;
        direction.Normalize();

        playerItem.StartMoving(direction);
    }

    private void UpdateItemsPosition()
    {
        playerItem.Update(Time.deltaTime);
        enemies.ForEach(x => x.Update(Time.deltaTime));
    }

    private void CheckNewScores()
    {
        List<EnemyItem> enemiesCaught = enemies.Where(enemy => !enemy.IsAgressive)
            .Where(enemy => (enemy.Position - playerItem.Position).magnitude < Sizes.BALL_RADIUS)
            .ToList();

        score += enemiesCaught.Count();

        enemiesCaught.ForEach(x => x.Destroy());
        enemies.RemoveAll(x => enemiesCaught.Contains(x));
    }

    private void CreateNewEnemies()
    {
        if (enemies.Count < enemiesCount)
        {
            GameObject enemyPrefab = Resources.Load<GameObject>("Item");
            System.Random random = new System.Random();

            GameObject enemyGO = Instantiate(enemyPrefab);
            enemyGO.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            enemyGO.transform.position = new Vector3(Sizes.EDGE_X, -Sizes.EDGE_Y - 1, 0);

            EnemyItem enemy = new EnemyItem(enemyGO, Sizes.EDGE_X, -Sizes.EDGE_Y - 1);
            enemy.SetNextJump(new EnemyJump()
            {
                Destination = new Vector3(-Sizes.EDGE_X, -Sizes.EDGE_Y + 0.5f, 0),
                IsAgressive = true,
                JumpVelocity = 4.0f,
                TimeToJump_Sec = 0.5f
            });
            enemy.OnEndJump += SetNextEnemyJump;

            enemies.Add(enemy);
        }
    }

    private void CheckGameOver()
    {
        bool playerCaught = enemies.Where(enemy => enemy.IsAgressive)
            .Any(enemy => (enemy.Position - playerItem.Position).magnitude < Sizes.BALL_RADIUS);

        if (playerCaught)
        {
            GameOver();
            return;
        }

        if (playerItem.State == ItemState.Fall)
        {
            GameOver();
            return;
        }
    }

    private void GameOver()
    {
        gameState = GameState.Finish;
        MessageHelper.GameOver(messageText);

        var p = restartButton.transform.position;
        restartButton.transform.position = new Vector3(0, p.y, p.z);
    }

    private void SetNextEnemyJump(object sender, System.Random r)
    {
        EnemyItem enemy = (EnemyItem)sender;

        if (r == null)
        {
            r = new System.Random();
        }

        float timeToJump_Sec = r.Next(1, 8);
        float jumpVelcoity = r.Next(4, 8);

        float destinationY = r.Next((int)(-Sizes.EDGE_Y * 100), (int)(Sizes.EDGE_Y * 100)) / 100.0f;
        float destinationX = -Math.Sign(enemy.Position.x) * Sizes.EDGE_X;
        Vector3 endPosition = new Vector3(destinationX, destinationY, 0);

        EnemyJump nextJump = new EnemyJump()
        {
            Destination = endPosition,
            IsAgressive = r.Next(10) < 5,
            JumpVelocity = jumpVelcoity,
            TimeToJump_Sec = timeToJump_Sec
        };

        enemy.SetNextJump(nextJump);
    }
}
