using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Services.UI
{

    public interface IGameUIManager
    {
        GameObject InstantiateLikeUIElement(GameObject prefab);

        T OpenPopup<T>() where T: Views.UI.IPopupView;

        bool ClosePopup<T>() where T : Views.UI.IPopupView;

        bool ClosePopup(Views.UI.IPopupView popupInstance);

    }

    public class GameUIManager : IGameUIManager
    {

        class GameUIManagerWorker : MonoBehaviour
        {


        }

        GameUIManagerWorker uiManagerWorker;

        Canvas mainCanvas;
        List<Views.UI.IPopupView> openedPopups = new List<Views.UI.IPopupView>();

        public GameUIManager()
        {
            uiManagerWorker = new GameObject("UIManagerWorker").AddComponent<GameUIManagerWorker>();
            mainCanvas = MonoBehaviour.FindObjectOfType<Canvas>();
        }

        public GameObject InstantiateLikeUIElement(GameObject prefab)
        {
            return MonoBehaviour.Instantiate(prefab, mainCanvas.transform, false);
        }

        public T OpenPopup<T>() where T : Views.UI.IPopupView
        {
            T newPopup = MainContext.InjectionBinder.GetInstance<T>();
            openedPopups.Add(newPopup as Views.UI.IPopupView);
            return newPopup;
        }

        public bool ClosePopup<T>() where T : Views.UI.IPopupView
        {
            Views.UI.IPopupView nowPopup = openedPopups.Find(popup => popup.GetType().Equals(typeof(T)));
            if(nowPopup != null)
            {
                return ClosePopup(nowPopup);
            }
            return false;
        }

        public bool ClosePopup(Views.UI.IPopupView popupInstance)
        {
            if (openedPopups.Contains(popupInstance))
            {
                openedPopups.Remove(popupInstance);
                popupInstance.Close();
                popupInstance = null;
                return true;
            }
            return false;
        }

    }

}