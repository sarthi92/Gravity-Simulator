using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Models.Enums
{
    public enum CmdType
    {
        ADD, //add to temp
        UPD, //update temp
        DEL, //del from temp
        LOD, //script to temp
        UNV, //univ to temp
        CLS, //discard temp
        SAV, //temp to script
        EXE, //temp to univ
        REC, //record univ
    }
}
