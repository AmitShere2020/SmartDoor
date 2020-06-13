using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using devDept.Geometry;
using DoubleR_ES.Models;
using IntelliCAD;
using Entity = devDept.Eyeshot.Entities.Entity;
using Line = devDept.Eyeshot.Entities.Line;
using Utility = devDept.Geometry.Utility;

namespace DoubleR_ES
{
    internal static class Utilities
    {
        public static InputData InputData { get; set; }

        /// <summary>
        /// Get distinct vertices of lines
        /// </summary>
        /// <param name="profLines"></param>
        /// <returns></returns>
        public static List<Point3D> GetVertices(List<Line> profLines)
        {
            List<Point3D> points = new List<Point3D>();
            foreach (var line in profLines)
            {
                if (!points.Exists(point=>point.Equals(line.StartPoint)))
                    points.Add(line.StartPoint);
                if (!points.Exists(point => point.Equals(line.EndPoint)))
                    points.Add(line.EndPoint);
            }

            return points;
        }

        public static List<Line> CreateLines(List<Point3D> listOfPoints, bool closed = false)
        {
            var lines = new List<Line>();
            for (var i = 0; i < listOfPoints.Count; i++)
            {
                var startPoint = listOfPoints[i];
                if (i + 1 == listOfPoints.Count)
                {
                    if (closed)
                    {
                        var end = listOfPoints[0];
                        lines.Add(new Line(startPoint, end));
                    }
                    break;
                }
                var endPoint = listOfPoints[i + 1];
                var line = new Line(startPoint,endPoint);
                lines.Add(line);
            }
            return lines;
        }

        public static Application IcadApplication { get; set; }
        public static Document ActiveDocument { get; set; }
        public static ModelSpace ModelSpace { get; set; }

        public static void InitializeCoreComponents()
        {
            var progId = "icad.application";

            try
            {
                IcadApplication = (Application)Marshal.GetActiveObject(progId);
            }
            catch
            {
                IcadApplication = (Application)Activator.CreateInstance(Type.GetTypeFromProgID(progId));
            }
            IcadApplication.Visible = true;
        }

        public static void CloningDxf(List<Line> lines)
        {
            InitializeCoreComponents();
            // open  new file here
            ActiveDocument=IcadApplication.Documents.Add();

            foreach (var line in lines)
            {
                var start = line.StartPoint;
                var end = line.EndPoint;
                var icadline = ActiveDocument.ModelSpace.AddLine(new Point() {x = start.X, y = start.Y},
                    new Point() {x = end.X, y = end.Y});
                icadline.Update();
            }
           
        }

        public static void TranslateEntities(List<BendData> bendList)
        {
            var lines=new List<Line>();
            for (var i = 0; i < bendList.Count; i++)
            {
                var line = bendList[i].Line;
                var disp = bendList[i].Displacement;
                if (disp==0)
                {
                    continue;
                }
                var angle = Utility.RadToDeg(line.Direction.AngleInXY);
                var tempPoint = line.EndPoint.Clone() as Point3D;

                if (angle % 90 == 0)
                {
                    line.EndPoint = line.PointAt(line.Length() + bendList[i].Displacement);
                }
                else
                {
                    line.EndPoint = line.PointAt(line.Length() + bendList[i].Displacement * 1.4142); // only for 45 Degree lines .. cos(45)*(disp+disp)
                }

                var dispVector = new Vector3D(tempPoint, line.EndPoint);

                for (int j = i + 1; j < bendList.Count; j++)
                {
                    bendList[j].Line.Translate(dispVector);
                }
            }

        }
    }
        


    }



    // Json Storage data
