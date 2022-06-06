using TMPro;
using UnityEngine;
using UnityEngine.Playables;


//From Game Dev Guide
namespace JZ.CUTSCENE
{
    public class SubtitleClip : PlayableAsset
    {
        [TextArea(10,20)] public string subtitleText = "";
        public Color subtitleColor = Color.white;
        public bool keepBetweenClips = false;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

            SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
            subtitleBehaviour.subtitleText = subtitleText;
            subtitleBehaviour.keepBetweenClips = keepBetweenClips;
            subtitleBehaviour.subtitleColor = subtitleColor;
            return playable;
        }
    }
}