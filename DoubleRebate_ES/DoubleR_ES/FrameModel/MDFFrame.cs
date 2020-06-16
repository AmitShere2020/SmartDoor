﻿using System;
using System.Collections.Generic;
using System.Linq;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using DoubleR_ES.Models;

namespace DoubleR_ES.FrameModel
{
    public class MDFFrame : Frame
    {
        private Point3D rightProfileTop;
        private Point3D rightProfileBottom;

        public MDFFrame(JsonData jsonData)
        {
            JsonData = jsonData;
            AssignHingeDisplacements();
        }

        #region HingeView

        public override void CreateHingeView()
        {
            CreateLeftProfile();
            CreateTab();
            CreateRightProfile();
            CreateHoles();
            CreateHinges();
            CreateSlots();
        }

        protected override void CreateLeftProfile()
        {
            var requiredDat = JsonData.BendDataList.Where(item => item.ModifiedLengthTxt >= item.BendAllowance)
                .ToList();
            RequiredList = requiredDat;

            Utilities.TranslateEntities(requiredDat);

            var leftEntities = requiredDat.Select(item => item.Line).ToList();
            HingEntities.AddRange(leftEntities);
        }

        private List<Point3D> GetTabPoints(List<BendData> bendDatas)
        {
            double factor1 = Utilities.InputData.Rebate1 > 48 ? 16.5 : 15.5;
            double factor2 = Utilities.InputData.Rebate2 > 48 ? 16.5 : 15.5;

            double tabBase = JsonData.Tab.TabBase;
            List<Point3D> tabBasePointList = new List<Point3D>();

            double newArchitraveX1 = bendDatas[2].Line.StartPoint.X;
            double newArchitraveY1 = bendDatas[2].Line.StartPoint.Y;

            double newStopHgtX1 = bendDatas[4].Line.StartPoint.X;
            double newStopHgtY1 = bendDatas[4].Line.StartPoint.Y;

            var newArchitraveX2 = bendDatas[bendDatas.Count - 2].Line.StartPoint.X;
            var newRebateY2 = bendDatas[bendDatas.Count - 2].Line.StartPoint.Y;

            double newMidThroatDepth = (bendDatas[5].Line.StartPoint.Y - bendDatas[4].Line.StartPoint.Y) / 2;

            tabBasePointList.Add(new Point3D { X = newArchitraveX1, Y = newArchitraveY1 + factor1 });
            tabBasePointList.Add(new Point3D { X = newStopHgtX1, Y = newStopHgtY1 + newMidThroatDepth - tabBase / 2 });
            tabBasePointList.Add(new Point3D { X = newArchitraveX2, Y = newRebateY2 - factor2 - tabBase });

            return tabBasePointList;
        }

        protected override void CreateTab()
        {
            var basePoints = GetTabPoints(JsonData.BendDataList);
            var tabList = new List<Entity>();
            var baseLines = new List<Line>()
            {
                JsonData.BendDataList[2].Line,
                JsonData.BendDataList[4].Line,
                JsonData.BendDataList[6].Line,
            };

            for (var i = 0; i < basePoints.Count; i++)
            {
                var basePoint = basePoints[i];
                var diffY = (JsonData.Tab.TabBase - JsonData.Tab.TabTop) / 2;

                var p1 = new Point3D { X = basePoint.X, Y = basePoint.Y };
                var p2 = new Point3D { X = p1.X - JsonData.Tab.TabBase, Y = p1.Y + diffY };
                var p3 = new Point3D { X = p2.X, Y = p2.Y + JsonData.Tab.TabTop };
                var p4 = new Point3D { X = p1.X, Y = p1.Y + JsonData.Tab.TabBase };

                tabList.AddRange(Utilities.CreateLines(new List<Point3D>() { p1, p2, p3, p4 }));

                baseLines[i].SplitBy(new List<Point3D> { p1, p4 }, out ICurve[] splitedCurves);

                HingEntities.Add(splitedCurves[0] as Entity);
                HingEntities.Add(splitedCurves[2] as Entity);

            }

            var tab1Line = HingEntities[2];
            var tab2Line = HingEntities[4];
            var tab3Line = HingEntities[6];
            HingEntities.Remove(tab1Line);
            HingEntities.Remove(tab2Line);
            HingEntities.Remove(tab3Line);

            HingEntities.AddRange(tabList);
        }

