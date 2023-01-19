using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWorldText : MonoBehaviour
{
    private static Color defaultColor = Color.white;
    [SerializeField] private GameObject defaultText;
    private static DisplayWorldText instance;
    public static void DisplayText(Transform _pos, string _text)
    {
        DisplayText(_pos, _text, defaultColor);
    }
    public static void DisplayText(Transform _pos, string _text, int _size = 42)
    {
        DisplayText(_pos, _text, defaultColor, _size);
    }
    public static void DisplayText(Transform _pos, string _text, Color _color, int _size = 42)
    {
        GameObject createdText = Instantiate(instance.defaultText, instance.transform);
        createdText.transform.position = _pos.position;

        TextMeshProUGUI textComponent = createdText.GetComponent<TextMeshProUGUI>();
        textComponent.text = _text;
        textComponent.color = _color;
        textComponent.fontSize = Mathf.Clamp(_size, 30, 72);
        // TODO: add opacity animation fade in out
        Destroy(createdText, 1);
    }
    private void Start()
    {
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("MULTIPLE TEXT DISPLAYERS ACTIVE");
            Destroy(this);
        }
    }
}