using UnityEngine;

public class Define
{
    public enum MainTab
    {
        Nikke,
        Squad,
        Lobby,
        Inventory,
        Recruit,
    }

    public enum Item
    {
        None,
        Jewel,
        Credit,

    }

    /// <summary>
    /// ��
    /// </summary>
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
        Operation,
    }

    /// <summary>
    /// � ������� ������ ����� �� ��Ÿ����.
    /// </summary>
    public enum Sound
    {
        Bgm,
        Effect,
        Max
    }

    /// <summary>
    /// UI �̺�Ʈ ���ε� �� ���
    /// </summary>
    public enum UIEvent
    {
        Cilck,
        BeginDrag,
        Drag,
        EndDrag,
        Drop,
    }

    /// <summary>
    /// ���콺 ����, �̺�Ʈ ���ε� �� ���
    /// </summary>
    public enum MouseEvent
    {
        Click,
        Pressed,
        Release,
    }

    /// <summary>
    /// ī�޶� ���
    /// </summary>
    public enum CameraMode
    {
        QuarterView,
    }

    public enum NikkeIconSortingType
    {
        CombatPointOrder, // ������ ��
        DirectoryOrder,   // ���� ��
        LevelOrder,       // ���� ��

        Max,
    }

    public enum NikkeIconFilterType
    {
        All,

        // �̸� ����
        Name,

        // ����Ʈ ����
        Burst1,
        Burst2,
        Burst3,

        // �ѱ� ���� ����
        AR,
        SR,
        SG,
        MG,
        SMG,
        RL,



        // ��� ����
        // ��... �̰� ���߿� �ұ��?

        // 
    }


    public enum NikkeIconClickContext
    {
        NikkeTab,
        SquadDetail
    }
}