        protected override void CreateRightProfile()
        {
            var firstPoint = RequiredList[0].Line.StartPoint;
            var secondPoint = RequiredList[RequiredList.Count - 1].Line.EndPoint;

            var width = Point3D.Distance(firstPoint, secondPoint);
            double totalLength1 = Utilities.InputData.RevealHeight + Utilities.InputData.Architrave1 - JsonData.BendDataList[1].BendAllowance;
            double totalLength2 = Utilities.InputData.RevealHeight + Utilities.InputData.Architrave2 - JsonData.BendDataList[7].BendAllowance;

            var thirdPoint = new Point3D(firstPoint.X + totalLength1, firstPoint.Y);
            var fourthPoint = new Point3D(firstPoint.X + totalLength2, firstPoint.Y + width);

            Line firstLine = new Line(firstPoint, thirdPoint);
            Line secondLine = new Line(secondPoint, fourthPoint);
            Line thirdLine = new Line(thirdPoint, fourthPoint);

            HingEntities.Add(firstLine);
            HingEntities.Add(secondLine);
            HingEntities.Add(thirdLine);

            rightProfileTop = fourthPoint;
            rightProfileBottom = thirdPoint;

        }

        private List<Point3D> GetFrmFixedPoints(double length)
        {
            List<Point3D> holePointList = new List<Point3D>();

            double circle1X = (Utilities.InputData.Architrave1 - JsonData.BendDataList[1].BendAllowance) + (Utilities.InputData.StopHgt1 - JsonData.BendDataList[3].BendAllowance) + 175; // New enhancement

            double circle1Y = (Utilities.InputData.Return1 - JsonData.BendDataList[0].BendAllowance) + (Utilities.InputData.Architrave1 - JsonData.BendDataList[1].BendAllowance) +
                              (Utilities.InputData.Rebate1 - JsonData.BendDataList[2].BendAllowance) + ((Utilities.InputData.Throat - JsonData.BendDataList[4].BendAllowance) / 2.0);

            double circle2X = circle1X + 525;

            if (length < 2200) // 4 holes
            {
                // Hole 4
                double circle4X = length - 175;

                //Hole 3
                double circle3X = circle4X - 525;


                holePointList.Add(new Point3D { X = circle1X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle2X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle3X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle4X, Y = circle1Y });
            }

            else if (length < 2700)  // 5 holes
            {
                //Hole 5
                double circle5X = length - 175;

                // Hole 4
                double circle4X = circle5X - 525;

                //Hole 3
                double circle3X = circle1X + ((circle5X - circle1X) / 2);

                holePointList.Add(new Point3D { X = circle1X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle2X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle3X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle4X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle5X, Y = circle1Y });

            }

            else if (length > 2700) // 6 holes
            {
                // Hole 3
                double circle3X = circle2X + 525;

                //Hole 6
                double circle6X = length - 175;

                //Hole 5
                double circle5X = circle6X - 525;

                // Hole 4
                double circle4X = circle1X + ((circle6X - circle1X) / 2);


                holePointList.Add(new Point3D { X = circle1X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle2X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle3X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle4X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle5X, Y = circle1Y });
                holePointList.Add(new Point3D { X = circle6X, Y = circle1Y });
            }

            return holePointList;
        }
        
        protected override void CreateHoles()
        {
            double totalLength = Utilities.InputData.RevealHeight + Utilities.InputData.Architrave1;
            var holePoints = GetFrmFixedPoints(totalLength);

            double radius = 0;
            if (Utilities.InputData.FrameFixing == "D/B-7.0")
            {
                radius = 3.5;
            }
            else if (Utilities.InputData.FrameFixing == "D/B-3.5")
            {
                radius = 1.8;
            }
            else
            {
                radius = 0;
            }

            if (radius != 0)
            {
                foreach (Point3D point in holePoints)
                {
                    Circle circle = new Circle(point, radius);
                    HingEntities.Add(circle);
                }
            }

        }

