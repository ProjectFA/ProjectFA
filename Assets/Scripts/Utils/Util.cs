using System;
using System.Linq;
using UnityEngine;

public class Util
{
    /// <summary>
    /// �־��� ���� ������ ����(NikkeStatus)�� ���� HP, ATK, DEF�� ����� ��, ��ų ������ ������ �������� ��ȯ�մϴ�.
    /// </summary>
    public static int CalculateCombatPoint(NikkeInfo info, NikkeStatus status)
    {
        // ���� �������ͽ� ���
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
    /// ���� �������ͽ��� ����� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="hp">�⺻ hp</param>
    /// <param name="att">�⺻ att</param>
    /// <param name="def">�⺻ def</param>
    /// <param name="lv">�⺻ lv</param>
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
    /// ���� Ŭ������ ���� ���� ��ġ�� �����ɴϴ�.
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="att"></param>
    /// <param name="def"></param>
    public static void GetStatusIncreaseByNikkeClass(NikkeInfo nikkeInfo, out int hp, out int att, out int def)
    {
        // ������ ��ġ
        // - ȭ����
        // 675 30 3
        // - ������
        // 750 25 5
        // - �����
        // 825 20 6
        // - 20������� ������ ��
        // 5% �߰� ���
        hp = 0;
        att = 0;
        def = 0;
        switch (nikkeInfo.nikkeClass)
        {
            case "ȭ����":
                hp = 675;
                att = 30;
                def = 3;
                break;

            case "������":
                hp = 750;
                att = 25;
                def = 3;
                break;

            case "�����":
                hp = 825;
                att = 20;
                def = 6;
                break;
        }

        if (hp == 0)
            Debug.LogError("nikkeInfo.nikkeClass�� �����Ͱ� �߸��Ǿ����ϴ�.");
    }



    /// <summary>
    /// ������Ʈ�� �����ɴϴ�. ���� �ش� ������Ʈ�� ���ٸ� �߰��� �� ��ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T">������ ������Ʈ�� Ÿ���Դϴ�.</typeparam>
    /// <param name="go">������Ʈ�� ������ ������Ʈ�Դϴ�.</param>
    /// <returns>�ش� Ÿ���� ������Ʈ�� ��ȯ�մϴ�.</returns>
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        if (go == null)
        {
            Debug.Log("GetOrAddComponent ����: ���� ������Ʈ�� null�Դϴ�.");
            return null;
        }

        T component = go.GetComponent<T>();
        return component ?? go.AddComponent<T>();
    }


    /// <summary>
    ///     �θ�-�ڽ� ���迡�� �θ� ������Ʈ�� ����ؼ� �ڽ� ������Ʈ�� Ž��
    /// </summary>
    /// <typeparam name="T">
    ///     ã�� ������Ʈ�� Ÿ�� 
    /// </typeparam>
    /// <param name="go">
    ///     �ڽ� ������Ʈ�� ��ȸ�� �θ� ������Ʈ
    /// </param>
    /// <param name="name">
    ///     ������Ʈ�� �̸����� ã��. �Է����� ���� �� Ÿ�����θ� ã�Ƽ� ��ȯ
    /// </param>
    /// <param name="recursive">
    ///     ��������� Ž���� ���ΰ�? false �� ��������� Ž������ ����.
    /// </param>
    /// <returns>
    ///     ���ǿ� ���� �ڽ� ������Ʈ�� ã�Ҵٸ�, �� ������Ʈ�� ��ȯ�Ѵ�.
    ///     ã�� ���ߴٸ�, null �� ��ȯ�Ѵ�.
    /// </returns>
    public static T FindChild<T> (GameObject go, string name = null, bool recursive = false) 
        where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        // ��������� Ž������ ����.
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
        // ��������� Ž����.
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
    ///     �θ�-�ڽ� ���迡�� �θ� ������Ʈ�� ����ؼ� �ڽ� ������Ʈ�� Ž��
    /// </summary>
    /// <param name="go">
    ///     �ڽ� ������Ʈ�� ��ȸ�� �θ� ������Ʈ
    /// </param>
    /// <param name="name">
    ///     ������Ʈ�� �̸����� ã��. �Է����� ���� �� Ÿ�����θ� ã�Ƽ� ��ȯ
    /// </param>
    /// <param name="recursive">
    ///     ��������� Ž���� ���ΰ�? false �� ��������� Ž������ ����.
    /// </param>
    /// <returns>
    ///     ���ǿ� ���� �ڽ� ������Ʈ�� ã�Ҵٸ�, �� ������Ʈ�� ��ȯ�Ѵ�.
    ///     ã�� ���ߴٸ�, null �� ��ȯ�Ѵ�.
    /// </returns>
    public static GameObject FindChildObject(GameObject go, string name = null, bool recursive = false)
    {
        Transform tr = FindChild<Transform>(go, name, recursive);
        return tr == null ? null : tr.gameObject;
    }
}
