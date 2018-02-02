using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private GameObject gameOverUI;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text finalScoreText;

	private bool playerActive = false;
	private bool gameOver = false;
	private bool gameStarted = false;
	private int points = 0;

	public bool PlayerActive {
		get { return playerActive; }
	}

	public bool GameOver {
		get { return gameOver; }
	}

	public bool GameStarted {
		get { return gameStarted; }
	}

	private void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	void Start () {
		playerActive = false;
		gameOver = false;
		if (SceneManager.GetActiveScene().name == "Game") {
			gameStarted = true;
			scoreText.text = points.ToString();
		} else
			gameStarted = false;
	}

	private void Update() {
		
	}

	public void StartGame() {
		playerActive = true;
	}

	public void HitObstacle() {
		scoreText.gameObject.SetActive(false);
		Destroy(player, 3);
		gameOver = true;
		gameOverUI.SetActive(true);
		finalScoreText.text = points.ToString();
	}

	public void PlayGame() {
		SceneManager.LoadScene("Game");
	}

	public void MainMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void Replay() {
		SceneManager.LoadScene("Game");
	}

	public void AddPoint() {
		points++;
		scoreText.text = points.ToString();
	}
}
