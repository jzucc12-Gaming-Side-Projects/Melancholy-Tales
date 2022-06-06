using System.Collections.Generic;
using System.Linq;
using JZ.INPUT;
using JZ.AUDIO;
using UnityEngine;
using UnityEngine.UI;
using JZ.CORE;

namespace JZ.MENU
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(AudioManager))]
    public class MenuManager : MonoBehaviour
    {
        #region//Cached variables
        [SerializeField] protected List<MenuMember> members = new List<MenuMember>();
        GeneralInputs inputs;
        protected AudioManager sfxManager;
        protected MenuingInputSystem menuSystem;
        Canvas myCanvas;
        GameObject blockingWindow;
        static List<MenuManager> openedMenus;
        List<MenuMember> originalMembers = new List<MenuMember>();
        #endregion

        #region//Menu variables
        int currentLocation = 0;
        [SerializeField] [Tooltip("Is the menu active upon instantiation?")] bool startOff = false;
        [SerializeField] [Tooltip("Should this be the only interactable menu when active?")] bool soleMenu = true;
        [SerializeField] [Tooltip("Should this menu only ever open once?")] bool singleOpen = false;
        [SerializeField] [Tooltip("Is navigation horizontal as opposed to vertical?")] bool isHorizontalMenu = false;
        [SerializeField] [Tooltip("Should the cursor loop when passing the end bounds?")] protected bool shouldLoop = true;
        MenuMember activeMember => members[currentLocation];
        bool opened = false;
        #endregion

        #region //SFX
        [SerializeField] bool useDefaultSFX = true;
        protected string moveSFX = "menu move";
        string selectSFX = "menu select";
        #endregion


        #region//Monobehaviour
        protected virtual void Awake()
        {
            var blockingWindowPrefab = Resources.Load<GameObject>("Blocking Window");
            blockingWindow = Instantiate(blockingWindowPrefab, transform);
            blockingWindow.transform.SetAsFirstSibling();

            openedMenus = new List<MenuManager>();
            sfxManager = GetComponent<AudioManager>();
            myCanvas = GetComponent<Canvas>();
            inputs = new GeneralInputs();
            menuSystem = new MenuingInputSystem(inputs);

            for(int ii = 0; ii < members.Count; ii++)
                originalMembers.Add(members[ii]);
        }

        private void OnEnable() 
        {
            SceneTransitionManager.StartTransitionOut += TransitionShutDown;
        }

        private void OnDisable() 
        {
            SceneTransitionManager.StartTransitionOut -= TransitionShutDown;
        }

        protected virtual void Start()
        {
            RemoveInactiveMembers();

            Canvas.ForceUpdateCanvases();
            foreach(var layout in GetComponentsInChildren<VerticalLayoutGroup>())
            {
                layout.enabled = false;
                layout.enabled = true;
            }

            foreach(var layout in GetComponentsInChildren<HorizontalLayoutGroup>())
            {
                layout.enabled = false;
                layout.enabled = true;
            }

            if (useDefaultSFX) AutoSetUpSFXManager();
            else SetSFXNames();

            if (startOff) ShutDown();
            else StartUp();
        }

        private void OnDestroy() 
        {
            ShutDown(false);    
        }

        protected virtual void Update()
        {
            CheckNavigation();
            CheckSelect();
            CheckSlider();
        }
        #endregion

        #region//Start up/Shutdown
        private void RemoveInactiveMembers()
        {
            List<MenuMember> toRemove = new List<MenuMember>();
            foreach (MenuMember member in members)
            {
                if (member.enabled && member.gameObject.activeInHierarchy) continue;
                toRemove.Add(member);
            }

            members.RemoveAll(x => toRemove.Contains(x));
        }
        
        void SetSFXNames()
        {
            sfxManager.ChangeSoundName(moveSFX, sfxManager.GetSoundCount() - 2);
            sfxManager.ChangeSoundName(selectSFX, sfxManager.GetSoundCount() - 1);
        }

        void AutoSetUpSFXManager()
        {
            AudioClip moveClip = Resources.Load<AudioClip>("SFX/Menu Move");
            AudioClip selectClip = Resources.Load<AudioClip>("SFX/Menu Select");

            Sound menuMove = new Sound(VolumeType.sfx, moveClip, moveSFX, 0.3f, 1);
            Sound menuSelect = new Sound(VolumeType.sfx, selectClip, selectSFX, 0.3f, 1);
            sfxManager.AddSound(menuMove);
            sfxManager.AddSound(menuSelect);
        }

        public void StartUp()
        {
            if (singleOpen && opened) return;
            opened = true;
            enabled = true;
            myCanvas.enabled = true;
            menuSystem.Activate(true);
            currentLocation = 0;
            activeMember.Hover(true);
            openedMenus.Add(this);

            foreach (MenuMember member in members)
            {
                member.PointerEnter += OnHover;
                if(member.GetComponent<Button>())
                {
                    member.GetComponent<Button>().onClick.AddListener(PlaySelectSFX);
                }
            }

            SoleMenuActivation();
        }

        public void ShutDown(bool _activateNext = true)
        {
            enabled = false;
            myCanvas.enabled = false;
            menuSystem.Activate(false);
            currentLocation = 0;
            if(openedMenus.Contains(this))
                openedMenus.Remove(this);

            foreach (MenuMember member in members)
            {
                member.Hover(false);
                member.PointerEnter -= OnHover;
                if(member.GetComponent<Button>())
                {
                    member.GetComponent<Button>().onClick.RemoveListener(PlaySelectSFX);
                }
            }

            SoleMenuDeactivation(_activateNext);
        }
        #endregion

        #region//Navigation and selection
        void PlaySelectSFX()
        {
            sfxManager.Play(selectSFX);
        }

        void CheckNavigation()
        {
            bool passLowerBound = false;
            bool passUpperBound = false;
            int nav = isHorizontalMenu ? menuSystem.xNav : -menuSystem.yNav;

            if (nav != 0)
            {
                int newLocation = currentLocation + nav;

                if (newLocation < 0)
                    passLowerBound = true;
                else if (newLocation >= members.Count)
                    passUpperBound = true;

                if(passLowerBound)
                    PassMenuBounds(false);
                else if(passUpperBound)
                    PassMenuBounds(true);
                else
                {
                    if(newLocation != currentLocation && members[newLocation].enabled)
                        sfxManager.Play(moveSFX);

                    SetPosition(newLocation);
                }
            }
        }

        protected virtual void PassMenuBounds(bool _passLastItem)
        {
            if(shouldLoop && members.Count > 1) 
            {
                sfxManager.Play(moveSFX);
            }

            int newLocation = (_passLastItem ^ shouldLoop ?
                               members.Count - 1 : 0);

            SetPosition(newLocation);
        }

        protected void SetPosition(int _location, bool _invoke = true)
        {
            activeMember.Hover(false);
            currentLocation = _location;
            activeMember.Hover(true);
            if(_invoke) MemberHovered(activeMember);
            menuSystem.ExpendAllDir();
        }

        void CheckSelect()
        {
            if (menuSystem.selected)
            {
                if(activeMember.GetComponent<Button>())
                {
                    Button button = activeMember.GetComponent<Button>();
                    button.onClick?.Invoke();
                }
                menuSystem.ExpendMenuSelect();
            }
        }

        void CheckSlider()
        {
            if(menuSystem.xNav != 0)
            {
                if(activeMember.GetComponent<Slider>())
                {
                    Slider slider = activeMember.GetComponent<Slider>();
                    slider.value += Mathf.Sign(menuSystem.xNav) * .01f;
                }
            }
        }

        void OnHover(MenuMember _member)
        {
            for(int ii = 0; ii < members.Count; ii++)
            {
                if(members[ii] == _member)
                {
                    if(ii != currentLocation)
                        sfxManager.Play(moveSFX);

                    SetPosition(ii);
                    break;
                }
            }
        }

        protected virtual void MemberHovered(MenuMember _member) { }
        #endregion
    
        #region //Locking
        public void LockControl(bool _lock)
        {
            menuSystem.Activate(!_lock);
            enabled = !_lock;
            activeMember.Hover(!_lock);
            menuSystem.ExpendAllDir();
            menuSystem.ExpendMenuSelect();
        }

        private void SoleMenuActivation()
        {
            if (!soleMenu) return;
            
            blockingWindow.SetActive(true);
            foreach (MenuManager menu in openedMenus)
                menu.LockControl(menu != this);
        }

        private void SoleMenuDeactivation(bool _activateNext)
        {
            if (!soleMenu) return;

            blockingWindow.SetActive(false);
            foreach (MenuManager menu in openedMenus)
                menu.LockControl(menu == this);

            if(openedMenus.Count <= 0) return;
            if(!_activateNext) return;
            MenuManager nextMenu = openedMenus.Last();
            nextMenu.SoleMenuActivation();
        }
        
        private void TransitionShutDown(bool _)
        {
            menuSystem.Activate(false);
        }
        #endregion
    
        #region //Member management
        public void RemakeMembers(bool _setPosition)
        {
            activeMember.Hover(false);
            members.Clear();
            var membersInChildren = GetComponentsInChildren<MenuMember>();
            for(int ii = 0; ii < membersInChildren.Length; ii++)
            {
                members.Add(membersInChildren[ii]);
            }

            if(_setPosition)
                SetPosition(currentLocation);
        }

        public void RestoreOriginalMembers()
        {
            members.Clear();
            for(int ii = 0; ii < originalMembers.Count; ii++)
                members.Add(originalMembers[ii]);
        }
    
        public void AddMember(MenuMember _member)
        {
            members.Add(_member);
        }
        #endregion
    }
}