using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class asyncloadscene : MonoBehaviour
{
    public Image loadingSlider;
    public Image player;
    private float loadingSpeed = 1;

    private float targetValue;

    private AsyncOperation async;
    public int scenenum;
    private void Start()

    {
        StartCoroutine(AsyncLoading());

    }

    IEnumerator AsyncLoading()

    {
        async = SceneManager.LoadSceneAsync(scenenum);
        async.allowSceneActivation = false;
        yield return async;

    }
    IEnumerator Gonextscene()
    {
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;


    }
    void Update()

    {

        if (async == null) { return; }

        targetValue = async.progress;

        if (async.progress >= 0.9f)

        {
            targetValue = 1.0f;

        }


        if (targetValue != loadingSlider.fillAmount)

        {

            loadingSlider.fillAmount = Mathf.Lerp(loadingSlider.fillAmount, targetValue, Time.deltaTime * loadingSpeed);
            player.rectTransform.localPosition = new Vector3(-940f + 1880f * loadingSlider.fillAmount, -458f, 0);
            if (Mathf.Abs(loadingSlider.fillAmount - targetValue) < 0.01f)
            {

                loadingSlider.fillAmount = targetValue;
            }

        }


        if ((int)(loadingSlider.fillAmount * 100) == 100)

        {
            StartCoroutine(Gonextscene());
        }

    }

}
