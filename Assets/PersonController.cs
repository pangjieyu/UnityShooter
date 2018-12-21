using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonController : MonoBehaviour {
    string clothStr = "sum";//当前服装标识
    Animation anim;//动画组件
    Animation[] anims;//三人的动画组件
    //初始化函数
	void Start () {
        GameObject mi = Instantiate(Resources.Load("Utc_" + clothStr + "_humanoid")) as GameObject;
        mi.transform.parent = transform;
        anim = mi.GetComponent<Animation>();
        anims = new Animation[3];
        anims[0] = anim;
    }
	//每帧都会执行的函数
	void Update () {
        //按键控制跑步动画举例
		if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.Play("Running@loop");
        } 
      
	}

    //播放动画
    private void PlayAnim(string str)
    {
        for(int i=0;i<3;++i)
        {
            if (anims[i])
                anims[i].Play(str);
            else break;
        }
    }

    public void OnClickDown()
    {
        PlayAnim("GoDown");//播放倒下动画
    }

    public void OnClickDamage()
    {
        PlayAnim("Damaged@loop");//播放挨打动画
    }

    public void OnClickDownToUp()
    {
        PlayAnim("DownToUp");//播放倒地后起身动画
    }

    public void OnClickKnee()
    {
        PlayAnim("KneelDown");//播放跪下动画
    }

    public void OnClickSalute()
    {
        PlayAnim("Salute");//播放敬礼动画
    }

    public void OnClickJump()
    {
        PlayAnim("Jumping@loop");//播放跳高动画
    }

    //多人按钮触发函数
    public void OnClickAddPeople(GameObject tog)
    {
        if(tog.GetComponent<Toggle>().isOn)
        {

        GameObject mi = Instantiate(Resources.Load("Misaki_"+ clothStr+"_humanoid")) as GameObject;
        GameObject Yu = Instantiate(Resources.Load("Yuko_" + clothStr + "_humanoid")) as GameObject;

        anims[1] = mi.GetComponent<Animation>();
        anims[2] = Yu.GetComponent<Animation>();
        }
        else
        {
            DestroyImmediate(anims[1].gameObject);
            DestroyImmediate(anims[2].gameObject);
            anims[1] = null;
            anims[2] = null;
        }
    }

    //切换服装按钮触发函数
    public void OnClickChangeCloth(Dropdown dd)
    {
        if (dd.value == 1)
        {
            clothStr = "win";
        }
        else
        {
            clothStr = "sum";
        }
        ChangeCloth();
    }

    //更换衣服
    private void ChangeCloth()
    {
                Destroy(anims[0].gameObject);
                GameObject mi0 = Instantiate(Resources.Load("Utc_" + clothStr + "_humanoid")) as GameObject;
                mi0.transform.parent = this.transform;
                anims[0] = mi0.GetComponent<Animation>();
           if (anims[1])
            {
                Destroy(anims[1].gameObject);
                GameObject mi = Instantiate(Resources.Load("Misaki_" + clothStr + "_humanoid")) as GameObject;
                anims[1] = mi.GetComponent<Animation>();
            }
             if (anims[2])
            {
                Destroy(anims[2].gameObject);
                GameObject mi = Instantiate(Resources.Load("Yuko_" + clothStr + "_humanoid")) as GameObject;
                anims[2] = mi.GetComponent<Animation>();
            }
        
    }

}
