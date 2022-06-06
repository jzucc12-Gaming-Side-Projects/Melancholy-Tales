using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using JZ.CORE;

namespace JZ.BUTTON
{
    public class HoldButton : Button, IProgressible
    {
        [SerializeField] [Tooltip("Time held for button to activate")] float holdTimer = 2f;
        float currHoldtimer = 0;
        IEnumerator holdingRoutine;


        #region //Pointer events
        public override void OnPointerDown(PointerEventData eventData)
        {
            eventData.selectedObject = null;
            holdingRoutine = HoldCount(eventData);
            StartCoroutine(holdingRoutine);
        }

        public override void OnPointerClick(PointerEventData eventData) { } 

        public override void OnPointerUp(PointerEventData eventData)
        {
            currHoldtimer = 0;
            StopCoroutine(holdingRoutine);
        }
        #endregion

        #region //Hold methods
        IEnumerator HoldCount(PointerEventData data)
        {
            while(currHoldtimer < holdTimer)
            {
                Debug.Log(currHoldtimer);
                currHoldtimer += Time.deltaTime;
                yield return null;
            }
            onClick.Invoke();
        }

        public float GetProgressPercentage() => currHoldtimer / holdTimer;
        #endregion
    
        public float GetHoldTimer() { return holdTimer; }
    }

    #region //Editor
#if UNITY_EDITOR
     [CustomEditor(typeof(HoldButton))]
     public class HoldButtonEditor : UnityEditor.UI.ButtonEditor
     {
         public SerializedProperty holdtimerProp;

        protected override void OnEnable() 
        {
            base.OnEnable();
            holdtimerProp = serializedObject.FindProperty("holdTimer");
        }

         public override void OnInspectorGUI()
         {
             HoldButton holdButton = (HoldButton)target;
             base.OnInspectorGUI();

             serializedObject.Update();
             EditorGUILayout.PropertyField(holdtimerProp);
             serializedObject.ApplyModifiedProperties();
         }
     }
#endif
    #endregion
}

