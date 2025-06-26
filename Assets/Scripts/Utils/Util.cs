using System;
using System.Linq;
using UnityEngine;

public class Util
{
    /// <summary>
    /// 주어진 니케 정보와 상태(NikkeStatus)로 현재 HP, ATK, DEF를 계산한 뒤, 스킬 레벨을 포함한 전투력을 반환합니다.
    /// </summary>
    public static int CalculateCombatPoint(NikkeInfo info, NikkeStatus status)
    {
        // 현재 스테이터스 계산
        CalCurNikkeStatus(info, out int curHp, out int curAtt, out int curDef, status.level);

        double raw = (0.7 * curHp
                      + 19.35 * curAtt
                      + 70 * curDef)
                     * (1.3
                        + status.skillLevels[0] * 0.01
                        + status.skillLevels[1] * 0.01
                        + status.skillLevels[2] * 0.02);

        return (int)Math.Truncate(raw / 100f);
    }


    /// <summary>
    /// 니케 스테이터스를 계산해 반환합니다.
    /// </summary>
    /// <param name="hp">기본 hp</param>
    /// <param name="att">기본 att</param>
    /// <param name="def">기본 def</param>
    /// <param name="lv">기본 lv</param>
    public static void CalCurNikkeStatus(NikkeInfo nikkeInfo, out int hp, out int att, out int def, int lv)
    {
        hp = nikkeInfo.hp;
        att = nikkeInfo.attack;
        def = nikkeInfo.defense;

        GetStatusIncreaseByNikkeClass(nikkeInfo, out int hpInc, out int attInc, out int defInc);

        int mul = (lv - 1) / 20 + 1;
        for (int i = 1; i < lv; i++)
        {
            if (i % 20 == 0)
            {
                hp = (int)(hp * 1.05f);
                att = (int)(att * 1.05f);
                def = (int)(def * 1.05f);
            }
            else
            {
                hp += hpInc * mul;
                att += attInc * mul;
                def += defInc * mul;
            }

            if (i % 20 == 0)
                mul++;
        }
    }

    /// <summary>
    /// 니케 클래스에 따라서 증가 수치를 가져옵니다.
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="att"></param>
    /// <param name="def"></param>
    public static void GetStatusIncreaseByNikkeClass(NikkeInfo nikkeInfo, out int hp, out int att, out int def)
    {
        // 레벨업 수치
        // - 화력형
        // 675 30 3
        // - 지원형
        // 750 25 5
        // - 방어형
        // 825 20 6
        // - 20배수에서 레벨업 시
        // 5% 추가 상승
        hp = 0;
        att = 0;
        def = 0;
        switch (nikkeInfo.nikkeClass)
        {
            case "화력형":
                hp = 675;
                att = 30;
                def = 3;
                break;

            case "지원형":
                hp = 750;
                att = 25;
                def = 3;
                break;

            case "방어형":
                hp = 825;
                att = 20;
                def = 6;
                break;
        }

        if (hp == 0)
            Debug.LogError("nikkeInfo.nikkeClass의 데이터가 잘못되었습니다.");
    }



    /// <summary>
    /// 컴포넌트를 가져옵니다. 만약 해당 컴포넌트가 없다면 추가한 후 반환합니다.
    /// </summary>
    /// <typeparam name="T">가져올 컴포넌트의 타입입니다.</typeparam>
    /// <param name="go">컴포넌트를 가져올 오브젝트입니다.</param>
    /// <returns>해당 타입의 컴포넌트를 반환합니다.</returns>
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        if (go == null)
        {
            Debug.Log("GetOrAddComponent 오류: 게임 오브젝트가 null입니다.");
            return null;
        }

        T component = go.GetComponent<T>();
        return component ?? go.AddComponent<T>();
    }


    /// <summary>
    ///     부모-자식 관계에서 부모 오브젝트를 사용해서 자식 오브젝트를 탐색
    /// </summary>
    /// <typeparam name="T">
    ///     찾을 오브젝트의 타입 
    /// </typeparam>
    /// <param name="go">
    ///     자식 오브젝트를 순회할 부모 오브젝트
    /// </param>
    /// <param name="name">
    ///     오브젝트를 이름으로 찾기. 입력하지 않을 시 타입으로만 찾아서 반환
    /// </param>
    /// <param name="recursive">
    ///     재귀적으로 탐색할 것인가? false 시 재귀적으로 탐색하지 않음.
    /// </param>
    /// <returns>
    ///     조건에 맞은 자식 오브젝트를 찾았다면, 그 오브젝트를 반환한다.
    ///     찾지 못했다면, null 을 반환한다.
    /// </returns>
    public static T FindChild<T> (GameObject go, string name = null, bool recursive = false) 
        where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        // 재귀적으로 탐색하지 않음.
        if (!recursive)
        {
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                Transform transform = go.transform.GetChild(i);

                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        // 재귀적으로 탐색함.
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }


        return null;
    }

    /// <summary>
    ///     부모-자식 관계에서 부모 오브젝트를 사용해서 자식 오브젝트를 탐색
    /// </summary>
    /// <param name="go">
    ///     자식 오브젝트를 순회할 부모 오브젝트
    /// </param>
    /// <param name="name">
    ///     오브젝트를 이름으로 찾기. 입력하지 않을 시 타입으로만 찾아서 반환
    /// </param>
    /// <param name="recursive">
    ///     재귀적으로 탐색할 것인가? false 시 재귀적으로 탐색하지 않음.
    /// </param>
    /// <returns>
    ///     조건에 맞은 자식 오브젝트를 찾았다면, 그 오브젝트를 반환한다.
    ///     찾지 못했다면, null 을 반환한다.
    /// </returns>
    public static GameObject FindChildObject(GameObject go, string name = null, bool recursive = false)
    {
        Transform tr = FindChild<Transform>(go, name, recursive);
        return tr == null ? null : tr.gameObject;
    }
}
