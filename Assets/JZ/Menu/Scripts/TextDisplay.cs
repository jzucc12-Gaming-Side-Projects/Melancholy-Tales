using TMPro;
using UnityEngine;

namespace JZ.MENU
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI myText = null;

        public void Show(bool _show)
        {
            myText.enabled = _show;
        }

        public void SetText(string _text)
        {
            myText.enabled = true;
            myText.text = _text;
            myText.text = myText.text.Replace("NEWLINE", "\n");
        }

        public void SetText(Color _color)
        {
            myText.enabled = true;
            myText.color = _color;
        }

        public void SetText(string _text, Color _color)
        {
            SetText(_text);
            SetText(_color);
        }

        public void SetText(string _text, Color _color, int _size)
        {
            SetText(_text);
            SetText(_color);
            myText.fontSize = _size;
        }
    }

}