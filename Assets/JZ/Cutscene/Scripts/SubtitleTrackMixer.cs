using JZ.DIALOGUE;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace JZ.CUTSCENE
{
    //From Game Dev Guide
    public class SubtitleTrackMixer : PlayableBehaviour
    {
        DisplayInfo lastDisplay = new DisplayInfo();
        bool keepBetweenClips = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TextMeshProUGUI subtitle = playerData as TextMeshProUGUI;
            TextPrinter printer = subtitle.GetComponent<TextPrinter>();
            DisplayInfo currentDisplay = new DisplayInfo();

            if (!subtitle) return;

            int inputCount = playable.GetInputCount();
            bool updateCurrent = false;
            for (int ii = 0; ii < inputCount; ii++)
            {
                float inputWeight = playable.GetInputWeight(ii);
                if (inputWeight <= 0) continue;

                ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(ii);
                SubtitleBehaviour input = inputPlayable.GetBehaviour();

                keepBetweenClips = input.keepBetweenClips;
                currentDisplay.text = input.subtitleText;
                currentDisplay.alpha = inputWeight;
                currentDisplay.textColor = input.subtitleColor;
                lastDisplay = new DisplayInfo(currentDisplay);
                updateCurrent = true;
            }
            
            if(!updateCurrent && keepBetweenClips)
                UpdateSubtitle(subtitle, printer, lastDisplay);
            else
                UpdateSubtitle(subtitle, printer, currentDisplay);
        }

        private static void UpdateSubtitle(TextMeshProUGUI _subtitle, TextPrinter _printer, DisplayInfo _info)
        {
            Color fade = _info.textColor;
            fade.a = _info.alpha;
            _subtitle.color = fade;

            if (_printer)
            {
                if (_printer.GetFinalText() == _info.text) return;
                _printer.PrintNewText(_info.text);
            }
            else _subtitle.text = _info.text;
        }
    }

    struct DisplayInfo
    {
        public string text;
        public Color textColor;
        public float alpha;

        public DisplayInfo(string _text = "", float _alpha = 0)
        {
            text = "";
            textColor = Color.white;
            alpha = 0;
        }

        public DisplayInfo(DisplayInfo _reference)
        {
            text = _reference.text;
            textColor = _reference.textColor;
            alpha = _reference.alpha;
        }
    }
}
