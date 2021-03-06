﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Colony.Menu
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject splashScreen, poliImg;
        public GameObject background;
        public GameObject menu;
        public GameObject credits;
        public GameObject leaderboard;
        public GameObject settings;

        private static bool isFirstRun = true;

        void Start()
        {
            if (isFirstRun)
            {
                HideMenus();
                StartCoroutine(ShowSplashScreen());
                isFirstRun = false;
            }
            else
            {
                background.SetActive(true);
                ShowMenu();
            }
        }

        public void Play()
        {
            SceneManager.LoadScene(GameManager.SceneName);
        }

        public void ShowMenu()
        {
            menu.SetActive(true);
            credits.SetActive(false);
            leaderboard.SetActive(false);
            settings.SetActive(false);
        }

        public void ShowCredits()
        {
            menu.SetActive(false);
            credits.SetActive(true);
            leaderboard.SetActive(false);
            settings.SetActive(false);
        }

        public void ShowLeaderboard()
        {
            menu.SetActive(false);
            credits.SetActive(false);
            leaderboard.SetActive(true);
            settings.SetActive(false);
        }

        public void ShowSettings()
        {
            menu.SetActive(false);
            credits.SetActive(false);
            leaderboard.SetActive(false);
            settings.SetActive(true);
        }

        public void HideMenus()
        {
            background.SetActive(false);
            menu.SetActive(false);
            credits.SetActive(false);
            leaderboard.SetActive(false);
            settings.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }

        IEnumerator ShowSplashScreen()
        {
            splashScreen.SetActive(true);
            yield return new WaitForSeconds(2.0f);
		poliImg.SetActive(false);
		yield return new WaitForSeconds(2.0f);
		splashScreen.SetActive(false);
            background.SetActive(true);
            ShowMenu();
            yield return null;
        }
    }
}
