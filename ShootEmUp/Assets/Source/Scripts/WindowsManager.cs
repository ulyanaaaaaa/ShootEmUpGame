using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    [SerializeField] private GameObject _failWindow;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager.OnWin += OpenWinWindow;
        _player.OnDie += OpenFailWindow;
    }
    
    private void OnDisable()
    {
        _gameManager.OnWin -= OpenWinWindow;
        _player.OnDie -= OpenFailWindow;
    }

    private void OpenFailWindow()
    {
        _failWindow.gameObject.SetActive(true);
    }

    private void OpenWinWindow()
    {
        _winWindow.gameObject.SetActive(true);
    }

}
