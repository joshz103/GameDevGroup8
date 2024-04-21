using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite normalSprite; // Assign "PlayButton" or "QuitButton" in the inspector
    public Sprite hoverSprite;  // Assign "Play2" or "Quit2" in the inspector
    public Sprite clickedSprite; // Assign "Play3" or "Quit3" in the inspector

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.sprite = clickedSprite;
        // Call the respective function after a short delay to allow the clicked sprite to display
        Invoke(nameof(HandleClickAction), 0.1f);
    }

    private void HandleClickAction()
    {
        if (gameObject.name == "PlayButton")
        {
            PlayGame();
        }
        else if (gameObject.name == "QuitButton")
        {
            QuitGame();
        }
    }

    private void PlayGame()
    {
        // Load your game scene here
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
        // Quit the game
        Application.Quit();
        Debug.Log("Quit game"); // Log message for the editor
    }
}
