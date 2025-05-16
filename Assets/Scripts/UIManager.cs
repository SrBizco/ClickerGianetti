using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelCreditos;
    public Button botonCreditos;
    public Button botonVolver;

    [Header("Lógica del juego")]
    public GameManager gameManager;

    void Start()
    {
        panelCreditos.SetActive(false);

        botonCreditos.onClick.AddListener(AbrirCreditos);
        botonVolver.onClick.AddListener(CerrarCreditos);
    }

    void AbrirCreditos()
    {
        panelCreditos.SetActive(true);
        gameManager.PausarJuego(true);
    }

    void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
        gameManager.PausarJuego(false);
    }
}
