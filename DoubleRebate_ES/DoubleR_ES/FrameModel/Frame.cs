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

        protected List<BendData> RequiredList { get; set; }

        public JsonData JsonData { get; protected set; }

        protected Frame()
        {
            HingEntities = new List<Entity>();
            TopEntities = new List<Entity>();
            LockEntities = new List<Entity>();
        }

        protected void AssignHingeDisplacements()
        {
            var allowances = JsonData.BendDataList;
            allowances[0].ModifiedLengthTxt = Utilities.InputData.Return1;
            allowances[0].Displacement = Utilities.InputData.Return1 - JsonData.ProfileInfo.Return1;

            allowances[1].ModifiedLengthTxt = Utilities.InputData.Architrave1;
            allowances[1].Displacement = Utilities.InputData.Architrave1 - JsonData.ProfileInfo.Architrave1;

            allowances[2].ModifiedLengthTxt = Utilities.InputData.Rebate1;
            allowances[2].Displacement = Utilities.InputData.Rebate1 - JsonData.ProfileInfo.Rebate1;

            allowances[3].ModifiedLengthTxt = Utilities.InputData.StopHgt1;
            allowances[3].Displacement = Utilities.InputData.StopHgt1 - JsonData.ProfileInfo.StopHgt1;

            allowances[4].ModifiedLengthTxt = Utilities.InputData.Throat;
            allowances[4].Displacement = Utilities.InputData.Throat - JsonData.ProfileInfo.Throat;

            allowances[5].ModifiedLengthTxt = Utilities.InputData.StopHgt2;
            allowances[5].Displacement = Utilities.InputData.StopHgt2 - JsonData.ProfileInfo.StopHgt2;

            allowances[6].ModifiedLengthTxt = Utilities.InputData.Rebate2;
            allowances[6].Displacement = Utilities.InputData.Rebate2 - JsonData.ProfileInfo.Rebate2;

            allowances[7].ModifiedLengthTxt = Utilities.InputData.Architrave2;
            allowances[7].Displacement = Utilities.InputData.Architrave2 - JsonData.ProfileInfo.Architrave2;

            allowances[8].ModifiedLengthTxt = Utilities.InputData.Return2;
            allowances[8].Displacement = Utilities.InputData.Return2 - JsonData.ProfileInfo.Return2;
        }

        protected void AssignTopDisplacement()
        {
            var calc = (Utilities.InputData.Return1 - JsonData.TopViewDataList[0].BendAllowance) +
                       (Utilities.InputData.Return2 - JsonData.TopViewDataList[8].BendAllowance) +
                       (Utilities.InputData.Architrave1 - JsonData.TopViewDataList[1].BendAllowance) +
                       (Utilities.InputData.Architrave2 - JsonData.TopViewDataList[7].BendAllowance) +
                       (JsonData.TopViewData.ConstVLine3 + JsonData.TopViewData.ConstVLine3);

            var totalHeight = Point3D.Distance(JsonData.TopViewDataList[0].Line.StartPoint,
                                                     JsonData.TopViewDataList[8].Line.EndPoint);

            var minWidth = totalHeight - calc;

            var allowances = JsonData.TopViewDataList;
            allowances[0].ModifiedLengthTxt = Utilities.InputData.Return1;
            allowances[0].Displacement = Utilities.InputData.Return1 - JsonData.ProfileInfo.Return1;

            allowances[1].ModifiedLengthTxt = Utilities.InputData.Architrave1;
            allowances[1].Displacement = Utilities.InputData.Architrave1 - JsonData.ProfileInfo.Architrave1;

            allowances[2].ModifiedLengthTxt = JsonData.TopViewData.ConstVLine3;
            allowances[2].Displacement = 0;

            allowances[3].ModifiedLengthTxt = JsonData.TopViewData.ConstHLine4;
            allowances[3].Displacement = 0;

            allowances[4].ModifiedLengthTxt = JsonData.TopViewData.MidWidth;
            allowances[4].Displacement = minWidth = JsonData.TopViewData.MidWidth;

            allowances[5].ModifiedLengthTxt = JsonData.TopViewData.ConstHLine4;
            allowances[5].Displacement = 0;

            allowances[6].ModifiedLengthTxt = JsonData.TopViewData.ConstVLine3;
            allowances[6].Displacement = 0;

            allowances[7].ModifiedLengthTxt = Utilities.InputData.Architrave2;
            allowances[7].Displacement = Utilities.InputData.Architrave2 - JsonData.ProfileInfo.Architrave2;

            allowances[8].ModifiedLengthTxt = Utilities.InputData.Return2;
            allowances[8].Displacement = Utilities.InputData.Return2 - JsonData.ProfileInfo.Return2;
        }

        public abstract void CreateHingeView();
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