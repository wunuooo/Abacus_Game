using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseGame : UIBase
{
    public void OnClickBackToTitle()
    {
        Destroy(UIMain.Instance.gameObject);
        SceneManager.Instance.LoadScene("Title");
    }

    public void OnClickSetting()
    {
        UIManager.Instance.Show<UISetting>();
    }

    public void OnClickExit()
    {
        //�˳���Ϸ
    }
}