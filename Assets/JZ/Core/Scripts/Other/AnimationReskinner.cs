using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.CORE
{
    public class AnimationReskinner : MonoBehaviour
    {
        [SerializeField] string resourcePath;
        [SerializeField] string fileName;
        [SerializeField] bool isImage = false;
        [SerializeField] bool reset = false;
        Sprite[] subSprites;
        int places;

        private void Awake()
        {
            subSprites = Resources.LoadAll<Sprite>(resourcePath + fileName);
            places = subSprites.Length.ToString().Count();
        }

        private void LateUpdate()
        {
            if(reset)
            {
                reset = false;
                Awake();
            }

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(resourcePath)) return;
            

            if (isImage)
            {
                Replace(subSprites, GetComponentInChildren<Image>());
            }
            else
            {
                Replace(subSprites, GetComponentInChildren<SpriteRenderer>());
            }
        }

        void Replace(Sprite[] subSprites, Image renderer)
        {
            string spriteName = renderer.sprite.name;
            int id = GetID(spriteName);
            renderer.sprite = subSprites[id];
        }

        void Replace(Sprite[] subSprites, SpriteRenderer renderer)
        {
            string spriteName = renderer.sprite.name;
            int id = GetID(spriteName);
            renderer.sprite = subSprites[id];
        }

        int GetID(string spriteName)
        {
            string chars = spriteName.Substring(spriteName.Length - places);
            string[] sections = chars.Split('_');
            return int.Parse(sections.Last());
        }
    }

}