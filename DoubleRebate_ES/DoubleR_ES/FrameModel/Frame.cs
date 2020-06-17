using System.Collections.Generic;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using DoubleR_ES.Models;

namespace DoubleR_ES.FrameModel
{
    public abstract class Frame
    {
        public List<Entity> HingEntities { get; protected set; }

        public List<Entity> TopEntities { get; protected set; }

        public List<Entity> LockEntities { get; protected set; }

        protected BendCollection RequiredHingeList { get; set; }
        protected BendCollection RequiredTopList { get; set; }

        public JsonData JsonData { get; protected set; }

        protected Frame()
        {
            HingEntities = new List<Entity>();
            TopEntities = new List<Entity>();
            LockEntities = new List<Entity>();
            RequiredHingeList = new BendCollection();
            RequiredTopList = new BendCollection();
        }

        protected void AssignHingeDisplacements()
        {
            var allowances = JsonData.BendDataList;
            allowances[LineType.Return_1].ModifiedLengthTxt = Utilities.InputData.Return1;
            allowances[LineType.Return_1].Displacement = Utilities.InputData.Return1 - JsonData.ProfileInfo.Return1;

            allowances[LineType.Architrave_1].ModifiedLengthTxt = Utilities.InputData.Architrave1;
            allowances[LineType.Architrave_1].Displacement = Utilities.InputData.Architrave1 - JsonData.ProfileInfo.Architrave1;

            allowances[LineType.Rebate_1].ModifiedLengthTxt = Utilities.InputData.Rebate1;
            allowances[LineType.Rebate_1].Displacement = Utilities.InputData.Rebate1 - JsonData.ProfileInfo.Rebate1;

            allowances[LineType.StopHgt_1].ModifiedLengthTxt = Utilities.InputData.StopHgt1;
            allowances[LineType.StopHgt_1].Displacement = Utilities.InputData.StopHgt1 - JsonData.ProfileInfo.StopHgt1;

            allowances[LineType.Throat].ModifiedLengthTxt = Utilities.InputData.Throat;
            allowances[LineType.Throat].Displacement = Utilities.InputData.Throat - JsonData.ProfileInfo.Throat;

            allowances[LineType.StopHgt_2].ModifiedLengthTxt = Utilities.InputData.StopHgt2;
            allowances[LineType.StopHgt_2].Displacement = Utilities.InputData.StopHgt2 - JsonData.ProfileInfo.StopHgt2;

            allowances[LineType.Rebate_2].ModifiedLengthTxt = Utilities.InputData.Rebate2;
            allowances[LineType.Rebate_2].Displacement = Utilities.InputData.Rebate2 - JsonData.ProfileInfo.Rebate2;

            allowances[LineType.Architrave_2].ModifiedLengthTxt = Utilities.InputData.Architrave2;
            allowances[LineType.Architrave_2].Displacement = Utilities.InputData.Architrave2 - JsonData.ProfileInfo.Architrave2;

            allowances[LineType.Return_2].ModifiedLengthTxt = Utilities.InputData.Return2;
            allowances[LineType.Return_2].Displacement = Utilities.InputData.Return2 - JsonData.ProfileInfo.Return2;
        }

        protected void AssignTopDisplacement()
        {
            var calc = (Utilities.InputData.Return1 - JsonData.TopViewDataList[LineType.Return_1].BendAllowance) +
                       (Utilities.InputData.Return2 - JsonData.TopViewDataList[LineType.Return_2].BendAllowance) +
                       (Utilities.InputData.Architrave1 - JsonData.TopViewDataList[LineType.Architrave_1].BendAllowance) +
                       (Utilities.InputData.Architrave2 - JsonData.TopViewDataList[LineType.Architrave_2].BendAllowance) +
                       (JsonData.TopViewData.ConstVLine3 + JsonData.TopViewData.ConstVLine3);

            int index = 0;
            var totalHeight = Point3D.Distance(JsonData.TopViewDataList[index].Line.StartPoint,
                                                     JsonData.TopViewDataList[JsonData.TopViewDataList.Count-1].Line.EndPoint);

            var minWidth = totalHeight - calc;

            var allowances = JsonData.TopViewDataList;
            allowances[LineType.Return_1].ModifiedLengthTxt = Utilities.InputData.Return1;
            allowances[LineType.Return_1].Displacement = Utilities.InputData.Return1 - JsonData.ProfileInfo.Return1;

            allowances[LineType.Architrave_1].ModifiedLengthTxt = Utilities.InputData.Architrave1;
            allowances[LineType.Architrave_1].Displacement = Utilities.InputData.Architrave1 - JsonData.ProfileInfo.Architrave1;

            allowances[LineType.Rebate_1].ModifiedLengthTxt = JsonData.TopViewData.ConstVLine3;
            allowances[LineType.Rebate_1].Displacement = 0;

            allowances[LineType.StopHgt_1].ModifiedLengthTxt = JsonData.TopViewData.ConstHLine4;
            allowances[LineType.StopHgt_1].Displacement = 0;

            allowances[LineType.Throat].ModifiedLengthTxt = JsonData.TopViewData.MidWidth;
            allowances[LineType.Throat].Displacement = minWidth = JsonData.TopViewData.MidWidth;

            allowances[LineType.StopHgt_2].ModifiedLengthTxt = JsonData.TopViewData.ConstHLine4;
            allowances[LineType.StopHgt_2].Displacement = 0;

            allowances[LineType.Rebate_2].ModifiedLengthTxt = JsonData.TopViewData.ConstVLine3;
            allowances[LineType.Rebate_2].Displacement = 0;

            allowances[LineType.Architrave_2].ModifiedLengthTxt = Utilities.InputData.Architrave2;
            allowances[LineType.Architrave_2].Displacement = Utilities.InputData.Architrave2 - JsonData.ProfileInfo.Architrave2;

            allowances[LineType.Return_2].ModifiedLengthTxt = Utilities.InputData.Return2;
            allowances[LineType.Return_2].Displacement = Utilities.InputData.Return2 - JsonData.ProfileInfo.Return2;
        }
        
        public abstract void CreateTopView();

        public abstract void CreateLockView();

        protected virtual void CreateLeftProfile()
        {

        }

        protected virtual void CreateRightProfile()
        {

        }

        protected virtual void CreateTab()
        {

        }

        protected virtual void CreateLock()
        {

        }

        protected virtual void CreateHoles()
        {

        }

        protected virtual void CreateHinges()
        {

        }

        protected virtual void CreateSlots()
        {

        }
        
        public abstract void CreateHingeView();
        

        protected List<Line> DrawPocketLines(Point3D basePoint, double height, double width)
        {
            var p1 = new Point3D { X = basePoint.X, Y = basePoint.Y };
            var p2 = new Point3D { X = p1.X, Y = p1.Y + height };
            var p3 = new Point3D { X = p2.X + width, Y = p2.Y };
            var p4 = new Point3D { X = p1.X + width, Y = p1.Y };

            List<Line> lines=new List<Line>()
            {
                new Line(p1,p2),
                new Line(p2,p3),
                new Line(p3,p4),
                new Line(p4,p1),
            };

            return lines;
        }
    }
}