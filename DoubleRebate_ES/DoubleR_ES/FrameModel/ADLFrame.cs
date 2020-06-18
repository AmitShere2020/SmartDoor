using DoubleR_ES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoubleR_ES.Json_Model;

namespace DoubleR_ES.FrameModel
{
    public class ADLFrame:Frame
    {
        public ADLFrame(MDFJson jsonData)
        {
            JsonData = jsonData;
        }

        public override void CreateHingeView()
        {
           
        }

        public override void CreateTopView()
        {
           
        }

        public override void CreateLockView()
        {
           
        }
    }
}