        private List<Point3D> GetHingPoints(double length, double hingeFeatLength, double hingeFeatWidth)
        {
            double hingeFeatBtmDistance = 250;

            List<Point3D> hingesPointList = new List<Point3D>();

            double factor1 = 0;

            double gaugeVale = Utilities.InputData.GaugeSize;

            switch (gaugeVale)
            {

                case 1.1:
                    factor1 = 2.4;
                    break;

                case 1.5:
                    factor1 = 2.9;
                    break;
            }


            // Hinge 1
            double hinge1X = Utilities.InputData.Architrave1 - JsonData.BendDataList[1].BendAllowance + 230;

            double hinge1Y = (Utilities.InputData.Return1 - JsonData.BendDataList[0].BendAllowance) + (Utilities.InputData.Architrave1 - JsonData.BendDataList[1].BendAllowance) - factor1;

            //Hinge 2
            double hinge2X = length - hingeFeatBtmDistance - hingeFeatLength;
            double hinge2Y = hinge1Y;

            //Hinge 3
            double hinge3X = hinge1X + ((hinge2X - hinge1X) / 2);
            double hinge3Y = hinge1Y;

            //Hinge 4
            double hinge4X = hinge1X + hingeFeatLength + 99.5;
            double hinge4Y = hinge1Y;

            //Hinge 5 A
            double hinge5X = hinge2X - hingeFeatLength - 99.5;
            double hinge5Y = hinge1Y;

            ////Hinge 5 B
            //double hinge5X = hinge4X + hingeFeatLength + 99.5;
            //double hinge5Y = hinge1Y;

            hingesPointList.Add(new Point3D { X = hinge1X, Y = hinge1Y });
            hingesPointList.Add(new Point3D { X = hinge2X, Y = hinge2Y });
            hingesPointList.Add(new Point3D { X = hinge3X, Y = hinge3Y });
            hingesPointList.Add(new Point3D { X = hinge4X, Y = hinge4Y });
            hingesPointList.Add(new Point3D { X = hinge5X, Y = hinge5Y });


            return hingesPointList;
        }

        protected override void CreateHinges()
        {
            double hingeFeatLength = 100.5;
            double hingeFeatWidth = 32.5;
            double totalLength = Utilities.InputData.RevealHeight + Utilities.InputData.Architrave1;

            var hingePoints = GetHingPoints(totalLength, hingeFeatLength, hingeFeatWidth);
            for (int i = 0; i < Utilities.InputData.HingeQty; i++)
            {
                var hingeLines = DrawPocketLines(hingePoints[i], hingeFeatWidth, hingeFeatLength);
                HingEntities.AddRange(hingeLines);
            }
        }

        private List<Point3D> GetSlotPoints(double length, double tabBase, double slotHeight, double slotWidth)
        {
            double factor1 = Utilities.InputData.Rebate1 > 48 ? 16.5 : 15.5;

            double factor2 = Utilities.InputData.Rebate2 > 48 ? 16.5 : 15.5;
            List<Point3D> slotBasePointList = new List<Point3D>();

            double btmX = length - (4 + slotWidth);
            double btmY;
            double topX = btmX;
            double topY = 0;

            if (Math.Abs(Utilities.InputData.Return1) > 0)
            {
                btmY =  RequiredList[1].Line.EndPoint.Y + factor1 + (tabBase / 2) - (slotHeight / 2);
               
            }
            else
            {
                btmY = RequiredList[0].Line.EndPoint.Y + factor1 + (tabBase / 2) - (slotHeight / 2);
            }


            if (Math.Abs(Utilities.InputData.Return2) > 0 && Math.Abs(Utilities.InputData.Architrave2) > 0)
            {
                topY = RequiredList[7].Line.StartPoint.Y - factor2 - (tabBase / 2) - (slotHeight / 2);

            }
            else
            {
                topY = rightProfileTop.Y - factor2 - (tabBase / 2) - (slotHeight / 2);
            }


            //double topY = (Utilities.InputData.Return2 - JsonData.BendDataList[JsonData.BendDataList.Count - 1].BendAllowance) +
            //              (Utilities.InputData.Architrave2 - JsonData.BendDataList[JsonData.BendDataList.Count - 2].BendAllowance)
            //              + factor2 + (tabBase / 2) + (slotHeight / 2);

            //topY = width - topY;
            slotBasePointList.Add(new Point3D { X = btmX, Y = btmY });
            slotBasePointList.Add(new Point3D { X = topX, Y = topY });

            return slotBasePointList;
        }

