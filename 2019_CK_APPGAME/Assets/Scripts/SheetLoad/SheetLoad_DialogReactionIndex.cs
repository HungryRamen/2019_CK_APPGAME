using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;
using DialogCommand;

namespace SheetLoad
{
    public class SheetLoad_DialogReactionIndex
    {
        public void IndexGet(string chID,out int startIndex,out int endIndex)
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/DialogReactionIndex");
            startIndex = Convert.ToInt32(jsonData[chID]["StartIndex"].ToString());
            endIndex = Convert.ToInt32(jsonData[chID]["EndIndex"].ToString());
        }
    }
}