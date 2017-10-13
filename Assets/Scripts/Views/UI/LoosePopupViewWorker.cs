using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Views.UI
{

    class LoosePopupViewWorker : MonoBehaviour
    {

        [SerializeField]
        Text textAttempsRemaning;
        [SerializeField]
        GameObject buttonContinue;

        public Text TextAttempsRemaning { get { return textAttempsRemaning; } }
        public GameObject ButtonContinue { get { return buttonContinue; } }

        public void ButtonContinue_OnClick()
        {
            MainContext.InjectionBinder.GetInstance<Controllers.UI.LoosePopupButtonClickSignal>().Dispatch(Controllers.UI.LoosePopupButtonType.Continue);
        }

        public void ButtonReplay_OnClick()
        {
            MainContext.InjectionBinder.GetInstance<Controllers.UI.LoosePopupButtonClickSignal>().Dispatch(Controllers.UI.LoosePopupButtonType.Replay);
        }

    }

    public class LoosePopupView : IPopupView
    {

        LoosePopupViewWorker loosePopupViewWorker;

        public LoosePopupView()
        {
            loosePopupViewWorker = MainContext.InjectionBinder.GetInstance<Services.UI.IGameUIManager>().
                InstantiateLikeUIElement(Models.UI.GameUIModel.Instance.LoosePopupPrefab).GetComponent<LoosePopupViewWorker>();
        }

        public void Close()
        {
            MonoBehaviour.Destroy(loosePopupViewWorker.gameObject);
            loosePopupViewWorker = null;
        }

        public void SetAttempsRemaningCount(byte count)
        {
            loosePopupViewWorker.TextAttempsRemaning.text = "Attemps remaning: " + count;
        }

        public void SetButtonContinueVisible(bool visible)
        {
            loosePopupViewWorker.ButtonContinue.SetActive(visible);
        }

    }

}