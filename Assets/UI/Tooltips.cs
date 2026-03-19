using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltips : MonoBehaviour
{
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }
    // Using only if want to view size in the editor
    private void Update()
    {
        if (Application.isEditor)
        {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        pivotX = 0f;
        pivotY = 1f;
        if (position.x > Screen.width - rectTransform.rect.width)
        {
            pivotX = 1f;
        }

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

    }




}
