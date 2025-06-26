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
    /// 씬
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
    /// 어떤 방식으로 음원을 재생할 지 나타내요.
    /// </summary>
    public enum Sound
    {
        Bgm,
        Effect,
        Max
    }

    /// <summary>
    /// UI 이벤트 바인딩 시 사용
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
    /// 마우스 상태, 이벤트 바인딩 시 사용
    /// </summary>
    public enum MouseEvent
    {
        Click,
        Pressed,
        Release,
    }

    /// <summary>
    /// 카메라 모드
    /// </summary>
    public enum CameraMode
    {
        QuarterView,
    }

    public enum NikkeIconSortingType
    {
        CombatPointOrder, // 전투력 순
        DirectoryOrder,   // 사전 순
        LevelOrder,       // 레벨 순

        Max,
    }

    public enum NikkeIconFilterType
    {
        All,

        // 이름 기준
        Name,

        // 버스트 기준
        Burst1,
        Burst2,
        Burst3,

        // 총기 종류 기준
        AR,
        SR,
        SG,
        MG,
        SMG,
        RL,



        // 기업 기준
        // 음... 이건 나중에 할까요?

        // 
    }


    public enum NikkeIconClickContext
    {
        NikkeTab,
        SquadDetail
    }
}
