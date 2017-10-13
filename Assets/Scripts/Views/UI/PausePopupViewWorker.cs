using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Views.UI
{
    class PausePopupViewWorker : MonoBehaviour
    {


        public void ButtonContinue_OnClick()
        {
            MainContext.InjectionBinder.GetInstance<Controllers.ChangePauseStateSignal>().Dispatch(false);
        }

    }

    public class PausePopupView : IPopupView
    {

        PausePopupViewWorker pausePopupViewWorker;

        public PausePopupView()
        {
            pausePopupViewWorker = MainContext.InjectionBinder.GetInstance<Services.UI.IGameUIManager>().
                InstantiateLikeUIElement(Models.UI.GameUIModel.Instance.PausePopupPrefab).GetComponent<PausePopupViewWorker>();
        }

        public void Close()
        {
            MonoBehaviour.Destroy(pausePopupViewWorker.gameObject);
            pausePopupViewWorker = null;
        }

    }


}