        protected override void CreateSlots()
        {
            double slotHeight = 20;
            double slotWidth = 2.5;
            double totalLength = Utilities.InputData.RevealHeight + Utilities.InputData.Architrave1;

            var slotPoints = GetSlotPoints(totalLength, JsonData.Tab.TabBase, slotHeight, slotWidth);
            foreach (var point in slotPoints)
            {
                var slotLines = DrawPocketLines(point, slotHeight, slotWidth);
                HingEntities.AddRange(slotLines);
            }

        }
        
        #endregion

        #region LockView

        public override void CreateLockView()
        {
            HingEntities.Clear();
            CreateLeftProfile();
            CreateTab();
            CreateRightProfile();
            CreateHoles();
            CreateLock();
            CreateSlots();
            LockEntities.AddRange(HingEntities);
            HingEntities.Clear();
        }

        protected override void CreateLock()
        {
            double totalLength = Utilities.InputData.RevealHeight + Utilities.InputData.Architrave1;
            DrawLockProfile(totalLength);
        }

        private void DrawLockProfile(double totalLength)
        {
            double L1 = 38.5;
            double H1 = Utilities.InputData.Rebate1 > 45 ? 11 : 9;
            double L2 = 70.5;
            double H2 = 29;

            double basePointX = totalLength - Utilities.InputData.StrikeHeight - (L1 / 2.0);
            double basePointY = (Utilities.InputData.Return1 - JsonData.BendDataList[0].BendAllowance) +
                (Utilities.InputData.Architrave1 - JsonData.BendDataList[1].BendAllowance) - 3.9;

            Point3D p1 = new Point3D { X = basePointX, Y = basePointY };
            Point3D p2 = new Point3D { X = p1.X, Y = p1.Y + H1 };
            Point3D p3 = new Point3D { X = p2.X - ((L2 - L1) / 2.0), Y = p2.Y };
            Point3D p4 = new Point3D { X = p3.X, Y = p3.Y + H2 };
            Point3D p5 = new Point3D { X = p4.X + L2, Y = p4.Y };
            Point3D p6 = new Point3D { X = p5.X, Y = p3.Y };
            Point3D p7 = new Point3D { X = p6.X - ((L2 - L1) / 2.0), Y = p2.Y };
            Point3D p8 = new Point3D { X = p1.X + L1, Y = p1.Y };

            var lockLines = Utilities.CreateLines(new List<Point3D>() { p1, p2, p3, p4, p5, p6, p7, p8 }, true);
            lockLines.Add(new Line(p2, p7));

            HingEntities.AddRange(lockLines);
        }

        #endregion

        #region TopView

        public override void CreateTopView()
        {
            CreateTopLeftProfile();
            CreateTopSlots();
            CreateMirrorProfile();
        }

        private void CreateTopLeftProfile()
        {
            AssignTopDisplacement();
            Utilities.TranslateEntities(JsonData.TopViewDataList);
            var leftEntities = JsonData.TopViewDataList.Select(item => item.Line).ToList();
            TopEntities.AddRange(leftEntities);
        }

        private void CreateTopSlots()
        {

        }

        private void CreateMirrorProfile()
        {
            var totalHeight = Point3D.Distance(JsonData.TopViewDataList[0].Line.StartPoint,
                JsonData.TopViewDataList[8].Line.EndPoint);

            Point3D bottomBasePoint = new Point3D(Utilities.InputData.RevealWidth / 2, 0, 0);
            Point3D topBasePoint = new Point3D(Utilities.InputData.RevealWidth / 2, totalHeight, 0);

            Line topLine = new Line(JsonData.TopViewDataList[0].Line.StartPoint, bottomBasePoint);
            Line bottomLine = new Line(JsonData.TopViewDataList[8].Line.EndPoint, topBasePoint);
            TopEntities.Add(topLine);
            TopEntities.Add(bottomLine);

            Plane mirrorPlane = new Plane(bottomBasePoint, new Vector3D(1, 0, 0));

            List<Entity> mirroredEntities = new List<Entity>();
            Transformation mirror = new Mirror(mirrorPlane);

            foreach (var topEntity in TopEntities)
            {
                Entity entity = topEntity.Clone() as Entity;
                entity.TransformBy(mirror);
                mirroredEntities.Add(entity);
            }

            TopEntities.AddRange(mirroredEntities);
        }
        
        #endregion

    }
}