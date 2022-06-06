using System.Collections;
using System.Collections.Generic;
using JZ.CORE;
using UnityEngine;


//Originally from Brackeys//
namespace JZ.AUDIO
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] List<Sound> mySounds = new List<Sound>();

        #region //Monobehaviour
        private void Awake()
        {
            if(mySounds.Count > 0) 
                SetUpSources();
        }

        protected virtual void OnEnable() 
        {
            VolumeManager.VolumeUpdated += UpdateVolumes;
            GameStartUp.SetUpDone += UpdateVolumes;
            SceneTransitionManager.StartTransitionOut += SceneTransition;
        }

        protected virtual void OnDisable() 
        {
            VolumeManager.VolumeUpdated -= UpdateVolumes;
            GameStartUp.SetUpDone -= UpdateVolumes;
            SceneTransitionManager.StartTransitionOut -= SceneTransition;
        }
        #endregion

        #region //Sound and source modification
        void SetUpSources()
        {
            foreach (Sound s in mySounds)
            {
                if(s.source != null) continue;
                GameObject child = new GameObject();
                child.transform.parent = transform;
                child.name = s.title + " sound";
                s.SetUpSource(child.AddComponent<AudioSource>());
            }
        }

        public void ChangeSoundName(string _name, int _id)
        {
            mySounds[_id].ChangeSoundName(_name);
        }

        public void AddSound(Sound s)
        {
            mySounds.Add(s);
            SetUpSources();
        }
        #endregion

        #region //Getters
        public int GetSoundCount() { return mySounds.Count; }
        #endregion

        #region //Using Sounds
        public Sound GetSound(string _name)
        {
            Sound sound = mySounds.Find(x => x.title == _name);

            if(sound == null)
            {
                Debug.LogWarning("Couldn't find clip");
                return null;
            }
            return sound;
        }

        public void Play(string _name)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return;

            if(sound.fading != null)
            {
                StopCoroutine(sound.fading);
            }
            sound.Play();
        }

        public bool IsPlaying(string _name)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return false;

            return sound.source.isPlaying;
        }
        
        public bool HasClip(string _name, AudioClip _clip)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return true;

            return sound.clip == _clip;
        }
        #endregion

        #region //Stopping
        public void Stop(string _name)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return;
            sound.source.Stop();
        }

        public void StopAllSFX()
        {
            foreach(Sound sound in mySounds)
            {
                if(sound.GetVolumeType() != VolumeType.sfx) continue;
                sound.source.Stop();
            }
        }
        #endregion

        #region //Fading
        public void FadeTo(string _name, float _fadeTime, float _finalVolumePercentage)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return;

            if(sound.fading == null)
                sound.fading = StartCoroutine(sound.FadeTo(_fadeTime, _finalVolumePercentage));
        }

        public void FadeOut(string _name, float _fadeTime)
        {
            FadeTo(_name, _fadeTime, 0);
        }

        public void FadeOutOneSecond(string _name)
        {
            FadeOut(_name, 1);
        }

        protected virtual void SceneTransition(bool _newSceneType)
        {
            if(!_newSceneType) return;
            float groupFadeOutTime = 1f;

            foreach(Sound sound in mySounds)
            {
                FadeTo(sound.title, groupFadeOutTime, 0);
            }
        }
        #endregion
    
        #region //Modification
        public void UpdateVolumes()
        {
            foreach(Sound sound in mySounds)
                sound.SetVolume();
        }
        
        public void ChangeClip(string _name, AudioClip _clip)
        {
            Sound sound = GetSound(_name);
            sound.ChangeClip(_clip);
        }
        #endregion
    }

    [System.Serializable]
    public class Sound
    {
        #region //Sound variables
        [Header("Sound Properties")]
        [HideInInspector] public AudioSource source;
        [SerializeField] public string title = "";
        [SerializeField] public AudioClip clip = null;
        [SerializeField] VolumeType myType = VolumeType.sfx;
        [HideInInspector] public Coroutine fading;

        [Header("Source Properties")]
        [Range(0f, 1f)] [SerializeField] float volume = 1f;
        [Range(0.1f, 3f)] [SerializeField] float pitch = 1f;
        [SerializeField] bool  isBackwards = false;
        [SerializeField] bool loop = false;
        [SerializeField] bool playOnStart = false;
        #endregion

        #region //Constructor
        public Sound(VolumeType _type, AudioClip _clip, string _title, float _volume, float _pitch, bool _loop = false, bool _backwards = false)
        {
            myType = _type;
            clip = _clip;
            title = _title;
            volume = _volume;
            pitch = _pitch;
            isBackwards = _backwards;
            loop = _loop;
        }
        #endregion

        #region //Sound modifciation
        public void SetUpSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
            SetVolume();
            source.pitch = pitch;
            source.loop = loop;
            source.playOnAwake = playOnStart;
            
            if(isBackwards)
            {
                source.pitch *= -1;
                source.timeSamples = source.clip.samples - 1;
            }

            if(playOnStart) Play();
        }

        public void ChangeSoundName(string _name)
        {
            title = _name;
            source.gameObject.name = _name + " sound";
        }
        #endregion
    
        #region //Playing
        public VolumeType GetVolumeType() { return myType; }

        public void Play()
        {
            fading = null;
            SetVolume();
            source.Play();
        }
        #endregion

        #region //Modification
        public void ChangeClip(AudioClip _clip)
        {
            clip = _clip;
            source.Stop();
            source.clip = _clip;
            Play();
        }

        public void SetVolume(float _mod = 1)
        {
            source.volume = volume * GameSettings.GetAdjustedVolume(myType) * _mod;
        }
        #endregion
        
        #region //Fading
        public float GetBaseVolume()
        {
            return volume * GameSettings.GetAdjustedVolume(myType);
        }
        
        public IEnumerator FadeTo(float _fadeTime, float _finalVolumePercentage)
        {
            float startVolumePercentage = source.volume / GetBaseVolume();
            float currTime = 0f;

            while(currTime < _fadeTime)
            {
                float newVolumePercentage = Mathf.Lerp(startVolumePercentage, _finalVolumePercentage, Mathf.Min(currTime / _fadeTime, 1));
                SetVolume(newVolumePercentage);
                currTime += Time.deltaTime;
                yield return null;
            }

            fading = null;
            SetVolume(_finalVolumePercentage);
            if(_finalVolumePercentage == 0) source.Stop();
        }
        #endregion
    }
}