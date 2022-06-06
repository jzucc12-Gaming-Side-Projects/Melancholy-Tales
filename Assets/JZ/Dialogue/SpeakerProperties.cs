using UnityEngine;

namespace JZ.DIALOGUE
{
    [System.Serializable]
    public class SpeakerProperties
    {
        [SerializeField] string speakerName = "";
        [SerializeField] Sprite speakerImage = null;
        [SerializeField] Color textColor = Color.white;

        public string GetSpeakerName() => speakerName;
        public Sprite GetSpeakerImage() => speakerImage;
        public Color GetTextColor() => textColor;
    }
}
