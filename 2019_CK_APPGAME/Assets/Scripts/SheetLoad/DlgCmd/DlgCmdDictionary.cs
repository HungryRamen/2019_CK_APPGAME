using System.Collections.Generic;
namespace DialogCommand
{

    public static class DlgCmdDictionary
    {
        public static Dictionary<string, DlgCmd> commandDictionary = new Dictionary<string, DlgCmd>();

        public static void Set()
        {
            if (commandDictionary.Count == 0)
            {
                commandDictionary.Add("출력속도", new DlgCmd_TextTime());
                commandDictionary.Add("크기", new DlgCmd_TextSize());
                commandDictionary.Add("색상", new DlgCmd_TextColor());
                commandDictionary.Add("이미지", new DlgCmd_CharImg());
                commandDictionary.Add("화면흔들기", new DlgCmd_ScreenShakeOn());
                commandDictionary.Add("흔들기끝", new DlgCmd_ScreenShakeOff());
                commandDictionary.Add("주문", new DlgCmd_Order());
                commandDictionary.Add("ch", new DlgCmd_Ch());
                commandDictionary.Add("점프", new DlgCmd_Jump());
                commandDictionary.Add("선택지", new DlgCmd_Select());
                commandDictionary.Add("입장", new DlgCmd_CharJoin());
                commandDictionary.Add("퇴장", new DlgCmd_CharExit());
                commandDictionary.Add("종료", new DlgCmd_End());
                commandDictionary.Add("IF", new DlgCmd_DayCheck());
                commandDictionary.Add("음식보너스", new DlgCmd_FoodBonus());
                commandDictionary.Add("리액션", new DlgCmd_FoodReaction());
                commandDictionary.Add("스테이터스업데이트", new DlgCmd_StatusUpdate());
                commandDictionary.Add("플레이어이름", new DlgCmd_PlayerName());
                commandDictionary.Add("재료해금", new DlgCmd_FoodMaterialUnLock());
                commandDictionary.Add("음료", new DlgCmd_Drink());
            }
        }


        public static string[] CommandSubstring(string str, int index, int lastIndex)
        {
            string[] array = new string[2];
            int num = index + 1;
            int num2 = str.IndexOf("::", index,lastIndex - index);
            if (num2 == -1)
            {
                array[0] = str.Substring(num, lastIndex - num);
                return array;
            }
            int num3 = num2 + 2;
            array[0] = str.Substring(num, num2 - num);
            array[1] = str.Substring(num3, lastIndex - num3);
            return array;
        }
    }
}
