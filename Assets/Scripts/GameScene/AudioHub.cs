using UnityEngine;

public class AudioHub : MonoBehaviour
{
    [SerializeField] private AudioSource winSound, gameLost;

    public void PlayWin() => winSound.Play();
    public void PlayGameOver() => gameLost.Play();
}